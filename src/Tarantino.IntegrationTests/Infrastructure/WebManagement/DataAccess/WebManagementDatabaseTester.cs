using System.Collections.Generic;

namespace Tarantino.IntegrationTests.Infrastructure.WebManagement.DataAccess
{
	public abstract class WebManagementDatabaseTester : DatabaseTesterBase
	{
		protected override IEnumerable<string> GetTablesToDelete()
		{
			return new[]{"ApplicationInstance"};
		}

		protected override string ConfigurationFile
		{
			get { return "webmanagement.hibernate.cfg.xml"; }
		}
	}
}