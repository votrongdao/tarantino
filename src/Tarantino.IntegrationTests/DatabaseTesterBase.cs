using System.Collections.Generic;
using NUnit.Framework;
using Tarantino.Core.Commons.Model;
using Tarantino.Infrastructure;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using Tarantino.Infrastructure.Commons.DataAccess.Repositories;

namespace Tarantino.IntegrationTests
{
	public abstract class DatabaseTesterBase : RepositoryBase
	{
		protected DatabaseTesterBase() : base(new HybridSessionBuilder())
		{
		}

		[SetUp]
		public virtual void SetUp()
		{
			InfrastructureDependencyRegistrar.RegisterInfrastructure();
			ClearTables();
			SetupDatabase();
		}

		[TearDown]
		public virtual void Teardown()
		{
			HybridSessionBuilder.ResetSession(ConfigurationFile);
		}

		protected void ClearTables()
		{
			var session = GetSession();

			foreach (var table in GetTablesToDelete())
			{
				var hql = string.Format("from {0}", table);
				session.Delete(hql);
			}

			session.Flush();

			HybridSessionBuilder.ResetSession(ConfigurationFile);
		}

		protected void Save(params PersistentObject[] objects)
		{
			var session = GetSession();

			foreach (var persistentObject in objects)
			{
				session.SaveOrUpdate(persistentObject);
			}

			session.Flush();
		}

		protected virtual void SetupDatabase()
		{
		}

		protected abstract IEnumerable<string> GetTablesToDelete();
	}
}