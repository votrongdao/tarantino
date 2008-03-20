using StructureMap;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(Keys.Default)]
	public interface ISecurityContext
	{
		string GetCurrentUsername();
	}
}