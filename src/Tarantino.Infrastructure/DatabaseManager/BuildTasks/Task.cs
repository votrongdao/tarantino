using NAnt.Core;
using NAnt.Core.Tasks;

namespace Tarantino.DatabaseManager.NAntTasks.Services.Impl
{
	public class Task : NAntTask, ITaskObserver
	{
		public void Log(string message)
		{
			Project.Log(Level.Info, message);
		}

		public void SetVariable(string name, string value)
		{
			PropertyDictionary properties = Project.Properties;

			if (properties.Contains(name))
			{
				properties[name] = value;
			}
			else
				properties.Add(name, value);
		}
	}
}