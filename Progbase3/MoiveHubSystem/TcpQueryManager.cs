using Dapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MoiveHubSystem
{
	public static class TcpQueryManager
	{
		private static int _port = 10000;
		private static string _ipAddress = "127.0.0.1";

		private static TcpClient _client;

		public static object ExecQuery(string procedureName, params object[] args)
		{
			object queryResult = null;
			using (_client = new TcpClient(_ipAddress, _port))
			{
				using (NetworkStream netStream = _client.GetStream())
				{
					using (StreamWriter sw = new StreamWriter(netStream))
					{
						string query = PrepareQuery(procedureName, args);
						sw.WriteLine(query);
						sw.Flush();

						object queryResultFromServer = new BinaryFormatter().Deserialize(netStream);
					}
				}
			}
			return queryResult;
		}

		//public static object ExecSelect(string procedureName, params object[] args)
		//{
		//	object queryResultCollection = default;
		//	try
		//	{
		//		using (_client = new TcpClient(_ipAddress, _port))
		//		{
		//			using (NetworkStream netStream = _client.GetStream())
		//			{
		//				using (StreamWriter sw = new StreamWriter(netStream))
		//				{
		//					BinaryFormatter bf = new();
		//					byte[] encodedParams = null;

		//					using (MemoryStream ms = new())
		//					{
		//						bf.Serialize(ms, args);
		//						encodedParams = ms.ToArray();
		//					}

		//					string query = $"SELECT;{procedureName};{Convert.ToBase64String(encodedParams)}";
		//					sw.WriteLine(query);
		//					sw.Flush();

		//					var querRes = bf.Deserialize(netStream);
		//				}
		//			}
		//		}
		//	}
		//	catch (Exception err)
		//	{
		//		throw new Exception($"Select Error: {err.Message}");
		//	}
		//	return queryResultCollection;
		//}

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
