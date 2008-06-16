
using Tarantino.Core.Deployer.Model;

namespace Tarantino.Core.Deployer.Services
{
	
	public interface IDeploymentResultCalculator
	{
		DeploymentResult GetResult(string output);
	}
}