using Tarantino.Core;
using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Deployer.Services.Configuration.Impl;
using StructureMap;

namespace Tarantino.Deployer.Services.UI
{
	[PluginFamily(Keys.Default)]
	public interface ILabelTextGenerator
	{
		string GetDeploymentText(Environment environment, string revisionNumberText, Deployment deployment);
		string GetCertificationText(string revisionNumberText, Deployment deployment);
	}
}