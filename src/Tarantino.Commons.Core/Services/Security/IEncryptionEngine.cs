using StructureMap;

namespace Tarantino.Commons.Core.Services.Security
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IEncryptionEngine
	{
		string Encrypt(string input);
		string Decrypt(string input);
	}
}