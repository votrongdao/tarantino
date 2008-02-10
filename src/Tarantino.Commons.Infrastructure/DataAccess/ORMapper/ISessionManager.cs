using NHibernate;
using StructureMap;

namespace Tarantino.Commons.Infrastructure.DataAccess.ORMapper
{
	public delegate object SessionCommand(ISession session, params object[] arguments);

	[PluginFamily("Default")]
	public interface ISessionManager
	{
		object Run(SessionCommand command, bool requiresTransaction, params object[] arguments);
		void ResetSession();
	}
}