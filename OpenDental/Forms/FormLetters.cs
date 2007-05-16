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

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormLetters : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.ListBox listLetters;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkIncludeRet;
		private OpenDental.UI.Button butEdit;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private bool localChanged;
		private System.Drawing.Printing.PrintDocument pd2;
		private bool bodyChanged;
		private OpenDental.UI.Button butPrint;
		private OpenDental.ODtextBox textBody;
		private int pagesPrinted=0;
		private Patient PatCur;
		private Letter LetterCur;
		///<summary>If this is not null, then this letter will be addressed to the referral rather than the patient.</summary>
		public Referral ReferralCur;

		///<summary></summary>
		public FormLetters(Patient patCur){
			InitializeComponent();// Required for Windows Form Designer support
			PatCur=patCur;
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
			this.checkIncludeRet = new System.Windows.Forms.CheckBox();
			this.butDelete = new OpenDental.UI.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.butPrint = new OpenDental.UI.Button();
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
			this.label2.Size = new System.Drawing.Size(167,86);
			this.label2.TabIndex = 12;
			this.label2.Text = "This creates a letter for a single patient.  For complex letters to multiple pati" +
    "ents, export data from a report and merge it with a Word or OpenOffice template." +
    "";
			// 
			// checkIncludeRet
			// 
			this.checkIncludeRet.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIncludeRet.Location = new System.Drawing.Point(206,1);
			this.checkIncludeRet.Name = "checkIncludeRet";
			this.checkIncludeRet.Size = new System.Drawing.Size(272,24);
			this.checkIncludeRet.TabIndex = 15;
			this.checkIncludeRet.Text = "Include Return Address";
			this.checkIncludeRet.Click += new System.EventHandler(this.checkIncludeRet_Click);
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
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(654,633);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(79,26);
			this.butPrint.TabIndex = 17;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// textBody
			// 
			this.textBody.AcceptsReturn = true;
			this.textBody.Location = new System.Drawing.Point(206,24);
			this.textBody.Multiline = true;
			this.textBody.Name = "textBody";
			this.textBody.QuickPasteType = OpenDentBusiness.QuickPasteType.Letter;
			this.textBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBody.Size = new System.Drawing.Size(630,595);
			this.textBody.TabIndex = 18;
			// 
			// FormLetters
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(858,674);
			this.Controls.Add(this.textBody);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.checkIncludeRet);
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
			if(((Pref)PrefB.HList["LettersIncludeReturnAddress"]).ValueString=="1")
				checkIncludeRet.Checked=true;
			FillList();
		}

		private void FillList(){
			Letters.Refresh();
			listLetters.Items.Clear();
			for(int i=0;i<Letters.List.Length;i++){
				listLetters.Items.Add(Letters.List[i].Description);
			}
			//no items are initially selected
		}

		private void listLetters_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listLetters.SelectedIndex==-1){
				return;
			}
			if(!WarnOK())
				return;
			LetterCur=Letters.List[listLetters.SelectedIndex];
			StringBuilder str=new StringBuilder();
			//return address
			if(checkIncludeRet.Checked){
				str.Append(((Pref)PrefB.HList["PracticeTitle"]).ValueString+"\r\n");
				str.Append(((Pref)PrefB.HList["PracticeAddress"]).ValueString+"\r\n");
				if(((Pref)PrefB.HList["PracticeAddress2"]).ValueString!="")
					str.Append(((Pref)PrefB.HList["PracticeAddress2"]).ValueString+"\r\n");
				str.Append(((Pref)PrefB.HList["PracticeCity"]).ValueString+", ");
				str.Append(((Pref)PrefB.HList["PracticeST"]).ValueString+"  ");
				str.Append(((Pref)PrefB.HList["PracticeZip"]).ValueString+"\r\n");
			}
			else{
				str.Append("\r\n\r\n\r\n\r\n");
			}
			str.Append("\r\n\r\n");
			//address
			if(ReferralCur==null){
				str.Append(PatCur.FName+" "+PatCur.MiddleI+" "+PatCur.LName+"\r\n");
				str.Append(PatCur.Address+"\r\n");
				if(PatCur.Address2!="")
					str.Append(PatCur.Address2+"\r\n");
				str.Append(PatCur.City+", "+PatCur.State+"  "+PatCur.Zip);
			}
			else{
				str.Append(Referrals.GetNameFL(ReferralCur.ReferralNum)+"\r\n");
				str.Append(ReferralCur.Address+"\r\n");
				if(ReferralCur.Address2!="")
					str.Append(ReferralCur.Address2+"\r\n");
				str.Append(ReferralCur.City+", "+ReferralCur.ST+"  "+ReferralCur.Zip);
			}
			str.Append("\r\n\r\n\r\n\r\n");
			//date
			str.Append(DateTime.Today.ToLongDateString()+"\r\n");
			//referral RE
			if(ReferralCur!=null){
				str.Append(Lan.g(this,"RE Patient: ")+PatCur.GetNameFL()+"\r\n");
			}
			str.Append("\r\n");
			//greeting
			str.Append(Lan.g(this,"Dear "));
			if(ReferralCur==null){
				if(CultureInfo.CurrentCulture.Name=="en-GB"){
					if(PatCur.Salutation!="")
						str.Append(PatCur.Salutation);
					else{
						if(PatCur.Gender==PatientGender.Female){
							str.Append("Ms. "+PatCur.LName);
						}
						else{
							str.Append("Mr. "+PatCur.LName);
						}
					}
				}
				else{
					if(PatCur.Salutation!="")
						str.Append(PatCur.Salutation);
					else if(PatCur.Preferred!="")
						str.Append(PatCur.Preferred);
					else
						str.Append(PatCur.FName);
				}
			}
			else{//referral
				str.Append(ReferralCur.FName);
			}
			str.Append(",\r\n\r\n");
			//body text
			str.Append(LetterCur.BodyText);
			//closing
			if(CultureInfo.CurrentCulture.Name=="en-GB"){
				str.Append("\r\n\r\nYours sincerely,\r\n\r\n\r\n\r\n");
			}
			else{
				str.Append("\r\n\r\n"+Lan.g(this,"Sincerely,")+"\r\n\r\n\r\n\r\n");
			}
			str.Append(((Pref)PrefB.HList["PracticeTitle"]).ValueString);
			textBody.Text=str.ToString();
			bodyChanged=false;
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

		private void checkIncludeRet_Click(object sender, System.EventArgs e) {	
			Prefs.UpdateBool("LettersIncludeReturnAddress",checkIncludeRet.Checked);
			localChanged=true;
			Prefs.Refresh();
		}

		///<summary>If the user has selected a letter, and then edited it in the main textbox, this warns them before continuing.</summary>
		private bool WarnOK(){
			if(bodyChanged){
				if(MessageBox.Show(Lan.g(this
					,"Any changes you made to the letter you were working on will be lost.  "
					+"Do you wish to continue?"),"",MessageBoxButtons.OKCancel)
					!=DialogResult.OK)
				{
					return false;
				}
			}
			return true;
		}

		private void textBody_TextChanged(object sender, System.EventArgs e) {
			bodyChanged=true;
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			if(textBody.Text=="") {
				MsgBox.Show(this,"Letter not allowed to be blank.");
				return;
			}
			pd2=new PrintDocument();
			pd2.PrintPage+=new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.OriginAtMargins=true;
			if(!Printers.SetPrinter(pd2,PrintSituation.Default)){
				return;
			}
			try{
				pd2.Print();
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
			Commlog CommlogCur=new Commlog();
			CommlogCur.CommDateTime=DateTime.Now;
			CommlogCur.CommType=CommItemType.Misc;
			CommlogCur.PatNum=PatCur.PatNum;
			CommlogCur.Note="Letter sent";
			if(LetterCur!=null) {
				CommlogCur.Note+=": "+LetterCur.Description+". ";
			}
			FormCommItem FormCI=new FormCommItem(CommlogCur);
			FormCI.IsNew=true;
			FormCI.ShowDialog();
			//this window now closes regardless of whether the user saved the comm item.
			DialogResult=DialogResult.OK;
		}

		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){//raised for each page to be printed.
			Graphics grfx=ev.Graphics;
			ev.PageSettings.Margins=new Margins(100,100,80,80);
			//ev.PageSettings.PrinterSettings.
			//ev.
			//grfx.TextRenderingHint=TextRenderingHint.
			//StringFormat format=new StringFormat();
			//format..FormatFlags=StringFormatFlags.
			grfx.DrawString(textBody.Text,new Font(FontFamily.GenericSansSerif,11),Brushes.Black
				,new RectangleF(0,0,ev.MarginBounds.Width,ev.MarginBounds.Height));
			pagesPrinted++;
			ev.HasMorePages=false;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormLetters_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(localChanged){
				DataValid.SetInvalid(InvalidTypes.Letters);
			}
		}


		

		

		

		

		

		

		

		


	}
}





















