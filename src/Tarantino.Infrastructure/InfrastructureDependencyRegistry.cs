using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.Commons.Services.Web.Impl;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace Tarantino.Infrastructure
{
	public class InfrastructureDependencyRegistry : Registry
	{
		protected override void configure()
		{
			Scan(y =>
			     	{
						y.TheCallingAssembly();
						y.WithDefaultConventions();
			     	});

			ForRequestedType<IDatabaseActionExecutor>()
				.AddInstances(y =>
				              	{
				              		y.OfConcreteType<DatabaseCreator>().Name = "Create";
				              		y.OfConcreteType<DatabaseDropper>().Name = "Drop";
				              		y.OfConcreteType<DatabaseUpdater>().Name = "Update";
				              	});

			ForRequestedType<IMailSender>().TheDefaultIsConcreteType<SmtpMailSender>();
			BuildInstancesOf<ISessionBuilder>().TheDefaultIsConcreteType<HybridSessionBuilder>();
		}
	}
}