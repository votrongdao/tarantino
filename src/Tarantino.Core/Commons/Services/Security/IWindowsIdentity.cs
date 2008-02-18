using StructureMap;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IWindowsIdentity
	{
		string GetCurrentUsername();
	}
}