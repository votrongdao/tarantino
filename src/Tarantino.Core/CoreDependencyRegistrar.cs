using StructureMap;
using Tarantino.Core.Commons.Services.Caching;
using Tarantino.Core.Commons.Services.Caching.Impl;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Configuration.Impl;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.DataFileManagement.Impl;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Environment.Impl;
using Tarantino.Core.Commons.Services.ListManagement;
using Tarantino.Core.Commons.Services.ListManagement.Impl;
using Tarantino.Core.Commons.Services.RandomDataCreation;
using Tarantino.Core.Commons.Services.RandomDataCreation.Impl;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;
using Tarantino.Core.Daemon.Services;
using Tarantino.Core.Daemon.Services.Impl;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.Core.Deployer.Services;
using Tarantino.Core.Deployer.Services.Configuration;
using Tarantino.Core.Deployer.Services.Configuration.Impl;
using Tarantino.Core.Deployer.Services.Impl;
using Tarantino.Core.Deployer.Services.UI;
using Tarantino.Core.Deployer.Services.UI.Impl;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;
using Tarantino.Core.WebManagement.Services.Views;
using Tarantino.Core.WebManagement.Services.Views.Impl;

namespace Tarantino.Core
{
	public class CoreDependencyRegistrar
	{
		public static void Register()
		{
			StructureMapConfiguration.ResetAll();

			StructureMapConfiguration.UseDefaultStructureMapConfigFile = false;

			StructureMapConfiguration.BuildInstancesOf<ICacheManager>().TheDefaultIsConcreteType<CacheManager>();
			StructureMapConfiguration.BuildInstancesOf<IEncryptionEngine>().TheDefaultIsConcreteType<AesEncryptionEngine>();
			StructureMapConfiguration.BuildInstancesOf<IApplicationConfiguration>().TheDefaultIsConcreteType<ApplicationConfiguration>();
			StructureMapConfiguration.BuildInstancesOf<IApplicationSettings>().TheDefaultIsConcreteType<ApplicationSettings>();
			StructureMapConfiguration.BuildInstancesOf<IConfigurationReader>().TheDefaultIsConcreteType<ConfigurationReader>();
			StructureMapConfiguration.BuildInstancesOf<IDataFileReader>().TheDefaultIsConcreteType<DataFileReader>();
			StructureMapConfiguration.BuildInstancesOf<IDataTableReader>().TheDefaultIsConcreteType<DataTableReader>();
			StructureMapConfiguration.BuildInstancesOf<IExcelWorkbookReader>().TheDefaultIsConcreteType<ExcelWorkbookReader>();
			StructureMapConfiguration.BuildInstancesOf<IExcelWorksheetReader>().TheDefaultIsConcreteType<ExcelWorksheetReader>();
			StructureMapConfiguration.BuildInstancesOf<ITokenReplacer>().TheDefaultIsConcreteType<TokenReplacer>();
			StructureMapConfiguration.BuildInstancesOf<IApplicationDomain>().TheDefaultIsConcreteType<ApplicationDomain>();
			StructureMapConfiguration.BuildInstancesOf<IAssemblyContext>().TheDefaultIsConcreteType<AssemblyContext>();
			StructureMapConfiguration.BuildInstancesOf<IDateContext>().TheDefaultIsConcreteType<DateContext>();
			StructureMapConfiguration.BuildInstancesOf<IFileStreamFactory>().TheDefaultIsConcreteType<FileStreamFactory>();
			StructureMapConfiguration.BuildInstancesOf<IFileSystem>().TheDefaultIsConcreteType<FileSystem>();
			StructureMapConfiguration.BuildInstancesOf<IResourceFileLocator>().TheDefaultIsConcreteType<ResourceFileLocator>();
			StructureMapConfiguration.BuildInstancesOf<ISystemClock>().TheDefaultIsConcreteType<SystemClock>();
			StructureMapConfiguration.BuildInstancesOf<ISystemEnvironment>().TheDefaultIsConcreteType<SystemEnvironment>();
			StructureMapConfiguration.BuildInstancesOf<ITypeActivator>().TheDefaultIsConcreteType<TypeActivator>();
			StructureMapConfiguration.BuildInstancesOf<IEnumerationHelper>().TheDefaultIsConcreteType<EnumerationHelper>();
			StructureMapConfiguration.BuildInstancesOf<ICodeGenerator>().TheDefaultIsConcreteType<CodeGenerator>();
			StructureMapConfiguration.BuildInstancesOf<IRandomCharacterGenerator>().TheDefaultIsConcreteType<RandomCharacterGenerator>();
			StructureMapConfiguration.BuildInstancesOf<IRandomNumberGenerator>().TheDefaultIsConcreteType<RandomNumberGenerator>();
			StructureMapConfiguration.BuildInstancesOf<ILoginChecker>().TheDefaultIsConcreteType<LoginChecker>();
			StructureMapConfiguration.BuildInstancesOf<IPrincipalFactory>().TheDefaultIsConcreteType<PrincipalFactory>();
			StructureMapConfiguration.BuildInstancesOf<IRoleManager>().TheDefaultIsConcreteType<RoleManager>();
			StructureMapConfiguration.BuildInstancesOf<ISecurityContext>().TheDefaultIsConcreteType<SecurityContext>();
			StructureMapConfiguration.BuildInstancesOf<ISystemUserContextManager>().TheDefaultIsConcreteType<SystemUserContextManager>();
			StructureMapConfiguration.BuildInstancesOf<IWindowsIdentity>().TheDefaultIsConcreteType<WindowsIdentity>();
			StructureMapConfiguration.BuildInstancesOf<IServiceAgentAggregator>().TheDefaultIsConcreteType<ServiceAgentAggregator>();
			StructureMapConfiguration.BuildInstancesOf<IServiceRunner>().TheDefaultIsConcreteType<ServiceRunner>();
			StructureMapConfiguration.BuildInstancesOf<IChangeScriptExecutor>().TheDefaultIsConcreteType<ChangeScriptExecutor>();
			StructureMapConfiguration.BuildInstancesOf<IConnectionStringGenerator>().TheDefaultIsConcreteType<ConnectionStringGenerator>();
			StructureMapConfiguration.BuildInstancesOf<IDatabaseActionResolver>().TheDefaultIsConcreteType<DatabaseActionResolver>();
			StructureMapConfiguration.BuildInstancesOf<IDatabaseConnectionDropper>().TheDefaultIsConcreteType<DatabaseConnectionDropper>();
			StructureMapConfiguration.BuildInstancesOf<IDatabaseVersioner>().TheDefaultIsConcreteType<DatabaseVersioner>();
			StructureMapConfiguration.BuildInstancesOf<ILogMessageGenerator>().TheDefaultIsConcreteType<LogMessageGenerator>();
			StructureMapConfiguration.BuildInstancesOf<ISchemaInitializer>().TheDefaultIsConcreteType<SchemaInitializer>();
			StructureMapConfiguration.BuildInstancesOf<IScriptExecutionTracker>().TheDefaultIsConcreteType<ScriptExecutionTracker>();
			StructureMapConfiguration.BuildInstancesOf<IScriptFolderExecutor>().TheDefaultIsConcreteType<ScriptFolderExecutor>();
			StructureMapConfiguration.BuildInstancesOf<ISqlDatabaseManager>().TheDefaultIsConcreteType<SqlDatabaseManager>();
			StructureMapConfiguration.BuildInstancesOf<ISqlFileLocator>().TheDefaultIsConcreteType<SqlFileLocator>();
			StructureMapConfiguration.BuildInstancesOf<IApplicationRepository>().TheDefaultIsConcreteType<ApplicationRepository>();
			StructureMapConfiguration.BuildInstancesOf<IDeploymentFactory>().TheDefaultIsConcreteType<DeploymentFactory>();
			StructureMapConfiguration.BuildInstancesOf<IDeploymentRecorder>().TheDefaultIsConcreteType<DeploymentRecorder>();
			StructureMapConfiguration.BuildInstancesOf<IDeploymentResultCalculator>().TheDefaultIsConcreteType<DeploymentResultCalculator>();
			StructureMapConfiguration.BuildInstancesOf<IRevisionCertifier>().TheDefaultIsConcreteType<RevisionCertifier>();
			StructureMapConfiguration.BuildInstancesOf<IRevisionNumberParser>().TheDefaultIsConcreteType<RevisionNumberParser>();
			StructureMapConfiguration.BuildInstancesOf<IDeploymentFormValidator>().TheDefaultIsConcreteType<DeploymentFormValidator>();
			StructureMapConfiguration.BuildInstancesOf<IDeploymentRowFactory>().TheDefaultIsConcreteType<DeploymentRowFactory>();
			StructureMapConfiguration.BuildInstancesOf<IDeploymentSelectionValidator>().TheDefaultIsConcreteType<DeploymentSelectionValidator>();
			StructureMapConfiguration.BuildInstancesOf<ILabelTextGenerator>().TheDefaultIsConcreteType<LabelTextGenerator>();
			StructureMapConfiguration.BuildInstancesOf<IAdministratorSecurityChecker>().TheDefaultIsConcreteType<AdministratorSecurityChecker>();
			StructureMapConfiguration.BuildInstancesOf<IApplicationInstanceCache>().TheDefaultIsConcreteType<ApplicationInstanceCache>();
			StructureMapConfiguration.BuildInstancesOf<IApplicationInstanceContext>().TheDefaultIsConcreteType<ApplicationInstanceContext>();
			StructureMapConfiguration.BuildInstancesOf<IApplicationInstanceFactory>().TheDefaultIsConcreteType<ApplicationInstanceFactory>();
			StructureMapConfiguration.BuildInstancesOf<IApplicationListingManager>().TheDefaultIsConcreteType<ApplicationListingManager>();
			StructureMapConfiguration.BuildInstancesOf<IApplicationListingRowView>().TheDefaultIsConcreteType<ApplicationListingRowView>();
			StructureMapConfiguration.BuildInstancesOf<IAvailabilityStatusUpdater>().TheDefaultIsConcreteType<AvailabilityStatusUpdater>();
			StructureMapConfiguration.BuildInstancesOf<ICurrentApplicationInstanceRetriever>().TheDefaultIsConcreteType<CurrentApplicationInstanceRetriever>();
			StructureMapConfiguration.BuildInstancesOf<IExceptionHandlingLoadBalanceStatusManager>().TheDefaultIsConcreteType<ExceptionHandlingLoadBalanceStatusManager>();
			StructureMapConfiguration.BuildInstancesOf<IExternalUrlChecker>().TheDefaultIsConcreteType<ExternalUrlChecker>();
			StructureMapConfiguration.BuildInstancesOf<IFileExtensionChecker>().TheDefaultIsConcreteType<FileExtensionChecker>();
			StructureMapConfiguration.BuildInstancesOf<ILoadBalanceStatusManager>().TheDefaultIsConcreteType<LoadBalanceStatusManager>();
			StructureMapConfiguration.BuildInstancesOf<IMaintenancePageRedirector>().TheDefaultIsConcreteType<MaintenancePageRedirector>();
			StructureMapConfiguration.BuildInstancesOf<IMaintenanceRedirectionChecker>().TheDefaultIsConcreteType<MaintenanceRedirectionChecker>();
			StructureMapConfiguration.BuildInstancesOf<ISecureAvailabilityStatusUpdater>().TheDefaultIsConcreteType<SecureAvailabilityStatusUpdater>();
			StructureMapConfiguration.BuildInstancesOf<IApplicationListingBodyView>().TheDefaultIsConcreteType<ApplicationListingBodyView>();
			StructureMapConfiguration.BuildInstancesOf<IApplicationListingView>().TheDefaultIsConcreteType<ApplicationListingView>();
			StructureMapConfiguration.BuildInstancesOf<ILoadBalancerBodyView>().TheDefaultIsConcreteType<LoadBalancerBodyView>();
			StructureMapConfiguration.BuildInstancesOf<ILoadBalancerView>().TheDefaultIsConcreteType<LoadBalancerView>();
			StructureMapConfiguration.BuildInstancesOf<IMenuView>().TheDefaultIsConcreteType<MenuView>();
			StructureMapConfiguration.BuildInstancesOf<IPageView>().TheDefaultIsConcreteType<PageView>();
			StructureMapConfiguration.BuildInstancesOf<IDatabaseActionExecutorFactory>().TheDefaultIsConcreteType<DatabaseActionExecutorFactory>();
			StructureMapConfiguration.BuildInstancesOf<IGuidGenerator>().TheDefaultIsConcreteType<GuidGenerator>();
		}
	}
}