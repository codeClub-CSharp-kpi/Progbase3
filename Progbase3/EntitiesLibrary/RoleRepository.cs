using DataManagersLibrary;
using System;
using System.Collections.Generic;

namespace EntitiesLibrary
{
	public class RoleRepository : IRoleRepository
	{
		public void Delete(int idEntityToDelete)
		{
			throw new NotImplementedException();
		}

		public void Insert(Role entityToInsert)
		{
			throw new NotImplementedException();
		}

		public void Update(Role entityToUpdate)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Role> GetAll()
		{
			return QueryManager.ExecSelect<Role>("GetAllRoles");
		}

		public Role GetById(int id)
		{
			throw new NotImplementedException();
		}
	}
}
