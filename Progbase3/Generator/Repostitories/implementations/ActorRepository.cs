using Generator.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories.implementations
{
	public class ActorRepository: interfaces.IActorRepository
	{
		public void Delete(models.Actor entityToDelete)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<models.Actor> GetAll()
		{
			return DapperORM.QueryManager.ExecSelect<models.Actor>("GetAllActors");
		}

		public Actor GetById(long id)
		{
			throw new NotImplementedException();
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
