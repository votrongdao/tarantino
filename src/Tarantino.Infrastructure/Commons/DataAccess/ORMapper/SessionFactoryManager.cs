using System.Collections;
using System.Data;
using System.Reflection;
using Tarantino.Core;
using Tarantino.Core.Commons.Services.Configuration;
using NHibernate;
using NHibernate.Cfg;
using StructureMap;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	[Pluggable(ServiceKeys.Default)]
	public class SessionFactoryManager : ISessionFactoryManager
	{
		protected ISessionFactory _factory;
		protected Configuration _getConfiguration;

		private readonly IApplicationSettings _applicationSettings;

		public SessionFactoryManager(IApplicationSettings applicationSettings)
		{
			_applicationSettings = applicationSettings;
		}

		public Configuration GetConfiguration()
		{
			initializeSessionFactory();
			return _getConfiguration;
		}

		public ISessionFactory GetSessionFactory()
		{
			initializeSessionFactory();
			return _factory;
		}

		public bool ReadyToGo()
		{
			return (_factory != null);
		}

		private void initializeSessionFactory()
		{
			if (_factory != null) return; //only need one ISessionFactory per appdomain.
			_getConfiguration = new Configuration();
			IDictionary properties = new Hashtable();

			properties["hibernate.connection.provider"] = "NHibernate.Connection.DriverConnectionProvider";
			properties["hibernate.dialect"] = "NHibernate.Dialect.MsSql2000Dialect";
			properties["hibernate.connection.driver_class"] = "NHibernate.Driver.SqlClientDriver";
			properties["hibernate.query.substitutions"] = "true='Y', false='N'";
			properties["hibernate.connection.connection_string"] = _applicationSettings.GetConnectionString();
			properties["hibernate.connection.isolation"] = IsolationLevel.ReadCommitted;
			properties["hibernate.cache.provider_class"] = "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache";
			properties["relativeExpiration"] = 60;
			properties["hibernate.cache.use_query_cache"] = false;
			properties["hibernate.show_sql"] = _applicationSettings.GetShowSql();
			properties["hibernate.use_reflection_optimizer"] = true;

			foreach (DictionaryEntry entry in properties)
			{
				_getConfiguration.SetProperty(entry.Key.ToString(), entry.Value.ToString());
			}

			foreach (string mappingAssembly in _applicationSettings.GetMappingAssemblies())
			{
				Assembly assembly = Assembly.Load(mappingAssembly);
				_getConfiguration.AddAssembly(assembly);
			}

			_factory = _getConfiguration.BuildSessionFactory();
		}
	}
}