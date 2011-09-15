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
		public static void SendData(Program ProgramCur, Patient pat){
			string iniString = 
				"[Patient]\r\n"
				+"ID="+pat.PatNum+"\r\n"
				+"Gender="+pat.Gender.ToString()+"\r\n"
				+"First="+pat.FName+"\r\n"
				+"Last="+pat.LName+"\r\n"
				+"Year="+pat.Birthdate.Year+"\r\n"
				+"Month="+pat.Birthdate.Month+"\r\n"
				+"Day="+pat.Birthdate.Day+"\r\n"
				+"[Dentist]\r\n"
				+"ID="+pat.PriProv+"\r\n"
				+"Password=digirex";
			// Write the string to a file.
			string path=ProgramProperties.GetPropVal(ProgramCur.ProgramNum,"System path to Apixia Digital Imaging ini file");
			StreamWriter iniFile = new System.IO.StreamWriter(path);
			iniFile.WriteLine(iniString);
			iniFile.Close();
			try{
				Process.Start(ProgramCur.Path);
			}
			catch{
				MessageBox.Show(ProgramCur.Path+" is not available.");
			}
		}

	}
}










