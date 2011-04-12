namespace OpenDental{
	partial class FormMedicationReconcile {
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkDiscontinued = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.pictBox = new System.Windows.Forms.PictureBox();
			this.gridMeds = new OpenDental.UI.ODGrid();
			this.butPickRxListImage = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictBox)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(112,5);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(272,20);
			this.textBox1.TabIndex = 71;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94,16);
			this.label1.TabIndex = 73;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkDiscontinued
			// 
			this.checkDiscontinued.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.checkDiscontinued.Location = new System.Drawing.Point(608,5);
			this.checkDiscontinued.Name = "checkDiscontinued";
			this.checkDiscontinued.Size = new System.Drawing.Size(212,23);
			this.checkDiscontinued.TabIndex = 70;
			this.checkDiscontinued.Tag = "";
			this.checkDiscontinued.Text = "Show Discontinued Medications";
			this.checkDiscontinued.UseVisualStyleBackColor = true;
			this.checkDiscontinued.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkDiscontinued_KeyUp);
			this.checkDiscontinued.MouseUp += new System.Windows.Forms.MouseEventHandler(this.checkDiscontinued_MouseUp);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,50F));
			this.tableLayoutPanel1.Controls.Add(this.pictBox,0,0);
			this.tableLayoutPanel1.Controls.Add(this.gridMeds,1,0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(7,30);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent,50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(914,569);
			this.tableLayoutPanel1.TabIndex = 69;
			// 
			// pictBox
			// 
			this.pictBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictBox.BackColor = System.Drawing.SystemColors.Window;
			this.pictBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pictBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictBox.InitialImage = null;
			this.pictBox.Location = new System.Drawing.Point(3,3);
			this.pictBox.Name = "pictBox";
			this.pictBox.Size = new System.Drawing.Size(451,563);
			this.pictBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictBox.TabIndex = 66;
			this.pictBox.TabStop = false;
			// 
			// gridMeds
			// 
			this.gridMeds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMeds.AutoSize = true;
			this.gridMeds.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.gridMeds.HScrollVisible = false;
			this.gridMeds.Location = new System.Drawing.Point(460,3);
			this.gridMeds.Name = "gridMeds";
			this.gridMeds.ScrollValue = 0;
			this.gridMeds.Size = new System.Drawing.Size(451,563);
			this.gridMeds.TabIndex = 65;
			this.gridMeds.Title = "Medications";
			this.gridMeds.TranslationName = "TableMedications";
			// 
			// butPickRxListImage
			// 
			this.butPickRxListImage.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPickRxListImage.Autosize = true;
			this.butPickRxListImage.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickRxListImage.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickRxListImage.CornerRadius = 4F;
			this.butPickRxListImage.Location = new System.Drawing.Point(390,3);
			this.butPickRxListImage.Name = "butPickRxListImage";
			this.butPickRxListImage.Size = new System.Drawing.Size(22,24);
			this.butPickRxListImage.TabIndex = 76;
			this.butPickRxListImage.Text = "...";
			this.butPickRxListImage.Click += new System.EventHandler(this.butPickRxListImage_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(467,4);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(123,23);
			this.butAdd.TabIndex = 75;
			this.butAdd.Text = "&Add Medication";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(843,605);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormMedicationReconcile
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(923,641);
			this.Controls.Add(this.butPickRxListImage);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkDiscontinued);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.butClose);
			this.Name = "FormMedicationReconcile";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reconcile Medications";
			this.Load += new System.EventHandler(this.BasicTemplate_Load);
			this.Resize += new System.EventHandler(this.FormMedicationReconcile_Resize);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkDiscontinued;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.PictureBox pictBox;
		private UI.ODGrid gridMeds;
		private UI.Button butAdd;
		private UI.Button butPickRxListImage;
	}
}