using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;


namespace OpenDental {
	public partial class FormMobileSetup:Form {
		private FormOpenDental frmOD=null;
		private static string RegistrationKey;
		private static MobileWeb.Mobile mb = new MobileWeb.Mobile();
		private static DateTime MobileSyncDateTimeLastRun;
		private static string MobileSyncServerURL;
		private static String SynchUrlStaging="https://192.168.0.196/WebHostSynch/Mobile.asmx";
		private static String SynchUrlDev="http://localhost:2923/Mobile.asmx";
		private static string MobileSyncWorkstationName;
		private static int MobileSyncIntervalMinutes;
		private static DateTime MobileExcludeApptsBeforeDate;
		private static String StatusMessage="";
		private static int BatchSize=100;
		private static bool PaidCustomer=false;
		private static bool IsSynching=false;// this variable prevents the synching methods from being called when a previous synch is in progress.
		private bool MobileUserNameChanged=false;
		private bool MobilePasswordChanged=false;
		private string NotPaidMessage="You must be a paid customer to use this feature. Please call Open Dental and register as a paid user";
		private string WebServiceUnavailableMessage="Either the web service is not available or the WebHostSynch URL is incorrect";
		private string SyncCompletedMessage="Sync Completed";
		private string FullSynchNotRunMessage="Sync has never been run. You must do a full sync first.";
		private string IncorrectRegKeyMessage="Registration key provided by the dental office is incorrect";

