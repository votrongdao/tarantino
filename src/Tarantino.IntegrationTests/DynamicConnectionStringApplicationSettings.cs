using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Configuration.Impl;

namespace Tarantino.IntegrationTests
{
	public class DynamicConnectionStringApplicationSettings : ApplicationSettings
	{
		private readonly string _connectionStringKey;

		public DynamicConnectionStringApplicationSettings(IConfigurationReader configurationReader, string connectionStringKey) : base(configurationReader)
		{
			_connectionStringKey = connectionStringKey;
		}

		public override string GetConnectionString()
		{
			return _configurationReader.GetConnectionString(_connectionStringKey);
		}
	}
}