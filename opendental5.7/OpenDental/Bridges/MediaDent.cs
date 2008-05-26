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
	public class MediaDent{

		/// <summary></summary>
		public MediaDent(){
			
		}

		///<summary>Launches the program using command line.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			//Usage: mediadent.exe /P<Patient Name> /D<Practitioner> /L<Language> /F<Image folder> /B<Birthdate>
			//Example: mediadent.exe /PJan Met De Pet /DOtté Gunter /L1 /Fc:\Mediadent\patients\1011 /B27071973
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);;
			if(pat==null){
				return;
			}
			string info="/P"+Cleanup(pat.FName+" "+pat.LName);
			Provider prov=Providers.GetProv(Patients.GetProvNum(pat));
			info+=" /D"+prov.FName+" "+prov.LName
				+" /L1 /F";
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Image Folder");
			info+=PPCur.PropertyValue;
			PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");;
			if(PPCur.PropertyValue=="0"){
				info+=pat.PatNum.ToString();
			}
			else{
				info+=Cleanup(pat.ChartNumber);
			}
			info+=" /B"+pat.Birthdate.ToString("ddMMyyyy");
			//MessageBox.Show(info);
			//not used yet: /inputfile "path to file"
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
			retVal=retVal.Replace("\"","");//get rid of any quotes
			retVal=retVal.Replace("'","");//get rid of any single quotes
			retVal=retVal.Replace("/","");//get rid of any /
			return retVal;
		}

	}
}










