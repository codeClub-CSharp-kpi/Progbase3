﻿using NetManagers;
using System;

namespace EntitiesLibrary
{
	[Serializable]
	public class Review : IEntity
	{
		public int Id { get; set; }
		public string Title { get; set; } // 50 symbs
		public bool isPositive { get; set; }
		public string ReviewText { get; set; } // 250 symbs
		public double Rate { get; set; }

		public int FilmId { get; set; }
		public Film Film
		{
			get
			{
				return TcpQueryManager.ExecQuery("GetFilmById", FilmId) as Film;
			}
		}
	}

}
