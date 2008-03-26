using StructureMap;

namespace Tarantino.Core.WebManagement.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IAvailabilityStatusUpdater
	{
		void SetAvailabilityStatus(bool enabled);
	}
}