using StructureMap;

namespace Tarantino.Core.Commons.Services.RandomDataCreation
{
	[PluginFamily(Keys.Default)]
	public interface IRandomCharacterGenerator
	{
		char GetRandomCharacter();
	}
}