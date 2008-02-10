using System;
using System.ServiceProcess;
using Tarantino.Commons.Core.Services.Daemon;
using Tarantino.Commons.Core.Services.Logging;
using StructureMap;

namespace Tarantino.Commons.Service
{
	public class DefaultService : ServiceBase
	{
		public const string SERVICE_NAME = "Tarantino.Commons";
		public const string SERVICE_DESCRIPTION = "This service manages the ticket offering workflow";

		private IServiceRunner _serviceRunner;

		public DefaultService()
		{
			Log.EnsureInitialized();

			try
			{
				Log.Info(this, "Tarantino.Commons Service starting");
				ServiceName = SERVICE_NAME;
				_serviceRunner = ObjectFactory.GetInstance<IServiceRunner>();
				Log.Info(this, "Service Runner loaded");
			}
			catch (Exception exc)
			{
				Log.Fatal(this, "Service failed to start", exc);
				throw;
			}
		}

		protected override void OnStart(string[] args)
		{
			try
			{
				Log.Info(this, "Service Runner executed");
				_serviceRunner.Start();
			}
			catch (Exception exc)
			{
				Log.Fatal(this, "Service failed to start", exc);
				throw;
			}
		}

		protected override void OnStop()
		{
			try
			{
				Log.Info(this, "Tarantino.Commons Service stopping");
				_serviceRunner.Stop();
			}
			catch (Exception exc)
			{
				Log.Fatal(this, "Service failed to stop", exc);
				throw;
			}
		}
	}
}