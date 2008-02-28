using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class SystemEnvironment : ISystemEnvironment
	{
		public string GetMachineName()
		{
			return System.Environment.MachineName;
		}
	}
}