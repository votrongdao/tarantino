using System.Collections.Generic;
using StructureMap;
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.Core.DatabaseManager.Services.Impl.Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.Core.DatabaseManager.Services
{
	[PluginFamily(Keys.Default)]
	public interface IDatabaseActionResolver
	{
		IEnumerable<DatabaseAction> GetActions(RequestedDatabaseAction requestedDatabaseAction);
	}
}