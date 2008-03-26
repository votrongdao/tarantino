using StructureMap;
using Tarantino.Core;

namespace Tarantino.Core.Commons.Services.RandomDataCreation
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ICodeGenerator
	{
		string GetRandomCode(int numberOfCharacters);
	}
}