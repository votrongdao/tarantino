using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using StructureMap;
using Tarantino.Core;
using Tarantino.Core.DatabaseManager.Services;
using ConnectionSettings=Tarantino.Core.DatabaseManager.Model.ConnectionSettings;

namespace Tarantino.Infrastructure.DatabaseManager.DataAccess
{
	
	public class QueryExecutor : IQueryExecutor
	{
		private IConnectionStringGenerator _connectionStringGenerator;

		public QueryExecutor(IConnectionStringGenerator connectionStringGenerator)
		{
			_connectionStringGenerator = connectionStringGenerator;
		}

		public void ExecuteNonQuery(ConnectionSettings settings, string sql, bool runAgainstSpecificDatabase)
		{
			string connectionString = _connectionStringGenerator.GetConnectionString(settings, runAgainstSpecificDatabase);

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				Server server = new Server(new ServerConnection(connection));
				server.ConnectionContext.ExecuteNonQuery(sql);
			}
		}

		public int ExecuteScalarInteger(ConnectionSettings settings, string sql)
		{
			string connectionString = _connectionStringGenerator.GetConnectionString(settings, true);

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				Server server = new Server(new ServerConnection(connection));
				return Convert.ToInt32(server.ConnectionContext.ExecuteScalar(sql));
			}
		}

		public string[] ReadFirstColumnAsStringArray(ConnectionSettings settings, string sql)
		{
			List<string> list = new List<string>();
			string connectionString = _connectionStringGenerator.GetConnectionString(settings, true);

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				Server server = new Server(new ServerConnection(connection));
				using (SqlDataReader reader = server.ConnectionContext.ExecuteReader(sql))
				{
					while (reader.Read())
					{
						string item = reader[0].ToString();
						list.Add(item);
					}
				}
			}
			return list.ToArray();
		}
	}
}