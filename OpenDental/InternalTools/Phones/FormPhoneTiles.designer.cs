namespace OpenDental {
	partial class FormPhoneTiles {
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
			this.timerMain = new System.Windows.Forms.Timer(this.components);
			this.labelMsg = new System.Windows.Forms.Label();
			this.timerMsgs = new System.Windows.Forms.Timer(this.components);
			this.menuNumbers = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemManage = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStatus = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemAvailable = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemTraining = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemTeamAssist = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemWrapUp = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemOfflineAssist = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemUnavailable = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemRinggroupAll = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemRinggroupNone = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemRinggroupsDefault = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemBackup = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemLunch = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemHome = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemBreak = new System.Windows.Forms.ToolStripMenuItem();
			this.butOverride = new OpenDental.UI.Button();
			this.phoneTile21 = new OpenDental.PhoneTile();
			this.phoneTile14 = new OpenDental.PhoneTile();
			this.phoneTile7 = new OpenDental.PhoneTile();
			this.phoneTile20 = new OpenDental.PhoneTile();
			this.phoneTile13 = new OpenDental.PhoneTile();
			this.phoneTile6 = new OpenDental.PhoneTile();
			this.phoneTile19 = new OpenDental.PhoneTile();
			this.phoneTile12 = new OpenDental.PhoneTile();
			this.phoneTile5 = new OpenDental.PhoneTile();
			this.phoneTile18 = new OpenDental.PhoneTile();
			this.phoneTile17 = new OpenDental.PhoneTile();
			this.phoneTile16 = new OpenDental.PhoneTile();
			this.phoneTile15 = new OpenDental.PhoneTile();
			this.phoneTile11 = new OpenDental.PhoneTile();
			this.phoneTile10 = new OpenDental.PhoneTile();
			this.phoneTile9 = new OpenDental.PhoneTile();
			this.phoneTile8 = new OpenDental.PhoneTile();
			this.phoneTile4 = new OpenDental.PhoneTile();
			this.phoneTile3 = new OpenDental.PhoneTile();
			this.phoneTile2 = new OpenDental.PhoneTile();
			this.phoneTile1 = new OpenDental.PhoneTile();
			this.menuNumbers.SuspendLayout();
			this.menuStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// timerMain
			// 
			this.timerMain.Interval = 1600;
			this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
			// 
			// labelMsg
			// 
			this.labelMsg.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelMsg.ForeColor = System.Drawing.Color.Firebrick;
			this.labelMsg.Location = new System.Drawing.Point(102,2);
			this.labelMsg.Name = "labelMsg";
			this.labelMsg.Size = new System.Drawing.Size(198,20);
			this.labelMsg.TabIndex = 27;
			this.labelMsg.Text = "Phone Messages: 0";
			this.labelMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// timerMsgs
			// 
			this.timerMsgs.Interval = 3000;
			this.timerMsgs.Tick += new System.EventHandler(this.timerMsgs_Tick);
			// 
			// menuNumbers
			// 
			this.menuNumbers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemManage,
            this.menuItemAdd});
			this.menuNumbers.Name = "contextMenuStrip1";
			this.menuNumbers.Size = new System.Drawing.Size(291,48);
			// 
			// menuItemManage
			// 
			this.menuItemManage.Name = "menuItemManage";
			this.menuItemManage.Size = new System.Drawing.Size(290,22);
			this.menuItemManage.Text = "Manage Phone Numbers";
			this.menuItemManage.Click += new System.EventHandler(this.menuItemManage_Click);
			// 
			// menuItemAdd
			// 
			this.menuItemAdd.Name = "menuItemAdd";
			this.menuItemAdd.Size = new System.Drawing.Size(290,22);
			this.menuItemAdd.Text = "Attach Phone Number to Current Patient";
			this.menuItemAdd.Click += new System.EventHandler(this.menuItemAdd_Click);
			// 
			// menuStatus
			// 
			this.menuStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAvailable,
            this.menuItemTraining,
            this.menuItemTeamAssist,
            this.menuItemWrapUp,
            this.menuItemOfflineAssist,
            this.menuItemUnavailable,
            this.toolStripMenuItem2,
            this.menuItemRinggroupAll,
            this.menuItemRinggroupNone,
            this.menuItemRinggroupsDefault,
            this.menuItemBackup,
            this.toolStripMenuItem1,
            this.menuItemLunch,
            this.menuItemHome,
            this.menuItemBreak});
			this.menuStatus.Name = "menuStatus";
			this.menuStatus.Size = new System.Drawing.Size(177,302);
			// 
			// menuItemAvailable
			// 
			this.menuItemAvailable.Name = "menuItemAvailable";
			this.menuItemAvailable.Size = new System.Drawing.Size(176,22);
			this.menuItemAvailable.Text = "Available";
			this.menuItemAvailable.Click += new System.EventHandler(this.menuItemAvailable_Click);
			// 
			// menuItemTraining
			// 
			this.menuItemTraining.Name = "menuItemTraining";
			this.menuItemTraining.Size = new System.Drawing.Size(176,22);
			this.menuItemTraining.Text = "Training";
			this.menuItemTraining.Click += new System.EventHandler(this.menuItemTraining_Click);
			// 
			// menuItemTeamAssist
			// 
			this.menuItemTeamAssist.Name = "menuItemTeamAssist";
			this.menuItemTeamAssist.Size = new System.Drawing.Size(176,22);
			this.menuItemTeamAssist.Text = "TeamAssist";
			this.menuItemTeamAssist.Click += new System.EventHandler(this.menuItemTeamAssist_Click);
			// 
			// menuItemWrapUp
			// 
			this.menuItemWrapUp.Name = "menuItemWrapUp";
			this.menuItemWrapUp.Size = new System.Drawing.Size(176,22);
			this.menuItemWrapUp.Text = "WrapUp";
			this.menuItemWrapUp.Click += new System.EventHandler(this.menuItemWrapUp_Click);
			// 
			// menuItemOfflineAssist
			// 
			this.menuItemOfflineAssist.Name = "menuItemOfflineAssist";
			this.menuItemOfflineAssist.Size = new System.Drawing.Size(176,22);
			this.menuItemOfflineAssist.Text = "OfflineAssist";
			this.menuItemOfflineAssist.Click += new System.EventHandler(this.menuItemOfflineAssist_Click);
			// 
			// menuItemUnavailable
			// 
			this.menuItemUnavailable.Name = "menuItemUnavailable";
			this.menuItemUnavailable.Size = new System.Drawing.Size(176,22);
			this.menuItemUnavailable.Text = "Unavailable";
			this.menuItemUnavailable.Click += new System.EventHandler(this.menuItemUnavailable_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(173,6);
			// 
			// menuItemRinggroupAll
			// 
			this.menuItemRinggroupAll.Name = "menuItemRinggroupAll";
			this.menuItemRinggroupAll.Size = new System.Drawing.Size(176,22);
			this.menuItemRinggroupAll.Text = "Ringgroups All";
			this.menuItemRinggroupAll.Click += new System.EventHandler(this.menuItemRinggroupAll_Click);
			// 
			// menuItemRinggroupNone
			// 
			this.menuItemRinggroupNone.Name = "menuItemRinggroupNone";
			this.menuItemRinggroupNone.Size = new System.Drawing.Size(176,22);
			this.menuItemRinggroupNone.Text = "Ringgroups None";
			this.menuItemRinggroupNone.Click += new System.EventHandler(this.menuItemRinggroupNone_Click);
			// 
			// menuItemRinggroupsDefault
			// 
			this.menuItemRinggroupsDefault.Name = "menuItemRinggroupsDefault";
			this.menuItemRinggroupsDefault.Size = new System.Drawing.Size(176,22);
			this.menuItemRinggroupsDefault.Text = "Ringgroups Default";
			this.menuItemRinggroupsDefault.Click += new System.EventHandler(this.menuItemRinggroupsDefault_Click);
			// 
			// menuItemBackup
			// 
			this.menuItemBackup.Name = "menuItemBackup";
			this.menuItemBackup.Size = new System.Drawing.Size(176,22);
			this.menuItemBackup.Text = "Backup";
			this.menuItemBackup.Click += new System.EventHandler(this.menuItemBackup_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(173,6);
			// 
			// menuItemLunch
			// 
			this.menuItemLunch.Name = "menuItemLunch";
			this.menuItemLunch.Size = new System.Drawing.Size(176,22);
			this.menuItemLunch.Text = "Lunch";
			this.menuItemLunch.Click += new System.EventHandler(this.menuItemLunch_Click);
			// 
			// menuItemHome
			// 
			this.menuItemHome.Name = "menuItemHome";
			this.menuItemHome.Size = new System.Drawing.Size(176,22);
			this.menuItemHome.Text = "Home";
			this.menuItemHome.Click += new System.EventHandler(this.menuItemHome_Click);
			// 
			// menuItemBreak
			// 
			this.menuItemBreak.Name = "menuItemBreak";
			this.menuItemBreak.Size = new System.Drawing.Size(176,22);
			this.menuItemBreak.Text = "Break";
			this.menuItemBreak.Click += new System.EventHandler(this.menuItemBreak_Click);
			// 
			// butOverride
			// 
			this.butOverride.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOverride.Autosize = true;
			this.butOverride.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOverride.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOverride.CornerRadius = 4F;
			this.butOverride.Location = new System.Drawing.Point(0,0);
			this.butOverride.Name = "butOverride";
			this.butOverride.Size = new System.Drawing.Size(75,24);
			this.butOverride.TabIndex = 26;
			this.butOverride.Text = "Override";
			this.butOverride.Click += new System.EventHandler(this.butOverride_Click);
			// 
			// phoneTile21
			// 
			this.phoneTile21.Location = new System.Drawing.Point(900,403);
			this.phoneTile21.Name = "phoneTile21";
			this.phoneTile21.PhoneCur = null;
			this.phoneTile21.Size = new System.Drawing.Size(150,181);
			this.phoneTile21.TabIndex = 20;
			// 
			// phoneTile14
			// 
			this.phoneTile14.Location = new System.Drawing.Point(900,216);
			this.phoneTile14.Name = "phoneTile14";
			this.phoneTile14.PhoneCur = null;
			this.phoneTile14.Size = new System.Drawing.Size(150,181);
			this.phoneTile14.TabIndex = 19;
			// 
			// phoneTile7
			// 
			this.phoneTile7.Location = new System.Drawing.Point(900,29);
			this.phoneTile7.Name = "phoneTile7";
			this.phoneTile7.PhoneCur = null;
			this.phoneTile7.Size = new System.Drawing.Size(150,181);
			this.phoneTile7.TabIndex = 18;
			// 
			// phoneTile20
			// 
			this.phoneTile20.Location = new System.Drawing.Point(750,403);
			this.phoneTile20.Name = "phoneTile20";
			this.phoneTile20.PhoneCur = null;
			this.phoneTile20.Size = new System.Drawing.Size(150,181);
			this.phoneTile20.TabIndex = 17;
			// 
			// phoneTile13
			// 
			this.phoneTile13.Location = new System.Drawing.Point(750,216);
			this.phoneTile13.Name = "phoneTile13";
			this.phoneTile13.PhoneCur = null;
			this.phoneTile13.Size = new System.Drawing.Size(150,181);
			this.phoneTile13.TabIndex = 16;
			// 
			// phoneTile6
			// 
			this.phoneTile6.Location = new System.Drawing.Point(750,29);
			this.phoneTile6.Name = "phoneTile6";
			this.phoneTile6.PhoneCur = null;
			this.phoneTile6.Size = new System.Drawing.Size(150,181);
			this.phoneTile6.TabIndex = 15;
			// 
			// phoneTile19
			// 
			this.phoneTile19.Location = new System.Drawing.Point(600,403);
			this.phoneTile19.Name = "phoneTile19";
			this.phoneTile19.PhoneCur = null;
			this.phoneTile19.Size = new System.Drawing.Size(150,181);
			this.phoneTile19.TabIndex = 14;
			// 
			// phoneTile12
			// 
			this.phoneTile12.Location = new System.Drawing.Point(600,216);
			this.phoneTile12.Name = "phoneTile12";
			this.phoneTile12.PhoneCur = null;
			this.phoneTile12.Size = new System.Drawing.Size(150,181);
			this.phoneTile12.TabIndex = 13;
			// 
			// phoneTile5
			// 
			this.phoneTile5.Location = new System.Drawing.Point(600,29);
			this.phoneTile5.Name = "phoneTile5";
			this.phoneTile5.PhoneCur = null;
			this.phoneTile5.Size = new System.Drawing.Size(150,181);
			this.phoneTile5.TabIndex = 12;
			// 
			// phoneTile18
			// 
			this.phoneTile18.Location = new System.Drawing.Point(450,403);
			this.phoneTile18.Name = "phoneTile18";
			this.phoneTile18.PhoneCur = null;
			this.phoneTile18.Size = new System.Drawing.Size(150,181);
			this.phoneTile18.TabIndex = 11;
			// 
			// phoneTile17
			// 
			this.phoneTile17.Location = new System.Drawing.Point(300,403);
			this.phoneTile17.Name = "phoneTile17";
			this.phoneTile17.PhoneCur = null;
			this.phoneTile17.Size = new System.Drawing.Size(150,181);
			this.phoneTile17.TabIndex = 10;
			// 
			// phoneTile16
			// 
			this.phoneTile16.Location = new System.Drawing.Point(150,403);
			this.phoneTile16.Name = "phoneTile16";
			this.phoneTile16.PhoneCur = null;
			this.phoneTile16.Size = new System.Drawing.Size(150,181);
			this.phoneTile16.TabIndex = 9;
			// 
			// phoneTile15
			// 
			this.phoneTile15.Location = new System.Drawing.Point(0,403);
			this.phoneTile15.Name = "phoneTile15";
			this.phoneTile15.PhoneCur = null;
			this.phoneTile15.Size = new System.Drawing.Size(150,181);
			this.phoneTile15.TabIndex = 8;
			// 
			// phoneTile11
			// 
			this.phoneTile11.Location = new System.Drawing.Point(450,216);
			this.phoneTile11.Name = "phoneTile11";
			this.phoneTile11.PhoneCur = null;
			this.phoneTile11.Size = new System.Drawing.Size(150,181);
			this.phoneTile11.TabIndex = 7;
			// 
			// phoneTile10
			// 
			this.phoneTile10.Location = new System.Drawing.Point(300,216);
			this.phoneTile10.Name = "phoneTile10";
			this.phoneTile10.PhoneCur = null;
			this.phoneTile10.Size = new System.Drawing.Size(150,181);
			this.phoneTile10.TabIndex = 6;
			// 
			// phoneTile9
			// 
			this.phoneTile9.Location = new System.Drawing.Point(150,216);
			this.phoneTile9.Name = "phoneTile9";
			this.phoneTile9.PhoneCur = null;
			this.phoneTile9.Size = new System.Drawing.Size(150,181);
			this.phoneTile9.TabIndex = 5;
			// 
			// phoneTile8
			// 
			this.phoneTile8.Location = new System.Drawing.Point(0,216);
			this.phoneTile8.Name = "phoneTile8";
			this.phoneTile8.PhoneCur = null;
			this.phoneTile8.Size = new System.Drawing.Size(150,181);
			this.phoneTile8.TabIndex = 4;
			// 
			// phoneTile4
			// 
			this.phoneTile4.Location = new System.Drawing.Point(450,29);
			this.phoneTile4.Name = "phoneTile4";
			this.phoneTile4.PhoneCur = null;
			this.phoneTile4.Size = new System.Drawing.Size(150,181);
			this.phoneTile4.TabIndex = 3;
			// 
			// phoneTile3
			// 
			this.phoneTile3.Location = new System.Drawing.Point(300,29);
			this.phoneTile3.Name = "phoneTile3";
			this.phoneTile3.PhoneCur = null;
			this.phoneTile3.Size = new System.Drawing.Size(150,181);
			this.phoneTile3.TabIndex = 2;
			// 
			// phoneTile2
			// 
			this.phoneTile2.Location = new System.Drawing.Point(150,29);
			this.phoneTile2.Name = "phoneTile2";
			this.phoneTile2.PhoneCur = null;
			this.phoneTile2.Size = new System.Drawing.Size(150,181);
			this.phoneTile2.TabIndex = 1;
			// 
			// phoneTile1
			// 
			this.phoneTile1.Location = new System.Drawing.Point(0,29);
			this.phoneTile1.Name = "phoneTile1";
			this.phoneTile1.PhoneCur = null;
			this.phoneTile1.Size = new System.Drawing.Size(150,181);
			this.phoneTile1.TabIndex = 0;
			// 
			// FormPhoneTiles
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1054,591);
			this.Controls.Add(this.labelMsg);
			this.Controls.Add(this.butOverride);
			this.Controls.Add(this.phoneTile21);
			this.Controls.Add(this.phoneTile14);
			this.Controls.Add(this.phoneTile7);
			this.Controls.Add(this.phoneTile20);
			this.Controls.Add(this.phoneTile13);
			this.Controls.Add(this.phoneTile6);
			this.Controls.Add(this.phoneTile19);
			this.Controls.Add(this.phoneTile12);
			this.Controls.Add(this.phoneTile5);
			this.Controls.Add(this.phoneTile18);
			this.Controls.Add(this.phoneTile17);
			this.Controls.Add(this.phoneTile16);
			this.Controls.Add(this.phoneTile15);
			this.Controls.Add(this.phoneTile11);
			this.Controls.Add(this.phoneTile10);
			this.Controls.Add(this.phoneTile9);
			this.Controls.Add(this.phoneTile8);
			this.Controls.Add(this.phoneTile4);
			this.Controls.Add(this.phoneTile3);
			this.Controls.Add(this.phoneTile2);
			this.Controls.Add(this.phoneTile1);
			this.Name = "FormPhoneTiles";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Phones";
			this.Load += new System.EventHandler(this.FormPhoneTiles_Load);
			this.Shown += new System.EventHandler(this.FormPhoneTiles_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPhoneTiles_FormClosing);
			this.menuNumbers.ResumeLayout(false);
			this.menuStatus.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private PhoneTile phoneTile1;
		private PhoneTile phoneTile2;
		private PhoneTile phoneTile4;
		private PhoneTile phoneTile3;
		private PhoneTile phoneTile11;
		private PhoneTile phoneTile10;
		private PhoneTile phoneTile9;
		private PhoneTile phoneTile8;
		private PhoneTile phoneTile18;
		private PhoneTile phoneTile17;
		private PhoneTile phoneTile16;
		private PhoneTile phoneTile15;
		private PhoneTile phoneTile19;
		private PhoneTile phoneTile12;
		private PhoneTile phoneTile5;
		private PhoneTile phoneTile20;
		private PhoneTile phoneTile13;
		private PhoneTile phoneTile6;
		private PhoneTile phoneTile21;
		private PhoneTile phoneTile14;
		private PhoneTile phoneTile7;
		private System.Windows.Forms.Timer timerMain;
		private System.Windows.Forms.Label labelMsg;
		private OpenDental.UI.Button butOverride;
		private System.Windows.Forms.Timer timerMsgs;
		private System.Windows.Forms.ContextMenuStrip menuNumbers;
		private System.Windows.Forms.ToolStripMenuItem menuItemManage;
		private System.Windows.Forms.ToolStripMenuItem menuItemAdd;
		private System.Windows.Forms.ContextMenuStrip menuStatus;
		private System.Windows.Forms.ToolStripMenuItem menuItemAvailable;
		private System.Windows.Forms.ToolStripMenuItem menuItemTraining;
		private System.Windows.Forms.ToolStripMenuItem menuItemTeamAssist;
		private System.Windows.Forms.ToolStripMenuItem menuItemWrapUp;
		private System.Windows.Forms.ToolStripMenuItem menuItemOfflineAssist;
		private System.Windows.Forms.ToolStripMenuItem menuItemUnavailable;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem menuItemRinggroupAll;
		private System.Windows.Forms.ToolStripMenuItem menuItemRinggroupNone;
		private System.Windows.Forms.ToolStripMenuItem menuItemRinggroupsDefault;
		private System.Windows.Forms.ToolStripMenuItem menuItemBackup;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem menuItemLunch;
		private System.Windows.Forms.ToolStripMenuItem menuItemHome;
		private System.Windows.Forms.ToolStripMenuItem menuItemBreak;
	}
}