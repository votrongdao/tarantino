using StructureMap;
using Tarantino.Core;
using Tarantino.Core.Commons.Model;

namespace Tarantino.Core.Commons.Services.Security
{
	[PluginFamily(Keys.Default)]
	public interface ISystemUserContextManager
	{
		void SetUserContext();
		ISystemUser GetCurrentUser();
	}
}