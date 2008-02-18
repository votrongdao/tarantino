using StructureMap;

namespace Tarantino.Core.Commons.Services.RandomDataCreation
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IRandomCharacterGenerator
	{
		char GetRandomCharacter();
	}
}