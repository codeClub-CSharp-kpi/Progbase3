using System.Linq;

namespace EntitiesLibrary
{
	public class Photo : IEntity
	{
		public int Id { get; set; }
		public string Path { get; set; }

		public Actor Actor
		{
			get
			{
				return new ActorRepository().GetAll().Where(a => a.PhotoId == Id).FirstOrDefault();
			}
		}
	}

}
