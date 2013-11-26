namespace OpenDental{
	partial class FormEhrPatientExport {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEhrPatientExport));
			this.comboProv = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboSite = new System.Windows.Forms.ComboBox();
			this.labelSite = new System.Windows.Forms.Label();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.labelLName = new System.Windows.Forms.Label();
			this.labelFName = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butSearch = new OpenDental.UI.Button();
			this.butExport = new OpenDental.UI.Button();
			this.textFName = new OpenDental.ODtextBox();
			this.textLName = new OpenDental.ODtextBox();
			this.butSelectAll = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textPatNum = new OpenDental.ODtextBox();
			this.SuspendLayout();
			// 
			// comboProv
			// 
			this.comboProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProv.Location = new System.Drawing.Point(348, 12);
			this.comboProv.MaxDropDownItems = 40;
			this.comboProv.Name = "comboProv";
			this.comboProv.Size = new System.Drawing.Size(160, 21);
			this.comboProv.TabIndex = 23;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(256, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(90, 21);
			this.label4.TabIndex = 22;
			this.label4.Text = "Primary Provider";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboSite
			// 
			this.comboSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSite.Location = new System.Drawing.Point(348, 54);
			this.comboSite.MaxDropDownItems = 40;
			this.comboSite.Name = "comboSite";
			this.comboSite.Size = new System.Drawing.Size(160, 21);
			this.comboSite.TabIndex = 29;
			// 
			// labelSite
			// 
			this.labelSite.Location = new System.Drawing.Point(276, 54);
			this.labelSite.Name = "labelSite";
			this.labelSite.Size = new System.Drawing.Size(70, 21);
			this.labelSite.TabIndex = 28;
			this.labelSite.Text = "Site";
			this.labelSite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(348, 33);
			this.comboClinic.MaxDropDownItems = 40;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(160, 21);
			this.comboClinic.TabIndex = 27;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(276, 33);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(70, 21);
			this.labelClinic.TabIndex = 26;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelLName
			// 
			this.labelLName.Location = new System.Drawing.Point(15, 32);
			this.labelLName.Name = "labelLName";
			this.labelLName.Size = new System.Drawing.Size(70, 22);
			this.labelLName.TabIndex = 37;
			this.labelLName.Text = "Last Name";
			this.labelLName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelFName
			// 
			this.labelFName.Location = new System.Drawing.Point(15, 12);
			this.labelFName.Name = "labelFName";
			this.labelFName.Size = new System.Drawing.Size(70, 21);
			this.labelFName.TabIndex = 35;
			this.labelFName.Text = "First Name";
			this.labelFName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridMain
			// 
			this.gridMain.AllowSortingByColumn = true;
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(18, 86);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(590, 365);
			this.gridMain.TabIndex = 4;
			this.gridMain.Title = "Patient Export List";
			this.gridMain.TranslationName = null;
			// 
			// butSearch
			// 
			this.butSearch.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSearch.Autosize = true;
			this.butSearch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearch.CornerRadius = 4F;
			this.butSearch.Location = new System.Drawing.Point(531, 10);
			this.butSearch.Name = "butSearch";
			this.butSearch.Size = new System.Drawing.Size(75, 23);
			this.butSearch.TabIndex = 33;
			this.butSearch.Text = "&Search";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
			// 
			// butExport
			// 
			this.butExport.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butExport.Autosize = true;
			this.butExport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butExport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butExport.CornerRadius = 4F;
			this.butExport.Location = new System.Drawing.Point(18, 462);
			this.butExport.Name = "butExport";
			this.butExport.Size = new System.Drawing.Size(100, 24);
			this.butExport.TabIndex = 30;
			this.butExport.Text = "Export Selected";
			this.butExport.Click += new System.EventHandler(this.butExport_Click);
			// 
			// textFName
			// 
			this.textFName.AcceptsTab = true;
			this.textFName.DetectUrls = false;
			this.textFName.Location = new System.Drawing.Point(87, 12);
			this.textFName.Multiline = false;
			this.textFName.Name = "textFName";
			this.textFName.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textFName.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textFName.Size = new System.Drawing.Size(160, 21);
			this.textFName.TabIndex = 39;
			this.textFName.Text = "";
			// 
			// textLName
			// 
			this.textLName.AcceptsTab = true;
			this.textLName.DetectUrls = false;
			this.textLName.Location = new System.Drawing.Point(87, 33);
			this.textLName.Multiline = false;
			this.textLName.Name = "textLName";
			this.textLName.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textLName.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textLName.Size = new System.Drawing.Size(160, 21);
			this.textLName.TabIndex = 38;
			this.textLName.Text = "";
			// 
			// butSelectAll
			// 
			this.butSelectAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSelectAll.Autosize = true;
			this.butSelectAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSelectAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSelectAll.CornerRadius = 4F;
			this.butSelectAll.Location = new System.Drawing.Point(276, 462);
			this.butSelectAll.Name = "butSelectAll";
			this.butSelectAll.Size = new System.Drawing.Size(75, 24);
			this.butSelectAll.TabIndex = 33;
			this.butSelectAll.Text = "Select All";
			this.butSelectAll.Click += new System.EventHandler(this.butSelectAll_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(531, 462);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(15, 54);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 22);
			this.label1.TabIndex = 40;
			this.label1.Text = "Patnum";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPatNum
			// 
			this.textPatNum.AcceptsTab = true;
			this.textPatNum.DetectUrls = false;
			this.textPatNum.Location = new System.Drawing.Point(87, 55);
			this.textPatNum.Multiline = false;
			this.textPatNum.Name = "textPatNum";
			this.textPatNum.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textPatNum.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textPatNum.Size = new System.Drawing.Size(160, 21);
			this.textPatNum.TabIndex = 41;
			this.textPatNum.Text = "";
			// 
			// FormEhrPatientExport
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(626, 498);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textPatNum);
			this.Controls.Add(this.butSearch);
			this.Controls.Add(this.butExport);
			this.Controls.Add(this.textFName);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.labelLName);
			this.Controls.Add(this.labelSite);
			this.Controls.Add(this.textLName);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelFName);
			this.Controls.Add(this.butSelectAll);
			this.Controls.Add(this.comboSite);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.comboProv);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(611, 243);
			this.Name = "FormEhrPatientExport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ehr Patient Export";
			this.Load += new System.EventHandler(this.FormEhrPatientExport_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.ComboBox comboProv;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboSite;
		private System.Windows.Forms.Label labelSite;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		private UI.Button butExport;
		private UI.Button butSelectAll;
		private UI.Button butSearch;
		private System.Windows.Forms.Label labelLName;
		private System.Windows.Forms.Label labelFName;
		private ODtextBox textFName;
		private ODtextBox textLName;
		private System.Windows.Forms.Label label1;
		private ODtextBox textPatNum;
	}
}