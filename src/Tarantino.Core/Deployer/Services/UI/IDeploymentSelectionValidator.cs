using Tarantino.Core;
using Tarantino.Core.Deployer.Model;


namespace Tarantino.Deployer.Services.UI
{
	
	public interface IDeploymentSelectionValidator
	{
		bool IsValid(string revisionNumberText, Deployment selectedDeployment);
	}
}