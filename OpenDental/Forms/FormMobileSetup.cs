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
		private static int MobileSyncIntervalMinutes;
		private static DateTime MobileExcludeApptsBeforeDate;
		private static String StatusMessage="";
		private static int BatchSize=100;

		public FormMobileSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormMobileSetup_Load(object sender,EventArgs e) {
			InitializeVariables();
			textDateTimeLastRun.Text=MobileSyncDateTimeLastRun.ToShortDateString()+" "+MobileSyncDateTimeLastRun.ToShortTimeString();
			textMobileSyncServerURL.Text=MobileSyncServerURL;
			textSynchMinutes.Text=MobileSyncIntervalMinutes+"";
			textDateBefore.Text=MobileExcludeApptsBeforeDate.ToShortDateString();
			butSavePreferences.Enabled=false;
		}

		internal void SetParentFormReference(FormOpenDental frmOD) {
			this.frmOD=frmOD;
		}
		private static void InitializeVariables() {
			RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
			MobileSyncServerURL=PrefC.GetString(PrefName.MobileSyncServerURL);
			MobileSyncDateTimeLastRun=PrefC.GetDateT(PrefName.MobileSyncDateTimeLastRun);
			MobileExcludeApptsBeforeDate=PrefC.GetDateT(PrefName.MobileExcludeApptsBeforeDate);
			bool PaidCustomer=true; // check for payment here
			if(PaidCustomer) {
				MobileSyncIntervalMinutes=PrefC.GetInt(PrefName.MobileSyncIntervalMinutes);
			}else{
				MobileSyncIntervalMinutes=0;
				Prefs.UpdateInt(PrefName.MobileSyncIntervalMinutes,MobileSyncIntervalMinutes);
			}
		}

		private static void Synch(DateTime GetChangedSince) {
			try {
				#if DEBUG
					IgnoreCertificateErrors();// used with faulty certificates only while debugging.
				#endif
				if(!TestWebServiceExists()) {
					return;
				}
				if(mb.GetCustomerNum(RegistrationKey)==0) {
				return;
				}
				//CreatePatients(100000);
				//CreateAppointments(10); // for each patient
				//CreatePrescriptions(10);// for each patient
				DateTime MobileSyncDateTimeLastRunNew= MiscData.GetNowDateTime();
				List<long> patNumList=Patientms.GetChangedSincePatNums(GetChangedSince);
				//SynchPatients(patNumList);
				List<long> aptNumList=Appointmentms.GetChangedSinceAptNums(GetChangedSince,MobileExcludeApptsBeforeDate);
				//SynchAppointments(aptNumList);
				List<long> rxNumList=RxPatms.GetChangedSinceRxNums(GetChangedSince);
				SynchPrescriptions(rxNumList);
				Prefs.UpdateDateT(PrefName.MobileSyncDateTimeLastRun,MobileSyncDateTimeLastRunNew);
				MobileSyncDateTimeLastRun=MobileSyncDateTimeLastRunNew;
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
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
				// Charge the customer!
				//MsgBox.Show(this,"You must be a paid customer to use this feature");
				//return;
			}
			if(MobileSyncDateTimeLastRun.Year<1880) {
				//MsgBox.Show(this,"Sync has never been run.  You must do a full sync first.");
				//return;
			}
			if(DateTime.Now>MobileSyncDateTimeLastRun.AddMinutes(MobileSyncIntervalMinutes)) {
				Synch(MobileSyncDateTimeLastRun);
			}
		}

		private static void SynchFull() {
			DateTime FullSynchDateTime=new DateTime(1880,1,1);
			//delete all records on server here
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
				StatusMessage=start + " records of "+patNumList.Count +" Patient Uploads";
				Application.DoEvents();// allows textProgress to be refreshed 
				mb.SynchPatients(RegistrationKey,ChangedPatientmList.ToArray());
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
				StatusMessage=start + " records of "+AptNumList.Count +" Appointmnet Uploads";
				Application.DoEvents();// allows textProgress to be refreshed 
				mb.SynchAppointments(RegistrationKey,ChangedAppointmentmList.ToArray());
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
				StatusMessage=start + " records of "+RxNumList.Count +" Prescriptions Uploads";
				Application.DoEvents();// allows textProgress to be refreshed 
				mb.SynchPrescriptions(RegistrationKey,ChangedRxList.ToArray());
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

		private void timerRefreshLastSynchTime_Tick(object sender,EventArgs e) {
			textDateTimeLastRun.Text=MobileSyncDateTimeLastRun.ToShortDateString()+" "+MobileSyncDateTimeLastRun.ToShortTimeString();
			textProgress.Text=StatusMessage;
		}

		private void butSavePreferences_Click(object sender,EventArgs e) {
			Prefs.UpdateString(PrefName.MobileSyncServerURL,textMobileSyncServerURL.Text.Trim());
			MobileSyncServerURL=textMobileSyncServerURL.Text.Trim();
			if(!FieldsValid()) {
				return;
			}
			SetMobileExcludeApptsBeforeDate();
			butSavePreferences.Enabled=false;
			mb.SetMobileWebUserPassword(RegistrationKey,textMobileUserName.Text.Trim(),textMobilePassword.Text.Trim());
			bool PaidCustomer=true; // check for payment here
			if(PaidCustomer) {
				Prefs.UpdateInt(PrefName.MobileSyncIntervalMinutes,PIn.Int(textSynchMinutes.Text));
				MobileSyncIntervalMinutes=PIn.Int(textSynchMinutes.Text);
				//start timer on main form
				if(MobileSyncIntervalMinutes!=0) {
					frmOD.StartTimerWebHostSynch();
				}
			}
			else {
				MobileSyncIntervalMinutes=0;
				Prefs.UpdateInt(PrefName.MobileSyncIntervalMinutes,MobileSyncIntervalMinutes);
			}

		}

		private void textMobileSyncServerURL_TextChanged(object sender,EventArgs e) {
			butSavePreferences.Enabled=true;
		}

		private void textSynchMinutes_TextChanged(object sender,EventArgs e) {
			butSavePreferences.Enabled=true;
		}

		private void textMobileUserName_TextChanged(object sender,EventArgs e) {
			butSavePreferences.Enabled=true;
		}

		private void textMobilePassword_TextChanged(object sender,EventArgs e) {
			butSavePreferences.Enabled=true;
		}

		private bool FieldsValid() {
			if(textDateBefore.errorProvider1.GetError(textDateBefore)!=""
				||textSynchMinutes.errorProvider1.GetError(textSynchMinutes)!="") {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}

			//Minimum 14 char.  Must contain uppercase, lowercase, numbers, and symbols. Valid symbols are: !@#$%^&+= 
			bool IsMatch=Regex.IsMatch(textMobileUserName.Text.Trim(),"^.*(?=.{14,})(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=]).*$");
			if(!IsMatch) {
				//MsgBox.Show(this,"The username must be atleast 14 characters and must contain an uppercase character, a lowercase character, a number and one of the following symbols:!@#$%^&+=");
				//return false;
			}
			if(RegistrationKey==textMobileUserName.Text.Trim()){
				MsgBox.Show(this,"The username cannot be the same as the Registration Key");
				return false;
			}
			if(textMobilePassword.Text.Trim()=="") {
				//MsgBox.Show(this,"The password cannot be empty");
				//return false;
			}

			return true;
	}
		
		/// <summary>
		/// For testing only
		/// </summary>
		private void CreatePatients(int PatientCount) {
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
				newPat.County= "county"+i;
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
		private void CreateAppointments(int AppointmentCount) {
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

		private bool CanProceed() {
			bool proceed=true;
			if(!FieldsValid()) {
				return false;
			}
			if(MobileSyncIntervalMinutes==0) {
				// Charge the customer!
				MsgBox.Show(this,"You must be a paid customer to use this feature");
				//return false;
			}
			if(MobileSyncDateTimeLastRun.Year<1880) {
				MsgBox.Show(this,"Sync has never been run.  You must do a full sync first.");
				return false;
			}
			if(!TestWebServiceExists()) {
				MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
				return false;
			}
			if(mb.GetCustomerNum(RegistrationKey)==0) {
				MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
				return false;
			}
			SetMobileExcludeApptsBeforeDate();
			return proceed;
		}
		
		private void butSync_Click(object sender,EventArgs e) {
			if(!CanProceed()) {
				return;
			}
			if(MobileSyncDateTimeLastRun.Year<1880) {
				MsgBox.Show(this,"Sync has never been run.  You must do a full sync first.");
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
			MsgBox.Show(this,"Sync Completed");
		}

		private void butFullSync_Click(object sender,EventArgs e) {
			if(!CanProceed()) {
				return;
			}
			if(!MsgBox.Show(this,true,"This will be time consuming.  Continue anyway?")) {
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
			MsgBox.Show(this,"Sync Completed");
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}












	}
}