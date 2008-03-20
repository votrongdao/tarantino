using System.Collections.Generic;
using StructureMap;
using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily(Keys.Default)]
	public interface IDatabaseActionExecutorFactory
	{
		IEnumerable<IDatabaseActionExecutor> GetExecutors(RequestedDatabaseAction requestedDatabaseAction);
	}
}