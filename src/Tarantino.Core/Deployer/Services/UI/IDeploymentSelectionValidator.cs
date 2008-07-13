using Tarantino.Core;
using Tarantino.Core.Deployer.Model;


namespace Tarantino.Core.Deployer.Services.UI
{
	public interface IDeploymentSelectionValidator
	{
		bool IsValid(string revisionNumberText, Deployment selectedDeployment);
	}
}