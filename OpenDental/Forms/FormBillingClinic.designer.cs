namespace OpenDental{
	partial class FormBillingClinic {
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
			this.labelClinic = new System.Windows.Forms.Label();
			this.listClinic = new System.Windows.Forms.ListBox();
			this.butOK = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(21, 9);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(179, 21);
			this.labelClinic.TabIndex = 253;
			this.labelClinic.Text = "Select a clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listClinic
			// 
			this.listClinic.FormattingEnabled = true;
			this.listClinic.Location = new System.Drawing.Point(24, 34);
			this.listClinic.Name = "listClinic";
			this.listClinic.Size = new System.Drawing.Size(176, 264);
			this.listClinic.TabIndex = 254;
			this.listClinic.DoubleClick += new System.EventHandler(this.listClinic_DoubleClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(187, 321);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormBillingClinic
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(274, 357);
			this.Controls.Add(this.listClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.butOK);
			this.Name = "FormBillingClinic";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Billing Clinics";
			this.Load += new System.EventHandler(this.FormBillingClinic_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label labelClinic;
		private System.Windows.Forms.ListBox listClinic;
	}
}