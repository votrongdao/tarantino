using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using StructureMap;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using Tarantino.IntegrationTests.Infrastructure.Deployer.DataAccess;

namespace Tarantino.IntegrationTests.Infrastructure.Commons.DataAccess.ORMapper
{
	[TestFixture]
	public class SessionFactoryManagerTester : DeployerDatabaseTester
	{
		[Test]
		public void Correctly_caches_session_factory_manager()
		{
			var sessionFactoryManager = ObjectFactory.GetInstance<ISessionFactoryManager>();

			var sessionFactory = sessionFactoryManager.GetSessionFactory(ConnectionStringKey);
			Assert.That(sessionFactoryManager.GetSessionFactory(ConnectionStringKey), Is.SameAs(sessionFactory));

			var sessionFactory2 = sessionFactoryManager.GetSessionFactory("TarantinoWebManagementConnectionString");
			Assert.That(sessionFactoryManager.GetSessionFactory(ConnectionStringKey), Is.Not.SameAs(sessionFactory2));

			var sessionFactory3 = sessionFactoryManager.GetSessionFactory("TarantinoWebManagementConnectionString");
			Assert.That(sessionFactory3, Is.SameAs(sessionFactory2));
		}
	}
}