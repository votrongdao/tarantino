using Tarantino.Deployer.Core.Model.Enumerations;
using Tarantino.Commons.Core;
using StructureMap;

namespace Tarantino.Deployer.Core.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IDeploymentResultCalculator
	{
		DeploymentResult GetResult(string output);
	}
}