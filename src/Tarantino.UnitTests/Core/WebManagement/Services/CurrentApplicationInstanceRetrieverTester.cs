using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class CurrentApplicationInstanceRetrieverTester
	{
		[Test]
		public void Correctly_finds_existing_application_instance_in_database()
		{
			ApplicationInstance instance = new ApplicationInstance();

			MockRepository mocks = new MockRepository();
			ISystemEnvironment environment = mocks.CreateMock<ISystemEnvironment>();
			IApplicationDomain domain = mocks.CreateMock<IApplicationDomain>();
			IApplicationInstanceRepository repository = mocks.CreateMock<IApplicationInstanceRepository>();

			using (mocks.Record())
			{
				Expect.Call(environment.GetMachineName()).Return("MyMachine");
				Expect.Call(domain.GetName()).Return("/LM/W3SVC/1/Root");
				Expect.Call(repository.GetByDomainAndMachineName("/LM/W3SVC/1/Root", "MyMachine")).Return(instance);
			}

			using (mocks.Playback())
			{
				ICurrentApplicationInstanceRetriever retriever = new CurrentApplicationInstanceRetriever(environment, domain, repository, null);
				Assert.That(retriever.GetApplicationInstance(), Is.SameAs(instance));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_creates_new_application_instance_if_existing_instance_does_not_exist()
		{
			ApplicationInstance instance = new ApplicationInstance();

			MockRepository mocks = new MockRepository();
			ISystemEnvironment environment = mocks.CreateMock<ISystemEnvironment>();
			IApplicationDomain domain = mocks.CreateMock<IApplicationDomain>();
			IApplicationInstanceRepository repository = mocks.CreateMock<IApplicationInstanceRepository>();
			IApplicationInstanceFactory factory = mocks.CreateMock<IApplicationInstanceFactory>();

			using (mocks.Record())
			{
				Expect.Call(environment.GetMachineName()).Return("MyMachine");
				Expect.Call(domain.GetName()).Return("/LM/W3SVC/1/Root");
				Expect.Call(repository.GetByDomainAndMachineName("/LM/W3SVC/1/Root", "MyMachine")).Return(null);
				Expect.Call(factory.Create()).Return(instance);
				repository.Save(instance);
			}

			using (mocks.Playback())
			{
				ICurrentApplicationInstanceRetriever retriever = new CurrentApplicationInstanceRetriever(environment, domain, repository, factory);
				Assert.That(retriever.GetApplicationInstance(), Is.SameAs(instance));
			}

			mocks.VerifyAll();
		}
	}
}