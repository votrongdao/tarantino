using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	[Pluggable(Keys.Default)]
	public class SystemEnvironment : ISystemEnvironment
	{
		public string GetMachineName()
		{
			return System.Environment.MachineName;
		}
	}
}