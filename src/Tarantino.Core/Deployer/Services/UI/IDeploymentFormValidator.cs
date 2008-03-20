using Tarantino.Core;
using Tarantino.Core.Deployer.Services.Configuration.Impl;
using StructureMap;

namespace Tarantino.Deployer.Services.UI
{
	[PluginFamily(Keys.Default)]
	public interface IDeploymentFormValidator
	{
		bool IsValid(Environment environment, string revisionNumberText);
	}
}