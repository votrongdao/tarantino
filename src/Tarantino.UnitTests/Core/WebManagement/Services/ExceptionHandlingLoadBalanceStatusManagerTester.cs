using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class ExceptionHandlingLoadBalanceStatusManagerTester
	{
		[Test]
		public void Correctly_returns_error_message_from_load_balance_manager_when_no_exception_occurs()
		{
			MockRepository mocks = new MockRepository();
			ILoadBalanceStatusManager manager = mocks.CreateMock<ILoadBalanceStatusManager>();

			using (mocks.Record())
			{
				Expect.Call(manager.HandleLoadBalanceRequest()).Return("My error message");
			}

			using (mocks.Playback())
			{
				IExceptionHandlingLoadBalanceStatusManager statusManager = new ExceptionHandlingLoadBalanceStatusManager(manager);
				Assert.That(statusManager.HandleLoadBalancing(), Is.EqualTo("My error message"));
			}
		}

		[Test]
		public void Correctly_swallows_exception_and_returns_error_message()
		{
			MockRepository mocks = new MockRepository();
			ILoadBalanceStatusManager manager = mocks.CreateMock<ILoadBalanceStatusManager>();

			using (mocks.Record())
			{
				Expect.Call(manager.HandleLoadBalanceRequest()).Throw(new Exception("My exception message"));
			}

			using (mocks.Playback())
			{
				IExceptionHandlingLoadBalanceStatusManager statusManager = new ExceptionHandlingLoadBalanceStatusManager(manager);
				Assert.That(statusManager.HandleLoadBalancing().Contains("My exception message"));
			}
		}
	}
}