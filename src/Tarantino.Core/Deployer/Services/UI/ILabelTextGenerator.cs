using Tarantino.Core;
using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Deployer.Services.Configuration.Impl;


namespace Tarantino.Core.Deployer.Services.UI
{
	public interface ILabelTextGenerator
	{
		string GetDeploymentText(Environment environment, string revisionNumberText, Deployment deployment);
		string GetCertificationText(string revisionNumberText, Deployment deployment);
	}
}