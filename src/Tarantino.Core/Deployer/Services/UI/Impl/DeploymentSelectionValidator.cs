using Tarantino.Core;
using Tarantino.Core.Deployer.Model;
using StructureMap;
using Tarantino.Deployer.Services.UI;

namespace Tarantino.Deployer.Services.UI.Impl
{
	[Pluggable(Keys.Default)]
	public class DeploymentSelectionValidator : IDeploymentSelectionValidator
	{
		public bool IsValid(string revisionNumberText, Deployment selectedDeployment)
		{
			bool isValidDeployment = (revisionNumberText != string.Empty) && (selectedDeployment != null);
			return isValidDeployment;
		}
	}
}