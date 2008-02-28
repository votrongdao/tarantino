using StructureMap;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class ApplicationInstanceFactory : IApplicationInstanceFactory
	{
		private readonly ISystemEnvironment _systemEnvironment;
		private readonly IApplicationDomain _domain;
		private readonly IAssemblyContext _assemblyContext;

		public ApplicationInstanceFactory(ISystemEnvironment systemEnvironment, IApplicationDomain domain, IAssemblyContext assemblyContext)
		{
			_systemEnvironment = systemEnvironment;
			_domain = domain;
			_assemblyContext = assemblyContext;
		}

		public ApplicationInstance Create()
		{
			ApplicationInstance instance = new ApplicationInstance();
			instance.MachineName = _systemEnvironment.GetMachineName();
			instance.ApplicationDomain = _domain.GetName();
			instance.AvailableForLoadBalancing = true;
			instance.Version = _assemblyContext.GetAssemblyVersion();

			return instance;
		}
	}
}