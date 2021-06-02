using EntitiesLibrary;
using MoiveHubSystem.Commands;
using MoiveHubSystem.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MoiveHubSystem.ViewModels
{
	class EditActorViewModel: INotifyPropertyChanged
	{
		private CountryRepository _countryRepository = new();
		private CityRepository _cityRepository = new();
		private ActorRepository _actorRepository = new();

		private Actor _preChnagedOriginal;

		public List<Country> Countries { get; set; }
		public List<City> Cities { get; set; }


		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		} // name of actor / 50 symbs

		private string _patronimic;
		public string Patronimic
		{
			get
			{
				return _patronimic;
			}
			set
			{
				_patronimic = value;
				OnPropertyChanged(nameof(Patronimic));
			}
		}// middle name / 50 symbs

		private string _surname;
		public string Surname
		{
			get
			{
				return _surname;
			}
			set
			{
				_surname = value;
				OnPropertyChanged(nameof(Surname));
			}
		}// last name / 50 symbs

		private string _bio;
		public string Bio
		{
			get
			{
				return _bio;
			}
			set
			{
				_bio = value;
				OnPropertyChanged(nameof(Bio));
			}
		}

		private City _city;
		public City City
		{
			get
			{
				return _city;
			}
			set
			{
				_city = value;
				OnPropertyChanged(nameof(City));
			}
		}


		private Country _selectedCountry;
		public Country SelectedCountry
		{
			get
			{
				return _selectedCountry;
			}
			set
			{
				_selectedCountry = value;
				OnPropertyChanged(nameof(SelectedCountry));
			}
		}

		private City _selectedCity;
		public City SelectedCity
		{
			get
			{
				return _selectedCity;
			}
			set
			{
				_selectedCity = value;
				OnPropertyChanged(nameof(SelectedCity));
			}
		}

		Actor _updatedActor;

		// Commands
		public ICommand AcceptEditActor
		{
			get => new RelayCommand(obj =>
			{
				try
				{
					int newCityId = SelectedCity?.Id ?? 0;
					_updatedActor = new Actor()
					{
						Id = _preChnagedOriginal.Id,
						Name = this.Name,
						Patronimic = this.Patronimic,
						Surname = this.Surname,
						Bio = this.Bio,
						CityId = newCityId != 0 ? newCityId : _preChnagedOriginal.CityId,
						PhotoId = _preChnagedOriginal.PhotoId
					};
					_actorRepository.Update(_updatedActor);
					MessageBox.Show("Updated Successfully", "Info",
						MessageBoxButton.OK, MessageBoxImage.Information);
				}
				catch (Exception err)
				{
					MessageBox.Show($"{err.Message}", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}

				(obj as EditActorWindow).Close();

			}, obj => {
				bool isChangedName = false;
				if (_preChnagedOriginal.Name != Name)
				{
					isChangedName = true;
				}

				bool isChangedPatronimic = false;
				if (_preChnagedOriginal.Patronimic != Patronimic)
				{
					isChangedPatronimic = true;
				}

				bool isChangedSurname = false;
				if (_preChnagedOriginal.Surname != Surname)
				{
					isChangedSurname = true;
				}

				bool isChangedBio = false;
				if (_preChnagedOriginal.Bio != Bio)
				{
					isChangedBio = true;
				}

				bool isChangedCity = false;
				if (SelectedCity != null && (SelectedCity.Id != _preChnagedOriginal.City.Id))
				{
					isChangedCity = true;
				}

				return isChangedName || isChangedPatronimic ||
						isChangedSurname || isChangedBio || isChangedCity;
			});
		}

		//
		public EditActorViewModel(Actor actorToEdit)
		{
			_preChnagedOriginal = actorToEdit;

			Name = actorToEdit.Name;
			Patronimic = actorToEdit.Patronimic;
			Surname = actorToEdit.Surname;
			Bio = actorToEdit.Bio;
			City = actorToEdit.City;

			Countries = _countryRepository.GetAll().ToList();
			Cities = _cityRepository.GetAll().ToList();
		}
		
		//
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
