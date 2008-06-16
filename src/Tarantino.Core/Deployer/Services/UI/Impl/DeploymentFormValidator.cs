
using Tarantino.Core;
using Tarantino.Deployer.Services.UI;
using Environment = Tarantino.Core.Deployer.Services.Configuration.Impl.Environment;

namespace Tarantino.Core.Deployer.Services.UI.Impl
{
	
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