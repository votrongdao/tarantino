using System.Reflection;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IAssemblyContext
	{
		Assembly GetExecutingAssembly();
		string GetAssemblyVersion();
	}
}