using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace OpenDentHL7 {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args) {
#if DEBUG
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormDebug());
//#else
			//ServiceBase[] ServicesToRun;
			//ServiceHL7 serviceHL7=new ServiceHL7();
			//serviceHL7.ServiceName="serviceHL7";
			//ServicesToRun = new ServiceBase[] 
			//{ 
				//new ServiceHL7() 
			//};
			//ServiceBase.Run(ServicesToRun);
			EventLog.WriteEntry("OpenDentHL7.Main", DateTime.Now.ToLongTimeString() +" - Service main method starting...");
			ServiceHL7 serviceHL7=new ServiceHL7();
			for(int i=0;i<args.Length;i++) {
				if(args[i].StartsWith("ServiceName=") && args[i].Length>12) {
					serviceHL7.ServiceName=args[i].Substring(11).Trim('"');
				}
			}
			if(serviceHL7.ServiceName=="") {
				serviceHL7.ServiceName="OpenDentHL7";
			}
			ServiceBase.Run(new ServiceHL7());
			EventLog.WriteEntry("OpenDentHL7.Main",DateTime.Now.ToLongTimeString() +" - Service main method exiting...");
#endif
		}
	}
}
