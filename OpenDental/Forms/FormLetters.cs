using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;
using System.IO;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormLetters : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.ListBox listLetters;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butEdit;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		//private bool localChanged;
		private System.Drawing.Printing.PrintDocument pd2;
		//private bool bodyChanged;
		private OpenDental.ODtextBox textBody;
		//private int pagesPrinted=0;
		private Patient PatCur;
		private Letter LetterCur;
		///<summary>If this is not null, then this letter will be addressed to the referral rather than the patient.</summary>
		public Referral ReferralCur;
		//<summary>Only used if FuchsOptionsOn</summary>
		//private string ExtraImageToPrint;

		///<summary></summary>
		public FormLetters(){
			InitializeComponent();// Required for Windows Form Designer support
			PatCur=new Patient();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLetters));
			this.butCancel = new OpenDental.UI.Button();
			this.listLetters = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butEdit = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.textBody = new OpenDental.ODtextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(758,633);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(79,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// listLetters
			// 
			this.listLetters.Location = new System.Drawing.Point(20,133);
			this.listLetters.Name = "listLetters";
			this.listLetters.Size = new System.Drawing.Size(164,277);
			this.listLetters.TabIndex = 2;
			this.listLetters.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listLetters_MouseDown);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(19,114);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124,14);
			this.label1.TabIndex = 3;
			this.label1.Text = "Letters";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEdit.Autosize = true;
			this.butEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEdit.CornerRadius = 4F;
			this.butEdit.Image = global::OpenDental.Properties.Resources.editPencil;
			this.butEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEdit.Location = new System.Drawing.Point(106,414);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(79,26);
			this.butEdit.TabIndex = 8;
			this.butEdit.Text = "&Edit";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(19,414);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79,26);
			this.butAdd.TabIndex = 7;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(22,12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(711,32);
			this.label2.TabIndex = 12;
			this.label2.Text = resources.GetString("label2.Text");
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(19,448);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(79,26);
			this.butDelete.TabIndex = 16;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textBody
			// 
			this.textBody.Location = new System.Drawing.Point(206,133);
			this.textBody.Multiline = true;
			this.textBody.Name = "textBody";
			this.textBody.QuickPasteType = OpenDentBusiness.QuickPasteType.Letter;
			this.textBody.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textBody.Size = new System.Drawing.Size(630,486);
			this.textBody.TabIndex = 18;
			// 
			// FormLetters
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(858,674);
			this.Controls.Add(this.textBody);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butEdit);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listLetters);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLetters";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Letters";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormLetters_Closing);
			this.Load += new System.EventHandler(this.FormLetterSetup_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormLetterSetup_Load(object sender, System.EventArgs e) {
			//if(PrefC.GetBool(PrefName.LettersIncludeReturnAddress")){
			//	checkIncludeRet.Checked=true;
			//}
			//if(PrefC.GetBool(PrefName.FuchsOptionsOn")) {
				//buttonTYDMF.Visible = true;
				//buttonTYREF.Visible = true;
			//}
			FillList();
		}

		private void FillList(){
			Letters.RefreshCache();
			listLetters.Items.Clear();
			for(int i=0;i<Letters.List.Length;i++){
				listLetters.Items.Add(Letters.List[i].Description);
			}
			//no items are initially selected
		}

		private void listLetters_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if (listLetters.SelectedIndex == -1) {
				return;
			}
			if (!WarnOK())
				return;
			LetterCur = Letters.List[listLetters.SelectedIndex];
			StringBuilder str = new StringBuilder();
			//return address
			//if (checkIncludeRet.Checked) {
				str.Append(PrefC.GetString(PrefName.PracticeTitle) + "\r\n");
				str.Append(PrefC.GetString(PrefName.PracticeAddress) + "\r\n");
				if (PrefC.GetString(PrefName.PracticeAddress2) != "")
					str.Append(PrefC.GetString(PrefName.PracticeAddress2) + "\r\n");
				str.Append(PrefC.GetString(PrefName.PracticeCity) + ", ");
				str.Append(PrefC.GetString(PrefName.PracticeST) + "  ");
				str.Append(PrefC.GetString(PrefName.PracticeZip) + "\r\n");
			//}
			//else {
			//	str.Append("\r\n\r\n\r\n\r\n");
			//}
			str.Append("\r\n\r\n");
			//address
			if (ReferralCur == null) {
				str.Append(PatCur.FName + " " + PatCur.MiddleI + " " + PatCur.LName + "\r\n");
				str.Append(PatCur.Address + "\r\n");
				if (PatCur.Address2 != "")
					str.Append(PatCur.Address2 + "\r\n");
				str.Append(PatCur.City + ", " + PatCur.State + "  " + PatCur.Zip);
			}
			else {
				str.Append(Referrals.GetNameFL(ReferralCur.ReferralNum) + "\r\n");
				str.Append(ReferralCur.Address + "\r\n");
				if (ReferralCur.Address2 != "")
					str.Append(ReferralCur.Address2 + "\r\n");
				str.Append(ReferralCur.City + ", " + ReferralCur.ST + "  " + ReferralCur.Zip);
			}
			str.Append("\r\n\r\n\r\n\r\n");
			//date
			str.Append(DateTimeOD.Today.ToLongDateString() + "\r\n");
			//referral RE
			if (ReferralCur != null) {
				str.Append(Lan.g(this, "RE Patient: ") + PatCur.GetNameFL() + "\r\n");
			}
			str.Append("\r\n");
			//greeting
			str.Append(Lan.g(this, "Dear "));
			if (ReferralCur == null) {
				if (CultureInfo.CurrentCulture.Name == "en-GB") {
					if (PatCur.Salutation != "")
						str.Append(PatCur.Salutation);
					else {
						if (PatCur.Gender == PatientGender.Female) {
							str.Append("Ms. " + PatCur.LName);
						}
						else {
							str.Append("Mr. " + PatCur.LName);
						}
					}
				}
				else {
					if (PatCur.Salutation != "")
						str.Append(PatCur.Salutation);
					else if (PatCur.Preferred != "")
						str.Append(PatCur.Preferred);
					else
						str.Append(PatCur.FName);
				}
			}
			else {//referral
				str.Append(ReferralCur.FName);
			}
			str.Append(",\r\n\r\n");
			//body text
			str.Append(LetterCur.BodyText);
			//closing
			if (CultureInfo.CurrentCulture.Name == "en-GB") {
				str.Append("\r\n\r\nYours sincerely,\r\n\r\n\r\n\r\n");
			}
			else {
				str.Append("\r\n\r\n" + Lan.g(this, "Sincerely,") + "\r\n\r\n\r\n\r\n");
			}
			if (PrefC.GetBool(PrefName.FuchsOptionsOn)) {
				str.Append("Dr. Fuchs");
			}
			else {
				str.Append(PrefC.GetString(PrefName.PracticeTitle));
			}
			textBody.Text = str.ToString();
			//bodyChanged = false;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			if(!WarnOK())
				return;
			FormLetterEdit FormLE=new FormLetterEdit();
			FormLE.LetterCur=new Letter();
			FormLE.IsNew=true;
			FormLE.ShowDialog();
			FillList();
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			if(!WarnOK())
				return;
			if(listLetters.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
      FormLetterEdit FormLE=new FormLetterEdit();
			FormLE.LetterCur=Letters.List[listLetters.SelectedIndex];//just in case
			FormLE.ShowDialog();
			FillList();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listLetters.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete letter permanently for all patients?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			Letters.Delete(Letters.List[listLetters.SelectedIndex]);
			FillList();
		}

		//private void checkIncludeRet_Click(object sender, System.EventArgs e) {	
		//	Prefs.UpdateBool(PrefName.LettersIncludeReturnAddress",checkIncludeRet.Checked);
		//	localChanged=true;
		//	CacheL.Refresh(InvalidType.Prefs);
		//}

		///<summary>If the user has selected a letter, and then edited it in the main textbox, this warns them before continuing.</summary>
		private bool WarnOK(){
			/*if(bodyChanged){
				if(!MsgBox.Show(this,true,
					"Any changes you made to the letter you were working on will be lost.  Do you wish to continue?"))
				{
					return false;
				}
			}*/
			return true;
		}

		private void textBody_TextChanged(object sender, System.EventArgs e) {
			//bodyChanged=true;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormLetters_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			//if(localChanged){
			//	DataValid.SetInvalid(InvalidType.Letters);
			//}
		}

		


		

		

		

		

		

		

		

		


	}
}





















