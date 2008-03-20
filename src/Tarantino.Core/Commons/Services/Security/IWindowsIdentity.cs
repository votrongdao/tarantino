using StructureMap;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(Keys.Default)]
	public interface IWindowsIdentity
	{
		string GetCurrentUsername();
	}
}