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

		object _repo;

		Repostitories.interfaces.IActorRepository _actRepo;
		Repostitories.interfaces.IFilmRepository _filmRepo;
		Repostitories.interfaces.IReviewRepository _revRepo;

		public bool IsExit { get; set; }

		public Menu()
		{
			
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
		
		public void GetEntityChoice()
		{
			ShowEntityChoiceMenu();
			switch (GetChoice())
			{
				case (int)Models.Actor:
					_repo = new Repostitories.implementations.ActorRepository();
					break;
				case (int)Models.Film:
					_repo = new Repostitories.implementations.FilmRepository();
					break;
				case (int)Models.Review:
					_repo = new Repostitories.implementations.ReviewRepository();
					break;
				default:
					throw new Exception("Invalid entity choice!");
			}
			Console.WriteLine("\n\n");
		}
		
		public void HandleOperationChoice()
		{
			ulong countOfEntities = ulong.TryParse(Console.ReadLine(), out ulong parsed)
				? parsed 
				: throw new Exception("Couldn't recognize count of entities!");


			_actRepo = _repo as Repostitories.implementations.ActorRepository;
			_filmRepo = _repo as Repostitories.implementations.FilmRepository;
			_revRepo = _repo as Repostitories.implementations.ReviewRepository;

		}


		void ShowEntityChoiceMenu()
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
