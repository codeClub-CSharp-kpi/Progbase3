using Generator.models;
using Generator.Repostitories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Repostitories.implementations
{
	public class FilmRepository : IFilmRepository
	{
		public void Delete(int idEntityToDelete)
		{
			throw new NotImplementedException();
		}

		public Film GetById(int id)
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
