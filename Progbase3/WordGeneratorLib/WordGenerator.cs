using EntitiesLibrary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;

namespace WordGeneratorLib
{
	public static class WordGenerator
	{
		private static IWParagraph paragraph;

		public static void GenerateWithActorData(string destinationDirectory, Actor sourceActor)
		{
			using (FileStream fs = new($"{destinationDirectory}/report.docx", FileMode.OpenOrCreate))
			{

				WordDocument newWd = new WordDocument();

				IWSection section = newWd.AddSection();

				paragraph = section.AddParagraph();

				paragraph.AppendText(sourceActor.ToString());

				paragraph = section.AddParagraph();
				paragraph.AppendText($"Total featured films: {sourceActor.Films.Count()}");

				Dictionary<Film, double> eachFilmAvg = new();
				foreach (var f in sourceActor.Films)
				{
					if (f.Reviews.Any())
					{
						eachFilmAvg.Add(f, f.Reviews.Average(obj => obj.Rate));
					}
				}

				double filmsAvgRating = 0.0;
				Film filmWithMaxRating;
				Film filmWithMinRating;

				filmWithMaxRating = filmWithMinRating = new Film()
				{
					Title = "-",
					OfficialReleaseDate = default,
					Slogan = "-",
					StoryLine = "-",
				};

				if (eachFilmAvg.Any())
				{
					filmsAvgRating = eachFilmAvg.Average(obj => obj.Value);
					filmWithMaxRating = eachFilmAvg.Where(obj => obj.Value == eachFilmAvg.Max(obj => obj.Value)).Select(obj => obj.Key).First();
					filmWithMinRating = eachFilmAvg.Where(obj => obj.Value == eachFilmAvg.Min(obj => obj.Value)).Select(obj => obj.Key).First();
				}

				paragraph = section.AddParagraph();
				paragraph.AppendText($"Film with max rating: ");
				WriteFilmToDoc(filmWithMaxRating);

				paragraph = section.AddParagraph();
				paragraph.AppendText($"\nFilm with min rating: ");
				WriteFilmToDoc(filmWithMinRating);

				paragraph = section.AddParagraph();
				paragraph.AppendText($"\nAverage rating of featured films: {filmsAvgRating}");
				newWd.Save(fs, FormatType.Docx);
			}
			

		}

		private static void WriteFilmToDoc(Film f)
		{
			paragraph.AppendText($"\nTitle: {f.Title}");
			paragraph.AppendText($"\nOfficial Release: {f.OfficialReleaseDate}");
			paragraph.AppendText($"\nSlogan: {f.Slogan}");
			paragraph.AppendText($"\nStory Line: {f.StoryLine}");
			paragraph.AppendText($"\r\n");
		}
	}
}
