using System.Web;
using Tarantino.Commons.Core;
using NHibernate;
using StructureMap;

namespace Tarantino.Commons.Infrastructure.DataAccess.ORMapper
{
	[Pluggable(ServiceKeys.Secondary)]
	public class HttpSessionScoper : ISessionScoper
	{
		private readonly ISessionFactoryManager _sessionFactoryManager;
		private static readonly string _httpLocalStorageKey = "orMapperSession";

		public HttpSessionScoper(ISessionFactoryManager sessionFactoryManager)
		{
			_sessionFactoryManager = sessionFactoryManager;
		}

		public ISession GetScopedSession()
		{
			ISession session = getCurrentSession();

			if (session == null || !session.IsOpen)
			{
				session = _sessionFactoryManager.GetSessionFactory().OpenSession();
				session.FlushMode = FlushMode.Commit;
				HttpContext.Current.Items[_httpLocalStorageKey] = session;
			}

			return session;
		}

		public void Reset()
		{
			ISession oldSession = getCurrentSession();

			if (oldSession != null)
			{
				oldSession.Dispose();
			}

			HttpContext.Current.Items[_httpLocalStorageKey] = null;
		}

		public bool CanHandle()
		{
			return true;
		}

		private static ISession getCurrentSession()
		{
			ISession session = HttpContext.Current.Items[_httpLocalStorageKey] as ISession;
			return session;
		}
	}
}