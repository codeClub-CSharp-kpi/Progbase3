using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EntitiesLibrary
{
	public static class Import
	{
		private static XmlSerializer _XMLserializer;
		
		private static IFilmRepository _filmsRepo;
		private static ICountryRepository _countryRepo;
		private static ICityRepository _cityRepo;

		private const int AvaliableToImportCountriesAtOnce = 5;
		private const int AvaliableToImportCitiesAtOnce = AvaliableToImportCountriesAtOnce;

		static Import()
		{
			_XMLserializer = new XmlSerializer(typeof(List<Film>));
			_filmsRepo = new FilmRepository();
			_countryRepo = new CountryRepository();
			_cityRepo = new CityRepository();
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

		public static Dictionary<Country, List<City>> ImportCountries(string sourceCountriesXMLFile)
		{
			string content = null;
			using (FileStream fs = new(sourceCountriesXMLFile, FileMode.Open))
			{
				using (StreamReader sr = new(fs))
				{
					content = sr.ReadToEnd();
				}
			}

			if (string.IsNullOrWhiteSpace(content))
			{
				throw new Exception("There is nothing to unpack");
			}

			XDocument doc = XDocument.Parse(content);
			XElement root = doc.Root;

			var countriesFromXml = root.Elements().ToList();
			var firstCountriesFromXml = countriesFromXml.Take(countriesFromXml.Count > AvaliableToImportCountriesAtOnce ? AvaliableToImportCountriesAtOnce: countriesFromXml.Count);

			Dictionary<Country, List<City>> CountryCityDeserialized = new Dictionary<Country, List<City>>();

			foreach (var item in firstCountriesFromXml)
			{
				Country newC = new Country()
				{
					Name = item.Element("Name").Value
				};

				List<City> citiesOfCountry = new();

				var citiesOfCountryFromXML = item.Element("Cities").Elements("City").ToList();

				
				var firstCitiesFromXml = citiesOfCountryFromXML.Take(citiesOfCountryFromXML.Count > AvaliableToImportCitiesAtOnce ? AvaliableToImportCitiesAtOnce : citiesOfCountryFromXML.Count);

				foreach (var c in firstCitiesFromXml)
				{
					citiesOfCountry.Add(new City()
					{
						Name = c.Element("Name").Value,
					});
				}
				CountryCityDeserialized.Add(newC, citiesOfCountry);
			}

			// validating them
			IEnumerable<Country> allCountriesFromDB = null;
			foreach (var xmlCountry in CountryCityDeserialized)
			{
				allCountriesFromDB = _countryRepo.GetAll();

				var countryFromXMLInDB = allCountriesFromDB.FirstOrDefault(obj => xmlCountry.Key.Name == obj.Name);

				if (countryFromXMLInDB == null) // there is no such country in database. By other words the country is not added to db yet
				{
					_countryRepo.Insert(new Country()
					{
						Name = xmlCountry.Key.Name
					});

					// finding just inserted city and receiving its id
					int countryIdOfCurrCity = _countryRepo.GetAll().FirstOrDefault(obj => obj.Name == xmlCountry.Key.Name).Id;

					foreach (var city in xmlCountry.Value)
					{
						city.CountryId = countryIdOfCurrCity;
						_cityRepo.Insert(city);
					}
				}
				else
				{
					foreach (var xmlCity in xmlCountry.Value)
					{
						City cityInDB = countryFromXMLInDB.Cities.FirstOrDefault(obj=> obj.Name == xmlCity.Name);
						
						if (cityInDB == null)
						{
							_cityRepo.Insert(new City()
							{
								Name = xmlCity.Name,
								CountryId = countryFromXMLInDB.Id,
							});
						}
					}
				}

			}


			return CountryCityDeserialized;
		}
	}
}
