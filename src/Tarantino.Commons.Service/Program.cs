using System.ServiceProcess;

namespace Tarantino.Commons.Service
{
	internal static class Program
	{
		private static void Main()
		{
			ServiceBase.Run(new ServiceBase[] {new DefaultService()});
		}
	}
}