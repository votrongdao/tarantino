using Tarantino.Core.Deployer.Model;
using StructureMap;

namespace Tarantino.Core.Deployer.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IRevisionCertifier
	{
		void Certify(Deployment deployment);
	}
}