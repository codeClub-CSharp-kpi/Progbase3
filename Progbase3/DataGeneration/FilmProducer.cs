using EntitiesLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGeneration
{
	public class FilmProducer : RandomProducer
	{
		private string _titleSource;
		private string _sentenceSource;

		private IList<string> _words; //for title
		private IList<string> _sentences;

		private DateTime _offReleaseLowBound;
		private DateTime _offReleaseUpBound;
		private void GetOfficialReleaseBounds()
		{
			Console.Write("\n>Release date lowerbound (DD-MM-YYYY): ");
			_offReleaseLowBound = DateTime.TryParse(Console.ReadLine(), out DateTime parsedLB) ? parsedLB : throw new Exception("Couldn't parse lowerbound");

			Console.Write("\n>Release date upperbound (DD-MM-YYYY): ");
			_offReleaseUpBound = DateTime.TryParse(Console.ReadLine(), out DateTime parsedUB) ? parsedUB : throw new Exception("Couldn't parse upperbound");

			if (!(_offReleaseLowBound < _offReleaseUpBound))
			{
				throw new Exception("Lowerbound is greater than upperbound");
			}

			if (_offReleaseLowBound == _offReleaseUpBound)
			{
				throw new Exception("Lowerbound equals to upperbound");
			}
		}

		public FilmProducer()
		{
			_titleSource = _fakeDataSource + "titles";
			_sentenceSource = _fakeDataSource + "loremIpsum";
			GetOfficialReleaseBounds();
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
			int daysBetween = (_offReleaseUpBound - _offReleaseLowBound).Days;
			int daysToAdd = _randProvider.Next(daysBetween); //from 0..to..daysCount

			return _offReleaseLowBound.AddDays(daysToAdd);
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
