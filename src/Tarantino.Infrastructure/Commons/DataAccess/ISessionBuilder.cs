using NHibernate;
using NHibernate.Cfg;

namespace Tarantino.Infrastructure.Commons.DataAccess
{
	public interface ISessionBuilder
	{
		ISession GetSession();
		Configuration GetConfiguration();
		ISession GetSession(string configurationFile);
		Configuration GetConfiguration(string configurationFile);
	}
}