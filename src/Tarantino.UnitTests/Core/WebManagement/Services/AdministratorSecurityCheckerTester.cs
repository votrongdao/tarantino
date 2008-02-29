using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class AdministratorSecurityCheckerTester
	{
		[Test]
		public void Does_not_validate_non_authenticated_administrator()
		{
			MockRepository mocks = new MockRepository();
			IWebContext context = mocks.CreateMock<IWebContext>();
			IRoleManager roleManager = mocks.CreateMock<IRoleManager>();

			using (mocks.Record())
			{
				Expect.Call(context.UserIsAuthenticated()).Return(false);
				Expect.Call(roleManager.IsCurrentUserInAtLeastOneRole(@"BUILTIN\Administrators", "Administrators")).Return(true);
			}

			using (mocks.Playback())
			{
				IAdministratorSecurityChecker checker = new AdministratorSecurityChecker(context, roleManager);
				Assert.That(checker.IsCurrentUserAdministrator(), Is.False);
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Does_not_validate_authenticated_non_administrator()
		{
			MockRepository mocks = new MockRepository();
			IWebContext context = mocks.CreateMock<IWebContext>();
			IRoleManager roleManager = mocks.CreateMock<IRoleManager>();

			using (mocks.Record())
			{
				Expect.Call(context.UserIsAuthenticated()).Return(true);
				Expect.Call(roleManager.IsCurrentUserInAtLeastOneRole(@"BUILTIN\Administrators", "Administrators")).Return(false);
			}

			using (mocks.Playback())
			{
				IAdministratorSecurityChecker checker = new AdministratorSecurityChecker(context, roleManager);
				Assert.That(checker.IsCurrentUserAdministrator(), Is.False);
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Validates_authenticated_administrator()
		{
			MockRepository mocks = new MockRepository();
			IWebContext context = mocks.CreateMock<IWebContext>();
			IRoleManager roleManager = mocks.CreateMock<IRoleManager>();

			using (mocks.Record())
			{
				Expect.Call(context.UserIsAuthenticated()).Return(true);
				Expect.Call(roleManager.IsCurrentUserInAtLeastOneRole(@"BUILTIN\Administrators", "Administrators")).Return(true);
			}

			using (mocks.Playback())
			{
				IAdministratorSecurityChecker checker = new AdministratorSecurityChecker(context, roleManager);
				Assert.That(checker.IsCurrentUserAdministrator(), Is.True);
			}

			mocks.VerifyAll();
		}
	}
}