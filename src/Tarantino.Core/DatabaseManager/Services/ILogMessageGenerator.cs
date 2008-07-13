
using Tarantino.Core.DatabaseManager.Model;
using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.Core.DatabaseManager.Services
{
	
	public interface ILogMessageGenerator
	{
		string GetInitialMessage(RequestedDatabaseAction requestedDatabaseAction, string scriptDirectory, ConnectionSettings settings);
	}
}