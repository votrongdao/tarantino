using System;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Environment;
using StructureMap;
using Tarantino.Core.Commons.Services.Logging;

namespace Tarantino.Core.Daemon.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class ServiceAgentAggregator : IServiceAgentAggregator
	{
		private readonly IApplicationSettings _settings;
		private readonly ITypeActivator _activator;
		private readonly ILogger _logger;

		public ServiceAgentAggregator(IApplicationSettings settings, ITypeActivator activator, ILogger logger)
		{
			_settings = settings;
			_activator = activator;
			_logger = logger;
		}

		public void ExecuteServiceAgentCycle()
		{
			string factoryType = _settings.GetServiceAgentFactory();
			IServiceAgentFactory factory = _activator.ActivateType<IServiceAgentFactory>(factoryType);

			foreach (IServiceAgent agent in factory.GetServiceAgents())
			{
				try
				{
					_logger.Debug(this, string.Format("Executing agent: {0}", agent.AgentName));
					agent.Run();
					_logger.Debug(this, string.Format("Agent execution completed: {0}", agent.AgentName));
				}
				catch (Exception ex)
				{
					_logger.Error(this, ex.Message, ex);
				}
			}
		}
	}
}