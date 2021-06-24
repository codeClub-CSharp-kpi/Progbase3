using EntitiesLibrary;
using MoiveHubSystem.Commands;
using MoiveHubSystem.Views;
using NetManagers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MoiveHubSystem.ViewModels
{
	class ReviewsViewModel
	{
		const int AmountOfInPageElements = 5;

		public ObservableCollection<Review> Reviews { get; set; } = new ObservableCollection<Review>();

		public ReviewsViewModel()
		{
			RefillObservedReviews();
		}

		private Review _selectedReview;
		public Review SelectedReview
		{
			get
			{
				RevsFilm.Clear();
				RevsFilm.Add(_selectedReview?.Film);
				return _selectedReview;
			}
			set
			{
				_selectedReview = value;
				OnPropertyChanged(nameof(SelectedReview));
			}
		}


		public ObservableCollection<Film> RevsFilm { get; set; } = new();

		//
		private int TotalPages
		{
			get
			{
				int total = (TcpQueryManager.ExecQuery("GetAllReviews") as IEnumerable<Review>).Count() / AmountOfInPageElements;
				if ((TcpQueryManager.ExecQuery("GetAllReviews") as IEnumerable<Review>).Count() % AmountOfInPageElements != 0)
				{
					return total + 1;
				}
				else
				{
					return total;
				}
			}
		}

		// Commands
		public ICommand AddReview
		{
			get => new RelayCommand(obj =>
			{
				var addWnd = new AddReviewWindow();
				addWnd.ShowDialog();

				int _currentAccountId = default;

				string authorOfReviewLogin = (obj as ReviewsWindow).userName.Text;
				if (!String.IsNullOrEmpty(authorOfReviewLogin))
				{
					_currentAccountId = (TcpQueryManager.ExecQuery("GetAllAccounts") as IEnumerable<Account>).Where(obj => obj.Login == authorOfReviewLogin).FirstOrDefault().Id;

					var recentlyAddedRev = (TcpQueryManager.ExecQuery("GetAllReviews") as IEnumerable<Review>).Where(obj => obj.Title == addWnd.titleField.Text).FirstOrDefault();
					if (recentlyAddedRev != null)
					{
						TcpQueryManager.ExecQuery("AddReviewAccount", new ReviewAccount()
						{
							AccountId = _currentAccountId,
							ReviewId = recentlyAddedRev.Id
						});
					}
				}
			});
		}

		public ICommand DelReview
		{
			get => new RelayCommand(obj =>
			{
				MessageBoxResult userDecisionDelOrNotDel = MessageBox.Show("You're deleting the review! Sure?", "Earasing review", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
				if (userDecisionDelOrNotDel == MessageBoxResult.OK)
				{
					var relationsToDel = (TcpQueryManager.ExecQuery("GetAllReviewsAccounts") as IEnumerable<ReviewAccount>).Where(obj => obj.ReviewId == SelectedReview.Id);

					foreach (var item in relationsToDel)
					{
						TcpQueryManager.ExecQuery("DelReviewAccount", item.Id);
					}

					TcpQueryManager.ExecQuery("DeleteReview", SelectedReview.Id);

					RefillObservedReviews();
				}
			}, obj => {

				int _currentAccountId = default;

				string authorOfReviewLogin = (obj as ReviewsWindow).userName.Text;
				if (!String.IsNullOrEmpty(authorOfReviewLogin))
				{
					_currentAccountId = (TcpQueryManager.ExecQuery("GetAllAccounts") as IEnumerable<Account>).Where(obj => obj.Login == authorOfReviewLogin).FirstOrDefault().Id;
				}


				bool reviewIsSelected = false;
				if (SelectedReview != null)
				{
					reviewIsSelected = true;
				}

				var reviewIDsOfCurrentAccount = (TcpQueryManager.ExecQuery("GetAllReviewsAccounts") as IEnumerable<ReviewAccount>).Where(obj => obj.AccountId == _currentAccountId).Select(obj => obj.ReviewId);

				bool isAccountsReview = false;
				if (SelectedReview != null && reviewIDsOfCurrentAccount.Contains(SelectedReview.Id))
				{
					isAccountsReview = true;
				}


				var currAccount = (TcpQueryManager.ExecQuery("GetAllAccounts") as IEnumerable<Account>).Where(obj => obj.Id == _currentAccountId).FirstOrDefault();
				if (currAccount?.RoleId == (int)Role_Id.Moderator)
				{
					isAccountsReview = true;
				}

				return reviewIsSelected && isAccountsReview;
			});
		}

		public ICommand EditReview
		{
			get => new RelayCommand(obj =>
			{
				EditReviewsWindow editActorWindow = new(SelectedReview);
				editActorWindow.ShowDialog();
			},obj => {

				int _currentAccountId = default;

				string authorOfReviewLogin = (obj as ReviewsWindow).userName.Text;
				if (!String.IsNullOrEmpty(authorOfReviewLogin))
				{
					_currentAccountId = (TcpQueryManager.ExecQuery("GetAllAccounts") as IEnumerable<Account>).Where(obj => obj.Login == authorOfReviewLogin).FirstOrDefault().Id;
				}


				bool reviewIsSelected = false;
				if (SelectedReview != null)
				{
					reviewIsSelected = true;
				}

				var reviewIDsOfCurrentAccount = (TcpQueryManager.ExecQuery("GetAllReviewsAccounts") as IEnumerable<ReviewAccount>).Where(obj => obj.AccountId == _currentAccountId).Select(obj => obj.ReviewId);

				bool isAccountsReview = false;
				if (SelectedReview != null && reviewIDsOfCurrentAccount.Contains(SelectedReview.Id))
				{
					isAccountsReview = true;
				}

				var currAccount = (TcpQueryManager.ExecQuery("GetAllAccounts") as IEnumerable<Account>).Where(obj => obj.Id == _currentAccountId).FirstOrDefault();
				if (currAccount?.RoleId == (int)Role_Id.Moderator)
				{
					isAccountsReview = true;
				}

				return reviewIsSelected && isAccountsReview;
			});
		}

		public ICommand LoadNextPage
		{
			get => new RelayCommand(obj =>
			{
				++_currentPageCounter;
				RefillObservedReviews();

			}, obj => (_currentPageCounter < TotalPages));
		}

		public ICommand LoadPrevPage
		{
			get => new RelayCommand(obj =>
			{
				--_currentPageCounter;
				RefillObservedReviews();

			}, obj => (1 < _currentPageCounter));
		}

		// current page counter
		private int _currentPageCounter = 1;

		// supporting private methods
		private IEnumerable<Review> GetPageForList(int pageNumber)
		{
			return TcpQueryManager.ExecQuery("GetReviewsPage", AmountOfInPageElements, AmountOfInPageElements * (pageNumber - 1)) as IEnumerable<Review>;
		}
		private void RefillObservedReviews()
		{
			Reviews.Clear();
			foreach (var item in GetPageForList(_currentPageCounter))
			{
				Reviews.Add(item);
			}
		}

		//
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
