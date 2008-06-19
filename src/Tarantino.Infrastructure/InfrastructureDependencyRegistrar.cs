using StructureMap;
using Tarantino.Core;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.Commons.Services.Web.Impl;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.Deployer.Services;
using Tarantino.Core.WebManagement.Services.Repositories;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using Tarantino.Infrastructure.Commons.DataAccess.Repositories;
using Tarantino.Infrastructure.Commons.UI.Services;
using Tarantino.Infrastructure.DatabaseManager.DataAccess;
using Tarantino.Infrastructure.Deployer.DataAccess.Repositories;
using Tarantino.Infrastructure.WebManagement.DataAccess.Repositories;
using ServiceLocator=Tarantino.Infrastructure.Commons.Services.Configuration.ServiceLocator;

namespace Tarantino.Infrastructure
{
	public class InfrastructureDependencyRegistrar
	{
		public static void RegisterInfrastructure()
		{
			CoreDependencyRegistrar.Register();

			StructureMapConfiguration.BuildInstancesOf<IServiceLocator>().TheDefaultIsConcreteType<ServiceLocator>();
			StructureMapConfiguration.BuildInstancesOf<IQueryExecutor>().TheDefaultIsConcreteType<QueryExecutor>();
			StructureMapConfiguration.BuildInstancesOf<IMailSender>().TheDefaultIsConcreteType<SmtpMailSender>();
			StructureMapConfiguration.BuildInstancesOf<IWebContext>().TheDefaultIsConcreteType<WebContext>();
			StructureMapConfiguration.BuildInstancesOf<IWebDataReader>().TheDefaultIsConcreteType<WebDataReader>();
			StructureMapConfiguration.BuildInstancesOf<IPersistentObjectRepository>().TheDefaultIsConcreteType<PersistentObjectRepository>();
			StructureMapConfiguration.BuildInstancesOf<IAuthenticationService>().TheDefaultIsConcreteType<AuthenticationService>();
			StructureMapConfiguration.BuildInstancesOf<IDeploymentRepository>().TheDefaultIsConcreteType<DeploymentRepository>();
			StructureMapConfiguration.BuildInstancesOf<IApplicationInstanceRepository>().TheDefaultIsConcreteType<ApplicationInstanceRepository>();
			StructureMapConfiguration.BuildInstancesOf<IForgottenPasswordMailer>().TheDefaultIsConcreteType<ForgottenPasswordMailer>();
			StructureMapConfiguration.BuildInstancesOf<IForgottenPasswordMailFactory>().TheDefaultIsConcreteType<ForgottenPasswordMailFactory>();
			StructureMapConfiguration.BuildInstancesOf<IForgottenPasswordService>().TheDefaultIsConcreteType<ForgottenPasswordService>();
			StructureMapConfiguration.BuildInstancesOf<ILoginService>().TheDefaultIsConcreteType<LoginService>();

			StructureMapConfiguration.BuildInstancesOf<ISessionBuilder>().TheDefaultIsConcreteType<HybridSessionBuilder>();
		}
	}
}