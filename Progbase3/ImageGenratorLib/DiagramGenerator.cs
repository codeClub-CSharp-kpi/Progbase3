
using EntitiesLibrary;
using ScottPlot;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImageGenratorLib
{
	public static class DiagramGenerator
	{
		public static string CompileStatisticsAccordingToActor(string destinationDirectory, Actor sourceActor)
		{
			var plt = new Plot(600, 400);

			string savePath = null;

			Dictionary<Film, double> eachFilmAvg = new();

			if (sourceActor.Films.Any())
			{
				var featuredFilms = sourceActor.Films.OrderBy(obj => obj.OfficialReleaseDate);

				if (featuredFilms.Count() % 2 != 0)
				{
					eachFilmAvg.Add(new Film(), 0.0);
				}

				foreach (var f in featuredFilms)
				{
					var currFilmReviews = f.Reviews;
					if (currFilmReviews.Any())
					{
						eachFilmAvg.Add(f, f.Reviews.Average(obj => obj.Rate));
					}
				}

				List<double> values = new();
				var t = eachFilmAvg.Select(obj => obj.Value).ToList();

				for (int i = 0; i < eachFilmAvg.Count; i++)
				{
					if (i + 1 == eachFilmAvg.Count)
					{
						break;
					}
					values.Add(t[i + 1] - t[i]);
				}
				

				double[] offsets = eachFilmAvg.Select(obj=>obj.Value).ToArray();

				var bar = plt.AddBar(values.ToArray());
				bar.ValueOffsets = offsets;
				bar.FillColorNegative = Color.Red;
				bar.FillColor = Color.Green;
				
				savePath = plt.SaveFig($@"{destinationDirectory}\Compiled_Stat_Pic.png");
			}

			return savePath;
		}
	}

}
