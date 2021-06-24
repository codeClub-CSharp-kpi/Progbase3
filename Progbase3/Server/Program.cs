using EntitiesLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Server
{
	class Program
	{
		private static TcpListener listener;

		private IAccountRepository _accRepo = new AccountRepository();
		private IActorRepository _actRepo = new ActorRepository();
		private ICityRepository _cityRepo = new CityRepository();
		private ICountryRepository _countryRepo = new CountryRepository();

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
		private static Dictionary<string, Queries> QueryExecution = new()
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
			do
			{
				try
				{
					int port = default;
					IPAddress ipAddress = default;

					Console.Write("\n> Enter port: ");
					port = int.Parse(Console.ReadLine());

					Console.Write("\n> Enter ip-address: ");
					ipAddress = IPAddress.Parse(Console.ReadLine());

					listener = new TcpListener(ipAddress, port);
					listener.Start(100);

					while (true)
					{
						Console.Write("\n\t> Waiting for client connection...");

						TcpClient client = listener.AcceptTcpClient();
						Console.Write("\n\t> Got client...");
						NetworkStream netStream = client.GetStream();

						StreamReader sr = new(netStream);
						string userQuery = sr.ReadLine();

						BinaryFormatter br = new();

						if (!QueryExecution.ContainsKey(userQuery))
						{
							throw new Exception("The query is invalid");
						}

						switch (QueryExecution[userQuery])
						{
							case Queries.AddAccount:
								break;
							case Queries.AddActor:
								break;
							case Queries.AddCity:
								break;
							case Queries.AddCountry:
								break;
							case Queries.AddFilm:
								break;
							case Queries.AddFilmActor:
								break;
							case Queries.AddPhoto:
								break;
							case Queries.AddReview:
								break;
							case Queries.AddReviewAccount:
								break;
							case Queries.DelActor:
								break;
							case Queries.DeleteCity:
								break;
							case Queries.DeleteCountry:
								break;
							case Queries.DeletePhoto:
								break;
							case Queries.DeleteReview:
								break;
							case Queries.DelFilm:
								break;
							case Queries.DelFilmActor:
								break;
							case Queries.DelReview:
								break;
							case Queries.DelReviewAccount:
								break;
							case Queries.GetActor:
								break;
							case Queries.GetActorsByFilm:
								break;
							case Queries.GetActorsPage:
								break;
							case Queries.GetAllAccounts:
								break;
							case Queries.GetAllActors:
								break;
							case Queries.GetAllCities:
								break;
							case Queries.GetAllCountries:
								break;
							case Queries.GetAllFilms:
								break;
							case Queries.GetAllFilmsActors:
								break;
							case Queries.GetAllPhotos:
								break;
							case Queries.GetAllReviews:
								break;
							case Queries.GetAllReviewsAccounts:
								break;
							case Queries.GetAllRoles:
								break;
							case Queries.GetCityById:
								break;
							case Queries.GetCountryById:
								break;
							case Queries.GetFilm:
								break;
							case Queries.GetFilmById:
								break;
							case Queries.GetFilmsByActor:
								break;
							case Queries.GetFilmsPage:
								break;
							case Queries.GetPhotoById:
								break;
							case Queries.GetReview:
								break;
							case Queries.GetReviewsPage:
								break;
							case Queries.UpdateActor:
								break;
							case Queries.UpdateCity:
								break;
							case Queries.UpdateCountry:
								break;
							case Queries.UpdateFilm:
								break;
							case Queries.UpdateFilmActor:
								break;
							case Queries.UpdatePhoto:
								break;
							case Queries.UpdateReview:
								break;
							default:
								break;
						}

						sr.Close();
						netStream.Close();
						client.Close();

					}
				}
				catch (Exception err)
				{
					Console.WriteLine($"Server error: {err.Message}");
				}
				finally
				{
					listener?.Stop();
				}
			} while (true);
		}


		// methods
		private void AddAccount()
		{
			
		}
	}
}
