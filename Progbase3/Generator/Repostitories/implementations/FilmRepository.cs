using Generator.models;
using Generator.Repostitories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories.implementations
{
	public class FilmRepository : Repostitories.interfaces.IFilmRepository
	{
		public void Delete(Film entityToDelete)
		{
			throw new NotImplementedException();
		}

		public Film GetById(long id)
		{
			throw new NotImplementedException();
		}

		public void Insert(Film entityToInsert)
		{
			throw new NotImplementedException();
		}

		public void Update(Film entityToUpdate)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Film> GetAll()
		{
			throw new NotImplementedException();
		}
	}
}
