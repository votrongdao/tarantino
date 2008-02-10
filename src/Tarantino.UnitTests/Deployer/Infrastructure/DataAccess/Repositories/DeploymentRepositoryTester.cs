using System.Collections.Generic;
using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Model.Enumerations;
using Tarantino.Deployer.Core.Model.Repositories;
using Tarantino.Deployer.Infrastructure.DataAccess.Repositories;
using Tarantino.Commons.Core;
using Tarantino.Commons.Core.Model.Enumerations;
using Tarantino.Commons.Core.Model.Repositories;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Deployer.Infrastructure.DataAccess.Repositories
{
	[TestFixture]
	public class DeploymentRepositoryTester
	{
		[Test]
		public void Returns_deployments_by_application_and_environment()
		{
			CriterionSet criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(Deployment.APPLICATION, "SampleApp1"));
			criteria.AddCriterion(new Criterion(Deployment.ENVIRONMENT, "Environment"));
			criteria.OrderBy = Deployment.DEPLOYED_ON;
			criteria.SortOrder = SortOrder.Descending;

			Deployment[] foundDeployments = new Deployment[0];

			MockRepository mocks = new MockRepository();
			IPersistentObjectRepository repository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				Expect.Call(repository.FindAll<Deployment>(criteria)).Return(foundDeployments);
			}

			using (mocks.Playback())
			{
				IDeploymentRepository deploymentRepository = new DeploymentRepository(repository);

				IEnumerable<Deployment> deployments = deploymentRepository.Find("SampleApp1", "Environment");

				Assert.That(deployments, Is.SameAs(foundDeployments));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Returns_uncertified_deployments_by_application_and_environment()
		{
			CriterionSet criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(Deployment.APPLICATION, "SampleApp1"));
			criteria.AddCriterion(new Criterion(Deployment.ENVIRONMENT, "Environment"));
			criteria.AddCriterion(new Criterion(Deployment.CERTIFIED_ON, null));
			criteria.AddCriterion(new Criterion(Deployment.RESULT, DeploymentResult.Success));
			criteria.OrderBy = Deployment.DEPLOYED_ON;
			criteria.SortOrder = SortOrder.Descending;

			Deployment[] foundDeployments = new Deployment[0];

			MockRepository mocks = new MockRepository();
			IPersistentObjectRepository repository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				Expect.Call(repository.FindAll<Deployment>(criteria)).Return(foundDeployments);
			}

			using (mocks.Playback())
			{
				IDeploymentRepository deploymentRepository = new DeploymentRepository(repository);

				IEnumerable<Deployment> deployments = deploymentRepository.FindSuccessfulUncertified("SampleApp1", "Environment");

				Assert.That(deployments, Is.SameAs(foundDeployments));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Returns_certified_deployments_by_application_and_environment()
		{
			CriterionSet criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(Deployment.APPLICATION, "SampleApp1"));
			criteria.AddCriterion(new Criterion(Deployment.ENVIRONMENT, "Environment"));
			criteria.AddCriterion(new Criterion(Deployment.CERTIFIED_ON, null, ComparisonOperator.NotEqual));
			criteria.AddCriterion(new Criterion(Deployment.RESULT, DeploymentResult.Success));
			criteria.OrderBy = Deployment.DEPLOYED_ON;
			criteria.SortOrder = SortOrder.Descending;

			Deployment[] foundDeployments = new Deployment[0];

			MockRepository mocks = new MockRepository();
			IPersistentObjectRepository repository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				Expect.Call(repository.FindAll<Deployment>(criteria)).Return(foundDeployments);
			}

			using (mocks.Playback())
			{
				IDeploymentRepository deploymentRepository = new DeploymentRepository(repository);

				IEnumerable<Deployment> deployments = deploymentRepository.FindCertified("SampleApp1", "Environment");

				Assert.That(deployments, Is.SameAs(foundDeployments));
			}

			mocks.VerifyAll();
		}
	}
}