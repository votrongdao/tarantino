using NHibernate;
using NHibernate.Cfg;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	public interface ISessionBuilder
	{
		ISession GetSession();
		Configuration GetConfiguration();
		ISession GetSession(string configurationFile);
		Configuration GetConfiguration(string configurationFile);
		ISession GetExistingWebSession(string configurationFile);
		ISession GetExistingWebSession();
	}
}