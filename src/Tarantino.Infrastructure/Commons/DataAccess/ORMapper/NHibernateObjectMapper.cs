using System;
using System.Collections;
using NHibernate;
using StructureMap;
using Tarantino.Core;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	
	public class NHibernateObjectMapper : IObjectMapper
	{
		public const string DefaultConnectionStringKey = "DatabaseConnectionString";

		private string _connectionStringKey = DefaultConnectionStringKey;

		public string ConnectionStringKey
		{
			get { return _connectionStringKey; }
			set { _connectionStringKey = value; }
		}

		private ISessionManager _manager;

		public NHibernateObjectMapper(ISessionManager manager)
		{
			_manager = manager;
		}

		public void Save(object domainObject)
		{
			_manager.Run(new SessionCommand(save), true, _connectionStringKey, domainObject);
		}

		private object save(ISession session, params object[] arguments)
		{
			session.Save(arguments[0]);
			return null;
		}

		public void SaveOrUpdate(object domainObject)
		{
			_manager.Run(new SessionCommand(saveOrUpdate), true, _connectionStringKey, domainObject);
		}

		private object saveOrUpdate(ISession session, params object[] arguments)
		{
			session.SaveOrUpdate(arguments[0]);
			return null;
		}

		public void Delete(params object[] domainObjects)
		{
			_manager.Run(new SessionCommand(delete), true, _connectionStringKey, domainObjects);
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
			object retValue = _manager.Run(new SessionCommand(load), false, _connectionStringKey, typeof (T), id);
			return (T) retValue;
		}

		private object load(ISession session, params object[] arguments)
		{
			return session.Load((Type) arguments[0], arguments[1]);
		}

		public T[] LoadAll<T>()
		{
			object retValue = _manager.Run(new SessionCommand(loadAll), false, _connectionStringKey, typeof (T));
			IList list = (IList) retValue;
			T[] allObjects = new T[list.Count];
			list.CopyTo(allObjects, 0);

			return allObjects;
		}

		public object[] LoadAll(Type type)
		{
			object retValue = _manager.Run(new SessionCommand(loadAll), false, _connectionStringKey, type);
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
			_manager.Run(new SessionCommand(flush), true, _connectionStringKey, null);
		}

		private object flush(ISession session, params object[] arguments)
		{
			session.Flush();
			return null;
		}

		public void Evict(params object[] domainObjects)
		{
			_manager.Run(new SessionCommand(evict), false, _connectionStringKey, domainObjects);
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
			_manager.Run(new SessionCommand(refresh), false, _connectionStringKey, domainObjects);
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
			return _manager.Run(command, isTransaction, _connectionStringKey, arguments);
		}

		public void ExecuteNonQuery(string sql, Type type)
		{
			//Using SystemUser because NHibernate requires an entity type.  Any type will do.  This doesn't return anything.
			_manager.Run(runSql, false, _connectionStringKey, sql, "su", type);
		}

		public bool IsDirty()
		{
			return (bool) _manager.Run(isDirty, false, _connectionStringKey);
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
			_manager.Run(lockObject, false, _connectionStringKey, domainObject);
		}

		private object lockObject(ISession session, params object[] arguments)
		{
			session.Lock(arguments[0], LockMode.Upgrade);
			return null;
		}

		public void Add(params object[] domainObjects)
		{
			_manager.Run(new SessionCommand(addObjects), false, _connectionStringKey, domainObjects);
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
			_manager.Run(new SessionCommand(addNewObject), false, _connectionStringKey, domainObject);
		}

		private object addNewObject(ISession session, params object[] arguments)
		{
			session.Save(arguments[0]);
			return null;
		}
	}
}