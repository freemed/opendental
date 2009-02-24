using System;
using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;
using System.ComponentModel;

namespace OpenDentServer {
	[RunInstallerAttribute(true)]
	public class MyProjectInstaller:Installer {
		private ServiceInstaller serviceInstaller1;
		private ServiceProcessInstaller processInstaller;

		public MyProjectInstaller() {
			processInstaller = new ServiceProcessInstaller();
			serviceInstaller1 = new ServiceInstaller();
			processInstaller.Account = ServiceAccount.LocalSystem;
			serviceInstaller1.StartType = ServiceStartMode.Automatic;          
			serviceInstaller1.ServiceName = "OpenDentalHL7";
			Installers.Add(serviceInstaller1);
			Installers.Add(processInstaller);
		}
	}
}
