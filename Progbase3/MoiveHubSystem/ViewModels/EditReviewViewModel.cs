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
	class EditReviewViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Film> PickList { get; set; } = new();
		public ObservableCollection<Film> FilmOnReview { get; set; } = new();

		private Review _preChnagedOriginal;

		public EditReviewViewModel(Review reviewToEdit)
		{
			_preChnagedOriginal = reviewToEdit;

			Title = reviewToEdit.Title;
			Rate = reviewToEdit.Rate;
			RevStatus = Convert.ToInt32(reviewToEdit.isPositive) == 1 ? ReviewStatus.Positive : ReviewStatus.Negative;
			ReviewText = reviewToEdit.ReviewText;
			FilmOnReview.Add(reviewToEdit.Film);

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

		private double _rate = 1;
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
		public ReviewStatus RevStatus
		{
			get
			{
				return _reviewStatus;
			}
			set
			{
				_reviewStatus = value;
				OnPropertyChanged(nameof(RevStatus));
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

		private Review _updatedReview;

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
					_updatedReview = new()
					{
						Id = _preChnagedOriginal.Id,
						Title = this.Title,
						Rate = this.Rate,
						isPositive = Convert.ToBoolean((int)RevStatus),
						FilmId = FilmOnReview.First().Id,
						ReviewText = this.ReviewText
					};
					TcpQueryManager.ExecQuery("UpdateReview", _updatedReview);

					MessageBox.Show("Updated Successfully", "Info",
						MessageBoxButton.OK, MessageBoxImage.Information);
				}
				catch (Exception err)
				{
					MessageBox.Show($"{err.Message}", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
				(obj as EditReviewsWindow).Close();
			}, obj => {

				bool isChangedTitle = false;
				if (_preChnagedOriginal.Title != Title)
				{
					isChangedTitle = true;
				}

				bool isChangedRate = false;
				if (_preChnagedOriginal.Rate !=  Rate)
				{
					isChangedRate = true;
				}

				bool isChangedReviewStatus = false;
				if (_preChnagedOriginal.isPositive !=  Convert.ToBoolean((int)RevStatus))
				{
					isChangedReviewStatus = true;
				}

				bool isChangedFilmOnReview = false;
				if ( _preChnagedOriginal.FilmId != FilmOnReview.FirstOrDefault()?.Id)
				{
					isChangedFilmOnReview = true;
				}

				bool isSetFilm = false;
				if (FilmOnReview.Count > 0)
				{
					isSetFilm = true;
				}

				bool isChangedRevText = false;
				if (_preChnagedOriginal.ReviewText != ReviewText)
				{
					isChangedRevText = true;
				}


				return  isSetFilm && (isChangedTitle || isChangedRate ||
						isChangedReviewStatus || isChangedFilmOnReview || isChangedRevText);
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
