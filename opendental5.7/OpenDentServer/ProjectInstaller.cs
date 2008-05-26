using System;
using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;
using System.ComponentModel;

namespace OpenDentServer {
	[RunInstallerAttribute(true)]
	public class MyProjectInstaller:Installer {
		private ServiceInstaller serviceInstaller1;
		//private ServiceInstaller serviceInstaller2;
		private ServiceProcessInstaller processInstaller;

		public MyProjectInstaller() {
			// Instantiate installers for process and services.
			processInstaller = new ServiceProcessInstaller();
			serviceInstaller1 = new ServiceInstaller();
			//serviceInstaller2 = new ServiceInstaller();

			// The services run under the system account.
			processInstaller.Account = ServiceAccount.LocalSystem;

			// The service is started automatically.
			serviceInstaller1.StartType = ServiceStartMode.Automatic;
			//serviceInstaller2.StartType = ServiceStartMode.Manual;

			// ServiceName must equal those on ServiceBase derived classes.            
			serviceInstaller1.ServiceName = "OpenDental";
			//serviceInstaller2.ServiceName = "Hello-World Service 2";

			// Add installers to collection. Order is not important.
			Installers.Add(serviceInstaller1);
			//Installers.Add(serviceInstaller2);
			Installers.Add(processInstaller);
		}
	}
}
