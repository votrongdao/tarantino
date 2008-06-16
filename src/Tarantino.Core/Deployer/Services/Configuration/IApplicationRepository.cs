using Tarantino.Core.Deployer.Services.Configuration.Impl;
using Tarantino.Core.Commons.Services.Configuration.Impl;


namespace Tarantino.Core.Deployer.Services.Configuration
{
	
	public interface IApplicationRepository
	{
		ElementCollection<Application> GetAll();
	}
}