﻿using DataManagersLibrary;
using System.Collections.Generic;

namespace EntitiesLibrary
{
	public class ReviewAccountRepository : IReviewAccountRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@revAccId", idEntityToDelete);

			QueryManager.ExecDML("DelReviewAccount", parameters);
		}

		public IEnumerable<ReviewAccount> GetAll()
		{
			return QueryManager.ExecSelect<ReviewAccount>("GetAllReviewsAccounts");
		}

		public ReviewAccount GetById(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Insert(ReviewAccount entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@revId ", entityToInsert.ReviewId);
			parameters.Add("@accId", entityToInsert.AccountId);

			QueryManager.ExecDML("AddReviewAccount", parameters);
		}

		public void Update(ReviewAccount entityToUpdate)
		{
			throw new System.NotImplementedException();
		}
	}
}
