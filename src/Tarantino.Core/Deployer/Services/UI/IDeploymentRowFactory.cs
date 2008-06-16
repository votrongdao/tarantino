using Tarantino.Core;
using Tarantino.Core.Deployer.Model;


namespace Tarantino.Deployer.Services.UI
{
	
	public interface IDeploymentRowFactory
	{
		string[] ConstructRow(Deployment deployment);
	}
}