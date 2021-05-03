using Generator.Repostitories.implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.models
{
	public class Film: IEntity
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
				return new ReviewRepository().GetAll().Where(r => r.FilmId == Id);
			}
		}

		//nav-ref that creates many2many relation
		public IEnumerable<Actor> Actors
		{
			get 
			{
				return new FilmActorRepository().GetActorsByFilm(Id);
			}
		}// Cast(by other words)
	}
}
