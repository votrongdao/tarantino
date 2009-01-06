using Tarantino.Core.Commons.Model;

namespace Tarantino.Core.Deployer.Model
{
	public class DeploymentOutput : PersistentObject
	{
		public virtual string Output { get; set; }
		public virtual Deployment Deployment { get; set; }
	}
}