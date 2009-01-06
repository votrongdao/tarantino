using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.Commons.Services.Configuration.Impl;
using Tarantino.Core.Deployer.Services.Configuration.Impl;

namespace Tarantino.UnitTests.Core.Deployer.Services.Configuration
{
	[TestFixture]
	public class ApplicationTester
	{
		[Test]
		public void Should_return_correct_configuration_element_name()
		{
			var application = new Application();
			Assert.That(application.GetElementName(), Is.EqualTo("Application"));
		}
	}
}