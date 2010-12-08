using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormMobile:Form {
		public FormMobile() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormMobile_Load(object sender,EventArgs e) {
			textPath.Text=PrefC.GetString(PrefName.MobileSyncPath);
			//textDateStart.Text=DateTime.Today.AddDays(-14).ToShortDateString();
			//textDateEnd.Text=DateTime.Today.AddDays(14).ToShortDateString();
			#if DEBUG
			textDateBefore.Text=DateTime.Today.AddDays(-14).ToShortDateString();
			#endif
			DateTime dt=PrefC.GetDateT(PrefName.MobileSyncDateTimeLastRun);
			if(dt.Year>1880){
				textDateTimeLastRun.Text=dt.ToShortDateString()+" "+dt.ToShortTimeString();
			}
		}

		private void textPath_TextChanged(object sender,EventArgs e) {
			if(Directory.Exists(textPath.Text)){
				labelValid.Visible=false;
				butSync.Enabled=true;
				butFullSync.Enabled=true;
			}
			else{
				labelValid.Visible=true;
				butSync.Enabled=false;
				butFullSync.Enabled=false;
			}
		}

		private void butSync_Click(object sender,EventArgs e) {
			if(PrefC.GetDateT(PrefName.MobileSyncDateTimeLastRun).Year<1880){
				MsgBox.Show(this,"Sync has never been run.  You must do a full sync first.");
				return;
			}
			Sync(false);
		}

		private void butFullSync_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"This will be time consuming when the mobile device imports.  Continue anyway?")){
				return;
			}
			Sync(true);
		}
		
		private void Sync(bool isFull){
			//no need to see if directory is valid because button will be disabled if not.
			if( textDateBefore.errorProvider1.GetError(textDateBefore)!="")
			//	|| textDateEnd.errorProvider1.GetError(textDateEnd)!=""
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string path=textPath.Text;
			#if DEBUG
				path=@"E:\My Documents\HTC emulator My Documents\Business\Open Dental";
			#endif
			XmlWriterSettings settings=new XmlWriterSettings();
			//settings.ConformanceLevel=ConformanceLevel.Fragment;
			settings.OmitXmlDeclaration=true;
			settings.Encoding=Encoding.UTF8;
			settings.Indent=true;
			settings.IndentChars="   ";
			StringBuilder strBuild=new StringBuilder();
			XmlWriter writer=XmlWriter.Create(strBuild,settings);
			writer.WriteRaw("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");//It kept writing utf-16, so this is my workaround.
			writer.WriteStartElement("InToMobile");
			writer.WriteAttributeString("MainVersion",Application.ProductVersion);
			writer.WriteAttributeString("MinimumMobileVersion","6.3.0.0");
			if(isFull){
				writer.WriteAttributeString("FullSync","true");
			}
			#region patients
			DateTime dateTimeLastSync;
			if(isFull){
				dateTimeLastSync=DateTime.MinValue;
			}
			else{
				dateTimeLastSync=PrefC.GetDateT(PrefName.MobileSyncDateTimeLastRun);
			}
			List<Patient> patientsToSynch=Patients.GetChangedSince(dateTimeLastSync);
			int objCount=patientsToSynch.Count;
			Dictionary<long,string> carrierNames=Carriers.GetCarrierNames(patientsToSynch);
			Patient pat;
			for(int i=0;i<patientsToSynch.Count;i++){
				pat=patientsToSynch[i];
				writer.WriteStartElement("patient");
				if(pat.PatStatus==PatientStatus.Deleted){
					//we won't store deleted patients
					writer.WriteAttributeString("action","delete");
				}
				else{
					writer.WriteAttributeString("action","write");
				}
				writer.WriteElementString("PatNum",pat.PatNum.ToString());
				writer.WriteElementString("LName",pat.LName);
				writer.WriteElementString("FName",pat.FName);
				writer.WriteElementString("Preferred",pat.Preferred);
				writer.WriteElementString("PatStatus",((int)pat.PatStatus).ToString());
				writer.WriteElementString("Gender",((int)pat.Gender).ToString());
				writer.WriteElementString("Position",((int)pat.Position).ToString());
				writer.WriteElementString("Birthdate",POut.Date(pat.Birthdate,false));
				writer.WriteElementString("Address",pat.Address);
				writer.WriteElementString("Address2",pat.Address2);
				writer.WriteElementString("City",pat.City);
				writer.WriteElementString("State",pat.State);
				writer.WriteElementString("HmPhone",pat.HmPhone);
				writer.WriteElementString("WkPhone",pat.WkPhone);
				writer.WriteElementString("WirelessPhone",pat.WirelessPhone);
				writer.WriteElementString("Guarantor",pat.Guarantor.ToString());
				writer.WriteElementString("CreditType",pat.CreditType);
				writer.WriteElementString("FamFinUrgNote",pat.FamFinUrgNote);
				writer.WriteElementString("MedUrgNote",pat.MedUrgNote);
				writer.WriteElementString("PrimaryInsurance",carrierNames[pat.PatNum]);
				writer.WriteEndElement();//patient
			}
			#endregion patients
			#region appointments
			DateTime dateBefore=PIn.Date(textDateBefore.Text);
			List<Appointment> apptsToSynch=Appointments.GetChangedSince(dateTimeLastSync,dateBefore);
			objCount+=apptsToSynch.Count;
			Appointment apt;
			for(int i=0;i<apptsToSynch.Count;i++){
				apt=apptsToSynch[i];
				writer.WriteStartElement("appointment");
				writer.WriteAttributeString("action","write");
				writer.WriteElementString("AptNum",apt.AptNum.ToString());
				writer.WriteElementString("PatNum",apt.PatNum.ToString());
				writer.WriteElementString("AptStatus",((int)apt.AptStatus).ToString());
				writer.WriteElementString("Pattern",apt.Pattern);
				writer.WriteElementString("Confirmed",apt.Confirmed.ToString());
				writer.WriteElementString("Op",apt.Op.ToString());
				writer.WriteElementString("Note",apt.Note);
				writer.WriteElementString("ProvNum",apt.ProvNum.ToString());
				writer.WriteElementString("ProvHyg",apt.ProvHyg.ToString());
				writer.WriteElementString("AptDateTime",POut.DateT(apt.AptDateTime,false));
				writer.WriteElementString("ProcDescript",apt.ProcDescript);
				writer.WriteElementString("IsHygiene",POut.Bool(apt.IsHygiene));
				writer.WriteEndElement();//appointment
			}
			#endregion appointments

			writer.WriteEndElement();//InToMobile
			writer.WriteEndDocument();
			writer.Close();
			if(objCount>0){
				int fileNumber=PrefC.GetInt(PrefName.MobileSyncLastFileNumber)+1;
				string filePath=Path.Combine(path,"in"+fileNumber+".xml");
				File.WriteAllText(filePath,strBuild.ToString(),Encoding.UTF8);
				Prefs.UpdateLong(PrefName.MobileSyncLastFileNumber,fileNumber);
			}
			DateTime now=MiscData.GetNowDateTime();//server time
			Prefs.UpdateDateT(PrefName.MobileSyncDateTimeLastRun,now);
			textDateTimeLastRun.Text=now.ToShortDateString()+" "+now.ToShortTimeString();
			//we will not trigger a refresh on other computers because this is the only one doing the sync.
			Cursor=Cursors.Default;
			if(objCount==0){
				MsgBox.Show(this,"Done. No sync necessary.");
			}
			else{
				MessageBox.Show(Lan.g(this,"Done.  Objects exported: ")+objCount.ToString());
			}
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		private void FormMobile_FormClosing(object sender,FormClosingEventArgs e) {
			if(textPath.Text!=PrefC.GetString(PrefName.MobileSyncPath)){
				if(MsgBox.Show(this,true,"Save changes to path?")){
					Prefs.UpdateString(PrefName.MobileSyncPath,textPath.Text);
					DataValid.SetInvalid(InvalidType.Prefs);
				}
			}
		}

	

		

		

		
	}
}