using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator.Models
{
	[Table("Actors")]
	class Actor
	{
		[Key]
		public int Id { get; set; } // primary key
		
		[Required]
		[StringLength(50)]
		public string Name { get; set; } // name of actor / 50 symbs
		
		[Required]
		[StringLength(50)]
		public string Patronimic { get; set; } // middle name / 50 symbs
		
		[Required]
		[StringLength(50)]
		public string Surname { get; set; } // last name / 50 symbs
		
		[Required]
		[StringLength(250)]
		public string Bio { get; set; } // actor biography / 250 symbs

		[ForeignKey("Photo")]
		public string PhotoId { get; set; } // apearence
		public virtual Photo Photo { get; set; }

		[ForeignKey("City")]
		public int CityId { get; set; } // where was born
		public virtual City City { get; set; }
	}

	[Table("Photos")]
	class Photo
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public string Path { get; set; }

		public virtual Actor Actor { get; set; }
	}

	[Table("Countries")]
	class Country
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; } // 50 symbs

		public virtual ICollection<City> Cities { get; set; }
	}

	[Table("Cities")]
	class City
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		[StringLength(50)]
		public string Name { get; set; } // 50 symbs
		
		[ForeignKey("Country")]
		public int CounrtyId { get; set; }
		public virtual Country Country { get; set; }
		
		public virtual ICollection<Actor> Actors { get; set; }
	}
	
}
