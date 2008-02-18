namespace Tarantino.DatabaseManager.NAntTasks.Services
{
	public interface IFileSystem
	{
		string[] GetAllFilesWithExtensionWithinFolder(string folder, string fileExtension);
		string ReadTextFile(string filename);
	}
}