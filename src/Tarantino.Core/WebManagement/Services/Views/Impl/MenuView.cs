using StructureMap;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.Environment;

namespace Tarantino.Core.WebManagement.Services.Views.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class MenuView : IMenuView
	{
		private readonly IResourceFileLocator _resourceFileLocator;
		private readonly ITokenReplacer _replacer;

		public MenuView(IResourceFileLocator resourceFileLocator, ITokenReplacer replacer)
		{
			_resourceFileLocator = resourceFileLocator;
			_replacer = replacer;
		}

		public string BuildHtml()
		{
			string template = _resourceFileLocator.ReadTextFile("Tarantino.Core", "Tarantino.Core.WebManagement.Services.Views.Resources.MenuTemplate.html");

			_replacer.Text = template;

			_replacer.Replace("APPLICATION_URL", "Tarantino.WebManagement.Application.axd");
			_replacer.Replace("CACHE_URL", "Tarantino.WebManagement.Cache.axd");
			_replacer.Replace("ASSEMBLY_URL", "Tarantino.WebManagement.Assemblies.axd");
			_replacer.Replace("LOADBALANCER_URL", "Tarantino.WebManagement.LoadBalancer.axd");
			_replacer.Replace("DISABLE_URL", "Tarantino.WebManagement.DisableSSL.axd");

			return _replacer.Text;
		}
	}
}