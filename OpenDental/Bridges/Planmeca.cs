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
	public class Planmeca{

		/// <summary></summary>
		public Planmeca(){
			
		}

		///<summary>Launches the program using the patient.Cur data.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			string path=Programs.GetProgramPath(ProgramCur);
			//DxStart.exe ”PatientID” ”FamilyName” ”FirstName” ”BirthDate”
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			if(pat==null){
				MessageBox.Show("Please select a patient first");
				return;
			}
			string info="";
			//Patient id can be any string format
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");
			if(PPCur.PropertyValue=="0"){
				info+="\""+pat.PatNum.ToString()+"\" ";
			}
			else{
				info+="\""+pat.ChartNumber.Replace("\"","")+"\" ";
			}
			info+="\""+pat.LName.Replace("\"","")+"\" "
				+"\""+pat.FName.Replace("\"","")+"\" "
				+"\""+pat.Birthdate.ToString("dd/MM/yyyy")+"\"";//Planmeca is a Finland based company, so their date format is dd/MM/yyyy. We used to send date format "MM/dd/yyyy" for our American customers before 12/19/2011.
			try{
				Process.Start(path,info);
			}
			catch{
				MessageBox.Show(path+" is not available.");
			}
			
		}

	}
}










