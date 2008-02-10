using Tarantino.Deployer.Core.Model;
using Tarantino.Commons.Core;
using StructureMap;

namespace Tarantino.Deployer.UI.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class DeploymentSelectionValidator : IDeploymentSelectionValidator
	{
		public bool IsValid(string revisionNumberText, Deployment selectedDeployment)
		{
			bool isValidDeployment = (revisionNumberText != string.Empty) && (selectedDeployment != null);
			return isValidDeployment;
		}
	}
}