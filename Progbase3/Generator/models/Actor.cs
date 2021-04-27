using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator.models
{
	class Actor
	{
		public int Id { get; set; } // primary key
		public string Name { get; set; } // name of actor / 50 symbs
		public string Patronimic { get; set; } // middle name / 50 symbs
		public string Surname { get; set; } // last name / 50 symbs
		public string Bio { get; set; } // actor biography / 250 symbs
		public int PhotoId { get; set; } // apearence
		public int CityId { get; set; } // where was born
	}

	class Photo
	{
		public int Id { get; set; }
		public string Path { get; set; }
	}

	class Country
	{
		public int Id { get; set; }
		public string Name { get; set; } // 50 symbs
	}

	class City
	{
		public int Id { get; set; }
		public string Name { get; set; } // 50 symbs
		public int CounrtyId { get; set; }
	}
	
}
