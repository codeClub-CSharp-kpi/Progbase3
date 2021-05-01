using Generator.models;
using Generator.Repostitories.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.EnityRandomProducers
{
	class ReviewProducer : RandomProducer
	{
		private string _titleSource;
		private string _sentenceSource;

		private IList<string> _words; //for title
		private IList<string> _sentences;

		public ReviewProducer()
		{
			_titleSource = _fakeDataSource + "titles";
			_sentenceSource = _fakeDataSource + "loremIpsum";
		}

		public override IEntity Create() 
		{
			return new Review()
			{
				Title = GenerateTitle(),
				isPositive = GenerateIsPositive(),
				FilmId = GenearateFilmId(),
				ReviewText = GenerateReviewText()
			};
		}

		private string GenerateTitle()
		{
			_words = ReadByLines(_titleSource);
			return _words[_randProvider.Next(_words.Count)];
		}

		private bool GenerateIsPositive()
		{
			return Convert.ToBoolean(_randProvider.Next(2));
		}

		private int GenearateFilmId()
		{
			var fr = new FilmRepository();
			List<Film> films = fr.GetAll().ToList(); // defining, what IDs exist

			return films[_randProvider.Next(films.Count)].Id;
		}

		private string GenerateReviewText()
		{
			const int StorylineLimit = 250;
			StringBuilder sb = new StringBuilder(StorylineLimit);

			_sentences = ReadBySentences(_sentenceSource);

			while (sb.Length < StorylineLimit)
			{
				sb.AppendFormat("{0}. ", _sentences[_randProvider.Next(_sentences.Count)]);
			}

			return sb.ToString();
		}
	}
}
