
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	
	public interface ILogMessageGenerator
	{
		string GetInitialMessage(RequestedDatabaseAction requestedDatabaseAction, string scriptDirectory, ConnectionSettings settings);
	}
}