namespace OpenDental{
	partial class FormFormularyMedEdit {
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
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.labelMedication = new System.Windows.Forms.Label();
			this.textMedication = new System.Windows.Forms.TextBox();
			this.butMedicationSelect = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Location = new System.Drawing.Point(210,116);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 10;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(291,116);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// labelMedication
			// 
			this.labelMedication.Location = new System.Drawing.Point(12,46);
			this.labelMedication.Name = "labelMedication";
			this.labelMedication.Size = new System.Drawing.Size(86,17);
			this.labelMedication.TabIndex = 13;
			this.labelMedication.Text = "Medication";
			this.labelMedication.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMedication
			// 
			this.textMedication.Location = new System.Drawing.Point(99,46);
			this.textMedication.Name = "textMedication";
			this.textMedication.ReadOnly = true;
			this.textMedication.Size = new System.Drawing.Size(232,20);
			this.textMedication.TabIndex = 11;
			// 
			// butMedicationSelect
			// 
			this.butMedicationSelect.Location = new System.Drawing.Point(337,44);
			this.butMedicationSelect.Name = "butMedicationSelect";
			this.butMedicationSelect.Size = new System.Drawing.Size(29,23);
			this.butMedicationSelect.TabIndex = 12;
			this.butMedicationSelect.Text = "...";
			this.butMedicationSelect.UseVisualStyleBackColor = true;
			this.butMedicationSelect.Click += new System.EventHandler(this.butMedicationSelect_Click);
			// 
			// butDelete
			// 
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Location = new System.Drawing.Point(23,116);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,24);
			this.butDelete.TabIndex = 14;
			this.butDelete.Text = "Delete";
			this.butDelete.UseVisualStyleBackColor = true;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormFormularyMedEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(388,152);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.labelMedication);
			this.Controls.Add(this.textMedication);
			this.Controls.Add(this.butMedicationSelect);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormFormularyMedEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Formulary Med Edit";
			this.Load += new System.EventHandler(this.FormFormularyMedEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Label labelMedication;
		private System.Windows.Forms.TextBox textMedication;
		private System.Windows.Forms.Button butMedicationSelect;
		private System.Windows.Forms.Button butDelete;

	}
}