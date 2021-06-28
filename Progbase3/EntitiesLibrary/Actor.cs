using NetManagers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EntitiesLibrary
{
	[Serializable]
	public class Actor : IEntity, INotifyPropertyChanged
	{
		public int _id;
		public int Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
				OnPropertyChanged(nameof(Id));
			}
		} // primary key

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
		}// actor biography / 250 symbs

		public int PhotoId { get; set; } // apearence
		public Photo Photo
		{
			get
			{
				return TcpQueryManager.ExecQuery("GetPhotoById", PhotoId) as Photo;
			}
		}

		public int CityId { get; set; } // where was born
		public City City
		{
			get
			{
				return TcpQueryManager.ExecQuery("GetCityById", CityId) as City;
			}
		}

		//nav-ref that creates many2many relation
		public IEnumerable<Film> Films
		{
			get
			{
				return TcpQueryManager.ExecQuery("GetFilmsByActor", Id) as IEnumerable<Film>;
			}
		}// featured films


		public override string ToString()
		{
			return $"{Name} {Patronimic} {Surname}";
		}

		//
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
