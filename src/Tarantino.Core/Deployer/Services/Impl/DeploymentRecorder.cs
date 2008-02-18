using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Model.Enumerations;
using Tarantino.Commons.Core;
using Tarantino.Commons.Core.Services.Security;
using StructureMap;

namespace Tarantino.Deployer.Core.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
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