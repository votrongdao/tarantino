using NUnit.Framework;
using StructureMap;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Services.Views.Impl;

namespace Tarantino.IntegrationTests.Infrastructure.WebManagement.Services.Views
{
	[TestFixture]
	public class ResourceTester
	{
		[Test]
		public void Correctly_finds_resource_files()
		{
			IResourceFileLocator locator = ObjectFactory.GetInstance<IResourceFileLocator>();

			Assert.That(locator.FileExists("Tarantino.Core", LoadBalancerBodyView.LoadBalancerBodyTemplate));
			Assert.That(locator.FileExists("Tarantino.Core", MenuView.MenuTemplate));
			Assert.That(locator.FileExists("Tarantino.Core", PageView.PageTemplate));
			Assert.That(locator.FileExists("Tarantino.Core", PageView.StylesheetTemplate));
		}
	}
}