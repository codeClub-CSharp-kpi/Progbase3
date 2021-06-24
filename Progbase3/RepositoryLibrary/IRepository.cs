using EntitiesLibrary;
using System;
using System.Collections.Generic;

namespace RepositoryLibrary
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
