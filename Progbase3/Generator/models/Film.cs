using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.models
{
	public class Film: UserEntity
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public double Rate { get; set; }
		public DateTime OfficialReleaseDate { get; set; }
		public string Slogan { get; set; } // 50 symbols
		public string StoryLine { get; set; } // 250 symbols
	}
}
