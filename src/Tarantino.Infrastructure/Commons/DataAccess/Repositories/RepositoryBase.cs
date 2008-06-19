using NHibernate;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace Tarantino.Infrastructure.Commons.DataAccess
{
	public class RepositoryBase
	{
		private readonly ISessionBuilder _sessionBuilder;

		public RepositoryBase(ISessionBuilder sessionFactory)
		{
			_sessionBuilder = sessionFactory;
		}

		protected ISession GetSession()
		{
			var session = _sessionBuilder.GetSession();
			return session;
		}

		protected ISession GetSession(string configurationFile)
		{
			var session = _sessionBuilder.GetSession(configurationFile);
			return session;
		}
	}
}