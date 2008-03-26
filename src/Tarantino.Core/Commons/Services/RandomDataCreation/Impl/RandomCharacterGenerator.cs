using System;
using Tarantino.Core.Commons.Services.RandomDataCreation;
using StructureMap;

namespace Tarantino.Core.Commons.Services.RandomDataCreation.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class RandomCharacterGenerator : IRandomCharacterGenerator
	{
		private const int _lastLetter = 25;
		private int _1 = 49;
		private int _a = 97;

		private readonly IRandomNumberGenerator _numberGenerator;

		public RandomCharacterGenerator(IRandomNumberGenerator numberGenerator)
		{
			_numberGenerator = numberGenerator;
		}

		public char GetRandomCharacter()
		{
			int randomNumber = _numberGenerator.GenerateRandomNumber(34);

			bool isLetter = randomNumber <= _lastLetter;

			int characterIndex = isLetter ? randomNumber + _a : (randomNumber - 26) + _1;
			char randomCharacter = Convert.ToChar(characterIndex);

			return randomCharacter;
		}
	}
}