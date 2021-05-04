using Generator.Repostitories.implementations;
using System.Collections.Generic;

namespace Generator.models
{
	public class Actor: IEntity
	{
		public int Id { get; set; } // primary key
		public string Name { get; set; } // name of actor / 50 symbs
		public string Patronimic { get; set; } // middle name / 50 symbs
		public string Surname { get; set; } // last name / 50 symbs
		public string Bio { get; set; } // actor biography / 250 symbs
		
		public int PhotoId { get;set; } // apearence
		public Photo Photo
		{
			get
			{
				return new PhotoRepository().GetById(PhotoId);
			}
		}

		public int CityId { get; set; } // where was born
		public City City
		{
			get
			{
				return new CityRepository().GetById(CityId);
			}
		}

		//nav-ref that creates many2many relation
		public IEnumerable<Film> Films
		{
			get
			{
				return new FilmActorRepository().GetFilmsByActor(Id);
			}
		}// featured films
	}
	
}
