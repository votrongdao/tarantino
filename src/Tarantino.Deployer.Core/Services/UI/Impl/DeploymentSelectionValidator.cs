using Tarantino.Deployer.Core.Services.UI;
using Tarantino.Deployer.Core.Model;

namespace Tarantino.Deployer.Core.Services.UI.Impl
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