using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories
{
	public interface IDataRepository
	{

	}
	public interface IDataRepository<T>: IDataRepository
		where T : class
	{
		void Insert(T entityToInsert);
		void Update(T entityToUpdate);
		IEnumerable<T> GetAll();
		void Delete(T entityToDelete);
	}
}
