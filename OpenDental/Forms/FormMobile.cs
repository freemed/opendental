using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;


namespace OpenDental {
	public partial class FormMobile:Form {
		private static MobileWeb.Mobile mb=new MobileWeb.Mobile();
		private static PatientPortal.PatientPortal pp=new PatientPortal.PatientPortal();
		private static int BatchSize=100;
		///<summary>This variable prevents the synching methods from being called when a previous synch is in progress.</summary>
		private static bool IsSynching;
		///<summary>True if a pref was saved and the other workstations need to have their cache refreshed when this form closes.</summary>
		private bool changed;

		private enum SynchEntity {
			patient,
			appointment,
			prescription,
			provider,
			medication,
			medicationpat
		}

		public FormMobile() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormMobileSetup_Load(object sender,EventArgs e) {
			textMobileSyncServerURL.Text=PrefC.GetString(PrefName.MobileSyncServerURL);
			textSynchMinutes.Text=PrefC.GetInt(PrefName.MobileSyncIntervalMinutes).ToString();
			textDateBefore.Text=PrefC.GetDate(PrefName.MobileExcludeApptsBeforeDate).ToShortDateString();
			textMobileSynchWorkStation.Text=PrefC.GetString(PrefName.MobileSyncWorkstationName);
			textMobileUserName.Text=PrefC.GetString(PrefName.MobileUserName);
			textMobilePassword.Text="";//not stored locally, and not pulled from web server
			DateTime lastRun=PrefC.GetDateT(PrefName.MobileSyncDateTimeLastRun);
			if(lastRun.Year>1880) {
				textDateTimeLastRun.Text=lastRun.ToShortDateString()+" "+lastRun.ToShortTimeString();
			}
			//Web server is not contacted when loading this form.  That would be too slow.
		}

		private void butCurrentWorkstation_Click(object sender,EventArgs e) {
			textMobileSynchWorkStation.Text=System.Environment.MachineName.ToUpper();
		}

		private void butSave_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			if(!SavePrefs()) {
				Cursor=Cursors.Default;
				return;
			}
			Cursor=Cursors.Default;
			MsgBox.Show(this,"Done");
		}

