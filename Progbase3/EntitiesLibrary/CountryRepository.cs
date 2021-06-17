using DataManagersLibrary;
using System.Collections.Generic;
using System.Linq;

namespace EntitiesLibrary
{
	public class CountryRepository : ICountryRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cid", idEntityToDelete);

			QueryManager.ExecDML("DeleteCountry", parameters);
		}

		public IEnumerable<Country> GetAll()
		{
			return QueryManager.ExecSelect<Country>("GetAllCountries");
		}

		public Country GetById(int id)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cid", id);

			return QueryManager.ExecSelect<Country>("GetCountryById", parameters).FirstOrDefault();
		}

		public void Insert(Country entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cName", entityToInsert.Name);

			QueryManager.ExecDML("AddCountry", parameters);
		}

		public void Update(Country entityToUpdate)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cid", entityToUpdate.Id);
			parameters.Add("@cName", entityToUpdate.Name);

			QueryManager.ExecDML("UpdateCountry", parameters);
		}

		public IEnumerable<City> GetCities(int countryId)
		{
			return new CityRepository().GetAll().Where(c => c.CountryId == countryId);
		}
	}





}
