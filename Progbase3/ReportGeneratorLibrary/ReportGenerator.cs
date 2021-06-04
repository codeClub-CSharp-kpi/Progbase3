using ScottPlot;
using System;

namespace ReportGeneratorLibrary
{
	public static class ReportGenerator
	{
		private static string _path;

		public static void GenerateReport(string path)
		{
			_path = path;
		}

		private static void GenerateWord()
		{

		}

		private static void GenerateImage()
		{
			var plt = new Plot(600, 400);

			OHLC[] ohlcs = DataGen.RandomStockPrices(rand: null, pointCount: 60, deltaMinutes: 10);
			plt.Title("Ten Minute Candlestick Chart");
			plt.YLabel("Stock Price (USD)");
			plt.PlotCandlestick(ohlcs);
			plt.Ticks(dateTimeX: true);

			plt.SaveFig("PlotTypes_Finance_Candle.png");
		}
	}
}
