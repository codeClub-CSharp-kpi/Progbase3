using Generator.models;
using Generator.Repostitories.interfaces;
using System.Collections.Generic;

namespace Generator.Repostitories.implementations
{
	public class FilmActorRepository : IFilmActorRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@filmActId", idEntityToDelete);

			DapperORM.QueryManager.ExecDML("DelFilmActor", parameters);
		}

		public IEnumerable<Actor> GetActorsByFilm(int filmId)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@filmId", filmId);
			return DapperORM.QueryManager.ExecSelect<Actor>("GetActorsByFilm", parameters);
		}

		public IEnumerable<FilmActor> GetAll()
		{
			return DapperORM.QueryManager.ExecSelect<FilmActor>("GetAllFilmsActors");
		}

		public FilmActor GetById(int id)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<Film> GetFilmsByActor(int actId)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@actId", actId);
			return DapperORM.QueryManager.ExecSelect<Film>("GetFilmsByActor", parameters);
		}

		public void Insert(FilmActor entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@filmId", entityToInsert.FilmId);
			parameters.Add("@actorId", entityToInsert.ActorId);

			DapperORM.QueryManager.ExecDML("AddFilmActor", parameters);
		}

		public void Update(FilmActor entityToUpdate)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@afid", entityToUpdate.Id);
			parameters.Add("@fid", entityToUpdate.FilmId);
			parameters.Add("@aid", entityToUpdate.ActorId);

			DapperORM.QueryManager.ExecDML("UpdateFilm", parameters);
		}
	}
}
