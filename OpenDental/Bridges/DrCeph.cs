#if !DISABLE_WINDOWS_BRIDGES
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VBbridges;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class DrCeph{
		
		/// <summary></summary>
		public DrCeph(){
			
		}

		///<summary>Uses a VB dll to launch.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			if(pat==null){
				MessageBox.Show("Please select a patient first.");
				return;
			}
			//Make sure the program is running
			if(Process.GetProcessesByName("DrCeph").Length==0) {
				try{
					Process.Start(ProgramCur.Path);
				}
				catch{
					MsgBox.Show("DrCeph","Program path not set properly.");
					return;
				}
				Thread.Sleep(TimeSpan.FromSeconds(4));
			}
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);;
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");;
			string patID="";
			if(PPCur.PropertyValue=="0"){
				patID=pat.PatNum.ToString();
			}
			else{
				patID=pat.ChartNumber;
			}
			try{
				RefAttach[] referalList=RefAttaches.Refresh(pat.PatNum);
				Provider prov=Providers.GetProv(Patients.GetProvNum(pat));
				string provName=prov.FName+" "+prov.MI+" "+prov.LName+" "+prov.Suffix;
				Family fam=Patients.GetFamily(pat.PatNum);
				Patient guar=fam.List[0];
				string relat="";
				if(guar.PatNum==pat.PatNum){
					relat="Self";
				}
				else if(guar.Gender==PatientGender.Male	&& pat.Position==PatientPosition.Child){
					relat="Father";
				}
				else if(guar.Gender==PatientGender.Female	&& pat.Position==PatientPosition.Child){
					relat="Mother";
				}
				else{
					relat="Unknown";
				}
				VBbridges.DrCephNew.Launch(patID,pat.FName,pat.MiddleI,pat.LName,pat.Address,pat.Address2,pat.City,
					pat.State,pat.Zip,pat.HmPhone,pat.SSN,pat.Gender.ToString(),pat.Race.ToString(),"",pat.Birthdate.ToString(),
					DateTime.Today.ToShortDateString(),RefAttaches.GetReferringDr(referalList),provName,
					guar.GetNameFL(),guar.Address,guar.Address2,guar.City,guar.State,guar.Zip,guar.HmPhone,relat);
			}
			catch{
				MessageBox.Show("DrCeph not responding.  It might not be installed properly.");
			}
		}

		

	}
}
#endif