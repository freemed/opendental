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
	public class PerioPal{

		/// <summary></summary>
		public PerioPal(){
			
		}

		///<summary>Launches the program using command line.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			//Usage: [Application Path]PerioPal "PtChart; PtName ; PtBday; PtMedAlert"
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);;
			if(pat==null){
				return;
			}
			string info="\"";
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");;
			if(PPCur.PropertyValue=="0") {
				info+=pat.PatNum.ToString();
			}
			else {
				info+=Cleanup(pat.ChartNumber);
			}
			info+="; "
				+Cleanup(pat.GetNameLF())+"; "
				+pat.Birthdate.ToShortDateString()+"; ";
			bool hasMedicalAlert=false;
			if(pat.MedUrgNote!=""){
				hasMedicalAlert=true;
			}
			if(pat.Premed){
				hasMedicalAlert=true;
			}
			if(hasMedicalAlert){
				info+="Y";
			}
			else{
				info+="N";
			}
			//MessageBox.Show(info);
			try{
				Process.Start(ProgramCur.Path,info);
			}
			catch{
				MessageBox.Show(ProgramCur.Path+" "+info+" is not available.");
			}
		}

		///<summary>Makes sure invalid characters don't slip through.</summary>
		private static string Cleanup(string input){
			string retVal=input;
			retVal=retVal.Replace(";","");//get rid of any semicolon
			return retVal;
		}

	}
}










