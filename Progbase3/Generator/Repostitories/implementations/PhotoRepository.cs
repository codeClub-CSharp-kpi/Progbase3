using Generator.models;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Repostitories.implementations
{
	public class PhotoRepository : interfaces.IPhotoRepository
	{
		public void Delete(int idEntityToDelete)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@pid", idEntityToDelete);

			DapperORM.QueryManager.ExecDML("DeletePhoto", parameters);
		}

		public IEnumerable<Photo> GetAll()
		{
			return DapperORM.QueryManager.ExecSelect<Photo>("GetAllPhotos");
		}

		public Photo GetById(int id)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@pid", id);

			return DapperORM.QueryManager.ExecSelect<Photo>("GetPhotoById", parameters).FirstOrDefault();
		}

		public void Insert(Photo entityToInsert)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@pName", entityToInsert.Path);

			DapperORM.QueryManager.ExecDML("AddPhoto", parameters);
		}

		public void Update(Photo entityToUpdate)
		{
			var parameters = new Dapper.DynamicParameters();
			parameters.Add("@pid", entityToUpdate.Id);
			parameters.Add("@pName", entityToUpdate.Path);

			DapperORM.QueryManager.ExecDML("UpdatePhoto", parameters);
		}
	}
}
