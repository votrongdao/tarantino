using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ITypeActivator
	{
		T ActivateType<T>(string typeDescriptor);
	}
}