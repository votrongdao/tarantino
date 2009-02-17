using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Commons.Services.Environment;


namespace Tarantino.Core.Deployer.Services.Impl
{
	
	public class DeploymentFactory : IDeploymentFactory
	{
		private readonly ISystemClock _clock;
		private readonly IRevisionNumberParser _revisionNumberParser;
		private readonly IDeploymentResultCalculator _resultCalculator;

		public DeploymentFactory(ISystemClock clock, IRevisionNumberParser revisionNumberParser, IDeploymentResultCalculator resultCalculator)
		{
			_clock = clock;
			_revisionNumberParser = revisionNumberParser;
			_resultCalculator = resultCalculator;
		}

		public Deployment CreateDeployment(string application, string environment, string deployedBy, string output)
		{
			var deployment = new Deployment
			                 	{
			                 		Application = application,
			                 		Environment = environment,
			                 		Revision = _revisionNumberParser.Parse(output),
			                 		DeployedBy = deployedBy,
			                 		DeployedOn = _clock.GetCurrentDateTime(),
			                 		Result = _resultCalculator.GetResult(output)
			                 	};

			deployment.SetOutput(new DeploymentOutput {Output = output});

			return deployment;
		}
	}
}