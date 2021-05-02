using Generator.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories.interfaces
{
	interface IFilmActorRepository
	{
		IEnumerable<Actor> GetActorsByFilm(int actId);
		IEnumerable<Film> GetFilmsByActor(int filmId);
	}
}
