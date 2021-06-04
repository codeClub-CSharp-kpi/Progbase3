
using EntitiesLibrary;
using ScottPlot;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImageGenratorLib
{
	public static class DiagramGenerator
	{
		public static void CompileStatisticsAccordingToActor(string destinationDirectory, Actor sourceActor)
		{
			var plt = new Plot(600, 400);

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

				plt.SaveFig($@"{destinationDirectory}\Compiled_Stat_Pic.png");
			}

			//var plt = new ScottPlot.Plot(600, 400);

			//double[] values = DataGen.RandomNormal(0, 12, 5, 10);
			//double[] offsets = Enumerable.Range(0, values.Length).Select(x => values.Take(x).Sum()).ToArray();

			//var bar = plt.AddBar(values);
			//bar.ValueOffsets = offsets;
			//bar.FillColorNegative = Color.Red;
			//bar.FillColor = Color.Green;

			//plt.SaveFig("bar_waterfall.png");
		}
	}

}
