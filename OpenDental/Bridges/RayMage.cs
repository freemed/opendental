using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class RayMage{

		/// <summary></summary>
		public RayMage() {
			
		}

		public static void SendData(Program ProgramCur,Patient pat) {
			if(pat==null) {
				try {
					Process.Start(ProgramCur.Path);//should start rayMage without bringing up a pt.
				}
				catch {
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}
			else {
				string info=" /PATID \"";
				if(ProgramProperties.GetPropVal(ProgramCur.ProgramNum,"Enter 0 to use PatientNum, or 1 to use ChartNum")=="0"){
					info+=pat.PatNum.ToString();
				}
				else{
					info+=pat.ChartNumber;
				}
				info+="\" /NAME \""+pat.FName.Replace(" ","").Replace("\"","")+"\" /SURNAME \""+pat.LName.Replace(" ","").Replace("\"","")+"\"";
				try {
					Process.Start(ProgramCur.Path,ProgramCur.CommandLine+info);
				}
				catch {
					MessageBox.Show(ProgramCur.Path+" is not available, or there is an error in the command line options.");
				}
			}
		}

	}
}










