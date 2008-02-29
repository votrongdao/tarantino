using System;
using StructureMap;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class ExceptionHandlingLoadBalanceStatusManager : IExceptionHandlingLoadBalanceStatusManager
	{
		private readonly ILoadBalanceStatusManager _manager;

		public ExceptionHandlingLoadBalanceStatusManager(ILoadBalanceStatusManager manager)
		{
			_manager = manager;
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

			return errorMessage;
		}
	}
}