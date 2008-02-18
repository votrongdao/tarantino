using Tarantino.Deployer.Core.Model.Enumerations;
using Tarantino.Commons.Core;
using StructureMap;

namespace Tarantino.Deployer.Core.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
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