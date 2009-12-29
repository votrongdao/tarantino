using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Services.Configuration.Impl;


namespace Tarantino.Deployer.Core.Services.UI
{
	public interface ILabelTextGenerator
	{
		string GetDeploymentText(Environment environment, string revisionNumberText, Deployment deployment);
		string GetCertificationText(string revisionNumberText, Deployment deployment);
	}
}