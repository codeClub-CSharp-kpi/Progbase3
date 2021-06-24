using DataManagersLibrary;
using EntitiesLibrary;
using System.Collections.Generic;

namespace RepositoryLibrary
{
	public class FilmActorRepository : IFilmActorRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@filmActId", idEntityToDelete);

			QueryManager.ExecDML("DelFilmActor", parameters);
		}

		public IEnumerable<Actor> GetActorsByFilm(int filmId)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@filmId", filmId);
			return QueryManager.ExecSelect<Actor>("GetActorsByFilm", parameters);
		}

		public IEnumerable<FilmActor> GetAll()
		{
			return QueryManager.ExecSelect<FilmActor>("GetAllFilmsActors");
		}

		public FilmActor GetById(int id)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<Film> GetFilmsByActor(int actId)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@actId", actId);
			return QueryManager.ExecSelect<Film>("GetFilmsByActor", parameters);
		}

		public void Insert(FilmActor entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@filmId", entityToInsert.FilmId);
			parameters.Add("@actorId", entityToInsert.ActorId);

			QueryManager.ExecDML("AddFilmActor", parameters);
		}

		public void Update(FilmActor entityToUpdate)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@afid", entityToUpdate.Id);
			parameters.Add("@fid", entityToUpdate.FilmId);
			parameters.Add("@aid", entityToUpdate.ActorId);

			QueryManager.ExecDML("UpdateFilm", parameters);
		}
	}
}
