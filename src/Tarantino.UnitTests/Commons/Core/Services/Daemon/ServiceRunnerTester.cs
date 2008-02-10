using System;
using System.Threading;
using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Commons.Core.Services.Configuration;
using Tarantino.Commons.Core.Services.Daemon;
using Tarantino.Commons.Core.Services.Daemon.Impl;

namespace Tarantino.UnitTests.Commons.Core.Services.Daemon
{
	[TestFixture]
	public class ServiceRunnerTester
	{
		[Test]
		public void Run_service()
		{
			MockRepository mocks = new MockRepository();
			IApplicationSettings settings = mocks.CreateMock<IApplicationSettings>();
			IServiceAgentAggregator aggregator = mocks.CreateMock<IServiceAgentAggregator>();

			using (mocks.Record())
			{
				aggregator.ExecuteServiceAgentCycle();
				LastCall.Repeat.Times(2, int.MaxValue);
				Expect.Call(settings.GetServiceSleepTime()).Return(10);
				LastCall.Repeat.Times(2, int.MaxValue);
			}

			using (mocks.Playback())
			{
				IServiceRunner runner = new ServiceRunner(aggregator, settings);

				runner.Start();
				Thread.Sleep(500);
				runner.Stop();
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Run_one_cycle_execute_agent_aggregator()
		{
			MockRepository mocks = new MockRepository();

			IApplicationSettings settings = mocks.CreateMock<IApplicationSettings>();
			IServiceAgentAggregator aggregator = mocks.CreateMock<IServiceAgentAggregator>();

			using (mocks.Record())
			{
				aggregator.ExecuteServiceAgentCycle();
			}

			using (mocks.Playback())
			{
				IServiceRunner runner = new ServiceRunner(aggregator, settings);
				runner.RunOneCycle();
			}

			mocks.VerifyAll();
		}

		[Test]
		public void When_the_cycle_completes_we_should_get_an_event()
		{
			_startFired = DateTime.MinValue;
			_completedFired = DateTime.MinValue;

			MockRepository mocks = new MockRepository();
			IServiceAgentAggregator aggregator = mocks.CreateMock<IServiceAgentAggregator>();
			aggregator.ExecuteServiceAgentCycle();
			LastCall.On(aggregator).Do(new Action(delegate { Thread.Sleep(50); }));

			mocks.ReplayAll();

			ServiceRunner runner = new ServiceRunner(aggregator, null);
			runner.CycleStarted += new EventHandler(runner_CycleStarted);
			runner.CycleCompleted += new EventHandler(runner_CycleCompleted);
			runner.RunOneCycle();

			mocks.VerifyAll();

			Assert.IsTrue(_completedFired > _startFired);
		}

		private DateTime _startFired;
		private DateTime _completedFired;

		private void runner_CycleStarted(object sender, EventArgs e)
		{
			_startFired = DateTime.Now;
		}

		private void runner_CycleCompleted(object sender, EventArgs e)
		{
			_completedFired = DateTime.Now;
		}
	}

	public delegate void Action();
}