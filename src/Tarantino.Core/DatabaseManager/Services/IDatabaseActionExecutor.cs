
using Tarantino.Core.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	public interface IDatabaseActionExecutor
	{
		void Execute(string scriptFolder, ConnectionSettings settings, ITaskObserver taskObserver);
	}
}