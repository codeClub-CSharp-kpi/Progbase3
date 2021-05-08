using Generator.models;
using Generator.Repostitories.interfaces;
using System.Collections.Generic;
using System.Linq;

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
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@actName", entityToInsert.Name);
			parameters.Add("@actPatro", entityToInsert.Patronimic);
			parameters.Add("@actSur", entityToInsert.Surname);
			parameters.Add("@actBio", entityToInsert.Bio);
			parameters.Add("@photoId", entityToInsert.PhotoId);
			parameters.Add("@cityId", entityToInsert.CityId);

			DapperORM.QueryManager.ExecDML("AddActor", parameters);
		}

		public void Update(Actor entityToUpdate)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@actName", entityToUpdate.Name);
			parameters.Add("@actPatro", entityToUpdate.Patronimic);
			parameters.Add("@actSur", entityToUpdate.Surname);
			parameters.Add("@actBio", entityToUpdate.Bio);
			parameters.Add("@photoId", entityToUpdate.PhotoId);
			parameters.Add("@cityId", entityToUpdate.CityId);

			DapperORM.QueryManager.ExecDML("UpdateActor", parameters);
		}

		public City GetCity(int actId)
		{
			int actorCityId = new ActorRepository().GetById(actId).CityId;
			return new CityRepository().GetById(actorCityId);
		}

		public IEnumerable<Film> GetFilms(int actId)
		{
			return new FilmActorRepository().GetFilmsByActor(actId);
		}
		
		public Photo GetPhoto(int actId)
		{
			int actorPhotoId = new ActorRepository().GetById(actId).PhotoId;
			return new PhotoRepository().GetById(actorPhotoId);
		}

		public IEnumerable<Actor> GetPage(int countOfElemsOnPage, int elemsToSkip)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@pageElems", countOfElemsOnPage);
			parameters.Add("@skippedElems", elemsToSkip);
			return DapperORM.QueryManager.ExecSelect<Actor>("GetActorsPage", parameters);
		}
	}
}
