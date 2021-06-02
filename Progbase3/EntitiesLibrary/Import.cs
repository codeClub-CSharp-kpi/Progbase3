using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace EntitiesLibrary
{
	public static class Import
	{
		private static XmlSerializer _XMLserializer;
		private static IFilmRepository _filmsRepo;

		static Import()
		{
			_XMLserializer = new XmlSerializer(typeof(List<Film>));
			_filmsRepo = new FilmRepository();
		}

		public static List<Film> ImportExportedFilms(string sourceFilmsXMLFile)
		{
			List<Film> newImportedFilms = new();

			var filmsFromXML = GetFilmsFromFile(sourceFilmsXMLFile);
			var allFilms = _filmsRepo.GetAll();

			foreach (var item in filmsFromXML)
			{
				if (allFilms.Where(obj => obj.Id == item.Id).FirstOrDefault() == null)
				{
					newImportedFilms.Add(item);
					_filmsRepo.Insert(item);
				}
			}

			return newImportedFilms;
		}

		private static List<Film> GetFilmsFromFile(string sourceFilmsXMLFile)
		{
			List<Film> _filmsFromFile;
			using (FileStream fs = new FileStream(sourceFilmsXMLFile, FileMode.Open, FileAccess.Read))
			{
				_filmsFromFile = (List<Film>)_XMLserializer.Deserialize(fs);
			}
			return _filmsFromFile;
		}


	}
}
