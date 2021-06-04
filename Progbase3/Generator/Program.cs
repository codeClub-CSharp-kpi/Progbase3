using Generator.UI;
using System;
using System.Threading.Tasks;

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
					Task t1 = new(() => m.ProceedGeneration());
					t1.Start();
					t1.Wait();
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
