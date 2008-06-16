using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Configuration.Impl;


namespace Tarantino.Core.Deployer.Services.Configuration.Impl
{
	
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