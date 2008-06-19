using System.Collections.Generic;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using Tarantino.Core.Commons.Services.Logging;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	public class HybridSessionBuilder : ISessionBuilder
	{
		private static readonly Dictionary<string, ISessionFactory> _sessionFactories = new Dictionary<string, ISessionFactory>();
		private static readonly Dictionary<string, ISession> _currentSessions = new Dictionary<string, ISession>();

		public ISession GetSession()
		{
			return GetSession("hibernate.cfg.xml");
		}

		public Configuration GetConfiguration()
		{
			return GetConfiguration("hibernate.cfg.xml");
		}

		public ISession GetSession(string configurationFile)
		{
			var factory = getSessionFactory(configurationFile);
			var session = getExistingOrNewSession(factory, configurationFile);
			Logger.Debug(this, string.Format("Using ISession {0}", session.GetHashCode()));
			return session;
		}

		public Configuration GetConfiguration(string configurationFile)
		{
			var configuration = new Configuration();
			configuration.Configure(configurationFile);
			return configuration;
		}

		public ISession GetExistingWebSession()
		{
			return GetExistingWebSession("hibernate.cfg.xml");
		}

		public ISession GetExistingWebSession(string configurationFile)
		{
			return HttpContext.Current.Items[configurationFile] as ISession;
		}

		public static void ResetSession()
		{
			var builder = new HybridSessionBuilder();
			builder.GetSession().Dispose();
		}

		public static void ResetSession(string configurationFile)
		{
			var builder = new HybridSessionBuilder();
			builder.GetSession(configurationFile).Dispose();
		}

		private ISessionFactory getSessionFactory(string configurationFile)
		{
			if (!_sessionFactories.ContainsKey(configurationFile))
			{
				var configuration = GetConfiguration(configurationFile);
				_sessionFactories[configurationFile] = configuration.BuildSessionFactory();
			}

			return _sessionFactories[configurationFile];
		}

		private ISession getExistingOrNewSession(ISessionFactory factory, string configurationFile)
		{
			if (HttpContext.Current != null)
			{
				var session = GetExistingWebSession();

				if (session == null || !session.IsOpen)
				{
					session = openSessionAndAddToContext(factory, configurationFile);
				}

				return session;
			}

			var currentSession = _currentSessions.ContainsKey(configurationFile) ? 
			                                                                     	_currentSessions[configurationFile] : null;

			if (currentSession == null || !currentSession.IsOpen)
			{
				_currentSessions[configurationFile] = factory.OpenSession();
			}

			return _currentSessions[configurationFile];
		}

		private ISession openSessionAndAddToContext(ISessionFactory factory, string configurationFile)
		{
			var session = factory.OpenSession();
			HttpContext.Current.Items.Remove(configurationFile);
			HttpContext.Current.Items.Add(configurationFile, session);
			return session;
		}
	}
}