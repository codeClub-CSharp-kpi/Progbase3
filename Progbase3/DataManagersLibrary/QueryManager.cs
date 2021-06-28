using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataManagersLibrary
{
	public static class QueryManager
	{
		private static string _connectionString;

		private static DbProviderFactory _factory;

		static QueryManager()
		{
			// Getting connection string literal in a 'concating commandline arguments' way
			string[] cmdArgs = Environment.GetCommandLineArgs();
			StringBuilder sb = new();
			for (int i = 1; i < cmdArgs.Length; i++)
			{
				sb.Append($"{cmdArgs[i]} ");
			}

			// intialize the connection string variable with the literal from StringBuidler
			_connectionString = sb.ToString();

			string providerName = "System.Data.SqlClient";
			DbProviderFactories.RegisterFactory(providerName, SqlClientFactory.Instance);

			IEnumerable<string> invariants = DbProviderFactories.GetProviderInvariantNames();

			// using abstract factory to provide work with any data-provider
			_factory = DbProviderFactories.GetFactory(invariants.Where(i => i == providerName).FirstOrDefault());
		}

		public static void ExecDML(string procedureName, DynamicParameters param = null)
		{
			using (IDbConnection conn = _factory.CreateConnection())
			{
				conn.ConnectionString = _connectionString;
				conn.Open();
				conn.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
			}
		}
		public static IEnumerable<T> ExecSelect<T>(string procedureName, DynamicParameters param = null)
		{
			using (IDbConnection conn = _factory.CreateConnection())
			{
				conn.ConnectionString = _connectionString;
				conn.Open();
				return conn.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
			}
		}
	}
}
