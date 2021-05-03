using Generator.EnityRandomProducers;
using Generator.models;
using Generator.Repostitories.implementations;
using Generator.Repostitories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.UI
{
	class Menu
	{
		enum Models
		{
			Actor = 1,
			Film,
			Review
		};
		enum CRUD_Operations
		{
			Insert,
			Delete,
			Update,
			GetAll
		};

		private int _choice;

		private IRepository<IEntity> _repo;
		private RandomProducer _rp;

		public bool IsExit { get; set; }

		public Menu()
		{
			
		}
		
		public void GetEntityChoice()
		{
			ShowEntityChoiceMenu();

			
			switch (GetChoice())
			{
				case (int)Models.Actor:
					_repo = (IRepository<IEntity>)new ActorRepository();
					_rp = new ActorProducer();
					break;
				case (int)Models.Film:
					_repo = (IRepository<IEntity>)new FilmRepository();
					_rp = new FilmProducer();
					break;
				case (int)Models.Review:
					_repo = (IRepository<IEntity>)new ReviewRepository();
					_rp = new ReviewProducer();
					break;
				default:
					throw new Exception("Invalid entity choice!");
			}
			Console.WriteLine("\n\n");
		}
		public void ProceedGeneration()
		{
			ulong count = GetCountOfEntities();
			int i = 0;

			while ((ulong)i < count)
			{
				_repo.Insert(_rp.Create());
				i++;
			}

		}
		
		private int GetChoice()
		{
			Console.Write("\n>");

			int choice = int.TryParse(Console.ReadLine(), out int parsedChoice) ? 
				parsedChoice : 
				throw new Exception("Couldn't parse the choice!");

			choice = _choice;

			return choice;
		}
		private ulong GetCountOfEntities()
		{
			Console.Write("\n>Count of entities: ");
			return ulong.TryParse(Console.ReadLine(), out ulong parsed)
				? parsed
				: throw new Exception("Couldn't recognize count of entities!");
		}
		private void ShowEntityChoiceMenu()
		{
			Console.WriteLine("\n\n");
			Console.WriteLine("---------------------");
			Console.WriteLine("1.  Generate actors  ");
			Console.WriteLine("2.  Generate films  ");
			Console.WriteLine("3.  Generate review  ");
			Console.WriteLine("---------------------");
			Console.WriteLine("\n\n");
		}
		

	}
}
