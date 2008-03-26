using StructureMap;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ISecurityContext
	{
		string GetCurrentUsername();
	}
}