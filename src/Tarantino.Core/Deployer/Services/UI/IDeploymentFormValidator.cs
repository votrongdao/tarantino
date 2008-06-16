using Tarantino.Core;
using Tarantino.Core.Deployer.Services.Configuration.Impl;


namespace Tarantino.Deployer.Services.UI
{
	
	public interface IDeploymentFormValidator
	{
		bool IsValid(Environment environment, string revisionNumberText);
	}
}