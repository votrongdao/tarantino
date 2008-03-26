using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Commons.Services.Environment;
using StructureMap;

namespace Tarantino.Core.Deployer.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
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
			Deployment deployment = new Deployment();
			deployment.Application = application;
			deployment.Environment = environment;
			deployment.Revision = _revisionNumberParser.Parse(output);
			deployment.DeployedBy = deployedBy;
			deployment.DeployedOn = _clock.GetCurrentDateTime();
			deployment.Output = output;
			deployment.Result = _resultCalculator.GetResult(output);

			return deployment;
		}
	}
}