namespace OpenDental{
	partial class FormApptPrintSetup {
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
			this.labelPagesAcross = new System.Windows.Forms.Label();
			this.textAcross = new OpenDental.ValidNumber();
			this.textTall = new OpenDental.ValidNumber();
			this.labelPagesTall = new System.Windows.Forms.Label();
			this.labelStartTime = new System.Windows.Forms.Label();
			this.labelStopTime = new System.Windows.Forms.Label();
			this.textStartTime = new OpenDental.ValidDate();
			this.textStopTime = new OpenDental.ValidDate();
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
			this.butOK.Location = new System.Drawing.Point(141,174);
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
			this.butCancel.Location = new System.Drawing.Point(222,174);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// labelPagesAcross
			// 
			this.labelPagesAcross.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelPagesAcross.Location = new System.Drawing.Point(29,41);
			this.labelPagesAcross.Name = "labelPagesAcross";
			this.labelPagesAcross.Size = new System.Drawing.Size(95,15);
			this.labelPagesAcross.TabIndex = 72;
			this.labelPagesAcross.Text = "Pages Across";
			// 
			// textAcross
			// 
			this.textAcross.Location = new System.Drawing.Point(27,57);
			this.textAcross.MaxVal = 255;
			this.textAcross.MinVal = 0;
			this.textAcross.Name = "textAcross";
			this.textAcross.Size = new System.Drawing.Size(100,20);
			this.textAcross.TabIndex = 73;
			this.textAcross.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textTall
			// 
			this.textTall.Location = new System.Drawing.Point(27,98);
			this.textTall.MaxVal = 255;
			this.textTall.MinVal = 0;
			this.textTall.Name = "textTall";
			this.textTall.Size = new System.Drawing.Size(100,20);
			this.textTall.TabIndex = 75;
			this.textTall.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// labelPagesTall
			// 
			this.labelPagesTall.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelPagesTall.Location = new System.Drawing.Point(29,82);
			this.labelPagesTall.Name = "labelPagesTall";
			this.labelPagesTall.Size = new System.Drawing.Size(95,15);
			this.labelPagesTall.TabIndex = 74;
			this.labelPagesTall.Text = "Pages Tall";
			// 
			// labelStartTime
			// 
			this.labelStartTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelStartTime.Location = new System.Drawing.Point(176,82);
			this.labelStartTime.Name = "labelStartTime";
			this.labelStartTime.Size = new System.Drawing.Size(95,15);
			this.labelStartTime.TabIndex = 76;
			this.labelStartTime.Text = "Start Time";
			// 
			// labelStopTime
			// 
			this.labelStopTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelStopTime.Location = new System.Drawing.Point(176,41);
			this.labelStopTime.Name = "labelStopTime";
			this.labelStopTime.Size = new System.Drawing.Size(95,15);
			this.labelStopTime.TabIndex = 78;
			this.labelStopTime.Text = "Stop Time";
			// 
			// textStartTime
			// 
			this.textStartTime.Location = new System.Drawing.Point(174,98);
			this.textStartTime.Name = "textStartTime";
			this.textStartTime.Size = new System.Drawing.Size(100,20);
			this.textStartTime.TabIndex = 80;
			// 
			// textStopTime
			// 
			this.textStopTime.Location = new System.Drawing.Point(174,57);
			this.textStopTime.Name = "textStopTime";
			this.textStopTime.Size = new System.Drawing.Size(100,20);
			this.textStopTime.TabIndex = 81;
			// 
			// FormApptPrintSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(309,210);
			this.Controls.Add(this.textStopTime);
			this.Controls.Add(this.textStartTime);
			this.Controls.Add(this.labelStopTime);
			this.Controls.Add(this.labelStartTime);
			this.Controls.Add(this.textTall);
			this.Controls.Add(this.labelPagesTall);
			this.Controls.Add(this.textAcross);
			this.Controls.Add(this.labelPagesAcross);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormApptPrintSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form Appt Print Setup";
			this.Load += new System.EventHandler(this.FormApptPrintSetup_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label labelPagesAcross;
		private ValidNumber textAcross;
		private ValidNumber textTall;
		private System.Windows.Forms.Label labelPagesTall;
		private System.Windows.Forms.Label labelStartTime;
		private System.Windows.Forms.Label labelStopTime;
		private ValidDate textStartTime;
		private ValidDate textStopTime;
	}
}