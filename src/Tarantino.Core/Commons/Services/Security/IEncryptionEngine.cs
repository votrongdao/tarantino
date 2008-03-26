using StructureMap;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IEncryptionEngine
	{
		string Encrypt(string input);
		string Decrypt(string input);
	}
}