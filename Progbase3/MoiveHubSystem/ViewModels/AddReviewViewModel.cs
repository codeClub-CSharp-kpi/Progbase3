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
	enum ReviewStatus
	{
		Negative = 0,
		Positive = 1
	}
	class AddReviewViewModel: INotifyPropertyChanged
	{
		public ObservableCollection<Film> PickList { get; set; } = new();
		public ObservableCollection<Film> FilmOnReview { get; set; } = new();

		public AddReviewViewModel()
		{
			RefillObservedFilms();
		}


		private Film _selectedFilm;
		public Film SelectedFilm
		{
			get
			{
				return _selectedFilm;
			}
			set
			{
				_selectedFilm = value;
				OnPropertyChanged(nameof(SelectedFilm));
			}
		}

		private string _title;
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
				OnPropertyChanged(nameof(Title));
			}
		}

		private double _rate;
		public double Rate
		{
			get
			{
				return _rate;
			}
			set
			{
				_rate = value;
				OnPropertyChanged(nameof(Rate));
			}
		}


		private ReviewStatus _reviewStatus;
		public ReviewStatus ReviewStatus
		{
			get
			{
				return _reviewStatus;
			}
			set
			{
				_reviewStatus = value;
				OnPropertyChanged(nameof(ReviewStatus));
			}
		}

		private string _reviewText;
		public string ReviewText
		{
			get
			{
				return _reviewText;
			}
			set
			{
				_reviewText = value;
				OnPropertyChanged(nameof(ReviewText));
			}
		}



		// Commands
		public ICommand ShiftToOnReview
		{
			get => new RelayCommand(obj =>
			{
				var filmInPickList = PickList.Where(obj => obj.Id == SelectedFilm?.Id).FirstOrDefault();
				PickList.Remove(filmInPickList);
				FilmOnReview.Add(filmInPickList);
			}, obj =>
			{
				return FilmOnReview.Where(obj => obj.Id == SelectedFilm?.Id).FirstOrDefault() == null && FilmOnReview.Count == 0;
			});
		}
		public ICommand ShiftFromOnReview
		{
			get => new RelayCommand(obj =>
			{
				var filmOnRev = FilmOnReview.Where(obj => obj.Id == SelectedFilm?.Id).FirstOrDefault();
				FilmOnReview.Remove(filmOnRev);
			});
		}
		public ICommand AcceptNewReview
		{
			get => new RelayCommand(obj =>
			{
				try
				{
					Review newReview = new()
					{
						Title = this.Title,
						Rate = this.Rate,
						isPositive = Convert.ToBoolean((int)ReviewStatus),
						FilmId = FilmOnReview.First().Id,
						ReviewText = this.ReviewText
					};
					TcpQueryManager.ExecQuery("AddReview", newReview);

					MessageBox.Show("New reivew has been added successfully!", "Info",
							MessageBoxButton.OK, MessageBoxImage.Information);
				}
				catch (Exception err)
				{
					MessageBox.Show($"{err.Message}", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
				(obj as AddReviewWindow).Close();
			}, obj => {

				bool isEmptyTitle = true;
				if (!string.IsNullOrWhiteSpace(Title))
				{
					isEmptyTitle = false;
				}

				bool isEmptyReviewText = true;
				if (!string.IsNullOrWhiteSpace(ReviewText))
				{
					isEmptyReviewText = false;
				}

				bool isNotSetReviewFilm= true;
				if (FilmOnReview.Count > 0)
				{
					isNotSetReviewFilm = false;
				}


				return !isEmptyTitle && !isEmptyReviewText && !isNotSetReviewFilm;
			});
		}

		// pagination commands
		public ICommand LoadNextPage
		{
			get => new RelayCommand(obj =>
			{
				++_currentPageCounter;
				RefillObservedFilms();

			}, obj => (_currentPageCounter < TotalPages));
		}
		public ICommand LoadPrevPage
		{
			get => new RelayCommand(obj =>
			{
				--_currentPageCounter;
				RefillObservedFilms();

			}, obj => (1 < _currentPageCounter));
		}

		// pagination attributes
		private int _currentPageCounter = 1;
		const int AmountOfInPageElements = 5;
		private int TotalPages
		{
			get
			{
				int total = (TcpQueryManager.ExecQuery("GetAllFilms") as IEnumerable<Film>).Count() / AmountOfInPageElements;
				if ((TcpQueryManager.ExecQuery("GetAllFilms") as IEnumerable<Film>).Count() % AmountOfInPageElements != 0)
				{
					return total + 1;
				}
				else
				{
					return total;
				}
			}
		}

		// supporting private methods
		private IEnumerable<Film> GetPageForList(int pageNumber)
		{
			return TcpQueryManager.ExecQuery("GetFilmsPage", AmountOfInPageElements, AmountOfInPageElements * (pageNumber - 1)) as IEnumerable<Film>;
		}
		private void RefillObservedFilms()
		{
			PickList.Clear();
			foreach (var item in GetPageForList(_currentPageCounter))
			{
				PickList.Add(item);
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
