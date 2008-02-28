using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class ApplicationInstanceFactoryTester
	{
		[Test]
		public void Creates_new_application_instance()
		{
			MockRepository mocks = new MockRepository();
			ISystemEnvironment systemEnvironment = mocks.CreateMock<ISystemEnvironment>();
			IApplicationDomain domain = mocks.CreateMock<IApplicationDomain>();
			IAssemblyContext context = mocks.CreateMock<IAssemblyContext>();

			using (mocks.Record())
			{
				Expect.Call(context.GetAssemblyVersion()).Return("1.0");
				Expect.Call(systemEnvironment.GetMachineName()).Return("MyMachine");
				Expect.Call(domain.GetName()).Return("/LM/W3SVC/1/Root");
			}

			using (mocks.Playback())
			{
				IApplicationInstanceFactory factory = new ApplicationInstanceFactory(systemEnvironment, domain, context);
				ApplicationInstance instance = factory.Create();
				
				Assert.That(instance.AvailableForLoadBalancing, Is.True);
				Assert.That(instance.MachineName, Is.EqualTo("MyMachine"));
				Assert.That(instance.ApplicationDomain, Is.EqualTo("/LM/W3SVC/1/Root"));
				Assert.That(instance.Version, Is.EqualTo("1.0"));
			}

			mocks.VerifyAll();
		}
	}
}