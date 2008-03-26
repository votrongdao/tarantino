using System;
using System.ServiceProcess;
using Tarantino.Core.Daemon.Services;
using StructureMap;
using Tarantino.Core.Commons.Services.Logging;

namespace Tarantino.Daemon
{
	public class DefaultService : ServiceBase
	{
		public const string SERVICE_NAME = "Tarantino.Daemon";
		public const string SERVICE_DESCRIPTION = "This service manages the execution of daemon service agents";

		private IServiceRunner _serviceRunner;

		public DefaultService()
		{
			ILogger logger = ObjectFactory.GetInstance<ILogger>();

			try
			{
				logger.Info(this, "Tarantino.Daemon Service starting");
				ServiceName = SERVICE_NAME;
				_serviceRunner = ObjectFactory.GetInstance<IServiceRunner>();
				logger.Info(this, "Service Runner loaded");
			}
			catch (Exception exc)
			{
				logger.Fatal(this, "Service failed to start", exc);
				throw;
			}
		}

		protected override void OnStart(string[] args)
		{
			ILogger logger = ObjectFactory.GetInstance<ILogger>();

			try
			{
				logger.Info(this, "Service Runner executed");
				_serviceRunner.Start();
			}
			catch (Exception exc)
			{
				logger.Fatal(this, "Service failed to start", exc);
				throw;
			}
		}

		protected override void OnStop()
		{
			ILogger logger = ObjectFactory.GetInstance<ILogger>();

			try
			{
				logger.Info(this, "Tarantino.Daemon Service stopping");
				_serviceRunner.Stop();
			}
			catch (Exception exc)
			{
				logger.Fatal(this, "Service failed to stop", exc);
				throw;
			}
		}
	}
}