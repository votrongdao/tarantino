using NHibernate;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	public delegate object SessionCommand(ISession session, params object[] arguments);

	public interface ISessionManager
	{
		object Run(SessionCommand command, bool requiresTransaction, string connectionStringKey, params object[] arguments);
	}
}