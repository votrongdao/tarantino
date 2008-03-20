using StructureMap;
using Tarantino.Core;

namespace Tarantino.Core.Commons.Services.RandomDataCreation
{
	[PluginFamily(Keys.Default)]
	public interface ICodeGenerator
	{
		string GetRandomCode(int numberOfCharacters);
	}
}