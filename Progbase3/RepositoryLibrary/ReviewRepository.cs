using DataManagersLibrary;
using EntitiesLibrary;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLibrary
{
	public class ReviewRepository : IReviewRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@revId", idEntityToDelete);

			QueryManager.ExecDML("DeleteReview", parameters);
		}

		public IEnumerable<Review> GetAll()
		{
			return QueryManager.ExecSelect<Review>("GetAllReviews");
		}

		public Review GetById(int id)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@reviewId", id);

			return QueryManager.ExecSelect<Review>("GetReview", parameters).FirstOrDefault();
		}

		public void Insert(Review entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@title", entityToInsert.Title);
			parameters.Add("@isPos", entityToInsert.isPositive);
			parameters.Add("@reviewText", entityToInsert.ReviewText);
			parameters.Add("@rate", entityToInsert.Rate);
			parameters.Add("@filmId", entityToInsert.FilmId);

			QueryManager.ExecDML("AddReview", parameters);
		}

		public void Update(Review entityToUpdate)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@rid", entityToUpdate.Id);
			parameters.Add("@title", entityToUpdate.Title);
			parameters.Add("@isPos", entityToUpdate.isPositive);
			parameters.Add("@reviewText", entityToUpdate.ReviewText);
			parameters.Add("@rate", entityToUpdate.Rate);
			parameters.Add("@filmId", entityToUpdate.FilmId);

			QueryManager.ExecDML("UpdateReview", parameters);
		}

		public Film GetFilm(int revId)
		{
			int filmOfReviewID = GetById(revId).FilmId;
			return new FilmRepository().GetById(filmOfReviewID);
		}

		public IEnumerable<Review> GetPage(int countOfElemsOnPage, int elemsToSkip)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@pageElems", countOfElemsOnPage);
			parameters.Add("@skippedElems", elemsToSkip);
			return QueryManager.ExecSelect<Review>("GetReviewsPage", parameters);
		}
	}
}
