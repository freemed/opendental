using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
//using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class Trophy{

		/// <summary></summary>
		public Trophy(){
			
		}

		///<summary>Launches the program using a combination of command line characters and the patient.Cur data.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);;
			if(pat!=null){
				ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Storage Path");
				string comline="-P"+PPCur.PropertyValue+@"\";
				//Patient id can be any string format
				PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");;
				if(PPCur.PropertyValue=="0"){
					comline+=pat.PatNum.ToString();
				}
				else{
					comline+=pat.ChartNumber;
				}
				comline+=" -N"+pat.LName+", "+pat.FName;
				comline=comline.Replace("\"","");//gets rid of any quotes
				comline=comline.Replace("'","");//gets rid of any single quotes
				try{
					Process.Start(ProgramCur.Path,comline);
				}
				catch{
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}//if patient is loaded
			else{
				try{
					Process.Start(ProgramCur.Path);//should start Trophy without bringing up a pt.
				}
				catch{
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}
		}

	}
}










