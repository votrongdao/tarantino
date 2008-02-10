using System;
using System.Collections.Generic;
using Tarantino.Deployer.Core.Model;
using Tarantino.IntegrationTests.Commons.Infrastructure.DataAccess;

namespace Tarantino.Deployer.Infrastructure.IntegrationTests.DataAccess
{
	public class DeploymentDatabaseTester : DatabaseTesterBase
	{
		public static string[] TablesToDelete
		{
			get { return new string[] { "Deployment" }; }
		}

		protected override IEnumerable<string> GetTablesToDelete()
		{
			return TablesToDelete;
		}

		protected override Type GetEntityType()
		{
			return typeof(Deployment);
		}
	}
}