#if !DISABLE_WINDOWS_BRIDGES
using System;
using System.Collections;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	///<summary>Provides bridging functionality to Schick CDR.</summary>
	public class Schick{

		///<summary>Default constructor</summary>
		public Schick(){

		}


		///<summary>Launches the main Patient Document window of Schick.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			if(pat==null){
				return;
			}
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);;
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");;
			string patID="";
			if(PPCur.PropertyValue=="0") {
				patID=pat.PatNum.ToString();
			}
			else {
				patID=pat.ChartNumber;
			}
			try {
				VBbridges.Schick.Launch(patID,pat.LName,pat.FName);
			}
			catch {
				MessageBox.Show("Error launching Schick CDR Dicom.");
			}
			


		}

		





	}
}
#endif