using StructureMap;

namespace Tarantino.Core.Commons.Services.Web
{
	[PluginFamily(Keys.Default)]
	public interface IWebDataReader
	{
		string ReadUrl(string url, string parameterName, string parameterValue);
		string ReadUrl(string url);
	}
}