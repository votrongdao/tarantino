using StructureMap;

namespace Tarantino.Core.WebManagement.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ISecureAvailabilityStatusUpdater
	{
		string SetStatus(bool enabled);
	}
}