﻿using Generator.models;
using Generator.Repostitories.implementations;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
			var cr = new CityRepository();
			List<City> cities = cr.GetAll().ToList(); // defining, what IDs exist

			return cities[_randProvider.Next(cities.Count)].Id;
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

	}
}
