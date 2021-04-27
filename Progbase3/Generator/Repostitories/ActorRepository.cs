using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories
{
	public class ActorRepository : IDataRepository<models.Actor>
	{
		public void Delete(models.Actor entityToDelete)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<models.Actor> GetAll()
		{
			return DapperORM.QueryManager.ExecSelect<models.Actor>("GetAllActors");
		}

		public void Insert(models.Actor entityToInsert)
		{
			throw new NotImplementedException();
		}

		public void Update(models.Actor entityToUpdate)
		{
			throw new NotImplementedException();
		}
	}
}
