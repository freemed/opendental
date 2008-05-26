using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	/// <summary>Also used by the XDR bridge.</summary>
	public class Dexis{

		/// <summary></summary>
		public Dexis(){
			
		}

		///<summary>Sends data for Patient.Cur to the InfoFile and launches the program.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "InfoFile path");
			string infoFile=PPCur.PropertyValue;
			if(pat!=null){
				try{
					using(StreamWriter sw=new StreamWriter(infoFile,false)){
						sw.WriteLine(pat.LName+", "+pat.FName
							+"  "+pat.Birthdate.ToShortDateString()
							+"  ("+pat.PatNum.ToString()+")");
						//patientID can be any string format, max 8 char.
						//There is no validation to ensure that length is 8 char or less.
						PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");
						if(PPCur.PropertyValue=="0"){
							sw.WriteLine("PN="+pat.PatNum.ToString());
						}
						else{
							sw.WriteLine("PN="+pat.ChartNumber);
						}
						//sw.WriteLine("PN="+pat.PatNum.ToString());
						sw.WriteLine("LN="+pat.LName);
						sw.WriteLine("FN="+pat.FName);
						sw.WriteLine("BD="+pat.Birthdate.ToShortDateString());
						if(pat.Gender==PatientGender.Female)
							sw.WriteLine("SX=F");
						else
							sw.WriteLine("SX=M");
					}
					Process.Start(ProgramCur.Path,"@"+infoFile);
				}
				catch{
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}//if patient is loaded
			else{
				try{
					Process.Start(ProgramCur.Path);//should start Dexis without bringing up a pt.
				}
				catch{
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}
		}

	}
}










