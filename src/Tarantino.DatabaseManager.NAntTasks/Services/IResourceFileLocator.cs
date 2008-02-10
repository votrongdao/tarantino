namespace Tarantino.DatabaseManager.NAntTasks.Services
{
	public interface IResourceFileLocator
	{
		string ReadTextFile(string resourceName);
	}
}