using NUnit.Framework;
using Tarantino.Core.WebManagement.Model;
using Tarantino.IntegrationTests.Infrastructure.Deployer.DataAccess;

namespace Tarantino.IntegrationTests.Infrastructure.WebManagement.DataAccess.Mappings
{
	[TestFixture]
	public class ApplicationInstanceMappingTester : WebManagementDatabaseTester
	{
		[Test]
		public void Can_persist_application_instance()
		{
			ApplicationInstance instance = new ApplicationInstance();

			instance.ApplicationDomain = "ApplicationDomain";
			instance.AvailableForLoadBalancing = true;
			instance.CacheRefreshQueryString = "QueryString";
			instance.DownForMaintenance = false;
			instance.LoadBalanceIP = "10.10.10.15";
			instance.MachineName = "My Machine";
			instance.MaintenanceHostHeader = "HostHeader";
			instance.UniqueHostHeader = "Unique Host Header";
			instance.Version = "Version";

			SaveAndFlushSessionFor(instance);
			ApplicationInstance reloadedInstance = LoadFromDatabaseAndAssertMatchFor(instance);

			AssertObjectsMatch(instance, reloadedInstance);
		}
	}
}