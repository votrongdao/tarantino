using System;
using Tarantino.Commons.Core.Services.Configuration;
using Tarantino.Commons.Core.Services.Environment;
using Tarantino.Commons.Core.Services.Logging;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Daemon.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class ServiceAgentAggregator : IServiceAgentAggregator
	{
		private readonly IApplicationSettings _settings;
		private readonly ITypeActivator _activator;

		public ServiceAgentAggregator(IApplicationSettings settings, ITypeActivator activator)
		{
			_settings = settings;
			_activator = activator;
		}

		public void ExecuteServiceAgentCycle()
		{
			string factoryType = _settings.GetServiceAgentFactory();
			IServiceAgentFactory factory = _activator.ActivateType<IServiceAgentFactory>(factoryType);

			foreach (IServiceAgent agent in factory.GetServiceAgents())
			{
				try
				{
					Type agentType = agent.GetType();
					Log.Debug(this, string.Format("Executing agent: {0}", agentType));
					agent.Run();
					Log.Debug(this, string.Format("Agent execution completed: {0}", agentType));
				}
				catch (Exception ex)
				{
					Log.Error(this, ex.Message, ex);
				}
			}
		}
	}
}