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
	class ActorsViewModel: INotifyPropertyChanged
	{
		const int AmountOfInPageElements = 5;
		
		private ActorRepository _actorRepo;
		private FilmActorRepository _filmActorRepository = new();
		private PhotoRepository _photoRepository = new();

		public ObservableCollection<Actor> Actors { get; set; }

		public ActorsViewModel()
		{
			_actorRepo = new ActorRepository();
			Actors = new ObservableCollection<Actor>();
			RefillObservedActors();
		}

		private string _searchField;
		public string SearchField
		{
			get
			{
				return _searchField;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					RefillObservedActors();
				}
				_searchField = value;
				OnPropertyChanged(nameof(SearchField));
			}
		}


		private Actor _selectedActor;
		public Actor SelectedActor
		{
			get
			{
				return _selectedActor;
			}
			set
			{
				_selectedActor = value;
				if (_selectedActor != null)
				{
					ImageOfActor = Convert.FromBase64String(_selectedActor.Photo.Path);
				}
				else
				{
					ImageOfActor = null;
				}
				OnPropertyChanged(nameof(SelectedActor));
			}
		}

		private byte[] _imageOfActor;
		public byte[] ImageOfActor
		{
			get
			{
				return _imageOfActor;
			}
			set
			{
				_imageOfActor = value;
				OnPropertyChanged(nameof(ImageOfActor));
			}
		}
		//
		private int TotalPages
		{
			get
			{
				int total = _actorRepo.GetAll().Count() / AmountOfInPageElements;
				if (_actorRepo.GetAll().Count() % AmountOfInPageElements != 0)
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
		public ICommand AddActor
		{
			get => new RelayCommand(obj =>
			{
				var addWnd = new AddActorWindow();
				addWnd.ShowDialog();
			});
		}

		public ICommand DelActor
		{
			get => new RelayCommand(obj =>
			{
				MessageBoxResult userDecisionDelOrNotDel = MessageBox.Show("You're deleting the actor! Sure?", "Earasing actor", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
				if (userDecisionDelOrNotDel == MessageBoxResult.OK)
				{
					foreach (var item in _filmActorRepository.GetAll().Where(obj => obj.ActorId == SelectedActor.Id))
					{
						_filmActorRepository.Delete(item.Id);
					}
					_actorRepo.Delete(SelectedActor.Id);

					if (_selectedActor.PhotoId != (int)StandartPhoto_Ids.Default)
					{
						_photoRepository.Delete(_selectedActor.PhotoId);
					}

					RefillObservedActors();
				}
			}, obj => SelectedActor != null);
		}

		public ICommand EditActor
		{
			get => new RelayCommand(obj =>
			{
				EditActorWindow editActorWindow = new(SelectedActor);
				editActorWindow.ShowDialog();
			}, obj => SelectedActor != null);
		}

		public ICommand FindActor
		{
			get => new RelayCommand(obj =>
			{
				Actors.Clear();
				foreach (var item in _actorRepo.GetAll().Where(obj => obj.ToString() == SearchField))
				{
					Actors.Add(item);
				}

			}, obj => !string.IsNullOrEmpty(SearchField));
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
		private IEnumerable<Actor> GetPageForList(int pageNumber)
		{
			return _actorRepo.GetPage(AmountOfInPageElements, AmountOfInPageElements * (pageNumber - 1));
		}
		private void RefillObservedActors()
		{
			Actors.Clear();
			foreach (var item in GetPageForList(_currentPageCounter))
			{
				Actors.Add(item);
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
