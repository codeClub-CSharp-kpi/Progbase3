using EntitiesLibrary;
using MoiveHubSystem.Commands;
using MoiveHubSystem.Views;
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

		

		private ReviewRepository _reviewRepository = new();
		private ReviewAccountRepository _reviewAccountRepository = new();
		private AccountRepository _accountRepository = new();

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
				int total = _reviewRepository.GetAll().Count() / AmountOfInPageElements;
				if (_reviewRepository.GetAll().Count() % AmountOfInPageElements != 0)
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
					_currentAccountId = _accountRepository.GetAll().Where(obj => obj.Login == authorOfReviewLogin).FirstOrDefault().Id;

					var recentlyAddedRev = _reviewRepository.GetAll().Where(obj => obj.Title == addWnd.titleField.Text).FirstOrDefault();
					_reviewAccountRepository.Insert(new ReviewAccount()
					{
						AccountId = _currentAccountId,
						ReviewId = recentlyAddedRev.Id
					});
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
					var relationsToDel = _reviewAccountRepository.GetAll().Where(obj => obj.ReviewId == SelectedReview.Id);

					foreach (var item in relationsToDel)
					{
						_reviewAccountRepository.Delete(item.Id);
					}
					
					
					_reviewRepository.Delete(SelectedReview.Id);

					RefillObservedReviews();
				}
			}, obj => {

				int _currentAccountId = default;

				string authorOfReviewLogin = (obj as ReviewsWindow).userName.Text;
				if (!String.IsNullOrEmpty(authorOfReviewLogin))
				{
					_currentAccountId = _accountRepository.GetAll().Where(obj => obj.Login == authorOfReviewLogin).FirstOrDefault().Id;
				}


				bool reviewIsSelected = false;
				if (SelectedReview != null)
				{
					reviewIsSelected = true;
				}

				var reviewIDsOfCurrentAccount = _reviewAccountRepository.GetAll().Where(obj => obj.AccountId == _currentAccountId).Select(obj => obj.ReviewId);

				bool isAccountsReview = false;
				if (SelectedReview != null && reviewIDsOfCurrentAccount.Contains(SelectedReview.Id))
				{
					isAccountsReview = true;
				}


				var currAccount = _accountRepository.GetAll().Where(obj => obj.Id == _currentAccountId).FirstOrDefault();
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
					_currentAccountId = _accountRepository.GetAll().Where(obj => obj.Login == authorOfReviewLogin).FirstOrDefault().Id;
				}


				bool reviewIsSelected = false;
				if (SelectedReview != null)
				{
					reviewIsSelected = true;
				}

				var reviewIDsOfCurrentAccount = _reviewAccountRepository.GetAll().Where(obj => obj.AccountId == _currentAccountId).Select(obj => obj.ReviewId);

				bool isAccountsReview = false;
				if (SelectedReview != null && reviewIDsOfCurrentAccount.Contains(SelectedReview.Id))
				{
					isAccountsReview = true;
				}

				var currAccount = _accountRepository.GetAll().Where(obj => obj.Id == _currentAccountId).FirstOrDefault();
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
			return _reviewRepository.GetPage(AmountOfInPageElements, AmountOfInPageElements * (pageNumber - 1));
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
