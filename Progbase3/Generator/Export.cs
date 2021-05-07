using Generator.models;
using Generator.Repostitories.implementations;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.IO.Compression;

namespace Generator
{
	public static class Export
	{
		private static  IEnumerable<Film> _filmsForXMLRecord;
		private static XmlSerializer _XMLserializer;

		static Export()
		{
			_XMLserializer = new XmlSerializer(typeof(IEnumerable<Film>));
		}
		public static void ExportFilmsByActor(Actor concreteActor, string directoryToPlaceExport)
		{
			FilmActorRepository filmActorRepository = new();
			_filmsForXMLRecord = filmActorRepository.GetFilmsByActor(concreteActor.Id);

			string newXmlFilePath = directoryToPlaceExport + @"\films.xml";
			// creating new .xml file, filled with data(i.e films)
			using (FileStream fs = new FileStream(newXmlFilePath, FileMode.Create, FileAccess.Write))
			{
				_XMLserializer.Serialize(fs, _filmsForXMLRecord);
			}
			
			// creating achieve in entered directory
			ZipFile.CreateFromDirectory(directoryToPlaceExport, newXmlFilePath);
			
			// earasing used .xml
			File.Delete(newXmlFilePath);
		}
	}
}
