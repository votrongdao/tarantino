using StructureMap;

namespace Tarantino.Core.Commons.Services.RandomDataCreation
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IRandomNumberGenerator
	{
		int GenerateRandomNumber(int maximumNumber);
	}
}