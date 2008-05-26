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
	public class DentForms{

		/// <summary></summary>
		public DentForms(){
			
		}

		///<summary>Launches the program using the patient.Cur data.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			//mtconnector.exe -patid 123  -fname John  -lname Doe  -ssn 123456789  -dob 01/25/1962  -gender M
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);;
			if(pat==null){
				MessageBox.Show("Please select a patient first");
				return;
			}
			string info="-patid ";
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");;
			if(PPCur.PropertyValue=="0"){
				info+=pat.PatNum.ToString()+"  ";
			}
			else{
				info+=pat.ChartNumber+"  ";
			}
			info+="-fname "+pat.FName+"  "
				+"-lname "+pat.LName+"  "
				+"-ssn "+pat.SSN+"  "
				+"-dob "+pat.Birthdate.ToShortDateString()+"  "
				+"-gender ";
			if(pat.Gender==PatientGender.Male){
				info+="M";
			}
			else{
				info+="F";
			}
			try{
				Process.Start(ProgramCur.Path,info);
			}
			catch{
				MessageBox.Show(ProgramCur.Path+" is not available.");
			}
			
		}

	}
}










