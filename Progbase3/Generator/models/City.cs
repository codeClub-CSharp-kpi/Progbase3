using System.Collections.Generic;

namespace Generator.models
{
	public class City: IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } // 50 symbs
		public int CounrtyId { get; set; }
		public Country Country { get; set; }

		public IEnumerable<Actor> Actors { get; set; }
	}
	
}
