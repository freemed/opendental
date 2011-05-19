namespace OpenDental.Forms {
	partial class FormEduResourceEdit {
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
			this.butProblemSelect = new System.Windows.Forms.Button();
			this.textProblem = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textMedication = new System.Windows.Forms.TextBox();
			this.butMedicationSelect = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.textLabResultsID = new System.Windows.Forms.TextBox();
			this.butCancel = new System.Windows.Forms.Button();
			this.butOk = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.textUrl = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textLabTestName = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.labelCriterionValue = new System.Windows.Forms.Label();
			this.textCompareValue = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butProblemSelect
			// 
			this.butProblemSelect.Location = new System.Drawing.Point(384,17);
			this.butProblemSelect.Name = "butProblemSelect";
			this.butProblemSelect.Size = new System.Drawing.Size(29,23);
			this.butProblemSelect.TabIndex = 0;
			this.butProblemSelect.Text = "...";
			this.butProblemSelect.UseVisualStyleBackColor = true;
			this.butProblemSelect.Click += new System.EventHandler(this.butProblemSelect_Click);
			// 
			// textProblem
			// 
			this.textProblem.Location = new System.Drawing.Point(144,19);
			this.textProblem.Name = "textProblem";
			this.textProblem.ReadOnly = true;
			this.textProblem.Size = new System.Drawing.Size(234,20);
			this.textProblem.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(43,20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Problem ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(43,88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,17);
			this.label2.TabIndex = 5;
			this.label2.Text = "Medication";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMedication
			// 
			this.textMedication.Location = new System.Drawing.Point(144,87);
			this.textMedication.Name = "textMedication";
			this.textMedication.ReadOnly = true;
			this.textMedication.Size = new System.Drawing.Size(234,20);
			this.textMedication.TabIndex = 4;
			// 
			// butMedicationSelect
			// 
			this.butMedicationSelect.Location = new System.Drawing.Point(384,85);
			this.butMedicationSelect.Name = "butMedicationSelect";
			this.butMedicationSelect.Size = new System.Drawing.Size(29,23);
			this.butMedicationSelect.TabIndex = 3;
			this.butMedicationSelect.Text = "...";
			this.butMedicationSelect.UseVisualStyleBackColor = true;
			this.butMedicationSelect.Click += new System.EventHandler(this.butMedicationSelect_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(15,17);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,17);
			this.label3.TabIndex = 8;
			this.label3.Text = "Test Id";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textLabResultsID
			// 
			this.textLabResultsID.Location = new System.Drawing.Point(116,16);
			this.textLabResultsID.Name = "textLabResultsID";
			this.textLabResultsID.Size = new System.Drawing.Size(111,20);
			this.textLabResultsID.TabIndex = 7;
			this.textLabResultsID.Click += new System.EventHandler(this.textLabResults_Click);
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(384,308);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,23);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOk
			// 
			this.butOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOk.Location = new System.Drawing.Point(303,308);
			this.butOk.Name = "butOk";
			this.butOk.Size = new System.Drawing.Size(75,23);
			this.butOk.TabIndex = 10;
			this.butOk.Text = "Ok";
			this.butOk.UseVisualStyleBackColor = true;
			this.butOk.Click += new System.EventHandler(this.butOk_Click);
			// 
			// butDelete
			// 
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Location = new System.Drawing.Point(12,308);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,23);
			this.butDelete.TabIndex = 11;
			this.butDelete.Text = "Delete";
			this.butDelete.UseVisualStyleBackColor = true;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(43,247);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,17);
			this.label4.TabIndex = 13;
			this.label4.Text = "Resource URL";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textUrl
			// 
			this.textUrl.Location = new System.Drawing.Point(144,246);
			this.textUrl.Name = "textUrl";
			this.textUrl.Size = new System.Drawing.Size(234,20);
			this.textUrl.TabIndex = 12;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(15,43);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,17);
			this.label5.TabIndex = 15;
			this.label5.Text = "Test Name";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textLabTestName
			// 
			this.textLabTestName.Location = new System.Drawing.Point(116,42);
			this.textLabTestName.Name = "textLabTestName";
			this.textLabTestName.Size = new System.Drawing.Size(230,20);
			this.textLabTestName.TabIndex = 14;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.labelCriterionValue);
			this.groupBox1.Controls.Add(this.textCompareValue);
			this.groupBox1.Controls.Add(this.textLabResultsID);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textLabTestName);
			this.groupBox1.Location = new System.Drawing.Point(28,132);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(367,100);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Lab Results";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(218,69);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(107,17);
			this.label6.TabIndex = 18;
			this.label6.Text = "For example, >120";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelCriterionValue
			// 
			this.labelCriterionValue.Location = new System.Drawing.Point(7,69);
			this.labelCriterionValue.Name = "labelCriterionValue";
			this.labelCriterionValue.Size = new System.Drawing.Size(107,17);
			this.labelCriterionValue.TabIndex = 17;
			this.labelCriterionValue.Text = "Compare Value";
			this.labelCriterionValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCompareValue
			// 
			this.textCompareValue.Location = new System.Drawing.Point(116,68);
			this.textCompareValue.Name = "textCompareValue";
			this.textCompareValue.Size = new System.Drawing.Size(100,20);
			this.textCompareValue.TabIndex = 16;
			// 
			// FormEduResourceEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(471,343);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textUrl);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOk);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textMedication);
			this.Controls.Add(this.butMedicationSelect);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textProblem);
			this.Controls.Add(this.butProblemSelect);
			this.Name = "FormEduResourceEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Education Resource";
			this.Load += new System.EventHandler(this.FormEduResourceEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butProblemSelect;
		private System.Windows.Forms.TextBox textProblem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textMedication;
		private System.Windows.Forms.Button butMedicationSelect;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textLabResultsID;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOk;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textUrl;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textLabTestName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label labelCriterionValue;
		private System.Windows.Forms.TextBox textCompareValue;
		private System.Windows.Forms.Label label6;
	}
}