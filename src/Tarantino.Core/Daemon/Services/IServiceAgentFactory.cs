namespace Tarantino.Commons.Core.Services.Daemon
{
	public interface IServiceAgentFactory
	{
		IServiceAgent[] GetServiceAgents();
	}
}