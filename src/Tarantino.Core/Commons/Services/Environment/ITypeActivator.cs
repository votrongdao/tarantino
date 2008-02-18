using StructureMap;

namespace Tarantino.Commons.Core.Services.Environment
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ITypeActivator
	{
		T ActivateType<T>(string typeDescriptor);
	}
}