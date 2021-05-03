using Generator.Repostitories.implementations;
using System.Collections.Generic;
using System.Linq;

namespace Generator.models
{
	public class Country: IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } // 50 symbs

		public IEnumerable<City> Cities
		{
			get
			{
				return new CityRepository().GetAll().Where(c => c.CountryId == Id);
			}
		}
	}
	
}
