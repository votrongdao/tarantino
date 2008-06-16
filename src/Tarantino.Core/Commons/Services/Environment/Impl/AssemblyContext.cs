using System.Reflection;


namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	
	public class AssemblyContext : IAssemblyContext
	{
		public Assembly GetExecutingAssembly()
		{
			var assembly = Assembly.GetExecutingAssembly();
			return assembly;
		}

		public string GetAssemblyVersion()
		{
			string version = GetExecutingAssembly().GetName().Version.ToString();
			return version;
		}
	}
}