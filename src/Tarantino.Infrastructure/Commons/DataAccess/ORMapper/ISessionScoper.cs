using Tarantino.Commons.Core;
using NHibernate;
using StructureMap;

namespace Tarantino.Commons.Infrastructure.DataAccess.ORMapper
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ISessionScoper
	{
		bool CanHandle();
		ISession GetScopedSession();
		void Reset();
	}
}