using StructureMap;

namespace Tarantino.Core.Commons.Services.RandomDataCreation
{
	[PluginFamily(Keys.Default)]
	public interface IRandomNumberGenerator
	{
		int GenerateRandomNumber(int maximumNumber);
	}
}