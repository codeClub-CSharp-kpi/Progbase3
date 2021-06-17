using DataManagersLibrary;
using System.Collections.Generic;
using System.Linq;

namespace EntitiesLibrary
{
	public class FilmRepository : IFilmRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@filmId", idEntityToDelete);

			QueryManager.ExecDML("DelFilm", parameters);
		}

		public Film GetById(int id)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@fid", id);

			return QueryManager.ExecSelect<Film>("GetFilmById", parameters).FirstOrDefault();
		}

		public void Insert(Film entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@title", entityToInsert.Title);
			parameters.Add("@offRelease", entityToInsert.OfficialReleaseDate);
			parameters.Add("@slogan", entityToInsert.Slogan);
			parameters.Add("@storyline", entityToInsert.StoryLine);

			QueryManager.ExecDML("AddFilm", parameters);
		}

		public void Update(Film entityToUpdate)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@fid", entityToUpdate.Id);
			parameters.Add("@title", entityToUpdate.Title);
			parameters.Add("@offRelease", entityToUpdate.OfficialReleaseDate);
			parameters.Add("@slogan", entityToUpdate.Slogan);
			parameters.Add("@storyline", entityToUpdate.StoryLine);

			QueryManager.ExecDML("UpdateFilm", parameters);
		}

		public IEnumerable<Film> GetAll()
		{
			return QueryManager.ExecSelect<Film>("GetAllFilms");
		}

		public IEnumerable<Review> GetReviews(int filmId)
		{
			return new ReviewRepository().GetAll().Where(r => r.FilmId == filmId);
		}

		public IEnumerable<Actor> GetActors(int filmId)
		{
			return new FilmActorRepository().GetActorsByFilm(filmId);
		}

		public IEnumerable<Film> GetPage(int countOfElemsOnPage, int elemsToSkip)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@pageElems", countOfElemsOnPage);
			parameters.Add("@skippedElems", elemsToSkip);
			return QueryManager.ExecSelect<Film>("GetFilmsPage", parameters);
		}
	}
}
