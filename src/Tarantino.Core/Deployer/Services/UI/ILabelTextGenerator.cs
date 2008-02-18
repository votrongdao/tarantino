using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Services.Configuration.Impl;
using Tarantino.Commons.Core;
using StructureMap;

namespace Tarantino.Deployer.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ILabelTextGenerator
	{
		string GetDeploymentText(Environment environment, string revisionNumberText, Deployment deployment);
		string GetCertificationText(string revisionNumberText, Deployment deployment);
	}
}