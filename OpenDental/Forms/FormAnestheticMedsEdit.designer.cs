namespace OpenDental{

	partial class FormAnestheticMedsEdit
	{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnestheticMedsEdit));
			this.groupAnesthMedsEdit = new System.Windows.Forms.GroupBox();
			this.labelHowSuppl = new System.Windows.Forms.Label();
			this.textHowSupplied = new System.Windows.Forms.TextBox();
			this.labelHowSupplied = new System.Windows.Forms.Label();
			this.labelUnitDose = new System.Windows.Forms.Label();
			this.textAnesthMed = new System.Windows.Forms.TextBox();
			this.labelAnesthMedName = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupAnesthMedsEdit.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupAnesthMedsEdit
			// 
			this.groupAnesthMedsEdit.Controls.Add(this.labelHowSuppl);
			this.groupAnesthMedsEdit.Controls.Add(this.textHowSupplied);
			this.groupAnesthMedsEdit.Controls.Add(this.labelHowSupplied);
			this.groupAnesthMedsEdit.Controls.Add(this.labelUnitDose);
			this.groupAnesthMedsEdit.Controls.Add(this.textAnesthMed);
			this.groupAnesthMedsEdit.Controls.Add(this.labelAnesthMedName);
			this.groupAnesthMedsEdit.Location = new System.Drawing.Point(22, 28);
			this.groupAnesthMedsEdit.Name = "groupAnesthMedsEdit";
			this.groupAnesthMedsEdit.Size = new System.Drawing.Size(532, 166);
			this.groupAnesthMedsEdit.TabIndex = 4;
			this.groupAnesthMedsEdit.TabStop = false;
			this.groupAnesthMedsEdit.Text = "Add or Edit Anesthetic Medication(s)";
			// 
			// labelHowSuppl
			// 
			this.labelHowSuppl.AutoSize = true;
			this.labelHowSuppl.Location = new System.Drawing.Point(274, 104);
			this.labelHowSuppl.Name = "labelHowSuppl";
			this.labelHowSuppl.Size = new System.Drawing.Size(240, 13);
			this.labelHowSuppl.TabIndex = 5;
			this.labelHowSuppl.Text = "(e.g. 2 mL ampules, 10 mL Multi Dose Vial (MDV))";
			// 
			// textHowSupplied
			// 
			this.textHowSupplied.Location = new System.Drawing.Point(50, 104);
			this.textHowSupplied.Name = "textHowSupplied";
			this.textHowSupplied.Size = new System.Drawing.Size(201, 20);
			this.textHowSupplied.TabIndex = 4;
			// 
			// labelHowSupplied
			// 
			this.labelHowSupplied.AutoSize = true;
			this.labelHowSupplied.Location = new System.Drawing.Point(47, 87);
			this.labelHowSupplied.Name = "labelHowSupplied";
			this.labelHowSupplied.Size = new System.Drawing.Size(71, 13);
			this.labelHowSupplied.TabIndex = 3;
			this.labelHowSupplied.Text = "How supplied";
			// 
			// labelUnitDose
			// 
			this.labelUnitDose.AutoSize = true;
			this.labelUnitDose.Location = new System.Drawing.Point(271, 51);
			this.labelUnitDose.Name = "labelUnitDose";
			this.labelUnitDose.Size = new System.Drawing.Size(215, 13);
			this.labelUnitDose.TabIndex = 2;
			this.labelUnitDose.Text = "(e.g. Fentanyl 50 mcg/mL, Versed 5 mg/mL)";
			// 
			// textAnesthMed
			// 
			this.textAnesthMed.Location = new System.Drawing.Point(47, 51);
			this.textAnesthMed.Name = "textAnesthMed";
			this.textAnesthMed.Size = new System.Drawing.Size(204, 20);
			this.textAnesthMed.TabIndex = 1;
			this.textAnesthMed.TextChanged += new System.EventHandler(this.textAnesthMed_TextChanged);
			// 
			// labelAnesthMedName
			// 
			this.labelAnesthMedName.AutoSize = true;
			this.labelAnesthMedName.Location = new System.Drawing.Point(44, 35);
			this.labelAnesthMedName.Name = "labelAnesthMedName";
			this.labelAnesthMedName.Size = new System.Drawing.Size(178, 13);
			this.labelAnesthMedName.TabIndex = 0;
			this.labelAnesthMedName.Text = "Anesthetic medication and unit dose";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(384, 219);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCancel.Location = new System.Drawing.Point(465, 219);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(74, 26);
			this.butCancel.TabIndex = 55;
			this.butCancel.Text = "&Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.button1_Click);
			// 
			// FormAnestheticMedsEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(577, 258);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.groupAnesthMedsEdit);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormAnestheticMedsEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add/Edit Anesthetic Med(s)";
			this.groupAnesthMedsEdit.ResumeLayout(false);
			this.groupAnesthMedsEdit.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.GroupBox groupAnesthMedsEdit;
		private System.Windows.Forms.Label labelAnesthMedName;
		private System.Windows.Forms.Label labelHowSuppl;
		private System.Windows.Forms.TextBox textHowSupplied;
		private System.Windows.Forms.Label labelHowSupplied;
		private System.Windows.Forms.Label labelUnitDose;
		private System.Windows.Forms.TextBox textAnesthMed;
		private OpenDental.UI.Button butCancel;
	}
}