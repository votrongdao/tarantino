using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Deployer.Core.Model;


namespace Tarantino.Deployer.Core.Services.Impl
{
	
	public class DeploymentFactory : IDeploymentFactory
	{
		private readonly ISystemClock _clock;

		public DeploymentFactory(ISystemClock clock)
		{
			_clock = clock;
		}

		public Deployment CreateDeployment(string application, string environment, string deployedBy, string output, string version, bool failed)
		{
			var deployment = new Deployment
			                 	{
			                 		Application = application,
			                 		Environment = environment,
			                 		Version = version,
			                 		DeployedBy = deployedBy,
			                 		DeployedOn = _clock.GetCurrentDateTime(),
			                 		Result = failed ? DeploymentResult.Failure : DeploymentResult.Success
			                 	};

			deployment.SetOutput(new DeploymentOutput {Output = output});

			return deployment;
		}
	}
}