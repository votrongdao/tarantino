using NHibernate;
using StructureMap;
using Tarantino.Core;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ISessionScoper
	{
		bool CanHandle();
		ISession GetScopedSession();
		void Reset();
	}
}