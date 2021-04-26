using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
	class Review
	{
		public int Id { get; set; }
		public string Title { get; set; } // 50 symbs
		public bool isPositive { get; set; }
		public string ReviewText { get; set; } // 250 symbs
		public int FilmId { get; set; }
	}
}
