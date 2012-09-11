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
			this.label3 = new System.Windows.Forms.Label();
			this.textRadioGroupName = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.checkRequired = new System.Windows.Forms.CheckBox();
			this.groupRadioMisc = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listRadio = new System.Windows.Forms.ListBox();
			this.groupRadio = new System.Windows.Forms.GroupBox();
			this.labelTabOrder = new System.Windows.Forms.Label();
			this.listMedical = new System.Windows.Forms.ListBox();
			this.labelMedical = new System.Windows.Forms.Label();
			this.radioYes = new System.Windows.Forms.RadioButton();
			this.radioNo = new System.Windows.Forms.RadioButton();
			this.textTabOrder = new OpenDental.ValidNum();
			this.butDelete = new OpenDental.UI.Button();
			this.textHeight = new OpenDental.ValidNum();
			this.textWidth = new OpenDental.ValidNum();
			this.textYPos = new OpenDental.ValidNum();
			this.textXPos = new OpenDental.ValidNum();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.labelRequired = new System.Windows.Forms.Label();
			this.textReportableName = new System.Windows.Forms.TextBox();
			this.labelReportableName = new System.Windows.Forms.Label();
			this.labelMiscInstructions = new System.Windows.Forms.Label();
			this.groupRadioMisc.SuspendLayout();
			this.groupRadio.SuspendLayout();
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
			this.listFields.Size = new System.Drawing.Size(142,472);
			this.listFields.TabIndex = 85;
			this.listFields.SelectedIndexChanged += new System.EventHandler(this.listFields_SelectedIndexChanged);
			this.listFields.DoubleClick += new System.EventHandler(this.listFields_DoubleClick);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(385,9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(71,16);
			this.label5.TabIndex = 90;
			this.label5.Text = "X Pos";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.Location = new System.Drawing.Point(385,32);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(71,16);
			this.label6.TabIndex = 92;
			this.label6.Text = "Y Pos";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.Location = new System.Drawing.Point(385,55);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(71,16);
			this.label7.TabIndex = 94;
			this.label7.Text = "Width";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.Location = new System.Drawing.Point(385,78);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(71,16);
			this.label8.TabIndex = 96;
			this.label8.Text = "Height";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(5,51);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89,16);
			this.label3.TabIndex = 103;
			this.label3.Text = "Group Name";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textRadioGroupName
			// 
			this.textRadioGroupName.Location = new System.Drawing.Point(94,50);
			this.textRadioGroupName.Name = "textRadioGroupName";
			this.textRadioGroupName.Size = new System.Drawing.Size(197,20);
			this.textRadioGroupName.TabIndex = 102;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(11,15);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(280,33);
			this.label9.TabIndex = 106;
			this.label9.Text = "Use the same Field Name (misc) and the same Group Name for each radio button in a" +
    " group.";
			// 
			// checkRequired
			// 
			this.checkRequired.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkRequired.Location = new System.Drawing.Point(373,184);
			this.checkRequired.Name = "checkRequired";
			this.checkRequired.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkRequired.Size = new System.Drawing.Size(97,17);
			this.checkRequired.TabIndex = 107;
			this.checkRequired.Text = "Required";
			this.checkRequired.UseVisualStyleBackColor = true;
			this.checkRequired.Visible = false;
			// 
			// groupRadioMisc
			// 
			this.groupRadioMisc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupRadioMisc.Controls.Add(this.label9);
			this.groupRadioMisc.Controls.Add(this.textRadioGroupName);
			this.groupRadioMisc.Controls.Add(this.label3);
			this.groupRadioMisc.Location = new System.Drawing.Point(362,100);
			this.groupRadioMisc.Name = "groupRadioMisc";
			this.groupRadioMisc.Size = new System.Drawing.Size(297,78);
			this.groupRadioMisc.TabIndex = 106;
			this.groupRadioMisc.TabStop = false;
			this.groupRadioMisc.Text = "Radio Button";
			this.groupRadioMisc.Visible = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8,20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(283,31);
			this.label1.TabIndex = 87;
			this.label1.Text = "Use the same Field Name for each radio button in a group.  But set a different Ra" +
    "dio Button Value for each.";
			// 
			// listRadio
			// 
			this.listRadio.FormattingEnabled = true;
			this.listRadio.Location = new System.Drawing.Point(94,56);
			this.listRadio.Name = "listRadio";
			this.listRadio.Size = new System.Drawing.Size(142,121);
			this.listRadio.TabIndex = 88;
			this.listRadio.Click += new System.EventHandler(this.listRadio_Click);
			// 
			// groupRadio
			// 
			this.groupRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupRadio.Controls.Add(this.listRadio);
			this.groupRadio.Controls.Add(this.label1);
			this.groupRadio.Location = new System.Drawing.Point(362,243);
			this.groupRadio.Name = "groupRadio";
			this.groupRadio.Size = new System.Drawing.Size(297,183);
			this.groupRadio.TabIndex = 101;
			this.groupRadio.TabStop = false;
			this.groupRadio.Text = "Radio Button Value";
			this.groupRadio.Visible = false;
			// 
			// labelTabOrder
			// 
			this.labelTabOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelTabOrder.Location = new System.Drawing.Point(385,491);
			this.labelTabOrder.Name = "labelTabOrder";
			this.labelTabOrder.Size = new System.Drawing.Size(71,16);
			this.labelTabOrder.TabIndex = 108;
			this.labelTabOrder.Text = "Tab Order";
			this.labelTabOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listMedical
			// 
			this.listMedical.FormattingEnabled = true;
			this.listMedical.Location = new System.Drawing.Point(186,37);
			this.listMedical.Name = "listMedical";
			this.listMedical.Size = new System.Drawing.Size(142,472);
			this.listMedical.TabIndex = 110;
			this.listMedical.Visible = false;
			this.listMedical.DoubleClick += new System.EventHandler(this.listMedical_DoubleClick);
			// 
			// labelMedical
			// 
			this.labelMedical.Location = new System.Drawing.Point(183,18);
			this.labelMedical.Name = "labelMedical";
			this.labelMedical.Size = new System.Drawing.Size(108,16);
			this.labelMedical.TabIndex = 111;
			this.labelMedical.Text = "labelMedical";
			this.labelMedical.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.labelMedical.Visible = false;
			// 
			// radioYes
			// 
			this.radioYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.radioYes.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioYes.Location = new System.Drawing.Point(409,203);
			this.radioYes.Name = "radioYes";
			this.radioYes.Size = new System.Drawing.Size(61,17);
			this.radioYes.TabIndex = 114;
			this.radioYes.TabStop = true;
			this.radioYes.Text = "Yes";
			this.radioYes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioYes.UseVisualStyleBackColor = true;
			this.radioYes.Visible = false;
			this.radioYes.Click += new System.EventHandler(this.radioYes_Click);
			// 
			// radioNo
			// 
			this.radioNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.radioNo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioNo.Location = new System.Drawing.Point(409,221);
			this.radioNo.Name = "radioNo";
			this.radioNo.Size = new System.Drawing.Size(61,17);
			this.radioNo.TabIndex = 115;
			this.radioNo.TabStop = true;
			this.radioNo.Text = "No";
			this.radioNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioNo.UseVisualStyleBackColor = true;
			this.radioNo.Visible = false;
			this.radioNo.Click += new System.EventHandler(this.radioNo_Click);
			// 
			// textTabOrder
			// 
			this.textTabOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textTabOrder.Location = new System.Drawing.Point(456,490);
			this.textTabOrder.MaxVal = 2000;
			this.textTabOrder.MinVal = -100;
			this.textTabOrder.Name = "textTabOrder";
			this.textTabOrder.Size = new System.Drawing.Size(69,20);
			this.textTabOrder.TabIndex = 109;
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
			this.butDelete.Location = new System.Drawing.Point(16,530);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(77,24);
			this.butDelete.TabIndex = 100;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textHeight
			// 
			this.textHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textHeight.Location = new System.Drawing.Point(456,77);
			this.textHeight.MaxVal = 2000;
			this.textHeight.MinVal = 1;
			this.textHeight.Name = "textHeight";
			this.textHeight.Size = new System.Drawing.Size(69,20);
			this.textHeight.TabIndex = 97;
			// 
			// textWidth
			// 
			this.textWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textWidth.Location = new System.Drawing.Point(456,54);
			this.textWidth.MaxVal = 2000;
			this.textWidth.MinVal = 1;
			this.textWidth.Name = "textWidth";
			this.textWidth.Size = new System.Drawing.Size(69,20);
			this.textWidth.TabIndex = 95;
			// 
			// textYPos
			// 
			this.textYPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textYPos.Location = new System.Drawing.Point(456,31);
			this.textYPos.MaxVal = 2000;
			this.textYPos.MinVal = -100;
			this.textYPos.Name = "textYPos";
			this.textYPos.Size = new System.Drawing.Size(69,20);
			this.textYPos.TabIndex = 93;
			// 
			// textXPos
			// 
			this.textXPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textXPos.Location = new System.Drawing.Point(456,8);
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
			this.butOK.Location = new System.Drawing.Point(503,530);
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
			this.butCancel.Location = new System.Drawing.Point(584,530);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// labelRequired
			// 
			this.labelRequired.Location = new System.Drawing.Point(480,184);
			this.labelRequired.Name = "labelRequired";
			this.labelRequired.Size = new System.Drawing.Size(177,56);
			this.labelRequired.TabIndex = 116;
			this.labelRequired.Text = "Radio buttons in a radio button group must all be marked required or all be marke" +
    "d not required.";
			// 
			// textReportableName
			// 
			this.textReportableName.Location = new System.Drawing.Point(456,466);
			this.textReportableName.Name = "textReportableName";
			this.textReportableName.Size = new System.Drawing.Size(197,20);
			this.textReportableName.TabIndex = 107;
			// 
			// labelReportableName
			// 
			this.labelReportableName.Location = new System.Drawing.Point(315,467);
			this.labelReportableName.Name = "labelReportableName";
			this.labelReportableName.Size = new System.Drawing.Size(141,16);
			this.labelReportableName.TabIndex = 108;
			this.labelReportableName.Text = "Reportable Name";
			this.labelReportableName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelMiscInstructions
			// 
			this.labelMiscInstructions.Location = new System.Drawing.Point(370,429);
			this.labelMiscInstructions.Name = "labelMiscInstructions";
			this.labelMiscInstructions.Size = new System.Drawing.Size(289,32);
			this.labelMiscInstructions.TabIndex = 117;
			this.labelMiscInstructions.Text = "To make misc radio buttons reportable, set a different Reportable Name for each b" +
    "utton in the group.";
			// 
			// FormSheetFieldCheckBox
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(675,568);
			this.Controls.Add(this.listMedical);
			this.Controls.Add(this.labelMiscInstructions);
			this.Controls.Add(this.textReportableName);
			this.Controls.Add(this.labelReportableName);
			this.Controls.Add(this.labelRequired);
			this.Controls.Add(this.radioNo);
			this.Controls.Add(this.radioYes);
			this.Controls.Add(this.labelMedical);
			this.Controls.Add(this.textTabOrder);
			this.Controls.Add(this.labelTabOrder);
			this.Controls.Add(this.checkRequired);
			this.Controls.Add(this.groupRadioMisc);
			this.Controls.Add(this.groupRadio);
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
			this.groupRadioMisc.ResumeLayout(false);
			this.groupRadioMisc.PerformLayout();
			this.groupRadio.ResumeLayout(false);
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
		private System.Windows.Forms.Label label8;
		private ValidNum textHeight;
		private UI.Button butDelete;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textRadioGroupName;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox checkRequired;
		private System.Windows.Forms.GroupBox groupRadioMisc;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listRadio;
		private System.Windows.Forms.GroupBox groupRadio;
		private ValidNum textTabOrder;
		private System.Windows.Forms.Label labelTabOrder;
		private System.Windows.Forms.ListBox listMedical;
		private System.Windows.Forms.Label labelMedical;
		private System.Windows.Forms.RadioButton radioYes;
		private System.Windows.Forms.RadioButton radioNo;
		private System.Windows.Forms.Label labelRequired;
		private System.Windows.Forms.TextBox textReportableName;
		private System.Windows.Forms.Label labelReportableName;
		private System.Windows.Forms.Label labelMiscInstructions;
	}
}