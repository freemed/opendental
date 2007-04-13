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
	public class Apteryx{

		/// <summary></summary>
		public Apteryx(){
			
		}

		///<summary>Launches the program using a combination of command line characters and the patient.Cur data.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);;
			if(pat!=null){
				string info="\""+pat.LName+", "+pat.FName+"::";
				if(pat.SSN.Length==9){
					info+=pat.SSN.Substring(0,3)+"-"
						+pat.SSN.Substring(3,2)+"-"
						+pat.SSN.Substring(5,4);
				}
				//Patient id can be any string format
				ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");;
				if(PPCur.PropertyValue=="0"){
					info+="::"+pat.PatNum.ToString();
				}
				else{
					info+="::"+pat.ChartNumber;
				}
				info+="::"+pat.Birthdate.ToShortDateString()+"::";
				if(pat.Gender==PatientGender.Female)
					info+="F";
				else
					info+="M";
				info+="\"";
				try{
					//commandline default is /p
					Process.Start(ProgramCur.Path,ProgramCur.CommandLine+info);
				}
				catch{
					MessageBox.Show(ProgramCur.Path+" is not available, or there is an error in the command line options.");
				}
			}//if patient is loaded
			else{
				try{
					Process.Start(ProgramCur.Path);//should start Apteryx without bringing up a pt.
				}
				catch{
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}
		}

	}
}










