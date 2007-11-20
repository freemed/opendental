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
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.butNewOrder = new OpenDental.UI.Button();
			this.gridOrder = new OpenDental.UI.ODGrid();
			this.butAddNeeded = new OpenDental.UI.Button();
			this.gridNeeded = new OpenDental.UI.ODGrid();
			this.label3 = new System.Windows.Forms.Label();
			this.comboSupplier = new System.Windows.Forms.ComboBox();
			this.butAddSupply = new OpenDental.UI.Button();
			this.gridSupply = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menuItemSuppliers = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCategories = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0,24);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.butNewOrder);
			this.splitContainer.Panel1.Controls.Add(this.gridOrder);
			this.splitContainer.Panel1.Controls.Add(this.butAddNeeded);
			this.splitContainer.Panel1.Controls.Add(this.gridNeeded);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.label3);
			this.splitContainer.Panel2.Controls.Add(this.comboSupplier);
			this.splitContainer.Panel2.Controls.Add(this.butAddSupply);
			this.splitContainer.Panel2.Controls.Add(this.gridSupply);
			this.splitContainer.Panel2.Controls.Add(this.butClose);
			this.splitContainer.Size = new System.Drawing.Size(757,583);
			this.splitContainer.SplitterDistance = 218;
			this.splitContainer.TabIndex = 11;
			// 
			// butNewOrder
			// 
			this.butNewOrder.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNewOrder.Autosize = true;
			this.butNewOrder.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNewOrder.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNewOrder.CornerRadius = 4F;
			this.butNewOrder.Image = global::OpenDental.Properties.Resources.Add;
			this.butNewOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butNewOrder.Location = new System.Drawing.Point(388,3);
			this.butNewOrder.Name = "butNewOrder";
			this.butNewOrder.Size = new System.Drawing.Size(95,26);
			this.butNewOrder.TabIndex = 12;
			this.butNewOrder.Text = "New Order";
			this.butNewOrder.Click += new System.EventHandler(this.butNewOrder_Click);
			// 
			// gridOrder
			// 
			this.gridOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridOrder.HScrollVisible = false;
			this.gridOrder.Location = new System.Drawing.Point(388,31);
			this.gridOrder.Name = "gridOrder";
			this.gridOrder.ScrollValue = 0;
			this.gridOrder.Size = new System.Drawing.Size(365,187);
			this.gridOrder.TabIndex = 11;
			this.gridOrder.Title = "Order History";
			this.gridOrder.TranslationName = null;
			this.gridOrder.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridOrder_CellClick);
			this.gridOrder.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridOrder_CellDoubleClick);
			// 
			// butAddNeeded
			// 
			this.butAddNeeded.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddNeeded.Autosize = true;
			this.butAddNeeded.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddNeeded.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddNeeded.CornerRadius = 4F;
			this.butAddNeeded.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddNeeded.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddNeeded.Location = new System.Drawing.Point(3,3);
			this.butAddNeeded.Name = "butAddNeeded";
			this.butAddNeeded.Size = new System.Drawing.Size(71,26);
			this.butAddNeeded.TabIndex = 5;
			this.butAddNeeded.Text = "Add";
			this.butAddNeeded.Click += new System.EventHandler(this.butAddNeeded_Click);
			// 
			// gridNeeded
			// 
			this.gridNeeded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridNeeded.HScrollVisible = false;
			this.gridNeeded.Location = new System.Drawing.Point(3,31);
			this.gridNeeded.Name = "gridNeeded";
			this.gridNeeded.ScrollValue = 0;
			this.gridNeeded.Size = new System.Drawing.Size(379,187);
			this.gridNeeded.TabIndex = 4;
			this.gridNeeded.Title = "Supplies Needed";
			this.gridNeeded.TranslationName = null;
			this.gridNeeded.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridNeeded_CellDoubleClick);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(487,6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(92,18);
			this.label3.TabIndex = 10;
			this.label3.Text = "Supplier";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboSupplier
			// 
			this.comboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSupplier.FormattingEnabled = true;
			this.comboSupplier.Location = new System.Drawing.Point(580,5);
			this.comboSupplier.Name = "comboSupplier";
			this.comboSupplier.Size = new System.Drawing.Size(173,21);
			this.comboSupplier.TabIndex = 8;
			this.comboSupplier.SelectionChangeCommitted += new System.EventHandler(this.comboSupplier_SelectionChangeCommitted);
			// 
			// butAddSupply
			// 
			this.butAddSupply.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddSupply.Autosize = true;
			this.butAddSupply.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddSupply.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddSupply.CornerRadius = 4F;
			this.butAddSupply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddSupply.Location = new System.Drawing.Point(3,2);
			this.butAddSupply.Name = "butAddSupply";
			this.butAddSupply.Size = new System.Drawing.Size(97,26);
			this.butAddSupply.TabIndex = 6;
			this.butAddSupply.Text = "Manage Supplies";
			this.butAddSupply.Click += new System.EventHandler(this.butManageSupplies_Click);
			// 
			// gridSupply
			// 
			this.gridSupply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridSupply.HScrollVisible = false;
			this.gridSupply.Location = new System.Drawing.Point(3,30);
			this.gridSupply.Name = "gridSupply";
			this.gridSupply.ScrollValue = 0;
			this.gridSupply.Size = new System.Drawing.Size(750,298);
			this.gridSupply.TabIndex = 5;
			this.gridSupply.Title = "Supplies for Order";
			this.gridSupply.TranslationName = null;
			this.gridSupply.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridSupply_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(678,332);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSuppliers,
            this.menuItemCategories});
			this.menuStrip1.Location = new System.Drawing.Point(0,0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(757,24);
			this.menuStrip1.TabIndex = 12;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// menuItemSuppliers
			// 
			this.menuItemSuppliers.Name = "menuItemSuppliers";
			this.menuItemSuppliers.Size = new System.Drawing.Size(62,20);
			this.menuItemSuppliers.Text = "Suppliers";
			this.menuItemSuppliers.Click += new System.EventHandler(this.menuItemSuppliers_Click);
			// 
			// menuItemCategories
			// 
			this.menuItemCategories.Name = "menuItemCategories";
			this.menuItemCategories.Size = new System.Drawing.Size(71,20);
			this.menuItemCategories.Text = "Categories";
			this.menuItemCategories.Click += new System.EventHandler(this.menuItemCategories_Click);
			// 
			// FormSupplyInventory
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(757,607);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FormSupplyInventory";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Supply Inventory";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormInventory_Load);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private OpenDental.UI.ODGrid gridNeeded;
		private OpenDental.UI.Button butAddNeeded;
		private System.Windows.Forms.SplitContainer splitContainer;
		private OpenDental.UI.ODGrid gridSupply;
		private OpenDental.UI.Button butNewOrder;
		private OpenDental.UI.ODGrid gridOrder;
		private OpenDental.UI.Button butAddSupply;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboSupplier;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menuItemSuppliers;
		private System.Windows.Forms.ToolStripMenuItem menuItemCategories;
	}
}