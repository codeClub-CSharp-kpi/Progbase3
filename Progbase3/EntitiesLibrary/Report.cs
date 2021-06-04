using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLibrary
{
	public static class Report
	{
		private static string _path;

		

		public static void GenerateReport(string path)
		{
			_path = path;
		}

		private static void GenerateWord()
		{

		}

		private static void GenerateImage(List<Review> sourceReviewsOfTheActorFilm)
		{
			
		}
	}
}
