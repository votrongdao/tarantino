using Tarantino.Core.Deployer.Model;
using StructureMap;

namespace Tarantino.Core.Deployer.Services
{
	[PluginFamily(Keys.Default)]
	public interface IRevisionCertifier
	{
		void Certify(Deployment deployment);
	}
}