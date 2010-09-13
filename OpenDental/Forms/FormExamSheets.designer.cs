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
			this.comboExamType = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butRefreshList = new OpenDental.UI.Button();
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
			this.menuStrip1.Size = new System.Drawing.Size(417,24);
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
			// comboExamType
			// 
			this.comboExamType.FormattingEnabled = true;
			this.comboExamType.Location = new System.Drawing.Point(11,53);
			this.comboExamType.Name = "comboExamType";
			this.comboExamType.Size = new System.Drawing.Size(259,21);
			this.comboExamType.TabIndex = 45;
			this.comboExamType.SelectedValueChanged += new System.EventHandler(this.comboExamType_SelectedValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12,36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(199,13);
			this.label1.TabIndex = 46;
			this.label1.Text = "Show Exam Type (blank shows all types)";
			// 
			// butRefreshList
			// 
			this.butRefreshList.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefreshList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefreshList.Autosize = true;
			this.butRefreshList.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefreshList.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefreshList.CornerRadius = 4F;
			this.butRefreshList.Location = new System.Drawing.Point(279,50);
			this.butRefreshList.Name = "butRefreshList";
			this.butRefreshList.Size = new System.Drawing.Size(127,24);
			this.butRefreshList.TabIndex = 47;
			this.butRefreshList.Text = "Refresh List";
			this.butRefreshList.Click += new System.EventHandler(this.butRefreshList_Click);
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
			this.butAdd.Location = new System.Drawing.Point(11,492);
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
			this.gridMain.Location = new System.Drawing.Point(12,80);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(394,406);
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
			this.butCancel.Location = new System.Drawing.Point(331,492);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormExamSheets
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(417,525);
			this.Controls.Add(this.butRefreshList);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboExamType);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.menuStrip1);
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
		private System.Windows.Forms.ComboBox comboExamType;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butRefreshList;
	}
}