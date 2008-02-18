using Tarantino.Core.Deployer.Services.Configuration;
using Tarantino.Core.Deployer.Services.Configuration.Impl;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Configuration.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Core.Deployer.Services.Configuration
{
	[TestFixture]
	public class ApplicationRepositoryTester
	{
		[Test]
		public void Reads_application_collection_from_configuration_file()
		{
			DeployerSettingsConfigurationHandler handler = new DeployerSettingsConfigurationHandler();

			MockRepository mocks = new MockRepository();
			IApplicationConfiguration configuration = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(configuration.GetSection("DeployerSettings")).Return(handler);
			}

			using (mocks.Playback())
			{
				IApplicationRepository repository = new ApplicationRepository(configuration);
				ElementCollection<Application> applications = repository.GetAll();

				Assert.That(applications, Is.SameAs(handler.Applications));
			}

			mocks.VerifyAll();
		}
	}
}