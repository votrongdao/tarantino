using System.Collections.Generic;
using StructureMap;
using Tarantino.Core.Deployer.Model;

namespace Tarantino.Core.Deployer.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IDeploymentRepository
	{
		IEnumerable<Deployment> Find(string application, string environment);
		IEnumerable<Deployment> FindSuccessfulUncertified(string application, string environment);
		IEnumerable<Deployment> FindCertified(string application, string environment);

		string ConnectionStringKey
		{
			get;
			set;
		}
	}
}