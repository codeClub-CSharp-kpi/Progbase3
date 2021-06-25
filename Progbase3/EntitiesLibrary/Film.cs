using NetManagers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntitiesLibrary
{
	[Serializable]
	public class Film : IEntity
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime OfficialReleaseDate { get; set; }
		public string Slogan { get; set; } // 50 symbols
		public string StoryLine { get; set; } // 250 symbols

		public IEnumerable<Review> Reviews
		{
			get
			{
				return (TcpQueryManager.ExecQuery("GetAllReviews") as IEnumerable<Review>).Where(r => r.FilmId == Id);
			}
		}

		//nav-ref that creates many2many relation
		public IEnumerable<Actor> Actors
		{
			get
			{
				return TcpQueryManager.ExecQuery("GetActorsByFilm", Id) as IEnumerable<Actor>;
			}
		}// Cast(by other words)

		public override string ToString()
		{
			return $"{Title}";
		}
	}
}
