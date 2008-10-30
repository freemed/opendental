namespace OpenDental{
	partial class FormAnesthMedsEdit {
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
            this.textAnesthMedName = new System.Windows.Forms.TextBox();
            this.textAnesthHowSupplied = new System.Windows.Forms.TextBox();
            this.textAnestheticMedNum = new System.Windows.Forms.TextBox();
            this.groupAnesthMedsEdit = new System.Windows.Forms.GroupBox();
            this.labelHowSupplied = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelHowSuppl = new System.Windows.Forms.Label();
            this.labelUnitDose = new System.Windows.Forms.Label();
            this.labelChanges = new System.Windows.Forms.Label();
            this.butDelete = new OpenDental.UI.Button();
            this.butOK = new OpenDental.UI.Button();
            this.butCancel = new OpenDental.UI.Button();
            this.groupAnesthMedsEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // textAnesthMedName
            // 
            this.textAnesthMedName.Location = new System.Drawing.Point(46, 51);
            this.textAnesthMedName.Name = "textAnesthMedName";
            this.textAnesthMedName.Size = new System.Drawing.Size(213, 20);
            this.textAnesthMedName.TabIndex = 1;
            this.textAnesthMedName.TextChanged += new System.EventHandler(this.textAnesthMedName_TextChanged);
            // 
            // textAnesthHowSupplied
            // 
            this.textAnesthHowSupplied.Location = new System.Drawing.Point(46, 101);
            this.textAnesthHowSupplied.Name = "textAnesthHowSupplied";
            this.textAnesthHowSupplied.Size = new System.Drawing.Size(213, 20);
            this.textAnesthHowSupplied.TabIndex = 3;
            // 
            // textAnestheticMedNum
            // 
            this.textAnestheticMedNum.Location = new System.Drawing.Point(136, 60);
            this.textAnestheticMedNum.Name = "textAnestheticMedNum";
            this.textAnestheticMedNum.Size = new System.Drawing.Size(144, 20);
            this.textAnestheticMedNum.TabIndex = 5;
            // 
            // groupAnesthMedsEdit
            // 
            this.groupAnesthMedsEdit.Controls.Add(this.labelHowSupplied);
            this.groupAnesthMedsEdit.Controls.Add(this.textAnesthMedName);
            this.groupAnesthMedsEdit.Controls.Add(this.label1);
            this.groupAnesthMedsEdit.Controls.Add(this.textAnesthHowSupplied);
            this.groupAnesthMedsEdit.Controls.Add(this.labelHowSuppl);
            this.groupAnesthMedsEdit.Controls.Add(this.labelUnitDose);
            this.groupAnesthMedsEdit.Controls.Add(this.labelChanges);
            this.groupAnesthMedsEdit.Location = new System.Drawing.Point(27, 23);
            this.groupAnesthMedsEdit.Name = "groupAnesthMedsEdit";
            this.groupAnesthMedsEdit.Size = new System.Drawing.Size(538, 185);
            this.groupAnesthMedsEdit.TabIndex = 9;
            this.groupAnesthMedsEdit.TabStop = false;
            this.groupAnesthMedsEdit.Text = "Add or Edit Anesthetic Medication(s)";
            // 
            // labelHowSupplied
            // 
            this.labelHowSupplied.AutoSize = true;
            this.labelHowSupplied.Location = new System.Drawing.Point(44, 84);
            this.labelHowSupplied.Name = "labelHowSupplied";
            this.labelHowSupplied.Size = new System.Drawing.Size(71, 13);
            this.labelHowSupplied.TabIndex = 3;
            this.labelHowSupplied.Text = "How supplied";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Anesthetic medication and unit dose";
            // 
            // labelHowSuppl
            // 
            this.labelHowSuppl.AutoSize = true;
            this.labelHowSuppl.Location = new System.Drawing.Point(279, 104);
            this.labelHowSuppl.Name = "labelHowSuppl";
            this.labelHowSuppl.Size = new System.Drawing.Size(240, 13);
            this.labelHowSuppl.TabIndex = 5;
            this.labelHowSuppl.Text = "(e.g. 2 mL ampules, 10 mL Multi Dose Vial (MDV))";
            // 
            // labelUnitDose
            // 
            this.labelUnitDose.AutoSize = true;
            this.labelUnitDose.Location = new System.Drawing.Point(279, 51);
            this.labelUnitDose.Name = "labelUnitDose";
            this.labelUnitDose.Size = new System.Drawing.Size(215, 13);
            this.labelUnitDose.TabIndex = 2;
            this.labelUnitDose.Text = "(e.g. Fentanyl 50 mcg/mL, Versed 5 mg/mL)";
            // 
            // labelChanges
            // 
            this.labelChanges.Location = new System.Drawing.Point(6, 137);
            this.labelChanges.Name = "labelChanges";
            this.labelChanges.Size = new System.Drawing.Size(508, 33);
            this.labelChanges.TabIndex = 6;
            this.labelChanges.Text = "NOTE: Once a medication has been added, spelling changes can be made here, but th" +
                "e name and type of medication should not be changed or the inventory system will" +
                " be adversely affected...";
            // 
            // butDelete
            // 
            this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butDelete.Autosize = true;
            this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butDelete.CornerRadius = 4F;
            this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
            this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butDelete.Location = new System.Drawing.Point(27, 226);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(81, 26);
            this.butDelete.TabIndex = 6;
            this.butDelete.Text = "Delete";
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // butOK
            // 
            this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butOK.Autosize = true;
            this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butOK.CornerRadius = 4F;
            this.butOK.Location = new System.Drawing.Point(473, 226);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(92, 26);
            this.butOK.TabIndex = 7;
            this.butOK.Text = "Save and  Close";
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butCancel
            // 
            this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.Autosize = true;
            this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butCancel.CornerRadius = 4F;
            this.butCancel.Image = global::OpenDental.Properties.Resources.deleteX;
            this.butCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butCancel.Location = new System.Drawing.Point(383, 226);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(83, 26);
            this.butCancel.TabIndex = 8;
            this.butCancel.Text = "&Cancel";
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // FormAnesthMedsEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(590, 265);
            this.Controls.Add(this.butDelete);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.groupAnesthMedsEdit);
            this.Name = "FormAnesthMedsEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add/Edit Anesthetic Med(s)";
            this.Load += new System.EventHandler(this.FormAnesthMedsEdit_Load);
            this.groupAnesthMedsEdit.ResumeLayout(false);
            this.groupAnesthMedsEdit.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TextBox textAnesthMedName;
        private System.Windows.Forms.TextBox textAnesthHowSupplied;
        private System.Windows.Forms.TextBox textAnestheticMedNum;
		private OpenDental.UI.Button butOK;
        private OpenDental.UI.Button butCancel;
        private OpenDental.UI.Button butDelete;
        private System.Windows.Forms.GroupBox groupAnesthMedsEdit;
        private System.Windows.Forms.Label labelHowSupplied;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelHowSuppl;
        private System.Windows.Forms.Label labelUnitDose;
        private System.Windows.Forms.Label labelChanges;
	}
}