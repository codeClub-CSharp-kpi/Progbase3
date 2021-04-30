﻿using Generator.models;
using Generator.Repostitories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories.implementations
{
	public class CityRepository : ICityRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cid", idEntityToDelete);

			DapperORM.QueryManager.ExecDML("DeleteCity", parameters);
		}

		public IEnumerable<City> GetAll()
		{
			return DapperORM.QueryManager.ExecSelect<City>("GetAllCities");
		}

		public City GetById(int id)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cid", id);

			return DapperORM.QueryManager.ExecSelect<City>("GetCityById", parameters).FirstOrDefault();
		}

		public void Insert(City entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cityName", entityToInsert.Name);
			parameters.Add("@countryId", entityToInsert.CounrtyId);

			DapperORM.QueryManager.ExecDML("AddCity", parameters);
		}

		public void Update(City entityToUpdate)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cid", entityToUpdate.Id);
			parameters.Add("@cityName", entityToUpdate.Name);
			parameters.Add("@countryId", entityToUpdate.CounrtyId);

			DapperORM.QueryManager.ExecDML("UpdateCity", parameters);
		}
	}
}