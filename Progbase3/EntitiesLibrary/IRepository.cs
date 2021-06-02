using System.Collections.Generic;

namespace EntitiesLibrary
{
	public interface IRepository<T>
		where T : IEntity
	{
		void Delete(int idEntityToDelete);
		IEnumerable<T> GetAll();
		T GetById(int id);
		void Insert(T entityToInsert);
		void Update(T entityToUpdate);
	}

}
