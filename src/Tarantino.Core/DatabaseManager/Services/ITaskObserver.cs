namespace Tarantino.DatabaseManager.NAntTasks.Services
{
	public interface ITaskObserver
	{
		void Log(string message);
		void SetVariable(string name, string value);
	}
}