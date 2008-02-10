using StructureMap;

namespace Tarantino.Commons.Core.Services.Configuration
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IServiceLocator
	{
		T CreateInstance<T>(string instanceKey);
		T CreateInstance<T>();
	}
}