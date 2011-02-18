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
			this.textMobileSyncServerURL = new System.Windows.Forms.TextBox();
			this.labelMobileSynchURL = new System.Windows.Forms.Label();
			this.labelMinutesBetweenSynch = new System.Windows.Forms.Label();
			this.timerRefreshLastSynchTime = new System.Windows.Forms.Timer(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupPreferences = new System.Windows.Forms.GroupBox();
			this.butCurrentWorkstation = new OpenDental.UI.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.textMobileSynchWorkStation = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textMobilePassword = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textMobileUserName = new System.Windows.Forms.TextBox();
			this.textSynchMinutes = new OpenDental.ValidNumber();
			this.butSavePreferences = new OpenDental.UI.Button();
			this.textDateBefore = new OpenDental.ValidDate();
			this.label1 = new System.Windows.Forms.Label();
			this.textProgress = new System.Windows.Forms.Label();
			this.textDateTimeLastRun = new System.Windows.Forms.Label();
			this.butFullSync = new OpenDental.UI.Button();
			this.butSync = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.groupPreferences.SuspendLayout();
			this.SuspendLayout();
			// 
			// textMobileSyncServerURL
			// 
			this.textMobileSyncServerURL.Location = new System.Drawing.Point(177,28);
			this.textMobileSyncServerURL.Name = "textMobileSyncServerURL";
			this.textMobileSyncServerURL.Size = new System.Drawing.Size(445,20);
			this.textMobileSyncServerURL.TabIndex = 75;
			this.textMobileSyncServerURL.TextChanged += new System.EventHandler(this.textMobileSyncServerURL_TextChanged);
			// 
			// labelMobileSynchURL
			// 
			this.labelMobileSynchURL.Location = new System.Drawing.Point(6,29);
			this.labelMobileSynchURL.Name = "labelMobileSynchURL";
			this.labelMobileSynchURL.Size = new System.Drawing.Size(169,19);
			this.labelMobileSynchURL.TabIndex = 76;
			this.labelMobileSynchURL.Text = "Host Server Address";
			this.labelMobileSynchURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelMinutesBetweenSynch
			// 
			this.labelMinutesBetweenSynch.Location = new System.Drawing.Point(6,57);
			this.labelMinutesBetweenSynch.Name = "labelMinutesBetweenSynch";
			this.labelMinutesBetweenSynch.Size = new System.Drawing.Size(169,19);
			this.labelMinutesBetweenSynch.TabIndex = 79;
			this.labelMinutesBetweenSynch.Text = "Minutes Between Synch";
			this.labelMinutesBetweenSynch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// timerRefreshLastSynchTime
			// 
			this.timerRefreshLastSynchTime.Enabled = true;
			this.timerRefreshLastSynchTime.Interval = 60000;
			this.timerRefreshLastSynchTime.Tick += new System.EventHandler(this.timerRefreshLastSynchTime_Tick);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(25,273);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(167,18);
			this.label3.TabIndex = 87;
			this.label3.Text = "Date/time of last sync";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(5,139);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(170,18);
			this.label2.TabIndex = 85;
			this.label2.Text = "Exclude appointments before";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupPreferences
			// 
			this.groupPreferences.Controls.Add(this.label7);
			this.groupPreferences.Controls.Add(this.butCurrentWorkstation);
			this.groupPreferences.Controls.Add(this.label6);
			this.groupPreferences.Controls.Add(this.textMobileSynchWorkStation);
			this.groupPreferences.Controls.Add(this.label5);
			this.groupPreferences.Controls.Add(this.textMobilePassword);
			this.groupPreferences.Controls.Add(this.label4);
			this.groupPreferences.Controls.Add(this.textMobileUserName);
			this.groupPreferences.Controls.Add(this.textSynchMinutes);
			this.groupPreferences.Controls.Add(this.label2);
			this.groupPreferences.Controls.Add(this.butSavePreferences);
			this.groupPreferences.Controls.Add(this.textDateBefore);
			this.groupPreferences.Controls.Add(this.labelMobileSynchURL);
			this.groupPreferences.Controls.Add(this.textMobileSyncServerURL);
			this.groupPreferences.Controls.Add(this.labelMinutesBetweenSynch);
			this.groupPreferences.Location = new System.Drawing.Point(18,12);
			this.groupPreferences.Name = "groupPreferences";
			this.groupPreferences.Size = new System.Drawing.Size(796,203);
			this.groupPreferences.TabIndex = 239;
			this.groupPreferences.TabStop = false;
			this.groupPreferences.Text = "Preferences";
			// 
			// butCurrentWorkstation
			// 
			this.butCurrentWorkstation.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCurrentWorkstation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCurrentWorkstation.Autosize = true;
			this.butCurrentWorkstation.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCurrentWorkstation.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCurrentWorkstation.CornerRadius = 4F;
			this.butCurrentWorkstation.Location = new System.Drawing.Point(458,167);
			this.butCurrentWorkstation.Name = "butCurrentWorkstation";
			this.butCurrentWorkstation.Size = new System.Drawing.Size(146,24);
			this.butCurrentWorkstation.TabIndex = 247;
			this.butCurrentWorkstation.Text = "Current Workstation";
			this.butCurrentWorkstation.Click += new System.EventHandler(this.butCurrentWorkstation_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(1,170);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(170,18);
			this.label6.TabIndex = 246;
			this.label6.Text = "Work Station for Synching";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMobileSynchWorkStation
			// 
			this.textMobileSynchWorkStation.Location = new System.Drawing.Point(177,168);
			this.textMobileSynchWorkStation.Name = "textMobileSynchWorkStation";
			this.textMobileSynchWorkStation.Size = new System.Drawing.Size(247,20);
			this.textMobileSynchWorkStation.TabIndex = 245;
			this.textMobileSynchWorkStation.TextChanged += new System.EventHandler(this.textMobileSynchWorkStation_TextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(5,113);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(169,19);
			this.label5.TabIndex = 244;
			this.label5.Text = "Mobile Phone Password";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMobilePassword
			// 
			this.textMobilePassword.Location = new System.Drawing.Point(177,112);
			this.textMobilePassword.Name = "textMobilePassword";
			this.textMobilePassword.PasswordChar = '*';
			this.textMobilePassword.Size = new System.Drawing.Size(247,20);
			this.textMobilePassword.TabIndex = 243;
			this.textMobilePassword.TextChanged += new System.EventHandler(this.textMobilePassword_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(5,85);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(169,19);
			this.label4.TabIndex = 243;
			this.label4.Text = "Mobile Phone User Name";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMobileUserName
			// 
			this.textMobileUserName.Location = new System.Drawing.Point(177,84);
			this.textMobileUserName.Name = "textMobileUserName";
			this.textMobileUserName.Size = new System.Drawing.Size(247,20);
			this.textMobileUserName.TabIndex = 242;
			this.textMobileUserName.TextChanged += new System.EventHandler(this.textMobileUserName_TextChanged);
			// 
			// textSynchMinutes
			// 
			this.textSynchMinutes.Location = new System.Drawing.Point(177,56);
			this.textSynchMinutes.MaxVal = 255;
			this.textSynchMinutes.MinVal = 0;
			this.textSynchMinutes.Name = "textSynchMinutes";
			this.textSynchMinutes.Size = new System.Drawing.Size(39,20);
			this.textSynchMinutes.TabIndex = 241;
			this.textSynchMinutes.TextChanged += new System.EventHandler(this.textSynchMinutes_TextChanged);
			// 
			// butSavePreferences
			// 
			this.butSavePreferences.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSavePreferences.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSavePreferences.Autosize = true;
			this.butSavePreferences.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSavePreferences.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSavePreferences.CornerRadius = 4F;
			this.butSavePreferences.Enabled = false;
			this.butSavePreferences.Location = new System.Drawing.Point(650,168);
			this.butSavePreferences.Name = "butSavePreferences";
			this.butSavePreferences.Size = new System.Drawing.Size(119,24);
			this.butSavePreferences.TabIndex = 240;
			this.butSavePreferences.Text = "Save Preferences";
			this.butSavePreferences.Click += new System.EventHandler(this.butSavePreferences_Click);
			// 
			// textDateBefore
			// 
			this.textDateBefore.Location = new System.Drawing.Point(177,140);
			this.textDateBefore.Name = "textDateBefore";
			this.textDateBefore.Size = new System.Drawing.Size(100,20);
			this.textDateBefore.TabIndex = 84;
			this.textDateBefore.TextChanged += new System.EventHandler(this.textDateBefore_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(26,245);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(167,18);
			this.label1.TabIndex = 241;
			this.label1.Text = "Upload Status";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textProgress
			// 
			this.textProgress.Location = new System.Drawing.Point(196,245);
			this.textProgress.Name = "textProgress";
			this.textProgress.Size = new System.Drawing.Size(464,18);
			this.textProgress.TabIndex = 242;
			this.textProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textDateTimeLastRun
			// 
			this.textDateTimeLastRun.Location = new System.Drawing.Point(196,273);
			this.textDateTimeLastRun.Name = "textDateTimeLastRun";
			this.textDateTimeLastRun.Size = new System.Drawing.Size(207,18);
			this.textDateTimeLastRun.TabIndex = 243;
			this.textDateTimeLastRun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butFullSync
			// 
			this.butFullSync.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFullSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butFullSync.Autosize = true;
			this.butFullSync.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFullSync.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFullSync.CornerRadius = 4F;
			this.butFullSync.Location = new System.Drawing.Point(427,397);
			this.butFullSync.Name = "butFullSync";
			this.butFullSync.Size = new System.Drawing.Size(68,24);
			this.butFullSync.TabIndex = 83;
			this.butFullSync.Text = "Full Sync";
			this.butFullSync.Click += new System.EventHandler(this.butFullSync_Click);
			// 
			// butSync
			// 
			this.butSync.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSync.Autosize = true;
			this.butSync.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSync.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSync.CornerRadius = 4F;
			this.butSync.Location = new System.Drawing.Point(554,397);
			this.butSync.Name = "butSync";
			this.butSync.Size = new System.Drawing.Size(68,24);
			this.butSync.TabIndex = 82;
			this.butSync.Text = "Sync";
			this.butSync.Click += new System.EventHandler(this.butSync_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(712,397);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 81;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(222,57);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(255,18);
			this.label7.TabIndex = 244;
			this.label7.Text = "Make this figure 0 to stop automatic Synchronization";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormMobileSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(826,452);
			this.Controls.Add(this.textDateTimeLastRun);
			this.Controls.Add(this.textProgress);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupPreferences);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butFullSync);
			this.Controls.Add(this.butSync);
			this.Controls.Add(this.butClose);
			this.Name = "FormMobileSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.FormMobileSetup_Load);
			this.groupPreferences.ResumeLayout(false);
			this.groupPreferences.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox textMobileSyncServerURL;
		private System.Windows.Forms.Label labelMobileSynchURL;
		private System.Windows.Forms.Label labelMinutesBetweenSynch;
		private System.Windows.Forms.Timer timerRefreshLastSynchTime;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private ValidDate textDateBefore;
		private UI.Button butFullSync;
		private UI.Button butSync;
		private UI.Button butClose;
		private System.Windows.Forms.GroupBox groupPreferences;
		private UI.Button butSavePreferences;
		private ValidNumber textSynchMinutes;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textMobileUserName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textMobilePassword;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textMobileSynchWorkStation;
		private UI.Button butCurrentWorkstation;
		private System.Windows.Forms.Label textProgress;
		private System.Windows.Forms.Label textDateTimeLastRun;
		private System.Windows.Forms.Label label7;
	}
}