using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Security;
using StructureMap;

namespace Tarantino.Core.Deployer.Services.Impl
{
	[Pluggable(Keys.Default)]
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