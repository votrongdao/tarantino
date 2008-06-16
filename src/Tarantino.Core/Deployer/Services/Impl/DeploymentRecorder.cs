using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Commons.Services.Security;


namespace Tarantino.Core.Deployer.Services.Impl
{
	
	public class DeploymentRecorder : IDeploymentRecorder
	{
		private readonly ISecurityContext _securityContext;
		private readonly IDeploymentFactory _factory;
		private readonly IPersistentObjectRepository _repository;

		public DeploymentRecorder(ISecurityContext securityContext, IDeploymentFactory factory, IPersistentObjectRepository repository)
		{
			_securityContext = securityContext;
			_factory = factory;
			_repository = repository;
		}

		public void RecordDeployment(string application, string environment, string output)
		{
			string deployedBy = _securityContext.GetCurrentUsername();
			Deployment deployment = _factory.CreateDeployment(application, environment, deployedBy, output);
			_repository.Save(deployment);
		}
	}
}