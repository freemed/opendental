using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRefAttachEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label3;
		///<summary></summary>
    public bool IsNew;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textName;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidNumber textOrder;
		private System.Windows.Forms.Label labelOrder;
		private OpenDental.UI.Button butEdit;
		private OpenDental.ValidDate textRefDate;
		private System.Windows.Forms.TextBox textReferralNotes;
		private System.Windows.Forms.Label labelPatient;
		private System.Windows.Forms.Label label5;
		private System.ComponentModel.Container components = null;
		private Label label6;
		private TextBox textNote;
		private ComboBox comboRefToStatus;
		private Label label7;
		private OpenDental.UI.Button butDelete;
		private ListBox listSheets;
		///<summary></summary>
		public RefAttach RefAttachCur;
		private Label label8;
		private ListBox listFromTo;
		private CheckBox checkIsTransitionOfCare;
		private Label label4;
		///<summary>List of referral slips for this pat/ref combo.</summary>
		private List<Sheet> SheetList; 

		///<summary></summary>
		public FormRefAttachEdit(){
			InitializeComponent();
			Lan.F(this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRefAttachEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.labelOrder = new System.Windows.Forms.Label();
			this.textOrder = new OpenDental.ValidNumber();
			this.butEdit = new OpenDental.UI.Button();
			this.textRefDate = new OpenDental.ValidDate();
			this.textReferralNotes = new System.Windows.Forms.TextBox();
			this.labelPatient = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.comboRefToStatus = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.listSheets = new System.Windows.Forms.ListBox();
			this.label8 = new System.Windows.Forms.Label();
			this.listFromTo = new System.Windows.Forms.ListBox();
			this.checkIsTransitionOfCare = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
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
			this.butCancel.Location = new System.Drawing.Point(602,391);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 6;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(602,354);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 0;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(65,155);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84,16);
			this.label3.TabIndex = 16;
			this.label3.Text = "Date";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(59,47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90,18);
			this.label1.TabIndex = 17;
			this.label1.Text = "Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textName
			// 
			this.textName.Location = new System.Drawing.Point(151,43);
			this.textName.Name = "textName";
			this.textName.ReadOnly = true;
			this.textName.Size = new System.Drawing.Size(258,20);
			this.textName.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(65,13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86,16);
			this.label2.TabIndex = 20;
			this.label2.Text = "From/To";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelOrder
			// 
			this.labelOrder.Location = new System.Drawing.Point(69,176);
			this.labelOrder.Name = "labelOrder";
			this.labelOrder.Size = new System.Drawing.Size(82,14);
			this.labelOrder.TabIndex = 73;
			this.labelOrder.Text = "Order";
			this.labelOrder.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textOrder
			// 
			this.textOrder.Location = new System.Drawing.Point(151,172);
			this.textOrder.MaxVal = 255;
			this.textOrder.MinVal = 0;
			this.textOrder.Name = "textOrder";
			this.textOrder.Size = new System.Drawing.Size(36,20);
			this.textOrder.TabIndex = 72;
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEdit.Autosize = true;
			this.butEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEdit.CornerRadius = 4F;
			this.butEdit.Location = new System.Drawing.Point(413,40);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(95,24);
			this.butEdit.TabIndex = 74;
			this.butEdit.Text = "&Edit Referral";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// textRefDate
			// 
			this.textRefDate.Location = new System.Drawing.Point(151,151);
			this.textRefDate.Name = "textRefDate";
			this.textRefDate.Size = new System.Drawing.Size(100,20);
			this.textRefDate.TabIndex = 75;
			// 
			// textReferralNotes
			// 
			this.textReferralNotes.Location = new System.Drawing.Point(151,84);
			this.textReferralNotes.Multiline = true;
			this.textReferralNotes.Name = "textReferralNotes";
			this.textReferralNotes.ReadOnly = true;
			this.textReferralNotes.Size = new System.Drawing.Size(363,66);
			this.textReferralNotes.TabIndex = 78;
			// 
			// labelPatient
			// 
			this.labelPatient.Location = new System.Drawing.Point(150,65);
			this.labelPatient.Name = "labelPatient";
			this.labelPatient.Size = new System.Drawing.Size(98,18);
			this.labelPatient.TabIndex = 80;
			this.labelPatient.Text = "(a patient)";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(3,84);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(146,38);
			this.label5.TabIndex = 81;
			this.label5.Text = "Notes about referral source";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(3,215);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(146,38);
			this.label6.TabIndex = 83;
			this.label6.Text = "Patient note";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(151,215);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(363,66);
			this.textNote.TabIndex = 1;
			// 
			// comboRefToStatus
			// 
			this.comboRefToStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboRefToStatus.FormattingEnabled = true;
			this.comboRefToStatus.Location = new System.Drawing.Point(151,193);
			this.comboRefToStatus.MaxDropDownItems = 20;
			this.comboRefToStatus.Name = "comboRefToStatus";
			this.comboRefToStatus.Size = new System.Drawing.Size(180,21);
			this.comboRefToStatus.TabIndex = 84;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(6,196);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(143,14);
			this.label7.TabIndex = 85;
			this.label7.Text = "Status (if refferred out)";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
			this.butDelete.Location = new System.Drawing.Point(14,391);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81,24);
			this.butDelete.TabIndex = 86;
			this.butDelete.Text = "Detach";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// listSheets
			// 
			this.listSheets.FormattingEnabled = true;
			this.listSheets.Location = new System.Drawing.Point(151,282);
			this.listSheets.Name = "listSheets";
			this.listSheets.Size = new System.Drawing.Size(120,69);
			this.listSheets.TabIndex = 90;
			this.listSheets.DoubleClick += new System.EventHandler(this.listSheets_DoubleClick);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(9,284);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(140,40);
			this.label8.TabIndex = 91;
			this.label8.Text = "Referral Slips\r\n(double click to view)";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listFromTo
			// 
			this.listFromTo.FormattingEnabled = true;
			this.listFromTo.Items.AddRange(new object[] {
            "From",
            "To"});
			this.listFromTo.Location = new System.Drawing.Point(151,12);
			this.listFromTo.Name = "listFromTo";
			this.listFromTo.Size = new System.Drawing.Size(65,30);
			this.listFromTo.TabIndex = 92;
			// 
			// checkIsTransitionOfCare
			// 
			this.checkIsTransitionOfCare.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsTransitionOfCare.Location = new System.Drawing.Point(29,354);
			this.checkIsTransitionOfCare.Name = "checkIsTransitionOfCare";
			this.checkIsTransitionOfCare.Size = new System.Drawing.Size(136,18);
			this.checkIsTransitionOfCare.TabIndex = 93;
			this.checkIsTransitionOfCare.Text = "Transition of Care";
			this.checkIsTransitionOfCare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsTransitionOfCare.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(166,355);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(195,14);
			this.label4.TabIndex = 94;
			this.label4.Text = "(From or To another doctor)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormRefAttachEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(689,427);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.checkIsTransitionOfCare);
			this.Controls.Add(this.listFromTo);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.listSheets);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.comboRefToStatus);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.labelPatient);
			this.Controls.Add(this.textReferralNotes);
			this.Controls.Add(this.textRefDate);
			this.Controls.Add(this.butEdit);
			this.Controls.Add(this.textOrder);
			this.Controls.Add(this.textName);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.labelOrder);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRefAttachEdit";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormRefAttachEdit";
			this.Load += new System.EventHandler(this.FormRefAttachEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRefAttachEdit_Load(object sender, System.EventArgs e) {
      if(IsNew){
        this.Text=Lan.g(this,"Add Referral Attachment");
      }
      else{
        this.Text=Lan.g(this,"Edit Referral Attachment");
      }
			FillData();
			FillSheets();
		}

		private void FillData(){
			Referral referral=Referrals.GetReferral(RefAttachCur.ReferralNum);
			textName.Text=referral.GetNameFL();
			labelPatient.Visible=referral.PatNum>0;
			textReferralNotes.Text=referral.Note;
			if(RefAttachCur.IsFrom){
				listFromTo.SelectedIndex=0;
      }
      else{
				listFromTo.SelectedIndex=1;
      }
			if(RefAttachCur.RefDate.CompareTo(new DateTime(1880,1,1))<0){
				textRefDate.Text="";
			}
			else{
				textRefDate.Text=RefAttachCur.RefDate.ToShortDateString();
			}
			textOrder.Text=RefAttachCur.ItemOrder.ToString();
			comboRefToStatus.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(ReferralToStatus)).Length;i++){
				comboRefToStatus.Items.Add(Lan.g("enumReferralToStatus",Enum.GetNames(typeof(ReferralToStatus))[i]));
				if((int)RefAttachCur.RefToStatus==i){
					comboRefToStatus.SelectedIndex=i;
				}
			}
			textNote.Text=RefAttachCur.Note;
			checkIsTransitionOfCare.Checked=RefAttachCur.IsTransitionOfCare;
		}

		private void FillSheets(){
			SheetList=Sheets.GetReferralSlips(RefAttachCur.PatNum,RefAttachCur.ReferralNum);
			listSheets.Items.Clear();
			for(int i=0;i<SheetList.Count;i++){
				listSheets.Items.Add(SheetList[i].DateTimeSheet.ToShortDateString());
			}
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			try{
				DataToCur();
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			Referral referral=Referrals.GetReferral(RefAttachCur.ReferralNum);
			FormReferralEdit FormRE=new FormReferralEdit(referral);
			FormRE.ShowDialog();
			Referrals.RefreshCache();
			FillData();
		}

		private void listSheets_DoubleClick(object sender,EventArgs e) {
			if(listSheets.SelectedIndex==-1){
				return;
			}
			Sheet sheet=SheetList[listSheets.SelectedIndex];
			SheetFields.GetFieldsAndParameters(sheet);
			FormSheetFillEdit FormS=new FormSheetFillEdit(sheet);
			FormS.ShowDialog();
			FillSheets();
		}

		///<summary>Surround with try-catch.  Attempts to take the data on the form and set the values of RefAttachCur.</summary>
		private void DataToCur(){
			if(textOrder.errorProvider1.GetError(textOrder)!=""
				|| textRefDate.errorProvider1.GetError(textRefDate)!="") 
			{
				throw new ApplicationException(Lan.g(this,"Please fix data entry errors first."));
			}
			if(listFromTo.SelectedIndex==0){
				RefAttachCur.IsFrom=true;
			}
			else{
				RefAttachCur.IsFrom=false;
			}
			RefAttachCur.RefDate=PIn.Date(textRefDate.Text); 
      RefAttachCur.ItemOrder=PIn.Int(textOrder.Text);
			RefAttachCur.RefToStatus=(ReferralToStatus)comboRefToStatus.SelectedIndex;
			RefAttachCur.Note=textNote.Text;
			RefAttachCur.IsTransitionOfCare=checkIsTransitionOfCare.Checked;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"Detach Referral?")) {
				return;
			}
			RefAttaches.Delete(RefAttachCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//this is an old pattern
			try{
				DataToCur();
				if(IsNew){
					RefAttaches.Insert(RefAttachCur);
				}
				else{
					RefAttaches.Update(RefAttachCur);
				}
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

	}
}