		///<summary>Returns false if validation failed.  This also makes sure the web service exists, the customer is paid, and the registration key is correct.</summary>
		private bool SavePrefs(){
			//validation
			if( textSynchMinutes.errorProvider1.GetError(textSynchMinutes)!=""
				|| textDateBefore.errorProvider1.GetError(textDateBefore)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}
			if(textMobileSynchWorkStation.Text=="") {
				MsgBox.Show(this,"WorkStation cannot be empty");
				return false;
			}
			// the text field is read because the keyed in values have not been saved yet
			if(textMobileSyncServerURL.Text.Contains("192.168.0.196") || textMobileSyncServerURL.Text.Contains("localhost")) {
				IgnoreCertificateErrors();// done so that TestWebServiceExists() does not thow an error.
			}
			if(!TestWebServiceExists()) {
				MsgBox.Show(this,"Web service not found.");
				return false;
			}
			if(mb.GetCustomerNum(PrefC.GetString(PrefName.RegistrationKey))==0) {
				MsgBox.Show(this,"Registration key is incorrect.");
				return false;
			}
			if(!VerifyPaidCustomer()) {
				return false;
			}
			//Minimum 10 char.  Must contain uppercase, lowercase, numbers, and symbols. Valid symbols are: !@#$%^&+= 
			//The set of symbols checked was far too small, not even including periods, commas, and parentheses.
			//So I rewrote it all.  New error messages say exactly what's wrong with it.
//to do: Dennis, make sure that ANY character is allowed on the server end.  
			//For example, a space, a {, and a Chinese character would all be allowed in username.
			if(textMobileUserName.Text.Length<10){
				MsgBox.Show(this,"User Name must be at least 10 characters long.");
				return false;
			}
			if(!Regex.IsMatch(textMobileUserName.Text,"[A-Z]+")){
				MsgBox.Show(this,"User Name must contain an uppercase letter.");
				return false;
			}
			if(!Regex.IsMatch(textMobileUserName.Text,"[a-z]+")){
				MsgBox.Show(this,"User Name must contain an lowercase letter.");
				return false;
			}
			if(!Regex.IsMatch(textMobileUserName.Text,"[0-9]+")){
				MsgBox.Show(this,"User Name must contain a number.");
				return false;
			}
			if(!Regex.IsMatch(textMobileUserName.Text,"[^0-9a-zA-Z]+")){//absolutely anything except number, lower or upper.
				MsgBox.Show(this,"User Name must contain punctuation or symbols.");
				return false;
			}
			if(textDateBefore.Text==""){//default to one year if empty
				textDateBefore.Text=DateTime.Today.AddYears(-1).ToShortDateString();
				//not going to bother informing user.  They can see it.
			}
			//save to db------------------------------------------------------------------------------------
			if(Prefs.UpdateString(PrefName.MobileSyncServerURL,textMobileSyncServerURL.Text)
				| Prefs.UpdateInt(PrefName.MobileSyncIntervalMinutes,PIn.Int(textSynchMinutes.Text))//blank entry allowed
				| Prefs.UpdateString(PrefName.MobileExcludeApptsBeforeDate,POut.Date(PIn.Date(textDateBefore.Text),false))//blank 
				| Prefs.UpdateString(PrefName.MobileSyncWorkstationName,textMobileSynchWorkStation.Text)
				| Prefs.UpdateString(PrefName.MobileUserName,textMobileUserName.Text)
			){
				changed=true;
				Prefs.RefreshCache();
			}
			//Username and password-----------------------------------------------------------------------------
			mb.SetMobileWebUserPassword(PrefC.GetString(PrefName.RegistrationKey),textMobileUserName.Text.Trim(),textMobilePassword.Text.Trim());
//todo: Dennis, if password is blank, you need to make the server not update the password for this user.
//That is changed behavior.
			return true;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!SavePrefs()) {
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete all your data from our server?  This happens automatically before a full synch.")) {
				return;
			}
			mb.DeleteAllRecords(PrefC.GetString(PrefName.RegistrationKey));
			MsgBox.Show(this,"Done");
		}

		private void butFullSync_Click(object sender,EventArgs e) {
			if(!SavePrefs()) {
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This will be time consuming. Continue anyway?")) {
				return;
			}
			//for full synch, delete all records then repopulate.
			mb.DeleteAllRecords(PrefC.GetString(PrefName.RegistrationKey));
			//calculate total number of records------------------------------------------------------------------------------
			DateTime timeSynchStarted=MiscData.GetNowDateTime();
			List<long> patNumList=Patientms.GetChangedSincePatNums(DateTime.MinValue);
			List<long> aptNumList=Appointmentms.GetChangedSinceAptNums(DateTime.MinValue,PrefC.GetDate(PrefName.MobileExcludeApptsBeforeDate));
			List<long> rxNumList=RxPatms.GetChangedSinceRxNums(DateTime.MinValue);
			List<long> provNumList=Providerms.GetChangedSinceProvNums(DateTime.MinValue);
			int totalCount=patNumList.Count+aptNumList.Count+rxNumList.Count+provNumList.Count;
			FormProgress FormP=new FormProgress();
			//start the thread that will perform the upload
			ThreadStart uploadDelegate= delegate { UploadWorker(patNumList,aptNumList,rxNumList,provNumList,ref FormP,timeSynchStarted); };
			Thread workerThread=new Thread(uploadDelegate);
			workerThread.Start();
			//display the progress dialog to the user:
			FormP.MaxVal=(double)totalCount;
			FormP.NumberMultiplication=100;
			FormP.DisplayText="?currentVal of ?maxVal records uploaded";
			FormP.NumberFormat="F0";
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.Cancel) {
				workerThread.Abort();
			}
			IsSynching=false;
			changed=true;
		}

		private void butSync_Click(object sender,EventArgs e) {
			if(!SavePrefs()) {
				return;
			}
			if(PrefC.GetDate(PrefName.MobileExcludeApptsBeforeDate).Year<1880) {
				MsgBox.Show(this,"Full synch has never been run before.");
				return;
			}
			//calculate total number of records------------------------------------------------------------------------------
			DateTime changedSince=PrefC.GetDateT(PrefName.MobileSyncDateTimeLastRun);
			DateTime timeSynchStarted=MiscData.GetNowDateTime();
			List<long> patNumList=Patientms.GetChangedSincePatNums(changedSince);
			List<long> aptNumList=Appointmentms.GetChangedSinceAptNums(changedSince,PrefC.GetDate(PrefName.MobileExcludeApptsBeforeDate));
			List<long> rxNumList=RxPatms.GetChangedSinceRxNums(changedSince);
			List<long> provNumList=Providerms.GetChangedSinceProvNums(changedSince);
			int totalCount=patNumList.Count+aptNumList.Count+rxNumList.Count+provNumList.Count;
			FormProgress FormP=new FormProgress();
			//start the thread that will perform the upload
			ThreadStart uploadDelegate= delegate { UploadWorker(patNumList,aptNumList,rxNumList,provNumList,ref FormP,timeSynchStarted); };
			Thread workerThread=new Thread(uploadDelegate);
			workerThread.Start();
			//display the progress dialog to the user:
			FormP.MaxVal=(double)totalCount;
			FormP.NumberMultiplication=100;
			FormP.DisplayText="?currentVal of ?maxVal records uploaded";
			FormP.NumberFormat="F0";
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.Cancel) {
				workerThread.Abort();
			}
			IsSynching=false;
			changed=true;
		}

		///<summary>This is the function that the worker thread uses to actually perform the upload.  Can also call this method in the ordinary way if the data to be transferred is small.  The timeSynchStarted must be passed in to ensure that no records are skipped due to small time differences.</summary>
		private static void UploadWorker(List<long> patNumList,List<long> aptNumList,List<long> rxNumList,List<long> provNumList,ref FormProgress progressIndicator,DateTime timeSynchStarted) {
			IsSynching=true;
			//patients--------------------------------------------------------------------
			int localBatchSize=BatchSize;
			for(int start=0;start<patNumList.Count;start+=localBatchSize) {
				if((start+localBatchSize)>patNumList.Count) {
					localBatchSize=patNumList.Count-start;
				}
				List<long> blockPatNumList=patNumList.GetRange(start,localBatchSize);
				List<Patientm> changedPatientmList=Patientms.GetMultPats(blockPatNumList);
				mb.SynchPatients(PrefC.GetString(PrefName.RegistrationKey),changedPatientmList.ToArray());
				progressIndicator.CurrentVal+=BatchSize;
			}
			//Appointments-----------------------------------------------------------------
			localBatchSize=BatchSize;
			for(int start=0;start<aptNumList.Count;start+=localBatchSize) {
				if((start+localBatchSize)>aptNumList.Count) {
					localBatchSize=aptNumList.Count-start;
				}
				List<long> blockAptNumList=aptNumList.GetRange(start,localBatchSize);
				List<Appointmentm> changedAppointmentmList=Appointmentms.GetMultApts(blockAptNumList);
				mb.SynchAppointments(PrefC.GetString(PrefName.RegistrationKey),changedAppointmentmList.ToArray());
				progressIndicator.CurrentVal+=BatchSize;
			}
			//Rx----------------------------------------------------------------------------
			localBatchSize=BatchSize;
			for(int start=0;start<rxNumList.Count;start+=localBatchSize) {
				if((start+localBatchSize)>rxNumList.Count) {
					localBatchSize=rxNumList.Count-start;
				}
				List<long> blockRxNumList=rxNumList.GetRange(start,localBatchSize);
				List<RxPatm> changedRxList=RxPatms.GetMultRxPats(blockRxNumList);
				mb.SynchPrescriptions(PrefC.GetString(PrefName.RegistrationKey),changedRxList.ToArray());
				progressIndicator.CurrentVal+=BatchSize;
			}
			//Providers----------------------------------------------------------------------------
			localBatchSize=BatchSize;
			for(int start=0;start<provNumList.Count;start+=localBatchSize) {
				if((start+localBatchSize)>provNumList.Count) {
					localBatchSize=provNumList.Count-start;
				}
				List<long> blockProvNumList=provNumList.GetRange(start,localBatchSize);
				List<Providerm> changedProvList=Providerms.GetMultProviderms(blockProvNumList);
				mb.SynchProviders(PrefC.GetString(PrefName.RegistrationKey),changedProvList.ToArray());
				progressIndicator.CurrentVal+=BatchSize;
			}
			Prefs.UpdateDateT(PrefName.MobileSyncDateTimeLastRun,timeSynchStarted);
			//if( {//MobileSyncDateTimeLastRunNew)) {
				//DataValid.SetInvalid(InvalidType.Prefs);// change values on all machines.  No need since this is the only workstation synching
				//would be troublesome with threading anyway
			//}
			IsSynching=false;
		}

		///<summary>dennis: code for patient portal starts here. This code may be moved to another location later</summary>
		private static void UploadWorkerPatPortal(DateTime changedSince,ref FormProgress FormP,DateTime timeSynchStarted) {
			pp.Url="http://localhost:2923/PatientPortal.asmx";
			IgnoreCertificateErrors();
			IsSynching=true;
			//List<long> medicationNumList=Medicationms.GetChangedSinceMedicationNums(changedSince);
			List<long> medicationPatNumList=MedicationPatms.GetChangedSinceMedicationPatNums(changedSince);
			//SynchGeneric(medicationNumList,SynchEntity.medication,ref FormP);
			SynchGeneric(medicationPatNumList,SynchEntity.medicationpat,ref FormP);
			Prefs.UpdateDateT(PrefName.MobileSyncDateTimeLastRun,timeSynchStarted);
			IsSynching=false;
		}

		/// <summary>
		/// a general function to reduce the amount of code for uploading
		/// </summary>
		private static void SynchGeneric(List<long> PKNumList,SynchEntity entity,ref FormProgress progressIndicator) {
			int LocalBatchSize=BatchSize;
			for(int start=0;start<PKNumList.Count;start+=LocalBatchSize) {
				if((start+LocalBatchSize)>PKNumList.Count) {
					LocalBatchSize=PKNumList.Count-start;
				}
				List<long> BlockPKNumList=PKNumList.GetRange(start,LocalBatchSize);
				switch(entity) {
					case SynchEntity.medication:
					//List<Medicationm> ChangedMedicationList=Medicationms.GetMultMedicationms(BlockPKNumList);
					//pp.Sy.SynchProviders(PrefC.GetString(PrefName.RegistrationKey),ChangedMedicationList.ToArray());
					break;
					case SynchEntity.medicationpat:
						List<MedicationPatm> ChangedMedicationPatList=MedicationPatms.GetMultMedicationPatms(BlockPKNumList);
						pp.SynchMedicationPats(PrefC.GetString(PrefName.RegistrationKey),ChangedMedicationPatList.ToArray());
					break;

				}
				progressIndicator.CurrentVal+=BatchSize;
			}
		}

		/// <summary>An empty method to test if the webservice is up and running. This was made with the intention of testing the correctness of the webservice URL. If an incorrect webservice URL is used in a background thread the exception cannot be handled easily to a point where even a correct URL cannot be keyed in by the user. Because an exception in a background thread closes the Form which spawned it.</summary>
		private static bool TestWebServiceExists() {
			try {
				mb.Url=PrefC.GetString(PrefName.MobileSyncServerURL);
				if(mb.ServiceExists()) {
					return true;
				}
			}
			catch{//(Exception ex) {
				return false;
			}
			return false;
		}

		private bool VerifyPaidCustomer() {
			if(textMobileSyncServerURL.Text.Contains("192.168.0.196") || textMobileSyncServerURL.Text.Contains("localhost")) {
				IgnoreCertificateErrors();
			}
			bool isPaidCustomer=mb.IsPaidCustomer(PrefC.GetString(PrefName.RegistrationKey));
			if(!isPaidCustomer) {
				textSynchMinutes.Text="0";
				Prefs.UpdateInt(PrefName.MobileSyncIntervalMinutes,0);
				changed=true;
				MsgBox.Show(this,"This feature requires a separate monthly payment.  Please call customer support.");
				return false;
			}
			return true;
		}

		///<summary>Only called from FormOpenDental</summary>
		public static void SynchFromMain() {
			if(Application.OpenForms["FormMobile"]!=null) {//tested.  This prevents main synch whenever this form is open.
				return;
			}
			if(IsSynching) {
				return;
			}
			DateTime timeSynchStarted=MiscData.GetNowDateTime();
			if(timeSynchStarted < PrefC.GetDateT(PrefName.MobileSyncDateTimeLastRun).AddMinutes(PrefC.GetInt(PrefName.MobileSyncIntervalMinutes))) {
				return;
			}
			if(PrefC.GetString(PrefName.MobileSyncServerURL).Contains("192.168.0.196") || PrefC.GetString(PrefName.MobileSyncServerURL).Contains("localhost")) {
				IgnoreCertificateErrors();
			}
			if(!TestWebServiceExists()) {
				return;
			}
			//calculate total number of records------------------------------------------------------------------------------
			DateTime changedSince=PrefC.GetDateT(PrefName.MobileSyncDateTimeLastRun);
			List<long> patNumList=Patientms.GetChangedSincePatNums(changedSince);
			List<long> aptNumList=Appointmentms.GetChangedSinceAptNums(changedSince,PrefC.GetDate(PrefName.MobileExcludeApptsBeforeDate));
			List<long> rxNumList=RxPatms.GetChangedSinceRxNums(changedSince);
			List<long> provNumList=Providerms.GetChangedSinceProvNums(changedSince);
			int totalCount=patNumList.Count+aptNumList.Count+rxNumList.Count+provNumList.Count;
			FormProgress FormP=new FormProgress();//but we won't display it.
			FormP.NumberFormat="";
			FormP.DisplayText="";
			//start the thread that will perform the upload
			ThreadStart uploadDelegate= delegate { UploadWorker(patNumList,aptNumList,rxNumList,provNumList,ref FormP,timeSynchStarted); };
			Thread workerThread=new Thread(uploadDelegate);
			workerThread.Start();
		}

		///<summary>Only called from FormOpenDental</summary>
		public static void SynchPatientPortalFromMain() {
			if(Application.OpenForms["FormMobile"]!=null) {//tested.  This prevents main synch whenever this form is open.
				return;
			}
			if(IsSynching) {
				return;
			}
			DateTime timeSynchStarted=MiscData.GetNowDateTime();
			if(timeSynchStarted < PrefC.GetDateT(PrefName.MobileSyncDateTimeLastRun).AddMinutes(PrefC.GetInt(PrefName.MobileSyncIntervalMinutes))) {
				return;
			}
			if(PrefC.GetString(PrefName.MobileSyncServerURL).Contains("192.168.0.196") || PrefC.GetString(PrefName.MobileSyncServerURL).Contains("localhost")) {
				IgnoreCertificateErrors();
			}
			if(!TestWebServiceExists()) {
				return;
			}
			//calculate total number of records------------------------------------------------------------------------------
			DateTime changedSince=PrefC.GetDateT(PrefName.MobileSyncDateTimeLastRun);
			//List<long> medicationNumList=Medicationms.GetChangedSinceMedicationNums(changedSince);
			List<long> medicationPatNumList=MedicationPatms.GetChangedSinceMedicationPatNums(changedSince);
			//int totalCount=medicationNumList.Count+medicationPatNumList.Count;
			int totalCount=medicationPatNumList.Count;
			FormProgress FormP=new FormProgress();//but we won't display it.
			FormP.NumberFormat="";
			FormP.DisplayText="";
			//start the thread that will perform the upload
			ThreadStart uploadDelegate= delegate { UploadWorkerPatPortal(changedSince,ref FormP,timeSynchStarted); };
			Thread workerThread=new Thread(uploadDelegate);
			workerThread.Start();
		}

		#region Testing
		///<summary>This allows the code to continue by not throwing an exception even if there is a problem with the security certificate.</summary>
		private static void IgnoreCertificateErrors() {
			System.Net.ServicePointManager.ServerCertificateValidationCallback+=
			delegate(object sender,System.Security.Cryptography.X509Certificates.X509Certificate certificate,
									System.Security.Cryptography.X509Certificates.X509Chain chain,
									System.Net.Security.SslPolicyErrors sslPolicyErrors) {
				return true;
			};
		}
		
		/// <summary>For testing only</summary>
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

		/// <summary>For testing only</summary>
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

		/// <summary>For testing only</summary>
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
		#endregion Testing

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		private void FormMobile_FormClosed(object sender,FormClosedEventArgs e) {
			if(changed) {
				DataValid.SetInvalid(InvalidType.Prefs);
			}
		}

		


















	}
}