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
	class ActorsViewModel: INotifyPropertyChanged
	{
		const int AmountOfInPageElements = 5;
		
		public ObservableCollection<Actor> Actors { get; set; }

		public ActorsViewModel()
		{
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
				int total = (TcpQueryManager.ExecQuery("GetAllActors") as IEnumerable<Actor>).Count() / AmountOfInPageElements;
				if ((TcpQueryManager.ExecQuery("GetAllActors") as IEnumerable<Actor>).Count() % AmountOfInPageElements != 0)
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
					foreach (var item in (TcpQueryManager.ExecQuery("GetAllFilmsActors") as IEnumerable<FilmActor>).Where(obj => obj.ActorId == SelectedActor.Id))
					{
						TcpQueryManager.ExecQuery("DelFilmActor", item.Id);
					}
					TcpQueryManager.ExecQuery("DelActor", SelectedActor.Id);

					if (_selectedActor.PhotoId != (int)StandartPhoto_Ids.Default)
					{
						TcpQueryManager.ExecQuery("DeletePhoto", _selectedActor.PhotoId);
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
				foreach (var item in (TcpQueryManager.ExecQuery("GetAllActors") as IEnumerable<Actor>).Where(obj => obj.ToString() == SearchField))
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
			return (TcpQueryManager.ExecQuery("GetActorsPage", AmountOfInPageElements, AmountOfInPageElements * (pageNumber - 1)) as IEnumerable<Actor>);
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
