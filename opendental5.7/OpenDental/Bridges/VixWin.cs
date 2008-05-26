using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges {
	/// <summary></summary>
	public class VixWin {

		/// <summary></summary>
		public VixWin() {

		}

		///<summary>Sends data for Patient.Cur by command line interface.</summary>
		public static void SendData(Program ProgramCur, Patient pat) {
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);;
			if(pat==null) {
				return;
			}
			//Example: c:\vixwin\vixwin -I 123ABC -N Bill^Smith
			string info="-I ";
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");;
			if(PPCur.PropertyValue=="0") {
				info+=pat.PatNum.ToString();
			}
			else {
				info+=pat.ChartNumber;//max 64 char
			}
			info+=" -N "+pat.FName.Replace(" ","")+"^"+pat.LName.Replace(" ","");//no spaces allowed
			//MessageBox.Show(info);
			try {
				Process.Start(ProgramCur.Path,info);
			}
			catch {
				MessageBox.Show(ProgramCur.Path+" is not available.");
			}
		}


	}
}











