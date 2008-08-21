using System.Reflection;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.Commons.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace Tarantino.Infrastructure.Commons.DataAccess.Repositories
{
	public class RepositoryBase
	{
		private readonly ISessionBuilder _sessionBuilder;

		public RepositoryBase(ISessionBuilder sessionFactory)
		{
			_sessionBuilder = sessionFactory;
		}

		public virtual string ConfigurationFile { get; set; }

		protected ISession GetSession()
		{
			var session = ConfigurationFile == null ? _sessionBuilder.GetSession() : _sessionBuilder.GetSession(ConfigurationFile);
			return session;
		}

		protected void AssertObjectCanBePersisted<T>(T persistentObject) where T : PersistentObject
		{
			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(persistentObject);
				session.Flush();
			}

			using (ISession session = GetSession())
			{
				var reloadedObject = session.Load<T>(persistentObject.Id);
				Assert.That(reloadedObject, Is.EqualTo(persistentObject));
				Assert.That(reloadedObject, Is.Not.SameAs(persistentObject));
				AssertObjectsMatch(persistentObject, reloadedObject);
			}
		}

		protected static void AssertObjectsMatch(object obj1, object obj2)
		{
			Assert.AreNotSame(obj1, obj2);
			Assert.AreEqual(obj1, obj2);

			var infos = obj1.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (var info in infos)
			{
				var value1 = info.GetValue(obj1, null);
				var value2 = info.GetValue(obj2, null);
				Assert.AreEqual(value1, value2, string.Format("Property {0} doesn't match", info.Name));
			}
		}
	}
}