using System;
using Tarantino.Core.Commons.Services.RandomDataCreation;
using StructureMap;

namespace Tarantino.Core.Commons.Services.RandomDataCreation.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class RandomNumberGenerator : IRandomNumberGenerator
	{
		private Random _numberGenerator = new Random();

		public int GenerateRandomNumber(int maximumNumber)
		{
			int randomNumber = _numberGenerator.Next(0, maximumNumber + 1);

			return randomNumber;
		}
	}
}