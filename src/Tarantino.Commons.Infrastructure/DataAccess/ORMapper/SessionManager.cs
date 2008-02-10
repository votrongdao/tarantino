using System;
using Tarantino.Commons.Core;
using NHibernate;
using StructureMap;

namespace Tarantino.Commons.Infrastructure.DataAccess.ORMapper
{
	[Pluggable(ServiceKeys.Default)]
	public class SessionManager : ISessionManager
	{
		private ISessionScoper _sessionScoper;

		public SessionManager(ISessionScoper sessionScoper)
		{
			_sessionScoper = sessionScoper;
		}

		public object Run(SessionCommand command, bool requiresTransaction, params object[] arguments)
		{
			object returnValue;
			ISession session = _sessionScoper.GetScopedSession();
			bool ownsTransaction = false;

			ITransaction transaction = null;

			if (requiresTransaction)
			{
				ownsTransaction = true;
				transaction = GetTransaction();
			}

			try
			{
				returnValue = RunSessionOperation(command, arguments, session);
				if (ownsTransaction)
				{
					CommitTransaction(transaction);
				}
			}
			catch (Exception ex)
			{
				Exception currentException = ex;

				while (currentException != null)
				{
					Console.WriteLine(ex.Message);
					currentException = currentException.InnerException;
				}

				if (transaction != null)
				{
					RollbackTransaction(transaction);
				}

				throw;
			}
			return returnValue;
		}

		private object RunSessionOperation(SessionCommand command, object[] arguments, ISession session)
		{
			return command(session, arguments);
		}

		public ITransaction GetTransaction()
		{
			ISession session = _sessionScoper.GetScopedSession();
			ITransaction transaction = session.BeginTransaction();
			return transaction;
		}

		public void CommitTransaction(ITransaction transaction)
		{
			try
			{
				transaction.Commit();
			}
			catch
			{
				RollbackTransaction(transaction);
				throw;
			}
		}

		public void RollbackTransaction(ITransaction transaction)
		{
			if (transaction.IsActive && !transaction.WasRolledBack)
				transaction.Rollback();

			_sessionScoper.Reset();
		}

		public void ResetSession()
		{
			_sessionScoper.Reset();
		}
	}
}