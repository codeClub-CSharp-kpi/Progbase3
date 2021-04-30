using System;
using System.Collections.Generic;
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
	}
}
