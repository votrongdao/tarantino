using Tarantino.Deployer.Core.Services.Configuration.Impl;
using Tarantino.Commons.Core;
using StructureMap;

namespace Tarantino.Deployer.UI.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IDeploymentFormValidator
	{
		bool IsValid(Environment environment, string revisionNumberText);
	}
}