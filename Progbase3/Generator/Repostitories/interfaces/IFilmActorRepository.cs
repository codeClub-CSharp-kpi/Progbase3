using Generator.models;
using System.Collections.Generic;

namespace Generator.Repostitories.interfaces
{
	interface IFilmActorRepository: IRepository<FilmActor>
	{
		IEnumerable<Actor> GetActorsByFilm(int actId);
		IEnumerable<Film> GetFilmsByActor(int filmId);


	}
}
