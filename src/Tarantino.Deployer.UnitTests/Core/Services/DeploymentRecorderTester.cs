using NUnit.Framework.SyntaxHelpers;
using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Services;
using Tarantino.Deployer.Core.Services.Impl;
using Tarantino.Core.Commons.Services.Security;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tarantino.Deployer.UnitTests.Core.Services
{
	[TestFixture]
	public class DeploymentRecorderTester
	{
		[Test]
		public void Records_deployment()
		{
			var deployment = new Deployment {Revision = 1234};

			var mocks = new MockRepository();
			var factory = mocks.CreateMock<IDeploymentFactory>();
			var repository = mocks.CreateMock<IDeploymentRepository>();
			var context = mocks.CreateMock<ISecurityContext>();

			using (mocks.Record())
			{
				Expect.Call(context.GetCurrentUsername()).Return("jsmith");
				Expect.Call(factory.CreateDeployment("application", "environment", "jsmith", "Output...")).Return(deployment);
				repository.Save(deployment);
			}

			using (mocks.Playback())
			{
				IDeploymentRecorder recorder = new DeploymentRecorder(context, factory, repository);
				int revision = recorder.RecordDeployment("application", "environment", "Output...");

				Assert.That(revision, Is.EqualTo(revision));
			}

			mocks.VerifyAll();
		}
	}
}