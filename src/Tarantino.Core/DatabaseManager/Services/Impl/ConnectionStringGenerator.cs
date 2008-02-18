using System.Text;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.DatabaseManager.Services.Impl
{
	public class ConnectionStringGenerator : IConnectionStringGenerator
	{
		public string GetConnectionString(ConnectionSettings settings)
		{
			StringBuilder connectionString = new StringBuilder();

			connectionString.AppendFormat("Data Source={0};", settings.Server);

			if (!string.IsNullOrEmpty(settings.Database))
			{
				connectionString.AppendFormat("Initial Catalog={0};", settings.Database);
			}

			if (settings.IntegratedAuthentication)
			{
				connectionString.Append("Integrated Security=True;");
			}
			else
				connectionString.AppendFormat("User ID={0};Password={1};", settings.Username, settings.Password);

			return connectionString.ToString();
		}
	}
}