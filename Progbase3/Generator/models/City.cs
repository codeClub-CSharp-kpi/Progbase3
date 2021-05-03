using Generator.Repostitories.implementations;
using System.Collections.Generic;
using System.Linq;

namespace Generator.models
{
	public class City: IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } // 50 symbs
		public int CountryId { get; set; }
		public Country Country
		{
			get 
			{
				return new CountryRepository().GetById(CountryId);
			}
		}

		public IEnumerable<Actor> Actors
		{
			get
			{
				return new ActorRepository().GetAll().Where(a => a.CityId == Id);
			}
		}
	}
	
}
