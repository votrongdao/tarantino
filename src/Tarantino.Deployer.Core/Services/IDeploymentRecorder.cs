namespace Tarantino.Deployer.Core.Services
{
	public interface IDeploymentRecorder
	{
		int RecordDeployment(string application, string environment, string output);
	}
}