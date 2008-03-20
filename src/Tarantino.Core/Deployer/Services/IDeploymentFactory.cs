using Tarantino.Core.Deployer.Model;
using StructureMap;

namespace Tarantino.Core.Deployer.Services
{
	[PluginFamily(Keys.Default)]
	public interface IDeploymentFactory
	{
		Deployment CreateDeployment(string application, string environment, string deployedBy, string output);
	}
}