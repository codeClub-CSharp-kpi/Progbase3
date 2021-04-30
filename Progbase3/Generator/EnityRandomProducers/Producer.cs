using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.EnityRandomProducers
{
	public abstract class Producer
	{
		private protected readonly string _fakeDataSource;
		public Producer()
		{
			_fakeDataSource = @"..\..\..\..\..\data\Generator\";
		}
		public abstract models.IEntity Create();

		// delegates for fake-dataset interaction
		private protected Func<string, List<string>> ReadByLines = (fullPath =>
		{
			List<string> lineStorage = new List<string>();

			using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
			{
				using (StreamReader sr = new StreamReader(fs))
				{
					string buff = String.Empty;
					do
					{
						buff = sr.ReadLine();
						if (buff == null)
							break;

						lineStorage.Add(buff);

					} while (true);

				}
			}
			return lineStorage;
		});

		private protected Func<string, List<string>> ReadBySentences = new Func<string, List<string>>(fullPath =>
		{
			List<string> _sentences = new List<string>();

			using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
			{
				using (StreamReader sr = new StreamReader(fs))
				{
					StringBuilder sb = new StringBuilder();

					string buff = String.Empty;
					do
					{
						buff = sr.ReadLine();

						if (buff == null)
							break;

						sb.Append(buff);
					}
					while (true);

					foreach (var sen in sb.ToString().Split('.'))
					{
						_sentences.Add(sen);
					}
				}
			}
			return _sentences;
		}
	}
}
