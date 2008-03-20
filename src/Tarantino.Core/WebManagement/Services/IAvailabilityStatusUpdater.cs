using StructureMap;

namespace Tarantino.Core.WebManagement.Services
{
	[PluginFamily(Keys.Default)]
	public interface IAvailabilityStatusUpdater
	{
		void SetAvailabilityStatus(bool enabled);
	}
}