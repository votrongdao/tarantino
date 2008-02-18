using System;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Deployer.Services;
using Tarantino.Core.Deployer.Services.Impl;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Security;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Core.Deployer.Services
{
	[TestFixture]
	public class RevisionCertifierTester
	{
		[Test]
		public void Certifies_deployment()
		{
			Deployment deployment = new Deployment();

			MockRepository mocks = new MockRepository();
			ISystemClock clock = mocks.CreateMock<ISystemClock>();
			ISecurityContext securityContext = mocks.CreateMock<ISecurityContext>();
			IPersistentObjectRepository repository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				Expect.Call(clock.GetCurrentDateTime()).Return(new DateTime(2007, 4, 15));
				Expect.Call(securityContext.GetCurrentUsername()).Return("khurwitz");
				repository.Save(deployment);
			}

			using (mocks.Playback())
			{
				IRevisionCertifier certifier = new RevisionCertifier(clock, securityContext, repository);
				certifier.Certify(deployment);
				
				Assert.That(deployment.CertifiedBy, Is.EqualTo("khurwitz"));
				Assert.That(deployment.CertifiedOn, Is.EqualTo(new DateTime(2007, 4, 15)));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Does_not_certify_undefined_deployment()
		{
			IRevisionCertifier certifier = new RevisionCertifier(null, null, null);
			certifier.Certify(null);
		}
	}
}