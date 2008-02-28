using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using StructureMap;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;
using Tarantino.IntegrationTests.Infrastructure.Deployer.DataAccess;

namespace Tarantino.IntegrationTests.Infrastructure.WebManagement.DataAccess.Repositories
{
	[TestFixture]
	public class ApplicationInstanceRepositoryTester : WebManagementDatabaseTester
	{
		private ApplicationInstance _instance1;
		private ApplicationInstance _instance2;
		private ApplicationInstance _instance3;
		private ApplicationInstance _instance4;

		protected override void SetupDatabase()
		{
			_instance1 = new ApplicationInstance();
			_instance2 = new ApplicationInstance();
			_instance3 = new ApplicationInstance();
			_instance4 = new ApplicationInstance();

			_instance1.ApplicationDomain = "Domain1";
			_instance2.ApplicationDomain = "Domain1";

			_instance1.MachineName = "Machine1";
			_instance2.MachineName = "Machine2";

			_instance3.MachineName = "Machine1";
			_instance4.MachineName = "Machine2";

			_instance3.UniqueHostHeader = "HostHeader1";
			_instance4.UniqueHostHeader = "HostHeader2";

			SaveAndFlushSessionFor(_instance1, _instance2, _instance3, _instance4);
		} 

		[Test]
		public void Can_correctly_gets_application_instances_by_machine_name_and_domain()
		{
			IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
			ApplicationInstance actualInstance = repository.GetByDomainAndMachineName("Domain1", "Machine2");

			Assert.That(actualInstance, Is.EqualTo(_instance2));
		}

		[Test]
		public void Can_correctly_gets_application_instances_by_host_header()
		{
			IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
			IEnumerable<ApplicationInstance> actualInstances = repository.GetByHostHeader("HostHeader2");

			Assert.That(actualInstances, Is.EqualTo(new ApplicationInstance[]{ _instance4 }));
		}
	}
}