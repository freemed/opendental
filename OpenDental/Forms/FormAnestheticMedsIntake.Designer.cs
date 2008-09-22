namespace OpenDental
{
	partial class FormAnestheticMedsIntake
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnestheticMedsIntake));
			this.textQty = new System.Windows.Forms.TextBox();
			this.labelQty = new System.Windows.Forms.Label();
			this.comboSupplier = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupSupplier = new System.Windows.Forms.GroupBox();
			this.labelInvoice = new System.Windows.Forms.Label();
			this.textDate = new System.Windows.Forms.TextBox();
			this.labelDate = new System.Windows.Forms.Label();
			this.butClose = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupSupplier.SuspendLayout();
			this.SuspendLayout();
			// 
			// textQty
			// 
			this.textQty.Location = new System.Drawing.Point(247, 20);
			this.textQty.Name = "textQty";
			this.textQty.Size = new System.Drawing.Size(219, 20);
			this.textQty.TabIndex = 2;
			// 
			// labelQty
			// 
			this.labelQty.AutoSize = true;
			this.labelQty.Location = new System.Drawing.Point(246, 0);
			this.labelQty.Name = "labelQty";
			this.labelQty.Size = new System.Drawing.Size(46, 13);
			this.labelQty.TabIndex = 4;
			this.labelQty.Text = "Quantity";
			// 
			// comboSupplier
			// 
			this.comboSupplier.FormattingEnabled = true;
			this.comboSupplier.Items.AddRange(new object[] {
            "Add new..."});
			this.comboSupplier.Location = new System.Drawing.Point(42, 110);
			this.comboSupplier.Name = "comboSupplier";
			this.comboSupplier.Size = new System.Drawing.Size(229, 21);
			this.comboSupplier.TabIndex = 5;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboBox1);
			this.groupBox1.Controls.Add(this.textQty);
			this.groupBox1.Controls.Add(this.labelQty);
			this.groupBox1.Location = new System.Drawing.Point(30, 39);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(482, 48);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Anesthetic Medication";
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(10, 19);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(231, 21);
			this.comboBox1.TabIndex = 0;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(280, 110);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(216, 20);
			this.textBox1.TabIndex = 7;
			// 
			// groupSupplier
			// 
			this.groupSupplier.Controls.Add(this.butClose);
			this.groupSupplier.Controls.Add(this.butCancel);
			this.groupSupplier.Controls.Add(this.labelInvoice);
			this.groupSupplier.Location = new System.Drawing.Point(30, 93);
			this.groupSupplier.Name = "groupSupplier";
			this.groupSupplier.Size = new System.Drawing.Size(482, 95);
			this.groupSupplier.TabIndex = 8;
			this.groupSupplier.TabStop = false;
			this.groupSupplier.Text = "Supplier";
			// 
			// labelInvoice
			// 
			this.labelInvoice.AutoSize = true;
			this.labelInvoice.Location = new System.Drawing.Point(247, 1);
			this.labelInvoice.Name = "labelInvoice";
			this.labelInvoice.Size = new System.Drawing.Size(52, 13);
			this.labelInvoice.TabIndex = 9;
			this.labelInvoice.Text = "Invoice #";
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(73, 12);
			this.textDate.Name = "textDate";
			this.textDate.ReadOnly = true;
			this.textDate.Size = new System.Drawing.Size(115, 20);
			this.textDate.TabIndex = 9;
			this.textDate.TextChanged += new System.EventHandler(this.textDate_TextChanged_1);
			// 
			// labelDate
			// 
			this.labelDate.AutoSize = true;
			this.labelDate.Location = new System.Drawing.Point(37, 15);
			this.labelDate.Name = "labelDate";
			this.labelDate.Size = new System.Drawing.Size(30, 13);
			this.labelDate.TabIndex = 10;
			this.labelDate.Text = "Date";
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClose.Location = new System.Drawing.Point(366, 53);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(90, 26);
			this.butClose.TabIndex = 137;
			this.butClose.Text = "Save and Close";
			this.butClose.UseVisualStyleBackColor = true;
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
			this.butCancel.Location = new System.Drawing.Point(294, 53);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(66, 26);
			this.butCancel.TabIndex = 54;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormAnestheticMedsIntake
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(532, 209);
			this.Controls.Add(this.labelDate);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.comboSupplier);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupSupplier);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormAnestheticMedsIntake";
			this.Text = "Anesthetic Medication Intake Form";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupSupplier.ResumeLayout(false);
			this.groupSupplier.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textQty;
		private System.Windows.Forms.Label labelQty;
		private System.Windows.Forms.ComboBox comboSupplier;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.GroupBox groupSupplier;
		private System.Windows.Forms.Label labelInvoice;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textDate;
		private System.Windows.Forms.Label labelDate;
		private System.Windows.Forms.ComboBox comboBox1;
	}
}