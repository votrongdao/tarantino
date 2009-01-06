using Tarantino.Core.Deployer.Model;

namespace Tarantino.Core.Deployer.Services.UI
{
	public interface IDeploymentRowFactory
	{
		string[] ConstructRow(Deployment deployment);
	}
}