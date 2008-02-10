using Tarantino.Deployer.Core.Model;
using Tarantino.Commons.Core;
using StructureMap;

namespace Tarantino.Deployer.UI.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IDeploymentRowFactory
	{
		string[] ConstructRow(Deployment deployment);
	}
}