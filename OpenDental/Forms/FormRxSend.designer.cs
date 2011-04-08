namespace OpenDental{
	partial class FormRxSend {
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
			this.comboPharmacy = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butSend = new OpenDental.UI.Button();
			this.butRefresh = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butAll = new OpenDental.UI.Button();
			this.butNone = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// comboPharmacy
			// 
			this.comboPharmacy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPharmacy.Location = new System.Drawing.Point(106,15);
			this.comboPharmacy.MaxDropDownItems = 40;
			this.comboPharmacy.Name = "comboPharmacy";
			this.comboPharmacy.Size = new System.Drawing.Size(160,21);
			this.comboPharmacy.TabIndex = 55;
			this.comboPharmacy.SelectionChangeCommitted += new System.EventHandler(this.comboPharmacy_SelectionChangeCommitted);
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(12,18);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(88,14);
			this.labelClinic.TabIndex = 54;
			this.labelClinic.Text = "Pharmacy";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,49);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(650,392);
			this.gridMain.TabIndex = 33;
			this.gridMain.Title = "Rx Waiting to Send";
			this.gridMain.TranslationName = "TableQueue";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.CornerRadius = 4F;
			this.butSend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSend.Location = new System.Drawing.Point(298,457);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(78,24);
			this.butSend.TabIndex = 248;
			this.butSend.Text = "Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(565,12);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(98,24);
			this.butRefresh.TabIndex = 56;
			this.butRefresh.Text = "&Refresh List";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(588,457);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAll.Location = new System.Drawing.Point(12,457);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(78,24);
			this.butAll.TabIndex = 249;
			this.butAll.Text = "All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// butNone
			// 
			this.butNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butNone.Autosize = true;
			this.butNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNone.CornerRadius = 4F;
			this.butNone.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butNone.Location = new System.Drawing.Point(96,457);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(78,24);
			this.butNone.TabIndex = 250;
			this.butNone.Text = "None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// FormRxSend
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(675,493);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.comboPharmacy);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butCancel);
			this.Name = "FormRxSend";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Rx Send";
			this.Load += new System.EventHandler(this.FormRxSend_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butCancel;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.ComboBox comboPharmacy;
		private System.Windows.Forms.Label labelClinic;
		private UI.Button butRefresh;
		private UI.Button butSend;
		private UI.Button butAll;
		private UI.Button butNone;
	}
}