using Generator.models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Generator
{
	public static class Import
	{
		private static XmlSerializer _XMLserializer;

		static Import()
		{
			_XMLserializer = new XmlSerializer(typeof(IEnumerable<Film>));
		}

		public static IEnumerable<Film> ImportExportedFilms(string sourceFilmsXMLFile, string destinationDirectory)
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
