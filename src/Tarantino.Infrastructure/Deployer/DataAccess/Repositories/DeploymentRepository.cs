using System.Collections.Generic;
using Tarantino.Core.Commons.Model.Enumerations;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Deployer.Services;

namespace Tarantino.Infrastructure.Deployer.DataAccess.Repositories
{
	public class DeploymentRepository : IDeploymentRepository
	{
		private readonly IPersistentObjectRepository _repository;

		public DeploymentRepository(IPersistentObjectRepository repository)
		{
			_repository = repository;
			_repository.ConfigurationFile = "deployer.hibernate.cfg.xml";
		}

		public IEnumerable<Deployment> Find(string application, string environment)
		{
			var criteria = getCriteria(application, environment);
			var deployments = _repository.FindAll<Deployment>(criteria);

			return deployments;
		}

		public IEnumerable<Deployment> FindSuccessfulUncertified(string application, string environment)
		{
			var criteria = getCriteria(application, environment);
			criteria.AddCriterion(new Criterion(Deployment.CERTIFIED_ON, null));
			criteria.AddCriterion(new Criterion(Deployment.RESULT, DeploymentResult.Success));
			var deployments = _repository.FindAll<Deployment>(criteria);

			return deployments;
		}

		public IEnumerable<Deployment> FindCertified(string application, string environment)
		{
			var criteria = getCriteria(application, environment);
			criteria.AddCriterion(new Criterion(Deployment.CERTIFIED_ON, null, ComparisonOperator.NotEqual));
			criteria.AddCriterion(new Criterion(Deployment.RESULT, DeploymentResult.Success));
			var deployments = _repository.FindAll<Deployment>(criteria);

			return deployments;
		}

		private CriterionSet getCriteria(string application, string environment)
		{
			var criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(Deployment.APPLICATION, application));
			criteria.AddCriterion(new Criterion(Deployment.ENVIRONMENT, environment));
			criteria.OrderBy = Deployment.DEPLOYED_ON;
			criteria.SortOrder = SortOrder.Descending;

			return criteria;
		}
	}
}