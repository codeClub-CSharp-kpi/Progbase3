using Generator.models;
using Generator.Repostitories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories.implementations
{
	public class ReviewRepository : IReviewRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@revId", idEntityToDelete);

			DapperORM.QueryManager.ExecDML("DeleteReview", parameters);
		}

		public IEnumerable<Review> GetAll()
		{
			return DapperORM.QueryManager.ExecSelect<Review>("GetAllReviews");
		}

		public Review GetById(int id)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@reviewId", id);

			return DapperORM.QueryManager.ExecSelect<Review>("GetReview", parameters).FirstOrDefault();
		}

		public void Insert(Review entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@title", entityToInsert.Title);
			parameters.Add("@isPos", entityToInsert.isPositive);
			parameters.Add("@reviewText", entityToInsert.ReviewText);
			parameters.Add("@filmId", entityToInsert.FilmId);

			DapperORM.QueryManager.ExecDML("AddReview", parameters);
		}

		public void Update(Review entityToUpdate)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@rid", entityToUpdate.Id);
			parameters.Add("@title", entityToUpdate.Title);
			parameters.Add("@isPos", entityToUpdate.isPositive);
			parameters.Add("@reviewText", entityToUpdate.ReviewText);
			parameters.Add("@filmId", entityToUpdate.FilmId);

			DapperORM.QueryManager.ExecDML("UpdateReview", parameters);
		}
	}
}
