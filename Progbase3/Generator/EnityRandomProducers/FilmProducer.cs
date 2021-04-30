using Generator.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Generator.EnityRandomProducers
{
	public class FilmProducer : RandomProducer
	{
		private string _titleSource;
		private string _sentenceSource;

		private IList<string> _words; //for title
		private IList<string> _sentences;

		public FilmProducer()
		{
			_titleSource = _fakeDataSource + "titles";
			_sentenceSource = _fakeDataSource + "loremIpsum";
		}

		public override IEntity Create()
		{
			return new Film()
			{
				Title = GenerateTitle(),
				Rate = GenerateRate(),
				OfficialReleaseDate = GenerateOffRelease(),
				Slogan = GenerateSlogan(),
				StoryLine = GenerateStoryLine(),
			};
		}

		private string GenerateTitle()
		{
			_words = ReadByLines(_titleSource);
			return _words[_randProvider.Next(_words.Count)];
		}

		private double GenerateRate()
		{
			Console.Write("\n>Rate lowerbound: ");
			uint lowBound = uint.TryParse(Console.ReadLine(), out uint parsedLB) ? parsedLB : throw new Exception("Couldn't parse lowerbound");
			
			Console.Write("\n>Rate upperbound: ");
			uint upBound = uint.TryParse(Console.ReadLine(), out uint parsedUB) ? parsedUB : throw new Exception("Couldn't parse upperbound");

			if (lowBound > 0 && lowBound <= 10)
			{
				throw new Exception("Lowerbound value is inappropriate");
			}
			
			if (upBound > 0 && upBound <= 10)
			{
				throw new Exception("Uppbound value is inappropriate");
			}

			if (!(lowBound < upBound))
			{
				throw new Exception("Lowerbound is greater than upperbound");
			}
			
			if (lowBound == upBound)
			{
				throw new Exception("Lowerbound equals to upperbound");
			}

			int integerUnit;
			double afterPointUnit;

			integerUnit = _randProvider.Next( (int)lowBound, (int)upBound);
			afterPointUnit = _randProvider.NextDouble();
			
			return integerUnit + afterPointUnit;
		}

		private DateTime GenerateOffRelease()
		{
			Console.Write("\n>Release date lowerbound (DD-MM-YYYY): ");
			DateTime lowBound = DateTime.TryParse(Console.ReadLine(), out DateTime parsedLB) ? parsedLB : throw new Exception("Couldn't parse lowerbound");

			Console.Write("\n>Release date upperbound (DD-MM-YYYY): ");
			DateTime upBound = DateTime.TryParse(Console.ReadLine(), out DateTime parsedUB) ? parsedUB : throw new Exception("Couldn't parse upperbound");

			if (!(lowBound < upBound))
			{
				throw new Exception("Lowerbound is greater than upperbound");
			}

			if (lowBound == upBound)
			{
				throw new Exception("Lowerbound equals to upperbound");
			}

			int daysBetween = (upBound - lowBound).Days;
			int daysToAdd = _randProvider.Next(daysBetween); //from 0..to..daysCount

			return lowBound.AddDays(daysBetween);
		}

		private string GenerateSlogan()
		{
			return GenerateTitle() + " " + GenerateTitle();
		}

		private string GenerateStoryLine()
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
