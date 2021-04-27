using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories
{
	public class FilmRepository : IDataRepository<FilmRepository>
	{
		public void Delete(FilmRepository entityToDelete)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<FilmRepository> GetAll()
		{
			return DapperORM.QueryManager.ExecSelect<FilmRepository>("GetAllFilms");
		}

		public void Insert(FilmRepository entityToInsert)
		{
			throw new NotImplementedException();
		}

		public void Update(FilmRepository entityToUpdate)
		{
			throw new NotImplementedException();
		}
	}
}
