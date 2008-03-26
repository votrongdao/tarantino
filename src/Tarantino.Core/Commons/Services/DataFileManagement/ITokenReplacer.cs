using StructureMap;

namespace Tarantino.Core.Commons.Services.DataFileManagement
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ITokenReplacer
	{
		void Replace(string token, string tokenValue);
		string Text { get; set; }
	}
}