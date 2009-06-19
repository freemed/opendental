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

		///<summary>Launches the program by passing the name of a file with data in it.  </summary>
		public static void SendData(Program ProgramCur,Patient pat) {
			//ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum); ;
			if(pat==null) {
				try {
					Process.Start(ProgramCur.Path);//should start Mediadent without bringing up a pt.
				}
				catch {
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}
			string infoFile=ProgramProperties.GetPropVal(ProgramCur.ProgramNum,"Text file path");
			try {
				using(StreamWriter sw=new StreamWriter(infoFile,false)) {
					string id="";
					if(ProgramProperties.GetPropVal(ProgramCur.ProgramNum,"Enter 0 to use PatientNum, or 1 to use ChartNum")=="0") {
						id=pat.PatNum.ToString();
					}
					else {
						id=pat.ChartNumber;
					}
					sw.WriteLine(pat.LName+", "+pat.FName
						+" "+pat.Birthdate.ToShortDateString()
						+" "+id);
					sw.WriteLine();
					sw.WriteLine("PN="+id);
					sw.WriteLine("LN="+pat.LName);
					sw.WriteLine("FN="+pat.FName);
					sw.WriteLine("BD="+pat.Birthdate.ToString("MM/dd/yyyy"));
					if(pat.Gender==PatientGender.Female) {
						sw.WriteLine("SX=F");
					}
					else {
						sw.WriteLine("SX=M");
					}
				}
			}
			catch {
				MessageBox.Show("Unable to write to text file: "+infoFile);
				return;
			}
			try{
				Process.Start(ProgramCur.Path,"@"+infoFile);
			}
			catch {
				MessageBox.Show(ProgramCur.Path+" is not available.");
			}
		}

		/*Outdated version 4.
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
		}*/

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










