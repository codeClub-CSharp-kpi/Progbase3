using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories.interfaces
{
	public interface IRepository<T>
		where T : models.IEntity
	{
		void Delete(T entityToDelete);
		IEnumerable<T> GetAll();
		T GetById(long id);
		void Insert(T entityToInsert);
		void Update(T entityToUpdate);
	}
}
