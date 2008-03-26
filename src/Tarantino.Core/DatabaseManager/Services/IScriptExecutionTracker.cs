using StructureMap;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IScriptExecutionTracker
	{
		void MarkScriptAsExecuted(ConnectionSettings settings, string scriptFilename, ITaskObserver task);
		bool ScriptAlreadyExecuted(ConnectionSettings settings, string scriptFilename);
	}
}