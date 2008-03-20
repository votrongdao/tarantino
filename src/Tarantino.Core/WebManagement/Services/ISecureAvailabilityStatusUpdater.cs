using StructureMap;

namespace Tarantino.Core.WebManagement.Services
{
	[PluginFamily(Keys.Default)]
	public interface ISecureAvailabilityStatusUpdater
	{
		string SetStatus(bool enabled);
	}
}