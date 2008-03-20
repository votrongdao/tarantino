using System.Web;
using NHibernate;
using StructureMap;
using Tarantino.Core;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	[Pluggable(Keys.Secondary)]
	public class HttpSessionScoper : ISessionScoper
	{
		private readonly ISessionFactoryManager _sessionFactoryManager;
		private static readonly string _httpLocalStorageKey = "orMapperSession_{0}";

		public HttpSessionScoper(ISessionFactoryManager sessionFactoryManager)
		{
			_sessionFactoryManager = sessionFactoryManager;
		}

		public ISession GetScopedSession(string connectionStringKey)
		{
			ISession session = getCurrentSession(connectionStringKey);

			if (session == null || !session.IsOpen)
			{
				session = _sessionFactoryManager.GetSessionFactory(connectionStringKey).OpenSession();
				session.FlushMode = FlushMode.Commit;
				HttpContext.Current.Items[getKey(connectionStringKey)] = session;
			}

			return session;
		}

		public void Reset(string connectionStringKey)
		{
			ISession oldSession = getCurrentSession(connectionStringKey);

			if (oldSession != null)
			{
				oldSession.Dispose();
			}

			HttpContext.Current.Items[getKey(connectionStringKey)] = null;
		}

		public bool CanHandle()
		{
			return true;
		}

		private static ISession getCurrentSession(string connectionStringKey)
		{
			ISession session = HttpContext.Current.Items[getKey(connectionStringKey)] as ISession;
			return session;
		}
		
		private static string getKey(string connectionStringKey)
		{
			return string.Format(_httpLocalStorageKey, connectionStringKey);
		}
	}
}