using System;
using System.Collections.Generic;
using Tarantino.Core.Deployer.Model;

namespace Tarantino.IntegrationTests
{
	public abstract class DeployerDatabaseTester : DatabaseTesterBase
	{
		protected override IEnumerable<string> GetTablesToDelete()
		{
			return new string[]{"Deployment"};
		}

		protected override Type GetEntityType()
		{
			return typeof(Deployment);
		}
	}
}