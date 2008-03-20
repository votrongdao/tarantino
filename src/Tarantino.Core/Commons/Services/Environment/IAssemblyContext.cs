using System.Reflection;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment
{
	[PluginFamily(Keys.Default)]
	public interface IAssemblyContext
	{
		Assembly GetExecutingAssembly();
		string GetAssemblyVersion();
	}
}