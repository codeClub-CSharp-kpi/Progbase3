using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Server
{
	class Program
	{
		private static TcpListener listener;

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

						switch (userQuery)
						{
							case "GetAllFaculties":
								{
									var allFacs = new FacultyRepository().GetFaculties().ToList();
									br.Serialize(netStream, allFacs);
									Console.WriteLine("\n>Got Faculties");
								}
								break;
							case "GetAllGroups":
								{
									var allGroups = new GroupRepository().GetGroups().ToList();
									br.Serialize(netStream, allGroups);
									Console.WriteLine("\n>Got Groups");
								}
								break;
							case "GetAllStudents":
								{
									var allStudents = new StudentRepository().GetStudents().ToList();
									br.Serialize(netStream, allStudents);
									Console.WriteLine("\n>Got Students");
								}
								break;
							default:
								{
									Console.WriteLine("\n>Uknown Query");
								}
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
	}
}
