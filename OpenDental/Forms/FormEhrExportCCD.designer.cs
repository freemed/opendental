namespace OpenDental{
	partial class FormEhrExportCCD {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEhrExportCCD));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.checkEncounter = new System.Windows.Forms.CheckBox();
			this.checkFunctionalStatus = new System.Windows.Forms.CheckBox();
			this.checkImmunization = new System.Windows.Forms.CheckBox();
			this.checkMedication = new System.Windows.Forms.CheckBox();
			this.checkAllergy = new System.Windows.Forms.CheckBox();
			this.checkPlanOfCare = new System.Windows.Forms.CheckBox();
			this.checkProblem = new System.Windows.Forms.CheckBox();
			this.checkProcedure = new System.Windows.Forms.CheckBox();
			this.checkResult = new System.Windows.Forms.CheckBox();
			this.checkSocialHistory = new System.Windows.Forms.CheckBox();
			this.checkVitalSign = new System.Windows.Forms.CheckBox();
			this.labelSections = new System.Windows.Forms.Label();
			this.butCheckAll = new OpenDental.UI.Button();
			this.butCheckNone = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(94, 321);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
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
			this.butCancel.Location = new System.Drawing.Point(175, 321);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// checkEncounter
			// 
			this.checkEncounter.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEncounter.Location = new System.Drawing.Point(12, 78);
			this.checkEncounter.Name = "checkEncounter";
			this.checkEncounter.Size = new System.Drawing.Size(238, 18);
			this.checkEncounter.TabIndex = 120;
			this.checkEncounter.Text = "Encounter";
			// 
			// checkFunctionalStatus
			// 
			this.checkFunctionalStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkFunctionalStatus.Location = new System.Drawing.Point(12, 102);
			this.checkFunctionalStatus.Name = "checkFunctionalStatus";
			this.checkFunctionalStatus.Size = new System.Drawing.Size(238, 18);
			this.checkFunctionalStatus.TabIndex = 121;
			this.checkFunctionalStatus.Text = "Functional Status";
			// 
			// checkImmunization
			// 
			this.checkImmunization.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkImmunization.Location = new System.Drawing.Point(12, 126);
			this.checkImmunization.Name = "checkImmunization";
			this.checkImmunization.Size = new System.Drawing.Size(238, 18);
			this.checkImmunization.TabIndex = 122;
			this.checkImmunization.Text = "Immunization";
			// 
			// checkMedication
			// 
			this.checkMedication.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkMedication.Location = new System.Drawing.Point(12, 150);
			this.checkMedication.Name = "checkMedication";
			this.checkMedication.Size = new System.Drawing.Size(238, 18);
			this.checkMedication.TabIndex = 123;
			this.checkMedication.Text = "Medication";
			// 
			// checkAllergy
			// 
			this.checkAllergy.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllergy.Location = new System.Drawing.Point(12, 54);
			this.checkAllergy.Name = "checkAllergy";
			this.checkAllergy.Size = new System.Drawing.Size(238, 18);
			this.checkAllergy.TabIndex = 124;
			this.checkAllergy.Text = "Allergy";
			// 
			// checkPlanOfCare
			// 
			this.checkPlanOfCare.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPlanOfCare.Location = new System.Drawing.Point(12, 174);
			this.checkPlanOfCare.Name = "checkPlanOfCare";
			this.checkPlanOfCare.Size = new System.Drawing.Size(238, 18);
			this.checkPlanOfCare.TabIndex = 125;
			this.checkPlanOfCare.Text = "Plan of Care";
			// 
			// checkProblem
			// 
			this.checkProblem.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkProblem.Location = new System.Drawing.Point(12, 198);
			this.checkProblem.Name = "checkProblem";
			this.checkProblem.Size = new System.Drawing.Size(238, 18);
			this.checkProblem.TabIndex = 126;
			this.checkProblem.Text = "Problem";
			// 
			// checkProcedure
			// 
			this.checkProcedure.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkProcedure.Location = new System.Drawing.Point(12, 222);
			this.checkProcedure.Name = "checkProcedure";
			this.checkProcedure.Size = new System.Drawing.Size(238, 18);
			this.checkProcedure.TabIndex = 127;
			this.checkProcedure.Text = "Procedure";
			// 
			// checkResult
			// 
			this.checkResult.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkResult.Location = new System.Drawing.Point(12, 246);
			this.checkResult.Name = "checkResult";
			this.checkResult.Size = new System.Drawing.Size(157, 18);
			this.checkResult.TabIndex = 129;
			this.checkResult.Text = "Result";
			// 
			// checkSocialHistory
			// 
			this.checkSocialHistory.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSocialHistory.Location = new System.Drawing.Point(12, 270);
			this.checkSocialHistory.Name = "checkSocialHistory";
			this.checkSocialHistory.Size = new System.Drawing.Size(157, 18);
			this.checkSocialHistory.TabIndex = 130;
			this.checkSocialHistory.Text = "Social History";
			// 
			// checkVitalSign
			// 
			this.checkVitalSign.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkVitalSign.Location = new System.Drawing.Point(12, 294);
			this.checkVitalSign.Name = "checkVitalSign";
			this.checkVitalSign.Size = new System.Drawing.Size(170, 18);
			this.checkVitalSign.TabIndex = 131;
			this.checkVitalSign.Text = "Vital Sign";
			// 
			// labelSections
			// 
			this.labelSections.Location = new System.Drawing.Point(9, 9);
			this.labelSections.Name = "labelSections";
			this.labelSections.Size = new System.Drawing.Size(241, 42);
			this.labelSections.TabIndex = 132;
			this.labelSections.Text = "Choose the sections to include in the Continuity of Care Document.";
			this.labelSections.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butCheckAll
			// 
			this.butCheckAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCheckAll.Autosize = true;
			this.butCheckAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCheckAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCheckAll.CornerRadius = 4F;
			this.butCheckAll.Location = new System.Drawing.Point(175, 237);
			this.butCheckAll.Name = "butCheckAll";
			this.butCheckAll.Size = new System.Drawing.Size(75, 24);
			this.butCheckAll.TabIndex = 133;
			this.butCheckAll.Text = "Check All";
			this.butCheckAll.Click += new System.EventHandler(this.butCheckAll_Click);
			// 
			// butCheckNone
			// 
			this.butCheckNone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCheckNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCheckNone.Autosize = true;
			this.butCheckNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCheckNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCheckNone.CornerRadius = 4F;
			this.butCheckNone.Location = new System.Drawing.Point(175, 267);
			this.butCheckNone.Name = "butCheckNone";
			this.butCheckNone.Size = new System.Drawing.Size(75, 24);
			this.butCheckNone.TabIndex = 134;
			this.butCheckNone.Text = "Check None";
			this.butCheckNone.Click += new System.EventHandler(this.butCheckNone_Click);
			// 
			// FormEhrExportCCD
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(262, 357);
			this.Controls.Add(this.butCheckNone);
			this.Controls.Add(this.butCheckAll);
			this.Controls.Add(this.labelSections);
			this.Controls.Add(this.checkVitalSign);
			this.Controls.Add(this.checkSocialHistory);
			this.Controls.Add(this.checkResult);
			this.Controls.Add(this.checkProcedure);
			this.Controls.Add(this.checkProblem);
			this.Controls.Add(this.checkPlanOfCare);
			this.Controls.Add(this.checkAllergy);
			this.Controls.Add(this.checkMedication);
			this.Controls.Add(this.checkImmunization);
			this.Controls.Add(this.checkFunctionalStatus);
			this.Controls.Add(this.checkEncounter);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(100, 100);
			this.Name = "FormEhrExportCCD";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Export CCD";
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.CheckBox checkEncounter;
		private System.Windows.Forms.CheckBox checkFunctionalStatus;
		private System.Windows.Forms.CheckBox checkImmunization;
		private System.Windows.Forms.CheckBox checkMedication;
		private System.Windows.Forms.CheckBox checkAllergy;
		private System.Windows.Forms.CheckBox checkPlanOfCare;
		private System.Windows.Forms.CheckBox checkProblem;
		private System.Windows.Forms.CheckBox checkProcedure;
		private System.Windows.Forms.CheckBox checkResult;
		private System.Windows.Forms.CheckBox checkSocialHistory;
		private System.Windows.Forms.CheckBox checkVitalSign;
		private System.Windows.Forms.Label labelSections;
		private UI.Button butCheckAll;
		private UI.Button butCheckNone;
	}
}