using NHibernate;
using NHibernate.Cfg;
using StructureMap;
using Tarantino.Core;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	[PluginFamily(ServiceKeys.Default, IsSingleton = true)]
	public interface ISessionFactoryManager
	{
		ISessionFactory GetSessionFactory();
		bool ReadyToGo();

		Configuration GetConfiguration();
	}
}