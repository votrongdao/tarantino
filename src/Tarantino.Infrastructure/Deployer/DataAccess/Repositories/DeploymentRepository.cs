using System.Collections.Generic;
using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Model.Enumerations;
using Tarantino.Deployer.Core.Model.Repositories;
using Tarantino.Commons.Core;
using Tarantino.Commons.Core.Model.Enumerations;
using Tarantino.Commons.Core.Model.Repositories;
using StructureMap;

namespace Tarantino.Deployer.Infrastructure.DataAccess.Repositories
{
	[Pluggable(ServiceKeys.Default)]
	public class DeploymentRepository : IDeploymentRepository
	{
		private readonly IPersistentObjectRepository _repository;

		public DeploymentRepository(IPersistentObjectRepository repository)
		{
			_repository = repository;
		}

		public IEnumerable<Deployment> Find(string application, string environment)
		{
			CriterionSet criteria = getCriteria(application, environment);
			IEnumerable<Deployment> deployments = _repository.FindAll<Deployment>(criteria);

			return deployments;
		}

		public IEnumerable<Deployment> FindSuccessfulUncertified(string application, string environment)
		{
			CriterionSet criteria = getCriteria(application, environment);
			criteria.AddCriterion(new Criterion(Deployment.CERTIFIED_ON, null));
			criteria.AddCriterion(new Criterion(Deployment.RESULT, DeploymentResult.Success));
			IEnumerable<Deployment> deployments = _repository.FindAll<Deployment>(criteria);

			return deployments;
		}

		public IEnumerable<Deployment> FindCertified(string application, string environment)
		{
			CriterionSet criteria = getCriteria(application, environment);
			criteria.AddCriterion(new Criterion(Deployment.CERTIFIED_ON, null, ComparisonOperator.NotEqual));
			criteria.AddCriterion(new Criterion(Deployment.RESULT, DeploymentResult.Success));
			IEnumerable<Deployment> deployments = _repository.FindAll<Deployment>(criteria);

			return deployments;
		}

		private CriterionSet getCriteria(string application, string environment)
		{
			CriterionSet criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(Deployment.APPLICATION, application));
			criteria.AddCriterion(new Criterion(Deployment.ENVIRONMENT, environment));
			criteria.OrderBy = Deployment.DEPLOYED_ON;
			criteria.SortOrder = SortOrder.Descending;

			return criteria;
		}
	}
}