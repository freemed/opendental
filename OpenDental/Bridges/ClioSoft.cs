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
	public class ClioSoft{

		/// <summary></summary>
		public ClioSoft() {
			
		}

		///<summary>Launches the program using a combination of command line characters and the patient.Cur data.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			string path=Programs.GetProgramPath(ProgramCur);
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			if(pat==null){
				try{
					Process.Start(path);//should start ClioSoft without bringing up a pt.
				}
				catch{
					MessageBox.Show(path+" is not available.");
				}
			}
			else {
				string info=" \"-";
				//Patient id can be any string format
				ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");
				if(PPCur.PropertyValue=="0"){
					info+=pat.PatNum.ToString();
				}
				else{
					info+=pat.ChartNumber;
				}
				//We remove double-quotes from the first and last name of the patient so extra double-quotes don't
				//cause confusion in the command line parameters for ClioSoft.
				info+=";"+pat.FName.Replace("\"","")+";"+pat.LName.Replace("\"","")+";";
				if(pat.Birthdate.Year>1880) {
					info+=pat.Birthdate.ToString("MM/dd/yyyy");
				}
				info+=";"+pat.SSN+";\"";
				try{
					Process.Start(path,ProgramCur.CommandLine+info);
				}
				catch{
					MessageBox.Show(path+" is not available, or there is an error in the command line options.");
				}
			}
		}

	}
}
