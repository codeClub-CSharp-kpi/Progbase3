using Generator.models;
using Generator.Repostitories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories.implementations
{
	public class ActorRepository: IActorRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@actId", idEntityToDelete);

			DapperORM.QueryManager.ExecDML("DelActor", parameters);
		}

		public IEnumerable<Actor> GetAll()
		{
			return DapperORM.QueryManager.ExecSelect<Actor>("GetAllActors");
		}

		public Actor GetById(int id)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@actId", id);

			return DapperORM.QueryManager.ExecSelect<Actor>("GetActor", parameters).FirstOrDefault();
		}

		public void Insert(Actor entityToInsert)
		{
			throw new NotImplementedException();
		}

		public void Update(Actor entityToUpdate)
		{
			throw new NotImplementedException();
		}
	}
}
