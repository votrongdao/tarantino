using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using StructureMap;
using Tarantino.Commons.Core.Services.Environment;

namespace Tarantino.IntegrationTests.Commons.Core.Services.Environment
{
	[TestFixture]
	public class TypeActivatorTester
	{
		[Test]
		public void Activates_type_from_descriptor()
		{
			ITypeActivator typeActivator = ObjectFactory.GetInstance<ITypeActivator>();

			IResourceFileLocator locator = typeActivator.ActivateType<IResourceFileLocator>(
				"Tarantino.Commons.Core.Services.Environment.Impl.ResourceFileLocator, Tarantino.Commons.Core");

			Assert.That(locator, Is.Not.Null);
		}
	}
}