using Tarantino.Commons.Core;
using Tarantino.Commons.Core.Services.Configuration;
using Tarantino.Commons.Core.Services.Configuration.Impl;
using StructureMap;

namespace Tarantino.Deployer.Core.Services.Configuration.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class ApplicationRepository : IApplicationRepository
	{
		private readonly IApplicationConfiguration _configuration;

		public ApplicationRepository(IApplicationConfiguration configuration)
		{
			_configuration = configuration;
		}

		public ElementCollection<Application> GetAll()
		{
			object sectionObject = _configuration.GetSection("DeployerSettings");

			DeployerSettingsConfigurationHandler handler = (DeployerSettingsConfigurationHandler) sectionObject;

			return handler.Applications;
		}
	}
}