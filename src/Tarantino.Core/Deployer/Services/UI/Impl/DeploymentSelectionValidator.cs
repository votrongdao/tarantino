using Tarantino.Core;
using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Deployer.Services.UI;

namespace Tarantino.Core.Deployer.Services.UI.Impl
{
	public class DeploymentSelectionValidator : IDeploymentSelectionValidator
	{
		public bool IsValid(string revisionNumberText, Deployment selectedDeployment)
		{
			bool isValidDeployment = (revisionNumberText != string.Empty) && (selectedDeployment != null);
			return isValidDeployment;
		}
	}
}