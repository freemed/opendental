using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges {
	class MiPACS {
		///<summary>Launches the program using a combination of command line characters and the patient.Cur data.///</summary>
/*The command line integration works as follows:

C:\Program Files\MiDentView\Cmdlink.exe /ID=12345 /FN=JOHN /LN=DOE /BD=10/01/1985 /Sex=M

Parameters:'/ID=' for ID number, '/FN=' for Firstname, '/LN=' for Lastname, '/BD=' for Birthdate, '/Sex=' for Sex.

Example of a name with special characters (in this case, spaces):
C:\Program Files\MiDentView\Cmdlink.exe /ID=12345 /FN=Oscar /LN=De La Hoya /BD=10/01/1985 /Sex=M
		 */
		public static void SendData(Program ProgramCur, Patient pat){
			if(pat==null) {
				try {
					Process.Start(ProgramCur.Path);//should start MiPACS without bringing up a pt.
				}
				catch {
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}
			string gender=(pat.Gender==PatientGender.Female)?"F":"M";//M for Male, F for Female, M for Unknown.
			string info="/ID=" + pat.PatNum.ToString() 
				+ " /FN=" + pat.FName //special characters claimed to be ok
				+ " /LN=" + pat.LName 
				+ " /BD=" + pat.Birthdate.ToShortDateString() 
				+ " /Sex=" + gender;
			try {
				Process.Start(ProgramCur.Path,info);
			}
			catch {
				MessageBox.Show(ProgramCur.Path + " is not available.");
			}
		}
	}
}
