using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UpdateFileCopier {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] arguments) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if(arguments.Length==2) {
				Application.Run(new FormMain(arguments[0],arguments[1]));
			}
			else {//just for rare debugging situations
				Application.Run(new FormMain(@"C:\OpenDentImages\UpdateFiles","0"));
			}
		}
	}
}
