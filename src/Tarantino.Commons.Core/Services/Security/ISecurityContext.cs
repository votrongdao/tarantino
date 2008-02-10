using StructureMap;

namespace Tarantino.Commons.Core.Services.Security
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ISecurityContext
	{
		string GetCurrentUsername();
	}
}