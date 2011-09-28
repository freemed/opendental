using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrOnlineAccess:Form {
		public Patient PatCur;
		///<summary>A copy of the original patient object, as it was when this form was first opened.</summary>
		private Patient PatOld;
		private MobileWeb.Mobile mb;

		public FormEhrOnlineAccess() {
			InitializeComponent();
			mb=new MobileWeb.Mobile();
		}

		private void FormOnlineAccess_Load(object sender,EventArgs e) {
			PatOld=PatCur.Copy();
			textOnlineUsername.Text=PatCur.FName+PatCur.PatNum;//if patient's first name changes, they will need a new link.
			textOnlinePassword.Text=PatCur.OnlinePassword;
			if(PatCur.OnlinePassword!="") {//if a password was already filled in
				butGiveAccess.Text="Remove Online Access";
				textOnlinePassword.ReadOnly=false;
			}
		}

		private void butGiveAccess_Click(object sender,EventArgs e) {
			string interval=PrefC.GetStringSilent(PrefName.MobileSyncIntervalMinutes);
			if(interval=="" || interval=="0") {//not a paid customer or chooses not to synch
				MessageBox.Show("Synch must be setup first from the Tools menu, Mobile and Patient Portal Synch.");
				return;
			}
			//we won't check PrefName.MobileSyncWorkstationName because we are forcing the synch
			if(System.Environment.MachineName.ToUpper()!=PrefC.GetStringSilent(PrefName.MobileSyncWorkstationName).ToUpper()) {
				//Since GetStringSilent returns "" before OD is connected to db, this gracefully loops out
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,
					"Warning.  Workstation is not entered in the Tools menu, Mobile and Patient Portal Synch.  No automatic synch is taking place.  Continue anyway?")) 
				{
					return;
				}
			}
			if(PrefC.GetDate(PrefName.MobileExcludeApptsBeforeDate).Year<1880) {
				MessageBox.Show("Full Synch must be run first first from the Tools menu, Mobile and Patient Portal Synch.");
				return;
			}
			if(butGiveAccess.Text=="Provide Online Access") {//When form open opens with a blank password
				Cursor=Cursors.WaitCursor;
				//1. Fill password.
				textOnlinePassword.Text=GenerateRandomPassword(8,10);//won't save until OK or Print
				//2. Fill link.
				textOnlineLink.Text=GetPatientPortalLink();
				//3. Reset timestamps for this patient to trigger all their objects to upload with the next synch
				LabPanels.ResetTimeStamps(PatCur.PatNum);
				Diseases.ResetTimeStamps(PatCur.PatNum);
				Allergies.ResetTimeStamps(PatCur.PatNum);
				MedicationPats.ResetTimeStamps(PatCur.PatNum);
				Statements.ResetTimeStamps(PatCur.PatNum);
				//Documents.ResetTimeStamps(PatCur.PatNum);
				//4. Make the password editable in case they want to change it.
				textOnlinePassword.ReadOnly=false;
				//5. Save password to db
				PatCur.OnlinePassword=textOnlinePassword.Text;
				Patients.Update(PatCur,PatOld);
				PatOld.OnlinePassword=textOnlinePassword.Text;//so that subsequent Updates will work.
				//6. Force a synch
				FormMobile.SynchFromMain(true);
				//7. Insert EhrMeasureEvent
				EhrMeasureEvent newMeasureEvent = new EhrMeasureEvent();
				newMeasureEvent.DateTEvent=DateTime.Now;
				newMeasureEvent.EventType=EhrMeasureEventType.OnlineAccessProvided;
				newMeasureEvent.PatNum=PatCur.PatNum;
				newMeasureEvent.MoreInfo="";
				EhrMeasureEvents.Insert(newMeasureEvent);
				//8. Rename button
				butGiveAccess.Text="Remove Online Access";
				Cursor=Cursors.Default;
			}
			else {//remove access
				Cursor=Cursors.WaitCursor;
				//1. Clear password
				textOnlinePassword.Text="";
				//2. Make in uneditable
				textOnlinePassword.ReadOnly=true;
				//3. Save password to db
				PatCur.OnlinePassword=textOnlinePassword.Text;
				Patients.Update(PatCur,PatOld);
				PatOld.OnlinePassword=textOnlinePassword.Text;
				//5. Force a synch
				FormMobile.SynchFromMain(true);
				//no event to insert
				//6. Rename button
				butGiveAccess.Text="Provide Online Access";
				Cursor=Cursors.Default;
			}
		}

		private void butGetLink_Click(object sender,EventArgs e) {
			if(textOnlinePassword.ReadOnly) {
				MessageBox.Show("Please use the Provide Online Access button first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			textOnlineLink.Text=GetPatientPortalLink();
			Cursor=Cursors.Default;
		}

		private void butOpen_Click(object sender,EventArgs e) {
			if(textOnlineLink.Text=="") {
				MessageBox.Show("Please use Get Link first.");
				return;
			}
			//No reason to force a synch to server.
			try {
				System.Diagnostics.Process.Start(textOnlineLink.Text);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		///<summary>This takes about 3-5 seconds to run, so we don't run it automatically when first opening the form.</summary>
		private string GetPatientPortalLink() {
			mb.Url=PrefC.GetString(PrefName.MobileSyncServerURL);
			if(!mb.ServiceExists()) {
				return "";
			}
			try {
				long customerNum=mb.GetCustomerNum(PrefC.GetString(PrefName.RegistrationKey));
				string patientPortalLink=mb.GetPatientPortalAddress(PrefC.GetString(PrefName.RegistrationKey))+"?DentalOfficeID="+customerNum;
				return patientPortalLink;
			}
			catch {
				return "";
			}
		}
		
		private void butGenerate_Click(object sender,EventArgs e) {
			if(textOnlinePassword.ReadOnly) {
				MessageBox.Show("Please use the Provide Online Access button first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			textOnlinePassword.Text=GenerateRandomPassword(8,10);
			PatCur.OnlinePassword=textOnlinePassword.Text;
			Patients.Update(PatCur,PatOld);
			PatOld.OnlinePassword=textOnlinePassword.Text;//so that subsequent Updates will work.
			string interval=PrefC.GetStringSilent(PrefName.MobileSyncIntervalMinutes);
			if(interval=="" || interval=="0") {//not a paid customer or chooses not to synch
				Cursor=Cursors.Default;
				MessageBox.Show("Synch must be setup first from the Tools menu, Mobile and Patient Portal Synch.");
				return;
			}
			//we won't check PrefName.MobileSyncWorkstationName because we are forcing the synch
			if(System.Environment.MachineName.ToUpper()!=PrefC.GetStringSilent(PrefName.MobileSyncWorkstationName).ToUpper()) {
				Cursor=Cursors.Default;
				//Since GetStringSilent returns "" before OD is connected to db, this gracefully loops out
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,
					"Warning.  Workstation is not entered in the Tools menu, Mobile and Patient Portal Synch.  No automatic synch is taking place.  Continue anyway?")) {
					return;
				}
			}
			if(PrefC.GetDate(PrefName.MobileExcludeApptsBeforeDate).Year<1880) {
				Cursor=Cursors.Default;
				MessageBox.Show("Full Synch must be run first first from the Tools menu, Mobile and Patient Portal Synch.");
				return;
			}
			FormMobile.SynchFromMain(true); 
			Cursor=Cursors.Default;
		}

		private void butSynch_Click(object sender,EventArgs e) {
			//The only other places on this form where a synch happens is when the 'Provide Online Access' or 'Generate New' buttons are clicked.
			if(textOnlinePassword.Text!="") {
				if(!PasswordIsValid()) {
					return;
				}
			}
			PatCur.OnlinePassword=textOnlinePassword.Text;
			Patients.Update(PatCur,PatOld);
			PatOld.OnlinePassword=textOnlinePassword.Text;//so that subsequent Updates will work.
			string interval=PrefC.GetStringSilent(PrefName.MobileSyncIntervalMinutes);
			if(interval=="" || interval=="0") {//not a paid customer or chooses not to synch
				MessageBox.Show("Synch must be setup first from the Tools menu, Mobile and Patient Portal Synch.");
				return;
			}
			//we won't check PrefName.MobileSyncWorkstationName because we are forcing the synch
			if(System.Environment.MachineName.ToUpper()!=PrefC.GetStringSilent(PrefName.MobileSyncWorkstationName).ToUpper()) {
				//Since GetStringSilent returns "" before OD is connected to db, this gracefully loops out
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,
					"Warning.  Workstation is not entered in the Tools menu, Mobile and Patient Portal Synch.  No automatic synch is taking place.  Continue anyway?")) {
					return;
				}
			}
			if(PrefC.GetDate(PrefName.MobileExcludeApptsBeforeDate).Year<1880) {
				MessageBox.Show("Full Synch must be run first first from the Tools menu, Mobile and Patient Portal Synch.");
				return;
			}
			FormMobile.SynchFromMain(true);
		}

		///<summary>Should generate a reasonably random password at least 8 char long and containing at least one uppercase, one lowercase, and one number.</summary>
		private static string GenerateRandomPassword(int minLength,int maxLength) {
			// chracters like o(letter 0), 0 (Zero),l (letter l),1(one) etc are avoided because they can be ambigious
			string PASSWORD_CHARS_LCASE="abcdefgijkmnopqrstwxyz";
			string PASSWORD_CHARS_UCASE="ABCDEFGHJKLMNPQRSTWXYZ";
			string PASSWORD_CHARS_NUMERIC="23456789";
			string PASSWORD_CHARS_SPECIAL="*$-?_=!{}/";//&, +, and % are specifically excluded because they are known to cause failure on the mobile version.
			// Make sure that input parameters are valid.
			if(minLength <=0 || maxLength <=0 || minLength > maxLength)
				return null;
			// Create a local array containing supported password characters
			// grouped by types. You can remove character groups from this
			// array, but doing so will weaken the password strength.
			char[][] charGroups=new char[][]{
            PASSWORD_CHARS_LCASE.ToCharArray(),
            PASSWORD_CHARS_UCASE.ToCharArray(),
            PASSWORD_CHARS_NUMERIC.ToCharArray(),
            PASSWORD_CHARS_SPECIAL.ToCharArray()};
			// Use this array to track the number of unused characters in each
			// character group.
			int[] charsLeftInGroup=new int[charGroups.Length];
			// Initially, all characters in each group are not used.
			for(int i=0;i<charsLeftInGroup.Length;i++) {
				charsLeftInGroup[i]=charGroups[i].Length;
			}
			// Use this array to track (iterate through) unused character groups.
			int[] leftGroupsOrder=new int[charGroups.Length];
			// Initially, all character groups are not used.
			for(int i=0;i<leftGroupsOrder.Length;i++) {
				leftGroupsOrder[i]=i;
			}
			// Because we cannot use the default randomizer, which is based on the
			// current time (it will produce the same "random" number within a
			// second), we will use a random number generator to seed the
			// randomizer.
			// Use a 4-byte array to fill it with random bytes and convert it then
			// to an integer value.
			byte[] randomBytes=new byte[4];
			// Generate 4 random bytes.
			RNGCryptoServiceProvider rng=new RNGCryptoServiceProvider();
			rng.GetBytes(randomBytes);
			// Convert 4 bytes into a 32-bit integer value.
			int seed=(randomBytes[0] & 0x7f) << 24 |
				randomBytes[1]         << 16 |
				randomBytes[2]         <<  8 |
				randomBytes[3];
			// Now, this is real randomization.
			Random random=new Random(seed);
			// This array will hold password characters.
			char[] password=null;
			// Allocate appropriate memory for the password.
			if(minLength < maxLength) {
				password=new char[random.Next(minLength,maxLength+1)];
			}
			else {
				password=new char[minLength];
			}
			// Index of the next character to be added to password.
			int nextCharIdx;
			// Index of the next character group to be processed.
			int nextGroupIdx;
			// Index which will be used to track not processed character groups.
			int nextLeftGroupsOrderIdx;
			// Index of the last non-processed character in a group.
			int lastCharIdx;
			// Index of the last non-processed group.
			int lastLeftGroupsOrderIdx=leftGroupsOrder.Length - 1;
			// Generate password characters one at a time.
			for(int i=0;i<password.Length;i++) {
				// If only one character group remained unprocessed, process it;
				// otherwise, pick a random character group from the unprocessed
				// group list. To allow a special character to appear in the
				// first position, increment the second parameter of the Next
				// function call by one, i.e. lastLeftGroupsOrderIdx + 1.
				if(lastLeftGroupsOrderIdx==0) {
					nextLeftGroupsOrderIdx=0;
				}
				else {
					nextLeftGroupsOrderIdx=random.Next(0,lastLeftGroupsOrderIdx);
				}
				// Get the actual index of the character group, from which we will
				// pick the next character.
				nextGroupIdx=leftGroupsOrder[nextLeftGroupsOrderIdx];
				// Get the index of the last unprocessed characters in this group.
				lastCharIdx=charsLeftInGroup[nextGroupIdx] - 1;
				// If only one unprocessed character is left, pick it; otherwise,
				// get a random character from the unused character list.
				if(lastCharIdx==0) {
					nextCharIdx=0;
				}
				else {
					nextCharIdx=random.Next(0,lastCharIdx+1);
				}
				// Add this character to the password.
				password[i]=charGroups[nextGroupIdx][nextCharIdx];
				// If we processed the last character in this group, start over.
				if(lastCharIdx==0) {
					charsLeftInGroup[nextGroupIdx]=charGroups[nextGroupIdx].Length;
					// There are more unprocessed characters left.
				}
				else {
					// Swap processed character with the last unprocessed character
					// so that we don't pick it until we process all characters in
					// this group.
					if(lastCharIdx !=nextCharIdx) {
						char temp=charGroups[nextGroupIdx][lastCharIdx];
						charGroups[nextGroupIdx][lastCharIdx]=charGroups[nextGroupIdx][nextCharIdx];
						charGroups[nextGroupIdx][nextCharIdx]=temp;
					}
					// Decrement the number of unprocessed characters in
					// this group.
					charsLeftInGroup[nextGroupIdx]--;
				}
				// If we processed the last group, start all over.
				if(lastLeftGroupsOrderIdx==0) {
					lastLeftGroupsOrderIdx=leftGroupsOrder.Length - 1;
					// There are more unprocessed groups left.
				}
				else {
					// Swap processed group with the last unprocessed group
					// so that we don't pick it until we process all groups.
					if(lastLeftGroupsOrderIdx !=nextLeftGroupsOrderIdx) {
						int temp=leftGroupsOrder[lastLeftGroupsOrderIdx];
						leftGroupsOrder[lastLeftGroupsOrderIdx]=
                                leftGroupsOrder[nextLeftGroupsOrderIdx];
						leftGroupsOrder[nextLeftGroupsOrderIdx]=temp;
					}
					// Decrement the number of unprocessed groups.
					lastLeftGroupsOrderIdx--;
				}
			}
			// Convert password characters into a string and return the result.
			return new string(password);
		}

		private void butPrint_Click(object sender,EventArgs e) {
			if(textOnlineLink.Text=="") {
				MsgBox.Show(this,"Online Access Link required. Please press Get Link.");
				return;
			}
			if(textOnlinePassword.Text==""){
				MessageBox.Show("Password required. Please generate a new password.");
				return;
			}
			if(!PasswordIsValid()) {//this gives a messagebox if invalid
				return;
			}
			//Then, print the info that the patient will be given in order for them to log in online.
			PrintDocument pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(25,25,40,40);
			if(pd.DefaultPageSettings.PrintableArea.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			try {
				#if !DEBUG
					FormRpPrintPreview pView = new FormRpPrintPreview();
					pView.printPreviewControl2.Document=pd;
					pView.ShowDialog();
				#else
						if(PrinterL.SetPrinter(pd,PrintSituation.Default)) {
							pd.Print();
						}
				#endif
			}
			catch {
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}

		private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=e.MarginBounds;
			//new Rectangle(50,40,800,1035);//Some printers can handle up to 1042
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font font=new Font("Arial",10,FontStyle.Regular);
			int yPos=bounds.Top+100;
			int center=bounds.X+bounds.Width/2;
			text="Online Access";
			g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,font).Width/2,yPos);
			yPos+=50;
			text="Website: "+textOnlineLink.Text;
			g.DrawString(text,font,Brushes.Black,bounds.Left+100,yPos);
			yPos+=25;
			text="Username: "+textOnlineUsername.Text;
			g.DrawString(text,font,Brushes.Black,bounds.Left+100,yPos);
			yPos+=25;
			text="Password: "+textOnlinePassword.Text;
			g.DrawString(text,font,Brushes.Black,bounds.Left+100,yPos);
			g.Dispose();
			e.HasMorePages=false;
		}

		///<summary>Used when click OK, and also when the Synch or Print buttons are clicked.</summary>
		private bool PasswordIsValid() {
			if(textOnlinePassword.Text.Length<8) {
				MessageBox.Show(this,"Password must be at least 8 characters long.");
				return false;
			}
			if(!Regex.IsMatch(textOnlinePassword.Text,"[A-Z]+")) {
				MessageBox.Show(this,"Password must contain an uppercase letter.");
				return false;
			}
			if(!Regex.IsMatch(textOnlinePassword.Text,"[a-z]+")) {
				MessageBox.Show(this,"Password must contain an lowercase letter.");
				return false;
			}
			if(!Regex.IsMatch(textOnlinePassword.Text,"[0-9]+")) {
				MessageBox.Show(this,"Password must contain a number.");
				return false;
			}
			return true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textOnlinePassword.Text!="") {
				if(!PasswordIsValid()) {
					return;
				}
			}
			PatCur.OnlinePassword=textOnlinePassword.Text;//already saved if used the Provide Online Access button, Generate New button, or Synch button.  This is just in case they subsequently changed it or removed it.
			//Do not force synch to server.  User has been informed to synch manually if they change the password.
			Patients.Update(PatCur,PatOld);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

		

		

		

	

	}
}
