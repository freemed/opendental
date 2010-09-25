namespace OpenDental{
	partial class FormWebForms {
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemSetup = new System.Windows.Forms.MenuItem();
			this.groupDateRange = new System.Windows.Forms.GroupBox();
			this.butRefresh = new OpenDental.UI.Button();
			this.butToday = new OpenDental.UI.Button();
			this.textDateStart = new OpenDental.ValidDate();
			this.labelStartDate = new System.Windows.Forms.Label();
			this.labelEndDate = new System.Windows.Forms.Label();
			this.textDateEnd = new OpenDental.ValidDate();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.menuWebFormsRight = new System.Windows.Forms.ContextMenu();
			this.menuItemViewSheet = new System.Windows.Forms.MenuItem();
			this.menuItemImportSheet = new System.Windows.Forms.MenuItem();
			this.menuItemViewAllSheets = new System.Windows.Forms.MenuItem();
			this.butRetrieve = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupDateRange.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSetup});
			// 
			// menuItemSetup
			// 
			this.menuItemSetup.Index = 0;
			this.menuItemSetup.Text = "Setup";
			this.menuItemSetup.Click += new System.EventHandler(this.menuItemSetup_Click);
			// 
			// groupDateRange
			// 
			this.groupDateRange.Controls.Add(this.butRefresh);
			this.groupDateRange.Controls.Add(this.butToday);
			this.groupDateRange.Controls.Add(this.textDateStart);
			this.groupDateRange.Controls.Add(this.labelStartDate);
			this.groupDateRange.Controls.Add(this.labelEndDate);
			this.groupDateRange.Controls.Add(this.textDateEnd);
			this.groupDateRange.Location = new System.Drawing.Point(134,12);
			this.groupDateRange.Name = "groupDateRange";
			this.groupDateRange.Size = new System.Drawing.Size(245,69);
			this.groupDateRange.TabIndex = 238;
			this.groupDateRange.TabStop = false;
			this.groupDateRange.Text = "Date Range";
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(158,39);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(77,24);
			this.butRefresh.TabIndex = 243;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// butToday
			// 
			this.butToday.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butToday.Autosize = true;
			this.butToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butToday.CornerRadius = 4F;
			this.butToday.Location = new System.Drawing.Point(158,14);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(77,24);
			this.butToday.TabIndex = 242;
			this.butToday.Text = "Today";
			this.butToday.Click += new System.EventHandler(this.butToday_Click);
			// 
			// textDateStart
			// 
			this.textDateStart.BackColor = System.Drawing.SystemColors.Window;
			this.textDateStart.ForeColor = System.Drawing.SystemColors.WindowText;
			this.textDateStart.Location = new System.Drawing.Point(75,16);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77,20);
			this.textDateStart.TabIndex = 223;
			// 
			// labelStartDate
			// 
			this.labelStartDate.Location = new System.Drawing.Point(6,19);
			this.labelStartDate.Name = "labelStartDate";
			this.labelStartDate.Size = new System.Drawing.Size(69,14);
			this.labelStartDate.TabIndex = 221;
			this.labelStartDate.Text = "Start Date";
			this.labelStartDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelEndDate
			// 
			this.labelEndDate.Location = new System.Drawing.Point(6,44);
			this.labelEndDate.Name = "labelEndDate";
			this.labelEndDate.Size = new System.Drawing.Size(69,14);
			this.labelEndDate.TabIndex = 222;
			this.labelEndDate.Text = "End Date";
			this.labelEndDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(75,41);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(77,20);
			this.textDateEnd.TabIndex = 224;
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// menuWebFormsRight
			// 
			this.menuWebFormsRight.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemViewSheet,
            this.menuItemImportSheet,
            this.menuItemViewAllSheets});
			// 
			// menuItemViewSheet
			// 
			this.menuItemViewSheet.Index = 0;
			this.menuItemViewSheet.Text = "View Sheet";
			this.menuItemViewSheet.Click += new System.EventHandler(this.menuItemViewSheet_Click);
			// 
			// menuItemImportSheet
			// 
			this.menuItemImportSheet.Index = 1;
			this.menuItemImportSheet.Text = "Import Sheet";
			this.menuItemImportSheet.Click += new System.EventHandler(this.menuItemImportSheet_Click);
			// 
			// menuItemViewAllSheets
			// 
			this.menuItemViewAllSheets.Index = 2;
			this.menuItemViewAllSheets.Text = "View all sheets of patient";
			this.menuItemViewAllSheets.Click += new System.EventHandler(this.menuItemViewAllSheets_Click);
			// 
			// butRetrieve
			// 
			this.butRetrieve.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRetrieve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRetrieve.Autosize = true;
			this.butRetrieve.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRetrieve.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRetrieve.CornerRadius = 4F;
			this.butRetrieve.Location = new System.Drawing.Point(536,18);
			this.butRetrieve.Name = "butRetrieve";
			this.butRetrieve.Size = new System.Drawing.Size(123,24);
			this.butRetrieve.TabIndex = 46;
			this.butRetrieve.Text = "&Retrieve New Forms";
			this.butRetrieve.Click += new System.EventHandler(this.butRetrieve_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,87);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(647,253);
			this.gridMain.TabIndex = 4;
			this.gridMain.Title = "Webforms";
			this.gridMain.TranslationName = "TableWebforms";
			this.gridMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridMain_MouseUp);
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(669,280);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(669,316);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormWebForms
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(755,352);
			this.Controls.Add(this.groupDateRange);
			this.Controls.Add(this.butRetrieve);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Menu = this.mainMenu1;
			this.Name = "FormWebForms";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Web Forms";
			this.Load += new System.EventHandler(this.FormWebForms_Load);
			this.Shown += new System.EventHandler(this.FormWebForms_Shown);
			this.groupDateRange.ResumeLayout(false);
			this.groupDateRange.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.ODGrid gridMain;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItemSetup;
		private OpenDental.UI.Button butRetrieve;
		private System.Windows.Forms.GroupBox groupDateRange;
		private OpenDental.UI.Button butToday;
		private ValidDate textDateStart;
		private System.Windows.Forms.Label labelStartDate;
		private System.Windows.Forms.Label labelEndDate;
		private ValidDate textDateEnd;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private OpenDental.UI.Button butRefresh;
		private System.Windows.Forms.ContextMenu menuWebFormsRight;
		private System.Windows.Forms.MenuItem menuItemViewSheet;
		private System.Windows.Forms.MenuItem menuItemImportSheet;
		private System.Windows.Forms.MenuItem menuItemViewAllSheets;
	}
}