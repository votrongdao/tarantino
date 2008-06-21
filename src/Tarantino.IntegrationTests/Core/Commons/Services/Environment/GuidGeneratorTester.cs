using NUnit.Framework;
using StructureMap;
using Tarantino.Core.Commons.Services.Environment;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Environment
{
	[TestFixture]
	public class GuidGeneratorTester : InfrastructureIntegrationTester
	{
		[Test]
		public void Should_generate_new_guid()
		{
			var generator = ObjectFactory.GetInstance<IGuidGenerator>();

			generator.CreateGuid();
		}
	}
}