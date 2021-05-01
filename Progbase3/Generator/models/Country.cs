using System.Collections.Generic;

namespace Generator.models
{
	public class Country: IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } // 50 symbs

		public IEnumerable<City> Cities { get; set; }
	}
	
}
