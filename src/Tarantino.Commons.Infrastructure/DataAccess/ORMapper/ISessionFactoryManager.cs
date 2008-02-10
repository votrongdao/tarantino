using Tarantino.Commons.Core;
using NHibernate;
using NHibernate.Cfg;
using StructureMap;

namespace Tarantino.Commons.Infrastructure.DataAccess.ORMapper
{
	[PluginFamily(ServiceKeys.Default, IsSingleton = true)]
	public interface ISessionFactoryManager
	{
		ISessionFactory GetSessionFactory();
		bool ReadyToGo();

		Configuration GetConfiguration();
	}
}