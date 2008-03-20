using StructureMap;

namespace Tarantino.Core.Deployer.Services
{
	[PluginFamily(Keys.Default)]
	public interface IDeploymentRecorder
	{
		void RecordDeployment(string application, string environment, string output);
	}
}