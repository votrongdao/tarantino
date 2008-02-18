using Tarantino.Core.Deployer.Services.Configuration.Impl;
using Tarantino.Core.Commons.Services.Configuration.Impl;
using StructureMap;

namespace Tarantino.Core.Deployer.Services.Configuration
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IApplicationRepository
	{
		ElementCollection<Application> GetAll();
	}
}