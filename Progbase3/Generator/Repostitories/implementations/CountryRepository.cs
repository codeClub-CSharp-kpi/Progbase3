using Generator.models;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Repostitories.implementations
{
	public class CountryRepository : interfaces.ICountryRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cid", idEntityToDelete);

			DapperORM.QueryManager.ExecDML("DeleteCountry", parameters);
		}

		public IEnumerable<Country> GetAll()
		{
			return DapperORM.QueryManager.ExecSelect<Country>("GetAllCountries");
		}

		public Country GetById(int id)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cid", id);

			return DapperORM.QueryManager.ExecSelect<Country>("GetCountryById", parameters).FirstOrDefault();
		}

		public void Insert(Country entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cName", entityToInsert.Name);

			DapperORM.QueryManager.ExecDML("AddCountry", parameters);
		}

		public void Update(Country entityToUpdate)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cid", entityToUpdate.Id);
			parameters.Add("@cName", entityToUpdate.Name);

			DapperORM.QueryManager.ExecDML("UpdateCountry", parameters);
		}

		public IEnumerable<City> GetCities(int countryId)
		{
			return new CityRepository().GetAll().Where(c => c.CountryId == countryId);
		}
	}
}
