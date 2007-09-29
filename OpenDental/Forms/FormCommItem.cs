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
		private CheckBox checkIsStatementSent;
		private TextBox textPatientName;
		private Label label5;
		private Commlog CommlogCur;

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
			this.checkIsStatementSent = new System.Windows.Forms.CheckBox();
			this.textPatientName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11,38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96,16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Date / Time";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(105,69);
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
			this.butOK.Location = new System.Drawing.Point(591,488);
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
			this.butCancel.Location = new System.Drawing.Point(591,521);
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
			this.butDelete.Location = new System.Drawing.Point(14,521);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81,25);
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(106,217);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82,16);
			this.label2.TabIndex = 18;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listType
			// 
			this.listType.Location = new System.Drawing.Point(107,87);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(120,95);
			this.listType.TabIndex = 20;
			// 
			// textDateTime
			// 
			this.textDateTime.Location = new System.Drawing.Point(107,36);
			this.textDateTime.Name = "textDateTime";
			this.textDateTime.Size = new System.Drawing.Size(205,20);
			this.textDateTime.TabIndex = 21;
			// 
			// listMode
			// 
			this.listMode.Location = new System.Drawing.Point(241,87);
			this.listMode.Name = "listMode";
			this.listMode.Size = new System.Drawing.Size(73,95);
			this.listMode.TabIndex = 23;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(240,70);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82,16);
			this.label3.TabIndex = 22;
			this.label3.Text = "Mode";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listSentOrReceived
			// 
			this.listSentOrReceived.Location = new System.Drawing.Point(329,87);
			this.listSentOrReceived.Name = "listSentOrReceived";
			this.listSentOrReceived.Size = new System.Drawing.Size(87,43);
			this.listSentOrReceived.TabIndex = 25;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(328,69);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(142,16);
			this.label4.TabIndex = 24;
			this.label4.Text = "Sent or Received";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(107,237);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.CommLog;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(557,209);
			this.textNote.TabIndex = 27;
			// 
			// checkIsStatementSent
			// 
			this.checkIsStatementSent.Location = new System.Drawing.Point(107,188);
			this.checkIsStatementSent.Name = "checkIsStatementSent";
			this.checkIsStatementSent.Size = new System.Drawing.Size(136,16);
			this.checkIsStatementSent.TabIndex = 28;
			this.checkIsStatementSent.Text = "Is Statement";
			this.checkIsStatementSent.UseVisualStyleBackColor = true;
			this.checkIsStatementSent.Click += new System.EventHandler(this.checkIsStatementSent_Click);
			// 
			// textPatientName
			// 
			this.textPatientName.Location = new System.Drawing.Point(107,10);
			this.textPatientName.Name = "textPatientName";
			this.textPatientName.ReadOnly = true;
			this.textPatientName.Size = new System.Drawing.Size(205,20);
			this.textPatientName.TabIndex = 30;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(11,12);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96,16);
			this.label5.TabIndex = 29;
			this.label5.Text = "Patient";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormCommItem
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(702,566);
			this.Controls.Add(this.textPatientName);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.checkIsStatementSent);
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
			textPatientName.Text=Patients.GetLim(CommlogCur.PatNum).GetNameFL();
			textDateTime.Text=CommlogCur.CommDateTime.ToString();
			//there will usually be a commtype set before this dialog is opened
			for(int i=0;i<DefB.Short[(int)DefCat.CommLogTypes].Length;i++){
				listType.Items.Add(DefB.Short[(int)DefCat.CommLogTypes][i].ItemName);
				if(DefB.Short[(int)DefCat.CommLogTypes][i].DefNum==CommlogCur.CommType){
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
			checkIsStatementSent.Checked=CommlogCur.IsStatementSent;
			textNote.Text=CommlogCur.Note;
			textNote.SelectionStart=textNote.Text.Length;
			textNote.Select();
		}

		/*
		///<summary>This button won't even be visible unless there is an email to view.</summary>
		private void butEmail_Click(object sender, System.EventArgs e) {
			EmailMessage message=EmailMessages.Refresh(CommlogCur.EmailMessageNum);
			//If a date is entered, user will not be able to click Send
			FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
			FormE.ShowDialog();
			CommlogCur=Commlogs.GetOne(CommlogCur.CommlogNum);
		}*/

		private void checkIsStatementSent_Click(object sender,EventArgs e) {
			if(checkIsStatementSent.Checked){
				listType.SelectedIndex=-1;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDateTime.Text==""
				//|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
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
			if(!checkIsStatementSent.Checked && listType.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a type.");
				return;
			}
			CommlogCur.CommDateTime=PIn.PDateT(textDateTime.Text);
			//there may not be a commtype selected.
			if(listType.SelectedIndex==-1){
				CommlogCur.CommType=0;
			}
			else{
				CommlogCur.CommType=DefB.Short[(int)DefCat.CommLogTypes][listType.SelectedIndex].DefNum;
			}
			CommlogCur.Mode_=(CommItemMode)listMode.SelectedIndex;
			CommlogCur.SentOrReceived=(CommSentOrReceived)listSentOrReceived.SelectedIndex;
			CommlogCur.Note=textNote.Text;
			CommlogCur.IsStatementSent=checkIsStatementSent.Checked;
			if(IsNew){
				Commlogs.Insert(CommlogCur);
			}
			else{
				Commlogs.Update(CommlogCur);
		  	//SecurityLogs.MakeLogEntry("Adjustment Edit",Adjustments.cmd.CommandText);
			}
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				//SecurityLogs.MakeLogEntry("Adjustment Edit","Delete. patNum: "+Adjustments.Cur.PatNum.ToString());
				Commlogs.Delete(CommlogCur);
				DialogResult=DialogResult.OK;
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		//private void button1_Click(object sender, System.EventArgs e) {
		//	textNote.Select(textNote.Text.Length,1);
		//}

	}

}
