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
			this.textLabName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textSpecimenCondition = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textSpecimenSourceCode = new System.Windows.Forms.TextBox();
			this.butAdd = new System.Windows.Forms.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.label5 = new System.Windows.Forms.Label();
			this.textName = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textSpecimenLocation = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textServiceID = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textServiceName = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.gridHL7Messages = new OpenDental.UI.ODGrid();
			this.gridSpecimen = new OpenDental.UI.ODGrid();
			this.gridNotes = new OpenDental.UI.ODGrid();
			this.gridClinicalInformation = new OpenDental.UI.ODGrid();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
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
			// textLabName
			// 
			this.textLabName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textLabName.Location = new System.Drawing.Point(130, 106);
			this.textLabName.Multiline = true;
			this.textLabName.Name = "textLabName";
			this.textLabName.Size = new System.Drawing.Size(781, 36);
			this.textLabName.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7, 107);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 18);
			this.label2.TabIndex = 7;
			this.label2.Text = "Lab Name / Address";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(2, 144);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(125, 17);
			this.label3.TabIndex = 9;
			this.label3.Text = "Specimen Condition";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSpecimenCondition
			// 
			this.textSpecimenCondition.Location = new System.Drawing.Point(130, 142);
			this.textSpecimenCondition.Name = "textSpecimenCondition";
			this.textSpecimenCondition.Size = new System.Drawing.Size(158, 20);
			this.textSpecimenCondition.TabIndex = 8;
			this.textSpecimenCondition.WordWrap = false;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 17);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 17);
			this.label4.TabIndex = 11;
			this.label4.Text = "Code";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSpecimenSourceCode
			// 
			this.textSpecimenSourceCode.Location = new System.Drawing.Point(113, 14);
			this.textSpecimenSourceCode.Name = "textSpecimenSourceCode";
			this.textSpecimenSourceCode.Size = new System.Drawing.Size(64, 20);
			this.textSpecimenSourceCode.TabIndex = 10;
			this.textSpecimenSourceCode.WordWrap = false;
			// 
			// butAdd
			// 
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Location = new System.Drawing.Point(207, 606);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(81, 24);
			this.butAdd.TabIndex = 13;
			this.butAdd.Text = "Add Result";
			this.butAdd.UseVisualStyleBackColor = true;
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12, 395);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(602, 200);
			this.gridMain.TabIndex = 0;
			this.gridMain.Title = "Lab Results";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(5, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(122, 17);
			this.label5.TabIndex = 15;
			this.label5.Text = "Patient Name";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textName
			// 
			this.textName.Location = new System.Drawing.Point(130, 14);
			this.textName.Name = "textName";
			this.textName.ReadOnly = true;
			this.textName.Size = new System.Drawing.Size(312, 20);
			this.textName.TabIndex = 14;
			this.textName.WordWrap = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textSpecimenLocation);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textSpecimenSourceCode);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Location = new System.Drawing.Point(17, 168);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(425, 61);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Specimen Source";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(183, 17);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(202, 17);
			this.label7.TabIndex = 14;
			this.label7.Text = "HL7 0070 Format";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textSpecimenLocation
			// 
			this.textSpecimenLocation.Location = new System.Drawing.Point(113, 34);
			this.textSpecimenLocation.Name = "textSpecimenLocation";
			this.textSpecimenLocation.Size = new System.Drawing.Size(302, 20);
			this.textSpecimenLocation.TabIndex = 12;
			this.textSpecimenLocation.WordWrap = false;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(24, 37);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 17);
			this.label6.TabIndex = 13;
			this.label6.Text = "Location";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textServiceID);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.textServiceName);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Location = new System.Drawing.Point(17, 38);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(425, 61);
			this.groupBox2.TabIndex = 17;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Service";
			// 
			// textServiceID
			// 
			this.textServiceID.Location = new System.Drawing.Point(113, 14);
			this.textServiceID.Name = "textServiceID";
			this.textServiceID.Size = new System.Drawing.Size(106, 20);
			this.textServiceID.TabIndex = 0;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(41, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(70, 17);
			this.label8.TabIndex = 2;
			this.label8.Text = "LOINC";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textServiceName
			// 
			this.textServiceName.Location = new System.Drawing.Point(113, 34);
			this.textServiceName.Name = "textServiceName";
			this.textServiceName.Size = new System.Drawing.Size(302, 20);
			this.textServiceName.TabIndex = 1;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(41, 36);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(70, 17);
			this.label9.TabIndex = 3;
			this.label9.Text = "Name";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridHL7Messages
			// 
			this.gridHL7Messages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridHL7Messages.HScrollVisible = false;
			this.gridHL7Messages.Location = new System.Drawing.Point(620, 395);
			this.gridHL7Messages.Name = "gridHL7Messages";
			this.gridHL7Messages.ScrollValue = 0;
			this.gridHL7Messages.Size = new System.Drawing.Size(291, 97);
			this.gridHL7Messages.TabIndex = 18;
			this.gridHL7Messages.Title = "HL7 Messages";
			this.gridHL7Messages.TranslationName = null;
			// 
			// gridSpecimen
			// 
			this.gridSpecimen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridSpecimen.HScrollVisible = false;
			this.gridSpecimen.Location = new System.Drawing.Point(620, 498);
			this.gridSpecimen.Name = "gridSpecimen";
			this.gridSpecimen.ScrollValue = 0;
			this.gridSpecimen.Size = new System.Drawing.Size(291, 97);
			this.gridSpecimen.TabIndex = 19;
			this.gridSpecimen.Title = "Specimens";
			this.gridSpecimen.TranslationName = null;
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
			this.gridNotes.Size = new System.Drawing.Size(602, 103);
			this.gridNotes.TabIndex = 20;
			this.gridNotes.Title = "Notes";
			this.gridNotes.TranslationName = null;
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
			this.gridClinicalInformation.Size = new System.Drawing.Size(291, 103);
			this.gridClinicalInformation.TabIndex = 21;
			this.gridClinicalInformation.Title = "Clinical Information";
			this.gridClinicalInformation.TranslationName = null;
			// 
			// FormEhrLabOrderEdit2014
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(923, 641);
			this.Controls.Add(this.gridClinicalInformation);
			this.Controls.Add(this.gridNotes);
			this.Controls.Add(this.gridSpecimen);
			this.Controls.Add(this.gridHL7Messages);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textName);
			this.Controls.Add(this.textSpecimenCondition);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textLabName);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOk);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.gridMain);
			this.Name = "FormEhrLabOrderEdit2014";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Lab Order";
			this.Load += new System.EventHandler(this.FormLabPanelEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.ODGrid gridMain;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOk;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.TextBox textLabName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textSpecimenCondition;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textSpecimenSourceCode;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textSpecimenLocation;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textServiceID;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textServiceName;
		private System.Windows.Forms.Label label9;
		private UI.ODGrid gridHL7Messages;
		private UI.ODGrid gridSpecimen;
		private UI.ODGrid gridNotes;
		private UI.ODGrid gridClinicalInformation;

	}
}