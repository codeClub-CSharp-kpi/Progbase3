using Generator.models;
using Generator.Repostitories.interfaces;
using System.Collections.Generic;

namespace Generator.Repostitories.implementations
{
	class FilmActorRepository : IFilmActorRepository
	{
		public IEnumerable<Actor> GetActorsByFilm(int filmId)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@filmId", filmId);
			return DapperORM.QueryManager.ExecSelect<Actor>("GetActorsByFilm", parameters);
		}

		public IEnumerable<Film> GetFilmsByActor(int actId)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@actId", actId);
			return DapperORM.QueryManager.ExecSelect<Film>("GetFilmsByActor", parameters);
		}
	}
}
