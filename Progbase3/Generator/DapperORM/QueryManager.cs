using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

namespace Generator.DapperORM
{
	public static class QueryManager
	{
		private static string _connectionString;

		private static string _providerName;
		private static DbProviderFactory _factory;

		static QueryManager()
		{
			_connectionString =@"Data Source=(LocalDB)\MSSQLLocalDB;
								AttachDbFilename=|DataDirectory|\Data\FilmManagmentDB.mdf;
								Integrated Security = True";
			_providerName = "System.Data.SqlClient";

			_factory = DbProviderFactories.GetFactory(_providerName);
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
