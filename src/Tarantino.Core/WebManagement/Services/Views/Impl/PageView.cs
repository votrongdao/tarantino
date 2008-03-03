using StructureMap;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.Environment;

namespace Tarantino.Core.WebManagement.Services.Views.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class PageView : IPageView
	{
		private readonly IResourceFileLocator _fileLocator;
		private readonly IMenuView _menuView;
		private readonly ITokenReplacer _replacer;

		public PageView(IResourceFileLocator fileLocator, IMenuView menuView, ITokenReplacer replacer)
		{
			_fileLocator = fileLocator;
			_menuView = menuView;
			_replacer = replacer;
		}

		public string BuildHtml(string bodyHtml)
		{
			string pageTemplate = _fileLocator.ReadTextFile("Tarantino.Core", "Tarantino.Core.WebManagement.Services.Views.Resources.PageTemplate.html");
			string cssHtml = _fileLocator.ReadTextFile("Tarantino.Core", "Tarantino.Core.WebManagement.Services.Views.Resources.StyleSheet.css");
			string menuHtml = _menuView.BuildHtml();

			_replacer.Text = pageTemplate;

			_replacer.Replace("CSS", cssHtml);
			_replacer.Replace("MENU", menuHtml);
			_replacer.Replace("BODY", bodyHtml);

			return _replacer.Text;
		}
	}
}