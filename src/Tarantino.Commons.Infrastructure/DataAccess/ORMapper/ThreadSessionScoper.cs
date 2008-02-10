using System;
using System.Threading;
using Tarantino.Commons.Core;
using NHibernate;
using StructureMap;

namespace Tarantino.Commons.Infrastructure.DataAccess.ORMapper
{
	[Pluggable(ServiceKeys.Default)]
	public class ThreadSessionScoper : ISessionScoper
	{
		private readonly ISessionFactoryManager _sessionFactoryManager;
		private static readonly string _threadLocalStorageKey = "orMapperSession";

		public ThreadSessionScoper(ISessionFactoryManager sessionFactoryManager)
		{
			_sessionFactoryManager = sessionFactoryManager;
		}

		public bool CanHandle()
		{
			return true;
		}

		public ISession GetScopedSession()
		{
			return getNHibernateSession();
		}

		private static LocalDataStoreSlot getThreadSlot()
		{
			return Thread.GetNamedDataSlot(_threadLocalStorageKey);
		}

		private ISession getNHibernateSession()
		{
			LocalDataStoreSlot sessionSlot = getThreadSlot();
			ISession threadSession = Thread.GetData(sessionSlot) as ISession;

			if (threadSession == null)
			{
				threadSession = attachNewSessionToThread(sessionSlot);
			}

			if (!threadSession.IsOpen)
			{
				threadSession = attachNewSessionToThread(sessionSlot);
			}

			return threadSession;
		}

		private ISession attachNewSessionToThread(LocalDataStoreSlot sessionSlot)
		{
			ISession threadSession;
			threadSession = _sessionFactoryManager.GetSessionFactory().OpenSession();
			threadSession.FlushMode = FlushMode.Commit;
			Thread.SetData(sessionSlot, threadSession);
			return threadSession;
		}

		public void Reset()
		{
			ResetSession();
		}

		public static void ResetSession()
		{
			LocalDataStoreSlot slot = getThreadSlot();
			ISession oldSession = Thread.GetData(slot) as ISession;
			if (oldSession != null)
			{
				oldSession.Dispose();
			}

			Thread.SetData(slot, null);
		}
	}
}