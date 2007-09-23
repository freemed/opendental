using System;
using System.Drawing;
using System.Collections;
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
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton radioFrom;
		private System.Windows.Forms.RadioButton radioTo;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidNumber textOrder;
		private System.Windows.Forms.Label labelOrder;
		private OpenDental.UI.Button butEdit;
		private OpenDental.ValidDate textRefDate;
		private System.Windows.Forms.CheckBox checkPatient;
		private System.Windows.Forms.TextBox textReferralNotes;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.ComponentModel.Container components = null;
		private Label label6;
		private TextBox textNote;
		private ComboBox comboRefToStatus;
		private Label label7;
		private OpenDental.UI.Button butDelete;
		///<summary></summary>
		public RefAttach RefAttachCur;

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
			this.panel1 = new System.Windows.Forms.Panel();
			this.radioTo = new System.Windows.Forms.RadioButton();
			this.radioFrom = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			this.labelOrder = new System.Windows.Forms.Label();
			this.textOrder = new OpenDental.ValidNumber();
			this.butEdit = new OpenDental.UI.Button();
			this.textRefDate = new OpenDental.ValidDate();
			this.checkPatient = new System.Windows.Forms.CheckBox();
			this.textReferralNotes = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.comboRefToStatus = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.panel1.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(593,324);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
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
			this.butOK.Location = new System.Drawing.Point(593,284);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 5;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(65,171);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84,16);
			this.label3.TabIndex = 16;
			this.label3.Text = "Date";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(59,24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90,18);
			this.label1.TabIndex = 17;
			this.label1.Text = "Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textName
			// 
			this.textName.Location = new System.Drawing.Point(151,20);
			this.textName.Name = "textName";
			this.textName.ReadOnly = true;
			this.textName.Size = new System.Drawing.Size(258,20);
			this.textName.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.radioTo);
			this.panel1.Controls.Add(this.radioFrom);
			this.panel1.Location = new System.Drawing.Point(151,141);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(196,24);
			this.panel1.TabIndex = 19;
			// 
			// radioTo
			// 
			this.radioTo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioTo.Location = new System.Drawing.Point(86,5);
			this.radioTo.Name = "radioTo";
			this.radioTo.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.radioTo.Size = new System.Drawing.Size(78,14);
			this.radioTo.TabIndex = 3;
			this.radioTo.Text = "To";
			// 
			// radioFrom
			// 
			this.radioFrom.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioFrom.Location = new System.Drawing.Point(2,4);
			this.radioFrom.Name = "radioFrom";
			this.radioFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.radioFrom.Size = new System.Drawing.Size(74,16);
			this.radioFrom.TabIndex = 2;
			this.radioFrom.Text = "From";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(65,147);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86,16);
			this.label2.TabIndex = 20;
			this.label2.Text = "From/To";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelOrder
			// 
			this.labelOrder.Location = new System.Drawing.Point(69,194);
			this.labelOrder.Name = "labelOrder";
			this.labelOrder.Size = new System.Drawing.Size(82,14);
			this.labelOrder.TabIndex = 73;
			this.labelOrder.Text = "Order";
			this.labelOrder.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textOrder
			// 
			this.textOrder.Location = new System.Drawing.Point(151,190);
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
			this.butEdit.Location = new System.Drawing.Point(419,17);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(95,24);
			this.butEdit.TabIndex = 74;
			this.butEdit.Text = "&Edit Referral";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// textRefDate
			// 
			this.textRefDate.Location = new System.Drawing.Point(151,167);
			this.textRefDate.Name = "textRefDate";
			this.textRefDate.Size = new System.Drawing.Size(100,20);
			this.textRefDate.TabIndex = 75;
			// 
			// checkPatient
			// 
			this.checkPatient.AutoCheck = false;
			this.checkPatient.BackColor = System.Drawing.SystemColors.Control;
			this.checkPatient.Enabled = false;
			this.checkPatient.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPatient.Location = new System.Drawing.Point(151,44);
			this.checkPatient.Name = "checkPatient";
			this.checkPatient.Size = new System.Drawing.Size(22,20);
			this.checkPatient.TabIndex = 76;
			this.checkPatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPatient.UseVisualStyleBackColor = false;
			// 
			// textReferralNotes
			// 
			this.textReferralNotes.Location = new System.Drawing.Point(151,67);
			this.textReferralNotes.Multiline = true;
			this.textReferralNotes.Name = "textReferralNotes";
			this.textReferralNotes.ReadOnly = true;
			this.textReferralNotes.Size = new System.Drawing.Size(363,66);
			this.textReferralNotes.TabIndex = 78;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(54,0);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(100,23);
			this.label17.TabIndex = 79;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(51,46);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(98,20);
			this.label4.TabIndex = 80;
			this.label4.Text = "Patient";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(3,67);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(146,38);
			this.label5.TabIndex = 81;
			this.label5.Text = "Notes about referral source";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(3,237);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(146,38);
			this.label6.TabIndex = 83;
			this.label6.Text = "Patient note";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(151,237);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(363,66);
			this.textNote.TabIndex = 82;
			// 
			// comboRefToStatus
			// 
			this.comboRefToStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboRefToStatus.FormattingEnabled = true;
			this.comboRefToStatus.Location = new System.Drawing.Point(151,213);
			this.comboRefToStatus.MaxDropDownItems = 20;
			this.comboRefToStatus.Name = "comboRefToStatus";
			this.comboRefToStatus.Size = new System.Drawing.Size(180,21);
			this.comboRefToStatus.TabIndex = 84;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(6,216);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(143,14);
			this.label7.TabIndex = 85;
			this.label7.Text = "Status (if refferred to)";
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
			this.butDelete.Location = new System.Drawing.Point(35,324);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81,26);
			this.butDelete.TabIndex = 86;
			this.butDelete.Text = "Detach";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormRefAttachEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(689,370);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.comboRefToStatus);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textReferralNotes);
			this.Controls.Add(this.textRefDate);
			this.Controls.Add(this.butEdit);
			this.Controls.Add(this.textOrder);
			this.Controls.Add(this.textName);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.checkPatient);
			this.Controls.Add(this.labelOrder);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.panel1);
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
			this.panel1.ResumeLayout(false);
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
		}

		private void FillData(){
			Referral referral=Referrals.GetReferral(RefAttachCur.ReferralNum);
			textName.Text=referral.LName+", "+referral.FName+" "+referral.MName;
			checkPatient.Checked=referral.PatNum>0;
			textReferralNotes.Text=referral.Note;
			if(RefAttachCur.IsFrom){
        radioFrom.Checked=true; 
      }
      else{
        radioTo.Checked=true;
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
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			try{
				SaveData();
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			Referral referral=Referrals.GetReferral(RefAttachCur.ReferralNum);
			FormReferralEdit FormRE=new FormReferralEdit(referral);
			FormRE.ShowDialog();
			Referrals.Refresh();
			FillData();
		}

		///<summary>Surround with try-catch.</summary>
		private void SaveData(){
			if(textOrder.errorProvider1.GetError(textOrder)!=""
				|| textRefDate.errorProvider1.GetError(textRefDate)!="") 
			{
				throw new ApplicationException(Lan.g(this,"Please fix data entry errors first."));
			}
			if(radioFrom.Checked){
				RefAttachCur.IsFrom=true;
			}
			else{
				RefAttachCur.IsFrom=false;
			}
			RefAttachCur.RefDate=PIn.PDate(textRefDate.Text); 
      RefAttachCur.ItemOrder=PIn.PInt(textOrder.Text);
			RefAttachCur.RefToStatus=(ReferralToStatus)comboRefToStatus.SelectedIndex;
			RefAttachCur.Note=textNote.Text;
			if(IsNew){
				RefAttaches.Insert(RefAttachCur);
			}
			else{
				RefAttaches.Update(RefAttachCur);
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"Detach Referral?")) {
				return;
			}
			RefAttaches.Delete(RefAttachCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			try{
				SaveData();
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








