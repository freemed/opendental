namespace OpenDental{
	partial class FormSupplyOrders {
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
			this.butCancel = new OpenDental.UI.Button();
			this.butNewOrder = new OpenDental.UI.Button();
			this.gridOrders = new OpenDental.UI.ODGrid();
			this.comboSupplier = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.gridOrderItem = new OpenDental.UI.ODGrid();
			this.butAddSupply = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
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
			this.butOK.Location = new System.Drawing.Point(561, 503);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(642, 503);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butNewOrder
			// 
			this.butNewOrder.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNewOrder.Autosize = true;
			this.butNewOrder.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNewOrder.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNewOrder.CornerRadius = 4F;
			this.butNewOrder.Image = global::OpenDental.Properties.Resources.Add;
			this.butNewOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butNewOrder.Location = new System.Drawing.Point(12, 7);
			this.butNewOrder.Name = "butNewOrder";
			this.butNewOrder.Size = new System.Drawing.Size(95, 24);
			this.butNewOrder.TabIndex = 16;
			this.butNewOrder.Text = "New Order";
			// 
			// gridOrder
			// 
			this.gridOrders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridOrders.HScrollVisible = false;
			this.gridOrders.Location = new System.Drawing.Point(12, 37);
			this.gridOrders.Name = "gridOrder";
			this.gridOrders.ScrollValue = 0;
			this.gridOrders.Size = new System.Drawing.Size(363, 184);
			this.gridOrders.TabIndex = 15;
			this.gridOrders.Title = "Order History";
			this.gridOrders.TranslationName = null;
			this.gridOrders.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridOrder_CellDoubleClick);
			this.gridOrders.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridOrder_CellClick);
			// 
			// comboSupplier
			// 
			this.comboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSupplier.FormattingEnabled = true;
			this.comboSupplier.Location = new System.Drawing.Point(231, 10);
			this.comboSupplier.Name = "comboSupplier";
			this.comboSupplier.Size = new System.Drawing.Size(144, 21);
			this.comboSupplier.TabIndex = 13;
			this.comboSupplier.SelectedIndexChanged += new System.EventHandler(this.comboSupplier_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(154, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 18);
			this.label3.TabIndex = 14;
			this.label3.Text = "Supplier";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridOrderItem
			// 
			this.gridOrderItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridOrderItem.HScrollVisible = false;
			this.gridOrderItem.Location = new System.Drawing.Point(12, 227);
			this.gridOrderItem.Name = "gridOrderItem";
			this.gridOrderItem.ScrollValue = 0;
			this.gridOrderItem.Size = new System.Drawing.Size(705, 270);
			this.gridOrderItem.TabIndex = 17;
			this.gridOrderItem.Title = "Supplies on one Order";
			this.gridOrderItem.TranslationName = null;
			this.gridOrderItem.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridOrderItem_CellDoubleClick);
			// 
			// butAddSupply
			// 
			this.butAddSupply.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddSupply.Autosize = true;
			this.butAddSupply.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddSupply.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddSupply.CornerRadius = 4F;
			this.butAddSupply.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddSupply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddSupply.Location = new System.Drawing.Point(648, 197);
			this.butAddSupply.Name = "butAddSupply";
			this.butAddSupply.Size = new System.Drawing.Size(69, 24);
			this.butAddSupply.TabIndex = 25;
			this.butAddSupply.Text = "Add";
			this.butAddSupply.Click += new System.EventHandler(this.butAddSupply_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(322, 502);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(80, 26);
			this.butPrint.TabIndex = 26;
			this.butPrint.Text = "Print";
			// 
			// FormSupplyOrders
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(725, 534);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butAddSupply);
			this.Controls.Add(this.gridOrderItem);
			this.Controls.Add(this.butNewOrder);
			this.Controls.Add(this.gridOrders);
			this.Controls.Add(this.comboSupplier);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormSupplyOrders";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Supply Orders";
			this.Load += new System.EventHandler(this.FormSupplyOrders_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private UI.Button butNewOrder;
		private UI.ODGrid gridOrders;
		private System.Windows.Forms.ComboBox comboSupplier;
		private System.Windows.Forms.Label label3;
		private UI.ODGrid gridOrderItem;
		private UI.Button butAddSupply;
		private UI.Button butPrint;
	}
}