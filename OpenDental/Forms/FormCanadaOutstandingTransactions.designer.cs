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
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.checkGetForAllCarriers = new System.Windows.Forms.CheckBox();
			this.groupCarrier = new System.Windows.Forms.GroupBox();
			this.groupCarrier.SuspendLayout();
			this.SuspendLayout();
			// 
			// listCarriers
			// 
			this.listCarriers.FormattingEnabled = true;
			this.listCarriers.Location = new System.Drawing.Point(6,19);
			this.listCarriers.Name = "listCarriers";
			this.listCarriers.Size = new System.Drawing.Size(280,212);
			this.listCarriers.TabIndex = 107;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(137,291);
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
			this.butCancel.Location = new System.Drawing.Point(218,291);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// checkGetForAllCarriers
			// 
			this.checkGetForAllCarriers.AutoSize = true;
			this.checkGetForAllCarriers.Checked = true;
			this.checkGetForAllCarriers.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkGetForAllCarriers.Location = new System.Drawing.Point(12,12);
			this.checkGetForAllCarriers.Name = "checkGetForAllCarriers";
			this.checkGetForAllCarriers.Size = new System.Drawing.Size(177,17);
			this.checkGetForAllCarriers.TabIndex = 108;
			this.checkGetForAllCarriers.Text = "Get Transactions For All Carriers";
			this.checkGetForAllCarriers.UseVisualStyleBackColor = true;
			this.checkGetForAllCarriers.Click += new System.EventHandler(this.checkGetForAllCarriers_Click);
			// 
			// groupCarrier
			// 
			this.groupCarrier.Controls.Add(this.listCarriers);
			this.groupCarrier.Enabled = false;
			this.groupCarrier.Location = new System.Drawing.Point(12,35);
			this.groupCarrier.Name = "groupCarrier";
			this.groupCarrier.Size = new System.Drawing.Size(293,239);
			this.groupCarrier.TabIndex = 109;
			this.groupCarrier.TabStop = false;
			this.groupCarrier.Text = "Carrier";
			// 
			// FormCanadaOutstandingTransactions
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(318,331);
			this.Controls.Add(this.groupCarrier);
			this.Controls.Add(this.checkGetForAllCarriers);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormCanadaOutstandingTransactions";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Outstanding Transactions Request";
			this.Load += new System.EventHandler(this.FormCanadaPaymentReconciliation_Load);
			this.groupCarrier.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.ListBox listCarriers;
		private System.Windows.Forms.CheckBox checkGetForAllCarriers;
		private System.Windows.Forms.GroupBox groupCarrier;
	}
}