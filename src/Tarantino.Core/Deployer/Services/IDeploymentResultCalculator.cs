using StructureMap;
using Tarantino.Core.Deployer.Model;

namespace Tarantino.Core.Deployer.Services
{
	[PluginFamily(Keys.Default)]
	public interface IDeploymentResultCalculator
	{
		DeploymentResult GetResult(string output);
	}
}