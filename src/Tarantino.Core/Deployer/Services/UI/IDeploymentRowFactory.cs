using Tarantino.Core;
using Tarantino.Core.Deployer.Model;
using StructureMap;

namespace Tarantino.Deployer.Services.UI
{
	[PluginFamily(Keys.Default)]
	public interface IDeploymentRowFactory
	{
		string[] ConstructRow(Deployment deployment);
	}
}