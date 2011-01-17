/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FormCommItem : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textDateTime;
		private System.Windows.Forms.ListBox listMode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listSentOrReceived;
		private System.Windows.Forms.Label label4;
		private OpenDental.ODtextBox textNote;
		private System.Windows.Forms.ListBox listType;
		private TextBox textPatientName;
		private Label label5;
		private TextBox textUser;
		private Label label16;
		private Label labelCommlogNum;
		private TextBox textCommlogNum;
		private OpenDental.UI.SignatureBoxWrapper signatureBoxWrapper;
		private Commlog CommlogCur;
		private bool IsStartingUp;
		private bool SigChanged;

		///<summary></summary>
		public FormCommItem(Commlog commlogCur){
			InitializeComponent();
			Lan.F(this);
			CommlogCur=commlogCur;
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCommItem));
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.listType = new System.Windows.Forms.ListBox();
			this.textDateTime = new System.Windows.Forms.TextBox();
			this.listMode = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.listSentOrReceived = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textNote = new OpenDental.ODtextBox();
			this.textPatientName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textUser = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.labelCommlogNum = new System.Windows.Forms.Label();
			this.textCommlogNum = new System.Windows.Forms.TextBox();
			this.signatureBoxWrapper = new OpenDental.UI.SignatureBoxWrapper();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(1,35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81,18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Date / Time";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(80,56);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(82,16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Type";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(564,515);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 6;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
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
			this.butCancel.Location = new System.Drawing.Point(564,548);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(27,544);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81,25);
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(81,173);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82,16);
			this.label2.TabIndex = 18;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listType
			// 
			this.listType.Location = new System.Drawing.Point(82,74);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(120,95);
			this.listType.TabIndex = 20;
			this.listType.SelectedIndexChanged += new System.EventHandler(this.listType_SelectedIndexChanged);
			// 
			// textDateTime
			// 
			this.textDateTime.Location = new System.Drawing.Point(82,33);
			this.textDateTime.Name = "textDateTime";
			this.textDateTime.Size = new System.Drawing.Size(205,20);
			this.textDateTime.TabIndex = 21;
			this.textDateTime.TextChanged += new System.EventHandler(this.textDateTime_TextChanged);
			// 
			// listMode
			// 
			this.listMode.Location = new System.Drawing.Point(215,74);
			this.listMode.Name = "listMode";
			this.listMode.Size = new System.Drawing.Size(73,95);
			this.listMode.TabIndex = 23;
			this.listMode.SelectedIndexChanged += new System.EventHandler(this.listMode_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(214,57);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82,16);
			this.label3.TabIndex = 22;
			this.label3.Text = "Mode";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listSentOrReceived
			// 
			this.listSentOrReceived.Location = new System.Drawing.Point(303,74);
			this.listSentOrReceived.Name = "listSentOrReceived";
			this.listSentOrReceived.Size = new System.Drawing.Size(87,43);
			this.listSentOrReceived.TabIndex = 25;
			this.listSentOrReceived.SelectedValueChanged += new System.EventHandler(this.listSentOrReceived_SelectedValueChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(302,56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(142,16);
			this.label4.TabIndex = 24;
			this.label4.Text = "Sent or Received";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(82,193);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.CommLog;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(557,209);
			this.textNote.TabIndex = 27;
			this.textNote.TextChanged += new System.EventHandler(this.textNote_TextChanged);
			// 
			// textPatientName
			// 
			this.textPatientName.Location = new System.Drawing.Point(82,7);
			this.textPatientName.Name = "textPatientName";
			this.textPatientName.ReadOnly = true;
			this.textPatientName.Size = new System.Drawing.Size(205,20);
			this.textPatientName.TabIndex = 30;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4,9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(78,18);
			this.label5.TabIndex = 29;
			this.label5.Text = "Patient";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(402,7);
			this.textUser.Name = "textUser";
			this.textUser.ReadOnly = true;
			this.textUser.Size = new System.Drawing.Size(119,20);
			this.textUser.TabIndex = 103;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(327,8);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(73,16);
			this.label16.TabIndex = 102;
			this.label16.Text = "User";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelCommlogNum
			// 
			this.labelCommlogNum.Location = new System.Drawing.Point(154,552);
			this.labelCommlogNum.Name = "labelCommlogNum";
			this.labelCommlogNum.Size = new System.Drawing.Size(96,16);
			this.labelCommlogNum.TabIndex = 104;
			this.labelCommlogNum.Text = "CommlogNum";
			this.labelCommlogNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCommlogNum
			// 
			this.textCommlogNum.Location = new System.Drawing.Point(256,549);
			this.textCommlogNum.Name = "textCommlogNum";
			this.textCommlogNum.ReadOnly = true;
			this.textCommlogNum.Size = new System.Drawing.Size(188,20);
			this.textCommlogNum.TabIndex = 105;
			// 
			// signatureBoxWrapper
			// 
			this.signatureBoxWrapper.BackColor = System.Drawing.SystemColors.ControlDark;
			this.signatureBoxWrapper.LabelText = null;
			this.signatureBoxWrapper.Location = new System.Drawing.Point(83,408);
			this.signatureBoxWrapper.Name = "signatureBoxWrapper";
			this.signatureBoxWrapper.Size = new System.Drawing.Size(364,81);
			this.signatureBoxWrapper.TabIndex = 106;
			this.signatureBoxWrapper.SignatureChanged += new System.EventHandler(this.signatureBoxWrapper_SignatureChanged);
			// 
			// FormCommItem
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(662,594);
			this.Controls.Add(this.signatureBoxWrapper);
			this.Controls.Add(this.textCommlogNum);
			this.Controls.Add(this.labelCommlogNum);
			this.Controls.Add(this.textUser);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.textPatientName);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.listSentOrReceived);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.listMode);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDateTime);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCommItem";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Communication Item";
			this.Load += new System.EventHandler(this.FormCommItem_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormCommItem_Load(object sender, System.EventArgs e) {
			IsStartingUp=true;
			textPatientName.Text=Patients.GetLim(CommlogCur.PatNum).GetNameFL();
			textUser.Text=Userods.GetName(CommlogCur.UserNum);//might be blank. 
			textDateTime.Text=CommlogCur.CommDateTime.ToString();
			//there will usually be a commtype set before this dialog is opened
			for(int i=0;i<DefC.Short[(int)DefCat.CommLogTypes].Length;i++){
				listType.Items.Add(DefC.Short[(int)DefCat.CommLogTypes][i].ItemName);
				if(DefC.Short[(int)DefCat.CommLogTypes][i].DefNum==CommlogCur.CommType){
					listType.SelectedIndex=i;
				}
			}
			for(int i=0;i<Enum.GetNames(typeof(CommItemMode)).Length;i++){
				listMode.Items.Add(Lan.g("enumCommItemMode",Enum.GetNames(typeof(CommItemMode))[i]));
			}
			listMode.SelectedIndex=(int)CommlogCur.Mode_;
			for(int i=0;i<Enum.GetNames(typeof(CommSentOrReceived)).Length;i++){
				listSentOrReceived.Items.Add
					(Lan.g("enumCommSentOrReceived",Enum.GetNames(typeof(CommSentOrReceived))[i]));
			}
			try{
				listSentOrReceived.SelectedIndex=(int)CommlogCur.SentOrReceived;
			}
			catch{
				MessageBox.Show(((int)CommlogCur.SentOrReceived).ToString());
			}
			//checkIsStatementSent.Checked=CommlogCur.IsStatementSent;
			textNote.Text=CommlogCur.Note;
			textNote.SelectionStart=textNote.Text.Length;
			#if !DEBUG
				labelCommlogNum.Visible=false;
				textCommlogNum.Visible=false;
			#endif
			textCommlogNum.Text=CommlogCur.CommlogNum.ToString();
			textNote.Select();
			string keyData=GetSignatureKey();
			signatureBoxWrapper.FillSignature(CommlogCur.SigIsTopaz,keyData,CommlogCur.Signature);
			signatureBoxWrapper.BringToFront();
			IsStartingUp=false;
			if(!Security.IsAuthorized(Permissions.CommlogEdit,CommlogCur.CommDateTime)){
				if(IsNew){
					DialogResult=DialogResult.Cancel;
					return;
				}
				butDelete.Enabled=false;
				butOK.Enabled=false;
			}
		}

		private void signatureBoxWrapper_SignatureChanged(object sender,EventArgs e) {
			CommlogCur.UserNum=Security.CurUser.UserNum;
			textUser.Text=Userods.GetName(CommlogCur.UserNum);
			SigChanged=true;
		}

		private void ClearSignature(){
			if(!IsStartingUp//so this happens only if user changes the note
				&& !SigChanged)//and the original signature is still showing.
			{
				//SigChanged=true;//happens automatically through the event.
				signatureBoxWrapper.ClearSignature();
			}
		}

		private string GetSignatureKey(){
			string keyData=CommlogCur.UserNum.ToString();
			keyData+=CommlogCur.CommDateTime.ToString();
			keyData+=CommlogCur.Mode_.ToString();
			keyData+=CommlogCur.SentOrReceived.ToString();
			if(CommlogCur.Note!=null){
				keyData+=CommlogCur.Note.ToString();
			}
			return keyData;
		}

		private void SaveSignature(){
			if(SigChanged){
				string keyData=GetSignatureKey();
				CommlogCur.Signature=signatureBoxWrapper.GetSignature(keyData);
				CommlogCur.SigIsTopaz=signatureBoxWrapper.GetSigIsTopaz();
			}
		}

		private void textDateTime_TextChanged(object sender,EventArgs e) {
			ClearSignature();
		}

		private void listType_SelectedIndexChanged(object sender,EventArgs e) {
			ClearSignature();
		}

		private void listMode_SelectedIndexChanged(object sender,EventArgs e) {
			ClearSignature();
		}

		private void listSentOrReceived_SelectedValueChanged(object sender,EventArgs e) {
			ClearSignature();
		}

		private void textNote_TextChanged(object sender,EventArgs e) {
			ClearSignature();
		}

		private void butDelete_Click(object sender,System.EventArgs e) {
			//button not enabled if no permission
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
				return;
			}
			SecurityLogs.MakeLogEntry(Permissions.CommlogEdit,CommlogCur.PatNum,"Delete");
			Commlogs.Delete(CommlogCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//button not enabled if no permission
			if(textDateTime.Text==""){
				MessageBox.Show(Lan.g(this,"Please enter a date first."));
				return;
			}
			try{
				DateTime.Parse(textDateTime.Text);
			}
			catch{
				MessageBox.Show(Lan.g(this,"Date and time invalid."));
				return;
			}
			CommlogCur.CommDateTime=PIn.DateT(textDateTime.Text);
			//there may not be a commtype selected.
			if(listType.SelectedIndex==-1){
				CommlogCur.CommType=0;
			}
			else{
				CommlogCur.CommType=DefC.Short[(int)DefCat.CommLogTypes][listType.SelectedIndex].DefNum;
			}
			CommlogCur.Mode_=(CommItemMode)listMode.SelectedIndex;
			CommlogCur.SentOrReceived=(CommSentOrReceived)listSentOrReceived.SelectedIndex;
			CommlogCur.Note=textNote.Text;
			try {
				SaveSignature();
			}
			catch(Exception ex){
				MessageBox.Show(Lan.g(this,"Error saving signature.")+"\r\n"+ex.Message);
				return;
			}
			if(IsNew){
				Commlogs.Insert(CommlogCur);
				SecurityLogs.MakeLogEntry(Permissions.CommlogEdit,CommlogCur.PatNum,"Insert");
			}
			else{
				Commlogs.Update(CommlogCur);
				SecurityLogs.MakeLogEntry(Permissions.CommlogEdit,CommlogCur.PatNum,"");
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


		



	}

}
