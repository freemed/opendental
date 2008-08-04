namespace OpenDental{
	partial class FormSheetFieldCheckBox {
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
			this.label2 = new System.Windows.Forms.Label();
			this.listFields = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.textHeight = new OpenDental.ValidNum();
			this.textWidth = new OpenDental.ValidNum();
			this.textYPos = new OpenDental.ValidNum();
			this.textXPos = new OpenDental.ValidNum();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13,18);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(108,16);
			this.label2.TabIndex = 86;
			this.label2.Text = "Field Name";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listFields
			// 
			this.listFields.FormattingEnabled = true;
			this.listFields.Location = new System.Drawing.Point(15,37);
			this.listFields.Name = "listFields";
			this.listFields.Size = new System.Drawing.Size(142,277);
			this.listFields.TabIndex = 85;
			this.listFields.DoubleClick += new System.EventHandler(this.listFields_DoubleClick);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(192,38);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(71,16);
			this.label5.TabIndex = 90;
			this.label5.Text = "X Pos";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(192,64);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(71,16);
			this.label6.TabIndex = 92;
			this.label6.Text = "Y Pos";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(192,90);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(71,16);
			this.label7.TabIndex = 94;
			this.label7.Text = "Width";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(192,116);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(71,16);
			this.label8.TabIndex = 96;
			this.label8.Text = "Height";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.butDelete.Location = new System.Drawing.Point(15,345);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(77,24);
			this.butDelete.TabIndex = 100;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textHeight
			// 
			this.textHeight.Location = new System.Drawing.Point(263,115);
			this.textHeight.MaxVal = 2000;
			this.textHeight.MinVal = 1;
			this.textHeight.Name = "textHeight";
			this.textHeight.Size = new System.Drawing.Size(69,20);
			this.textHeight.TabIndex = 97;
			// 
			// textWidth
			// 
			this.textWidth.Location = new System.Drawing.Point(263,89);
			this.textWidth.MaxVal = 2000;
			this.textWidth.MinVal = 1;
			this.textWidth.Name = "textWidth";
			this.textWidth.Size = new System.Drawing.Size(69,20);
			this.textWidth.TabIndex = 95;
			// 
			// textYPos
			// 
			this.textYPos.Location = new System.Drawing.Point(263,63);
			this.textYPos.MaxVal = 2000;
			this.textYPos.MinVal = -100;
			this.textYPos.Name = "textYPos";
			this.textYPos.Size = new System.Drawing.Size(69,20);
			this.textYPos.TabIndex = 93;
			// 
			// textXPos
			// 
			this.textXPos.Location = new System.Drawing.Point(263,37);
			this.textXPos.MaxVal = 2000;
			this.textXPos.MinVal = -100;
			this.textXPos.Name = "textXPos";
			this.textXPos.Size = new System.Drawing.Size(69,20);
			this.textXPos.TabIndex = 91;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(346,315);
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
			this.butCancel.Location = new System.Drawing.Point(346,345);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormSheetFieldCheckBox
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(433,381);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textHeight);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textWidth);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textYPos);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textXPos);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listFields);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormSheetFieldCheckBox";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit CheckBox";
			this.Load += new System.EventHandler(this.FormSheetFieldCheckBox_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listFields;
		private System.Windows.Forms.Label label5;
		private ValidNum textXPos;
		private ValidNum textYPos;
		private System.Windows.Forms.Label label6;
		private ValidNum textWidth;
		private System.Windows.Forms.Label label7;
		private ValidNum textHeight;
		private System.Windows.Forms.Label label8;
		private OpenDental.UI.Button butDelete;
	}
}