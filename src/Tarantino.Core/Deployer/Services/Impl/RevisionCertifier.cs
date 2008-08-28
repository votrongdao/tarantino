using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Deployer.Model;

namespace Tarantino.Core.Deployer.Services.Impl
{
	public class RevisionCertifier : IRevisionCertifier
	{
		private readonly ISystemClock _clock;
		private readonly ISecurityContext _securityContext;
		private readonly IDeploymentRepository _repository;

		public RevisionCertifier(ISystemClock clock, ISecurityContext securityContext, IDeploymentRepository repository)
		{
			_clock = clock;
			_securityContext = securityContext;
			_repository = repository;
		}

		public void Certify(Deployment deployment)
		{
			if (deployment != null)
			{
				deployment.CertifiedBy = _securityContext.GetCurrentUsername();
				deployment.CertifiedOn = _clock.GetCurrentDateTime();

				_repository.Save(deployment);
			}
		}
	}
}