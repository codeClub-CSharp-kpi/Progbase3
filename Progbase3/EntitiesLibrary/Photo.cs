using NetManagers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntitiesLibrary
{
	[Serializable]
	public class Photo : IEntity
	{
		public int Id { get; set; }
		public string Path { get; set; }

		public Actor Actor
		{
			get
			{
				return (TcpQueryManager.ExecQuery("GetAllActors") as IEnumerable<Actor>).Where(a => a.PhotoId == Id).FirstOrDefault();
			}
		}
	}

}
