using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;
using Tarantino.Infrastructure.WebManagement.DataAccess.Repositories;

namespace Tarantino.UnitTests.Infrastructure.WebManagement.DataAccess.Repositories
{
	[TestFixture]
	public class ApplicationInstanceRepositoryTester
	{
		[Test]
		public void Can_correctly_gets_all_application_instances()
		{
			ApplicationInstance instance1 = new ApplicationInstance();
			ApplicationInstance instance2 = new ApplicationInstance();
			ApplicationInstance[] instances = new ApplicationInstance[] { instance1, instance2 };

			MockRepository mocks = new MockRepository();
			IPersistentObjectRepository objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				objectRepository.ConnectionStringKey = "TarantinoWebManagementConnectionString";
				Expect.Call(objectRepository.GetAll<ApplicationInstance>()).Return(instances);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceRepository repository = new ApplicationInstanceRepository(objectRepository);
				IEnumerable<ApplicationInstance> actualInstances = repository.GetAll();
				
				EnumerableAssert.That(actualInstances, Is.EqualTo(instances));
			}
		}

		[Test]
		public void Can_correctly_gets_single_application_instance()
		{
			Guid id = Guid.NewGuid();
			ApplicationInstance instance = new ApplicationInstance();

			MockRepository mocks = new MockRepository();
			IPersistentObjectRepository objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				objectRepository.ConnectionStringKey = "TarantinoWebManagementConnectionString";
				Expect.Call(objectRepository.GetById<ApplicationInstance>(id)).Return(instance);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceRepository repository = new ApplicationInstanceRepository(objectRepository);
				ApplicationInstance actualInstance = repository.GetById(id);
				
				Assert.That(actualInstance, Is.SameAs(instance));
			}
		}

		[Test]
		public void Can_correctly_save_application_instance()
		{
			ApplicationInstance instance = new ApplicationInstance();

			MockRepository mocks = new MockRepository();
			IPersistentObjectRepository objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				objectRepository.ConnectionStringKey = "TarantinoWebManagementConnectionString";
				objectRepository.Save(instance);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceRepository repository = new ApplicationInstanceRepository(objectRepository);
				repository.Save(instance);
			}
		}

		[Test]
		public void Can_correctly_delete_application_instance()
		{
			ApplicationInstance instance = new ApplicationInstance();

			MockRepository mocks = new MockRepository();
			IPersistentObjectRepository objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				objectRepository.ConnectionStringKey = "TarantinoWebManagementConnectionString";
				objectRepository.Delete(instance);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceRepository repository = new ApplicationInstanceRepository(objectRepository);
				repository.Delete(instance);
			}
		}

		[Test]
		public void Can_correctly_gets_application_instances_by_machine_name_and_domain()
		{
			ApplicationInstance instance = new ApplicationInstance();

			CriterionSet criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion("MachineName", "MyMachine"));
			criteria.AddCriterion(new Criterion("ApplicationDomain", "MyDomain"));

			MockRepository mocks = new MockRepository();
			IPersistentObjectRepository objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				objectRepository.ConnectionStringKey = "TarantinoWebManagementConnectionString";
				Expect.Call(objectRepository.FindFirst<ApplicationInstance>(criteria)).Return(instance);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceRepository repository = new ApplicationInstanceRepository(objectRepository);
				ApplicationInstance actualInstance = repository.GetByDomainAndMachineName("MyDomain", "MyMachine");

				Assert.That(actualInstance, Is.SameAs(instance));
			}
		}

		[Test]
		public void Can_correctly_gets_application_instances_by_host_header()
		{
			ApplicationInstance instance1 = new ApplicationInstance();
			ApplicationInstance instance2 = new ApplicationInstance();
			ApplicationInstance[] instances = new ApplicationInstance[] { instance1, instance2 };

			CriterionSet criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion("UniqueHostHeader", "MyHostHeader"));

			MockRepository mocks = new MockRepository();
			IPersistentObjectRepository objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				objectRepository.ConnectionStringKey = "TarantinoWebManagementConnectionString";
				Expect.Call(objectRepository.FindAll<ApplicationInstance>(criteria)).Return(instances);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceRepository repository = new ApplicationInstanceRepository(objectRepository);
				IEnumerable<ApplicationInstance> actualInstances = repository.GetByHostHeader("MyHostHeader");

				EnumerableAssert.That(actualInstances, Is.EqualTo(instances));
			}
		}
	}
}