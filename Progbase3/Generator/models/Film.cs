using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
	[Table("Films")]
	class Film
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		[StringLength(50)]
		public string Title { get; set; }
		
		[Required]
		[Column(TypeName="float")]
		public double Rate { get; set; }

		[Required]
		[Column(TypeName = "date")]
		public DateTime OfficialReleaseDate { get; set; }

		[Required]
		[StringLength(50)]
		public string Slogan { get; set; } // 50 symbols

		[Required]
		[StringLength(250)]
		public string StoryLine { get; set; } // 250 symbols

		public virtual ICollection<Review> Reviews { get; set; }
	}
}
