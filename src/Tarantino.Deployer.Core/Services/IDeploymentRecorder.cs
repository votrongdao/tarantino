namespace Tarantino.Deployer.Core.Services
{
	public interface IDeploymentRecorder
	{
		string RecordDeployment(string application, string environment, string output, string revision, bool failed);
	}
}