		public FormMobileSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormMobileSetup_Load(object sender,EventArgs e) {
			try {
				InitializeVariables();
				textDateTimeLastRun.Text=MobileSyncDateTimeLastRun.ToShortDateString()+" "+MobileSyncDateTimeLastRun.ToShortTimeString();
				textMobileSyncServerURL.Text=MobileSyncServerURL;
				textMobileSynchWorkStation.Text=MobileSyncWorkstationName;
				textSynchMinutes.Text=MobileSyncIntervalMinutes+"";
				textDateBefore.Text=MobileExcludeApptsBeforeDate.ToShortDateString();
				butSavePreferences.Enabled=false;//this line is repeated below on purpose
				if(!TestWebServiceExists()) {
					MsgBox.Show(this,WebServiceUnavailableMessage);
					return;
				}
				if(!PaidCustomer) {
					MsgBox.Show(this,NotPaidMessage);
					return;
				}
				textMobileUserName.Text=mb.GetUserName(RegistrationKey);
				MobileUserNameChanged=false;// when textMobileUserName is changed in textMobileUserName_TextChanged MobileUserNameChanged is set to true
				butSavePreferences.Enabled=false;//this line is repeated on purpose
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private static void InitializeVariables() {
				RegistrationKey=PrefC.GetStringSilent(PrefName.RegistrationKey);
				MobileSyncServerURL=PrefC.GetStringSilent(PrefName.MobileSyncServerURL);
				MobileSyncWorkstationName=PrefC.GetStringSilent(PrefName.MobileSyncWorkstationName);
				MobileSyncDateTimeLastRun=PrefC.GetDateT(PrefName.MobileSyncDateTimeLastRun);
				MobileExcludeApptsBeforeDate=PrefC.GetDateT(PrefName.MobileExcludeApptsBeforeDate);
				//#if DEBUG	
				if((MobileSyncServerURL==SynchUrlStaging)||(MobileSyncServerURL==SynchUrlDev)) {
					IgnoreCertificateErrors();
				}
				//#endif
				StatusMessage="";
				if(!TestWebServiceExists()) {
					return;
				}
				PaidCustomer=mb.IsPaidCustomer(RegistrationKey); // check for payment here
				if(PaidCustomer) {
					MobileSyncIntervalMinutes=PrefC.GetInt(PrefName.MobileSyncIntervalMinutes);
				}else{
					MobileSyncIntervalMinutes=0;
					Prefs.UpdateInt(PrefName.MobileSyncIntervalMinutes,MobileSyncIntervalMinutes);
				}
		}

		private static void Synch(DateTime GetChangedSince) {
			try {
				if(!TestWebServiceExists()) {
					return;
				}
				if(mb.GetCustomerNum(RegistrationKey)==0) {
				return;
				}
				if(!IsSynching) {// making sure that the previous synching process is complete.
					IsSynching=true;
					//CreatePatients(1000);//for testing only
					//CreateAppointments(10); // for each patient //for testing only
					//CreatePrescriptions(10);// for each patient //for testing only
					DateTime MobileSyncDateTimeLastRunNew=MiscData.GetNowDateTime();
					List<long> patNumList=Patientms.GetChangedSincePatNums(GetChangedSince);
					SynchPatients(patNumList);
					List<long> aptNumList=Appointmentms.GetChangedSinceAptNums(GetChangedSince,MobileExcludeApptsBeforeDate);
					SynchAppointments(aptNumList);
					List<long> rxNumList=RxPatms.GetChangedSinceRxNums(GetChangedSince);
					SynchPrescriptions(rxNumList);
					if(Prefs.UpdateDateT(PrefName.MobileSyncDateTimeLastRun,MobileSyncDateTimeLastRunNew)){
						DataValid.SetInvalid(InvalidType.Prefs);// change value on all machines
					}
					MobileSyncDateTimeLastRun=MobileSyncDateTimeLastRunNew;
					IsSynching=false;
				}
			}
			catch(Exception ex) {
				IsSynching=false;
				MessageBox.Show(ex.Message);// will this show up ever?
			}
		}

		private static void SynchNow() {
			Synch(MobileSyncDateTimeLastRun);
		}
		
		/// <summary>
		/// Called from the main form
		/// </summary>
		internal static void Synch() {
			InitializeVariables();
			if(MobileSyncIntervalMinutes==0) {
				return;// not a paid customer
			}
			if(MobileSyncDateTimeLastRun.Year<1880) {
				return;//Sync has never been run before.
			}
			if(DateTime.Now>MobileSyncDateTimeLastRun.AddMinutes(MobileSyncIntervalMinutes)) {
				Synch(MobileSyncDateTimeLastRun);
			}
		}

		private static void SynchFull() {
			DateTime FullSynchDateTime=new DateTime(1880,1,1);
			mb.DeleteAllRecords(RegistrationKey);//for full synch, delete all records then repopulate.
			Synch(FullSynchDateTime);
		}

		private static void SynchPatients(List<long> patNumList) {
			// major problem  for a large number of records, a system out of memory exception is thrown, hence the synch is done in batches.
			int LocalBatchSize=BatchSize;
			for(int start=0;start<patNumList.Count;start+=LocalBatchSize) {
				if((start+LocalBatchSize)>patNumList.Count) {
					LocalBatchSize=patNumList.Count-start;
				}
				List<long> BlockPatNumList=patNumList.GetRange(start,LocalBatchSize);
				List<Patientm> ChangedPatientmList=Patientms.GetMultPats(BlockPatNumList);
				mb.SynchPatients(RegistrationKey,ChangedPatientmList.ToArray());
				StatusMessage=start+LocalBatchSize+" record(s) of "+patNumList.Count +" Patient Uploads";
				Application.DoEvents();// allows textProgress to be refreshed 
			}
		}

		private static void SynchAppointments(List<long> AptNumList) {
			// major problem  for a large number of records, a system out of memory exception is thrown, hence the synch is done in batches.
			int LocalBatchSize=BatchSize;
			for(int start=0;start<AptNumList.Count;start+=LocalBatchSize) {
				if((start+LocalBatchSize)>AptNumList.Count) {
					LocalBatchSize=AptNumList.Count-start;
				}
				List<long> BlockAptNumList=AptNumList.GetRange(start,LocalBatchSize);
				List<Appointmentm> ChangedAppointmentmList=Appointmentms.GetMultApts(BlockAptNumList);
				mb.SynchAppointments(RegistrationKey,ChangedAppointmentmList.ToArray());
				StatusMessage=start+LocalBatchSize+" records(s) of "+AptNumList.Count +" Appointmnet Uploads";
				Application.DoEvents();// allows textProgress to be refreshed 
			}
		}

		private static void SynchPrescriptions(List<long> RxNumList) {
			// major problem  for a large number of records, a system out of memory exception is thrown, hence the synch is done in batches.
			int LocalBatchSize=BatchSize;
			for(int start=0;start<RxNumList.Count;start+=LocalBatchSize) {
				if((start+LocalBatchSize)>RxNumList.Count) {
					LocalBatchSize=RxNumList.Count-start;
				}
				List<long> BlockRxNumList=RxNumList.GetRange(start,LocalBatchSize);
				List<RxPatm> ChangedRxList=RxPatms.GetMultRxPats(BlockRxNumList);
				mb.SynchPrescriptions(RegistrationKey,ChangedRxList.ToArray());
				StatusMessage=start+LocalBatchSize+" records(s) of "+RxNumList.Count +" Prescriptions Uploads";
				Application.DoEvents();// allows textProgress to be refreshed 
			}
		}

		/// <summary>
		/// An empty method to test if the webservice is up and running. This was made with the intention of testing the correctness of the webservice URL. If an incorrect webservice URL is used in a background thread the exception cannot be handled easily to a point where even a correct URL cannot be keyed in by the user. Because an exception in a background thread closes the Form which spawned it.
		/// </summary>
		/// <returns></returns>
		private static bool TestWebServiceExists() {
			try {
				mb.Url=MobileSyncServerURL;
				if(mb.ServiceExists()) {
					return true;
				}
			}
			catch (Exception ex) {
				return false;
			}
			return false;
		}

		/// <summary>
		///  This method is used only for testing with security certificates that has problems.
		/// </summary>
		private static void IgnoreCertificateErrors() {
			///the line below will allow the code to continue by not throwing an exception.
			///It will accept the security certificate if there is a problem with the security certificate.
			System.Net.ServicePointManager.ServerCertificateValidationCallback+=
			delegate(object sender,System.Security.Cryptography.X509Certificates.X509Certificate certificate,
									System.Security.Cryptography.X509Certificates.X509Chain chain,
									System.Net.Security.SslPolicyErrors sslPolicyErrors) {
				return true;
			};
		}

		/// <summary>This is set to 60 second</summary>
		private void timerRefreshLastSynchTime_Tick(object sender,EventArgs e) {
			textDateTimeLastRun.Text=MobileSyncDateTimeLastRun.ToShortDateString()+" "+MobileSyncDateTimeLastRun.ToShortTimeString();
			textProgress.Text=StatusMessage;
		}

		private void butCurrentWorkstation_Click(object sender,EventArgs e) {
			textMobileSynchWorkStation.Text=System.Environment.MachineName.ToUpper();
		}
		
		private void butSavePreferences_Click(object sender,EventArgs e) {
			try {
				Prefs.UpdateString(PrefName.MobileSyncServerURL,textMobileSyncServerURL.Text.Trim());
				MobileSyncServerURL=textMobileSyncServerURL.Text.Trim();
				butSavePreferences.Enabled=false;
				if(!TestWebServiceExists()) {
					MsgBox.Show(this,WebServiceUnavailableMessage);
					return;
				}
				InitializeVariables();//payment is checked here.
				if(!PaidCustomer) {
					textSynchMinutes.Text="0";
					butSavePreferences.Enabled=false;
					MsgBox.Show(this,NotPaidMessage);
					return;
				}
				if(!FieldsValid()) {
					return;
				}
				Prefs.UpdateInt(PrefName.MobileSyncIntervalMinutes,PIn.Int(textSynchMinutes.Text.Trim()));
				MobileSyncIntervalMinutes=PrefC.GetInt(PrefName.MobileSyncIntervalMinutes);
				Prefs.UpdateString(PrefName.MobileSyncWorkstationName,textMobileSynchWorkStation.Text.Trim());
				MobileSyncWorkstationName=PrefC.GetStringSilent(PrefName.MobileSyncWorkstationName);
				SetMobileExcludeApptsBeforeDate();
				butSavePreferences.Enabled=false;
				if((MobileUserNameChanged||MobilePasswordChanged)) {
					mb.SetMobileWebUserPassword(RegistrationKey,textMobileUserName.Text.Trim(),textMobilePassword.Text.Trim());
					MobileUserNameChanged=false;
					MobilePasswordChanged=false;
				}
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}

		}

		private void textMobileSyncServerURL_TextChanged(object sender,EventArgs e) {
			butSavePreferences.Enabled=true;
		}

		private void textSynchMinutes_TextChanged(object sender,EventArgs e) {
			butSavePreferences.Enabled=true;
		}

		private void textMobileUserName_TextChanged(object sender,EventArgs e) {
			MobileUserNameChanged=true;
			butSavePreferences.Enabled=true;
		}

		private void textMobilePassword_TextChanged(object sender,EventArgs e) {
			MobilePasswordChanged=true;
			butSavePreferences.Enabled=true;
		}

		private void textDateBefore_TextChanged(object sender,EventArgs e) {
			butSavePreferences.Enabled=true;
		}

		private void textMobileSynchWorkStation_TextChanged(object sender,EventArgs e) {
			butSavePreferences.Enabled=true;
		}

		private bool FieldsValid() {
			if(textDateBefore.errorProvider1.GetError(textDateBefore)!=""
				||textSynchMinutes.errorProvider1.GetError(textSynchMinutes)!="") {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}

			if((MobileUserNameChanged||MobilePasswordChanged)) {
				//Minimum 14 char.  Must contain uppercase, lowercase, numbers, and symbols. Valid symbols are: !@#$%^&+= 
				bool IsMatch=Regex.IsMatch(textMobileUserName.Text.Trim(),"^.*(?=.{10,})(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=]).*$");
				if(!IsMatch) {
					MsgBox.Show(this,"The username must be atleast 10 characters and must contain an uppercase character, a lowercase character, a number and one of the following symbols:!@#$%^&+=");
					return false;
				}
				if(RegistrationKey==textMobileUserName.Text.Trim()) {
					MsgBox.Show(this,"The username cannot be the same as the Registration Key");
					return false;
				}
				if(textMobilePassword.Text.Trim()=="") {
					MsgBox.Show(this,"The password cannot be empty");
					return false;
				}
			}

			if(textMobileSynchWorkStation.Text.Trim()=="") {
					MsgBox.Show(this,"WorkStation field cannot be empty");
					return false;
				}
			return true;
	}
		
		/// <summary>
		/// For testing only
		/// </summary>
		private static void CreatePatients(int PatientCount) {
			for(int i=0;i<PatientCount;i++) {
				Patient newPat=new Patient();
				newPat.LName="Mathew"+i;
				newPat.FName="Dennis"+i;
				newPat.Address="Address Line 1.Address Line 1___"+i;
				newPat.Address2="Address Line 2. Address Line 2__"+i;
				newPat.AddrNote="Lives off in far off Siberia Lives off in far off Siberia"+i;
				newPat.AdmitDate=new DateTime(1985,3,3).AddDays(i);
				newPat.ApptModNote="Flies from Siberia on specially chartered flight piloted by goblins:)"+i;
				newPat.AskToArriveEarly=1555;
				newPat.BillingType=3;
				newPat.ChartNumber="111111"+i;
				newPat.City="NL";
				newPat.ClinicNum=i;
				newPat.CreditType="A";
				newPat.DateFirstVisit=new DateTime(1985,3,3).AddDays(i);
				newPat.Email="dennis.mathew________________seb@siberiacrawlmail.com";
				newPat.HmPhone="416-222-5678";
				newPat.WkPhone="416-222-5678";
				newPat.Zip="M3L 2L9";
				newPat.WirelessPhone="416-222-5678";
				newPat.Birthdate=new DateTime(1970,3,3).AddDays(i);
				Patients.Insert(newPat,false);
				//set Guarantor field the same as PatNum
				Patient patOld=newPat.Copy();
				newPat.Guarantor=newPat.PatNum;
				Patients.Update(newPat,patOld);
			}
		}

		/// <summary>
		/// For testing only
		/// </summary>
		private static void CreateAppointments(int AppointmentCount) {
			long[] patNumArray=Patients.GetAllPatNums();
			DateTime appdate= new DateTime(2010,12,1,11,0,0);
			for(int i=0;i<patNumArray.Length;i++) {
				appdate=appdate.AddDays(2);
				for(int j=0;j<AppointmentCount;j++) {
					Appointment apt=new Appointment();
					apt.PatNum=patNumArray[i];
					apt.DateTimeArrived=appdate;
					apt.DateTimeAskedToArrive=appdate;
					apt.DateTimeDismissed=appdate;
					apt.DateTimeSeated=appdate;
					apt.Note="some notenote noten otenotenot enotenot enote"+j;
					apt.IsNewPatient=true;
					apt.ProvNum=3;
					apt.AptStatus=ApptStatus.Scheduled;
					apt.AptDateTime=appdate;
					Appointments.Insert(apt);
				}
			}
		}

		/// <summary>
		/// For testing only
		/// </summary>
		private static void CreatePrescriptions(int PrescriptionCount) {
			long[] patNumArray=Patients.GetAllPatNums();
			for(int i=0;i<patNumArray.Length;i++) {
				for(int j=0;j<PrescriptionCount;j++) {
					RxPat rxpat= new RxPat();
					rxpat.Drug="VicodinA VicodinB VicodinC"+j;
					rxpat.Disp="50.50";
					rxpat.IsControlled=true;
					rxpat.PatNum=patNumArray[i];
					rxpat.RxDate=new DateTime(2010,12,1,11,0,0);
					RxPats.Insert(rxpat);
				}
			}
		}
		
		/// <summary>
		/// If the MobileExcludeApptsBeforeDate is not specified then it defaults to a year before the current time.
		/// </summary>
		private void SetMobileExcludeApptsBeforeDate() {
			if(textDateBefore.Text.Trim()=="") {
				MobileExcludeApptsBeforeDate=DateTime.Now.AddYears(-1);
			}
			else {
				Prefs.UpdateDateT(PrefName.MobileExcludeApptsBeforeDate,PIn.Date(textDateBefore.Text));
				MobileExcludeApptsBeforeDate=PIn.Date(textDateBefore.Text);
			}
		}

		private void butSync_Click(object sender,EventArgs e) {
			try {
				if(!TestWebServiceExists()) {
					MsgBox.Show(this,WebServiceUnavailableMessage);
					return;
				}
				InitializeVariables();//payment is checked here.
				if(!PaidCustomer) {
					MsgBox.Show(this,NotPaidMessage);
					return;
				}
				if(MobileSyncDateTimeLastRun.Year<1880) {
					MsgBox.Show(this,FullSynchNotRunMessage);
					return;
				}
				
				if(mb.GetCustomerNum(RegistrationKey)==0) {
					MsgBox.Show(this,IncorrectRegKeyMessage);
					return;
				}
				/*if(!FieldsValid()) {
					return;
				}*/
				SetMobileExcludeApptsBeforeDate();
				/*if(MobileSyncIntervalMinutes==0) {
					MsgBox.Show(this,"Minutes Between Synch must be set to greater than zero");
					return;
				}*/
				if(MobileSyncDateTimeLastRun.Year<1880) {
					MsgBox.Show(this,FullSynchNotRunMessage);
					return;
				}
				Cursor=Cursors.WaitCursor;
				try {
					SynchNow();
					textDateTimeLastRun.Text=MobileSyncDateTimeLastRun.ToShortDateString()+" "+MobileSyncDateTimeLastRun.ToShortTimeString();
				}
				catch(Exception ex) {
					Cursor=Cursors.Default;
					MessageBox.Show(ex.Message);
				}
				Cursor=Cursors.Default;
				MsgBox.Show(this,SyncCompletedMessage);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butFullSync_Click(object sender,EventArgs e) {
			try {
				if(!TestWebServiceExists()) {
					MsgBox.Show(this,WebServiceUnavailableMessage);
					return;
				}
				InitializeVariables();//payment is checked here.

				if(!PaidCustomer) {
					MsgBox.Show(this,NotPaidMessage);
					return;
				}
				if(mb.GetCustomerNum(RegistrationKey)==0) {
					MsgBox.Show(this,IncorrectRegKeyMessage);
					return;
				}
				/*if(!FieldsValid()) {
					return;
				}*/
				SetMobileExcludeApptsBeforeDate();
				if(!MsgBox.Show(this,true,"This will be time consuming. Continue anyway?")) {
					return;
				}
				Cursor=Cursors.WaitCursor;
				try {
					SynchFull();
				}
				catch(Exception ex) {
					Cursor=Cursors.Default;
					MessageBox.Show(ex.Message);
				}
				Cursor=Cursors.Default;
				MsgBox.Show(this,SyncCompletedMessage);
				}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}


















	}
}