using Tarantino.Commons.Core.Services.Configuration;
using Tarantino.Commons.Core.Services.Configuration.Impl;
using Tarantino.Commons.Core.Services.Net;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.IntegrationTests.Commons.Core.Services.Configuration
{
	[TestFixture]
	public class ServiceLocatorTester
	{
		[Test]
		public void Correctly_Constructs_Instance()
		{
			IServiceLocator serviceLocator = new ServiceLocator();

			IWebDataReader instance = serviceLocator.CreateInstance<IWebDataReader>();

			Assert.That(instance, Is.Not.Null);
		}

		[Test]
		public void Correctly_Constructs_Instance_With_Key()
		{
			IServiceLocator serviceLocator = new ServiceLocator();

			IWebDataReader instance = serviceLocator.CreateInstance<IWebDataReader>("Default");
			
			Assert.That(instance, Is.Not.Null);
		}
	}
}