using Tarantino.Deployer.Core.Model;
using Tarantino.Commons.Core;
using StructureMap;

namespace Tarantino.Deployer.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IDeploymentRowFactory
	{
		string[] ConstructRow(Deployment deployment);
	}
}