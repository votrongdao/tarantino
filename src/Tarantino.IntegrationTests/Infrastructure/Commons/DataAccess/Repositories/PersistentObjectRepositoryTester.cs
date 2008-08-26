using System;
using Tarantino.Core.Commons.Model;
using Tarantino.Core.Commons.Model.Enumerations;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using StructureMap;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Deployer.Model;
using Tarantino.IntegrationTests.Infrastructure.Deployer.DataAccess;

namespace Tarantino.IntegrationTests.Infrastructure.Commons.DataAccess.Repositories
{
	[TestFixture]
	public class PersistentObjectRepositoryTester : DeployerDatabaseTester
	{
		private Deployment _deployment1;
		private Deployment _deployment2;

		protected override void SetupDatabase()
		{
			_deployment1 = new Deployment();
			_deployment2 = new Deployment();

			_deployment1.DeployedOn = new DateTime(2007, 5, 15);

			_deployment2.Environment = "Development";
			_deployment2.DeployedOn = new DateTime(2007, 4, 15);

			Save(_deployment1, _deployment2);
		}

		[Test]
		public void Can_find_deployment_using_criteria()
		{
			var repository = getRepository();

			var set = new CriterionSet();
			set.AddCriterion(new Criterion(PersistentObject.ID, _deployment2.Id));

			Assert.That(repository.FindAll<Deployment>(set), Is.EqualTo(new[] { _deployment2 }));
			Assert.That(repository.FindFirst<Deployment>(set), Is.EqualTo(_deployment2));
		}

		[Test]
		public void Can_find_deployment_using_null_string_criteria()
		{
			var repository = getRepository();

			var set = new CriterionSet();
			set.AddCriterion(new Criterion(Deployment.ENVIRONMENT, null));

			Assert.That(repository.FindAll<Deployment>(set), Is.EquivalentTo(new[] { _deployment1 }));
		}

		[Test]
		public void Can_find_deployment_using_not_null_string_criteria()
		{
			var repository = getRepository();

			var set = new CriterionSet();
			set.AddCriterion(new Criterion(Deployment.ENVIRONMENT, null, ComparisonOperator.NotEqual));

			Assert.That(repository.FindAll<Deployment>(set), Is.EquivalentTo(new[] { _deployment2 }));
		}

		[Test]
		public void Can_find_deployments_in_order_of_create_date_descending()
		{
			var repository = getRepository();

			var set = new CriterionSet {OrderBy = Deployment.DEPLOYED_ON, SortOrder = SortOrder.Ascending};

			Assert.That(repository.FindAll<Deployment>(set), Is.EqualTo(new[] { _deployment2, _deployment1 }));
		}

		[Test]
		public void Can_find_deployment_using_not_equal_string_criteria()
		{
			var repository = getRepository();

			var set = new CriterionSet();
			set.AddCriterion(new Criterion(Deployment.ENVIRONMENT, "Development", ComparisonOperator.NotEqual));

			Assert.That(repository.FindAll<Deployment>(set), Is.EquivalentTo(
			                                                 	new Deployment[0]));
		}

		[Test]
		public void Can_find_all_deployments_by_not_using_criteria()
		{
			var repository = getRepository();

			var set = new CriterionSet();

			var deployments = repository.FindAll<Deployment>(set);
			Assert.That(deployments, Is.EquivalentTo(new[] { _deployment1, _deployment2 }));
		}

		[Test]
		public void Can_get_all_deployments()
		{
			var repository = getRepository();
			var deployments = repository.GetAll<Deployment>();
			EnumerableAssert.That(deployments, Is.EquivalentTo(new[] { _deployment1, _deployment2 }));
		}

		[Test]
		public void Can_get_single_deployment()
		{
			var repository = getRepository();
			var deployment = repository.GetById<Deployment>(_deployment2.Id);
			Assert.That(deployment, Is.EqualTo(_deployment2));
		}

		[Test]
		public void Can_delete_single_deployment()
		{
			var repository = getRepository();
			repository.Delete(_deployment1);

			var deployments = repository.GetAll<Deployment>();
			EnumerableAssert.That(deployments, Is.EquivalentTo(new[] { _deployment2 }));
		}

		private IPersistentObjectRepository getRepository()
		{
			var repository = ObjectFactory.GetInstance<IPersistentObjectRepository>();
			repository.ConfigurationFile = ConfigurationFile;
			return repository;
		}
	}
}