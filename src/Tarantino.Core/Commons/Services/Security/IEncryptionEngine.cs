using StructureMap;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(Keys.Default)]
	public interface IEncryptionEngine
	{
		string Encrypt(string input);
		string Decrypt(string input);
	}
}