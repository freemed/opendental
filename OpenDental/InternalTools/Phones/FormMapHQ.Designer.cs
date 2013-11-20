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
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.labelTriageCoordinator = new System.Windows.Forms.Label();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fullScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupPhoneMetrics = new System.Windows.Forms.GroupBox();
			this.labelCurrentTime = new OpenDental.MapAreaRoomControl();
			this.labelTriageOpsStaff = new OpenDental.MapAreaRoomControl();
			this.labelTriageTimeSpan = new OpenDental.MapAreaRoomControl();
			this.labelTriageRedCalls = new OpenDental.MapAreaRoomControl();
			this.labelTriageRedTimeSpan = new OpenDental.MapAreaRoomControl();
			this.labelVoicemailTimeSpan = new OpenDental.MapAreaRoomControl();
			this.labelTriageCalls = new OpenDental.MapAreaRoomControl();
			this.labelVoicemailCalls = new OpenDental.MapAreaRoomControl();
			this.mapAreaPanelHQ = new OpenDental.MapAreaPanel();
			this.menuStrip.SuspendLayout();
			this.groupPhoneMetrics.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Calibri", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(4, 76);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(182, 46);
			this.label1.TabIndex = 6;
			this.label1.Text = "Triage Red";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Calibri", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(4, 175);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(170, 46);
			this.label4.TabIndex = 8;
			this.label4.Text = "Voicemail";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Calibri", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(4, 274);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(114, 46);
			this.label6.TabIndex = 10;
			this.label6.Text = "Triage";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Calibri", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(172, 16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(119, 46);
			this.label10.TabIndex = 15;
			this.label10.Text = "# Calls";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Calibri", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(343, 15);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(97, 46);
			this.label11.TabIndex = 16;
			this.label11.Text = "Time";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Calibri", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(193, 469);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(90, 46);
			this.label12.TabIndex = 19;
			this.label12.Text = "Staff";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Calibri", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(-2, 529);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(183, 46);
			this.label14.TabIndex = 17;
			this.label14.Text = "Triage Ops";
			// 
			// labelTriageCoordinator
			// 
			this.labelTriageCoordinator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelTriageCoordinator.Font = new System.Drawing.Font("Calibri", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageCoordinator.Location = new System.Drawing.Point(4, -3);
			this.labelTriageCoordinator.Name = "labelTriageCoordinator";
			this.labelTriageCoordinator.Size = new System.Drawing.Size(1875, 79);
			this.labelTriageCoordinator.TabIndex = 22;
			this.labelTriageCoordinator.Text = "Call Center Status Map - Triage Coordinator - Jim Smith";
			this.labelTriageCoordinator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// menuStrip
			// 
			this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupToolStripMenuItem,
            this.fullScreenToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(133, 24);
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
			// fullScreenToolStripMenuItem
			// 
			this.fullScreenToolStripMenuItem.Name = "fullScreenToolStripMenuItem";
			this.fullScreenToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
			this.fullScreenToolStripMenuItem.Text = "Full Screen";
			this.fullScreenToolStripMenuItem.Click += new System.EventHandler(this.fullScreenToolStripMenuItem_Click);
			// 
			// groupPhoneMetrics
			// 
			this.groupPhoneMetrics.Controls.Add(this.labelTriageTimeSpan);
			this.groupPhoneMetrics.Controls.Add(this.labelTriageRedCalls);
			this.groupPhoneMetrics.Controls.Add(this.label1);
			this.groupPhoneMetrics.Controls.Add(this.labelTriageRedTimeSpan);
			this.groupPhoneMetrics.Controls.Add(this.label4);
			this.groupPhoneMetrics.Controls.Add(this.labelVoicemailTimeSpan);
			this.groupPhoneMetrics.Controls.Add(this.label11);
			this.groupPhoneMetrics.Controls.Add(this.label6);
			this.groupPhoneMetrics.Controls.Add(this.label10);
			this.groupPhoneMetrics.Controls.Add(this.labelTriageCalls);
			this.groupPhoneMetrics.Controls.Add(this.labelVoicemailCalls);
			this.groupPhoneMetrics.Location = new System.Drawing.Point(4, 62);
			this.groupPhoneMetrics.Name = "groupPhoneMetrics";
			this.groupPhoneMetrics.Size = new System.Drawing.Size(499, 353);
			this.groupPhoneMetrics.TabIndex = 25;
			this.groupPhoneMetrics.TabStop = false;
			// 
			// labelCurrentTime
			// 
			this.labelCurrentTime.AllowDragging = false;
			this.labelCurrentTime.AllowEdit = false;
			this.labelCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelCurrentTime.BorderThickness = 2;
			this.labelCurrentTime.Elapsed = null;
			this.labelCurrentTime.EmployeeName = null;
			this.labelCurrentTime.EmployeeNum = ((long)(0));
			this.labelCurrentTime.Empty = false;
			this.labelCurrentTime.Extension = null;
			this.labelCurrentTime.Font = new System.Drawing.Font("Agency FB", 39.75F);
			this.labelCurrentTime.FontHeader = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelCurrentTime.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelCurrentTime.InnerColor = System.Drawing.SystemColors.Control;
			this.labelCurrentTime.Location = new System.Drawing.Point(1717, 6);
			this.labelCurrentTime.Name = "labelCurrentTime";
			this.labelCurrentTime.OuterColor = System.Drawing.SystemColors.Control;
			this.labelCurrentTime.PhoneImage = null;
			this.labelCurrentTime.Size = new System.Drawing.Size(182, 61);
			this.labelCurrentTime.Status = null;
			this.labelCurrentTime.TabIndex = 32;
			this.labelCurrentTime.Text = "12:45 PM";
			// 
			// labelTriageOpsStaff
			// 
			this.labelTriageOpsStaff.AllowDragging = false;
			this.labelTriageOpsStaff.AllowEdit = false;
			this.labelTriageOpsStaff.BorderThickness = 1;
			this.labelTriageOpsStaff.Elapsed = null;
			this.labelTriageOpsStaff.EmployeeName = null;
			this.labelTriageOpsStaff.EmployeeNum = ((long)(0));
			this.labelTriageOpsStaff.Empty = false;
			this.labelTriageOpsStaff.Extension = null;
			this.labelTriageOpsStaff.Font = new System.Drawing.Font("Calibri", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageOpsStaff.FontHeader = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageOpsStaff.InnerColor = System.Drawing.Color.LightCyan;
			this.labelTriageOpsStaff.Location = new System.Drawing.Point(182, 517);
			this.labelTriageOpsStaff.Name = "labelTriageOpsStaff";
			this.labelTriageOpsStaff.OuterColor = System.Drawing.Color.Blue;
			this.labelTriageOpsStaff.PhoneImage = null;
			this.labelTriageOpsStaff.Size = new System.Drawing.Size(107, 70);
			this.labelTriageOpsStaff.Status = null;
			this.labelTriageOpsStaff.TabIndex = 31;
			this.labelTriageOpsStaff.Text = "0";
			// 
			// labelTriageTimeSpan
			// 
			this.labelTriageTimeSpan.AllowDragging = false;
			this.labelTriageTimeSpan.AllowEdit = false;
			this.labelTriageTimeSpan.BorderThickness = 1;
			this.labelTriageTimeSpan.Elapsed = null;
			this.labelTriageTimeSpan.EmployeeName = null;
			this.labelTriageTimeSpan.EmployeeNum = ((long)(0));
			this.labelTriageTimeSpan.Empty = false;
			this.labelTriageTimeSpan.Extension = null;
			this.labelTriageTimeSpan.Font = new System.Drawing.Font("Calibri", 56F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageTimeSpan.FontHeader = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageTimeSpan.InnerColor = System.Drawing.Color.White;
			this.labelTriageTimeSpan.Location = new System.Drawing.Point(291, 262);
			this.labelTriageTimeSpan.Name = "labelTriageTimeSpan";
			this.labelTriageTimeSpan.OuterColor = System.Drawing.Color.Black;
			this.labelTriageTimeSpan.PhoneImage = null;
			this.labelTriageTimeSpan.Size = new System.Drawing.Size(202, 70);
			this.labelTriageTimeSpan.Status = null;
			this.labelTriageTimeSpan.TabIndex = 33;
			this.labelTriageTimeSpan.Text = "0000";
			// 
			// labelTriageRedCalls
			// 
			this.labelTriageRedCalls.AllowDragging = false;
			this.labelTriageRedCalls.AllowEdit = false;
			this.labelTriageRedCalls.BackColor = System.Drawing.Color.White;
			this.labelTriageRedCalls.BorderThickness = 1;
			this.labelTriageRedCalls.Elapsed = null;
			this.labelTriageRedCalls.EmployeeName = null;
			this.labelTriageRedCalls.EmployeeNum = ((long)(0));
			this.labelTriageRedCalls.Empty = false;
			this.labelTriageRedCalls.Extension = null;
			this.labelTriageRedCalls.Font = new System.Drawing.Font("Calibri", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageRedCalls.FontHeader = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageRedCalls.InnerColor = System.Drawing.Color.White;
			this.labelTriageRedCalls.Location = new System.Drawing.Point(178, 64);
			this.labelTriageRedCalls.Name = "labelTriageRedCalls";
			this.labelTriageRedCalls.OuterColor = System.Drawing.Color.Black;
			this.labelTriageRedCalls.PhoneImage = null;
			this.labelTriageRedCalls.Size = new System.Drawing.Size(107, 70);
			this.labelTriageRedCalls.Status = null;
			this.labelTriageRedCalls.TabIndex = 12;
			this.labelTriageRedCalls.Text = "0";
			// 
			// labelTriageRedTimeSpan
			// 
			this.labelTriageRedTimeSpan.AllowDragging = false;
			this.labelTriageRedTimeSpan.AllowEdit = false;
			this.labelTriageRedTimeSpan.BackColor = System.Drawing.Color.White;
			this.labelTriageRedTimeSpan.BorderThickness = 1;
			this.labelTriageRedTimeSpan.Elapsed = null;
			this.labelTriageRedTimeSpan.EmployeeName = null;
			this.labelTriageRedTimeSpan.EmployeeNum = ((long)(0));
			this.labelTriageRedTimeSpan.Empty = false;
			this.labelTriageRedTimeSpan.Extension = null;
			this.labelTriageRedTimeSpan.Font = new System.Drawing.Font("Calibri", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageRedTimeSpan.FontHeader = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageRedTimeSpan.InnerColor = System.Drawing.Color.White;
			this.labelTriageRedTimeSpan.Location = new System.Drawing.Point(291, 64);
			this.labelTriageRedTimeSpan.Name = "labelTriageRedTimeSpan";
			this.labelTriageRedTimeSpan.OuterColor = System.Drawing.Color.Black;
			this.labelTriageRedTimeSpan.PhoneImage = null;
			this.labelTriageRedTimeSpan.Size = new System.Drawing.Size(203, 70);
			this.labelTriageRedTimeSpan.Status = null;
			this.labelTriageRedTimeSpan.TabIndex = 7;
			this.labelTriageRedTimeSpan.Text = "00:00";
			// 
			// labelVoicemailTimeSpan
			// 
			this.labelVoicemailTimeSpan.AllowDragging = false;
			this.labelVoicemailTimeSpan.AllowEdit = false;
			this.labelVoicemailTimeSpan.BackColor = System.Drawing.Color.White;
			this.labelVoicemailTimeSpan.BorderThickness = 1;
			this.labelVoicemailTimeSpan.Elapsed = null;
			this.labelVoicemailTimeSpan.EmployeeName = null;
			this.labelVoicemailTimeSpan.EmployeeNum = ((long)(0));
			this.labelVoicemailTimeSpan.Empty = false;
			this.labelVoicemailTimeSpan.Extension = null;
			this.labelVoicemailTimeSpan.Font = new System.Drawing.Font("Calibri", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelVoicemailTimeSpan.FontHeader = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelVoicemailTimeSpan.InnerColor = System.Drawing.Color.White;
			this.labelVoicemailTimeSpan.Location = new System.Drawing.Point(291, 163);
			this.labelVoicemailTimeSpan.Name = "labelVoicemailTimeSpan";
			this.labelVoicemailTimeSpan.OuterColor = System.Drawing.Color.Black;
			this.labelVoicemailTimeSpan.PhoneImage = null;
			this.labelVoicemailTimeSpan.Size = new System.Drawing.Size(202, 70);
			this.labelVoicemailTimeSpan.Status = null;
			this.labelVoicemailTimeSpan.TabIndex = 9;
			this.labelVoicemailTimeSpan.Text = "00:00";
			// 
			// labelTriageCalls
			// 
			this.labelTriageCalls.AllowDragging = false;
			this.labelTriageCalls.AllowEdit = false;
			this.labelTriageCalls.BackColor = System.Drawing.Color.White;
			this.labelTriageCalls.BorderThickness = 1;
			this.labelTriageCalls.Elapsed = null;
			this.labelTriageCalls.EmployeeName = null;
			this.labelTriageCalls.EmployeeNum = ((long)(0));
			this.labelTriageCalls.Empty = false;
			this.labelTriageCalls.Extension = null;
			this.labelTriageCalls.Font = new System.Drawing.Font("Calibri", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageCalls.FontHeader = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriageCalls.InnerColor = System.Drawing.Color.White;
			this.labelTriageCalls.Location = new System.Drawing.Point(178, 262);
			this.labelTriageCalls.Name = "labelTriageCalls";
			this.labelTriageCalls.OuterColor = System.Drawing.Color.Black;
			this.labelTriageCalls.PhoneImage = null;
			this.labelTriageCalls.Size = new System.Drawing.Size(107, 70);
			this.labelTriageCalls.Status = null;
			this.labelTriageCalls.TabIndex = 14;
			this.labelTriageCalls.Text = "0";
			// 
			// labelVoicemailCalls
			// 
			this.labelVoicemailCalls.AllowDragging = false;
			this.labelVoicemailCalls.AllowEdit = false;
			this.labelVoicemailCalls.BackColor = System.Drawing.Color.White;
			this.labelVoicemailCalls.BorderThickness = 1;
			this.labelVoicemailCalls.Elapsed = null;
			this.labelVoicemailCalls.EmployeeName = null;
			this.labelVoicemailCalls.EmployeeNum = ((long)(0));
			this.labelVoicemailCalls.Empty = false;
			this.labelVoicemailCalls.Extension = null;
			this.labelVoicemailCalls.Font = new System.Drawing.Font("Calibri", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelVoicemailCalls.FontHeader = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelVoicemailCalls.InnerColor = System.Drawing.Color.White;
			this.labelVoicemailCalls.Location = new System.Drawing.Point(178, 163);
			this.labelVoicemailCalls.Name = "labelVoicemailCalls";
			this.labelVoicemailCalls.OuterColor = System.Drawing.Color.Black;
			this.labelVoicemailCalls.PhoneImage = null;
			this.labelVoicemailCalls.Size = new System.Drawing.Size(107, 70);
			this.labelVoicemailCalls.Status = null;
			this.labelVoicemailCalls.TabIndex = 13;
			this.labelVoicemailCalls.Text = "0";
			// 
			// mapAreaPanelHQ
			// 
			this.mapAreaPanelHQ.AllowDragging = false;
			this.mapAreaPanelHQ.AllowEditing = false;
			this.mapAreaPanelHQ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mapAreaPanelHQ.AutoScroll = true;
			this.mapAreaPanelHQ.AutoScrollMinSize = new System.Drawing.Size(1326, 935);
			this.mapAreaPanelHQ.FloorColor = System.Drawing.Color.White;
			this.mapAreaPanelHQ.FloorHeightFeet = 55;
			this.mapAreaPanelHQ.FloorWidthFeet = 78;
			this.mapAreaPanelHQ.Font = new System.Drawing.Font("Calibri", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mapAreaPanelHQ.FontCubicle = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mapAreaPanelHQ.FontCubicleHeader = new System.Drawing.Font("Calibri", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mapAreaPanelHQ.FontLabel = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mapAreaPanelHQ.GridColor = System.Drawing.Color.LightGray;
			this.mapAreaPanelHQ.Location = new System.Drawing.Point(505, 67);
			this.mapAreaPanelHQ.Name = "mapAreaPanelHQ";
			this.mapAreaPanelHQ.PixelsPerFoot = 17;
			this.mapAreaPanelHQ.ShowGrid = false;
			this.mapAreaPanelHQ.ShowOutline = true;
			this.mapAreaPanelHQ.Size = new System.Drawing.Size(1374, 973);
			this.mapAreaPanelHQ.TabIndex = 5;
			// 
			// FormMapHQ
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1884, 1042);
			this.Controls.Add(this.labelCurrentTime);
			this.Controls.Add(this.labelTriageOpsStaff);
			this.Controls.Add(this.groupPhoneMetrics);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.mapAreaPanelHQ);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.labelTriageCoordinator);
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
		private MapAreaRoomControl labelTriageRedTimeSpan;
		private MapAreaRoomControl labelVoicemailTimeSpan;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private MapAreaRoomControl labelTriageCalls;
		private MapAreaRoomControl labelVoicemailCalls;
		private MapAreaRoomControl labelTriageRedCalls;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label labelTriageCoordinator;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupPhoneMetrics;
		private System.Windows.Forms.ToolStripMenuItem fullScreenToolStripMenuItem;
		private MapAreaRoomControl labelTriageOpsStaff;
		private MapAreaRoomControl labelCurrentTime;
		private MapAreaRoomControl labelTriageTimeSpan;
	}
}