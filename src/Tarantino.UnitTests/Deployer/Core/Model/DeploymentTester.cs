using System;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Model.Enumerations;
using NUnit.Framework;

namespace Tarantino.UnitTests.Deployer.Core.Model
{
	[TestFixture]
	public class DeploymentTester
	{
		[Test]
		public void Property_accessors_work()
		{
			Deployment deployment = new Deployment();
			
			Assert.AreEqual(null, deployment.Application);
			deployment.Application = "Application";
			Assert.AreEqual("Application", deployment.Application);

			Assert.AreEqual(null, deployment.Environment);
			deployment.Environment = "Environment";
			Assert.AreEqual("Environment", deployment.Environment);

			Assert.AreEqual(0, deployment.Revision);
			deployment.Revision = 5;
			Assert.AreEqual(5, deployment.Revision);

			Assert.AreEqual(DateTime.MinValue, deployment.DeployedOn);
			deployment.DeployedOn = new DateTime(2007, 4, 15);
			Assert.AreEqual(new DateTime(2007, 4, 15), deployment.DeployedOn);

			Assert.AreEqual(null, deployment.DeployedBy);
			deployment.DeployedBy = "DeployedBy";
			Assert.AreEqual("DeployedBy", deployment.DeployedBy);

			Assert.AreEqual(null, deployment.CertifiedOn);
			deployment.CertifiedOn = new DateTime(2007, 4, 15);
			Assert.AreEqual(new DateTime(2007, 4, 15), deployment.CertifiedOn);

			Assert.AreEqual(null, deployment.CertifiedBy);
			deployment.CertifiedBy = "CertifiedBy";
			Assert.AreEqual("CertifiedBy", deployment.CertifiedBy);

			Assert.AreEqual(null, deployment.Output);
			deployment.Output = "Output";
			Assert.AreEqual("Output", deployment.Output);

			Assert.AreEqual(null, deployment.Result);
			deployment.Result = DeploymentResult.Failure;
			Assert.AreSame(DeploymentResult.Failure, deployment.Result);

		}

		[Test]
		public void Assert_serialization_works()
		{
			Deployment deployment = new Deployment();
			deployment.Revision = 155;
			Assert.That(deployment.ToString(), Is.EqualTo("155"));
		}
	}
}