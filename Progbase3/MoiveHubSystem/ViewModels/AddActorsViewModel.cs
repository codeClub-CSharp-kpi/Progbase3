using EntitiesLibrary;
using MoiveHubSystem.Commands;
using MoiveHubSystem.Views;
using NetManagers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MoiveHubSystem.ViewModels
{
	class AddActorsViewModel: INotifyPropertyChanged
	{
		public AddActorsViewModel()
		{
			Countries = (TcpQueryManager.ExecQuery("GetAllCountries") as IEnumerable<Country>).ToList();
		}

		public List<Country> Countries { get; set; }

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


		// Commands
		public ICommand AcceptNewActor
		{
			get => new RelayCommand(obj =>
			{
				try
				{
					TcpQueryManager.ExecQuery("AddActor", new Actor()
					{
						Name = this.Name,
						Patronimic = this.Patronimic,
						Surname = this.Surname,
						Bio = this.Bio,
						CityId = this.SelectedCity.Id,
						PhotoId = (int)StandartPhoto_Ids.Default
					});
					MessageBox.Show("New actor has been added successfully!", "Info",
							MessageBoxButton.OK, MessageBoxImage.Information);
				}
				catch (System.Exception err)
				{
					MessageBox.Show($"{err.Message}", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
				(obj as AddActorWindow).Close();
			}, obj => {
				bool isEmptyName = true;
				if (!string.IsNullOrWhiteSpace(Name))
				{
					isEmptyName = false;
				}

				bool isEmptyPatronimic = true;
				if (!string.IsNullOrWhiteSpace(Patronimic))
				{
					isEmptyPatronimic = false;
				}

				bool isEmptySurname = true;
				if (!string.IsNullOrWhiteSpace(Surname))
				{
					isEmptySurname = false;
				}

				bool isEmptyBio = true;
				if (!string.IsNullOrWhiteSpace(Bio))
				{
					isEmptyBio = false;
				}

				bool isUnSelectedCountry = true;
				if (SelectedCountry != null)
				{
					isUnSelectedCountry = false;
				}

				bool isUnSelectedCity = true;
				if (SelectedCity != null)
				{
					isUnSelectedCity = false;
				}

				return !isEmptyName && !isEmptyPatronimic &&
						!isEmptySurname && !isEmptyBio && !isUnSelectedCountry 
						&& !isUnSelectedCity;
			});
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
