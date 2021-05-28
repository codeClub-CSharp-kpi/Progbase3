using Generator.models;
using Generator.Repostitories.implementations;
using Generator.Repostitories.interfaces;
using MoiveHubSystem.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MoiveHubSystem.ViewModels
{
	class ActorsViewModel: INotifyPropertyChanged
	{
		const int AmountOfInPageElements = 5;
		
		private ActorRepository _actorRepo;
		private CityRepository _cityRepo;

		public ObservableCollection<Actor> Actors { get; set; }
		public IEnumerable<City> Cities { get; set; }

		public ActorsViewModel()
		{
			_actorRepo = new ActorRepository();
			_cityRepo = new CityRepository();
			Actors = new ObservableCollection<Actor>();
			Cities = new ObservableCollection<City>();
			

			foreach (var item in GetPageForList(_currentPageCounter))
			{
				Actors.Add(item);
			}

			Cities = _cityRepo.GetAll();
			//var coll = _cityRepo.GetAll();
			//foreach (var item in coll)
			//{
			//	Cities.Add(item);
			//}

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
				OnPropertyChanged(nameof(SelectedActor));
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

				//Adding default object
				Actor defaultActor = new Actor()
				{
					Name = "New actor",
					Bio = "Actors biography",
					CityId = 0,
					PhotoId = 1
				};
				
				Actors.Add(defaultActor);
				SelectedActor = defaultActor;

			}, obj => (_currentPageCounter == TotalPages));
		}

		public ICommand DelActor
		{
			get => new RelayCommand(obj =>
			{
				
			}, obj => SelectedActor != null);
		}

		public ICommand SaveActor
		{
			get => new RelayCommand(obj =>
			{
				foreach (var item in Actors)
				{
					if (item.Id == 0)
					{
						_actorRepo.Insert(item);
					}
					else
					{
						var actorWithSameId = _actorRepo.GetById(SelectedActor.Id);
						_actorRepo.Update(new Actor()
						{
							Name = actorWithSameId.Name,
							Patronimic = actorWithSameId.Patronimic,
							Surname = actorWithSameId.Surname,
							Bio = actorWithSameId.Bio,
							CityId = actorWithSameId.CityId
						});
					}
				}
			}, obj => (SelectedActor != null) &&
			 !String.IsNullOrWhiteSpace(SelectedActor.Name) &&
			 !String.IsNullOrWhiteSpace(SelectedActor.Patronimic) &&
			 !String.IsNullOrWhiteSpace(SelectedActor.Surname) &&
			 !String.IsNullOrWhiteSpace(SelectedActor.Bio)
			);
		}

		public ICommand LoadNextPage
		{
			get => new RelayCommand(obj =>
			{
				++_currentPageCounter;
				Actors.Clear();
				foreach (var item in GetPageForList(_currentPageCounter))
				{
					Actors.Add(item);
				}

			}, obj => (_currentPageCounter < TotalPages));
		}

		public ICommand LoadPrevPage
		{
			get => new RelayCommand(obj =>
			{
				--_currentPageCounter;
				Actors.Clear();
				foreach (var item in GetPageForList(_currentPageCounter))
				{
					Actors.Add(item);
				}

			}, obj => (1 < _currentPageCounter));
		}
		
		// current page counter
		private int _currentPageCounter = 1;

		// supporting private methods
		private IEnumerable<Actor> GetPageForList(int pageNumber)
		{
			return _actorRepo.GetPage(AmountOfInPageElements, AmountOfInPageElements * (pageNumber - 1));
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
