using System;
using System.Collections.Generic;

namespace EntitiesLibrary
{

	public class AccountRepository : IAccountRepository
	{
		public void Delete(int idEntityToDelete)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Account> GetAll()
		{
			return QueryManager.ExecSelect<Account>("GetAllAccounts");
		}

		public Account GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Insert(Account entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@login", entityToInsert.Login);
			parameters.Add("@password", entityToInsert.Password);
			parameters.Add("@rid", entityToInsert.RoleId);

			QueryManager.ExecDML("AddAccount", parameters);
		}

		public void Update(Account entityToUpdate)
		{
			throw new NotImplementedException();
		}
	}
}
