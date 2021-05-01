using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator.models
{
	public class Actor: IEntity
	{
		public int Id { get; set; } // primary key
		public string Name { get; set; } // name of actor / 50 symbs
		public string Patronimic { get; set; } // middle name / 50 symbs
		public string Surname { get; set; } // last name / 50 symbs
		public string Bio { get; set; } // actor biography / 250 symbs
		public int PhotoId { get; set; } // apearence
		public Photo Photo { get; set; }

		public int CityId { get; set; } // where was born
		public City City { get; set; }

		//nav-ref that creates many2many relation
		public IEnumerable<Film> Films { get; set; } // featured films
	}
	
}
