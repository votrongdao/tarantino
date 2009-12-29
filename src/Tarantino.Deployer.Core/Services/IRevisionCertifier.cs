using Tarantino.Deployer.Core.Model;


namespace Tarantino.Deployer.Core.Services
{
	
	public interface IRevisionCertifier
	{
		void Certify(Deployment deployment);
	}
}