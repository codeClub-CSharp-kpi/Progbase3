using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace NetManagers
{
	public static class TcpQueryManager
	{
		private static int _port = 10000;
		private static string _ipAddress = "127.0.0.1";

		private static TcpClient _client;

		private static BinaryFormatter bf = new BinaryFormatter();

		public static object ExecQuery(string procedureName, params object[] args)
		{
			_client = new TcpClient();

			IPEndPoint entryPoint = new IPEndPoint(IPAddress.Parse(_ipAddress), _port);
			_client.Connect(entryPoint);

			NetworkStream netStream = _client.GetStream();
			
			StreamWriter sw = new StreamWriter(netStream);
			string query = PrepareQuery(procedureName, args);
			sw.WriteLine(query);
			sw.Flush();

			object queryResultFromServer = null;
			if (!(procedureName.StartsWith("Add") || procedureName.StartsWith("Del") || procedureName.StartsWith("Upd")))
			{
				queryResultFromServer = bf.Deserialize(netStream);
			}

			sw.Close();
			netStream.Close();
			_client.Close();

			return queryResultFromServer;
		}

		private static string PrepareQuery(string procedureName, params object[] args)
		{

			BinaryFormatter bf = new();
			byte[] encodedParams = null;

			using (MemoryStream ms = new())
			{
				bf.Serialize(ms, args);
				encodedParams = ms.ToArray();
			}
			return $"{procedureName};{Convert.ToBase64String(encodedParams)}";
		}

	}
}
