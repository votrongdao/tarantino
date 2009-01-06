namespace Tarantino.Core.Deployer.Services
{
	public interface IDeploymentRecorder
	{
		int RecordDeployment(string application, string environment, string output);
	}
}