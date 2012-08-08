namespace OpenDental{
	partial class FormHL7DefFieldEdit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHL7DefFieldEdit));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listFieldNames = new System.Windows.Forms.ListBox();
			this.comboDataType = new System.Windows.Forms.ComboBox();
			this.textTableId = new System.Windows.Forms.TextBox();
			this.labelItemOrder = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.labelDelete = new System.Windows.Forms.Label();
			this.textItemOrder = new OpenDental.ValidNum();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(228,433);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
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
			this.butCancel.Location = new System.Drawing.Point(314,433);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label1.Location = new System.Drawing.Point(11,57);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(125,18);
			this.label1.TabIndex = 14;
			this.label1.Text = "Item Order";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listFieldNames
			// 
			this.listFieldNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listFieldNames.FormattingEnabled = true;
			this.listFieldNames.Location = new System.Drawing.Point(136,80);
			this.listFieldNames.Name = "listFieldNames";
			this.listFieldNames.Size = new System.Drawing.Size(199,329);
			this.listFieldNames.TabIndex = 13;
			// 
			// comboDataType
			// 
			this.comboDataType.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.comboDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDataType.Location = new System.Drawing.Point(136,10);
			this.comboDataType.MaxDropDownItems = 100;
			this.comboDataType.Name = "comboDataType";
			this.comboDataType.Size = new System.Drawing.Size(138,21);
			this.comboDataType.TabIndex = 11;
			// 
			// textTableId
			// 
			this.textTableId.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.textTableId.Location = new System.Drawing.Point(136,34);
			this.textTableId.Name = "textTableId";
			this.textTableId.Size = new System.Drawing.Size(83,20);
			this.textTableId.TabIndex = 12;
			// 
			// labelItemOrder
			// 
			this.labelItemOrder.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.labelItemOrder.Location = new System.Drawing.Point(11,34);
			this.labelItemOrder.Name = "labelItemOrder";
			this.labelItemOrder.Size = new System.Drawing.Size(125,18);
			this.labelItemOrder.TabIndex = 8;
			this.labelItemOrder.Text = "Table ID";
			this.labelItemOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(11,80);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(125,18);
			this.label12.TabIndex = 9;
			this.label12.Text = "Field Name";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label10.Location = new System.Drawing.Point(11,9);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(125,18);
			this.label10.TabIndex = 10;
			this.label10.Text = "Data Type";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.butDelete.Location = new System.Drawing.Point(14,433);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(85,24);
			this.butDelete.TabIndex = 65;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// labelDelete
			// 
			this.labelDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelDelete.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelDelete.Location = new System.Drawing.Point(11,361);
			this.labelDelete.Name = "labelDelete";
			this.labelDelete.Size = new System.Drawing.Size(125,69);
			this.labelDelete.TabIndex = 66;
			this.labelDelete.Text = "This HL7Def is internal. To edit this HL7Def you must first copy it to the Custom" +
    " list.";
			this.labelDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelDelete.Visible = false;
			// 
			// textItemOrder
			// 
			this.textItemOrder.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.textItemOrder.Location = new System.Drawing.Point(136,57);
			this.textItemOrder.MaxVal = 255;
			this.textItemOrder.MinVal = 0;
			this.textItemOrder.Name = "textItemOrder";
			this.textItemOrder.Size = new System.Drawing.Size(34,20);
			this.textItemOrder.TabIndex = 15;
			// 
			// FormHL7DefFieldEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(405,473);
			this.Controls.Add(this.textItemOrder);
			this.Controls.Add(this.labelDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listFieldNames);
			this.Controls.Add(this.comboDataType);
			this.Controls.Add(this.textTableId);
			this.Controls.Add(this.labelItemOrder);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label10);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormHL7DefFieldEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HL7 Def Field Edit";
			this.Load += new System.EventHandler(this.FormHL7DefFieldEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listFieldNames;
		private System.Windows.Forms.ComboBox comboDataType;
		private System.Windows.Forms.TextBox textTableId;
		private System.Windows.Forms.Label labelItemOrder;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label10;
		private UI.Button butDelete;
		private System.Windows.Forms.Label labelDelete;
		private ValidNum textItemOrder;
	}
}
