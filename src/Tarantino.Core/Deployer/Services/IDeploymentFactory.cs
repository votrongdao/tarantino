using Tarantino.Core.Deployer.Model;


namespace Tarantino.Core.Deployer.Services
{
	
	public interface IDeploymentFactory
	{
		Deployment CreateDeployment(string application, string environment, string deployedBy, string output);
	}
}