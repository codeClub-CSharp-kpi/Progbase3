using NetManagers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntitiesLibrary
{
	[Serializable]
	public class Country : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } // 50 symbs

		public IEnumerable<City> Cities
		{
			get
			{
				return (TcpQueryManager.ExecQuery("GetAllCities") as IEnumerable<City>).Where(c => c.CountryId == Id);
			}
		}
	}
}
