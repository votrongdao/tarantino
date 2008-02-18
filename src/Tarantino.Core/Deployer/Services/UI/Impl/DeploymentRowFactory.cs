using Tarantino.Deployer.Core.Model;
using Tarantino.Commons.Core;
using StructureMap;

namespace Tarantino.Deployer.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class DeploymentRowFactory : IDeploymentRowFactory
	{
		public string[] ConstructRow(Deployment deployment)
		{
			string revision = deployment.Revision.ToString();
			string deployedOn = deployment.DeployedOn.ToString("g");
			string deployedBy = deployment.DeployedBy;
			string result = deployment.Result.DisplayName;
			string certifiedOn = deployment.CertifiedOn != null ? deployment.CertifiedOn.Value.ToString("g") : string.Empty;
			string certifiedBy = deployment.CertifiedBy;
			string output = deployment.Output;

			return new string[] { revision, deployedOn, deployedBy, result, certifiedOn, certifiedBy, output };
		}
	}
}