namespace OpenDental{
	partial class FormReportSetup {
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
			this.checkReportsProcDate = new System.Windows.Forms.CheckBox();
			this.checkReportProdWO = new System.Windows.Forms.CheckBox();
			this.checkReportsShowPatNum = new System.Windows.Forms.CheckBox();
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
			this.butOK.Location = new System.Drawing.Point(233,112);
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
			this.butCancel.Location = new System.Drawing.Point(314,112);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// checkReportsProcDate
			// 
			this.checkReportsProcDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkReportsProcDate.Location = new System.Drawing.Point(20,19);
			this.checkReportsProcDate.Name = "checkReportsProcDate";
			this.checkReportsProcDate.Size = new System.Drawing.Size(333,17);
			this.checkReportsProcDate.TabIndex = 199;
			this.checkReportsProcDate.Text = "Default to using Proc Date for PPO writeoffs";
			// 
			// checkReportProdWO
			// 
			this.checkReportProdWO.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkReportProdWO.Location = new System.Drawing.Point(20,57);
			this.checkReportProdWO.Name = "checkReportProdWO";
			this.checkReportProdWO.Size = new System.Drawing.Size(385,17);
			this.checkReportProdWO.TabIndex = 201;
			this.checkReportProdWO.Text = "Monthly P&&I scheduled production subtracts PPO writeoffs";
			// 
			// checkReportsShowPatNum
			// 
			this.checkReportsShowPatNum.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkReportsShowPatNum.Location = new System.Drawing.Point(20,38);
			this.checkReportsShowPatNum.Name = "checkReportsShowPatNum";
			this.checkReportsShowPatNum.Size = new System.Drawing.Size(385,17);
			this.checkReportsShowPatNum.TabIndex = 200;
			this.checkReportsShowPatNum.Text = "Show PatNum: Aging, OutstandingIns, ProcsNotBilled";
			// 
			// FormReportSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(401,148);
			this.Controls.Add(this.checkReportsShowPatNum);
			this.Controls.Add(this.checkReportProdWO);
			this.Controls.Add(this.checkReportsProcDate);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormReportSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Report Setup";
			this.Load += new System.EventHandler(this.FormReportSetup_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.CheckBox checkReportsProcDate;
		private System.Windows.Forms.CheckBox checkReportProdWO;
		private System.Windows.Forms.CheckBox checkReportsShowPatNum;
	}
}