using NHibernate;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	public interface ISessionScoper
	{
		bool CanHandle();
		ISession GetScopedSession(string connectionStringKey);
		void Reset(string connectionStringKey);
	}
}