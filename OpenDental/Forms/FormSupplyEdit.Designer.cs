namespace OpenDental{
	partial class FormSupplyEdit {
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
			this.textCatalogDescript = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textCommonName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textUnitType = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.checkIsHidden = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.comboCategory = new System.Windows.Forms.ComboBox();
			this.textLevelDesired = new OpenDental.ValidDouble();
			this.textPrice = new OpenDental.ValidDouble();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
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
			// textCatalogDescript
			// 
			this.textCatalogDescript.Location = new System.Drawing.Point(166,87);
			this.textCatalogDescript.MaxLength = 255;
			this.textCatalogDescript.Name = "textCatalogDescript";
			this.textCatalogDescript.Size = new System.Drawing.Size(401,20);
			this.textCatalogDescript.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6,88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(157,18);
			this.label3.TabIndex = 10;
			this.label3.Text = "Catalog Description";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCommonName
			// 
			this.textCommonName.Location = new System.Drawing.Point(166,113);
			this.textCommonName.Name = "textCommonName";
			this.textCommonName.Size = new System.Drawing.Size(401,20);
			this.textCommonName.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8,114);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(155,18);
			this.label4.TabIndex = 12;
			this.label4.Text = "Common Name (optional)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			// textUnitType
			// 
			this.textUnitType.Location = new System.Drawing.Point(166,139);
			this.textUnitType.Name = "textUnitType";
			this.textUnitType.Size = new System.Drawing.Size(80,20);
			this.textUnitType.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(32,165);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(132,18);
			this.label6.TabIndex = 16;
			this.label6.Text = "Level Desired";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(166,217);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(401,60);
			this.textNote.TabIndex = 6;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(33,216);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(132,18);
			this.label7.TabIndex = 18;
			this.label7.Text = "Note";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIsHidden
			// 
			this.checkIsHidden.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsHidden.Location = new System.Drawing.Point(76,282);
			this.checkIsHidden.Name = "checkIsHidden";
			this.checkIsHidden.Size = new System.Drawing.Size(104,18);
			this.checkIsHidden.TabIndex = 7;
			this.checkIsHidden.Text = "Hidden";
			this.checkIsHidden.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsHidden.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(32,191);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(132,18);
			this.label8.TabIndex = 20;
			this.label8.Text = "Price";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(31,139);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(132,18);
			this.label9.TabIndex = 21;
			this.label9.Text = "Unit Type";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(228,168);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(173,19);
			this.label12.TabIndex = 24;
			this.label12.Text = "Decimals allowed.";
			// 
			// comboCategory
			// 
			this.comboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCategory.FormattingEnabled = true;
			this.comboCategory.Location = new System.Drawing.Point(166,34);
			this.comboCategory.Name = "comboCategory";
			this.comboCategory.Size = new System.Drawing.Size(228,21);
			this.comboCategory.TabIndex = 11;
			// 
			// textLevelDesired
			// 
			this.textLevelDesired.BackColor = System.Drawing.SystemColors.Window;
			this.textLevelDesired.ForeColor = System.Drawing.SystemColors.WindowText;
			this.textLevelDesired.Location = new System.Drawing.Point(166,165);
			this.textLevelDesired.Name = "textLevelDesired";
			this.textLevelDesired.Size = new System.Drawing.Size(62,20);
			this.textLevelDesired.TabIndex = 4;
			// 
			// textPrice
			// 
			this.textPrice.BackColor = System.Drawing.SystemColors.Window;
			this.textPrice.ForeColor = System.Drawing.SystemColors.WindowText;
			this.textPrice.Location = new System.Drawing.Point(166,191);
			this.textPrice.Name = "textPrice";
			this.textPrice.Size = new System.Drawing.Size(80,20);
			this.textPrice.TabIndex = 5;
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
			this.butDelete.Location = new System.Drawing.Point(27,370);
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
			this.butOK.Location = new System.Drawing.Point(582,329);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 8;
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
			this.butCancel.Location = new System.Drawing.Point(582,370);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormSupplyEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(682,421);
			this.Controls.Add(this.textLevelDesired);
			this.Controls.Add(this.comboCategory);
			this.Controls.Add(this.textPrice);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.checkIsHidden);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textUnitType);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textCommonName);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textCatalogDescript);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textCatalogNumber);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textSupplier);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormSupplyEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Supplier";
			this.Load += new System.EventHandler(this.FormSupplyEdit_Load);
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
		private System.Windows.Forms.TextBox textCatalogDescript;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textCommonName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textUnitType;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox checkIsHidden;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label12;
		private ValidDouble textPrice;
		private System.Windows.Forms.ComboBox comboCategory;
		private ValidDouble textLevelDesired;
	}
}