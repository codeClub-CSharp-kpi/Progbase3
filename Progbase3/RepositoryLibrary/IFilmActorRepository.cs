using EntitiesLibrary;
using System.Collections.Generic;

namespace RepositoryLibrary
{
	public interface IFilmActorRepository : IRepository<FilmActor>
	{
		IEnumerable<Actor> GetActorsByFilm(int actId);
		IEnumerable<Film> GetFilmsByActor(int filmId);
	}
}
