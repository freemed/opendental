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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menuItemSuppliers = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCategories = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.butNewOrder = new OpenDental.UI.Button();
			this.gridOrder = new OpenDental.UI.ODGrid();
			this.butAddNeeded = new OpenDental.UI.Button();
			this.gridNeeded = new OpenDental.UI.ODGrid();
			this.comboSupplier = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabSupplies = new System.Windows.Forms.TabPage();
			this.checkShowHidden = new System.Windows.Forms.CheckBox();
			this.butAddToOrder = new OpenDental.UI.Button();
			this.butNewSupply = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butRefresh = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.textFind = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.gridSupplyMain = new OpenDental.UI.ODGrid();
			this.tabOrderItems = new System.Windows.Forms.TabPage();
			this.gridOrderItem = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.butPrint = new OpenDental.UI.Button();
			this.menuStrip1.SuspendLayout();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabSupplies.SuspendLayout();
			this.tabOrderItems.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSuppliers,
            this.menuItemCategories});
			this.menuStrip1.Location = new System.Drawing.Point(0,0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(763,24);
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
			this.splitContainer.Panel1.Controls.Add(this.comboSupplier);
			this.splitContainer.Panel1.Controls.Add(this.label3);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.butPrint);
			this.splitContainer.Panel2.Controls.Add(this.tabControl);
			this.splitContainer.Panel2.Controls.Add(this.butClose);
			this.splitContainer.Size = new System.Drawing.Size(763,569);
			this.splitContainer.SplitterDistance = 212;
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
			this.butNewOrder.Location = new System.Drawing.Point(3,2);
			this.butNewOrder.Name = "butNewOrder";
			this.butNewOrder.Size = new System.Drawing.Size(95,24);
			this.butNewOrder.TabIndex = 12;
			this.butNewOrder.Text = "New Order";
			this.butNewOrder.Click += new System.EventHandler(this.butNewOrder_Click);
			// 
			// gridOrder
			// 
			this.gridOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridOrder.HScrollVisible = false;
			this.gridOrder.Location = new System.Drawing.Point(3,27);
			this.gridOrder.Name = "gridOrder";
			this.gridOrder.ScrollValue = 0;
			this.gridOrder.Size = new System.Drawing.Size(372,184);
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
			this.butAddNeeded.Location = new System.Drawing.Point(381,2);
			this.butAddNeeded.Name = "butAddNeeded";
			this.butAddNeeded.Size = new System.Drawing.Size(71,24);
			this.butAddNeeded.TabIndex = 5;
			this.butAddNeeded.Text = "Add";
			this.butAddNeeded.Click += new System.EventHandler(this.butAddNeeded_Click);
			// 
			// gridNeeded
			// 
			this.gridNeeded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridNeeded.HScrollVisible = false;
			this.gridNeeded.Location = new System.Drawing.Point(381,27);
			this.gridNeeded.Name = "gridNeeded";
			this.gridNeeded.ScrollValue = 0;
			this.gridNeeded.Size = new System.Drawing.Size(379,184);
			this.gridNeeded.TabIndex = 4;
			this.gridNeeded.Title = "Supplies Needed";
			this.gridNeeded.TranslationName = null;
			this.gridNeeded.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridNeeded_CellDoubleClick);
			// 
			// comboSupplier
			// 
			this.comboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSupplier.FormattingEnabled = true;
			this.comboSupplier.Location = new System.Drawing.Point(231,4);
			this.comboSupplier.Name = "comboSupplier";
			this.comboSupplier.Size = new System.Drawing.Size(144,21);
			this.comboSupplier.TabIndex = 8;
			this.comboSupplier.SelectionChangeCommitted += new System.EventHandler(this.comboSupplier_SelectionChangeCommitted);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(154,6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75,18);
			this.label3.TabIndex = 10;
			this.label3.Text = "Supplier";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tabControl.Controls.Add(this.tabSupplies);
			this.tabControl.Controls.Add(this.tabOrderItems);
			this.tabControl.Location = new System.Drawing.Point(3,2);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(759,321);
			this.tabControl.TabIndex = 11;
			// 
			// tabSupplies
			// 
			this.tabSupplies.BackColor = System.Drawing.SystemColors.Control;
			this.tabSupplies.Controls.Add(this.checkShowHidden);
			this.tabSupplies.Controls.Add(this.butAddToOrder);
			this.tabSupplies.Controls.Add(this.butNewSupply);
			this.tabSupplies.Controls.Add(this.butUp);
			this.tabSupplies.Controls.Add(this.butRefresh);
			this.tabSupplies.Controls.Add(this.butDown);
			this.tabSupplies.Controls.Add(this.textFind);
			this.tabSupplies.Controls.Add(this.label2);
			this.tabSupplies.Controls.Add(this.gridSupplyMain);
			this.tabSupplies.Location = new System.Drawing.Point(4,22);
			this.tabSupplies.Name = "tabSupplies";
			this.tabSupplies.Padding = new System.Windows.Forms.Padding(3);
			this.tabSupplies.Size = new System.Drawing.Size(751,295);
			this.tabSupplies.TabIndex = 1;
			this.tabSupplies.Text = "Main Supply List";
			// 
			// checkShowHidden
			// 
			this.checkShowHidden.Location = new System.Drawing.Point(184,5);
			this.checkShowHidden.Name = "checkShowHidden";
			this.checkShowHidden.Size = new System.Drawing.Size(95,18);
			this.checkShowHidden.TabIndex = 25;
			this.checkShowHidden.Text = "Show Hidden";
			this.checkShowHidden.UseVisualStyleBackColor = true;
			this.checkShowHidden.Click += new System.EventHandler(this.checkShowHidden_Click);
			// 
			// butAddToOrder
			// 
			this.butAddToOrder.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddToOrder.Autosize = true;
			this.butAddToOrder.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddToOrder.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddToOrder.CornerRadius = 4F;
			this.butAddToOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddToOrder.Location = new System.Drawing.Point(71,0);
			this.butAddToOrder.Name = "butAddToOrder";
			this.butAddToOrder.Size = new System.Drawing.Size(85,24);
			this.butAddToOrder.TabIndex = 25;
			this.butAddToOrder.Text = "Add to Order";
			this.butAddToOrder.Click += new System.EventHandler(this.butAddToOrder_Click);
			// 
			// butNewSupply
			// 
			this.butNewSupply.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNewSupply.Autosize = true;
			this.butNewSupply.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNewSupply.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNewSupply.CornerRadius = 4F;
			this.butNewSupply.Image = global::OpenDental.Properties.Resources.Add;
			this.butNewSupply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butNewSupply.Location = new System.Drawing.Point(0,0);
			this.butNewSupply.Name = "butNewSupply";
			this.butNewSupply.Size = new System.Drawing.Size(69,24);
			this.butNewSupply.TabIndex = 24;
			this.butNewSupply.Text = "New";
			this.butNewSupply.Click += new System.EventHandler(this.butNewSupply_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.Location = new System.Drawing.Point(284,0);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(24,24);
			this.butUp.TabIndex = 15;
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butRefresh.Location = new System.Drawing.Point(455,0);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(64,24);
			this.butRefresh.TabIndex = 23;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.Location = new System.Drawing.Point(311,0);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(24,24);
			this.butDown.TabIndex = 16;
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// textFind
			// 
			this.textFind.Location = new System.Drawing.Point(385,3);
			this.textFind.Name = "textFind";
			this.textFind.Size = new System.Drawing.Size(67,20);
			this.textFind.TabIndex = 22;
			this.textFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textFind_KeyDown);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(334,3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50,18);
			this.label2.TabIndex = 21;
			this.label2.Text = "Find";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridSupplyMain
			// 
			this.gridSupplyMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridSupplyMain.HScrollVisible = false;
			this.gridSupplyMain.Location = new System.Drawing.Point(0,25);
			this.gridSupplyMain.Name = "gridSupplyMain";
			this.gridSupplyMain.ScrollValue = 0;
			this.gridSupplyMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridSupplyMain.Size = new System.Drawing.Size(750,270);
			this.gridSupplyMain.TabIndex = 11;
			this.gridSupplyMain.Title = "Supplies";
			this.gridSupplyMain.TranslationName = null;
			this.gridSupplyMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridSupplyMain_CellDoubleClick);
			// 
			// tabOrderItems
			// 
			this.tabOrderItems.BackColor = System.Drawing.SystemColors.Control;
			this.tabOrderItems.Controls.Add(this.gridOrderItem);
			this.tabOrderItems.Location = new System.Drawing.Point(4,22);
			this.tabOrderItems.Name = "tabOrderItems";
			this.tabOrderItems.Padding = new System.Windows.Forms.Padding(3);
			this.tabOrderItems.Size = new System.Drawing.Size(751,295);
			this.tabOrderItems.TabIndex = 0;
			this.tabOrderItems.Text = "Supplies on one Order";
			// 
			// gridOrderItem
			// 
			this.gridOrderItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridOrderItem.HScrollVisible = false;
			this.gridOrderItem.Location = new System.Drawing.Point(0,28);
			this.gridOrderItem.Name = "gridOrderItem";
			this.gridOrderItem.ScrollValue = 0;
			this.gridOrderItem.Size = new System.Drawing.Size(628,267);
			this.gridOrderItem.TabIndex = 5;
			this.gridOrderItem.Title = "Supplies on one Order";
			this.gridOrderItem.TranslationName = null;
			this.gridOrderItem.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridOrderItem_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(684,324);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(482,2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92,18);
			this.label1.TabIndex = 10;
			this.label1.Text = "Supplier";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(576,0);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(173,21);
			this.comboBox1.TabIndex = 8;
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(311,324);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(80,26);
			this.butPrint.TabIndex = 24;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// FormSupplyInventory
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(763,593);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FormSupplyInventory";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Supply Inventory";
			this.Load += new System.EventHandler(this.FormInventory_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabSupplies.ResumeLayout(false);
			this.tabSupplies.PerformLayout();
			this.tabOrderItems.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private OpenDental.UI.ODGrid gridNeeded;
		private OpenDental.UI.Button butAddNeeded;
		private System.Windows.Forms.SplitContainer splitContainer;
		private OpenDental.UI.ODGrid gridOrderItem;
		private OpenDental.UI.Button butNewOrder;
		private OpenDental.UI.ODGrid gridOrder;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboSupplier;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menuItemSuppliers;
		private System.Windows.Forms.ToolStripMenuItem menuItemCategories;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabSupplies;
		private System.Windows.Forms.TabPage tabOrderItems;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private OpenDental.UI.ODGrid gridSupplyMain;
		private OpenDental.UI.Button butUp;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butRefresh;
		private System.Windows.Forms.TextBox textFind;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkShowHidden;
		private OpenDental.UI.Button butNewSupply;
		private OpenDental.UI.Button butAddToOrder;
		private OpenDental.UI.Button butPrint;
	}
}