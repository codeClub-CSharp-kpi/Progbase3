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
		void Delete(int idEntityToDelete);
		IEnumerable<T> GetAll();
		T GetById(int id);
		void Insert(T entityToInsert);
		void Update(T entityToUpdate);
	}
}
