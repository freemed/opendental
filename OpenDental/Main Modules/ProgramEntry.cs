using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace OpenDental {
	static class ProgramEntry {
		[STAThread]
		static void Main(string[] args) {
			//Register an EventHandler which handles unhandled exceptions.
			//AppDomain.CurrentDomain.UnhandledException+=new UnhandledExceptionEventHandler(OnUnhandeledExceptionPolicy);
			bool isSecondInstance=false ;//or more.
			Process[] processes=Process.GetProcesses();
			for(int i=0;i<processes.Length;i++) {
				if(processes[i].Id==Process.GetCurrentProcess().Id) {
					continue;
				}
				//we have to do it this way because during debugging, the name has vshost tacked onto the end.
				if(processes[i].ProcessName.StartsWith("OpenDental")) {
					isSecondInstance=true;
					break;
				}
			}
			if(args.Length>0) {//if any command line args, then we will attempt to reuse an existing OD window.
				if(isSecondInstance){
					FormCommandLinePassOff formCommandLine=new FormCommandLinePassOff();
					formCommandLine.CommandLineArgs=new string[args.Length];
					args.CopyTo(formCommandLine.CommandLineArgs,0);
					Application.Run(formCommandLine);
					return;
				}
			}
			Application.EnableVisualStyles();//changes appearance to XP
			Application.DoEvents();//workaround for a known MS bug in the line above
			FormOpenDental formOD=new FormOpenDental();
			formOD.IsSecondInstance=isSecondInstance;
			formOD.CommandLineArgs=new string[args.Length];
			args.CopyTo(formOD.CommandLineArgs,0);
			Application.Run(formOD);
		}

		/*
		///<summary>Overrides the default Windows unhandled exception functionality.</summary>
		static void OnUnhandeledExceptionPolicy(Object Sender,UnhandledExceptionEventArgs e) {
			Exception ex=e.ExceptionObject as Exception;
			string message="Unhandled Exception: ";
			if(ex!=null) {//The unhandeled Exception is CLS compliant.
				message+=ex.ToString();
			}else{//The unhandeled Exception is not CLS compliant.				
				//You can only handle this Exception as Object
				message+="Object Type: "+e.ExceptionObject.GetType()+", Object String: "+e.ExceptionObject.ToString();
			}
			if(!e.IsTerminating){
				//Exception occurred in a thread pool or finalizer thread. Debugger launches only explicitly.
				Logger.openlog.LogMB(message,Logger.Severity.ERROR);
#if(DEBUG)
				Debugger.Launch();
#endif
			}else{
				//Exception occurred in managed thread. Debugger will automatically launch in visual studio.
				Logger.openlog.LogMB(message,Logger.Severity.FATAL_ERROR);
			}
		}*/
	}
}
