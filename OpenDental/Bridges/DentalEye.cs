#if !DISABLE_WINDOWS_BRIDGES
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NDde;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class DentalEye{

		/// <summary></summary>
		public DentalEye(){
			
		}

		///<summary>Launches the program if necessary.  Then passes patient.Cur data using DDE.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);;
			if(pat==null){
				MessageBox.Show("Please select a patient first");
				return;
			}
			//The path is available in the registry, but we'll just make the user enter it.
			if(!File.Exists(ProgramCur.Path)){
				MessageBox.Show("Could not find "+ProgramCur.Path);
				return;
			}
			//Make sure the program is running
			if(Process.GetProcessesByName("DentalEye").Length==0){
				Process.Start(ProgramCur.Path);
				Thread.Sleep(TimeSpan.FromSeconds(4));
			}
			//command="[Add][PatNum][Fname][Lname][Address|Address2|City, ST Zip][phone1][phone2][mobile phone][email][sex(M/F)][birthdate (YYYY-MM-DD)]"
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");;
			string patID;
			if(PPCur.PropertyValue=="0"){
				patID=pat.PatNum.ToString();
			}
			else{
				if(pat.ChartNumber==""){
					MessageBox.Show("ChartNumber for this patient is blank.");
					return;
				}
				patID=pat.ChartNumber;
			}
			string command="[Add]["+patID+"]"
				+"["+pat.FName+"]"
				+"["+pat.LName+"]"
				+"["+pat.Address+"|";
			if(pat.Address2!=""){
				command+=pat.Address2+"|";
			}
			command+=pat.City+", "+pat.State+" "+pat.Zip+"]"
				+"["+pat.HmPhone+"]"
				+"["+pat.WkPhone+"]"
				+"["+pat.WirelessPhone+"]"
				+"["+pat.Email+"]";
			if(pat.Gender==PatientGender.Female)
				command+="[F]";
			else
				command+="[M]";
			command+="["+pat.Birthdate.ToString("yyyy-MM-dd")+"]";
			//MessageBox.Show(command);
			try {
				//Create a context that uses a dedicated thread for DDE message pumping.
				using(DdeContext context=new DdeContext()){
					//Create a client.
					using(DdeClient client=new DdeClient("DENTEYE","Patient",context)){
						//Establish the conversation.
						client.Connect();
						//Add patient or modify if already exists
						client.Execute(command,2000);//timeout 2 secs
						//Then, select patient
						command="[Search]["+patID+"]";
						client.Execute(command,2000);
					}
				}
			}
			catch{
				//MessageBox.Show(e.Message);
			}
		}

	}
}
#endif