namespace OpenDental{
	partial class FormWebFormSetup {
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
			this.textboxWebHostAddress = new System.Windows.Forms.TextBox();
			this.labelWebhostURL = new System.Windows.Forms.Label();
			this.labelBorderColor = new System.Windows.Forms.Label();
			this.textBoxWebformsHeading1 = new System.Windows.Forms.TextBox();
			this.labelWebformsHeading1 = new System.Windows.Forms.Label();
			this.textBoxWebformsHeading2 = new System.Windows.Forms.TextBox();
			this.labelWebformsHeading2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butChange = new OpenDental.UI.Button();
			this.butWebformBorderColor = new System.Windows.Forms.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxWebFormAddress = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// textboxWebHostAddress
			// 
			this.textboxWebHostAddress.Location = new System.Drawing.Point(195,34);
			this.textboxWebHostAddress.Name = "textboxWebHostAddress";
			this.textboxWebHostAddress.Size = new System.Drawing.Size(320,20);
			this.textboxWebHostAddress.TabIndex = 45;
			// 
			// labelWebhostURL
			// 
			this.labelWebhostURL.Location = new System.Drawing.Point(24,35);
			this.labelWebhostURL.Name = "labelWebhostURL";
			this.labelWebhostURL.Size = new System.Drawing.Size(169,19);
			this.labelWebhostURL.TabIndex = 46;
			this.labelWebhostURL.Text = "Server Address for WebHost";
			this.labelWebhostURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelBorderColor
			// 
			this.labelBorderColor.Location = new System.Drawing.Point(28,24);
			this.labelBorderColor.Name = "labelBorderColor";
			this.labelBorderColor.Size = new System.Drawing.Size(141,19);
			this.labelBorderColor.TabIndex = 48;
			this.labelBorderColor.Text = "Border Color for Webforms";
			this.labelBorderColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxWebformsHeading1
			// 
			this.textBoxWebformsHeading1.Location = new System.Drawing.Point(171,54);
			this.textBoxWebformsHeading1.Name = "textBoxWebformsHeading1";
			this.textBoxWebformsHeading1.Size = new System.Drawing.Size(320,20);
			this.textBoxWebformsHeading1.TabIndex = 49;
			// 
			// labelWebformsHeading1
			// 
			this.labelWebformsHeading1.Location = new System.Drawing.Point(55,54);
			this.labelWebformsHeading1.Name = "labelWebformsHeading1";
			this.labelWebformsHeading1.Size = new System.Drawing.Size(114,19);
			this.labelWebformsHeading1.TabIndex = 50;
			this.labelWebformsHeading1.Text = "Webforms Heading 1";
			this.labelWebformsHeading1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxWebformsHeading2
			// 
			this.textBoxWebformsHeading2.Location = new System.Drawing.Point(171,75);
			this.textBoxWebformsHeading2.Multiline = true;
			this.textBoxWebformsHeading2.Name = "textBoxWebformsHeading2";
			this.textBoxWebformsHeading2.Size = new System.Drawing.Size(320,74);
			this.textBoxWebformsHeading2.TabIndex = 51;
			// 
			// labelWebformsHeading2
			// 
			this.labelWebformsHeading2.Location = new System.Drawing.Point(55,75);
			this.labelWebformsHeading2.Name = "labelWebformsHeading2";
			this.labelWebformsHeading2.Size = new System.Drawing.Size(114,19);
			this.labelWebformsHeading2.TabIndex = 52;
			this.labelWebformsHeading2.Text = "Webforms Heading 2";
			this.labelWebformsHeading2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.butChange);
			this.groupBox2.Controls.Add(this.butWebformBorderColor);
			this.groupBox2.Controls.Add(this.labelBorderColor);
			this.groupBox2.Controls.Add(this.textBoxWebformsHeading2);
			this.groupBox2.Controls.Add(this.labelWebformsHeading2);
			this.groupBox2.Controls.Add(this.labelWebformsHeading1);
			this.groupBox2.Controls.Add(this.textBoxWebformsHeading1);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(24,61);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(515,160);
			this.groupBox2.TabIndex = 53;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Web Form Preferences";
			// 
			// butChange
			// 
			this.butChange.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChange.Autosize = true;
			this.butChange.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChange.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChange.CornerRadius = 4F;
			this.butChange.Location = new System.Drawing.Point(210,24);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(75,24);
			this.butChange.TabIndex = 72;
			this.butChange.Text = "Change";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// butWebformBorderColor
			// 
			this.butWebformBorderColor.BackColor = System.Drawing.Color.RoyalBlue;
			this.butWebformBorderColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butWebformBorderColor.Location = new System.Drawing.Point(171,24);
			this.butWebformBorderColor.Name = "butWebformBorderColor";
			this.butWebformBorderColor.Size = new System.Drawing.Size(24,24);
			this.butWebformBorderColor.TabIndex = 71;
			this.butWebformBorderColor.UseVisualStyleBackColor = false;
			this.butWebformBorderColor.Click += new System.EventHandler(this.butWebformBorderColor_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24,8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(169,19);
			this.label1.TabIndex = 54;
			this.label1.Text = "Browser address for web form";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxWebFormAddress
			// 
			this.textBoxWebFormAddress.Location = new System.Drawing.Point(195,8);
			this.textBoxWebFormAddress.Name = "textBoxWebFormAddress";
			this.textBoxWebFormAddress.Size = new System.Drawing.Size(320,20);
			this.textBoxWebFormAddress.TabIndex = 55;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(576,158);
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
			this.butCancel.Location = new System.Drawing.Point(576,196);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// FormWebFormSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(683,234);
			this.Controls.Add(this.textBoxWebFormAddress);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.textboxWebHostAddress);
			this.Controls.Add(this.labelWebhostURL);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FormWebFormSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Web Form Setup";
			this.Load += new System.EventHandler(this.FormWebFormSetup_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textboxWebHostAddress;
		private System.Windows.Forms.Label labelWebhostURL;
		private System.Windows.Forms.Label labelBorderColor;
		private System.Windows.Forms.TextBox textBoxWebformsHeading1;
		private System.Windows.Forms.Label labelWebformsHeading1;
		private System.Windows.Forms.TextBox textBoxWebformsHeading2;
		private System.Windows.Forms.Label labelWebformsHeading2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button butWebformBorderColor;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxWebFormAddress;
		private OpenDental.UI.Button butChange;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
	}
}