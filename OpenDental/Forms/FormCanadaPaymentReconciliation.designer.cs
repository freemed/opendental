namespace OpenDental{
	partial class FormCanadaPaymentReconciliation {
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
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.listCarriers = new System.Windows.Forms.ListBox();
			this.listNetworks = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.listBillingProvider = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.listTreatingProvider = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textDateReconciliation = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupCarrierOrNetwork = new System.Windows.Forms.GroupBox();
			this.groupProviders = new System.Windows.Forms.GroupBox();
			this.checkGetForAllCarriers = new System.Windows.Forms.CheckBox();
			this.groupCarrierOrNetwork.SuspendLayout();
			this.groupProviders.SuspendLayout();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6,251);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(151,17);
			this.label5.TabIndex = 105;
			this.label5.Text = "Network";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6,16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(278,17);
			this.label1.TabIndex = 106;
			this.label1.Text = "Carrier";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// listCarriers
			// 
			this.listCarriers.FormattingEnabled = true;
			this.listCarriers.Location = new System.Drawing.Point(9,36);
			this.listCarriers.Name = "listCarriers";
			this.listCarriers.Size = new System.Drawing.Size(275,212);
			this.listCarriers.TabIndex = 107;
			this.listCarriers.Click += new System.EventHandler(this.listCarriers_Click);
			// 
			// listNetworks
			// 
			this.listNetworks.FormattingEnabled = true;
			this.listNetworks.Location = new System.Drawing.Point(9,271);
			this.listNetworks.Name = "listNetworks";
			this.listNetworks.Size = new System.Drawing.Size(275,160);
			this.listNetworks.TabIndex = 108;
			this.listNetworks.Click += new System.EventHandler(this.listNetwork_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8,16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(278,17);
			this.label2.TabIndex = 109;
			this.label2.Text = "Billing Provider";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// listBillingProvider
			// 
			this.listBillingProvider.FormattingEnabled = true;
			this.listBillingProvider.Location = new System.Drawing.Point(11,36);
			this.listBillingProvider.Name = "listBillingProvider";
			this.listBillingProvider.Size = new System.Drawing.Size(275,212);
			this.listBillingProvider.TabIndex = 110;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8,251);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(278,17);
			this.label3.TabIndex = 111;
			this.label3.Text = "Treating Provider";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// listTreatingProvider
			// 
			this.listTreatingProvider.FormattingEnabled = true;
			this.listTreatingProvider.Location = new System.Drawing.Point(11,271);
			this.listTreatingProvider.Name = "listTreatingProvider";
			this.listTreatingProvider.Size = new System.Drawing.Size(276,160);
			this.listTreatingProvider.TabIndex = 112;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(379,473);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(103,17);
			this.label4.TabIndex = 113;
			this.label4.Text = "Reconciliation Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textDateReconciliation
			// 
			this.textDateReconciliation.Location = new System.Drawing.Point(488,472);
			this.textDateReconciliation.Name = "textDateReconciliation";
			this.textDateReconciliation.Size = new System.Drawing.Size(100,20);
			this.textDateReconciliation.TabIndex = 114;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(442,505);
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
			this.butCancel.Location = new System.Drawing.Point(523,505);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// groupCarrierOrNetwork
			// 
			this.groupCarrierOrNetwork.Controls.Add(this.label1);
			this.groupCarrierOrNetwork.Controls.Add(this.label5);
			this.groupCarrierOrNetwork.Controls.Add(this.listCarriers);
			this.groupCarrierOrNetwork.Controls.Add(this.listNetworks);
			this.groupCarrierOrNetwork.Enabled = false;
			this.groupCarrierOrNetwork.Location = new System.Drawing.Point(3,29);
			this.groupCarrierOrNetwork.Name = "groupCarrierOrNetwork";
			this.groupCarrierOrNetwork.Size = new System.Drawing.Size(293,437);
			this.groupCarrierOrNetwork.TabIndex = 115;
			this.groupCarrierOrNetwork.TabStop = false;
			this.groupCarrierOrNetwork.Text = "Carrier or Network";
			// 
			// groupProviders
			// 
			this.groupProviders.Controls.Add(this.label2);
			this.groupProviders.Controls.Add(this.listBillingProvider);
			this.groupProviders.Controls.Add(this.label3);
			this.groupProviders.Controls.Add(this.listTreatingProvider);
			this.groupProviders.Location = new System.Drawing.Point(302,29);
			this.groupProviders.Name = "groupProviders";
			this.groupProviders.Size = new System.Drawing.Size(296,437);
			this.groupProviders.TabIndex = 116;
			this.groupProviders.TabStop = false;
			this.groupProviders.Text = "Providers";
			// 
			// checkGetForAllCarriers
			// 
			this.checkGetForAllCarriers.AutoSize = true;
			this.checkGetForAllCarriers.Checked = true;
			this.checkGetForAllCarriers.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkGetForAllCarriers.Location = new System.Drawing.Point(12,6);
			this.checkGetForAllCarriers.Name = "checkGetForAllCarriers";
			this.checkGetForAllCarriers.Size = new System.Drawing.Size(177,17);
			this.checkGetForAllCarriers.TabIndex = 117;
			this.checkGetForAllCarriers.Text = "Get Transactions For All Carriers";
			this.checkGetForAllCarriers.UseVisualStyleBackColor = true;
			this.checkGetForAllCarriers.Click += new System.EventHandler(this.checkGetForAllCarriers_Click);
			// 
			// FormCanadaPaymentReconciliation
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(607,541);
			this.Controls.Add(this.checkGetForAllCarriers);
			this.Controls.Add(this.groupProviders);
			this.Controls.Add(this.groupCarrierOrNetwork);
			this.Controls.Add(this.textDateReconciliation);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormCanadaPaymentReconciliation";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Payment Reconciliation Request";
			this.Load += new System.EventHandler(this.FormCanadaPaymentReconciliation_Load);
			this.groupCarrierOrNetwork.ResumeLayout(false);
			this.groupProviders.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listCarriers;
		private System.Windows.Forms.ListBox listNetworks;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listBillingProvider;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listTreatingProvider;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textDateReconciliation;
		private System.Windows.Forms.GroupBox groupCarrierOrNetwork;
		private System.Windows.Forms.GroupBox groupProviders;
		private System.Windows.Forms.CheckBox checkGetForAllCarriers;
	}
}