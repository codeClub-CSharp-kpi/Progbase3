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
			throw new NotImplementedException();
		}

		public IEnumerable<Review> GetAll()
		{
			return DapperORM.QueryManager.ExecSelect<Review>("GetAllReviews");
		}

		public Review GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Insert(Review entityToInsert)
		{
			throw new NotImplementedException();
		}

		public void Update(Review entityToUpdate)
		{
			throw new NotImplementedException();
		}
	}
}
