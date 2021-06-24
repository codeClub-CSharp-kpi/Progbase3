using DataManagersLibrary;
using EntitiesLibrary;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLibrary
{
	public class PhotoRepository : IPhotoRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@pid", idEntityToDelete);

			QueryManager.ExecDML("DeletePhoto", parameters);
		}

		public IEnumerable<Photo> GetAll()
		{
			return QueryManager.ExecSelect<Photo>("GetAllPhotos");
		}

		public Photo GetById(int id)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@pid", id);

			return QueryManager.ExecSelect<Photo>("GetPhotoById", parameters).FirstOrDefault();
		}

		public void Insert(Photo entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@path", entityToInsert.Path);

			QueryManager.ExecDML("AddPhoto", parameters);
		}

		public void Update(Photo entityToUpdate)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@pid", entityToUpdate.Id);
			parameters.Add("@pName", entityToUpdate.Path);

			QueryManager.ExecDML("UpdatePhoto", parameters);
		}

		public Actor GetActor(int photoId)
		{
			return new ActorRepository().GetAll().Where(a => a.PhotoId == photoId).FirstOrDefault();
		}
	}
}
