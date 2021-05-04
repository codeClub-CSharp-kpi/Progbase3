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

		private static DateTime OffReleaseLowBound;
		private static DateTime OffReleaseUpBound;
		private static void GetOfficialReleaseBounds()
		{
			Console.Write("\n>Release date lowerbound (DD-MM-YYYY): ");
			OffReleaseLowBound = DateTime.TryParse(Console.ReadLine(), out DateTime parsedLB) ? parsedLB : throw new Exception("Couldn't parse lowerbound");

			Console.Write("\n>Release date upperbound (DD-MM-YYYY): ");
			OffReleaseUpBound = DateTime.TryParse(Console.ReadLine(), out DateTime parsedUB) ? parsedUB : throw new Exception("Couldn't parse upperbound");

			if (!(OffReleaseLowBound < OffReleaseUpBound))
			{
				throw new Exception("Lowerbound is greater than upperbound");
			}

			if (OffReleaseLowBound == OffReleaseUpBound)
			{
				throw new Exception("Lowerbound equals to upperbound");
			}
		}
		
		static FilmProducer()
		{
			GetOfficialReleaseBounds();
		}
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
				OfficialReleaseDate = GenerateOffRelease(),
				Slogan = GenerateSlogan(),
				StoryLine = GenerateStoryLine()
			};
		}

		private string GenerateTitle()
		{
			_words = ReadByLines(_titleSource);
			return _words[_randProvider.Next(_words.Count)];
		}

		private DateTime GenerateOffRelease()
		{
			int daysBetween = (OffReleaseUpBound - OffReleaseLowBound).Days;
			int daysToAdd = _randProvider.Next(daysBetween); //from 0..to..daysCount

			return OffReleaseLowBound.AddDays(daysToAdd);
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
