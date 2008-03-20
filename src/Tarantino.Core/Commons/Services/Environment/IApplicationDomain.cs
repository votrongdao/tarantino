using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment
{
	[PluginFamily(Keys.Default)]
	public interface IApplicationDomain
	{
		string GetBaseFolder();
	}
}