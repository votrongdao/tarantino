using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment
{
	[PluginFamily(Keys.Default)]
	public interface ITypeActivator
	{
		T ActivateType<T>(string typeDescriptor);
	}
}