using Tarantino.Deployer.Core.Model;
using Tarantino.Commons.Core;
using StructureMap;

namespace Tarantino.Deployer.Core.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IRevisionCertifier
	{
		void Certify(Deployment deployment);
	}
}