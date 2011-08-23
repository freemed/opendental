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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkReportsShowPatNum = new System.Windows.Forms.CheckBox();
			this.checkReportProdWO = new System.Windows.Forms.CheckBox();
			this.checkReportsProcDate = new System.Windows.Forms.CheckBox();
			this.butAgg = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkReportsShowPatNum);
			this.groupBox1.Controls.Add(this.checkReportProdWO);
			this.groupBox1.Controls.Add(this.checkReportsProcDate);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(12,12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(453,73);
			this.groupBox1.TabIndex = 203;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Reports";
			// 
			// checkReportsShowPatNum
			// 
			this.checkReportsShowPatNum.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkReportsShowPatNum.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkReportsShowPatNum.Location = new System.Drawing.Point(23,32);
			this.checkReportsShowPatNum.Name = "checkReportsShowPatNum";
			this.checkReportsShowPatNum.Size = new System.Drawing.Size(414,17);
			this.checkReportsShowPatNum.TabIndex = 200;
			this.checkReportsShowPatNum.Text = "Show PatNum: Aging, OutstandingIns, ProcsNotBilled";
			this.checkReportsShowPatNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkReportProdWO
			// 
			this.checkReportProdWO.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkReportProdWO.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkReportProdWO.Location = new System.Drawing.Point(23,51);
			this.checkReportProdWO.Name = "checkReportProdWO";
			this.checkReportProdWO.Size = new System.Drawing.Size(414,17);
			this.checkReportProdWO.TabIndex = 201;
			this.checkReportProdWO.Text = "Monthly P&&I scheduled production subtracts PPO writeoffs";
			this.checkReportProdWO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkReportsProcDate
			// 
			this.checkReportsProcDate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkReportsProcDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkReportsProcDate.Location = new System.Drawing.Point(75,13);
			this.checkReportsProcDate.Name = "checkReportsProcDate";
			this.checkReportsProcDate.Size = new System.Drawing.Size(362,17);
			this.checkReportsProcDate.TabIndex = 199;
			this.checkReportsProcDate.Text = "Default to using Proc Date for PPO writeoffs";
			this.checkReportsProcDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butAgg
			// 
			this.butAgg.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAgg.Autosize = true;
			this.butAgg.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAgg.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAgg.CornerRadius = 4F;
			this.butAgg.Location = new System.Drawing.Point(12,91);
			this.butAgg.Name = "butAgg";
			this.butAgg.Size = new System.Drawing.Size(75,24);
			this.butAgg.TabIndex = 204;
			this.butAgg.Text = "Agg Setup";
			this.butAgg.Click += new System.EventHandler(this.butAgg_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(377,151);
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
			this.butCancel.Location = new System.Drawing.Point(377,192);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormReportSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(477,243);
			this.Controls.Add(this.butAgg);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormReportSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkReportsShowPatNum;
		private System.Windows.Forms.CheckBox checkReportProdWO;
		private System.Windows.Forms.CheckBox checkReportsProcDate;
		private UI.Button butAgg;
	}
}