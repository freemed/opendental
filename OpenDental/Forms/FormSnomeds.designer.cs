namespace OpenDental{
	partial class FormSnomeds {
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
			this.butOK = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.textCode = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butSearch = new OpenDental.UI.Button();
			this.butImport = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butCrossMap = new OpenDental.UI.Button();
			this.butMapICD9 = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(849, 625);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(103, 24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(849, 655);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(103, 24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textCode
			// 
			this.textCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textCode.Location = new System.Drawing.Point(180, 10);
			this.textCode.Name = "textCode";
			this.textCode.Size = new System.Drawing.Size(399, 20);
			this.textCode.TabIndex = 17;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(172, 16);
			this.label1.TabIndex = 18;
			this.label1.Text = "Code(s) or Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butSearch
			// 
			this.butSearch.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSearch.Autosize = true;
			this.butSearch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearch.CornerRadius = 4F;
			this.butSearch.Location = new System.Drawing.Point(585, 8);
			this.butSearch.Name = "butSearch";
			this.butSearch.Size = new System.Drawing.Size(75, 24);
			this.butSearch.TabIndex = 19;
			this.butSearch.Text = "Search";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
			// 
			// butImport
			// 
			this.butImport.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.butImport.Autosize = true;
			this.butImport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butImport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butImport.CornerRadius = 4F;
			this.butImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butImport.Location = new System.Drawing.Point(6, 19);
			this.butImport.Name = "butImport";
			this.butImport.Size = new System.Drawing.Size(103, 24);
			this.butImport.TabIndex = 16;
			this.butImport.Text = "&Import";
			this.butImport.Click += new System.EventHandler(this.butImport_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(20, 38);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(817, 641);
			this.gridMain.TabIndex = 20;
			this.gridMain.Title = "SNOMED Codes";
			this.gridMain.TranslationName = "FormPatientList";
			this.gridMain.WrapText = false;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butCrossMap
			// 
			this.butCrossMap.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCrossMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butCrossMap.Autosize = true;
			this.butCrossMap.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCrossMap.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCrossMap.CornerRadius = 4F;
			this.butCrossMap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCrossMap.Location = new System.Drawing.Point(849, 273);
			this.butCrossMap.Name = "butCrossMap";
			this.butCrossMap.Size = new System.Drawing.Size(103, 24);
			this.butCrossMap.TabIndex = 21;
			this.butCrossMap.Text = "Crossmap";
			this.butCrossMap.Click += new System.EventHandler(this.butCrossMap_Click);
			// 
			// butMapICD9
			// 
			this.butMapICD9.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butMapICD9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.butMapICD9.Autosize = true;
			this.butMapICD9.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMapICD9.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMapICD9.CornerRadius = 4F;
			this.butMapICD9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butMapICD9.Location = new System.Drawing.Point(6, 49);
			this.butMapICD9.Name = "butMapICD9";
			this.butMapICD9.Size = new System.Drawing.Size(103, 24);
			this.butMapICD9.TabIndex = 24;
			this.butMapICD9.Text = "Map to ICD9";
			this.butMapICD9.Click += new System.EventHandler(this.butMapToSnomed_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.butImport);
			this.groupBox1.Controls.Add(this.butMapICD9);
			this.groupBox1.Location = new System.Drawing.Point(843, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(115, 82);
			this.groupBox1.TabIndex = 25;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "One-Time Tools";
			// 
			// FormSnomeds
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(961, 691);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butCrossMap);
			this.Controls.Add(this.butSearch);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textCode);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butClose);
			this.Name = "FormSnomeds";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Snomeds";
			this.Load += new System.EventHandler(this.FormSnomeds_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textCode;
		private System.Windows.Forms.Label label1;
		private UI.Button butSearch;
		private UI.Button butImport;
		private UI.ODGrid gridMain;
		private UI.Button butCrossMap;
		private UI.Button butMapICD9;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}