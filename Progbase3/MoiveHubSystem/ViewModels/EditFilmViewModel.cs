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
	class EditFilmViewModel : INotifyPropertyChanged
	{
		private CountryRepository _countryRepository = new();
		private CityRepository _cityRepository = new();
		private ActorRepository _actorRepository = new();

		private Film _preChnagedOriginal;


		private ActorRepository _actorRepo = new();
		private FilmRepository _filmRepo = new();
		private FilmActorRepository _filmActRepo = new();

		public ObservableCollection<Actor> PickList { get; set; } = new();
		public ObservableCollection<Actor> Cast { get; set; } = new();

		public EditFilmViewModel(Film filmToEdit)
		{
			_preChnagedOriginal = filmToEdit;

			Title = filmToEdit.Title;
			OfficialReleaseDate = filmToEdit.OfficialReleaseDate;
			Slogan = filmToEdit.Slogan;
			StoryLine = filmToEdit.StoryLine;
			
			foreach (var item in filmToEdit.Actors)
			{
				Cast.Add(item);
			}

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

		private DateTime _officialReleaseDate = DateTime.Now;
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
	

		private Film _updatedFilm;

		// Commands
		public ICommand AcceptEditActor
		{
			get => new RelayCommand(obj =>
			{
				try
				{
					_updatedFilm = new Film()
					{
						Id = _preChnagedOriginal.Id,
						Title = this.Title,
						OfficialReleaseDate = this.OfficialReleaseDate,
						Slogan = this.Slogan,
						StoryLine = this.StoryLine
					};
					_filmRepo.Update(_updatedFilm);

					var q = Cast.Select(obj => obj.Id).Intersect(_preChnagedOriginal.Actors.Select(obj => obj.Id));

					//comparing q with pre pick collection
					//and deleting elems which are not in q
					foreach (int aid in _preChnagedOriginal.Actors.Select(obj => obj.Id))
					{
						if (!q.Contains(aid))
						{
							var FilmActor_ToDelId = _filmActRepo.GetAll().Where(obj => obj.FilmId == _updatedFilm.Id && obj.ActorId == aid).Select(obj=> obj.Id).FirstOrDefault();
							_filmActRepo.Delete(FilmActor_ToDelId);
						}
					}

					//comparing q with current cast collection
					//and add elems which are not in q
					foreach (int aid in Cast.Select(obj => obj.Id))
					{
						if (!q.Contains(aid))
						{
							_filmActRepo.Insert(new FilmActor()
							{
								ActorId = aid,
								FilmId = _updatedFilm.Id
							});
						}
					}

					MessageBox.Show("Updated Successfully", "Info",
						MessageBoxButton.OK, MessageBoxImage.Information);
				}
				catch (Exception err)
				{
					MessageBox.Show($"{err.Message}", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}

				(obj as EditFilmWindow).Close();

			}, obj => {
				bool isChangedTitle = false;
				if (_preChnagedOriginal.Title != Title)
				{
					isChangedTitle = true;
				}

				bool isChangedOfficialReleaseDate = false;
				if (_preChnagedOriginal.OfficialReleaseDate != OfficialReleaseDate)
				{
					isChangedOfficialReleaseDate = true;
				}

				bool isChangedSlogan = false;
				if (_preChnagedOriginal.Slogan != Slogan)
				{
					isChangedSlogan = true;
				}

				bool isChangedStoryLine = false;
				if (_preChnagedOriginal.StoryLine != StoryLine)
				{
					isChangedStoryLine = true;
				}

				// Implementing if changed cast equals to original cast
				bool isChangedCast = false;
				isChangedCast = _preChnagedOriginal.Actors.Count() > Cast.Count ?
					_preChnagedOriginal.Actors.Select(obj => obj.Id).Except(Cast.Select(obj => obj.Id)).Any():
					Cast.Select(obj => obj.Id).Except(_preChnagedOriginal.Actors.Select(obj => obj.Id)).Any();
				
				return isChangedTitle || isChangedOfficialReleaseDate ||
						isChangedSlogan || isChangedStoryLine || isChangedCast;
			});
		}
		public ICommand ShiftToCast
		{
			get => new RelayCommand(obj =>
			{
				var actorInPickList = PickList.Where(obj => obj.Id == SelectedActor.Id).FirstOrDefault();
				PickList.Remove(actorInPickList);
				Cast.Add(actorInPickList);
			}, obj =>
			{
				return Cast.Where(obj => obj.Id == SelectedActor.Id).FirstOrDefault() == null;
			});
		}
		public ICommand ShiftFromCast
		{
			get => new RelayCommand(obj =>
			{
				var actorInCast = Cast.Where(obj => obj.Id == SelectedActor.Id).FirstOrDefault();
				Cast.Remove(actorInCast);
			});
		}
		//


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
