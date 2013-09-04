namespace OpenDental{
	partial class FormEhrSettings {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEhrSettings));
			this.checkAlertHighSeverity = new System.Windows.Forms.CheckBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.checkMU2 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// checkAlertHighSeverity
			// 
			this.checkAlertHighSeverity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAlertHighSeverity.Location = new System.Drawing.Point(43, 32);
			this.checkAlertHighSeverity.Name = "checkAlertHighSeverity";
			this.checkAlertHighSeverity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkAlertHighSeverity.Size = new System.Drawing.Size(280, 20);
			this.checkAlertHighSeverity.TabIndex = 4;
			this.checkAlertHighSeverity.Text = "Only show high significance Rx alerts";
			this.checkAlertHighSeverity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAlertHighSeverity.UseVisualStyleBackColor = true;
			this.checkAlertHighSeverity.Click += new System.EventHandler(this.checkAlertHighSeverity_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(233, 112);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 3;
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
			this.butCancel.Location = new System.Drawing.Point(314, 112);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// checkMU2
			// 
			this.checkMU2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMU2.Location = new System.Drawing.Point(43, 58);
			this.checkMU2.Name = "checkMU2";
			this.checkMU2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkMU2.Size = new System.Drawing.Size(280, 20);
			this.checkMU2.TabIndex = 5;
			this.checkMU2.Text = "Meaningful Use Stage 2";
			this.checkMU2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMU2.UseVisualStyleBackColor = true;
			this.checkMU2.Click += new System.EventHandler(this.checkMU2_Click);
			// 
			// FormEhrSettings
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(401, 148);
			this.Controls.Add(this.checkMU2);
			this.Controls.Add(this.checkAlertHighSeverity);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormEhrSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "EHR Settings";
			this.Load += new System.EventHandler(this.FormEhrSettings_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.CheckBox checkAlertHighSeverity;
		private System.Windows.Forms.CheckBox checkMU2;
	}
}