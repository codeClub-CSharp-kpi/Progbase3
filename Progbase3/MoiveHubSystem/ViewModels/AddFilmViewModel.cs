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
	
	class AddFilmViewModel: INotifyPropertyChanged
	{
		private ActorRepository _actorRepo = new();
		private FilmRepository _filmRepo = new();
		private FilmActorRepository _filmActRepo = new();

		public ObservableCollection<Actor> PickList { get; set; } = new();
		public ObservableCollection<Actor> AddedCast { get; set; } = new();


		public AddFilmViewModel()
		{
			RefillObservedActors();
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
		} // name of actor / 50 symbs

		private DateTime _officialReleaseDate;
		public DateTime OfficialReleaseDate
		{
			get
			{
				return _officialReleaseDate;
			}
			set
			{
				_officialReleaseDate = value;
				OnPropertyChanged(nameof(OfficialReleaseDate));
			}
		}// middle name / 50 symbs

		private string _slogan;
		public string Slogan
		{
			get
			{
				return _slogan;
			}
			set
			{
				_slogan = value;
				OnPropertyChanged(nameof(Slogan));
			}
		}// last name / 50 symbs

		private string _storyLine;
		public string StoryLine
		{
			get
			{
				return _storyLine;
			}
			set
			{
				_storyLine = value;
				OnPropertyChanged(nameof(StoryLine));
			}
		}



		// Commands
		public ICommand ShiftToAddedCast
		{
			get => new RelayCommand(obj =>
			{
				var actorInPickList = PickList.Where(obj => obj.Id == SelectedActor.Id).FirstOrDefault();
				PickList.Remove(actorInPickList);
				AddedCast.Add(actorInPickList);
			}, obj => 
				{
					return AddedCast.Where(obj => obj.Id == SelectedActor.Id).FirstOrDefault() == null;
				});
		}
		public ICommand ShiftFromAddedCast
		{
			get => new RelayCommand(obj =>
			{
				var actorInCast = AddedCast.Where(obj => obj.Id == SelectedActor.Id).FirstOrDefault();
				AddedCast.Remove(actorInCast);
			});
		}
		public ICommand AcceptNewFilm
		{
			get => new RelayCommand(obj =>
			{
				try
				{
					Film newFilm = new()
					{
						Title = this.Title,
						OfficialReleaseDate = this.OfficialReleaseDate,
						Slogan = this.Slogan,
						StoryLine = this.StoryLine,
					};
					_filmRepo.Insert(newFilm);

					var justInsertedFilm = _filmRepo.GetAll().Where(obj => obj.Title == newFilm.Title).FirstOrDefault();
					foreach (var item in AddedCast)
					{
						_filmActRepo.Insert(new FilmActor()
						{
							FilmId = justInsertedFilm.Id,
							ActorId = item.Id
						});
					}

					MessageBox.Show("New film has been added successfully!", "Info",
							MessageBoxButton.OK, MessageBoxImage.Information);
				}
				catch (Exception err)
				{
					MessageBox.Show($"{err.Message}", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
				(obj as AddFilmWindow).Close();
			}, obj => {
				
				bool isEmptyTitle = true;
				if (!string.IsNullOrWhiteSpace(Title))
				{
					isEmptyTitle = false;
				}
				
				bool isEmptyOfficialReleaseDate = true;
				if (OfficialReleaseDate != default)
				{
					isEmptyOfficialReleaseDate = false;
				}

				bool isEmptySlogan = true;
				if (!string.IsNullOrWhiteSpace(Slogan))
				{
					isEmptySlogan = false;
				}

				bool isEmptyStoryLine = true;
				if (!string.IsNullOrWhiteSpace(StoryLine))
				{
					isEmptyStoryLine = false;
				}

				return !isEmptyTitle && !isEmptyOfficialReleaseDate &&
						!isEmptySlogan && !isEmptyStoryLine;
			});
		}


		// pagination commands
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

		// pagination attributes
		private int _currentPageCounter = 1;
		const int AmountOfInPageElements = 5;
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


		// supporting private methods
		private IEnumerable<Actor> GetPageForList(int pageNumber)
		{
			return _actorRepo.GetPage(AmountOfInPageElements, AmountOfInPageElements * (pageNumber - 1));
		}
		private void RefillObservedActors()
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
