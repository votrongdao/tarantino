using StructureMap;

namespace Tarantino.Commons.Core.Services.Performance
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