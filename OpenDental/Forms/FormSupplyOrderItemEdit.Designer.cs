namespace OpenDental{
	partial class FormSupplyOrderItemEdit {
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
			this.label1 = new System.Windows.Forms.Label();
			this.textSupplier = new System.Windows.Forms.TextBox();
			this.textCatalogNumber = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textDescript = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.textPrice = new OpenDental.ValidDouble();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textQty = new OpenDental.ValidNumber();
			this.textCategory = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textSubtotal = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(31,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(132,18);
			this.label1.TabIndex = 4;
			this.label1.Text = "Supplier";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSupplier
			// 
			this.textSupplier.Location = new System.Drawing.Point(166,8);
			this.textSupplier.Name = "textSupplier";
			this.textSupplier.ReadOnly = true;
			this.textSupplier.Size = new System.Drawing.Size(295,20);
			this.textSupplier.TabIndex = 10;
			// 
			// textCatalogNumber
			// 
			this.textCatalogNumber.Location = new System.Drawing.Point(166,61);
			this.textCatalogNumber.Name = "textCatalogNumber";
			this.textCatalogNumber.ReadOnly = true;
			this.textCatalogNumber.Size = new System.Drawing.Size(144,20);
			this.textCatalogNumber.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7,62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(156,18);
			this.label2.TabIndex = 8;
			this.label2.Text = "Catalog Item Number";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescript
			// 
			this.textDescript.Location = new System.Drawing.Point(166,87);
			this.textDescript.MaxLength = 255;
			this.textDescript.Name = "textDescript";
			this.textDescript.ReadOnly = true;
			this.textDescript.Size = new System.Drawing.Size(401,20);
			this.textDescript.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6,88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(157,18);
			this.label3.TabIndex = 10;
			this.label3.Text = "Description";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(31,35);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(132,18);
			this.label5.TabIndex = 14;
			this.label5.Text = "Category";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(32,113);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(132,18);
			this.label6.TabIndex = 16;
			this.label6.Text = "Quantity";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(32,139);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(132,18);
			this.label8.TabIndex = 20;
			this.label8.Text = "Price";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPrice
			// 
			this.textPrice.BackColor = System.Drawing.SystemColors.Window;
			this.textPrice.ForeColor = System.Drawing.SystemColors.WindowText;
			this.textPrice.Location = new System.Drawing.Point(166,139);
			this.textPrice.Name = "textPrice";
			this.textPrice.Size = new System.Drawing.Size(80,20);
			this.textPrice.TabIndex = 1;
			this.textPrice.TextChanged += new System.EventHandler(this.textPrice_TextChanged);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(27,218);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81,26);
			this.butDelete.TabIndex = 6;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(582,177);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(582,218);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textQty
			// 
			this.textQty.Location = new System.Drawing.Point(166,113);
			this.textQty.MaxVal = 10000;
			this.textQty.MinVal = 0;
			this.textQty.Name = "textQty";
			this.textQty.Size = new System.Drawing.Size(56,20);
			this.textQty.TabIndex = 0;
			this.textQty.TextChanged += new System.EventHandler(this.textQty_TextChanged);
			// 
			// textCategory
			// 
			this.textCategory.Location = new System.Drawing.Point(166,35);
			this.textCategory.Name = "textCategory";
			this.textCategory.ReadOnly = true;
			this.textCategory.Size = new System.Drawing.Size(225,20);
			this.textCategory.TabIndex = 26;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(32,165);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(132,18);
			this.label4.TabIndex = 28;
			this.label4.Text = "Subtotal";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSubtotal
			// 
			this.textSubtotal.Location = new System.Drawing.Point(166,165);
			this.textSubtotal.Name = "textSubtotal";
			this.textSubtotal.ReadOnly = true;
			this.textSubtotal.Size = new System.Drawing.Size(80,20);
			this.textSubtotal.TabIndex = 29;
			// 
			// FormSupplyOrderItemEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(682,269);
			this.Controls.Add(this.textSubtotal);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textCategory);
			this.Controls.Add(this.textQty);
			this.Controls.Add(this.textPrice);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textDescript);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textCatalogNumber);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textSupplier);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormSupplyOrderItemEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Supply Order Item";
			this.Load += new System.EventHandler(this.FormSupplyOrderItemEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textSupplier;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.TextBox textCatalogNumber;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textDescript;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private ValidDouble textPrice;
		private ValidNumber textQty;
		private System.Windows.Forms.TextBox textCategory;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textSubtotal;
	}
}