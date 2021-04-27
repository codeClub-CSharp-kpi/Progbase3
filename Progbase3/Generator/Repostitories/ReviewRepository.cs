using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories
{
	public class ReviewRepository : IDataRepository<ReviewRepository>
	{
		public void Delete(ReviewRepository entityToDelete)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ReviewRepository> GetAll()
		{
			return DapperORM.QueryManager.ExecSelect<ReviewRepository>("GetAllReviews");
		}

		public void Insert(ReviewRepository entityToInsert)
		{
			throw new NotImplementedException();
		}

		public void Update(ReviewRepository entityToUpdate)
		{
			throw new NotImplementedException();
		}
	}
}
