using Tarantino.Deployer.Core.Services.Configuration.Impl;
using Tarantino.Commons.Core;
using Tarantino.Commons.Core.Services.Configuration.Impl;
using StructureMap;

namespace Tarantino.Deployer.Core.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IApplicationRepository
	{
		ElementCollection<Application> GetAll();
	}
}