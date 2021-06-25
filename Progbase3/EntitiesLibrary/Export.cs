using NetManagers;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;

namespace EntitiesLibrary
{
	public static class Export
	{
		private static List<Film> _filmsForXMLRecord;
		private static XmlSerializer _XMLserializer;

		static Export()
		{
			_XMLserializer = new XmlSerializer(typeof(List<Film>));
		}
		public static void ExportFilmsByActor(Actor concreteActor, string directoryToPlaceExport)
		{
			_filmsForXMLRecord =  (TcpQueryManager.ExecQuery("GetFilmsByActor", concreteActor.Id) as IEnumerable<Film>).ToList();

			string fileName = $"{concreteActor.Name}{concreteActor.Patronimic}{concreteActor.Surname}";
			string secureFileName = new(fileName.Select(ch => Path.GetInvalidFileNameChars().Contains(ch) ? '_' : ch).ToArray());

			string newXmlFilePath = directoryToPlaceExport + $@"\{secureFileName}_Filmography.xml";

			// creating new .xml file, filled with data(i.e films)
			using (FileStream fs = new FileStream(newXmlFilePath, FileMode.Create, FileAccess.Write))
			{
				_XMLserializer.Serialize(fs, _filmsForXMLRecord);
			}

			// creating achieve in entered directory

			string exportPath = $@"{directoryToPlaceExport}\export";
			int uniqeCounter = 0;
			do
			{
				if (!File.Exists($"{exportPath}.zip"))
				{
					exportPath = $"{exportPath}.zip";
					break;
				}
				else
				{
					exportPath = $"{exportPath}{++uniqeCounter}";
				}

			} while (true);

			using (var archieve = ZipFile.Open(exportPath, ZipArchiveMode.Create))
			{
				archieve.CreateEntryFromFile(newXmlFilePath, Path.GetFileName(newXmlFilePath));
			}

			File.Delete(newXmlFilePath);
		}
	}
}
