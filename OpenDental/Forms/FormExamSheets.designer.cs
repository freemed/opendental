namespace OpenDental{
	partial class FormExamSheets {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExamSheets));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSheets = new System.Windows.Forms.ToolStripMenuItem();
			this.checkShowOralCancerExams = new System.Windows.Forms.CheckBox();
			this.checkShowPlaqueIndexExams = new System.Windows.Forms.CheckBox();
			this.checkShowPsrExams = new System.Windows.Forms.CheckBox();
			this.butAdd = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butCancel = new OpenDental.UI.Button();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0,0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(415,24);
			this.menuStrip1.TabIndex = 8;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// setupToolStripMenuItem
			// 
			this.setupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSheets});
			this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
			this.setupToolStripMenuItem.Size = new System.Drawing.Size(49,20);
			this.setupToolStripMenuItem.Text = "Setup";
			// 
			// menuItemSheets
			// 
			this.menuItemSheets.Name = "menuItemSheets";
			this.menuItemSheets.Size = new System.Drawing.Size(108,22);
			this.menuItemSheets.Text = "Sheets";
			this.menuItemSheets.Click += new System.EventHandler(this.menuItemSheets_Click);
			// 
			// checkShowOralCancerExams
			// 
			this.checkShowOralCancerExams.AutoSize = true;
			this.checkShowOralCancerExams.Checked = true;
			this.checkShowOralCancerExams.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowOralCancerExams.Location = new System.Drawing.Point(23,38);
			this.checkShowOralCancerExams.Name = "checkShowOralCancerExams";
			this.checkShowOralCancerExams.Size = new System.Drawing.Size(197,17);
			this.checkShowOralCancerExams.TabIndex = 9;
			this.checkShowOralCancerExams.Text = "Show Oral Cancer Screening Exams";
			this.checkShowOralCancerExams.UseVisualStyleBackColor = true;
			// 
			// checkShowPlaqueIndexExams
			// 
			this.checkShowPlaqueIndexExams.AutoSize = true;
			this.checkShowPlaqueIndexExams.Checked = true;
			this.checkShowPlaqueIndexExams.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowPlaqueIndexExams.Location = new System.Drawing.Point(23,61);
			this.checkShowPlaqueIndexExams.Name = "checkShowPlaqueIndexExams";
			this.checkShowPlaqueIndexExams.Size = new System.Drawing.Size(152,17);
			this.checkShowPlaqueIndexExams.TabIndex = 10;
			this.checkShowPlaqueIndexExams.Text = "Show Plaque Index Exams";
			this.checkShowPlaqueIndexExams.UseVisualStyleBackColor = true;
			// 
			// checkShowPsrExams
			// 
			this.checkShowPsrExams.AutoSize = true;
			this.checkShowPsrExams.Checked = true;
			this.checkShowPsrExams.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowPsrExams.Location = new System.Drawing.Point(23,84);
			this.checkShowPsrExams.Name = "checkShowPsrExams";
			this.checkShowPsrExams.Size = new System.Drawing.Size(112,17);
			this.checkShowPsrExams.TabIndex = 11;
			this.checkShowPsrExams.Text = "Show PSR Exams";
			this.checkShowPsrExams.UseVisualStyleBackColor = true;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(12,490);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,24);
			this.butAdd.TabIndex = 6;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(11,116);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(392,368);
			this.gridMain.TabIndex = 4;
			this.gridMain.Title = "Exam Sheets";
			this.gridMain.TranslationName = "FormPatientForms";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(330,490);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormExamSheets
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(415,523);
			this.Controls.Add(this.checkShowPsrExams);
			this.Controls.Add(this.checkShowPlaqueIndexExams);
			this.Controls.Add(this.checkShowOralCancerExams);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FormExamSheets";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Exam Sheets";
			this.Load += new System.EventHandler(this.FormExamSheets_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.ODGrid gridMain;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem menuItemSheets;
		private System.Windows.Forms.CheckBox checkShowOralCancerExams;
		private System.Windows.Forms.CheckBox checkShowPlaqueIndexExams;
		private System.Windows.Forms.CheckBox checkShowPsrExams;
	}
}