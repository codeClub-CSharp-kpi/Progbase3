
using DataGeneratorsLibrary;
using EntitiesLibrary;
using System;

namespace Generator.UI
{
	class Menu
	{
		enum Models
		{
			Actor = 1,
			Film,
			Review,
			FilmActor,
			None
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

		IActorRepository _actRepo;
		IFilmRepository _filmRepo;
		IReviewRepository _revRepo;
		IFilmActorRepository _filmActRepo;


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
				case (int)Models.FilmActor:
					_repo = new FilmActorRepository();
					break;
				case (int)Models.None:
					IsExit = true;
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
			_filmActRepo = _repo as FilmActorRepository;

			RandomProducer rp = null;

			if (_actRepo != null)
			{
				rp = new ActorProducer();
			}
			else if (_filmRepo != null)
			{
				rp = new FilmProducer();
			}
			else if(_revRepo != null)
			{
				rp = new ReviewProducer();
			}
			else if (_filmActRepo != null)
			{
				rp = new FilmActorProducer();
			}

			IEntity genEntity = null;
			while ((ulong)i < count)
			{
				genEntity = rp.Create();
				_actRepo?.Insert(genEntity as Actor);
				_filmRepo?.Insert(genEntity as Film);
				_revRepo?.Insert(genEntity as Review);
				_filmActRepo?.Insert(genEntity as FilmActor);

				i++;
			}
			Console.WriteLine("Success!");
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
			Console.WriteLine("{0}.  Generate actors  ", (int)Models.Actor);
			Console.WriteLine("{0}.  Generate films   ", (int)Models.Film);
			Console.WriteLine("{0}.  Generate review  ", (int)Models.Review);
			Console.WriteLine("{0}.  Generate film-actor  ", (int)Models.FilmActor);
			Console.WriteLine("{0}.  Exit             ", (int)Models.None);
			Console.WriteLine("---------------------");
			Console.WriteLine("\n\n");
		}
		

	}
}
