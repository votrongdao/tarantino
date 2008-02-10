using System;
using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Model.Enumerations;
using Tarantino.Deployer.UI.Services;
using Tarantino.Deployer.UI.Services.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.UnitTests.Deployer.UI.Services
{
	[TestFixture]
	public class DeploymentRowFactoryTester
	{
		[Test]
		public void Constructs_deployment_row()
		{
			Deployment deployment = new Deployment();
			deployment.Revision = 845;
			deployment.DeployedOn = new DateTime(2007, 4, 15, 6, 45, 32);
			deployment.DeployedBy = "khurwitz";
			deployment.Result = DeploymentResult.Failure;
			deployment.CertifiedOn = new DateTime(2007, 5, 15, 8, 45, 32);
			deployment.CertifiedBy = "jpalermo";
			deployment.Output = "Output...";

			IDeploymentRowFactory factory = new DeploymentRowFactory();

			string[] row = factory.ConstructRow(deployment);

			Assert.That(row.Length, Is.EqualTo(7));
			Assert.That(row[0], Is.EqualTo("845"));
			Assert.That(row[1], Is.EqualTo("4/15/2007 6:45 AM"));
			Assert.That(row[2], Is.EqualTo("khurwitz"));
			Assert.That(row[3], Is.EqualTo("Failure"));
			Assert.That(row[4], Is.EqualTo("5/15/2007 8:45 AM"));
			Assert.That(row[5], Is.EqualTo("jpalermo"));
			Assert.That(row[6], Is.EqualTo("Output..."));

			//string revision = deployment.Revision.ToString();
			//string deployedOn = deployment.DeployedOn.ToString("g");
			//string deployedBy = deployment.DeployedBy;
			//string result = deployment.Result.DisplayName;
			//string certifiedOn = deployment.CertifiedOn != null ? deployment.CertifiedOn.Value.ToString("g") : string.Empty;
			//string certifiedBy = deployment.CertifiedBy;
			//string output = deployment.Output;
		}
	}
}