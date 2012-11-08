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
	public class Apixia{

		/// <summary></summary>
		public Apixia(){
			
		}

		///<summary>Launches the program using a combination of command line characters and the patient.Cur data.///</summary>
/*Data like the following:
[Patient]
ID=A123456789
Gender=Male
First=John
Last=Smith
Year=1955
Month=1
Day=23
[Dentist]
ID=001
Password=1234
		 
Should appear in the following file: C:/Program Files/Digirex/Switch.ini
and should be accessed/opened by C:/Program Files/Digirex/digirex.ini
		 */
		public static void SendData(Program ProgramCur,Patient pat) {
			string path=Programs.GetProgramPath(ProgramCur);
			if(pat==null) {
				try {
					Process.Start(path);//should start Apixia without bringing up a pt.
				}
				catch {
					MessageBox.Show(path+" is not available.");
				}
			}
			else {
				string iniString="[Patient]\r\n";
				if(ProgramProperties.GetPropVal(ProgramCur.ProgramNum,"Enter 0 to use PatientNum, or 1 to use ChartNum")=="0"){
					iniString+="ID="+pat.PatNum.ToString()+"\r\n";
				}
				else{
					iniString+="ID="+pat.ChartNumber+"\r\n";
				}
				Provider priProv=Providers.GetProv(pat.PriProv);
				iniString+="Gender="+pat.Gender.ToString()+"\r\n"
				+"First="+pat.FName+"\r\n"
				+"Last="+pat.LName+"\r\n"
				+"Year="+pat.Birthdate.Year.ToString()+"\r\n"
				+"Month="+pat.Birthdate.Month.ToString()+"\r\n"
				+"Day="+pat.Birthdate.Day.ToString()+"\r\n"
				+"[Dentist]\r\n"
				+"ID="+priProv.Abbr+"\r\n"//Abbreviation is guaranteed non-blank, because of UI validation in the provider edit window.
				+"Password=digirex";
				// Write the string to a file.
				string iniPath=ProgramProperties.GetPropVal(ProgramCur.ProgramNum,"System path to Apixia Digital Imaging ini file");
				StreamWriter iniFile = new System.IO.StreamWriter(iniPath);
				iniFile.WriteLine(iniString);
				iniFile.Close();
				try {
					Process.Start(path);
				}
				catch {
					MessageBox.Show(path+" is not available.");
				}
			}
		}

	}
}










