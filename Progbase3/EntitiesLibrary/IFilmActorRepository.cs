using System.Collections.Generic;

namespace EntitiesLibrary
{
	public interface IFilmActorRepository : IRepository<FilmActor>
	{
		IEnumerable<Actor> GetActorsByFilm(int actId);
		IEnumerable<Film> GetFilmsByActor(int filmId);
	}
}
