using System.Collections;
using System.Data;
using System.Reflection;
using Tarantino.Core;
using Tarantino.Core.Commons.Services.Caching;
using Tarantino.Core.Commons.Services.Configuration;
using NHibernate;
using NHibernate.Cfg;
using StructureMap;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	[Pluggable(ServiceKeys.Default)]
	public class SessionFactoryManager : ISessionFactoryManager
	{
		private readonly IApplicationSettings _applicationSettings;
		private readonly IApplicationConfiguration _applicationConfiguration;
		private readonly ICacheManager _cacheManager;

		public SessionFactoryManager(IApplicationSettings applicationSettings, IApplicationConfiguration applicationConfiguration, ICacheManager cacheManager)
		{
			_applicationSettings = applicationSettings;
			_applicationConfiguration = applicationConfiguration;
			_cacheManager = cacheManager;
		}

		public ISessionFactory GetSessionFactory(string connectionStringKey)
		{
			ISessionFactory sessionFactory = _cacheManager.Get<ISessionFactory>(connectionStringKey);

			if (sessionFactory == null)
			{
				Configuration configuration = new Configuration();

				IDictionary properties = new Hashtable();

				properties["hibernate.connection.provider"] = "NHibernate.Connection.DriverConnectionProvider";
				properties["hibernate.dialect"] = "NHibernate.Dialect.MsSql2000Dialect";
				properties["hibernate.connection.driver_class"] = "NHibernate.Driver.SqlClientDriver";
				properties["hibernate.query.substitutions"] = "true='Y', false='N'";
				properties["hibernate.connection.connection_string"] = _applicationConfiguration.GetConnectionString(connectionStringKey);
				properties["hibernate.connection.isolation"] = IsolationLevel.ReadCommitted;
				properties["hibernate.cache.provider_class"] = "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache";
				properties["relativeExpiration"] = 60;
				properties["hibernate.cache.use_query_cache"] = false;
				properties["hibernate.show_sql"] = _applicationSettings.GetShowSql();
				properties["hibernate.use_reflection_optimizer"] = true;

				foreach (DictionaryEntry entry in properties)
				{
					string key = entry.Key.ToString();
					string value = entry.Value.ToString();
					configuration.SetProperty(key, value);
				}

				foreach (string mappingAssembly in _applicationSettings.GetMappingAssemblies())
				{
					Assembly assembly = Assembly.Load(mappingAssembly);
					configuration.AddAssembly(assembly);
				}

				sessionFactory = configuration.BuildSessionFactory();

				_cacheManager.Set(connectionStringKey, sessionFactory);
			}

			return sessionFactory;
		}
	}
}