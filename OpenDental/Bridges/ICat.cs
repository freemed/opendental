using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class ICat{

		/// <summary></summary>
		public ICat() {
			
		}

		///<summary>Command line.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram,"Acquisition computer name");
			if(Environment.MachineName==PPCur.PropertyValue) {
				SendDataServer(ProgramCur,pat);
			}
			else {
				SendDataWorkstation(ProgramCur,pat);
			}
		}

		///<summary>XML file.</summary>
		private static void SendDataServer(Program ProgramCur,Patient pat) {

		}

		///<summary>Command line.</summary>
		private static void SendDataWorkstation(Program ProgramCur,Patient pat) {

		}

		/*
			string infoFile=PPCur.PropertyValue;
			if(pat!=null) {
				try {
					//patientID can be any string format, max 8 char.
					//There is no validation to ensure that length is 8 char or less.
					PPCur=ProgramProperties.GetCur(ForProgram,"Enter 0 to use PatientNum, or 1 to use ChartNum");
					string id="";
					if(PPCur.PropertyValue=="0") {
						id=pat.PatNum.ToString();
					} else {
						id=pat.ChartNumber;
					}
					using(StreamWriter sw=new StreamWriter(infoFile,false)) {
						sw.WriteLine(pat.LName+", "+pat.FName
							+"  "+pat.Birthdate.ToShortDateString()
							+"  ("+id+")");
						sw.WriteLine("PN="+id);
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
				} catch {
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}//if patient is loaded
			else {
				try {
					Process.Start(ProgramCur.Path);//should start Dexis without bringing up a pt.
				} catch {
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}
		}*/

	}
}










