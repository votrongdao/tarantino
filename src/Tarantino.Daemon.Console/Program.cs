using System;
using StructureMap;
using Tarantino.Core.Commons.Services.Logging;
using Tarantino.Core.Daemon.Services;

namespace Tarantino.Daemon.Console
{
	class Program
	{
		static void Main(string[] args)
		{

			try
			{
				IServiceRunner serviceRunner = ObjectFactory.GetInstance<IServiceRunner>();
				Logger.Info(serviceRunner, "Tarantino.Daemon Console starting");
			}
			catch (Exception exc)
			{
				System.Console.WriteLine("Console failed to run: {0}", exc);
				throw;
			}
		}
	}
}