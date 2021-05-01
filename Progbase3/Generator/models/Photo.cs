namespace Generator.models
{
	public class Photo: IEntity
	{
		public int Id { get; set; }
		public string Path { get; set; }

		public Actor Actor { get; set; }
	}
	
}
