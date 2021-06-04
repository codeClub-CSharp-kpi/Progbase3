using EntitiesLibrary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xceed.Words.NET;


namespace WordGeneratorLib
{
	public static class WordGenerator
	{
		public static void GenerateWithActorData(string destinationDirectory, Actor sourceActor)
		{
			using (FileStream fs = new($@"{destinationDirectory}\report.docx", FileMode.OpenOrCreate, FileAccess.Write))
			{
				DocX theDoc = DocX.Create(fs, Xceed.Document.NET.DocumentTypes.Document);

				theDoc.InsertParagraph(sourceActor.ToString());
				theDoc.InsertParagraph($"Total featured films: {sourceActor.Films.Count()}");

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

				theDoc.InsertParagraph($"Film with max rating: ");
				WriteFilmToDoc(filmWithMaxRating, theDoc);

				theDoc.InsertParagraph($"Film with min rating: ");
				WriteFilmToDoc(filmWithMinRating, theDoc);

				theDoc.InsertParagraph($"Average rating of featured films: {filmsAvgRating}");


				theDoc.Save();
			}
		}

		private static void WriteFilmToDoc(Film f, DocX docToWtireIn)
		{
			docToWtireIn.InsertParagraph($"Title: {f.Title}");
			docToWtireIn.InsertParagraph($"Official Release: {f.OfficialReleaseDate}");
			docToWtireIn.InsertParagraph($"Slogan: {f.Slogan}");
			docToWtireIn.InsertParagraph($"Story Line: {f.StoryLine}");
			docToWtireIn.InsertParagraph($"\r\n");
		}
	}
}
