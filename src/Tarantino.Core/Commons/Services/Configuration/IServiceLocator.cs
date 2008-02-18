using StructureMap;

namespace Tarantino.Core.Commons.Services.Configuration
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IServiceLocator
	{
		T CreateInstance<T>(string instanceKey);
		T CreateInstance<T>();
	}
}