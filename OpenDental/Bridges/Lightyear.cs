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
	public class Lightyear{

		/// <summary></summary>
		public Lightyear(){
			
		}

		///<summary>Launches the program using the patient.Cur data.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);;
			if(pat==null){
				MessageBox.Show("Please select a patient first");
				return;
			}
			string info="";
			//Patient id can be any string format
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");;
			if(PPCur.PropertyValue=="0"){
				info+="-i \""+pat.PatNum.ToString()+"\" ";
			}
			else{
				info+="-i \""+pat.ChartNumber.Replace("\"","")+"\" ";
			}
			info+="-n \""+pat.LName.Replace("\"","")+", "
				+pat.FName.Replace("\"","")+"\"";
			//MessageBox.Show(info);
			try{
				Process.Start(ProgramCur.Path,info);
			}
			catch{
				MessageBox.Show(ProgramCur.Path+" is not available.");
			}
		}

	}
}










