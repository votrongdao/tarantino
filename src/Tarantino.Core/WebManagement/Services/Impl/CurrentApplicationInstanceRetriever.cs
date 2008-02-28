using StructureMap;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class CurrentApplicationInstanceRetriever : ICurrentApplicationInstanceRetriever
	{
		private readonly ISystemEnvironment _environment;
		private readonly IApplicationDomain _applicationDomain;
		private readonly IApplicationInstanceRepository _repository;
		private readonly IApplicationInstanceFactory _factory;

		public CurrentApplicationInstanceRetriever(ISystemEnvironment environment, IApplicationDomain applicationDomain, IApplicationInstanceRepository repository, IApplicationInstanceFactory factory)
		{
			_environment = environment;
			_applicationDomain = applicationDomain;
			_repository = repository;
			_factory = factory;
		}

		public ApplicationInstance GetApplicationInstance()
		{
			string machineName = _environment.GetMachineName();
			string applicationDomainName = _applicationDomain.GetName();
			ApplicationInstance instance = _repository.GetByDomainAndMachineName(applicationDomainName, machineName);
			
			if (instance == null)
			{
				instance = _factory.Create();
				_repository.Save(instance);
			}
			
			return instance;
		}
	}
}