using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator
{
	[Table("Reviews")]
	class Review
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Title { get; set; } // 50 symbs

		[Required]
		[Column(TypeName="bit")]
		public bool isPositive { get; set; }

		[Required]
		[StringLength(250)]
		public string ReviewText { get; set; } // 250 symbs
		
		[ForeignKey("Film")]
		public int FilmId { get; set; }
		public virtual Film Film { get; set; }
		
	}
}
