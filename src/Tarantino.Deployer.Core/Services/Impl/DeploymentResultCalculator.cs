using Tarantino.Deployer.Core.Model;

namespace Tarantino.Deployer.Core.Services.Impl
{
	
	public class DeploymentResultCalculator : IDeploymentResultCalculator
	{
		public DeploymentResult GetResult(string output)
		{
			bool buildFailed = output.Contains("BUILD FAILED");
			DeploymentResult result = buildFailed ? DeploymentResult.Failure : DeploymentResult.Success;
			return result;
		}
	}
}