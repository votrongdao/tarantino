using System;
using NHibernate;
using StructureMap;
using Tarantino.Core;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	[Pluggable(Keys.Default)]
	public class SessionManager : ISessionManager
	{
		private ISessionScoper _sessionScoper;

		public SessionManager(ISessionScoper sessionScoper)
		{
			_sessionScoper = sessionScoper;
		}

		public object Run(SessionCommand command, bool requiresTransaction, string connectionStringKey, params object[] arguments)
		{
			object returnValue;
			ISession session = _sessionScoper.GetScopedSession(connectionStringKey);
			bool ownsTransaction = false;

			ITransaction transaction = null;

			if (requiresTransaction)
			{
				ownsTransaction = true;
				transaction = GetTransaction(connectionStringKey);
			}

			try
			{
				returnValue = RunSessionOperation(command, arguments, session);
				if (ownsTransaction)
				{
					CommitTransaction(transaction, connectionStringKey);
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
					RollbackTransaction(transaction, connectionStringKey);
				}

				throw;
			}
			return returnValue;
		}

		private object RunSessionOperation(SessionCommand command, object[] arguments, ISession session)
		{
			return command(session, arguments);
		}

		public ITransaction GetTransaction(string connectionStringKey)
		{
			ISession session = _sessionScoper.GetScopedSession(connectionStringKey);
			ITransaction transaction = session.BeginTransaction();
			return transaction;
		}

		public void CommitTransaction(ITransaction transaction, string connectionStringKey)
		{
			try
			{
				transaction.Commit();
			}
			catch
			{
				RollbackTransaction(transaction, connectionStringKey);
				throw;
			}
		}

		public void RollbackTransaction(ITransaction transaction, string connectionStringKey)
		{
			if (transaction.IsActive && !transaction.WasRolledBack)
				transaction.Rollback();

			_sessionScoper.Reset(connectionStringKey);
		}
	}
}