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
			this.labelColumnsPerPage = new System.Windows.Forms.Label();
			this.labelFontSize = new System.Windows.Forms.Label();
			this.labelStartTime = new System.Windows.Forms.Label();
			this.labelStopTime = new System.Windows.Forms.Label();
			this.textStopTime = new System.Windows.Forms.TextBox();
			this.textStartTime = new System.Windows.Forms.TextBox();
			this.textFontSize = new ODR.ValidDouble();
			this.butSave = new OpenDental.UI.Button();
			this.textColumnsPerPage = new OpenDental.ValidNumber();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// labelColumnsPerPage
			// 
			this.labelColumnsPerPage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelColumnsPerPage.Location = new System.Drawing.Point(152,24);
			this.labelColumnsPerPage.Name = "labelColumnsPerPage";
			this.labelColumnsPerPage.Size = new System.Drawing.Size(128,15);
			this.labelColumnsPerPage.TabIndex = 72;
			this.labelColumnsPerPage.Text = "Columns per page";
			// 
			// labelFontSize
			// 
			this.labelFontSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelFontSize.Location = new System.Drawing.Point(152,66);
			this.labelFontSize.Name = "labelFontSize";
			this.labelFontSize.Size = new System.Drawing.Size(95,15);
			this.labelFontSize.TabIndex = 74;
			this.labelFontSize.Text = "Font size";
			// 
			// labelStartTime
			// 
			this.labelStartTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelStartTime.Location = new System.Drawing.Point(29,65);
			this.labelStartTime.Name = "labelStartTime";
			this.labelStartTime.Size = new System.Drawing.Size(95,15);
			this.labelStartTime.TabIndex = 76;
			this.labelStartTime.Text = "Start time";
			// 
			// labelStopTime
			// 
			this.labelStopTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelStopTime.Location = new System.Drawing.Point(29,24);
			this.labelStopTime.Name = "labelStopTime";
			this.labelStopTime.Size = new System.Drawing.Size(95,15);
			this.labelStopTime.TabIndex = 78;
			this.labelStopTime.Text = "Stop time";
			// 
			// textStopTime
			// 
			this.textStopTime.Location = new System.Drawing.Point(27,40);
			this.textStopTime.Name = "textStopTime";
			this.textStopTime.Size = new System.Drawing.Size(100,20);
			this.textStopTime.TabIndex = 83;
			// 
			// textStartTime
			// 
			this.textStartTime.Location = new System.Drawing.Point(27,82);
			this.textStartTime.Name = "textStartTime";
			this.textStartTime.Size = new System.Drawing.Size(100,20);
			this.textStartTime.TabIndex = 84;
			// 
			// textFontSize
			// 
			this.textFontSize.Location = new System.Drawing.Point(150,82);
			this.textFontSize.Name = "textFontSize";
			this.textFontSize.Size = new System.Drawing.Size(100,20);
			this.textFontSize.TabIndex = 85;
			// 
			// butSave
			// 
			this.butSave.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSave.Autosize = true;
			this.butSave.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSave.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSave.CornerRadius = 4F;
			this.butSave.Location = new System.Drawing.Point(12,164);
			this.butSave.Name = "butSave";
			this.butSave.Size = new System.Drawing.Size(75,24);
			this.butSave.TabIndex = 82;
			this.butSave.Text = "Save";
			this.butSave.Click += new System.EventHandler(this.butSave_Click);
			// 
			// textColumnsPerPage
			// 
			this.textColumnsPerPage.Location = new System.Drawing.Point(150,40);
			this.textColumnsPerPage.MaxVal = 255;
			this.textColumnsPerPage.MinVal = 0;
			this.textColumnsPerPage.Name = "textColumnsPerPage";
			this.textColumnsPerPage.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.textColumnsPerPage.Size = new System.Drawing.Size(100,20);
			this.textColumnsPerPage.TabIndex = 73;
			this.textColumnsPerPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(205,134);
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
			this.butCancel.Location = new System.Drawing.Point(204,164);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormApptPrintSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(291,200);
			this.Controls.Add(this.textFontSize);
			this.Controls.Add(this.textStartTime);
			this.Controls.Add(this.textStopTime);
			this.Controls.Add(this.butSave);
			this.Controls.Add(this.labelStopTime);
			this.Controls.Add(this.labelStartTime);
			this.Controls.Add(this.labelFontSize);
			this.Controls.Add(this.textColumnsPerPage);
			this.Controls.Add(this.labelColumnsPerPage);
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
		private System.Windows.Forms.Label labelColumnsPerPage;
		private ValidNumber textColumnsPerPage;
		private System.Windows.Forms.Label labelFontSize;
		private System.Windows.Forms.Label labelStartTime;
		private System.Windows.Forms.Label labelStopTime;
		private UI.Button butSave;
		private System.Windows.Forms.TextBox textStopTime;
		private System.Windows.Forms.TextBox textStartTime;
		private ODR.ValidDouble textFontSize;
	}
}