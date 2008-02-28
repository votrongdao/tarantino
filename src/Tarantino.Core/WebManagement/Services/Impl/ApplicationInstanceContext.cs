using StructureMap;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class ApplicationInstanceContext : IApplicationInstanceContext
	{
		private readonly IApplicationInstanceCache _cache;
		private readonly ICurrentApplicationInstanceRetriever _retriever;
		private readonly IApplicationInstanceFactory _factory;

		public ApplicationInstanceContext(IApplicationInstanceCache cache, ICurrentApplicationInstanceRetriever retriever, IApplicationInstanceFactory factory)
		{
			_cache = cache;
			_retriever = retriever;
			_factory = factory;
		}

		public ApplicationInstance GetCurrent()
		{
			ApplicationInstance instance = _cache.GetCurrent();
			
			try
			{
				if (instance == null)
				{
					instance = _retriever.GetApplicationInstance();
				}
			}
			catch
			{
				instance = _factory.Create();
			}
			
			return instance;
		}
	}
}