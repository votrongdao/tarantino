using Tarantino.Deployer.Core.Model;
using Tarantino.Commons.Core;
using Tarantino.Commons.Core.Services.Environment;
using Tarantino.Commons.Core.Services.Security;
using StructureMap;

namespace Tarantino.Deployer.Core.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class RevisionCertifier : IRevisionCertifier
	{
		private readonly ISystemClock _clock;
		private readonly ISecurityContext _securityContext;
		private readonly IPersistentObjectRepository _repository;

		public RevisionCertifier(ISystemClock clock, ISecurityContext securityContext, IPersistentObjectRepository repository)
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