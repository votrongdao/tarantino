using StructureMap;
using Tarantino.Core.Deployer.Model;

namespace Tarantino.Core.Deployer.Services.Impl
{
	[Pluggable(Keys.Default)]
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