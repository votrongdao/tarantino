using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Deployer.Services;
using Tarantino.Core.Deployer.Services.Impl;
using Tarantino.Core.Commons.Services.Security;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Core.Deployer.Services
{
	[TestFixture]
	public class DeploymentRecorderTester
	{
		[Test]
		public void Records_deployment()
		{
			var deployment = new Deployment();

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
				recorder.RecordDeployment("application", "environment", "Output...");
			}

			mocks.VerifyAll();
		}
	}
}