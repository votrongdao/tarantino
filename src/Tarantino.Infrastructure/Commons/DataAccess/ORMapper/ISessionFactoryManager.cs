using NHibernate;
using StructureMap;
using Tarantino.Core;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ISessionFactoryManager
	{
		ISessionFactory GetSessionFactory(string connectionStringKey);
	}
}