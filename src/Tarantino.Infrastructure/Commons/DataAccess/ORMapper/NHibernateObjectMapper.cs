using System;
using System.Collections;
using Tarantino.Commons.Core;
using NHibernate;
using StructureMap;

namespace Tarantino.Commons.Infrastructure.DataAccess.ORMapper
{
	[Pluggable(ServiceKeys.Default)]
	public class NHibernateObjectMapper : IObjectMapper
	{
		private ISessionManager _manager;

		public NHibernateObjectMapper(ISessionManager manager)
		{
			_manager = manager;
		}

		public void Save(object domainObject)
		{
			_manager.Run(new SessionCommand(save), true, domainObject);
		}

		private object save(ISession session, params object[] arguments)
		{
			session.Save(arguments[0]);
			return null;
		}

		public void SaveOrUpdate(object domainObject)
		{
			_manager.Run(new SessionCommand(saveOrUpdate), true, domainObject);
		}

		private object saveOrUpdate(ISession session, params object[] arguments)
		{
			session.SaveOrUpdate(arguments[0]);
			return null;
		}

		public void Delete(params object[] domainObjects)
		{
			_manager.Run(new SessionCommand(delete), true, domainObjects);
		}

		private object delete(ISession session, params object[] arguments)
		{
			foreach (object domainObject in arguments)
			{
				session.Delete(domainObject);
			}

			return null;
		}

		public T Load<T>(object id)
		{
			object retValue = _manager.Run(new SessionCommand(load), false, typeof (T), id);
			return (T) retValue;
		}

		private object load(ISession session, params object[] arguments)
		{
			return session.Load((Type) arguments[0], arguments[1]);
		}

		public T[] LoadAll<T>()
		{
			object retValue = _manager.Run(new SessionCommand(loadAll), false, typeof (T));
			IList list = (IList) retValue;
			T[] allObjects = new T[list.Count];
			list.CopyTo(allObjects, 0);

			return allObjects;
		}

		public object[] LoadAll(Type type)
		{
			object retValue = _manager.Run(new SessionCommand(loadAll), false, type);
			IList list = (IList) retValue;
			return (object[]) new ArrayList(list).ToArray(typeof (object));
		}

		private object loadAll(ISession session, params object[] arguments)
		{
			ICriteria criteria = session.CreateCriteria((Type) arguments[0]);
			criteria.SetCacheable(true);
			IList list = criteria.List();
			return list;
		}

		public void Flush()
		{
			_manager.Run(new SessionCommand(flush), true, null);
		}

		private object flush(ISession session, params object[] arguments)
		{
			session.Flush();
			return null;
		}

		public void Evict(params object[] domainObjects)
		{
			_manager.Run(new SessionCommand(evict), false, domainObjects);
		}

		private object evict(ISession session, params object[] arguments)
		{
			foreach (object argument in arguments)
			{
				session.Evict(argument);
			}
			return null;
		}

		public void Refresh(params object[] domainObjects)
		{
			_manager.Run(new SessionCommand(refresh), false, domainObjects);
		}

		private object refresh(ISession session, params object[] arguments)
		{
			foreach (object argument in arguments)
			{
				session.Refresh(argument);
			}
			return null;
		}

		public object Run(SessionCommand command, bool isTransaction, params object[] arguments)
		{
			return _manager.Run(command, isTransaction, arguments);
		}

		public void ExecuteNonQuery(string sql, Type type)
		{
			//Using SystemUser because NHibernate requires an entity type.  Any type will do.  This doesn't return anything.
			_manager.Run(runSql, false, sql, "su", type);
		}

		public bool IsDirty()
		{
			return (bool) _manager.Run(isDirty, false);
		}

		private object isDirty(ISession session, params object[] arguments)
		{
			return session.IsDirty();
		}

		private object runSql(ISession session, params object[] arguments)
		{
			string sqlString = arguments[0].ToString();
			string typeAlias = arguments[1].ToString();
			Type typeClass = (Type) arguments[2];
			IQuery query = session.CreateSQLQuery(sqlString).AddEntity(typeAlias, typeClass);
			return query.UniqueResult();
		}

		public void Lock(object domainObject)
		{
			_manager.Run(lockObject, false, domainObject);
		}

		private object lockObject(ISession session, params object[] arguments)
		{
			session.Lock(arguments[0], LockMode.Upgrade);
			return null;
		}

		public void Add(params object[] domainObjects)
		{
			_manager.Run(new SessionCommand(addObjects), false, domainObjects);
		}

		private object addObjects(ISession session, params object[] arguments)
		{
			foreach (object domainObject in arguments)
			{
				session.SaveOrUpdate(domainObject);
			}
			return null;
		}

		public void AddNew(object domainObject)
		{
			_manager.Run(new SessionCommand(addNewObject), false, domainObject);
		}

		private object addNewObject(ISession session, params object[] arguments)
		{
			session.Save(arguments[0]);
			return null;
		}
	}
}