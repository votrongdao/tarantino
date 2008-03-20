using NHibernate;
using NHibernate.Cfg;
using StructureMap;
using Tarantino.Core;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	[PluginFamily(Keys.Default, IsSingleton = true)]
	public interface ISessionFactoryManager
	{
		ISessionFactory GetSessionFactory(string connectionStringKey);
	}
}