
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	
	public interface IDatabaseVersioner
	{
		void VersionDatabase(ConnectionSettings settings, ITaskObserver taskObserver);
	}
}