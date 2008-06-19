using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using StructureMap;
using Tarantino.Infrastructure.Commons.DataAccess;
using Tarantino.IntegrationTests.Infrastructure.Deployer.DataAccess;

namespace Tarantino.IntegrationTests.Infrastructure.Commons.DataAccess.ORMapper
{
	[TestFixture]
	public class SessionFactoryManagerTester : DeployerDatabaseTester
	{
		[Test]
		public void Correctly_caches_sessions()
		{
			var sessionBuilder = ObjectFactory.GetInstance<ISessionBuilder>();

			var deployerSession = sessionBuilder.GetSession(ConfigurationFile);
			var webSession1 = sessionBuilder.GetSession("webmanagement.hibernate.cfg.xml");
			Assert.That(deployerSession, Is.Not.SameAs(webSession1));

			var webSession2 = sessionBuilder.GetSession("webmanagement.hibernate.cfg.xml");
			Assert.That(webSession2, Is.SameAs(webSession1));
		}
	}
}