namespace OpenDental {
	partial class FormSupplyInventory {
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
			this.menuItemSetupSuppliers = new System.Windows.Forms.MenuItem();
			this.butAdd = new OpenDental.UI.Button();
			this.gridNeeded = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSetupSuppliers});
			// 
			// menuItemSetupSuppliers
			// 
			this.menuItemSetupSuppliers.Index = 0;
			this.menuItemSetupSuppliers.Text = "Setup Suppliers";
			this.menuItemSetupSuppliers.Click += new System.EventHandler(this.menuItemSetupSuppliers_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(12,16);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,26);
			this.butAdd.TabIndex = 5;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// gridNeeded
			// 
			this.gridNeeded.HScrollVisible = false;
			this.gridNeeded.Location = new System.Drawing.Point(12,48);
			this.gridNeeded.Name = "gridNeeded";
			this.gridNeeded.ScrollValue = 0;
			this.gridNeeded.Size = new System.Drawing.Size(446,339);
			this.gridNeeded.TabIndex = 4;
			this.gridNeeded.Title = "Supplies Needed";
			this.gridNeeded.TranslationName = null;
			this.gridNeeded.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridNeeded_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(783,635);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormSupplyInventory
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(883,686);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.gridNeeded);
			this.Controls.Add(this.butClose);
			this.Menu = this.mainMenu1;
			this.Name = "FormSupplyInventory";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Supply Inventory";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormInventory_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private OpenDental.UI.ODGrid gridNeeded;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItemSetupSuppliers;
	}
}