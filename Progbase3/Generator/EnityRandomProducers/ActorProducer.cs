using Generator.models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generator.Repostitories.implementations;

namespace Generator.EnityRandomProducers
{

	public class ActorProducer: Producer
	{
		private Random _randProvider;

		private string _nameSource;
		private string _surnameSource;
		private string _sentenceSource;

		private IList<string> _names;
		private IList<string> _surnames;
		private IList<string> _sentences;

		public ActorProducer()
		{
			_randProvider = new Random();

			_nameSource = _fakeDataSource + "names";
			_surnameSource = _fakeDataSource + "surnames";
			_sentenceSource = _fakeDataSource + "loremIpsum";
		}

		public override IEntity Create()
		{
			return new Actor()
			{
				Name = GenerateName(),
				Patronimic = GeneratePatronimic(),
				Surname = GenerateSurname(),
				CityId = GenerateCityId(),
				PhotoId = GeneratePhotoId(),
				Bio = GenerateBio()
			};
		}

		private string GenerateName()
		{
			_names = ReadByLines(_nameSource);
			return _names[_randProvider.Next(_names.Count)];
		}

		private string GeneratePatronimic()
		{
			return GenerateName();
		}
		
		private string GenerateSurname()
		{
			_surnames = ReadByLines(_surnameSource);
			return _surnames[_randProvider.Next(_surnames.Count)];
		}
		
		private int GenerateCityId()
		{
			var cr = new CountryRepository();
			List<Country> countries = cr.GetAll().ToList(); // defining, what IDs exist

			return countries[_randProvider.Next(countries.Count)].Id;
		}
		
		private int GeneratePhotoId()
		{
			var pr = new PhotoRepository();
			List<Photo> photos = pr.GetAll().ToList(); // defining, what IDs exist

			return photos[_randProvider.Next(photos.Count)].Id;
		}

		private string GenerateBio()
		{
			const int bioLimit = 250;
			StringBuilder sb = new StringBuilder(bioLimit);

			_sentences = ReadBySentences(_sentenceSource);
			while (sb.Length < bioLimit)
			{
				sb.AppendFormat("{0}. ", _sentences[_randProvider.Next(_sentences.Count)]);
			}

			return sb.ToString();
		}

		private void ReadLinesToCollection(string fullPath, IList<string> lineStorage)
		{
			using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
			{
				using (StreamReader sr = new StreamReader(fs))
				{
					string buff = String.Empty;
					do
					{
						buff = sr.ReadLine();
						if (buff == null)
							break;

						lineStorage.Add(buff);

					} while (true);

				}
			}
		}
		
		private void ReadSentencesToCollection(string fullPath, IList<string> sentenceStorage)
		{
			using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
			{
				using (StreamReader sr = new StreamReader(fs))
				{
					StringBuilder sb = new StringBuilder();

					string buff = String.Empty;
					do
					{
						buff = sr.ReadLine();

						if (buff == null)
							break;

						sb.Append(buff);
					}
					while (true);

					foreach(var sen in sb.ToString().Split('.'))
					{
						_sentences.Add(sen);
					}
				}
			}
		}


	}
}
