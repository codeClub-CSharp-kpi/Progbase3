﻿using System;

namespace EntitiesLibrary
{
	[Serializable]
	public class ReviewAccount:IEntity
	{
		public int Id { get; set; }
		public int ReviewId { get; set; }
		public int AccountId { get; set; }
	}
}
