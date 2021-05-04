using Generator.UI;
using System;

namespace Generator
{
	class Program
	{
		static void Main(string[] args)
		{
			Menu m = new Menu();
			try
			{
				do
				{
					m.GetEntityChoice();
					if (m.IsExit)
					{
						break;
					}
					m.ProceedGeneration();
				} 
				while (true);
			}
			catch (Exception err)
			{
				Console.WriteLine($"Error: {err.InnerException?.Message ?? err.Message}");
			}
		}
	}
}
