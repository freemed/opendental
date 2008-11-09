namespace OpenDental{
	partial class FormAnestheticMedsAdjQtys
	{
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnestheticMedsAdjQtys));
            this.labelAdjustQtys = new System.Windows.Forms.Label();
            this.groupBoxAdjQtys = new System.Windows.Forms.GroupBox();
            this.gridAnesthMedsAdjQty = new OpenDental.UI.ODGrid();
            this.butCancel = new OpenDental.UI.Button();
            this.butClose = new OpenDental.UI.Button();
            this.groupBoxAdjQtys.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAdjustQtys
            // 
            this.labelAdjustQtys.AutoSize = true;
            this.labelAdjustQtys.Location = new System.Drawing.Point(27, 16);
            this.labelAdjustQtys.Name = "labelAdjustQtys";
            this.labelAdjustQtys.Size = new System.Drawing.Size(472, 13);
            this.labelAdjustQtys.TabIndex = 143;
            this.labelAdjustQtys.Text = "You may adjust quantities by entering positive or negative values in the \"Quantit" +
                "y Adjustment\" Field";
            // 
            // groupBoxAdjQtys
            // 
            this.groupBoxAdjQtys.Controls.Add(this.gridAnesthMedsAdjQty);
            this.groupBoxAdjQtys.Controls.Add(this.labelAdjustQtys);
            this.groupBoxAdjQtys.Controls.Add(this.butCancel);
            this.groupBoxAdjQtys.Controls.Add(this.butClose);
            this.groupBoxAdjQtys.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAdjQtys.Name = "groupBoxAdjQtys";
            this.groupBoxAdjQtys.Size = new System.Drawing.Size(840, 410);
            this.groupBoxAdjQtys.TabIndex = 144;
            this.groupBoxAdjQtys.TabStop = false;
            this.groupBoxAdjQtys.Enter += new System.EventHandler(this.groupBoxAdjQtys_Enter);
            // 
            // gridAnesthMedsAdjQty
            // 
            this.gridAnesthMedsAdjQty.HScrollVisible = false;
            this.gridAnesthMedsAdjQty.Location = new System.Drawing.Point(17, 41);
            this.gridAnesthMedsAdjQty.Name = "gridAnesthMedsAdjQty";
            this.gridAnesthMedsAdjQty.ScrollValue = 0;
            this.gridAnesthMedsAdjQty.Size = new System.Drawing.Size(803, 307);
            this.gridAnesthMedsAdjQty.TabIndex = 145;
            this.gridAnesthMedsAdjQty.Title = "Anesthetic Medication Inventory";
            this.gridAnesthMedsAdjQty.TranslationName = "TableAnesthMedsInventory";
            this.gridAnesthMedsAdjQty.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAnesthMedsAdjQty_CellDoubleClick);
            // 
            // butCancel
            // 
            this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butCancel.Autosize = true;
            this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butCancel.CornerRadius = 4F;
            this.butCancel.Image = global::OpenDental.Properties.Resources.deleteX;
            this.butCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butCancel.Location = new System.Drawing.Point(653, 367);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(66, 26);
            this.butCancel.TabIndex = 141;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.button_Click);
            // 
            // butClose
            // 
            this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butClose.Autosize = true;
            this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butClose.CornerRadius = 4F;
            this.butClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butClose.Location = new System.Drawing.Point(725, 367);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(90, 26);
            this.butClose.TabIndex = 142;
            this.butClose.Text = "Save and Close";
            this.butClose.UseVisualStyleBackColor = true;
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // FormAnestheticMedsAdjQtys
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(869, 442);
            this.Controls.Add(this.groupBoxAdjQtys);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAnestheticMedsAdjQtys";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adjust Anesthetic Medication Inventory Quantities";
            this.Load += new System.EventHandler(this.FormAnestheticMedsAdjQtys_Load);
            this.groupBoxAdjQtys.ResumeLayout(false);
            this.groupBoxAdjQtys.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label labelAdjustQtys;
        private System.Windows.Forms.GroupBox groupBoxAdjQtys;
        private OpenDental.UI.ODGrid gridAnesthMedsAdjQty;
	}
}