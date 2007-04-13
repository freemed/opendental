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
	public class FormDunningEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.ListBox listBillType;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radio30;
		private System.Windows.Forms.RadioButton radio90;
		private System.Windows.Forms.RadioButton radio60;
		private System.Windows.Forms.RadioButton radioAny;
		private System.Windows.Forms.TextBox textDunMessage;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioY;
		private System.Windows.Forms.RadioButton radioN;
		private System.Windows.Forms.RadioButton radioU;
		private Dunning DunningCur;

		///<summary></summary>
		public FormDunningEdit(Dunning dunningCur)
		{
			//
			// Required for Windows Form Designer support
			//
			DunningCur=dunningCur.Copy();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDunningEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textDunMessage = new System.Windows.Forms.TextBox();
			this.butDelete = new OpenDental.UI.Button();
			this.listBillType = new System.Windows.Forms.ListBox();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radio30 = new System.Windows.Forms.RadioButton();
			this.radio90 = new System.Windows.Forms.RadioButton();
			this.radio60 = new System.Windows.Forms.RadioButton();
			this.radioAny = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioY = new System.Windows.Forms.RadioButton();
			this.radioN = new System.Windows.Forms.RadioButton();
			this.radioU = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(366,387);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 9;
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
			this.butOK.Location = new System.Drawing.Point(281,387);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 8;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(27,244);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(148,17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Message";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textDunMessage
			// 
			this.textDunMessage.AcceptsReturn = true;
			this.textDunMessage.AcceptsTab = true;
			this.textDunMessage.Location = new System.Drawing.Point(28,265);
			this.textDunMessage.Multiline = true;
			this.textDunMessage.Name = "textDunMessage";
			this.textDunMessage.Size = new System.Drawing.Size(412,113);
			this.textDunMessage.TabIndex = 0;
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
			this.butDelete.Location = new System.Drawing.Point(27,387);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81,26);
			this.butDelete.TabIndex = 4;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// listBillType
			// 
			this.listBillType.Location = new System.Drawing.Point(28,35);
			this.listBillType.Name = "listBillType";
			this.listBillType.Size = new System.Drawing.Size(158,199);
			this.listBillType.TabIndex = 113;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(27,17);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(147,16);
			this.label8.TabIndex = 114;
			this.label8.Text = "Billing Type:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radio30);
			this.groupBox1.Controls.Add(this.radio90);
			this.groupBox1.Controls.Add(this.radio60);
			this.groupBox1.Controls.Add(this.radioAny);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(240,29);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(198,110);
			this.groupBox1.TabIndex = 115;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Age of Account";
			// 
			// radio30
			// 
			this.radio30.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio30.Location = new System.Drawing.Point(12,41);
			this.radio30.Name = "radio30";
			this.radio30.Size = new System.Drawing.Size(120,16);
			this.radio30.TabIndex = 1;
			this.radio30.Text = "Over 30 Days";
			// 
			// radio90
			// 
			this.radio90.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio90.Location = new System.Drawing.Point(12,85);
			this.radio90.Name = "radio90";
			this.radio90.Size = new System.Drawing.Size(120,18);
			this.radio90.TabIndex = 3;
			this.radio90.Text = "Over 90 Days";
			// 
			// radio60
			// 
			this.radio60.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio60.Location = new System.Drawing.Point(12,62);
			this.radio60.Name = "radio60";
			this.radio60.Size = new System.Drawing.Size(117,18);
			this.radio60.TabIndex = 2;
			this.radio60.Text = "Over 60 Days";
			// 
			// radioAny
			// 
			this.radioAny.Checked = true;
			this.radioAny.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioAny.Location = new System.Drawing.Point(12,18);
			this.radioAny.Name = "radioAny";
			this.radioAny.Size = new System.Drawing.Size(117,18);
			this.radioAny.TabIndex = 0;
			this.radioAny.TabStop = true;
			this.radioAny.Text = "Any Balance";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioY);
			this.groupBox2.Controls.Add(this.radioN);
			this.groupBox2.Controls.Add(this.radioU);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(240,147);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(198,87);
			this.groupBox2.TabIndex = 117;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Insurance Payment Pending";
			// 
			// radioY
			// 
			this.radioY.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioY.Location = new System.Drawing.Point(12,40);
			this.radioY.Name = "radioY";
			this.radioY.Size = new System.Drawing.Size(120,16);
			this.radioY.TabIndex = 1;
			this.radioY.Text = "Yes";
			// 
			// radioN
			// 
			this.radioN.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioN.Location = new System.Drawing.Point(12,61);
			this.radioN.Name = "radioN";
			this.radioN.Size = new System.Drawing.Size(117,18);
			this.radioN.TabIndex = 2;
			this.radioN.Text = "No";
			// 
			// radioU
			// 
			this.radioU.Checked = true;
			this.radioU.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioU.Location = new System.Drawing.Point(12,17);
			this.radioU.Name = "radioU";
			this.radioU.Size = new System.Drawing.Size(117,18);
			this.radioU.TabIndex = 0;
			this.radioU.TabStop = true;
			this.radioU.Text = "Doesn\'t Matter";
			// 
			// FormDunningEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(464,431);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listBillType);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textDunMessage);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDunningEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Dunning Message";
			this.Load += new System.EventHandler(this.FormDunningEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormDunningEdit_Load(object sender, System.EventArgs e) {
			listBillType.Items.Add(Lan.g(this,"all"));
			listBillType.SetSelected(0,true);
			for(int i=0;i<DefB.Short[(int)DefCat.BillingTypes].Length;i++){
				listBillType.Items.Add(DefB.Short[(int)DefCat.BillingTypes][i].ItemName);
				if(DunningCur.BillingType==DefB.Short[(int)DefCat.BillingTypes][i].DefNum){
					listBillType.SetSelected(i+1,true);
				}
			}
			switch(DunningCur.AgeAccount){
				case 0:
					radioAny.Checked=true;
					break;
				case 30:
					radio30.Checked=true;
					break;
				case 60:
					radio60.Checked=true;
					break;
				case 90:
					radio90.Checked=true;
					break;
			}
			switch(DunningCur.InsIsPending){
				case YN.Unknown:
					radioU.Checked=true;
					break;
				case YN.Yes:
					radioY.Checked=true;
					break;
				case YN.No:
					radioN.Checked=true;
					break;
			}
			textDunMessage.Text=DunningCur.DunMessage;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				Dunnings.Delete(DunningCur);
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDunMessage.Text==""){
				MessageBox.Show(Lan.g(this,"Message cannot be blank."));
				return;
			}
			if(listBillType.SelectedIndex==0){
				DunningCur.BillingType=0;
			}
			else{
				DunningCur.BillingType=DefB.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndex-1].DefNum;
			}
			if(radioAny.Checked){
				DunningCur.AgeAccount=0;
			}
			else if(radio30.Checked){
				DunningCur.AgeAccount=30;
			}
			else if(radio60.Checked){
				DunningCur.AgeAccount=60;
			}
			else if(radio90.Checked){
				DunningCur.AgeAccount=90;
			}
			if(radioU.Checked){
				DunningCur.InsIsPending=YN.Unknown;
			}
			else if(radioY.Checked){
				DunningCur.InsIsPending=YN.Yes;
			}
			else if(radioN.Checked){
				DunningCur.InsIsPending=YN.No;
			}
			DunningCur.DunMessage=textDunMessage.Text;
			Dunnings.InsertOrUpdate(DunningCur,IsNew);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}





















