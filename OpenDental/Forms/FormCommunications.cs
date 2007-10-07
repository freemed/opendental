using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using System.IO;
using System.Text;
using System.Diagnostics;
using CodeBase;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormCommunications:System.Windows.Forms.Form {
		private OpenDental.UI.Button butQuest;
		private OpenDental.UI.Button butLabel;
		private GroupBox groupBox1;
		private OpenDental.UI.Button butLetterMerge;
		private OpenDental.UI.Button butLetterSimple;
		private OpenDental.UI.Button butEmail;
		private GroupBox groupBox2;
		private OpenDental.UI.Button butLabelRef;
		private OpenDental.UI.Button butEmailRef;
		private OpenDental.UI.Button butLetterSimpleRef;
		private IContainer components;
		public Patient PatCur;
		private TextBox textReferral;
		public bool ViewingInRecall;
		private OpenDental.UI.Button butStationary;
		private ToolTip toolTip1;
		///<summary>Can be null.</summary>
		private Referral ReferralCur;

		///<summary></summary>
		public FormCommunications()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCommunications));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butStationary = new OpenDental.UI.Button();
			this.butLetterMerge = new OpenDental.UI.Button();
			this.butLetterSimple = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textReferral = new System.Windows.Forms.TextBox();
			this.butLabelRef = new OpenDental.UI.Button();
			this.butEmailRef = new OpenDental.UI.Button();
			this.butLetterSimpleRef = new OpenDental.UI.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.butQuest = new OpenDental.UI.Button();
			this.butLabel = new OpenDental.UI.Button();
			this.butEmail = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butStationary);
			this.groupBox1.Controls.Add(this.butLetterMerge);
			this.groupBox1.Controls.Add(this.butLetterSimple);
			this.groupBox1.Location = new System.Drawing.Point(12,25);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(106,110);
			this.groupBox1.TabIndex = 88;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Letter to Patient";
			// 
			// butStationary
			// 
			this.butStationary.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butStationary.Autosize = true;
			this.butStationary.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butStationary.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butStationary.CornerRadius = 4F;
			this.butStationary.Location = new System.Drawing.Point(8,79);
			this.butStationary.Name = "butStationary";
			this.butStationary.Size = new System.Drawing.Size(90,25);
			this.butStationary.TabIndex = 54;
			this.butStationary.Text = "Stationery";
			this.toolTip1.SetToolTip(this.butStationary,resources.GetString("butStationary.ToolTip"));
			this.butStationary.Click += new System.EventHandler(this.butStationary_Click);
			// 
			// butLetterMerge
			// 
			this.butLetterMerge.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLetterMerge.Autosize = true;
			this.butLetterMerge.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLetterMerge.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLetterMerge.CornerRadius = 4F;
			this.butLetterMerge.Location = new System.Drawing.Point(8,48);
			this.butLetterMerge.Name = "butLetterMerge";
			this.butLetterMerge.Size = new System.Drawing.Size(90,25);
			this.butLetterMerge.TabIndex = 51;
			this.butLetterMerge.Text = "Merge";
			this.butLetterMerge.Click += new System.EventHandler(this.butLetterMerge_Click);
			// 
			// butLetterSimple
			// 
			this.butLetterSimple.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLetterSimple.Autosize = true;
			this.butLetterSimple.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLetterSimple.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLetterSimple.CornerRadius = 4F;
			this.butLetterSimple.Location = new System.Drawing.Point(8,17);
			this.butLetterSimple.Name = "butLetterSimple";
			this.butLetterSimple.Size = new System.Drawing.Size(90,25);
			this.butLetterSimple.TabIndex = 50;
			this.butLetterSimple.Text = "Simple";
			this.butLetterSimple.Click += new System.EventHandler(this.butLetterSimple_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textReferral);
			this.groupBox2.Controls.Add(this.butLabelRef);
			this.groupBox2.Controls.Add(this.butEmailRef);
			this.groupBox2.Controls.Add(this.butLetterSimpleRef);
			this.groupBox2.Location = new System.Drawing.Point(134,25);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(186,189);
			this.groupBox2.TabIndex = 91;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "To Referral";
			// 
			// textReferral
			// 
			this.textReferral.Location = new System.Drawing.Point(6,19);
			this.textReferral.Name = "textReferral";
			this.textReferral.ReadOnly = true;
			this.textReferral.Size = new System.Drawing.Size(174,20);
			this.textReferral.TabIndex = 92;
			// 
			// butLabelRef
			// 
			this.butLabelRef.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLabelRef.Autosize = true;
			this.butLabelRef.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabelRef.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabelRef.CornerRadius = 4F;
			this.butLabelRef.Image = ((System.Drawing.Image)(resources.GetObject("butLabelRef.Image")));
			this.butLabelRef.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabelRef.Location = new System.Drawing.Point(6,124);
			this.butLabelRef.Name = "butLabelRef";
			this.butLabelRef.Size = new System.Drawing.Size(90,25);
			this.butLabelRef.TabIndex = 91;
			this.butLabelRef.Text = "Label";
			this.butLabelRef.Click += new System.EventHandler(this.butLabelRef_Click);
			// 
			// butEmailRef
			// 
			this.butEmailRef.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEmailRef.Autosize = true;
			this.butEmailRef.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEmailRef.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEmailRef.CornerRadius = 4F;
			this.butEmailRef.Location = new System.Drawing.Point(6,155);
			this.butEmailRef.Name = "butEmailRef";
			this.butEmailRef.Size = new System.Drawing.Size(90,25);
			this.butEmailRef.TabIndex = 90;
			this.butEmailRef.Text = "E-mail";
			this.butEmailRef.Click += new System.EventHandler(this.butEmailRef_Click);
			// 
			// butLetterSimpleRef
			// 
			this.butLetterSimpleRef.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLetterSimpleRef.Autosize = true;
			this.butLetterSimpleRef.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLetterSimpleRef.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLetterSimpleRef.CornerRadius = 4F;
			this.butLetterSimpleRef.Location = new System.Drawing.Point(6,47);
			this.butLetterSimpleRef.Name = "butLetterSimpleRef";
			this.butLetterSimpleRef.Size = new System.Drawing.Size(89,25);
			this.butLetterSimpleRef.TabIndex = 51;
			this.butLetterSimpleRef.Text = "Simple Letter";
			this.butLetterSimpleRef.Click += new System.EventHandler(this.butLetterSimpleRef_Click);
			// 
			// butQuest
			// 
			this.butQuest.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butQuest.Autosize = true;
			this.butQuest.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butQuest.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butQuest.CornerRadius = 4F;
			this.butQuest.Location = new System.Drawing.Point(20,211);
			this.butQuest.Name = "butQuest";
			this.butQuest.Size = new System.Drawing.Size(90,25);
			this.butQuest.TabIndex = 90;
			this.butQuest.Text = "Questionnaire";
			this.butQuest.Click += new System.EventHandler(this.butQuest_Click);
			// 
			// butLabel
			// 
			this.butLabel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLabel.Autosize = true;
			this.butLabel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabel.CornerRadius = 4F;
			this.butLabel.Image = ((System.Drawing.Image)(resources.GetObject("butLabel.Image")));
			this.butLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabel.Location = new System.Drawing.Point(20,141);
			this.butLabel.Name = "butLabel";
			this.butLabel.Size = new System.Drawing.Size(90,25);
			this.butLabel.TabIndex = 89;
			this.butLabel.Text = "Label";
			this.butLabel.Click += new System.EventHandler(this.butLabel_Click);
			// 
			// butEmail
			// 
			this.butEmail.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEmail.Autosize = true;
			this.butEmail.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEmail.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEmail.CornerRadius = 4F;
			this.butEmail.Location = new System.Drawing.Point(20,172);
			this.butEmail.Name = "butEmail";
			this.butEmail.Size = new System.Drawing.Size(90,25);
			this.butEmail.TabIndex = 87;
			this.butEmail.Text = "E-mail";
			this.butEmail.Click += new System.EventHandler(this.butEmail_Click);
			// 
			// FormCommunications
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(332,267);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butQuest);
			this.Controls.Add(this.butLabel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butEmail);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCommunications";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Communications";
			this.Load += new System.EventHandler(this.FormCommunications_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormCommunications_Load(object sender,EventArgs e) {
			ReferralCur=Referrals.GetReferralForPat(PatCur.PatNum);
			if(ReferralCur==null){
				butLetterSimpleRef.Enabled=false;
				butLabelRef.Enabled=false;
				butEmailRef.Enabled=false;
			}
			else{
				textReferral.Text=Referrals.GetNameFL(ReferralCur.ReferralNum);
				if(ReferralCur.EMail==""){
					butEmailRef.Enabled=false;
				}
			}
			if(PatCur.Email=="") {
				butEmail.Enabled=false;
			}
		}

		/*private void butComm_Click(object sender,EventArgs e) {
			Commlog CommlogCur=new Commlog();
			CommlogCur.PatNum=PatCur.PatNum;
			CommlogCur.CommDateTime=DateTime.Now;
			if(ViewingInRecall)
				CommlogCur.CommType=CommItemType.Recall;
			else
				CommlogCur.CommType=CommItemType.Financial;
			FormCommItem FormCI=new FormCommItem(CommlogCur);
			FormCI.IsNew=true;
			FormCI.ShowDialog();
			if(FormCI.DialogResult==DialogResult.OK){
				DialogResult=DialogResult.OK;
			}
		}*/

		private void butLetterSimple_Click(object sender,EventArgs e) {
			FormLetters FormL=new FormLetters(PatCur);
			FormL.ShowDialog();
			if(FormL.DialogResult==DialogResult.OK) {
				DialogResult=DialogResult.OK;
			}
		}

		private void butLetterMerge_Click(object sender,EventArgs e) {
			FormLetterMerges FormL=new FormLetterMerges(PatCur);
			FormL.ShowDialog();
			if(FormL.DialogResult==DialogResult.OK) {
				DialogResult=DialogResult.OK;
			}
		}

		private void butLabel_Click(object sender,EventArgs e) {
			LabelSingle label=new LabelSingle();
			label.PrintPat(PatCur);
			DialogResult=DialogResult.Cancel;//because there's no need to refresh ContrAccount
		}

		private void butEmail_Click(object sender,EventArgs e) {
			EmailMessage message=new EmailMessage();
			message.PatNum=PatCur.PatNum;
			message.ToAddress=PatCur.Email;
			message.FromAddress=PrefB.GetString("EmailSenderAddress");
			FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
			FormE.IsNew=true;
			FormE.ShowDialog();
			if(FormE.DialogResult==DialogResult.OK) {
				DialogResult=DialogResult.OK;
			}
		}

		private void butQuest_Click(object sender,EventArgs e) {
			FormPat form=new FormPat();
			form.PatNum=PatCur.PatNum;
			form.FormDateTime=DateTime.Now;
			FormFormPatEdit FormP=new FormFormPatEdit();
			FormP.FormPatCur=form;
			FormP.IsNew=true;
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.OK) {
				DialogResult=DialogResult.OK;
			}
		}

		private void butLetterSimpleRef_Click(object sender,EventArgs e) {
			FormLetters FormL=new FormLetters(PatCur);
			FormL.ReferralCur=ReferralCur;
			FormL.ShowDialog();
			if(FormL.DialogResult==DialogResult.OK) {
				DialogResult=DialogResult.OK;
			}
		}

		private void butLabelRef_Click(object sender,EventArgs e) {
			LabelSingle label=new LabelSingle();
			label.PrintReferral(ReferralCur);
			DialogResult=DialogResult.Cancel;//because there's no need to refresh ContrAccount
		}

		private void butEmailRef_Click(object sender,EventArgs e) {
			EmailMessage message=new EmailMessage();
			message.PatNum=PatCur.PatNum;
			message.ToAddress=ReferralCur.EMail;
			message.FromAddress=PrefB.GetString("EmailSenderAddress");
			message.Subject="RE: "+PatCur.GetNameFL();
			FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
			FormE.IsNew=true;
			FormE.ShowDialog();
			if(FormE.DialogResult==DialogResult.OK) {
				DialogResult=DialogResult.OK;
			}
		}
		public void PtLetter_ToClipboard() {
			StringBuilder str = new StringBuilder();
			str.Append(PatCur.FName + " " + PatCur.MiddleI + " " + PatCur.LName + "\r\n");
			str.Append(PatCur.Address + "\r\n");
			if (PatCur.Address2 != "")
				str.Append(PatCur.Address2 + "\r\n");
			str.Append(PatCur.City + ", " + PatCur.State + "  " + PatCur.Zip);
			str.Append("\r\n\r\n\r\n\r\n");
			//date
			str.Append(DateTime.Today.ToLongDateString() + "\r\n\r\n");
			//greeting
			str.Append("Dear ");
			if (PatCur.Salutation != "")
				str.Append(PatCur.Salutation);
			else if (PatCur.Preferred != "")
				str.Append(PatCur.Preferred);
			else
				str.Append(PatCur.FName);
			str.Append(",\r\n\r\n");
			//closing
			str.Append("\r\n\r\nSincerely,\r\n\r\n\r\n\r\n");
			//Clipboard.SetDataObject(textQuery.Text);
			//str.CopyTo;
			//string FinalString = str.CopyTo;
			//textTest.Text = str.ToString();
			Clipboard.SetDataObject(str.ToString());
		}

		private void butStationary_Click(object sender, EventArgs e) {
				if(PrefB.GetString("StationaryDocument")==""){
					MsgBox.Show(this,"You must setup your stationary document and word processor path in Setup | Misc");
					return;
				}
				Cursor = Cursors.AppStarting;
				PtLetter_ToClipboard();
				try {
					this.Cursor = Cursors.AppStarting;
					string patFolder=ODFileUtils.CombinePaths(
						FormPath.GetPreferredImagePath(),
						PatCur.ImageFolder.Substring(0, 1),
						PatCur.ImageFolder);
					//string ProgName = @"C:\Program Files\OpenOffice.org 2.0\program\swriter.exe";
					//string ProgName = PrefB.GetString("WordProcessorPath");
					string TheFile=ODFileUtils.CombinePaths(patFolder,"Letter_"+DateTime.Now.ToFileTime()+".doc");
					try{
						File.Copy(
							ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),PrefB.GetString("StationaryDocument")),
							TheFile);
						DialogResult = DialogResult.OK;
					}
					catch {
					}
					try {
						Process.Start(TheFile);
					}
					catch {
					}
					this.Cursor = Cursors.Default;
					Commlog CommlogCur = new Commlog();
					CommlogCur.CommDateTime = DateTime.Now;
					CommlogCur.CommType = Commlogs.GetTypeAuto(CommItemTypeAuto.MISC);
					CommlogCur.PatNum = PatCur.PatNum;
					CommlogCur.Note = Lan.g(this,"Letter sent: See Images for this date.");
					Commlogs.Insert(CommlogCur);
				}
				catch{
					Cursor = Cursors.Default;
					MsgBox.Show(this, "Cannot find stationary document. Or another problem exists.");
				}
		}

		

		
		


	}
}





















