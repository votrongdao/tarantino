

namespace Tarantino.Core.Deployer.Services
{
	
	public interface IDeploymentRecorder
	{
		void RecordDeployment(string application, string environment, string output);
	}
}