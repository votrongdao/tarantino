using Tarantino.Commons.Core;
using StructureMap;
using Environment = Tarantino.Deployer.Core.Services.Configuration.Impl.Environment;

namespace Tarantino.Deployer.UI.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class DeploymentFormValidator : IDeploymentFormValidator
	{
		public bool IsValid(Environment environment, string revisionNumberText)
		{
			string predecessor = environment.Predecessor;
			bool environmentHasPredecessor = !string.IsNullOrEmpty(predecessor);
			bool revisionDefined = !string.IsNullOrEmpty(revisionNumberText);

			bool isValid = !environmentHasPredecessor || revisionDefined;
			return isValid;
		}
	}
}