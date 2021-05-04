using Generator.models;
using System.Collections.Generic;

namespace Generator.Repostitories.interfaces
{
	interface IFilmActorRepository
	{
		IEnumerable<Actor> GetActorsByFilm(int actId);
		IEnumerable<Film> GetFilmsByActor(int filmId);
	}
}
