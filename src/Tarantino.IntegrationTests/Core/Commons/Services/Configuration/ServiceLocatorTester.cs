using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Environment;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Infrastructure.Commons.Services.Configuration;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Configuration
{
	[TestFixture]
	public class ServiceLocatorTester : InfrastructureIntegrationTester
	{
		[Test]
		public void Correctly_Constructs_Instance()
		{
			IServiceLocator serviceLocator = new ServiceLocator();

			var instance = serviceLocator.CreateInstance<IResourceFileLocator>();

			Assert.That(instance, Is.Not.Null);
		}

		[Test]
		public void Correctly_Constructs_Instance_With_Key()
		{
			IServiceLocator serviceLocator = new ServiceLocator();

			var instance = serviceLocator.CreateInstance<IDatabaseActionExecutor>("Create");

			Assert.That(instance, Is.Not.Null);
		}
	}
}