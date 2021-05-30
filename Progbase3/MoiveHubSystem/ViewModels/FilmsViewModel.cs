using Generator.models;
using Generator.Repostitories.implementations;
using MoiveHubSystem.Commands;
using MoiveHubSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MoiveHubSystem.ViewModels
{
	class FilmsViewModel: INotifyPropertyChanged
	{
		const int AmountOfInPageElements = 5;

		private FilmRepository _filmRepository = new();
		private ActorRepository _actorRepository = new();

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
				int total = _filmRepository.GetAll().Count() / AmountOfInPageElements;
				if (_filmRepository.GetAll().Count() % AmountOfInPageElements != 0)
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
				MessageBoxResult userDecisionDelOrNotDel = MessageBox.Show("You're deleting the actor! Sure?", "Earasing actor", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
				if (userDecisionDelOrNotDel == MessageBoxResult.OK)
				{
					_filmRepository.Delete(SelectedFilm.Id);
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
			return _filmRepository.GetPage(AmountOfInPageElements, AmountOfInPageElements * (pageNumber - 1));
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
