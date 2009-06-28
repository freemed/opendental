namespace OpenDental{
	partial class FormBackupReminder {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBackupReminder));
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.checkNoBackups = new System.Windows.Forms.CheckBox();
			this.checkA1 = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.checkA2 = new System.Windows.Forms.CheckBox();
			this.checkA4 = new System.Windows.Forms.CheckBox();
			this.checkA3 = new System.Windows.Forms.CheckBox();
			this.checkB2 = new System.Windows.Forms.CheckBox();
			this.checkB1 = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkNoProof = new System.Windows.Forms.CheckBox();
			this.checkNoStrategy = new System.Windows.Forms.CheckBox();
			this.checkC2 = new System.Windows.Forms.CheckBox();
			this.checkC1 = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
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
			this.butOK.Location = new System.Drawing.Point(525,406);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(552,74);
			this.label1.TabIndex = 4;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// checkNoBackups
			// 
			this.checkNoBackups.Location = new System.Drawing.Point(45,191);
			this.checkNoBackups.Name = "checkNoBackups";
			this.checkNoBackups.Size = new System.Drawing.Size(151,20);
			this.checkNoBackups.TabIndex = 6;
			this.checkNoBackups.Text = "No backups";
			this.checkNoBackups.UseVisualStyleBackColor = true;
			// 
			// checkA1
			// 
			this.checkA1.Location = new System.Drawing.Point(45,111);
			this.checkA1.Name = "checkA1";
			this.checkA1.Size = new System.Drawing.Size(200,20);
			this.checkA1.TabIndex = 8;
			this.checkA1.Text = "Online";
			this.checkA1.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(42,95);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(523,18);
			this.label3.TabIndex = 7;
			this.label3.Text = "Do you make backups every single day?  Backup method:";
			// 
			// checkA2
			// 
			this.checkA2.Location = new System.Drawing.Point(45,131);
			this.checkA2.Name = "checkA2";
			this.checkA2.Size = new System.Drawing.Size(530,20);
			this.checkA2.TabIndex = 9;
			this.checkA2.Text = "Removable (external HD, USB drive, etc)";
			this.checkA2.UseVisualStyleBackColor = true;
			// 
			// checkA4
			// 
			this.checkA4.Location = new System.Drawing.Point(45,171);
			this.checkA4.Name = "checkA4";
			this.checkA4.Size = new System.Drawing.Size(151,20);
			this.checkA4.TabIndex = 11;
			this.checkA4.Text = "Other backup method";
			this.checkA4.UseVisualStyleBackColor = true;
			// 
			// checkA3
			// 
			this.checkA3.Location = new System.Drawing.Point(45,151);
			this.checkA3.Name = "checkA3";
			this.checkA3.Size = new System.Drawing.Size(302,20);
			this.checkA3.TabIndex = 10;
			this.checkA3.Text = "Network (to another computer in your office)";
			this.checkA3.UseVisualStyleBackColor = true;
			// 
			// checkB2
			// 
			this.checkB2.Location = new System.Drawing.Point(45,261);
			this.checkB2.Name = "checkB2";
			this.checkB2.Size = new System.Drawing.Size(250,20);
			this.checkB2.TabIndex = 14;
			this.checkB2.Text = "Run backup from a second server";
			this.checkB2.UseVisualStyleBackColor = true;
			// 
			// checkB1
			// 
			this.checkB1.Location = new System.Drawing.Point(45,241);
			this.checkB1.Name = "checkB1";
			this.checkB1.Size = new System.Drawing.Size(352,20);
			this.checkB1.TabIndex = 13;
			this.checkB1.Text = "Restore to home computer at least once a week";
			this.checkB1.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(42,225);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(523,18);
			this.label2.TabIndex = 12;
			this.label2.Text = "What proof do you have that your recent backups are good?";
			// 
			// checkNoProof
			// 
			this.checkNoProof.Location = new System.Drawing.Point(45,281);
			this.checkNoProof.Name = "checkNoProof";
			this.checkNoProof.Size = new System.Drawing.Size(250,20);
			this.checkNoProof.TabIndex = 15;
			this.checkNoProof.Text = "No proof";
			this.checkNoProof.UseVisualStyleBackColor = true;
			// 
			// checkNoStrategy
			// 
			this.checkNoStrategy.Location = new System.Drawing.Point(45,369);
			this.checkNoStrategy.Name = "checkNoStrategy";
			this.checkNoStrategy.Size = new System.Drawing.Size(250,20);
			this.checkNoStrategy.TabIndex = 19;
			this.checkNoStrategy.Text = "No strategy";
			this.checkNoStrategy.UseVisualStyleBackColor = true;
			// 
			// checkC2
			// 
			this.checkC2.Location = new System.Drawing.Point(45,349);
			this.checkC2.Name = "checkC2";
			this.checkC2.Size = new System.Drawing.Size(312,20);
			this.checkC2.TabIndex = 18;
			this.checkC2.Text = "Saved hardcopy paper reports";
			this.checkC2.UseVisualStyleBackColor = true;
			// 
			// checkC1
			// 
			this.checkC1.Location = new System.Drawing.Point(45,329);
			this.checkC1.Name = "checkC1";
			this.checkC1.Size = new System.Drawing.Size(352,20);
			this.checkC1.TabIndex = 17;
			this.checkC1.Text = "Completely separate archives stored offsite (DVD, HD, etc)";
			this.checkC1.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(42,313);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(523,18);
			this.label4.TabIndex = 16;
			this.label4.Text = "What secondary long-term mechanism do you use to ensure minimal data loss?";
			// 
			// FormBackupReminder
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(612,442);
			this.ControlBox = false;
			this.Controls.Add(this.checkNoStrategy);
			this.Controls.Add(this.checkC2);
			this.Controls.Add(this.checkC1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.checkNoProof);
			this.Controls.Add(this.checkB2);
			this.Controls.Add(this.checkB1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkA4);
			this.Controls.Add(this.checkA3);
			this.Controls.Add(this.checkA2);
			this.Controls.Add(this.checkA1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkNoBackups);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Name = "FormBackupReminder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Backup Reminder";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBackupReminder_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkNoBackups;
		private System.Windows.Forms.CheckBox checkA1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkA2;
		private System.Windows.Forms.CheckBox checkA4;
		private System.Windows.Forms.CheckBox checkA3;
		private System.Windows.Forms.CheckBox checkB2;
		private System.Windows.Forms.CheckBox checkB1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkNoProof;
		private System.Windows.Forms.CheckBox checkNoStrategy;
		private System.Windows.Forms.CheckBox checkC2;
		private System.Windows.Forms.CheckBox checkC1;
		private System.Windows.Forms.Label label4;
	}
}