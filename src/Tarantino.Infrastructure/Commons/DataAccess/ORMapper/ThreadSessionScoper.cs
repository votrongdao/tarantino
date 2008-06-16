using System;
using System.Threading;
using NHibernate;
using StructureMap;
using Tarantino.Core;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	
	public class ThreadSessionScoper : ISessionScoper
	{
		private readonly ISessionFactoryManager _sessionFactoryManager;
		private static readonly string _threadLocalStorageKey = "orMapperSession_{0}";

		public ThreadSessionScoper(ISessionFactoryManager sessionFactoryManager)
		{
			_sessionFactoryManager = sessionFactoryManager;
		}

		public bool CanHandle()
		{
			return true;
		}

		public ISession GetScopedSession(string connectionStringKey)
		{
			LocalDataStoreSlot sessionSlot = getThreadSlot(connectionStringKey);
			ISession threadSession = Thread.GetData(sessionSlot) as ISession;

			if (threadSession == null)
			{
				threadSession = attachNewSessionToThread(sessionSlot, connectionStringKey);
			}

			if (!threadSession.IsOpen)
			{
				threadSession = attachNewSessionToThread(sessionSlot, connectionStringKey);
			}

			return threadSession;
		}

		private static LocalDataStoreSlot getThreadSlot(string connectionStringKey)
		{
			string key = string.Format(_threadLocalStorageKey, connectionStringKey);
			LocalDataStoreSlot slot = Thread.GetNamedDataSlot(key);
			return slot;
		}

		private ISession attachNewSessionToThread(LocalDataStoreSlot sessionSlot, string connectionStringKey)
		{
			ISession threadSession;
			threadSession = _sessionFactoryManager.GetSessionFactory(connectionStringKey).OpenSession();
			threadSession.FlushMode = FlushMode.Commit;
			Thread.SetData(sessionSlot, threadSession);
			return threadSession;
		}

		public void Reset(string connectionStringKey)
		{
			ResetSession(connectionStringKey);
		}

		public static void ResetSession(string connectionStringKey)
		{
			LocalDataStoreSlot slot = getThreadSlot(connectionStringKey);
			ISession oldSession = Thread.GetData(slot) as ISession;
			if (oldSession != null)
			{
				oldSession.Dispose();
			}

			Thread.SetData(slot, null);
		}
	}
}