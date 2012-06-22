using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class Tscan{

		/// <summary></summary>
		public Tscan() {
			
		}

		///<summary>Launches the program using a combination of command line characters and the patient.Cur data.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			string path=Programs.GetProgramPath(ProgramCur);
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			if(pat==null){
				try {
					Process.Start(path);//should start Tscan without bringing up a pt.
				}
				catch {
					MessageBox.Show(path+" is not available.");
				}
			}
			else {
				string info=" -f"+Regex.Replace(pat.FName,"[^a-zA-Z0-9]","");//First name can only be alpha-numeric, so we remove non-alpha-numeric characters.
				if(Regex.Replace(pat.MiddleI,"[^a-zA-Z0-9]","")!="") {
					info+=" -m"+Regex.Replace(pat.MiddleI,"[^a-zA-Z0-9]","");//Middle name can only be alpha-numeric, so we remove non-alpha-numeric characters.
				}
				info+=" -l"+Regex.Replace(pat.LName,"[^a-zA-Z0-9]","");//Last name can only be alpha-numeric, so we remove non-alpha-numeric characters.
				//Patient id can only be aplha numeric, but they will almost always be so we don't check that here.
				ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");
				if(PPCur.PropertyValue=="0"){
					info+=" -i"+pat.PatNum.ToString();
				}
				else{
					info+=" -i"+Regex.Replace(pat.ChartNumber,"[^a-zA-Z0-9]","");//Spaces not allowed.
				}
				if(pat.Birthdate.Year>1880) {
					info+=" -d"+pat.Birthdate.Day.ToString().PadLeft(2,'0');
					info+=" -j"+pat.Birthdate.Month.ToString().PadLeft(2,'0');
					info+=" -y"+pat.Birthdate.Year.ToString();
				}
				if(pat.Gender==PatientGender.Female) {
					info+=" -g1";
				}
				else if(pat.Gender==PatientGender.Male) { 
					info+=" -g2";
				}
				try{
					Process.Start(path,ProgramCur.CommandLine+info);
				}
				catch{
					MessageBox.Show(path+" is not available, or there is an error in the command line options.");
				}
			}
		}

	}
}










