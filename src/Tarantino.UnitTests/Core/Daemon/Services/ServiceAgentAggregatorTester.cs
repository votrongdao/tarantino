using System;
using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Daemon.Services;
using Tarantino.Core.Daemon.Services.Impl;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Logging;

namespace Tarantino.UnitTests.Core.Daemon.Services
{
	[TestFixture]
	public class ServiceAgentAggregatorTester
	{
		[Test]
		public void Correctly_executes_service_agents()
		{
			MockRepository mocks = new MockRepository();

			IApplicationSettings settings = mocks.CreateMock<IApplicationSettings>();
			ITypeActivator activator = mocks.CreateMock<ITypeActivator>();
			IServiceAgentFactory factory = mocks.CreateMock<IServiceAgentFactory>();
			IServiceAgent serviceAgent1 = mocks.CreateMock<IServiceAgent>();
			IServiceAgent serviceAgent2 = mocks.CreateMock<IServiceAgent>();
			ILogger logger = mocks.CreateMock<ILogger>();
			IServiceAgent[] serviceAgents = new IServiceAgent[] { serviceAgent1, serviceAgent2 };

			IServiceAgentAggregator aggregator = new ServiceAgentAggregator(settings, activator, logger);

			using (mocks.Record())
			{
				Expect.Call(settings.GetServiceAgentFactory()).Return("serviceAgentType");
				Expect.Call(activator.ActivateType<IServiceAgentFactory>("serviceAgentType")).Return(factory);
				Expect.Call(factory.GetServiceAgents()).Return(serviceAgents);

				Expect.Call(serviceAgent1.AgentName).Return("FirstAgent").Repeat.Any();
				logger.Debug(aggregator, "Executing agent: FirstAgent");
				serviceAgent1.Run();
				logger.Debug(aggregator, "Agent execution completed: FirstAgent");

				Expect.Call(serviceAgent2.AgentName).Return("SecondAgent").Repeat.Any();
				logger.Debug(aggregator, "Executing agent: SecondAgent");
				serviceAgent2.Run();
				logger.Debug(aggregator, "Agent execution completed: SecondAgent");
			}

			using (mocks.Playback())
			{
				aggregator.ExecuteServiceAgentCycle();
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Should_continue_with_next_agents_event_if_one_agent_fails()
		{
			ApplicationException exception = new ApplicationException("Test Exception");

			MockRepository mocks = new MockRepository();

			IApplicationSettings settings = mocks.CreateMock<IApplicationSettings>();
			ITypeActivator activator = mocks.CreateMock<ITypeActivator>();
			IServiceAgentFactory factory = mocks.CreateMock<IServiceAgentFactory>();

			IServiceAgent serviceAgent1 = mocks.CreateMock<IServiceAgent>();
			IServiceAgent serviceAgent2 = mocks.CreateMock<IServiceAgent>();

			IServiceAgent[] serviceAgents = new IServiceAgent[] { serviceAgent1, serviceAgent2 };

			ILogger logger = mocks.CreateMock<ILogger>();

			IServiceAgentAggregator aggregator = new ServiceAgentAggregator(settings, activator, logger);

			using (mocks.Record())
			{
				Expect.Call(settings.GetServiceAgentFactory()).Return("serviceAgentType");
				Expect.Call(activator.ActivateType<IServiceAgentFactory>("serviceAgentType")).Return(factory);
				Expect.Call(factory.GetServiceAgents()).Return(serviceAgents);

				Expect.Call(serviceAgent1.AgentName).Return("FirstAgent").Repeat.Any();
				logger.Debug(aggregator, "Executing agent: FirstAgent");
				serviceAgent1.Run();
				LastCall.On(serviceAgent1).Throw(exception);
				logger.Error(aggregator, "Test Exception", exception);

				Expect.Call(serviceAgent2.AgentName).Return("SecondAgent").Repeat.Any();
				logger.Debug(aggregator, "Executing agent: SecondAgent");
				serviceAgent2.Run();
				logger.Debug(aggregator, "Agent execution completed: SecondAgent");
			}

			using (mocks.Playback())
			{
				aggregator.ExecuteServiceAgentCycle();
			}

			mocks.VerifyAll();
		}
	}
}