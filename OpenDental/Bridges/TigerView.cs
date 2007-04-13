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
	public class TigerView{
		private static string iniFile;

		/// <summary></summary>
		public TigerView(){
			
		}

		[DllImport("kernel32")]//this is the windows function for working with ini files.
    private static extern long WritePrivateProfileString(string section,string key,string val,string filePath);

		private static void WriteValue(string key,string val){
			WritePrivateProfileString("Slave",key,val,iniFile);
		}


		///<summary>Sends data for Patient.Cur to the Tiger1.ini file and launches the program.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Tiger1.ini path");
			iniFile=PPCur.PropertyValue;
			if(pat!=null){
				if(!File.Exists(iniFile)){
					MessageBox.Show("Could not find "+iniFile);
					return;
				}
				WriteValue("LastName",pat.LName);
				WriteValue("FirstName",pat.FName);
				//Patient Id can be any string format.
				PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");
				if(PPCur.PropertyValue=="0"){
					WriteValue("PatientID",pat.PatNum.ToString());
				}
				else{
					WriteValue("PatientID",pat.ChartNumber);
				}
				WriteValue("PatientSSN",pat.SSN);
				//WriteValue("SubscriberSSN",pat);
				if(pat.Gender==PatientGender.Female)
					WriteValue("Gender","Female");
				else
					WriteValue("Gender","Male");
				WriteValue("DOB",pat.Birthdate.ToString("MM/dd/yy"));
				//WriteValue("ClaimID",pat);??huh
				WriteValue("AddrStreetNo",pat.Address);
				//WriteValue("AddrStreetName",pat);??
				//WriteValue("AddrSuiteNo",pat);??
				WriteValue("AddrCity",pat.City);
				WriteValue("AddrState",pat.State);
				WriteValue("AddrZip",pat.Zip);
				WriteValue("PhHome",LimitLength(pat.HmPhone,13));
				WriteValue("PhWork",LimitLength(pat.WkPhone,13));
				try{
					Process.Start(ProgramCur.Path,ProgramCur.CommandLine);
				}
				catch{
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}//if patient is loaded
			else{
				try{
					Process.Start(ProgramCur.Path);//should start TigerView without bringing up a pt.
				}
				catch{
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}
		}

		private static string LimitLength(string str,int length){
			if(str.Length<length+1){
				return str;
			}
			return str.Substring(0,length);
		}

	}
}










