using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.models
{
	public class FilmsActors: IEntity
	{
		public int Id { get; set; }
		public int FilmId { get; set; }
		public int ActorId { get; set; }
	}
}
