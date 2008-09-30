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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnestheticMedsAdjQtys));
			this.gridAnesthMedsAdjQtys = new System.Windows.Forms.DataGridView();
			this.AnestheticMed = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.HowSupplied = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.QtyOnHand = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.QtyAdjustment = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.labelAdjustQtys = new System.Windows.Forms.Label();
			this.groupBoxAdjQtys = new System.Windows.Forms.GroupBox();
			this.butCancel = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			((System.ComponentModel.ISupportInitialize)(this.gridAnesthMedsAdjQtys)).BeginInit();
			this.groupBoxAdjQtys.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridAnesthMedsAdjQtys
			// 
			this.gridAnesthMedsAdjQtys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridAnesthMedsAdjQtys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AnestheticMed,
            this.HowSupplied,
            this.QtyOnHand,
            this.QtyAdjustment,
            this.Notes});
			this.gridAnesthMedsAdjQtys.Location = new System.Drawing.Point(12, 43);
			this.gridAnesthMedsAdjQtys.Name = "gridAnesthMedsAdjQtys";
			this.gridAnesthMedsAdjQtys.Size = new System.Drawing.Size(803, 307);
			this.gridAnesthMedsAdjQtys.TabIndex = 4;
			this.gridAnesthMedsAdjQtys.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAnesthMeds_CellContentClick);
			// 
			// AnestheticMed
			// 
			this.AnestheticMed.HeaderText = "Anesthetic medication";
			this.AnestheticMed.Name = "AnestheticMed";
			this.AnestheticMed.ReadOnly = true;
			this.AnestheticMed.Width = 240;
			// 
			// HowSupplied
			// 
			this.HowSupplied.HeaderText = "How supplied";
			this.HowSupplied.Name = "HowSupplied";
			this.HowSupplied.ReadOnly = true;
			this.HowSupplied.Width = 160;
			// 
			// QtyOnHand
			// 
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.QtyOnHand.DefaultCellStyle = dataGridViewCellStyle1;
			this.QtyOnHand.HeaderText = "Quantity on hand (mLs)";
			this.QtyOnHand.Name = "QtyOnHand";
			this.QtyOnHand.ReadOnly = true;
			this.QtyOnHand.Width = 80;
			// 
			// QtyAdjustment
			// 
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.QtyAdjustment.DefaultCellStyle = dataGridViewCellStyle2;
			this.QtyAdjustment.HeaderText = "Quantity Adjustment (mLs)";
			this.QtyAdjustment.Name = "QtyAdjustment";
			// 
			// Notes
			// 
			this.Notes.HeaderText = "Notes";
			this.Notes.Name = "Notes";
			this.Notes.Width = 200;
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
			this.groupBoxAdjQtys.Controls.Add(this.gridAnesthMedsAdjQtys);
			this.groupBoxAdjQtys.Controls.Add(this.labelAdjustQtys);
			this.groupBoxAdjQtys.Controls.Add(this.butCancel);
			this.groupBoxAdjQtys.Controls.Add(this.butClose);
			this.groupBoxAdjQtys.Location = new System.Drawing.Point(12, 12);
			this.groupBoxAdjQtys.Name = "groupBoxAdjQtys";
			this.groupBoxAdjQtys.Size = new System.Drawing.Size(832, 410);
			this.groupBoxAdjQtys.TabIndex = 144;
			this.groupBoxAdjQtys.TabStop = false;
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
			this.ClientSize = new System.Drawing.Size(864, 442);
			this.Controls.Add(this.groupBoxAdjQtys);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormAnestheticMedsAdjQtys";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Adjust Anesthetic Medication Inventory Quantities";
			this.Load += new System.EventHandler(this.BasicTemplate_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridAnesthMedsAdjQtys)).EndInit();
			this.groupBoxAdjQtys.ResumeLayout(false);
			this.groupBoxAdjQtys.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gridAnesthMedsAdjQtys;
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label labelAdjustQtys;
		private System.Windows.Forms.GroupBox groupBoxAdjQtys;
		private System.Windows.Forms.DataGridViewTextBoxColumn AnestheticMed;
		private System.Windows.Forms.DataGridViewTextBoxColumn HowSupplied;
		private System.Windows.Forms.DataGridViewTextBoxColumn QtyOnHand;
		private System.Windows.Forms.DataGridViewTextBoxColumn QtyAdjustment;
		private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
	}
}