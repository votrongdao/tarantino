using System;
using System.Collections.Generic;
using Tarantino.Core.Deployer.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace Tarantino.IntegrationTests.Infrastructure.Deployer.DataAccess
{
	public abstract class DeployerDatabaseTester : DatabaseTesterBase
	{
		protected override IEnumerable<string> GetTablesToDelete()
		{
			return new string[]{"Deployment"};
		}

		protected override Type GetEntityType()
		{
			return typeof(Deployment);
		}

		protected override string ConnectionStringKey
		{
			get { return NHibernateObjectMapper.DefaultConnectionStringKey; }
		}
	}
}