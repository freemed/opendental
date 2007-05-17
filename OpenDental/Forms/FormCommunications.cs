using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

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
		private OpenDental.UI.Button butComm;
		private GroupBox groupBox2;
		private OpenDental.UI.Button butLabelRef;
		private OpenDental.UI.Button butEmailRef;
		private OpenDental.UI.Button butLetterSimpleRef;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public Patient PatCur;
		private TextBox textReferral;
		public bool ViewingInRecall;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCommunications));
			this.butQuest = new OpenDental.UI.Button();
			this.butLabel = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butLetterMerge = new OpenDental.UI.Button();
			this.butLetterSimple = new OpenDental.UI.Button();
			this.butEmail = new OpenDental.UI.Button();
			this.butComm = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butLetterSimpleRef = new OpenDental.UI.Button();
			this.butLabelRef = new OpenDental.UI.Button();
			this.butEmailRef = new OpenDental.UI.Button();
			this.textReferral = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butQuest
			// 
			this.butQuest.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butQuest.Autosize = true;
			this.butQuest.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butQuest.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butQuest.CornerRadius = 4F;
			this.butQuest.Location = new System.Drawing.Point(20,224);
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
			this.butLabel.Location = new System.Drawing.Point(20,149);
			this.butLabel.Name = "butLabel";
			this.butLabel.Size = new System.Drawing.Size(90,25);
			this.butLabel.TabIndex = 89;
			this.butLabel.Text = "Label";
			this.butLabel.Click += new System.EventHandler(this.butLabel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butLetterMerge);
			this.groupBox1.Controls.Add(this.butLetterSimple);
			this.groupBox1.Location = new System.Drawing.Point(12,55);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(106,81);
			this.groupBox1.TabIndex = 88;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Letter to Patient";
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
			// butEmail
			// 
			this.butEmail.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEmail.Autosize = true;
			this.butEmail.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEmail.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEmail.CornerRadius = 4F;
			this.butEmail.Location = new System.Drawing.Point(20,180);
			this.butEmail.Name = "butEmail";
			this.butEmail.Size = new System.Drawing.Size(90,25);
			this.butEmail.TabIndex = 87;
			this.butEmail.Text = "E-mail";
			this.butEmail.Click += new System.EventHandler(this.butEmail_Click);
			// 
			// butComm
			// 
			this.butComm.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butComm.Autosize = true;
			this.butComm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butComm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butComm.CornerRadius = 4F;
			this.butComm.Image = ((System.Drawing.Image)(resources.GetObject("butComm.Image")));
			this.butComm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butComm.Location = new System.Drawing.Point(20,12);
			this.butComm.Name = "butComm";
			this.butComm.Size = new System.Drawing.Size(90,26);
			this.butComm.TabIndex = 86;
			this.butComm.Text = "Commlog";
			this.butComm.Click += new System.EventHandler(this.butComm_Click);
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
			// textReferral
			// 
			this.textReferral.Location = new System.Drawing.Point(6,19);
			this.textReferral.Name = "textReferral";
			this.textReferral.ReadOnly = true;
			this.textReferral.Size = new System.Drawing.Size(174,20);
			this.textReferral.TabIndex = 92;
			// 
			// FormCommunications
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(332,278);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butQuest);
			this.Controls.Add(this.butLabel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butEmail);
			this.Controls.Add(this.butComm);
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

		private void butComm_Click(object sender,EventArgs e) {
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
		}

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

		
		


	}
}





















