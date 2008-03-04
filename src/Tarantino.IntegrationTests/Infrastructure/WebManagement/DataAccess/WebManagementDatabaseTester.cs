using System;
using System.Collections.Generic;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.IntegrationTests.Infrastructure.Deployer.DataAccess
{
	public abstract class WebManagementDatabaseTester : DatabaseTesterBase
	{
		protected override IEnumerable<string> GetTablesToDelete()
		{
			return new string[]{"Management_Application_Instance"};
		}

		protected override Type GetEntityType()
		{
			return typeof(ApplicationInstance);
		}

		protected override string ConnectionStringKey
		{
			get { return "TarantinoWebManagementConnectionString"; }
		}

	}
}