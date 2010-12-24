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
			this.textboxMobileSyncServerURL = new System.Windows.Forms.TextBox();
			this.labelMobileSynchURL = new System.Windows.Forms.Label();
			this.labelMinutesBetweenSynch = new System.Windows.Forms.Label();
			this.timerRefreshLastSynchTime = new System.Windows.Forms.Timer(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this.textDateTimeLastRun = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupPreferences = new System.Windows.Forms.GroupBox();
			this.textBoxSynchMinutes = new OpenDental.ValidNumber();
			this.butSavePreferences = new OpenDental.UI.Button();
			this.textDateBefore = new OpenDental.ValidDate();
			this.butFullSync = new OpenDental.UI.Button();
			this.butSync = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.groupPreferences.SuspendLayout();
			this.SuspendLayout();
			// 
			// textboxMobileSyncServerURL
			// 
			this.textboxMobileSyncServerURL.Location = new System.Drawing.Point(177,28);
			this.textboxMobileSyncServerURL.Name = "textboxMobileSyncServerURL";
			this.textboxMobileSyncServerURL.Size = new System.Drawing.Size(445,20);
			this.textboxMobileSyncServerURL.TabIndex = 75;
			this.textboxMobileSyncServerURL.TextChanged += new System.EventHandler(this.textboxMobileSyncServerURL_TextChanged);
			// 
			// labelMobileSynchURL
			// 
			this.labelMobileSynchURL.Location = new System.Drawing.Point(6,28);
			this.labelMobileSynchURL.Name = "labelMobileSynchURL";
			this.labelMobileSynchURL.Size = new System.Drawing.Size(169,19);
			this.labelMobileSynchURL.TabIndex = 76;
			this.labelMobileSynchURL.Text = "Host Server Address";
			this.labelMobileSynchURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelMinutesBetweenSynch
			// 
			this.labelMinutesBetweenSynch.Location = new System.Drawing.Point(6,60);
			this.labelMinutesBetweenSynch.Name = "labelMinutesBetweenSynch";
			this.labelMinutesBetweenSynch.Size = new System.Drawing.Size(169,19);
			this.labelMinutesBetweenSynch.TabIndex = 79;
			this.labelMinutesBetweenSynch.Text = "Minutes between synch";
			this.labelMinutesBetweenSynch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// timerRefreshLastSynchTime
			// 
			this.timerRefreshLastSynchTime.Enabled = true;
			this.timerRefreshLastSynchTime.Interval = 10000;
			this.timerRefreshLastSynchTime.Tick += new System.EventHandler(this.timerRefreshLastSynchTime_Tick);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(26,157);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(167,18);
			this.label3.TabIndex = 87;
			this.label3.Text = "Date/time of last sync";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateTimeLastRun
			// 
			this.textDateTimeLastRun.Location = new System.Drawing.Point(195,157);
			this.textDateTimeLastRun.Name = "textDateTimeLastRun";
			this.textDateTimeLastRun.ReadOnly = true;
			this.textDateTimeLastRun.Size = new System.Drawing.Size(188,20);
			this.textDateTimeLastRun.TabIndex = 86;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(23,188);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(170,18);
			this.label2.TabIndex = 85;
			this.label2.Text = "Exclude appointments before";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupPreferences
			// 
			this.groupPreferences.Controls.Add(this.textBoxSynchMinutes);
			this.groupPreferences.Controls.Add(this.butSavePreferences);
			this.groupPreferences.Controls.Add(this.labelMobileSynchURL);
			this.groupPreferences.Controls.Add(this.textboxMobileSyncServerURL);
			this.groupPreferences.Controls.Add(this.labelMinutesBetweenSynch);
			this.groupPreferences.Location = new System.Drawing.Point(18,12);
			this.groupPreferences.Name = "groupPreferences";
			this.groupPreferences.Size = new System.Drawing.Size(753,109);
			this.groupPreferences.TabIndex = 239;
			this.groupPreferences.TabStop = false;
			this.groupPreferences.Text = "Preferences";
			// 
			// textBoxSynchMinutes
			// 
			this.textBoxSynchMinutes.Location = new System.Drawing.Point(177,59);
			this.textBoxSynchMinutes.MaxVal = 255;
			this.textBoxSynchMinutes.MinVal = 0;
			this.textBoxSynchMinutes.Name = "textBoxSynchMinutes";
			this.textBoxSynchMinutes.Size = new System.Drawing.Size(39,20);
			this.textBoxSynchMinutes.TabIndex = 241;
			this.textBoxSynchMinutes.TextChanged += new System.EventHandler(this.textBoxSynchMinutes_TextChanged);
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
			this.butSavePreferences.Location = new System.Drawing.Point(602,70);
			this.butSavePreferences.Name = "butSavePreferences";
			this.butSavePreferences.Size = new System.Drawing.Size(119,24);
			this.butSavePreferences.TabIndex = 240;
			this.butSavePreferences.Text = "Save Preferences";
			this.butSavePreferences.Click += new System.EventHandler(this.butSavePreferences_Click);
			// 
			// textDateBefore
			// 
			this.textDateBefore.Location = new System.Drawing.Point(195,187);
			this.textDateBefore.Name = "textDateBefore";
			this.textDateBefore.Size = new System.Drawing.Size(100,20);
			this.textDateBefore.TabIndex = 84;
			// 
			// butFullSync
			// 
			this.butFullSync.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFullSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butFullSync.Autosize = true;
			this.butFullSync.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFullSync.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFullSync.CornerRadius = 4F;
			this.butFullSync.Location = new System.Drawing.Point(427,265);
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
			this.butSync.Location = new System.Drawing.Point(554,265);
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
			this.butClose.Location = new System.Drawing.Point(712,265);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 81;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormMobileSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(826,320);
			this.Controls.Add(this.groupPreferences);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDateTimeLastRun);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textDateBefore);
			this.Controls.Add(this.butFullSync);
			this.Controls.Add(this.butSync);
			this.Controls.Add(this.butClose);
			this.Name = "FormMobileSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.FormMobileSetup_Load);
			this.groupPreferences.ResumeLayout(false);
			this.groupPreferences.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textboxMobileSyncServerURL;
		private System.Windows.Forms.Label labelMobileSynchURL;
		private System.Windows.Forms.Label labelMinutesBetweenSynch;
		private System.Windows.Forms.Timer timerRefreshLastSynchTime;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textDateTimeLastRun;
		private System.Windows.Forms.Label label2;
		private ValidDate textDateBefore;
		private UI.Button butFullSync;
		private UI.Button butSync;
		private UI.Button butClose;
		private System.Windows.Forms.GroupBox groupPreferences;
		private UI.Button butSavePreferences;
		private ValidNumber textBoxSynchMinutes;
	}
}