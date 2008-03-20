using System.Reflection;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	[Pluggable(Keys.Default)]
	public class AssemblyContext : IAssemblyContext
	{
		public Assembly GetExecutingAssembly()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			return assembly;
		}

		public string GetAssemblyVersion()
		{
			string version = GetExecutingAssembly().GetName().Version.ToString();
			return version;
		}
	}
}