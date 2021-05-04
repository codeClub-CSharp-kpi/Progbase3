using System.Collections.Generic;

namespace Generator.Repostitories.interfaces
{
	public interface IRepository<T>
		where T : models.IEntity
	{
		void Delete(int idEntityToDelete);
		IEnumerable<T> GetAll();
		T GetById(int id);
		void Insert(T entityToInsert);
		void Update(T entityToUpdate);
	}
}
