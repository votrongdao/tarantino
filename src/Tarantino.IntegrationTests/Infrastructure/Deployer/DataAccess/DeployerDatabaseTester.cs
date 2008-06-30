using System.Collections.Generic;

namespace Tarantino.IntegrationTests.Infrastructure.Deployer.DataAccess
{
	public abstract class DeployerDatabaseTester : DatabaseTesterBase
	{
		protected override IEnumerable<string> GetTablesToDelete()
		{
			return new[] {"Deployment"};
		}

		public override string ConfigurationFile
		{
			get { return "deployer.hibernate.cfg.xml"; }
		}
	}
}