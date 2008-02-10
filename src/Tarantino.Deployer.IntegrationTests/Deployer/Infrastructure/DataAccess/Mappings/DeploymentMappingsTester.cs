using System;
using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Model.Enumerations;
using Tarantino.Deployer.Infrastructure.IntegrationTests.DataAccess;
using NUnit.Framework;

namespace Tarantino.Deployer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	[TestFixture]
	public class DeploymentMappingTester : DeploymentDatabaseTester
	{
		[Test]
		public void Can_persist_deployment()
		{
			Deployment deployment = new Deployment();

			deployment.Application = "SampleApp1";
			deployment.Environment = "Development";
			deployment.CertifiedBy = "Certifer";
			deployment.DeployedBy = "Deployer";
			deployment.DeployedOn = new DateTime(2007, 3, 15);
			deployment.CertifiedOn = new DateTime(2007, 4, 15);
			deployment.Revision = 250;
			deployment.Output = "Output text";
			deployment.Result = DeploymentResult.Failure;

			SaveAndFlushSessionFor(deployment);
			Deployment reloadedDeployment = LoadFromDatabaseAndAssertMatchFor(deployment);

			AssertObjectsMatch(deployment, reloadedDeployment);
		}
	}
}