namespace OpenDental{
	partial class FormEmailAddresses {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEmailAddresses));
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butAdd = new OpenDental.UI.Button();
			this.butSetDefault = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textInboxComputerName = new OpenDental.ODtextBox();
			this.labelInboxComputerName = new System.Windows.Forms.Label();
			this.labelInboxCheckInterval = new System.Windows.Forms.Label();
			this.textInboxCheckInterval = new OpenDental.ODtextBox();
			this.labelInboxCheckUnits = new System.Windows.Forms.Label();
			this.butThisComputer = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(18,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(391,440);
			this.gridMain.TabIndex = 4;
			this.gridMain.Title = "Email Addresses";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(435,136);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,24);
			this.butAdd.TabIndex = 3;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butSetDefault
			// 
			this.butSetDefault.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSetDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSetDefault.Autosize = true;
			this.butSetDefault.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetDefault.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetDefault.CornerRadius = 4F;
			this.butSetDefault.Location = new System.Drawing.Point(435,56);
			this.butSetDefault.Name = "butSetDefault";
			this.butSetDefault.Size = new System.Drawing.Size(75,24);
			this.butSetDefault.TabIndex = 3;
			this.butSetDefault.Text = "Set Default";
			this.butSetDefault.Click += new System.EventHandler(this.butSetDefault_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(435,516);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(435,472);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textInboxComputerName
			// 
			this.textInboxComputerName.AcceptsTab = true;
			this.textInboxComputerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textInboxComputerName.DetectUrls = false;
			this.textInboxComputerName.Location = new System.Drawing.Point(18,472);
			this.textInboxComputerName.Name = "textInboxComputerName";
			this.textInboxComputerName.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textInboxComputerName.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textInboxComputerName.Size = new System.Drawing.Size(240,24);
			this.textInboxComputerName.TabIndex = 5;
			this.textInboxComputerName.Text = "";
			// 
			// labelInboxComputerName
			// 
			this.labelInboxComputerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelInboxComputerName.Location = new System.Drawing.Point(16,453);
			this.labelInboxComputerName.Name = "labelInboxComputerName";
			this.labelInboxComputerName.Size = new System.Drawing.Size(335,18);
			this.labelInboxComputerName.TabIndex = 6;
			this.labelInboxComputerName.Text = "Computer Name To Fetch New Email From";
			this.labelInboxComputerName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelInboxCheckInterval
			// 
			this.labelInboxCheckInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelInboxCheckInterval.Location = new System.Drawing.Point(16,497);
			this.labelInboxCheckInterval.Name = "labelInboxCheckInterval";
			this.labelInboxCheckInterval.Size = new System.Drawing.Size(335,18);
			this.labelInboxCheckInterval.TabIndex = 7;
			this.labelInboxCheckInterval.Text = "Inbox Check Interval";
			this.labelInboxCheckInterval.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textInboxCheckInterval
			// 
			this.textInboxCheckInterval.AcceptsTab = true;
			this.textInboxCheckInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textInboxCheckInterval.DetectUrls = false;
			this.textInboxCheckInterval.Location = new System.Drawing.Point(18,516);
			this.textInboxCheckInterval.Name = "textInboxCheckInterval";
			this.textInboxCheckInterval.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textInboxCheckInterval.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textInboxCheckInterval.Size = new System.Drawing.Size(30,24);
			this.textInboxCheckInterval.TabIndex = 8;
			this.textInboxCheckInterval.Text = "";
			// 
			// labelInboxCheckUnits
			// 
			this.labelInboxCheckUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelInboxCheckUnits.Location = new System.Drawing.Point(49,519);
			this.labelInboxCheckUnits.Name = "labelInboxCheckUnits";
			this.labelInboxCheckUnits.Size = new System.Drawing.Size(198,18);
			this.labelInboxCheckUnits.TabIndex = 9;
			this.labelInboxCheckUnits.Text = "minutes (1 to 60)";
			this.labelInboxCheckUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butThisComputer
			// 
			this.butThisComputer.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butThisComputer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butThisComputer.Autosize = true;
			this.butThisComputer.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butThisComputer.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butThisComputer.CornerRadius = 4F;
			this.butThisComputer.Location = new System.Drawing.Point(264,472);
			this.butThisComputer.Name = "butThisComputer";
			this.butThisComputer.Size = new System.Drawing.Size(87,24);
			this.butThisComputer.TabIndex = 10;
			this.butThisComputer.Text = "This Computer";
			this.butThisComputer.Click += new System.EventHandler(this.butThisComputer_Click);
			// 
			// FormEmailAddresses
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(529,558);
			this.Controls.Add(this.butThisComputer);
			this.Controls.Add(this.labelInboxCheckUnits);
			this.Controls.Add(this.textInboxCheckInterval);
			this.Controls.Add(this.labelInboxCheckInterval);
			this.Controls.Add(this.labelInboxComputerName);
			this.Controls.Add(this.textInboxComputerName);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butSetDefault);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormEmailAddresses";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Email Addresses";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEmailAddresses_FormClosing);
			this.Load += new System.EventHandler(this.FormEmailAddresses_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butSetDefault;
		private OpenDental.UI.Button butCancel;
		private UI.ODGrid gridMain;
		private UI.Button butAdd;
		private UI.Button butOK;
		private ODtextBox textInboxComputerName;
		private System.Windows.Forms.Label labelInboxComputerName;
		private System.Windows.Forms.Label labelInboxCheckInterval;
		private ODtextBox textInboxCheckInterval;
		private System.Windows.Forms.Label labelInboxCheckUnits;
		private UI.Button butThisComputer;
	}
}