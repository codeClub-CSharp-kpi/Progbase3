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

	public class ActorProducer: RandomProducer
	{
		private string _nameSource;
		private string _surnameSource;
		private string _sentenceSource;

		private IList<string> _names;
		private IList<string> _surnames;
		private IList<string> _sentences;

		public ActorProducer()
		{
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
		
		private int GneratePhotoId()
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

	}
}
