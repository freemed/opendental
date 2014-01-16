namespace OpenDental {
	partial class FormEhrMedicalOrders {
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
			this.butAddLabOrder = new System.Windows.Forms.Button();
			this.butAddRadOrder = new System.Windows.Forms.Button();
			this.butClose = new System.Windows.Forms.Button();
			this.checkBoxShowDiscontinued = new System.Windows.Forms.CheckBox();
			this.labelProv = new System.Windows.Forms.Label();
			this.gridMedOrders = new OpenDental.UI.ODGrid();
			this.butAddMedOrder = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butAddLabOrder
			// 
			this.butAddLabOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddLabOrder.Location = new System.Drawing.Point(634, 64);
			this.butAddLabOrder.Name = "butAddLabOrder";
			this.butAddLabOrder.Size = new System.Drawing.Size(75, 23);
			this.butAddLabOrder.TabIndex = 8;
			this.butAddLabOrder.Text = "Add Lab";
			this.butAddLabOrder.UseVisualStyleBackColor = true;
			this.butAddLabOrder.Click += new System.EventHandler(this.butAddLabOrder_Click);
			// 
			// butAddRadOrder
			// 
			this.butAddRadOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddRadOrder.Location = new System.Drawing.Point(634, 93);
			this.butAddRadOrder.Name = "butAddRadOrder";
			this.butAddRadOrder.Size = new System.Drawing.Size(75, 23);
			this.butAddRadOrder.TabIndex = 7;
			this.butAddRadOrder.Text = "Add Rad";
			this.butAddRadOrder.UseVisualStyleBackColor = true;
			this.butAddRadOrder.Click += new System.EventHandler(this.butAddRadOrder_Click);
			// 
			// butClose
			// 
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Location = new System.Drawing.Point(634, 367);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 23);
			this.butClose.TabIndex = 9;
			this.butClose.Text = "Close";
			this.butClose.UseVisualStyleBackColor = true;
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// checkBoxShowDiscontinued
			// 
			this.checkBoxShowDiscontinued.Location = new System.Drawing.Point(12, 12);
			this.checkBoxShowDiscontinued.Name = "checkBoxShowDiscontinued";
			this.checkBoxShowDiscontinued.Size = new System.Drawing.Size(532, 17);
			this.checkBoxShowDiscontinued.TabIndex = 10;
			this.checkBoxShowDiscontinued.Text = "Show discontinued orders and all medications";
			this.checkBoxShowDiscontinued.UseVisualStyleBackColor = true;
			this.checkBoxShowDiscontinued.Click += new System.EventHandler(this.checkBoxShowDiscontinued_Click);
			// 
			// labelProv
			// 
			this.labelProv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelProv.ForeColor = System.Drawing.Color.DarkRed;
			this.labelProv.Location = new System.Drawing.Point(467, 9);
			this.labelProv.Name = "labelProv";
			this.labelProv.Size = new System.Drawing.Size(242, 18);
			this.labelProv.TabIndex = 32;
			this.labelProv.Text = "ehr provider not logged on";
			this.labelProv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridMedOrders
			// 
			this.gridMedOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMedOrders.HScrollVisible = false;
			this.gridMedOrders.Location = new System.Drawing.Point(12, 35);
			this.gridMedOrders.Name = "gridMedOrders";
			this.gridMedOrders.ScrollValue = 0;
			this.gridMedOrders.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMedOrders.Size = new System.Drawing.Size(609, 326);
			this.gridMedOrders.TabIndex = 5;
			this.gridMedOrders.Title = "CPOE";
			this.gridMedOrders.TranslationName = null;
			this.gridMedOrders.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMedOrders_CellDoubleClick);
			// 
			// butAddMedOrder
			// 
			this.butAddMedOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddMedOrder.Location = new System.Drawing.Point(634, 35);
			this.butAddMedOrder.Name = "butAddMedOrder";
			this.butAddMedOrder.Size = new System.Drawing.Size(75, 23);
			this.butAddMedOrder.TabIndex = 33;
			this.butAddMedOrder.Text = "Add Med";
			this.butAddMedOrder.UseVisualStyleBackColor = true;
			this.butAddMedOrder.Click += new System.EventHandler(this.butAddMedOrder_Click);
			// 
			// FormEhrMedicalOrders
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(721, 402);
			this.Controls.Add(this.butAddMedOrder);
			this.Controls.Add(this.labelProv);
			this.Controls.Add(this.checkBoxShowDiscontinued);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.butAddLabOrder);
			this.Controls.Add(this.butAddRadOrder);
			this.Controls.Add(this.gridMedOrders);
			this.Name = "FormEhrMedicalOrders";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Computerized Provider Order Entry";
			this.Load += new System.EventHandler(this.FormMedicalOrders_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button butAddLabOrder;
		private System.Windows.Forms.Button butAddRadOrder;
		private OpenDental.UI.ODGrid gridMedOrders;
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.CheckBox checkBoxShowDiscontinued;
		private System.Windows.Forms.Label labelProv;
		private System.Windows.Forms.Button butAddMedOrder;
	}
}