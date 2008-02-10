using System.Collections.Generic;
using Tarantino.Commons.Core;
using StructureMap;

namespace Tarantino.Deployer.Core.Model.Repositories
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IDeploymentRepository
	{
		IEnumerable<Deployment> Find(string application, string environment);
		IEnumerable<Deployment> FindSuccessfulUncertified(string application, string environment);
		IEnumerable<Deployment> FindCertified(string application, string environment);
	}
}