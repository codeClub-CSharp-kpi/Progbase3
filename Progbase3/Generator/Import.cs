using Generator.models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using Generator.Repostitories.interfaces;
using Generator.Repostitories.implementations;

namespace Generator
{
	public static class Import
	{
		private static XmlSerializer _XMLserializer;
		private static IFilmRepository _filmsRepo;
		// window with imported enitites...: add 'report'list where saved only imported films

		static Import()
		{
			_XMLserializer = new XmlSerializer(typeof(IEnumerable<Film>));
			_filmsRepo = new FilmRepository();
		}

		public static void ImportExportedFilms(string sourceFilmsXMLFile)
		{
			var filmsFromXML = GetFilmsFromFile(sourceFilmsXMLFile);

			foreach (var item in filmsFromXML)
			{
				try
				{
					_filmsRepo.GetById(item.Id);
				}
				catch (System.Exception)
				{
					_filmsRepo.Insert(item);
					// can be added a dialog window with imported enitites
				}
			}
		}

		private static IEnumerable<Film> GetFilmsFromFile(string sourceFilmsXMLFile)
		{
			IEnumerable<Film> _filmsFromFile;
			using (FileStream fs = new FileStream(sourceFilmsXMLFile, FileMode.Open, FileAccess.Read))
			{
				_filmsFromFile = (IEnumerable<Film>)_XMLserializer.Deserialize(fs);
			}
			return _filmsFromFile;
		}

		
	}
}
