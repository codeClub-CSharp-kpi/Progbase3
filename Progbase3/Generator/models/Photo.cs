using Generator.Repostitories.implementations;
using System.Linq;

namespace Generator.models
{
	public delegate void ChangedField(object obj);
	public class Photo: IEntity
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
