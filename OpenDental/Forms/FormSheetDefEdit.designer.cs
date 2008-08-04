namespace OpenDental{
	partial class FormSheetDefEdit {
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
			this.textDescription = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelMain = new OpenDental.PanelGraphics();
			this.labelInternal = new System.Windows.Forms.Label();
			this.listFields = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupAddNew = new System.Windows.Forms.GroupBox();
			this.butAddCheckBox = new OpenDental.UI.Button();
			this.butAddRect = new OpenDental.UI.Button();
			this.butAddLine = new OpenDental.UI.Button();
			this.butAddImage = new OpenDental.UI.Button();
			this.butAddStaticText = new OpenDental.UI.Button();
			this.butAddInputField = new OpenDental.UI.Button();
			this.butAddOutputText = new OpenDental.UI.Button();
			this.butEdit = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.panel1.SuspendLayout();
			this.groupAddNew.SuspendLayout();
			this.SuspendLayout();
			// 
			// textDescription
			// 
			this.textDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textDescription.Location = new System.Drawing.Point(699,3);
			this.textDescription.Name = "textDescription";
			this.textDescription.ReadOnly = true;
			this.textDescription.Size = new System.Drawing.Size(144,20);
			this.textDescription.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.panelMain);
			this.panel1.Location = new System.Drawing.Point(3,3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(678,653);
			this.panel1.TabIndex = 81;
			// 
			// panelMain
			// 
			this.panelMain.BackColor = System.Drawing.Color.Transparent;
			this.panelMain.Location = new System.Drawing.Point(0,0);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(549,513);
			this.panelMain.TabIndex = 0;
			this.panelMain.TabStop = true;
			this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
			this.panelMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseMove);
			this.panelMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseDoubleClick);
			this.panelMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseDown);
			this.panelMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseUp);
			// 
			// labelInternal
			// 
			this.labelInternal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.labelInternal.Location = new System.Drawing.Point(720,553);
			this.labelInternal.Name = "labelInternal";
			this.labelInternal.Size = new System.Drawing.Size(128,46);
			this.labelInternal.TabIndex = 82;
			this.labelInternal.Text = "This is an internal sheet, so it may not be edited.  Make a copy instead.";
			this.labelInternal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listFields
			// 
			this.listFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listFields.FormattingEnabled = true;
			this.listFields.Location = new System.Drawing.Point(701,165);
			this.listFields.Name = "listFields";
			this.listFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listFields.Size = new System.Drawing.Size(142,355);
			this.listFields.TabIndex = 83;
			this.listFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listFields_MouseDoubleClick);
			this.listFields.Click += new System.EventHandler(this.listFields_Click);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(698,148);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(108,16);
			this.label2.TabIndex = 84;
			this.label2.Text = "Fields";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupAddNew
			// 
			this.groupAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupAddNew.Controls.Add(this.butAddCheckBox);
			this.groupAddNew.Controls.Add(this.butAddRect);
			this.groupAddNew.Controls.Add(this.butAddLine);
			this.groupAddNew.Controls.Add(this.butAddImage);
			this.groupAddNew.Controls.Add(this.butAddStaticText);
			this.groupAddNew.Controls.Add(this.butAddInputField);
			this.groupAddNew.Controls.Add(this.butAddOutputText);
			this.groupAddNew.Location = new System.Drawing.Point(699,48);
			this.groupAddNew.Name = "groupAddNew";
			this.groupAddNew.Size = new System.Drawing.Size(144,97);
			this.groupAddNew.TabIndex = 86;
			this.groupAddNew.TabStop = false;
			this.groupAddNew.Text = "Add new";
			// 
			// butAddCheckBox
			// 
			this.butAddCheckBox.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddCheckBox.Autosize = true;
			this.butAddCheckBox.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddCheckBox.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddCheckBox.CornerRadius = 4F;
			this.butAddCheckBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddCheckBox.Location = new System.Drawing.Point(3,75);
			this.butAddCheckBox.Name = "butAddCheckBox";
			this.butAddCheckBox.Size = new System.Drawing.Size(67,20);
			this.butAddCheckBox.TabIndex = 91;
			this.butAddCheckBox.TabStop = false;
			this.butAddCheckBox.Text = "CheckBox";
			this.butAddCheckBox.Click += new System.EventHandler(this.butAddCheckBox_Click);
			// 
			// butAddRect
			// 
			this.butAddRect.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddRect.Autosize = true;
			this.butAddRect.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddRect.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddRect.CornerRadius = 4F;
			this.butAddRect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddRect.Location = new System.Drawing.Point(70,55);
			this.butAddRect.Name = "butAddRect";
			this.butAddRect.Size = new System.Drawing.Size(67,20);
			this.butAddRect.TabIndex = 90;
			this.butAddRect.TabStop = false;
			this.butAddRect.Text = "Rectangle";
			this.butAddRect.Click += new System.EventHandler(this.butAddRect_Click);
			// 
			// butAddLine
			// 
			this.butAddLine.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddLine.Autosize = true;
			this.butAddLine.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddLine.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddLine.CornerRadius = 4F;
			this.butAddLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddLine.Location = new System.Drawing.Point(3,55);
			this.butAddLine.Name = "butAddLine";
			this.butAddLine.Size = new System.Drawing.Size(67,20);
			this.butAddLine.TabIndex = 89;
			this.butAddLine.TabStop = false;
			this.butAddLine.Text = "Line";
			this.butAddLine.Click += new System.EventHandler(this.butAddLine_Click);
			// 
			// butAddImage
			// 
			this.butAddImage.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddImage.Autosize = true;
			this.butAddImage.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddImage.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddImage.CornerRadius = 4F;
			this.butAddImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddImage.Location = new System.Drawing.Point(70,35);
			this.butAddImage.Name = "butAddImage";
			this.butAddImage.Size = new System.Drawing.Size(67,20);
			this.butAddImage.TabIndex = 88;
			this.butAddImage.TabStop = false;
			this.butAddImage.Text = "Image";
			this.butAddImage.Click += new System.EventHandler(this.butAddImage_Click);
			// 
			// butAddStaticText
			// 
			this.butAddStaticText.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddStaticText.Autosize = true;
			this.butAddStaticText.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddStaticText.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddStaticText.CornerRadius = 4F;
			this.butAddStaticText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddStaticText.Location = new System.Drawing.Point(70,15);
			this.butAddStaticText.Name = "butAddStaticText";
			this.butAddStaticText.Size = new System.Drawing.Size(67,20);
			this.butAddStaticText.TabIndex = 87;
			this.butAddStaticText.TabStop = false;
			this.butAddStaticText.Text = "StaticText";
			this.butAddStaticText.Click += new System.EventHandler(this.butAddStaticText_Click);
			// 
			// butAddInputField
			// 
			this.butAddInputField.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddInputField.Autosize = true;
			this.butAddInputField.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddInputField.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddInputField.CornerRadius = 4F;
			this.butAddInputField.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddInputField.Location = new System.Drawing.Point(3,35);
			this.butAddInputField.Name = "butAddInputField";
			this.butAddInputField.Size = new System.Drawing.Size(67,20);
			this.butAddInputField.TabIndex = 86;
			this.butAddInputField.TabStop = false;
			this.butAddInputField.Text = "InputField";
			this.butAddInputField.Click += new System.EventHandler(this.butAddInputField_Click);
			// 
			// butAddOutputText
			// 
			this.butAddOutputText.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddOutputText.Autosize = true;
			this.butAddOutputText.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddOutputText.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddOutputText.CornerRadius = 4F;
			this.butAddOutputText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddOutputText.Location = new System.Drawing.Point(3,15);
			this.butAddOutputText.Name = "butAddOutputText";
			this.butAddOutputText.Size = new System.Drawing.Size(67,20);
			this.butAddOutputText.TabIndex = 85;
			this.butAddOutputText.TabStop = false;
			this.butAddOutputText.Text = "OutputText";
			this.butAddOutputText.Click += new System.EventHandler(this.butAddOutputText_Click);
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butEdit.Autosize = true;
			this.butEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEdit.CornerRadius = 4F;
			this.butEdit.Location = new System.Drawing.Point(753,24);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(90,24);
			this.butEdit.TabIndex = 87;
			this.butEdit.TabStop = false;
			this.butEdit.Text = "Edit Properties";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(766,526);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(77,24);
			this.butDelete.TabIndex = 80;
			this.butDelete.TabStop = false;
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
			this.butOK.Location = new System.Drawing.Point(766,602);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(77,24);
			this.butOK.TabIndex = 3;
			this.butOK.TabStop = false;
			this.butOK.Text = "OK";
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
			this.butCancel.Location = new System.Drawing.Point(766,632);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(77,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.TabStop = false;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormSheetDefEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(850,664);
			this.Controls.Add(this.butEdit);
			this.Controls.Add(this.groupAddNew);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listFields);
			this.Controls.Add(this.labelInternal);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.DoubleBuffered = true;
			this.KeyPreview = true;
			this.Name = "FormSheetDefEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Sheet Def";
			this.Load += new System.EventHandler(this.FormSheetDefEdit_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormSheetDefEdit_KeyUp);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSheetDefEdit_KeyDown);
			this.panel1.ResumeLayout(false);
			this.groupAddNew.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textDescription;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Panel panel1;
		private OpenDental.PanelGraphics panelMain;
		private System.Windows.Forms.Label labelInternal;
		private System.Windows.Forms.ListBox listFields;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butAddOutputText;
		private System.Windows.Forms.GroupBox groupAddNew;
		private OpenDental.UI.Button butAddStaticText;
		private OpenDental.UI.Button butAddInputField;
		private OpenDental.UI.Button butEdit;
		private OpenDental.UI.Button butAddImage;
		private OpenDental.UI.Button butAddRect;
		private OpenDental.UI.Button butAddLine;
		private OpenDental.UI.Button butAddCheckBox;
	}
}