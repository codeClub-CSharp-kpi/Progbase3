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

		object _repo;

		Repostitories.interfaces.IActorRepository _actRepo;
		Repostitories.interfaces.IFilmRepository _filmRepo;
		Repostitories.interfaces.IReviewRepository _revRepo;



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
					_repo = new ActorRepository();
					break;
				case (int)Models.Film:
					_repo = new FilmRepository();
					break;
				case (int)Models.Review:
					_repo = new ReviewRepository();
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

			_actRepo = _repo as ActorRepository;
			_filmRepo = _repo as FilmRepository;
			_revRepo = _repo as ReviewRepository;

			while ((ulong)i < count)
			{
				_actRepo?.Insert(new ActorProducer().Create() as Actor);
				_filmRepo?.Insert(new FilmProducer().Create() as Film);
				_revRepo?.Insert(new ReviewProducer().Create() as Review);
				
				i++;
			}

		}
		
		private int GetChoice()
		{
			Console.Write("\n>");

			int choice = int.TryParse(Console.ReadLine(), out int parsedChoice) ? 
				parsedChoice : 
				throw new Exception("Couldn't parse the choice!");

			_choice = choice;

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
