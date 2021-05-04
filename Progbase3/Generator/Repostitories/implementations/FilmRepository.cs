using Generator.models;
using Generator.Repostitories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories.implementations
{
	public class FilmRepository : IFilmRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@filmId", idEntityToDelete);

			DapperORM.QueryManager.ExecDML("DelFilm", parameters);
		}

		public Film GetById(int id)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@fid", id);

			return DapperORM.QueryManager.ExecSelect<Film>("GetFilmById", parameters).FirstOrDefault();
		}

		public void Insert(Film entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@title", entityToInsert.Title);
			parameters.Add("@offRelease", entityToInsert.OfficialReleaseDate);
			parameters.Add("@slogan", entityToInsert.Slogan);
			parameters.Add("@storyline", entityToInsert.StoryLine);

			DapperORM.QueryManager.ExecDML("AddFilm", parameters);
		}

		public void Update(Film entityToUpdate)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@fid", entityToUpdate.Id);
			parameters.Add("@title", entityToUpdate.Title);
			parameters.Add("@offRelease", entityToUpdate.OfficialReleaseDate);
			parameters.Add("@slogan", entityToUpdate.Slogan);
			parameters.Add("@storyline", entityToUpdate.StoryLine);

			DapperORM.QueryManager.ExecDML("UpdateFilm", parameters);
		}

		public IEnumerable<Film> GetAll()
		{
			return DapperORM.QueryManager.ExecSelect<Film>("GetAllFilms");
		}

		public IEnumerable<Review> GetReviews(int filmId)
		{
			return new ReviewRepository().GetAll().Where(r => r.FilmId == filmId);
		}
		
		public IEnumerable<Actor> GetActors(int filmId)
		{
			return new FilmActorRepository().GetActorsByFilm(filmId);
		}
	}
}
