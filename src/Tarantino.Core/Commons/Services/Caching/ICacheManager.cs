using StructureMap;

namespace Tarantino.Core.Commons.Services.Caching
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ICacheManager
	{
		void Set<T>(object key, T objectToCache);
		T Get<T>(object key);
		void Clear();
		bool Has(object key);
	}
}