using System;
using System.Threading;
using Tarantino.Core.Commons.Services.Configuration;
using StructureMap;
using Tarantino.Core.Commons.Services.Logging;

namespace Tarantino.Core.Daemon.Services.Impl
{
	[Pluggable(Keys.Default)]
	public class ServiceRunner : IServiceRunner
	{
		private bool _continue = false;
		private Thread _workerThread;
		private IServiceAgentAggregator _aggregator;
		private readonly IApplicationSettings _applicationSettings;
		public event EventHandler CycleStarted;
		public event EventHandler CycleCompleted;

		public ServiceRunner(IServiceAgentAggregator aggregator, IApplicationSettings applicationSettings)
		{
			_aggregator = aggregator;
			_applicationSettings = applicationSettings;
		}

		public void Start()
		{
			_workerThread = new Thread(workerThreadStart);
			_continue = true;
			_workerThread.Start();
		}

		public void Stop()
		{
			_continue = false;
            Logger.Debug(this, "Service Runner stopping");

			if (_workerThread != null)
			{
				_workerThread.Join();
				Logger.Debug(this, "Service Runner thread stopped");
			}

            Logger.Debug(this, "Service Runner stopped");
		}

		private void workerThreadStart()
		{
			try
			{
                Logger.Debug(this, "Service Runner thread initializing");

				while (_continue)
				{
                    Logger.Debug(this, "Service Runner thread initialized");

					RunOneCycle();
					int sleepTime = _applicationSettings.GetServiceSleepTime();
					Thread.Sleep(sleepTime);
				}
			}
			catch (Exception ex)
			{
                Logger.Fatal(this, "Running service cycle failed", ex);
			}
		}

		private void OnCycleStarted()
		{
            Logger.Debug(this, "Starting Cycle");

			if (CycleStarted != null)
			{
				CycleStarted(this, EventArgs.Empty);
			}
		}

		private void OnCycleCompleted()
		{
            Logger.Debug(this, "Finished Cycle");

			if (CycleCompleted != null)
			{
				CycleCompleted(this, EventArgs.Empty);
			}
		}

		public void RunOneCycle()
		{
			OnCycleStarted();

			_aggregator.ExecuteServiceAgentCycle();

			OnCycleCompleted();
		}
	}
}