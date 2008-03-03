using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;
using Tarantino.Core.WebManagement.Services.Views;

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
			ILoadBalancerView view = mocks.CreateMock<ILoadBalancerView>();

			using (mocks.Record())
			{
				Expect.Call(manager.HandleLoadBalanceRequest()).Return("My error message");
				view.Render("My error message");
			}

			using (mocks.Playback())
			{
				IExceptionHandlingLoadBalanceStatusManager statusManager = new ExceptionHandlingLoadBalanceStatusManager(manager, view);
				Assert.That(statusManager.HandleLoadBalancing(), Is.EqualTo("My error message"));
			}
		}

		[Test]
		public void Correctly_swallows_exception_and_returns_error_message()
		{
			MockRepository mocks = new MockRepository();
			ILoadBalanceStatusManager manager = mocks.CreateMock<ILoadBalanceStatusManager>();
			ILoadBalancerView view = mocks.CreateMock<ILoadBalancerView>();
			Exception exception = mocks.PartialMock<Exception>();

			using (mocks.Record())
			{
				SetupResult.For(exception.ToString()).Return("My exception message");
				Expect.Call(manager.HandleLoadBalanceRequest()).Throw(exception);
				view.Render("My exception message");
			}

			using (mocks.Playback())
			{
				IExceptionHandlingLoadBalanceStatusManager statusManager = new ExceptionHandlingLoadBalanceStatusManager(manager, view);
				Assert.That(statusManager.HandleLoadBalancing(), Is.EqualTo("My exception message"));
			}
		}
	}
}