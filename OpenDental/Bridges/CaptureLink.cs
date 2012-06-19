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
	public class CaptureLink{

		///<summary></summary>
		public CaptureLink(){

		}

		///<summary>CaptureLink reads the bridge parameters from the clipboard.</summary>
		//Command format: LName FName PatID
		public static void SendData(Program ProgramCur, Patient pat){
			if(pat==null){
				MessageBox.Show("No patient selected.");
				return;
			}
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			string info=Tidy(pat.LName)+" "+Tidy(pat.FName)+" ";
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");
			if(PPCur.PropertyValue=="0"){
				info+=pat.PatNum.ToString();
			}
			else{
				info+=pat.ChartNumber;
			}
			Clipboard.SetText(info,TextDataFormat.Text);
		}

		private static string Tidy(string str){
			return str.Replace("\"","");
		}

	}
}
