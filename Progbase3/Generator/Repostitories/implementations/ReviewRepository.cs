using Generator.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories.implementations
{
	public class ReviewRepository : interfaces.IReviewRepository
	{
		public void Delete(Review entityToDelete)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Review> GetAll()
		{
			return DapperORM.QueryManager.ExecSelect<Review>("GetAllReviews");
		}

		public Review GetById(long id)
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
