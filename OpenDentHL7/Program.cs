using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace OpenDentHL7 {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main() {
#if DEBUG
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormDebug());
#else
			ServiceBase[] ServicesToRun;
			ServiceHL7 serviceHL7=new ServiceHL7();
			serviceHL7.ServiceName="serviceHL7";
			ServicesToRun = new ServiceBase[] 
			{ 
				new ServiceHL7() 
			};
			ServiceBase.Run(ServicesToRun);
#endif
		}
	}
}
