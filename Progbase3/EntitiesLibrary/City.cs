using NetManagers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntitiesLibrary
{
	[Serializable]
	public class City : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } // 50 symbs
		public int CountryId { get; set; }
		public Country Country
		{
			get
			{
				return TcpQueryManager.ExecQuery("GetCountryById", CountryId) as Country;
			}
		}

		public IEnumerable<Actor> Actors
		{
			get
			{
				return (TcpQueryManager.ExecQuery("GetAllActors") as IEnumerable<Actor>).Where(a => a.CityId == Id);
			}
		}

		public override string ToString()
		{
			return $"{Name}";
		}
	}
}
