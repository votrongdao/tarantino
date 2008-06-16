
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	
	public class LogMessageGenerator : ILogMessageGenerator
	{
		public string GetInitialMessage(RequestedDatabaseAction requestedDatabaseAction, string scriptDirectory, ConnectionSettings settings)
		{
			string scriptFolder = requestedDatabaseAction != RequestedDatabaseAction.Drop
															? string.Format(" using scripts from {0}", scriptDirectory)
															: string.Empty;

			string logMessage = string.Format("{0} {1} on {2}{3}\n", requestedDatabaseAction, 
				settings.Database, settings.Server, scriptFolder);

			return logMessage;
		}
	}
}