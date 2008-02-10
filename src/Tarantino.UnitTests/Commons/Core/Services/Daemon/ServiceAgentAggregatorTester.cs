using System;
using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Commons.Core.Services.Configuration;
using Tarantino.Commons.Core.Services.Daemon;
using Tarantino.Commons.Core.Services.Daemon.Impl;
using Tarantino.Commons.Core.Services.Environment;

namespace Tarantino.UnitTests.Commons.Core.Services.Daemon
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

			IServiceAgent[] serviceAgents = new IServiceAgent[] { serviceAgent1, serviceAgent2 };

			using (mocks.Record())
			{
				Expect.Call(settings.GetServiceAgentFactory()).Return("serviceAgentType");
				Expect.Call(activator.ActivateType<IServiceAgentFactory>("serviceAgentType")).Return(factory);
				Expect.Call(factory.GetServiceAgents()).Return(serviceAgents);

				serviceAgent1.Run();
				serviceAgent2.Run();
			}

			using (mocks.Playback())
			{
				IServiceAgentAggregator aggregator = new ServiceAgentAggregator(settings, activator);

				aggregator.ExecuteServiceAgentCycle();
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Should_continue_with_next_agents_event_if_one_agent_fails()
		{
			MockRepository mocks = new MockRepository();

			IApplicationSettings settings = mocks.CreateMock<IApplicationSettings>();
			ITypeActivator activator = mocks.CreateMock<ITypeActivator>();
			IServiceAgentFactory factory = mocks.CreateMock<IServiceAgentFactory>();

			IServiceAgent serviceAgent1 = mocks.CreateMock<IServiceAgent>();
			IServiceAgent serviceAgent2 = mocks.CreateMock<IServiceAgent>();

			IServiceAgent[] serviceAgents = new IServiceAgent[] { serviceAgent1, serviceAgent2 };

			using (mocks.Record())
			{
				Expect.Call(settings.GetServiceAgentFactory()).Return("serviceAgentType");
				Expect.Call(activator.ActivateType<IServiceAgentFactory>("serviceAgentType")).Return(factory);
				Expect.Call(factory.GetServiceAgents()).Return(serviceAgents);

				serviceAgent1.Run();
				LastCall.On(serviceAgent1).Throw(new ApplicationException());
				serviceAgent2.Run();
			}

			using (mocks.Playback())
			{
				IServiceAgentAggregator aggregator = new ServiceAgentAggregator(settings, activator);

				aggregator.ExecuteServiceAgentCycle();
			}

			mocks.VerifyAll();
		}
	}
}