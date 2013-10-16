namespace OpenDental{
	partial class FormEhrTimeSynch {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEhrTimeSynch));
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.label6 = new System.Windows.Forms.Label();
			this.labelDatabaseOutOfSynch = new System.Windows.Forms.Label();
			this.labelLocalOutOfSynch = new System.Windows.Forms.Label();
			this.labelAllSynched = new System.Windows.Forms.Label();
			this.butSynchTime = new OpenDental.UI.Button();
			this.textLocalTime = new OpenDental.ODtextBox();
			this.textServerTime = new OpenDental.ODtextBox();
			this.textNistTime = new OpenDental.ODtextBox();
			this.butRefreshTime = new OpenDental.UI.Button();
			this.textNistUrl = new OpenDental.ODtextBox();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(25, 78);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(106, 20);
			this.label2.TabIndex = 81;
			this.label2.Text = "NIST server address";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(22, 104);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(109, 20);
			this.label1.TabIndex = 82;
			this.label1.Text = "NIST server";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(25, 130);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(106, 20);
			this.label3.TabIndex = 83;
			this.label3.Text = "Database server";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(28, 156);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(103, 20);
			this.label4.TabIndex = 84;
			this.label4.Text = "Local machine";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(28, 205);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(345, 104);
			this.label5.TabIndex = 85;
			this.label5.Text = resources.GetString("label5.Text");
			// 
			// timer1
			// 
			this.timer1.Interval = 4000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(25, 21);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(456, 44);
			this.label6.TabIndex = 87;
			this.label6.Text = resources.GetString("label6.Text");
			// 
			// labelDatabaseSynch
			// 
			this.labelDatabaseOutOfSynch.ForeColor = System.Drawing.Color.DarkRed;
			this.labelDatabaseOutOfSynch.Location = new System.Drawing.Point(309, 134);
			this.labelDatabaseOutOfSynch.Name = "labelDatabaseSynch";
			this.labelDatabaseOutOfSynch.Size = new System.Drawing.Size(187, 19);
			this.labelDatabaseOutOfSynch.TabIndex = 88;
			this.labelDatabaseOutOfSynch.Text = "Database time out of synch with local";
			this.labelDatabaseOutOfSynch.Visible = false;
			// 
			// labelLocalSynch
			// 
			this.labelLocalOutOfSynch.ForeColor = System.Drawing.Color.DarkRed;
			this.labelLocalOutOfSynch.Location = new System.Drawing.Point(326, 160);
			this.labelLocalOutOfSynch.Name = "labelLocalSynch";
			this.labelLocalOutOfSynch.Size = new System.Drawing.Size(187, 19);
			this.labelLocalOutOfSynch.TabIndex = 89;
			this.labelLocalOutOfSynch.Text = "Local time out of synch with NIST";
			this.labelLocalOutOfSynch.Visible = false;
			// 
			// labelAllSynched
			// 
			this.labelAllSynched.ForeColor = System.Drawing.Color.DarkRed;
			this.labelAllSynched.Location = new System.Drawing.Point(294, 108);
			this.labelAllSynched.Name = "labelAllSynched";
			this.labelAllSynched.Size = new System.Drawing.Size(200, 19);
			this.labelAllSynched.TabIndex = 90;
			this.labelAllSynched.Text = "All times synchronized within one second";
			this.labelAllSynched.Visible = false;
			// 
			// butSynchTime
			// 
			this.butSynchTime.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSynchTime.Autosize = true;
			this.butSynchTime.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSynchTime.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSynchTime.CornerRadius = 4F;
			this.butSynchTime.Location = new System.Drawing.Point(254, 154);
			this.butSynchTime.Name = "butSynchTime";
			this.butSynchTime.Size = new System.Drawing.Size(61, 24);
			this.butSynchTime.TabIndex = 80;
			this.butSynchTime.Text = "Synch";
			this.butSynchTime.Click += new System.EventHandler(this.butSynchTime_Click);
			// 
			// textLocalTime
			// 
			this.textLocalTime.AcceptsTab = true;
			this.textLocalTime.DetectUrls = false;
			this.textLocalTime.Location = new System.Drawing.Point(137, 156);
			this.textLocalTime.Multiline = false;
			this.textLocalTime.Name = "textLocalTime";
			this.textLocalTime.QuickPasteType = OpenDentBusiness.QuickPasteType.MedicationEdit;
			this.textLocalTime.ReadOnly = true;
			this.textLocalTime.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textLocalTime.Size = new System.Drawing.Size(111, 20);
			this.textLocalTime.TabIndex = 79;
			this.textLocalTime.Text = "";
			// 
			// textServerTime
			// 
			this.textServerTime.AcceptsTab = true;
			this.textServerTime.DetectUrls = false;
			this.textServerTime.Location = new System.Drawing.Point(137, 130);
			this.textServerTime.Multiline = false;
			this.textServerTime.Name = "textServerTime";
			this.textServerTime.QuickPasteType = OpenDentBusiness.QuickPasteType.MedicationEdit;
			this.textServerTime.ReadOnly = true;
			this.textServerTime.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textServerTime.Size = new System.Drawing.Size(111, 20);
			this.textServerTime.TabIndex = 78;
			this.textServerTime.Text = "";
			// 
			// textNistTime
			// 
			this.textNistTime.AcceptsTab = true;
			this.textNistTime.DetectUrls = false;
			this.textNistTime.Location = new System.Drawing.Point(137, 104);
			this.textNistTime.Multiline = false;
			this.textNistTime.Name = "textNistTime";
			this.textNistTime.QuickPasteType = OpenDentBusiness.QuickPasteType.MedicationEdit;
			this.textNistTime.ReadOnly = true;
			this.textNistTime.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textNistTime.Size = new System.Drawing.Size(111, 20);
			this.textNistTime.TabIndex = 77;
			this.textNistTime.Text = "";
			// 
			// butRefreshTime
			// 
			this.butRefreshTime.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRefreshTime.Autosize = true;
			this.butRefreshTime.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefreshTime.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefreshTime.CornerRadius = 4F;
			this.butRefreshTime.Location = new System.Drawing.Point(398, 76);
			this.butRefreshTime.Name = "butRefreshTime";
			this.butRefreshTime.Size = new System.Drawing.Size(83, 24);
			this.butRefreshTime.TabIndex = 76;
			this.butRefreshTime.Text = "Refresh Times";
			this.butRefreshTime.Click += new System.EventHandler(this.butRefreshTime_Click);
			// 
			// textNistUrl
			// 
			this.textNistUrl.AcceptsTab = true;
			this.textNistUrl.DetectUrls = false;
			this.textNistUrl.Location = new System.Drawing.Point(137, 78);
			this.textNistUrl.Multiline = false;
			this.textNistUrl.Name = "textNistUrl";
			this.textNistUrl.QuickPasteType = OpenDentBusiness.QuickPasteType.MedicationEdit;
			this.textNistUrl.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textNistUrl.Size = new System.Drawing.Size(255, 20);
			this.textNistUrl.TabIndex = 75;
			this.textNistUrl.Text = "";
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(438, 275);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormEhrTimeSynch
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(538, 326);
			this.Controls.Add(this.labelAllSynched);
			this.Controls.Add(this.labelLocalOutOfSynch);
			this.Controls.Add(this.labelDatabaseOutOfSynch);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butSynchTime);
			this.Controls.Add(this.textLocalTime);
			this.Controls.Add(this.textServerTime);
			this.Controls.Add(this.textNistTime);
			this.Controls.Add(this.butRefreshTime);
			this.Controls.Add(this.textNistUrl);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.label5);
			this.Name = "FormEhrTimeSynch";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Time Synchronization";
			this.Load += new System.EventHandler(this.FormEhrTime_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private ODtextBox textNistUrl;
		private UI.Button butRefreshTime;
		private ODtextBox textNistTime;
		private ODtextBox textServerTime;
		private ODtextBox textLocalTime;
		private UI.Button butSynchTime;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label labelDatabaseOutOfSynch;
		private System.Windows.Forms.Label labelLocalOutOfSynch;
		private System.Windows.Forms.Label labelAllSynched;
	}
}