using Generator.models;
using Generator.Repostitories.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator.EnityRandomProducers
{
	class ReviewProducer : RandomProducer
	{
		private string _titleSource;
		private string _sentenceSource;

		private IList<string> _words; //for title
		private IList<string> _sentences;

		private static uint RatelowBound;
		private static uint RateupBound;
		private static void GetRateBounds()
		{
			Console.Write("\n>Rate lowerbound: ");
			RatelowBound = uint.TryParse(Console.ReadLine(), out uint parsedLB) ? parsedLB : throw new Exception("Couldn't parse lowerbound");

			Console.Write("\n>Rate upperbound: ");
			RateupBound = uint.TryParse(Console.ReadLine(), out uint parsedUB) ? parsedUB : throw new Exception("Couldn't parse upperbound");

			if (!(RatelowBound > 0 && RatelowBound <= 10))
			{
				throw new Exception("Lowerbound value is inappropriate");
			}

			if (!(RateupBound > 0 && RateupBound <= 10))
			{
				throw new Exception("Uppbound value is inappropriate");
			}

			if (!(RatelowBound < RateupBound))
			{
				throw new Exception("Lowerbound is greater than upperbound");
			}

			if (RatelowBound == RateupBound)
			{
				throw new Exception("Lowerbound equals to upperbound");
			}
		}

		static ReviewProducer()
		{
			GetRateBounds();
		}
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
				ReviewText = GenerateReviewText(),
				Rate = GenerateRate(),
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

		private double GenerateRate()
		{
			int integerUnit;
			double afterPointUnit;

			integerUnit = _randProvider.Next((int)RatelowBound, (int)RateupBound);
			afterPointUnit = _randProvider.NextDouble();

			return integerUnit + afterPointUnit;
		}
	}
}
