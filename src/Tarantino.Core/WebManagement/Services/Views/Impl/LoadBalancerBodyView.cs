using StructureMap;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Impl;
using Tarantino.Core.WebManagement.Services.Views;

namespace Tarantino.Core.WebManagement.Services.Views.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class LoadBalancerBodyView : ILoadBalancerBodyView
	{
		private const string _changeStateLinkTemplate = "<a href=\"?{0}={1}\">{2}</a>";

		private readonly IApplicationInstanceContext _context;
		private readonly IResourceFileLocator _fileLocator;
		private readonly ITokenReplacer _replacer;
		private readonly IAdministratorSecurityChecker _securityChecker;

		public LoadBalancerBodyView(IApplicationInstanceContext context, IResourceFileLocator fileLocator, ITokenReplacer replacer, IAdministratorSecurityChecker securityChecker)
		{
			_context = context;
			_fileLocator = fileLocator;
			_replacer = replacer;
			_securityChecker = securityChecker;
		}

		public string BuildHtml(string errorMessage)
		{
			ApplicationInstance instance = _context.GetCurrent();

			string template = _fileLocator.ReadTextFile("Tarantino.Core", "Tarantino.Core.WebManagement.Services.Views.Resources.LoadBalancerBodyTemplate.html");
			_replacer.Text = template;

			if (errorMessage.Length>0)
				_replacer.Replace("ERROR_MESSAGE", errorMessage);

			_replacer.Replace("CURRENT_STATE", instance.AvailableForLoadBalancing ? "enabled" : "disabled");
			_replacer.Replace("MACHINE", instance.MachineName);

			if (_securityChecker.IsCurrentUserAdministrator())
			{
				bool newState = !instance.AvailableForLoadBalancing;
				string newStateLabel = newState ? "enable" : "disable";

				_replacer.Replace("CHANGE_STATE_LINK", string.Format(_changeStateLinkTemplate, LoadBalanceStatusManager.ENABLED_PARAM, newState, newStateLabel));
			}

			return _replacer.Text;
		}
	}
}