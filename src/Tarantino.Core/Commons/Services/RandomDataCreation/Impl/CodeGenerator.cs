using System.Collections.Generic;
using Tarantino.Core.Commons.Services.RandomDataCreation;
using StructureMap;

namespace Tarantino.Core.Commons.Services.RandomDataCreation.Impl
{
	[Pluggable(Keys.Default)]
	public class CodeGenerator : ICodeGenerator
	{
		private readonly IRandomCharacterGenerator _characterGenerator;

		public CodeGenerator(IRandomCharacterGenerator characterGenerator)
		{
			_characterGenerator = characterGenerator;
		}

		public string GetRandomCode(int numberOfCharacters)
		{
			List<char> characters = new List<char>();
			
			for(int i = 0; i < numberOfCharacters; i++)
			{
				char randomCharacter = _characterGenerator.GetRandomCharacter();
				characters.Add(randomCharacter);
			}

			string randomCode = new string(characters.ToArray());
			return randomCode;
		}
	}
}