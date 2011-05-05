namespace OpenDental{
	partial class FormCanadaOutstandingTransactions {
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
			this.listCarriers = new System.Windows.Forms.ListBox();
			this.groupCarrier = new System.Windows.Forms.GroupBox();
			this.radioVersion2 = new System.Windows.Forms.RadioButton();
			this.radioVersion4Itrans = new System.Windows.Forms.RadioButton();
			this.radioVersion4ToCarrier = new System.Windows.Forms.RadioButton();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupOfficeNumber = new System.Windows.Forms.GroupBox();
			this.listOfficeNumbers = new System.Windows.Forms.ListBox();
			this.groupCarrier.SuspendLayout();
			this.groupOfficeNumber.SuspendLayout();
			this.SuspendLayout();
			// 
			// listCarriers
			// 
			this.listCarriers.FormattingEnabled = true;
			this.listCarriers.Location = new System.Drawing.Point(6,19);
			this.listCarriers.Name = "listCarriers";
			this.listCarriers.Size = new System.Drawing.Size(280,95);
			this.listCarriers.TabIndex = 107;
			// 
			// groupCarrier
			// 
			this.groupCarrier.Controls.Add(this.listCarriers);
			this.groupCarrier.Enabled = false;
			this.groupCarrier.Location = new System.Drawing.Point(29,81);
			this.groupCarrier.Name = "groupCarrier";
			this.groupCarrier.Size = new System.Drawing.Size(293,120);
			this.groupCarrier.TabIndex = 109;
			this.groupCarrier.TabStop = false;
			this.groupCarrier.Text = "Carrier";
			// 
			// radioVersion2
			// 
			this.radioVersion2.AutoSize = true;
			this.radioVersion2.Location = new System.Drawing.Point(12,12);
			this.radioVersion2.Name = "radioVersion2";
			this.radioVersion2.Size = new System.Drawing.Size(69,17);
			this.radioVersion2.TabIndex = 110;
			this.radioVersion2.Text = "Version 2";
			this.radioVersion2.UseVisualStyleBackColor = true;
			this.radioVersion2.Click += new System.EventHandler(this.radioVersion2_Click);
			// 
			// radioVersion4Itrans
			// 
			this.radioVersion4Itrans.AutoSize = true;
			this.radioVersion4Itrans.Checked = true;
			this.radioVersion4Itrans.Location = new System.Drawing.Point(12,35);
			this.radioVersion4Itrans.Name = "radioVersion4Itrans";
			this.radioVersion4Itrans.Size = new System.Drawing.Size(171,17);
			this.radioVersion4Itrans.TabIndex = 111;
			this.radioVersion4Itrans.TabStop = true;
			this.radioVersion4Itrans.Text = "Version 4 To ITRANS Network";
			this.radioVersion4Itrans.UseVisualStyleBackColor = true;
			this.radioVersion4Itrans.Click += new System.EventHandler(this.radioVersion4Itrans_Click);
			// 
			// radioVersion4ToCarrier
			// 
			this.radioVersion4ToCarrier.AutoSize = true;
			this.radioVersion4ToCarrier.Location = new System.Drawing.Point(12,58);
			this.radioVersion4ToCarrier.Name = "radioVersion4ToCarrier";
			this.radioVersion4ToCarrier.Size = new System.Drawing.Size(159,17);
			this.radioVersion4ToCarrier.TabIndex = 112;
			this.radioVersion4ToCarrier.TabStop = true;
			this.radioVersion4ToCarrier.Text = "Version 4 To Specific Carrier";
			this.radioVersion4ToCarrier.UseVisualStyleBackColor = true;
			this.radioVersion4ToCarrier.Click += new System.EventHandler(this.radioVersion4ToCarrier_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(166,279);
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
			this.butCancel.Location = new System.Drawing.Point(247,279);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// groupOfficeNumber
			// 
			this.groupOfficeNumber.Controls.Add(this.listOfficeNumbers);
			this.groupOfficeNumber.Location = new System.Drawing.Point(29,201);
			this.groupOfficeNumber.Name = "groupOfficeNumber";
			this.groupOfficeNumber.Size = new System.Drawing.Size(293,70);
			this.groupOfficeNumber.TabIndex = 113;
			this.groupOfficeNumber.TabStop = false;
			this.groupOfficeNumber.Text = "Office Number";
			// 
			// listOfficeNumbers
			// 
			this.listOfficeNumbers.FormattingEnabled = true;
			this.listOfficeNumbers.Location = new System.Drawing.Point(6,19);
			this.listOfficeNumbers.Name = "listOfficeNumbers";
			this.listOfficeNumbers.Size = new System.Drawing.Size(280,43);
			this.listOfficeNumbers.TabIndex = 0;
			// 
			// FormCanadaOutstandingTransactions
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(351,316);
			this.Controls.Add(this.groupOfficeNumber);
			this.Controls.Add(this.radioVersion4ToCarrier);
			this.Controls.Add(this.radioVersion4Itrans);
			this.Controls.Add(this.radioVersion2);
			this.Controls.Add(this.groupCarrier);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormCanadaOutstandingTransactions";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Outstanding Transactions Request";
			this.Load += new System.EventHandler(this.FormCanadaOutstandingTransactions_Load);
			this.groupCarrier.ResumeLayout(false);
			this.groupOfficeNumber.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.ListBox listCarriers;
		private System.Windows.Forms.GroupBox groupCarrier;
		private System.Windows.Forms.RadioButton radioVersion2;
		private System.Windows.Forms.RadioButton radioVersion4Itrans;
		private System.Windows.Forms.RadioButton radioVersion4ToCarrier;
		private System.Windows.Forms.GroupBox groupOfficeNumber;
		private System.Windows.Forms.ListBox listOfficeNumbers;
	}
}