using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using StructureMap;
using Tarantino.Core.Commons.Model;
using NUnit.Framework;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace Tarantino.IntegrationTests
{
	public abstract class DatabaseTesterBase
	{
		private IObjectMapper _mapper;

		protected abstract string ConnectionStringKey { get; }

		[SetUp]
		public virtual void SetUp()
		{
			_mapper = ObjectFactory.GetInstance<IObjectMapper>();
			_mapper.ConnectionStringKey = ConnectionStringKey;
			ThreadSessionScoper.ResetSession(ConnectionStringKey);
			ClearTables();
			SetupDatabase();
		}

		protected virtual void SetupDatabase()
		{
		}

		protected void ClearTables()
		{
			var tables = GetTablesToDelete();

			foreach (var table in tables)
			{
				DeleteFromTable(table);
			}
		}

		protected abstract IEnumerable<string> GetTablesToDelete();

		private void DeleteFromTable(string tableName)
		{
			_mapper.ExecuteNonQuery(string.Format("delete from {0}", tableName), GetEntityType());
		}

		protected abstract Type GetEntityType();

		protected void SaveAndFlushSessionFor(params PersistentObject[] persistentObjects)
		{
			foreach (var persistentObject in persistentObjects)
			{
				_mapper.SaveOrUpdate(persistentObject);
				_mapper.Evict(persistentObject);
			}
		}

		protected T LoadFromDatabaseAndAssertMatchFor<T>(T entity) where T : PersistentObject
		{
			_mapper.Evict(entity);

			var entityFromDatabase = _mapper.Load<T>(entity.Id);

			AssertObjectsMatch(entity, entityFromDatabase);
			return entityFromDatabase;
		}

		protected void AssertObjectsMatch(object obj1, object obj2)
		{
			Assert.AreEqual(obj1.GetType(), obj2.GetType());
			Assert.AreNotSame(obj1, obj2);
			Assert.AreEqual(obj1, obj2);

			var infos = obj1.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (var info in infos)
			{
				if (info.PropertyType != typeof(IDictionary))
				{
					var value1 = info.GetValue(obj1, null);
					var value2 = info.GetValue(obj2, null);
					Assert.AreEqual(value1, value2, string.Format("Property {0} doesn't match", info.Name));
				}
			}
		}
	}
}