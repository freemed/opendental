namespace OpenDental{
	partial class FormBillingDefaults {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.checkIntermingled = new System.Windows.Forms.CheckBox();
			this.labelStartDate = new System.Windows.Forms.Label();
			this.textNote = new OpenDental.ODtextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textDays = new OpenDental.ValidNum();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkUseElectronic = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textVendorId = new System.Windows.Forms.TextBox();
			this.textVendorPMScode = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textClientAcctNumber = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkMC = new System.Windows.Forms.CheckBox();
			this.checkV = new System.Windows.Forms.CheckBox();
			this.checkD = new System.Windows.Forms.CheckBox();
			this.checkAmEx = new System.Windows.Forms.CheckBox();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textUserName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(479,422);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
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
			this.butCancel.Location = new System.Drawing.Point(560,422);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// checkIntermingled
			// 
			this.checkIntermingled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIntermingled.Location = new System.Drawing.Point(13,34);
			this.checkIntermingled.Name = "checkIntermingled";
			this.checkIntermingled.Size = new System.Drawing.Size(150,20);
			this.checkIntermingled.TabIndex = 243;
			this.checkIntermingled.Text = "Intermingle family members";
			// 
			// labelStartDate
			// 
			this.labelStartDate.Location = new System.Drawing.Point(16,9);
			this.labelStartDate.Name = "labelStartDate";
			this.labelStartDate.Size = new System.Drawing.Size(147,14);
			this.labelStartDate.TabIndex = 221;
			this.labelStartDate.Text = "Start Date Last";
			this.labelStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(12,75);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Statement;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(616,102);
			this.textNote.TabIndex = 241;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(13,55);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(575,16);
			this.label4.TabIndex = 240;
			this.label4.Text = "General Message (in addition to any dunning messages and appointment messages)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textDays
			// 
			this.textDays.Location = new System.Drawing.Point(165,7);
			this.textDays.MaxVal = 255;
			this.textDays.MinVal = 0;
			this.textDays.Name = "textDays";
			this.textDays.Size = new System.Drawing.Size(44,20);
			this.textDays.TabIndex = 244;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(211,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65,14);
			this.label1.TabIndex = 245;
			this.label1.Text = "Days";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textPassword);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textUserName);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.textClientAcctNumber);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textVendorPMScode);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textVendorId);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.checkUseElectronic);
			this.groupBox1.Location = new System.Drawing.Point(13,183);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(420,241);
			this.groupBox1.TabIndex = 246;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Electronic Billing";
			// 
			// checkUseElectronic
			// 
			this.checkUseElectronic.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkUseElectronic.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkUseElectronic.Location = new System.Drawing.Point(23,17);
			this.checkUseElectronic.Name = "checkUseElectronic";
			this.checkUseElectronic.Size = new System.Drawing.Size(219,16);
			this.checkUseElectronic.TabIndex = 244;
			this.checkUseElectronic.Text = "Use electronic billing";
			this.checkUseElectronic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3,39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(224,16);
			this.label2.TabIndex = 245;
			this.label2.Text = "Vendor ID";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textVendorId
			// 
			this.textVendorId.Location = new System.Drawing.Point(229,37);
			this.textVendorId.Name = "textVendorId";
			this.textVendorId.Size = new System.Drawing.Size(100,20);
			this.textVendorId.TabIndex = 246;
			// 
			// textVendorPMScode
			// 
			this.textVendorPMScode.Location = new System.Drawing.Point(229,58);
			this.textVendorPMScode.Name = "textVendorPMScode";
			this.textVendorPMScode.Size = new System.Drawing.Size(100,20);
			this.textVendorPMScode.TabIndex = 248;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3,60);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(224,16);
			this.label3.TabIndex = 247;
			this.label3.Text = "Vendor PMS Code";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textClientAcctNumber
			// 
			this.textClientAcctNumber.Location = new System.Drawing.Point(229,173);
			this.textClientAcctNumber.Name = "textClientAcctNumber";
			this.textClientAcctNumber.Size = new System.Drawing.Size(100,20);
			this.textClientAcctNumber.TabIndex = 250;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(3,175);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(224,16);
			this.label5.TabIndex = 249;
			this.label5.Text = "Client Account Number";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkAmEx);
			this.groupBox2.Controls.Add(this.checkD);
			this.groupBox2.Controls.Add(this.checkV);
			this.groupBox2.Controls.Add(this.checkMC);
			this.groupBox2.Location = new System.Drawing.Point(66,83);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(197,85);
			this.groupBox2.TabIndex = 251;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Credit Card Choices";
			// 
			// checkMC
			// 
			this.checkMC.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMC.Location = new System.Drawing.Point(46,16);
			this.checkMC.Name = "checkMC";
			this.checkMC.Size = new System.Drawing.Size(132,16);
			this.checkMC.TabIndex = 0;
			this.checkMC.Text = "Master Card";
			this.checkMC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMC.UseVisualStyleBackColor = true;
			// 
			// checkV
			// 
			this.checkV.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkV.Location = new System.Drawing.Point(46,32);
			this.checkV.Name = "checkV";
			this.checkV.Size = new System.Drawing.Size(132,16);
			this.checkV.TabIndex = 1;
			this.checkV.Text = "Visa";
			this.checkV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkV.UseVisualStyleBackColor = true;
			// 
			// checkD
			// 
			this.checkD.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkD.Location = new System.Drawing.Point(46,48);
			this.checkD.Name = "checkD";
			this.checkD.Size = new System.Drawing.Size(132,16);
			this.checkD.TabIndex = 2;
			this.checkD.Text = "Discover";
			this.checkD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkD.UseVisualStyleBackColor = true;
			// 
			// checkAmEx
			// 
			this.checkAmEx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAmEx.Location = new System.Drawing.Point(46,64);
			this.checkAmEx.Name = "checkAmEx";
			this.checkAmEx.Size = new System.Drawing.Size(132,16);
			this.checkAmEx.TabIndex = 3;
			this.checkAmEx.Text = "American Express";
			this.checkAmEx.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAmEx.UseVisualStyleBackColor = true;
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(229,215);
			this.textPassword.Name = "textPassword";
			this.textPassword.Size = new System.Drawing.Size(165,20);
			this.textPassword.TabIndex = 255;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(3,217);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(224,16);
			this.label6.TabIndex = 254;
			this.label6.Text = "Password";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textUserName
			// 
			this.textUserName.Location = new System.Drawing.Point(229,194);
			this.textUserName.Name = "textUserName";
			this.textUserName.Size = new System.Drawing.Size(165,20);
			this.textUserName.TabIndex = 253;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(3,196);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(224,16);
			this.label7.TabIndex = 252;
			this.label7.Text = "User Name";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormBillingDefaults
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(659,458);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDays);
			this.Controls.Add(this.checkIntermingled);
			this.Controls.Add(this.labelStartDate);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormBillingDefaults";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Billing Defaults";
			this.Load += new System.EventHandler(this.FormBillingDefaults_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.CheckBox checkIntermingled;
		private System.Windows.Forms.Label labelStartDate;
		private ODtextBox textNote;
		private System.Windows.Forms.Label label4;
		private ValidNum textDays;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkUseElectronic;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textVendorPMScode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textVendorId;
		private System.Windows.Forms.TextBox textClientAcctNumber;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkMC;
		private System.Windows.Forms.CheckBox checkV;
		private System.Windows.Forms.CheckBox checkAmEx;
		private System.Windows.Forms.CheckBox checkD;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textUserName;
		private System.Windows.Forms.Label label7;
	}
}