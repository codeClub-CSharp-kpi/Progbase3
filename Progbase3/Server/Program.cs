using EntitiesLibrary;
using RepositoryLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Server
{
	class Program
	{

		private static TcpListener listener;
		private static TcpClient client;

		private static NetworkStream netStream;
		private static StreamReader sr;

		enum Queries
		{
			AddAccount,
			AddActor,
			AddCity,
			AddCountry,
			AddFilm,
			AddFilmActor,
			AddPhoto,
			AddReview,
			AddReviewAccount,
			DelActor,
			DeleteCity,
			DeleteCountry,
			DeletePhoto,
			DeleteReview,
			DelFilm,
			DelFilmActor,
			DelReview,
			DelReviewAccount,
			GetActor,
			GetActorsByFilm,
			GetActorsPage,
			GetAllAccounts,
			GetAllActors,
			GetAllCities,
			GetAllCountries,
			GetAllFilms,
			GetAllFilmsActors,
			GetAllPhotos,
			GetAllReviews,
			GetAllReviewsAccounts,
			GetAllRoles,
			GetCityById,
			GetCountryById,
			GetFilm,
			GetFilmById,
			GetFilmsByActor,
			GetFilmsPage,
			GetPhotoById,
			GetReview,
			GetReviewsPage,
			UpdateActor,
			UpdateCity,
			UpdateCountry,
			UpdateFilm,
			UpdateFilmActor,
			UpdatePhoto,
			UpdateReview,
		}
		private static Dictionary<string, Queries> QueryStr_QueryEnumDict= new()
		{
			{ "AddAccount", Queries.AddAccount },
			{ "AddActor", Queries.AddActor },
			{ "AddCity", Queries.AddCity },
			{ "AddCountry", Queries.AddCountry },
			{ "AddFilm", Queries.AddFilm },
			{ "AddFilmActor", Queries.AddFilmActor },
			{ "AddPhoto", Queries.AddPhoto },
			{ "AddReview", Queries.AddReview },
			{ "AddReviewAccount", Queries.AddReviewAccount },
			{ "DelActor", Queries.DelActor },
			{ "DeleteCity", Queries.DeleteCity },
			{ "DeleteCountry", Queries.DeleteCountry },
			{ "DeletePhoto", Queries.DeletePhoto },
			{ "DeleteReview", Queries.DeleteReview },
			{ "DelFilm", Queries.DelFilm },
			{ "DelFilmActor", Queries.DelFilmActor },
			{ "DelReview", Queries.DelReview },
			{ "DelReviewAccount", Queries.DelReviewAccount },
			{ "GetActor", Queries.GetActor },
			{ "GetActorsByFilm", Queries.GetActorsByFilm },
			{ "GetActorsPage", Queries.GetActorsPage },
			{ "GetAllAccounts", Queries.GetAllAccounts },
			{ "GetAllActors", Queries.GetAllActors },
			{ "GetAllCities", Queries.GetAllCities },
			{ "GetAllCountries", Queries.GetAllCountries },
			{ "GetAllFilms", Queries.GetAllFilms },
			{ "GetAllFilmsActors", Queries.GetAllFilmsActors },
			{ "GetAllPhotos", Queries.GetAllPhotos },
			{ "GetAllReviews", Queries.GetAllReviews },
			{ "GetAllReviewsAccounts", Queries.GetAllReviewsAccounts },
			{ "GetAllRoles", Queries.GetAllRoles },
			{ "GetCityById", Queries.GetCityById },
			{ "GetCountryById", Queries.GetCountryById },
			{ "GetFilm", Queries.GetFilm },
			{ "GetFilmById", Queries.GetFilmById },
			{ "GetFilmsByActor", Queries.GetFilmsByActor },
			{ "GetFilmsPage", Queries.GetFilmsPage },
			{ "GetPhotoById", Queries.GetPhotoById },
			{ "GetReview", Queries.GetReview },
			{ "GetReviewsPage", Queries.GetReviewsPage },
			{ "UpdateActor", Queries.UpdateActor },
			{ "UpdateCity", Queries.UpdateCity },
			{ "UpdateCountry", Queries.UpdateCountry },
			{ "UpdateFilm", Queries.UpdateFilm },
			{ "UpdateFilmActor", Queries.UpdateFilmActor },
			{ "UpdatePhoto", Queries.UpdatePhoto },
			{ "UpdateReview", Queries.UpdateReview }
		};

		static void Main(string[] args)
		{
			int port = default;
			IPAddress ipAddress = default;

			Console.Write("\n> Enter port: ");
			port = int.Parse(Console.ReadLine());

			Console.Write("\n> Enter ip-address: ");
			ipAddress = IPAddress.Parse(Console.ReadLine());

			listener = new TcpListener(ipAddress, port);
			listener.Start(100);
			do
			{
				try
				{
					while (true)
					{
						Console.Write("\n\t> Waiting for client connection...");

						client = listener.AcceptTcpClient();
						Console.Write("\n\t> Got client...");
						netStream = client.GetStream();

						sr = new StreamReader(netStream);
						string userQueryRaw = sr.ReadLine();

						string[] parts = userQueryRaw.Split(';');

						string queryName = parts[0];
						byte[] argumentsInBytes = Convert.FromBase64String(parts[1]);
						object[] argumentsDeserialized = null;


						var br = new BinaryFormatter();
						using (MemoryStream ms = new(argumentsInBytes))
						{
							var obj = br.Deserialize(ms);
							argumentsDeserialized = obj as object[];
						}

						BinaryFormatter bf = new();
						try
						{
							if (!QueryStr_QueryEnumDict.ContainsKey(queryName))
							{
								throw new Exception("The query is invalid");
							}

							switch (QueryStr_QueryEnumDict[queryName])
							{
								case Queries.AddAccount:
									{
										_accRepo.Insert(argumentsDeserialized[0] as Account);
									}
									break;
								case Queries.AddActor:
									{
										_actRepo.Insert(argumentsDeserialized[0] as Actor);
									}
									break;
								case Queries.AddCity:
									{
										_cityRepo.Insert(argumentsDeserialized[0] as City);
									}
									break;
								case Queries.AddCountry:
									{
										_countryRepo.Insert(argumentsDeserialized[0] as Country);
									}
									break;
								case Queries.AddFilm:
									{
										_filmRepo.Insert(argumentsDeserialized[0] as Film);
									}
									break;
								case Queries.AddFilmActor:
									{
										_filmActorRepo.Insert(argumentsDeserialized[0] as FilmActor);
									}
									break;
								case Queries.AddPhoto:
									{
										_photoRepo.Insert(argumentsDeserialized[0] as Photo);
									}
									break;
								case Queries.AddReview:
									{
										_reviewRepo.Insert(argumentsDeserialized[0] as Review);
									}
									break;
								case Queries.AddReviewAccount:
									{
										_reviewAccountRepo.Insert(argumentsDeserialized[0] as ReviewAccount);
									}
									break;
								case Queries.DelActor:
									{
										_actRepo.Delete(Convert.ToInt32(argumentsDeserialized[0]));
									}
									break;
								case Queries.DeleteCity:
									{
										_cityRepo.Delete(Convert.ToInt32(argumentsDeserialized[0]));
									}
									break;
								case Queries.DeleteCountry:
									{
										_countryRepo.Delete(Convert.ToInt32(argumentsDeserialized[0]));
									}
									break;
								case Queries.DeletePhoto:
									{
										_photoRepo.Delete(Convert.ToInt32(argumentsDeserialized[0]));
									}
									break;
								case Queries.DeleteReview:
									{
										_reviewRepo.Delete(Convert.ToInt32(argumentsDeserialized[0]));
									}
									break;
								case Queries.DelFilm:
									{
										_filmRepo.Delete(Convert.ToInt32(argumentsDeserialized[0]));
									}
									break;
								case Queries.DelFilmActor:
									{
										_filmActorRepo.Delete(Convert.ToInt32(argumentsDeserialized[0]));
									}
									break;
								case Queries.DelReviewAccount:
									{
										_reviewAccountRepo.Delete(Convert.ToInt32(argumentsDeserialized[0]));
									}
									break;
								case Queries.GetActor:
									{
										bf.Serialize(netStream, _actRepo.GetById(Convert.ToInt32(argumentsDeserialized[0])));
									}
									break;
								case Queries.GetActorsByFilm:
									{
										bf.Serialize(netStream, _filmActorRepo.GetActorsByFilm(Convert.ToInt32(argumentsDeserialized[0])));
									}
									break;
								case Queries.GetActorsPage:
									{
										bf.Serialize(netStream, _actRepo.GetPage(Convert.ToInt32(argumentsDeserialized[0]), Convert.ToInt32(argumentsDeserialized[1])));
									}
									break;
								case Queries.GetAllAccounts:
									{

										var accs = _accRepo.GetAll();
										bf.Serialize(netStream, accs);
									}
									break;
								case Queries.GetAllActors:
									{
										bf.Serialize(netStream, _actRepo.GetAll());
									}
									break;
								case Queries.GetAllCities:
									{
										bf.Serialize(netStream, _cityRepo.GetAll());
									}
									break;
								case Queries.GetAllCountries:
									{
										bf.Serialize(netStream, _countryRepo.GetAll());
									}
									break;
								case Queries.GetAllFilms:
									{
										bf.Serialize(netStream, _filmRepo.GetAll());
									}
									break;
								case Queries.GetAllFilmsActors:
									{
										bf.Serialize(netStream, _filmActorRepo.GetAll());
									}
									break;
								case Queries.GetAllPhotos:
									{
										bf.Serialize(netStream, _photoRepo.GetAll());
									}
									break;
								case Queries.GetAllReviews:
									{
										bf.Serialize(netStream, _reviewRepo.GetAll());
									}
									break;
								case Queries.GetAllReviewsAccounts:
									{
										bf.Serialize(netStream, _reviewAccountRepo.GetAll());
									}
									break;
								case Queries.GetAllRoles:
									{
										bf.Serialize(netStream, _roleRepo.GetAll());
									}
									break;
								case Queries.GetCityById:
									{
										bf.Serialize(netStream, _cityRepo.GetById(Convert.ToInt32(argumentsDeserialized[0])));
									}
									break;
								case Queries.GetCountryById:
									{
										bf.Serialize(netStream, _countryRepo.GetById(Convert.ToInt32(argumentsDeserialized[0])));
									}
									break;
								case Queries.GetFilmById:
									{
										bf.Serialize(netStream, _filmRepo.GetById(Convert.ToInt32(argumentsDeserialized[0])));
									}
									break;
								case Queries.GetFilmsByActor:
									{
										bf.Serialize(netStream, _filmActorRepo.GetFilmsByActor(Convert.ToInt32(argumentsDeserialized[0])));
									}
									break;
								case Queries.GetFilmsPage:
									{
										bf.Serialize(netStream, _filmRepo.GetPage(Convert.ToInt32(argumentsDeserialized[0]), Convert.ToInt32(argumentsDeserialized[1])));
									}
									break;
								case Queries.GetPhotoById:
									{
										bf.Serialize(netStream, _photoRepo.GetById(Convert.ToInt32(argumentsDeserialized[0])));
									}
									break;
								case Queries.GetReview:
									{
										bf.Serialize(netStream, _reviewRepo.GetById(Convert.ToInt32(argumentsDeserialized[0])));
									}
									break;
								case Queries.GetReviewsPage:
									{
										bf.Serialize(netStream, _reviewRepo.GetPage(Convert.ToInt32(argumentsDeserialized[0]), Convert.ToInt32(argumentsDeserialized[1])));
									}
									break;
								case Queries.UpdateActor:
									{
										_actRepo.Update(argumentsDeserialized[0] as Actor);
									}
									break;
								case Queries.UpdateCity:
									{
										_cityRepo.Update(argumentsDeserialized[0] as City);
									}
									break;
								case Queries.UpdateCountry:
									{
										_countryRepo.Update(argumentsDeserialized[0] as Country);
									}
									break;
								case Queries.UpdateFilm:
									{
										_filmRepo.Update(argumentsDeserialized[0] as Film);
									}
									break;
								case Queries.UpdateFilmActor:
									{
										_filmActorRepo.Update(argumentsDeserialized[0] as FilmActor);
									}
									break;
								case Queries.UpdatePhoto:
									{
										_photoRepo.Update(argumentsDeserialized[0] as Photo);
									}
									break;
								case Queries.UpdateReview:
									{
										_reviewRepo.Update(argumentsDeserialized[0] as Review);
									}
									break;
								default:
									break;
							}
							Console.WriteLine($"\n>Executed {QueryStr_QueryEnumDict[queryName]}");
						}
						catch (Exception err)
						{
							string errorMessage = $"Error in operation: {err.Message}";
							bf.Serialize(netStream, errorMessage);
							throw new Exception(err.Message);
						}
					}
				}
				catch (Exception err)
				{
					Console.WriteLine($"\n>Server error: {err.Message}");
				}
				finally
				{
					sr?.Close();
					netStream?.Close();
					client?.Close();
					listener?.Stop();
				}
			} while (true);
		}


		// repository refferences
		private static AccountRepository _accRepo = new();
		private static ActorRepository _actRepo = new();
		private static CityRepository _cityRepo = new();
		private static CountryRepository _countryRepo = new();
		private static FilmRepository _filmRepo = new();
		private static FilmActorRepository _filmActorRepo = new();
		private static PhotoRepository _photoRepo = new();
		private static ReviewRepository _reviewRepo = new();
		private static ReviewAccountRepository _reviewAccountRepo = new();
		private static RoleRepository _roleRepo = new();
	}
}
