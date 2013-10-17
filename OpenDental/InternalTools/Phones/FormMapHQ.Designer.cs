namespace OpenDental {
	partial class FormMapHQ {
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
			this.label1 = new System.Windows.Forms.Label();
			this.labelTriageRedTimeSpan = new System.Windows.Forms.Label();
			this.labelVoicemailTimeSpan = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.labelTriageTimeSpan = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.labelTriageCalls = new System.Windows.Forms.Label();
			this.labelVoicemailCalls = new System.Windows.Forms.Label();
			this.labelTriageRedCalls = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.labelTriageOpsStaff = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.labelTriageCoordinator = new System.Windows.Forms.Label();
			this.butFullScreen = new System.Windows.Forms.Button();
			this.timerRefresh = new System.Windows.Forms.Timer(this.components);
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mapAreaPanelHQ = new OpenDental.MapAreaPanel();
			this.groupPhoneMetrics = new System.Windows.Forms.GroupBox();
			this.menuStrip.SuspendLayout();
			this.groupPhoneMetrics.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Calibri", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(162, 41);
			this.label1.TabIndex = 6;
			this.label1.Text = "Triage Red";
			// 
			// labelTriageRedTimeSpan
			// 
			this.labelTriageRedTimeSpan.BackColor = System.Drawing.Color.White;
			this.labelTriageRedTimeSpan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelTriageRedTimeSpan.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageRedTimeSpan.Location = new System.Drawing.Point(255, 48);
			this.labelTriageRedTimeSpan.Name = "labelTriageRedTimeSpan";
			this.labelTriageRedTimeSpan.Size = new System.Drawing.Size(127, 42);
			this.labelTriageRedTimeSpan.TabIndex = 7;
			this.labelTriageRedTimeSpan.Text = "0:00:00";
			this.labelTriageRedTimeSpan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelVoicemailTimeSpan
			// 
			this.labelVoicemailTimeSpan.BackColor = System.Drawing.Color.White;
			this.labelVoicemailTimeSpan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelVoicemailTimeSpan.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelVoicemailTimeSpan.Location = new System.Drawing.Point(255, 110);
			this.labelVoicemailTimeSpan.Name = "labelVoicemailTimeSpan";
			this.labelVoicemailTimeSpan.Size = new System.Drawing.Size(127, 42);
			this.labelVoicemailTimeSpan.TabIndex = 9;
			this.labelVoicemailTimeSpan.Text = "0:00:00";
			this.labelVoicemailTimeSpan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Calibri", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(8, 110);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(152, 41);
			this.label4.TabIndex = 8;
			this.label4.Text = "Voicemail";
			// 
			// labelTriageTimeSpan
			// 
			this.labelTriageTimeSpan.BackColor = System.Drawing.Color.White;
			this.labelTriageTimeSpan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelTriageTimeSpan.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageTimeSpan.Location = new System.Drawing.Point(255, 169);
			this.labelTriageTimeSpan.Name = "labelTriageTimeSpan";
			this.labelTriageTimeSpan.Size = new System.Drawing.Size(127, 42);
			this.labelTriageTimeSpan.TabIndex = 11;
			this.labelTriageTimeSpan.Text = "0:00:00";
			this.labelTriageTimeSpan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Calibri", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(8, 169);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(102, 41);
			this.label6.TabIndex = 10;
			this.label6.Text = "Triage";
			// 
			// labelTriageCalls
			// 
			this.labelTriageCalls.BackColor = System.Drawing.Color.White;
			this.labelTriageCalls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelTriageCalls.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageCalls.Location = new System.Drawing.Point(176, 169);
			this.labelTriageCalls.Name = "labelTriageCalls";
			this.labelTriageCalls.Size = new System.Drawing.Size(73, 42);
			this.labelTriageCalls.TabIndex = 14;
			this.labelTriageCalls.Text = "0";
			this.labelTriageCalls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelVoicemailCalls
			// 
			this.labelVoicemailCalls.BackColor = System.Drawing.Color.White;
			this.labelVoicemailCalls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelVoicemailCalls.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelVoicemailCalls.Location = new System.Drawing.Point(176, 110);
			this.labelVoicemailCalls.Name = "labelVoicemailCalls";
			this.labelVoicemailCalls.Size = new System.Drawing.Size(73, 42);
			this.labelVoicemailCalls.TabIndex = 13;
			this.labelVoicemailCalls.Text = "0";
			this.labelVoicemailCalls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTriageRedCalls
			// 
			this.labelTriageRedCalls.BackColor = System.Drawing.Color.White;
			this.labelTriageRedCalls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelTriageRedCalls.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageRedCalls.Location = new System.Drawing.Point(176, 48);
			this.labelTriageRedCalls.Name = "labelTriageRedCalls";
			this.labelTriageRedCalls.Size = new System.Drawing.Size(73, 42);
			this.labelTriageRedCalls.TabIndex = 12;
			this.labelTriageRedCalls.Text = "0";
			this.labelTriageRedCalls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(170, 15);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(84, 33);
			this.label10.TabIndex = 15;
			this.label10.Text = "# Calls";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(284, 15);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(69, 33);
			this.label11.TabIndex = 16;
			this.label11.Text = "Time";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(184, 334);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(65, 33);
			this.label12.TabIndex = 19;
			this.label12.Text = "Staff";
			// 
			// labelTriageOpsStaff
			// 
			this.labelTriageOpsStaff.BackColor = System.Drawing.Color.White;
			this.labelTriageOpsStaff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelTriageOpsStaff.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageOpsStaff.Location = new System.Drawing.Point(180, 367);
			this.labelTriageOpsStaff.Name = "labelTriageOpsStaff";
			this.labelTriageOpsStaff.Size = new System.Drawing.Size(73, 42);
			this.labelTriageOpsStaff.TabIndex = 18;
			this.labelTriageOpsStaff.Text = "0";
			this.labelTriageOpsStaff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Calibri", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(12, 367);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(164, 41);
			this.label14.TabIndex = 17;
			this.label14.Text = "Triage Ops";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Calibri", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.Location = new System.Drawing.Point(703, -2);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(286, 41);
			this.label16.TabIndex = 21;
			this.label16.Text = "Triage Coordinator:";
			// 
			// labelTriageCoordinator
			// 
			this.labelTriageCoordinator.Font = new System.Drawing.Font("Calibri", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageCoordinator.Location = new System.Drawing.Point(984, -2);
			this.labelTriageCoordinator.Name = "labelTriageCoordinator";
			this.labelTriageCoordinator.Size = new System.Drawing.Size(617, 41);
			this.labelTriageCoordinator.TabIndex = 22;
			// 
			// butFullScreen
			// 
			this.butFullScreen.Location = new System.Drawing.Point(330, 1007);
			this.butFullScreen.Name = "butFullScreen";
			this.butFullScreen.Size = new System.Drawing.Size(56, 35);
			this.butFullScreen.TabIndex = 23;
			this.butFullScreen.Text = "Full Screen";
			this.butFullScreen.UseVisualStyleBackColor = true;
			this.butFullScreen.Click += new System.EventHandler(this.butFullScreen_Click);
			// 
			// timerRefresh
			// 
			this.timerRefresh.Interval = 5000;
			this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
			// 
			// menuStrip
			// 
			this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(57, 24);
			this.menuStrip.TabIndex = 24;
			this.menuStrip.Text = "menuStrip1";
			// 
			// setupToolStripMenuItem
			// 
			this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
			this.setupToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
			this.setupToolStripMenuItem.Text = "Setup";
			this.setupToolStripMenuItem.Click += new System.EventHandler(this.setupToolStripMenuItem_Click);
			// 
			// mapAreaPanelHQ
			// 
			this.mapAreaPanelHQ.AllowDragging = false;
			this.mapAreaPanelHQ.AutoScroll = true;
			this.mapAreaPanelHQ.AutoScrollMinSize = new System.Drawing.Size(1207, 1003);
			this.mapAreaPanelHQ.FloorColor = System.Drawing.Color.White;
			this.mapAreaPanelHQ.FloorHeightFeet = 59;
			this.mapAreaPanelHQ.FloorWidthFeet = 71;
			this.mapAreaPanelHQ.Font = new System.Drawing.Font("Calibri", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mapAreaPanelHQ.FontCubicle = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mapAreaPanelHQ.FontLabel = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mapAreaPanelHQ.GridColor = System.Drawing.Color.LightGray;
			this.mapAreaPanelHQ.Location = new System.Drawing.Point(392, 37);
			this.mapAreaPanelHQ.Name = "mapAreaPanelHQ";
			this.mapAreaPanelHQ.PixelsPerFoot = 17;
			this.mapAreaPanelHQ.ShowGrid = false;
			this.mapAreaPanelHQ.ShowOutline = true;
			this.mapAreaPanelHQ.Size = new System.Drawing.Size(1212, 1008);
			this.mapAreaPanelHQ.TabIndex = 5;
			// 
			// groupPhoneMetrics
			// 
			this.groupPhoneMetrics.Controls.Add(this.labelTriageRedCalls);
			this.groupPhoneMetrics.Controls.Add(this.label1);
			this.groupPhoneMetrics.Controls.Add(this.labelTriageRedTimeSpan);
			this.groupPhoneMetrics.Controls.Add(this.label4);
			this.groupPhoneMetrics.Controls.Add(this.labelVoicemailTimeSpan);
			this.groupPhoneMetrics.Controls.Add(this.label11);
			this.groupPhoneMetrics.Controls.Add(this.label6);
			this.groupPhoneMetrics.Controls.Add(this.label10);
			this.groupPhoneMetrics.Controls.Add(this.labelTriageTimeSpan);
			this.groupPhoneMetrics.Controls.Add(this.labelTriageCalls);
			this.groupPhoneMetrics.Controls.Add(this.labelVoicemailCalls);
			this.groupPhoneMetrics.Location = new System.Drawing.Point(2, 37);
			this.groupPhoneMetrics.Name = "groupPhoneMetrics";
			this.groupPhoneMetrics.Size = new System.Drawing.Size(386, 218);
			this.groupPhoneMetrics.TabIndex = 25;
			this.groupPhoneMetrics.TabStop = false;
			this.groupPhoneMetrics.Visible = false;
			// 
			// FormMapHQ
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1634, 1042);
			this.Controls.Add(this.groupPhoneMetrics);
			this.Controls.Add(this.butFullScreen);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.labelTriageOpsStaff);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.mapAreaPanelHQ);
			this.Controls.Add(this.labelTriageCoordinator);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.menuStrip);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "FormMapHQ";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HQ";
			this.Load += new System.EventHandler(this.FormMapHQ_Load);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.groupPhoneMetrics.ResumeLayout(false);
			this.groupPhoneMetrics.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MapAreaPanel mapAreaPanelHQ;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelTriageRedTimeSpan;
		private System.Windows.Forms.Label labelVoicemailTimeSpan;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelTriageTimeSpan;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label labelTriageCalls;
		private System.Windows.Forms.Label labelVoicemailCalls;
		private System.Windows.Forms.Label labelTriageRedCalls;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label labelTriageOpsStaff;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label labelTriageCoordinator;
		private System.Windows.Forms.Button butFullScreen;
		private System.Windows.Forms.Timer timerRefresh;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupPhoneMetrics;
	}
}