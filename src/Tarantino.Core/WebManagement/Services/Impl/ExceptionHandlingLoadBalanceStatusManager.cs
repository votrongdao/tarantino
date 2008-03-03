using System;
using StructureMap;
using Tarantino.Core.WebManagement.Services.Views;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class ExceptionHandlingLoadBalanceStatusManager : IExceptionHandlingLoadBalanceStatusManager
	{
		private readonly ILoadBalanceStatusManager _manager;
		private readonly ILoadBalancerView _view;

		public ExceptionHandlingLoadBalanceStatusManager(ILoadBalanceStatusManager manager, ILoadBalancerView view)
		{
			_manager = manager;
			_view = view;
		}

		public string HandleLoadBalancing()
		{
			string errorMessage;
			try
			{
				errorMessage = _manager.HandleLoadBalanceRequest();
			}
			catch (Exception ex)
			{
				errorMessage = ex.ToString();
			}

			_view.Render(errorMessage);

			return errorMessage;
		}
	}
}