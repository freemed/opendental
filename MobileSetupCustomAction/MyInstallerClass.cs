using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace MobileSetupCustomAction {
	[RunInstaller(true)]
	public partial class MyInstallerClass:Installer {
		public MyInstallerClass() {
			InitializeComponent();
		}

		public override void Commit(System.Collections.IDictionary savedState){
			// Call the Commit method of the base class
			base.Commit(savedState);
			// Open the registry key containing the path to the Application Manager
			Microsoft.Win32.RegistryKey key = null;
			key
				=Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\microsoft\\windows\\currentversion\\app paths\\ceappmgr.exe");
			if (key == null){
				return;// No Active Sync - throw a message
			}
			//The key is not null, so ActiveSync is installed on the user's desktop computer
			//Get the path to the Application Manager from the registry value
			string appPath = null;
			appPath = key.GetValue(null).ToString();
			//Get the target directory where the .ini file is installed.
			//This is sent from the Setup application
			//string strIniFilePath = "\"" + Context.Parameters["InstallDirectory"] + "OpenDentMobile.ini\"";
			string strIniFilePath = "\"" + Context.Parameters["targetdir"] + "OpenDentMobile.ini\"";
			if(appPath != null){
				//Now launch the Application Manager
				System.Diagnostics.Process process = new System.Diagnostics.Process();
				process.StartInfo.FileName = appPath;
				process.StartInfo.Arguments = strIniFilePath;
				process.Start();
			}
		}
	}
}
