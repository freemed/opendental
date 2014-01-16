namespace OpenDental {
	partial class FormEhrLabOrderEdit2014 {
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
			this.butCancel = new System.Windows.Forms.Button();
			this.butOk = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.butAdd = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.textName = new System.Windows.Forms.TextBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.comboProvLang = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textProvID = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textProvName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.comboUnits = new System.Windows.Forms.ComboBox();
			this.butProvSelect = new OpenDental.UI.Button();
			this.gridClinicalInformation = new OpenDental.UI.ODGrid();
			this.gridNotes = new OpenDental.UI.ODGrid();
			this.gridSpecimen = new OpenDental.UI.ODGrid();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button4 = new OpenDental.UI.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox6.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(836, 606);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOk
			// 
			this.butOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOk.Location = new System.Drawing.Point(755, 606);
			this.butOk.Name = "butOk";
			this.butOk.Size = new System.Drawing.Size(75, 24);
			this.butOk.TabIndex = 2;
			this.butOk.Text = "OK";
			this.butOk.UseVisualStyleBackColor = true;
			this.butOk.Click += new System.EventHandler(this.butOk_Click);
			// 
			// butDelete
			// 
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Location = new System.Drawing.Point(12, 606);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 24);
			this.butDelete.TabIndex = 3;
			this.butDelete.Text = "Delete";
			this.butDelete.UseVisualStyleBackColor = true;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butAdd
			// 
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Location = new System.Drawing.Point(533, 429);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(81, 24);
			this.butAdd.TabIndex = 13;
			this.butAdd.Text = "Add Result";
			this.butAdd.UseVisualStyleBackColor = true;
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 18);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 17);
			this.label5.TabIndex = 15;
			this.label5.Text = "Patient";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textName
			// 
			this.textName.Location = new System.Drawing.Point(111, 16);
			this.textName.Name = "textName";
			this.textName.ReadOnly = true;
			this.textName.Size = new System.Drawing.Size(241, 20);
			this.textName.TabIndex = 14;
			this.textName.WordWrap = false;
			// 
			// groupBox6
			// 
			this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox6.Controls.Add(this.butProvSelect);
			this.groupBox6.Controls.Add(this.comboProvLang);
			this.groupBox6.Controls.Add(this.label7);
			this.groupBox6.Controls.Add(this.textProvID);
			this.groupBox6.Controls.Add(this.label6);
			this.groupBox6.Controls.Add(this.textProvName);
			this.groupBox6.Controls.Add(this.label1);
			this.groupBox6.Location = new System.Drawing.Point(547, 175);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(273, 88);
			this.groupBox6.TabIndex = 165;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Provider";
			// 
			// comboProvLang
			// 
			this.comboProvLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvLang.Location = new System.Drawing.Point(124, 61);
			this.comboProvLang.MaxDropDownItems = 30;
			this.comboProvLang.Name = "comboProvLang";
			this.comboProvLang.Size = new System.Drawing.Size(137, 21);
			this.comboProvLang.TabIndex = 171;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(27, 65);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(94, 16);
			this.label7.TabIndex = 170;
			this.label7.Text = "Language";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textProvID
			// 
			this.textProvID.Location = new System.Drawing.Point(124, 38);
			this.textProvID.Name = "textProvID";
			this.textProvID.Size = new System.Drawing.Size(137, 20);
			this.textProvID.TabIndex = 167;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(27, 42);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(94, 16);
			this.label6.TabIndex = 168;
			this.label6.Text = "Provider ID";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textProvName
			// 
			this.textProvName.Location = new System.Drawing.Point(124, 15);
			this.textProvName.Name = "textProvName";
			this.textProvName.Size = new System.Drawing.Size(137, 20);
			this.textProvName.TabIndex = 165;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(27, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94, 16);
			this.label1.TabIndex = 166;
			this.label1.Text = "Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(533, 286);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(81, 24);
			this.button1.TabIndex = 166;
			this.button1.Text = "Add Result";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.Location = new System.Drawing.Point(836, 286);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(81, 24);
			this.button2.TabIndex = 167;
			this.button2.Text = "Add Result";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button3.Location = new System.Drawing.Point(836, 429);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(81, 24);
			this.button3.TabIndex = 168;
			this.button3.Text = "Add Result";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// comboUnits
			// 
			this.comboUnits.FormattingEnabled = true;
			this.comboUnits.Location = new System.Drawing.Point(362, 175);
			this.comboUnits.Name = "comboUnits";
			this.comboUnits.Size = new System.Drawing.Size(75, 21);
			this.comboUnits.TabIndex = 226;
			// 
			// butProvSelect
			// 
			this.butProvSelect.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butProvSelect.Autosize = true;
			this.butProvSelect.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butProvSelect.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butProvSelect.CornerRadius = 4F;
			this.butProvSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butProvSelect.Location = new System.Drawing.Point(9, 19);
			this.butProvSelect.Name = "butProvSelect";
			this.butProvSelect.Size = new System.Drawing.Size(29, 25);
			this.butProvSelect.TabIndex = 174;
			this.butProvSelect.Text = "...";
			// 
			// gridClinicalInformation
			// 
			this.gridClinicalInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridClinicalInformation.HScrollVisible = false;
			this.gridClinicalInformation.Location = new System.Drawing.Point(620, 286);
			this.gridClinicalInformation.Name = "gridClinicalInformation";
			this.gridClinicalInformation.ScrollValue = 0;
			this.gridClinicalInformation.Size = new System.Drawing.Size(210, 133);
			this.gridClinicalInformation.TabIndex = 21;
			this.gridClinicalInformation.Title = "Clinical Information";
			this.gridClinicalInformation.TranslationName = null;
			// 
			// gridNotes
			// 
			this.gridNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridNotes.HScrollVisible = false;
			this.gridNotes.Location = new System.Drawing.Point(12, 286);
			this.gridNotes.Name = "gridNotes";
			this.gridNotes.ScrollValue = 0;
			this.gridNotes.Size = new System.Drawing.Size(515, 133);
			this.gridNotes.TabIndex = 20;
			this.gridNotes.Title = "Notes";
			this.gridNotes.TranslationName = null;
			// 
			// gridSpecimen
			// 
			this.gridSpecimen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridSpecimen.HScrollVisible = false;
			this.gridSpecimen.Location = new System.Drawing.Point(620, 429);
			this.gridSpecimen.Name = "gridSpecimen";
			this.gridSpecimen.ScrollValue = 0;
			this.gridSpecimen.Size = new System.Drawing.Size(210, 166);
			this.gridSpecimen.TabIndex = 19;
			this.gridSpecimen.Title = "Specimens";
			this.gridSpecimen.TranslationName = null;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12, 429);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(515, 166);
			this.gridMain.TabIndex = 0;
			this.gridMain.Title = "Lab Results";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(266, 177);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 16);
			this.label2.TabIndex = 175;
			this.label2.Text = "Units";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Location = new System.Drawing.Point(65, 86);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(273, 88);
			this.groupBox1.TabIndex = 227;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Provider";
			// 
			// button4
			// 
			this.button4.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button4.Autosize = true;
			this.button4.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button4.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button4.CornerRadius = 4F;
			this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button4.Location = new System.Drawing.Point(358, 12);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(29, 25);
			this.button4.TabIndex = 228;
			this.button4.Text = "...";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(205, 49);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(137, 20);
			this.textBox1.TabIndex = 175;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(108, 53);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(94, 16);
			this.label3.TabIndex = 176;
			this.label3.Text = "Provider ID";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormEhrLabOrderEdit2014
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(923, 641);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboUnits);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.gridClinicalInformation);
			this.Controls.Add(this.gridNotes);
			this.Controls.Add(this.gridSpecimen);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textName);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOk);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.gridMain);
			this.Name = "FormEhrLabOrderEdit2014";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Lab Order";
			this.Load += new System.EventHandler(this.FormLabPanelEdit_Load);
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.ODGrid gridMain;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOk;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textName;
		private UI.ODGrid gridSpecimen;
		private UI.ODGrid gridNotes;
		private UI.ODGrid gridClinicalInformation;
		private System.Windows.Forms.GroupBox groupBox6;
		private UI.Button butProvSelect;
		private System.Windows.Forms.ComboBox comboProvLang;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textProvID;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textProvName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.ComboBox comboUnits;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private UI.Button button4;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label3;

	}
}