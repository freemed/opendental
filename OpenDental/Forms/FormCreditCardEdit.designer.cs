namespace OpenDental{
	partial class FormCreditCardEdit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreditCardEdit));
			this.label3 = new System.Windows.Forms.Label();
			this.textCardNumber = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textExpDate = new System.Windows.Forms.TextBox();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textZip = new System.Windows.Forms.TextBox();
			this.labelAddress = new System.Windows.Forms.Label();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.labelZip = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textDateStop = new OpenDental.ValidDate();
			this.label2 = new System.Windows.Forms.Label();
			this.textDateStart = new OpenDental.ValidDate();
			this.textChargeAmt = new OpenDental.ValidDouble();
			this.label5 = new System.Windows.Forms.Label();
			this.groupRecurringCharges = new System.Windows.Forms.GroupBox();
			this.labelPayPlan = new System.Windows.Forms.Label();
			this.comboPaymentPlans = new System.Windows.Forms.ComboBox();
			this.butToday = new OpenDental.UI.Button();
			this.butClear = new OpenDental.UI.Button();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.groupRecurringCharges.SuspendLayout();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(25, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 8;
			this.label3.Text = "Card Number";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCardNumber
			// 
			this.textCardNumber.Location = new System.Drawing.Point(126, 12);
			this.textCardNumber.Name = "textCardNumber";
			this.textCardNumber.Size = new System.Drawing.Size(240, 20);
			this.textCardNumber.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(41, 39);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Exp (MMYY)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textExpDate
			// 
			this.textExpDate.Location = new System.Drawing.Point(126, 38);
			this.textExpDate.Name = "textExpDate";
			this.textExpDate.Size = new System.Drawing.Size(71, 20);
			this.textExpDate.TabIndex = 4;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(337, 345);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 10;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(418, 345);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 11;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(126, 90);
			this.textZip.MaxLength = 100;
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(136, 20);
			this.textZip.TabIndex = 9;
			// 
			// labelAddress
			// 
			this.labelAddress.Location = new System.Drawing.Point(25, 65);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(99, 16);
			this.labelAddress.TabIndex = 63;
			this.labelAddress.Text = "Address";
			this.labelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(126, 64);
			this.textAddress.MaxLength = 100;
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(365, 20);
			this.textAddress.TabIndex = 6;
			// 
			// labelZip
			// 
			this.labelZip.Location = new System.Drawing.Point(29, 91);
			this.labelZip.Name = "labelZip";
			this.labelZip.Size = new System.Drawing.Size(96, 16);
			this.labelZip.TabIndex = 66;
			this.labelZip.Text = "Zip";
			this.labelZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(21, 345);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 24);
			this.butDelete.TabIndex = 12;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 108);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 16);
			this.label1.TabIndex = 72;
			this.label1.Text = "Date Stop";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateStop
			// 
			this.textDateStop.Location = new System.Drawing.Point(114, 107);
			this.textDateStop.Name = "textDateStop";
			this.textDateStop.Size = new System.Drawing.Size(100, 20);
			this.textDateStop.TabIndex = 71;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 83);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(106, 16);
			this.label2.TabIndex = 70;
			this.label2.Text = "Date Start";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(114, 81);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(100, 20);
			this.textDateStart.TabIndex = 69;
			// 
			// textChargeAmt
			// 
			this.textChargeAmt.Location = new System.Drawing.Point(114, 55);
			this.textChargeAmt.Name = "textChargeAmt";
			this.textChargeAmt.Size = new System.Drawing.Size(100, 20);
			this.textChargeAmt.TabIndex = 68;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 56);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(106, 16);
			this.label5.TabIndex = 67;
			this.label5.Text = "Charge Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupRecurringCharges
			// 
			this.groupRecurringCharges.Controls.Add(this.labelPayPlan);
			this.groupRecurringCharges.Controls.Add(this.comboPaymentPlans);
			this.groupRecurringCharges.Controls.Add(this.butToday);
			this.groupRecurringCharges.Controls.Add(this.butClear);
			this.groupRecurringCharges.Controls.Add(this.textNote);
			this.groupRecurringCharges.Controls.Add(this.label7);
			this.groupRecurringCharges.Controls.Add(this.label6);
			this.groupRecurringCharges.Controls.Add(this.textChargeAmt);
			this.groupRecurringCharges.Controls.Add(this.label1);
			this.groupRecurringCharges.Controls.Add(this.label5);
			this.groupRecurringCharges.Controls.Add(this.textDateStop);
			this.groupRecurringCharges.Controls.Add(this.textDateStart);
			this.groupRecurringCharges.Controls.Add(this.label2);
			this.groupRecurringCharges.Location = new System.Drawing.Point(12, 120);
			this.groupRecurringCharges.Name = "groupRecurringCharges";
			this.groupRecurringCharges.Size = new System.Drawing.Size(479, 210);
			this.groupRecurringCharges.TabIndex = 73;
			this.groupRecurringCharges.TabStop = false;
			this.groupRecurringCharges.Text = "Authorized Recurring Charges";
			// 
			// labelPayPlan
			// 
			this.labelPayPlan.Location = new System.Drawing.Point(0, 27);
			this.labelPayPlan.Name = "labelPayPlan";
			this.labelPayPlan.Size = new System.Drawing.Size(112, 16);
			this.labelPayPlan.TabIndex = 132;
			this.labelPayPlan.Text = "Payment Plan";
			this.labelPayPlan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboPaymentPlans
			// 
			this.comboPaymentPlans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPaymentPlans.Location = new System.Drawing.Point(114, 26);
			this.comboPaymentPlans.MaxDropDownItems = 30;
			this.comboPaymentPlans.Name = "comboPaymentPlans";
			this.comboPaymentPlans.Size = new System.Drawing.Size(167, 21);
			this.comboPaymentPlans.TabIndex = 131;
			// 
			// butToday
			// 
			this.butToday.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butToday.Autosize = true;
			this.butToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butToday.CornerRadius = 4F;
			this.butToday.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butToday.Location = new System.Drawing.Point(218, 80);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(63, 22);
			this.butToday.TabIndex = 77;
			this.butToday.Text = "Today";
			this.butToday.Click += new System.EventHandler(this.butToday_Click);
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClear.Autosize = true;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClear.CornerRadius = 4F;
			this.butClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClear.Location = new System.Drawing.Point(218, 54);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(63, 22);
			this.butClear.TabIndex = 76;
			this.butClear.Text = "Clear";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(114, 131);
			this.textNote.MaxLength = 10000;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(344, 64);
			this.textNote.TabIndex = 75;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(-43, 134);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(156, 16);
			this.label7.TabIndex = 74;
			this.label7.Text = "Note";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(318, 56);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(146, 71);
			this.label6.TabIndex = 73;
			this.label6.Text = "Date Stop will be blank if the charges will be repeated indefinitely.  Clear all " +
    "these values if no further recurring charges are planned.";
			// 
			// FormCreditCardEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(510, 381);
			this.Controls.Add(this.groupRecurringCharges);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textZip);
			this.Controls.Add(this.labelAddress);
			this.Controls.Add(this.textAddress);
			this.Controls.Add(this.labelZip);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.textExpDate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textCardNumber);
			this.Controls.Add(this.label3);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormCreditCardEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Credit Card Edit";
			this.Load += new System.EventHandler(this.FormCreditCardEdit_Load);
			this.groupRecurringCharges.ResumeLayout(false);
			this.groupRecurringCharges.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textCardNumber;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textExpDate;
		private System.Drawing.Printing.PrintDocument pd2;
		private UI.Button butOK;
		private UI.Button butCancel;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.Label labelAddress;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.Label labelZip;
		private UI.Button butDelete;
		private System.Windows.Forms.Label label1;
		private ValidDate textDateStop;
		private System.Windows.Forms.Label label2;
		private ValidDate textDateStart;
		private ValidDouble textChargeAmt;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupRecurringCharges;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label7;
		private UI.Button butClear;
		private UI.Button butToday;
		private System.Windows.Forms.Label labelPayPlan;
		private System.Windows.Forms.ComboBox comboPaymentPlans;
	}
}