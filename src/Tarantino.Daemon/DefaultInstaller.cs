using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Tarantino.Daemon
{
	[RunInstaller(true)]
	public class DefaultInstaller : Installer
	{
		private ServiceInstaller serviceInstaller;
		private ServiceProcessInstaller processInstaller;

		public DefaultInstaller()
		{
			// define and create the service installer
			serviceInstaller = new ServiceInstaller();
			serviceInstaller.StartType = ServiceStartMode.Manual;
			serviceInstaller.ServiceName = DefaultService.SERVICE_NAME;
			serviceInstaller.DisplayName = DefaultService.SERVICE_NAME;
			serviceInstaller.Description = DefaultService.SERVICE_DESCRIPTION;
			Installers.Add(serviceInstaller);

			// define and create the process installer
			processInstaller = new ServiceProcessInstaller();
			processInstaller.Account = ServiceAccount.LocalSystem;

			Installers.Add(processInstaller);
		}
	}
}