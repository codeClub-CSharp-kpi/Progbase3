using EntitiesLibrary;
using MoiveHubSystem.Commands;
using MoiveHubSystem.Views;
using NetManagers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MoiveHubSystem.ViewModels
{
	class FilmsViewModel: INotifyPropertyChanged
	{
		const int AmountOfInPageElements = 5;

		public ObservableCollection<Film> Films { get; set; } = new ObservableCollection<Film>();

		public FilmsViewModel()
		{
			RefillObservedActors();
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

		//
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

		// Commands
		public ICommand AddFilm
		{
			get => new RelayCommand(obj =>
			{
				var addWnd = new AddFilmWindow();
				addWnd.ShowDialog();
			});
		}

		public ICommand DelFilm
		{
			get => new RelayCommand(obj =>
			{
				MessageBoxResult userDecisionDelOrNotDel = MessageBox.Show("You're deleting the film! Sure?", "Earasing film", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
				if (userDecisionDelOrNotDel == MessageBoxResult.OK)
				{
					foreach (var item in (TcpQueryManager.ExecQuery("GetAllFilmsActors") as IEnumerable<FilmActor>).Where(obj=> obj.FilmId == SelectedFilm.Id))
					{
						TcpQueryManager.ExecQuery("DelFilmActor", item.Id);
					}

					foreach (var item in (TcpQueryManager.ExecQuery("GetAllReviews") as IEnumerable<Review>).Where(obj => obj.FilmId == SelectedFilm.Id))
					{
						TcpQueryManager.ExecQuery("DeleteReview", item.Id);
					}
					TcpQueryManager.ExecQuery("DelFilm", SelectedFilm.Id);

					RefillObservedActors();
				}
			}, obj => SelectedFilm != null);
		}

		public ICommand EditFilm
		{
			get => new RelayCommand(obj =>
			{
				EditFilmWindow editActorWindow = new(SelectedFilm);
				editActorWindow.ShowDialog();
			}, obj => SelectedFilm != null);
		}

		public ICommand LoadNextPage
		{
			get => new RelayCommand(obj =>
			{
				++_currentPageCounter;
				RefillObservedActors();

			}, obj => (_currentPageCounter < TotalPages));
		}

		public ICommand LoadPrevPage
		{
			get => new RelayCommand(obj =>
			{
				--_currentPageCounter;
				RefillObservedActors();

			}, obj => (1 < _currentPageCounter));
		}

		// current page counter
		private int _currentPageCounter = 1;

		// supporting private methods
		private IEnumerable<Film> GetPageForList(int pageNumber)
		{
			return (TcpQueryManager.ExecQuery("GetFilmsPage", AmountOfInPageElements, AmountOfInPageElements * (pageNumber - 1)) as IEnumerable<Film>);
		}
		private void RefillObservedActors()
		{
			Films.Clear();
			foreach (var item in GetPageForList(_currentPageCounter))
			{
				Films.Add(item);
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
