using Generator.models;
using Generator.Repostitories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
