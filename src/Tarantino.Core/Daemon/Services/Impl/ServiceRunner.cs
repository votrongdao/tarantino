using System;
using System.Threading;
using Tarantino.Core.Commons.Services.Configuration;
using StructureMap;
using Tarantino.Core.Commons.Services.Logging;

namespace Tarantino.Core.Daemon.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class ServiceRunner : IServiceRunner
	{
		private bool _continue = false;
		private Thread _workerThread;
		private IServiceAgentAggregator _aggregator;
		private readonly IApplicationSettings _applicationSettings;
		private readonly ILogger _logger;
		public event EventHandler CycleStarted;
		public event EventHandler CycleCompleted;

		public ServiceRunner(IServiceAgentAggregator aggregator, IApplicationSettings applicationSettings, ILogger logger)
		{
			_aggregator = aggregator;
			_applicationSettings = applicationSettings;
			_logger = logger;
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
			_logger.Debug(this, "Service Runner stopping");

			if (_workerThread != null)
			{
				_workerThread.Join();
				_logger.Debug(this, "Service Runner thread stopped");
			}

			_logger.Debug(this, "Service Runner stopped");
		}

		private void workerThreadStart()
		{
			try
			{
				_logger.Debug(this, "Service Runner thread initializing");

				while (_continue)
				{
					_logger.Debug(this, "Service Runner thread initialized");

					RunOneCycle();
					int sleepTime = _applicationSettings.GetServiceSleepTime();
					Thread.Sleep(sleepTime);
				}
			}
			catch (Exception ex)
			{
				_logger.Fatal(this, "Running service cycle failed", ex);
			}
		}

		private void OnCycleStarted()
		{
			_logger.Debug(this, "Starting Cycle");

			if (CycleStarted != null)
			{
				CycleStarted(this, EventArgs.Empty);
			}
		}

		private void OnCycleCompleted()
		{
			_logger.Debug(this, "Finished Cycle");

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