namespace OpenDental{
	partial class FormMobileSetup {
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
			this.components = new System.ComponentModel.Container();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butSave = new OpenDental.UI.Button();
			this.textboxWebHostAddress = new System.Windows.Forms.TextBox();
			this.labelWebhostURL = new System.Windows.Forms.Label();
			this.labelTimeLastSynch = new System.Windows.Forms.Label();
			this.labelMinutesBetweenSynch = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.butSynchNow = new OpenDental.UI.Button();
			this.timerRefreshLastSynchTime = new System.Windows.Forms.Timer(this.components);
			this.labelTimeLastSynchDisplay = new System.Windows.Forms.Label();
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
			this.butOK.Location = new System.Drawing.Point(612,133);
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
			this.butCancel.Location = new System.Drawing.Point(612,174);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butSave
			// 
			this.butSave.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSave.Autosize = true;
			this.butSave.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSave.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSave.CornerRadius = 4F;
			this.butSave.Location = new System.Drawing.Point(616,52);
			this.butSave.Name = "butSave";
			this.butSave.Size = new System.Drawing.Size(64,24);
			this.butSave.TabIndex = 77;
			this.butSave.Text = "Save";
			this.butSave.Click += new System.EventHandler(this.butSave_Click);
			// 
			// textboxWebHostAddress
			// 
			this.textboxWebHostAddress.Location = new System.Drawing.Point(165,54);
			this.textboxWebHostAddress.Name = "textboxWebHostAddress";
			this.textboxWebHostAddress.Size = new System.Drawing.Size(445,20);
			this.textboxWebHostAddress.TabIndex = 75;
			// 
			// labelWebhostURL
			// 
			this.labelWebhostURL.Location = new System.Drawing.Point(-6,55);
			this.labelWebhostURL.Name = "labelWebhostURL";
			this.labelWebhostURL.Size = new System.Drawing.Size(169,19);
			this.labelWebhostURL.TabIndex = 76;
			this.labelWebhostURL.Text = "Host Server Address";
			this.labelWebhostURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelTimeLastSynch
			// 
			this.labelTimeLastSynch.Location = new System.Drawing.Point(0,9);
			this.labelTimeLastSynch.Name = "labelTimeLastSynch";
			this.labelTimeLastSynch.Size = new System.Drawing.Size(169,19);
			this.labelTimeLastSynch.TabIndex = 78;
			this.labelTimeLastSynch.Text = "Time of Last Synch";
			this.labelTimeLastSynch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelMinutesBetweenSynch
			// 
			this.labelMinutesBetweenSynch.Location = new System.Drawing.Point(-6,96);
			this.labelMinutesBetweenSynch.Name = "labelMinutesBetweenSynch";
			this.labelMinutesBetweenSynch.Size = new System.Drawing.Size(169,19);
			this.labelMinutesBetweenSynch.TabIndex = 79;
			this.labelMinutesBetweenSynch.Text = "Minutes between synch";
			this.labelMinutesBetweenSynch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(165,96);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(22,20);
			this.textBox1.TabIndex = 80;
			// 
			// butSynchNow
			// 
			this.butSynchNow.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSynchNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSynchNow.Autosize = true;
			this.butSynchNow.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSynchNow.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSynchNow.CornerRadius = 4F;
			this.butSynchNow.Location = new System.Drawing.Point(409,9);
			this.butSynchNow.Name = "butSynchNow";
			this.butSynchNow.Size = new System.Drawing.Size(75,24);
			this.butSynchNow.TabIndex = 81;
			this.butSynchNow.Text = "Synch Now";
			this.butSynchNow.Click += new System.EventHandler(this.butSynchNow_Click);
			// 
			// timerRefreshLastSynchTime
			// 
			this.timerRefreshLastSynchTime.Enabled = true;
			this.timerRefreshLastSynchTime.Interval = 10000;
			this.timerRefreshLastSynchTime.Tick += new System.EventHandler(this.timerRefreshLastSynchTime_Tick);
			// 
			// labelTimeLastSynchDisplay
			// 
			this.labelTimeLastSynchDisplay.Location = new System.Drawing.Point(175,9);
			this.labelTimeLastSynchDisplay.Name = "labelTimeLastSynchDisplay";
			this.labelTimeLastSynchDisplay.Size = new System.Drawing.Size(185,19);
			this.labelTimeLastSynchDisplay.TabIndex = 82;
			this.labelTimeLastSynchDisplay.Text = "0:00 a.m";
			this.labelTimeLastSynchDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormMobileSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(712,225);
			this.Controls.Add(this.labelTimeLastSynchDisplay);
			this.Controls.Add(this.butSynchNow);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.labelMinutesBetweenSynch);
			this.Controls.Add(this.labelTimeLastSynch);
			this.Controls.Add(this.butSave);
			this.Controls.Add(this.textboxWebHostAddress);
			this.Controls.Add(this.labelWebhostURL);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormMobileSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.FormMobileSetup_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private UI.Button butSave;
		private System.Windows.Forms.TextBox textboxWebHostAddress;
		private System.Windows.Forms.Label labelWebhostURL;
		private System.Windows.Forms.Label labelTimeLastSynch;
		private System.Windows.Forms.Label labelMinutesBetweenSynch;
		private System.Windows.Forms.TextBox textBox1;
		private UI.Button butSynchNow;
		private System.Windows.Forms.Timer timerRefreshLastSynchTime;
		private System.Windows.Forms.Label labelTimeLastSynchDisplay;
	}
}