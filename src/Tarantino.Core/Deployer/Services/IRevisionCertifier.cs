using Tarantino.Core.Deployer.Model;


namespace Tarantino.Core.Deployer.Services
{
	
	public interface IRevisionCertifier
	{
		void Certify(Deployment deployment);
	}
}