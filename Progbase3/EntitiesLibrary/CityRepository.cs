using System.Collections.Generic;
using System.Linq;

namespace EntitiesLibrary
{
	public class CityRepository : ICityRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cid", idEntityToDelete);

			QueryManager.ExecDML("DeleteCity", parameters);
		}

		public IEnumerable<City> GetAll()
		{
			return QueryManager.ExecSelect<City>("GetAllCities");
		}

		public City GetById(int id)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cid", id);

			return QueryManager.ExecSelect<City>("GetCityById", parameters).FirstOrDefault();
		}

		public void Insert(City entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cityName", entityToInsert.Name);
			parameters.Add("@countryId", entityToInsert.CountryId);

			QueryManager.ExecDML("AddCity", parameters);
		}

		public void Update(City entityToUpdate)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@cid", entityToUpdate.Id);
			parameters.Add("@cityName", entityToUpdate.Name);
			parameters.Add("@countryId", entityToUpdate.CountryId);

			QueryManager.ExecDML("UpdateCity", parameters);
		}

		public Country GetCountry(int cityId)
		{
			int countryOfCityId = new CityRepository().GetById(cityId).CountryId;
			return new CountryRepository().GetById(countryOfCityId);
		}

		public IEnumerable<Actor> GetActors(int cityId)
		{
			return new ActorRepository().GetAll().Where(a => a.CityId == cityId);
		}
	}



}
