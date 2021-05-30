using Generator.models;
using Generator.Repostitories.implementations;
using System.Linq;

namespace Generator.EnityRandomProducers
{
	class FilmActorProducer: RandomProducer
	{
		public override IEntity Create()
		{
			int genedFId;
			int genedAId;

			FilmActorRepository fr = new();

			do
			{
				genedFId = GenerateFilmId();
				genedAId = GenerateActorId();
			}
			while (fr.GetAll().Where(obj => obj.FilmId == genedFId && obj.ActorId == genedAId).FirstOrDefault() != null);

			return new FilmActor()
			{
				FilmId = genedFId,
				ActorId = genedAId
			};
		}

		private int GenerateFilmId()
		{
			FilmRepository fr = new();
			var avaliableFilmIDs = fr.GetAll().Select(obj => obj.Id).ToList(); // defining, what IDs exist

			return avaliableFilmIDs[_randProvider.Next(avaliableFilmIDs.Count)];
		}

		private int GenerateActorId()
		{
			ActorRepository ar = new();
			var avaliableActorIDs = ar.GetAll().Select(obj => obj.Id).ToList(); // defining, what IDs exist

			return avaliableActorIDs[_randProvider.Next(avaliableActorIDs.Count)];
		}
	}
}
