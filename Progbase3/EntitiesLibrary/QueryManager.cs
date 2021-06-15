using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace EntitiesLibrary
{
	public static class QueryManager
	{
		private static string _connectionString;

		private static DbProviderFactory _factory;

		static QueryManager()
		{
			_connectionString = @"Data Source=SQL5080.site4now.net;
			Initial Catalog=db_a76153_filmmanagmentdb;
			User ID=db_a76153_filmmanagmentdb_admin;
			Password=filmDb753"; // insecure(?) how to create app.config in .net core&


			DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);

			IEnumerable<string> invariants = DbProviderFactories.GetProviderInvariantNames();


			// using abstract factory to provide work with any data-provider
			_factory = DbProviderFactories.GetFactory(invariants.Where(i => i == "System.Data.SqlClient").FirstOrDefault());
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
