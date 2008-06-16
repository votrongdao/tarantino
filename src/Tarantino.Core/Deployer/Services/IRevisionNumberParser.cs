

namespace Tarantino.Core.Deployer.Services
{
	
	public interface IRevisionNumberParser
	{
		int Parse(string output);
	}
}