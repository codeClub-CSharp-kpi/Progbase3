namespace EntitiesLibrary
{
	public class FilmActor : IEntity
	{
		public int Id { get; set; }

		public int FilmId { get; set; }
		public Film Film { get; set; }

		public int ActorId { get; set; }
		public Actor Actor { get; set; }
	}
}
