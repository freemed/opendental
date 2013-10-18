namespace OpenDental {
	partial class FormEHR {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEHR));
			this.butClose = new System.Windows.Forms.Button();
			this.butMeasures = new System.Windows.Forms.Button();
			this.butHash = new System.Windows.Forms.Button();
			this.butEncryption = new System.Windows.Forms.Button();
			this.butQuality = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.but2014CQM = new System.Windows.Forms.Button();
			this.butEhrNotPerformed = new System.Windows.Forms.Button();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.butVaccines = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelProvPat = new System.Windows.Forms.Label();
			this.labelProvUser = new System.Windows.Forms.Label();
			this.butPatList = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.butLabPanelLOINC = new System.Windows.Forms.Button();
			this.butAmendments = new System.Windows.Forms.Button();
			this.butEncounters = new System.Windows.Forms.Button();
			this.butInterventions = new System.Windows.Forms.Button();
			this.gridMu = new OpenDental.UI.ODGrid();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Location = new System.Drawing.Point(713, 638);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(86, 23);
			this.butClose.TabIndex = 9;
			this.butClose.Text = "Close";
			this.butClose.UseVisualStyleBackColor = true;
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butMeasures
			// 
			this.butMeasures.Location = new System.Drawing.Point(10, 19);
			this.butMeasures.Name = "butMeasures";
			this.butMeasures.Size = new System.Drawing.Size(84, 23);
			this.butMeasures.TabIndex = 11;
			this.butMeasures.Text = "Measure Calc";
			this.butMeasures.UseVisualStyleBackColor = true;
			this.butMeasures.Click += new System.EventHandler(this.butMeasures_Click);
			// 
			// butHash
			// 
			this.butHash.Location = new System.Drawing.Point(10, 19);
			this.butHash.Name = "butHash";
			this.butHash.Size = new System.Drawing.Size(84, 23);
			this.butHash.TabIndex = 13;
			this.butHash.Text = "Hash";
			this.butHash.UseVisualStyleBackColor = true;
			this.butHash.Click += new System.EventHandler(this.butHash_Click);
			// 
			// butEncryption
			// 
			this.butEncryption.Location = new System.Drawing.Point(10, 48);
			this.butEncryption.Name = "butEncryption";
			this.butEncryption.Size = new System.Drawing.Size(84, 23);
			this.butEncryption.TabIndex = 17;
			this.butEncryption.Text = "Encryption";
			this.butEncryption.UseVisualStyleBackColor = true;
			this.butEncryption.Click += new System.EventHandler(this.butEncryption_Click);
			// 
			// butQuality
			// 
			this.butQuality.Location = new System.Drawing.Point(10, 48);
			this.butQuality.Name = "butQuality";
			this.butQuality.Size = new System.Drawing.Size(84, 23);
			this.butQuality.TabIndex = 20;
			this.butQuality.Text = "Quality Meas";
			this.butQuality.UseVisualStyleBackColor = true;
			this.butQuality.Click += new System.EventHandler(this.butQuality_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.butMeasures);
			this.groupBox4.Controls.Add(this.butQuality);
			this.groupBox4.Location = new System.Drawing.Point(702, 81);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(104, 83);
			this.groupBox4.TabIndex = 25;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "For All Patients";
			// 
			// but2014CQM
			// 
			this.but2014CQM.Location = new System.Drawing.Point(712, 492);
			this.but2014CQM.Name = "but2014CQM";
			this.but2014CQM.Size = new System.Drawing.Size(84, 23);
			this.but2014CQM.TabIndex = 21;
			this.but2014CQM.Text = "2014 CQM";
			this.but2014CQM.UseVisualStyleBackColor = true;
			this.but2014CQM.Visible = false;
			this.but2014CQM.Click += new System.EventHandler(this.but2014CQM_Click);
			// 
			// butEhrNotPerformed
			// 
			this.butEhrNotPerformed.Location = new System.Drawing.Point(712, 405);
			this.butEhrNotPerformed.Name = "butEhrNotPerformed";
			this.butEhrNotPerformed.Size = new System.Drawing.Size(84, 23);
			this.butEhrNotPerformed.TabIndex = 38;
			this.butEhrNotPerformed.Text = "Not Performed";
			this.butEhrNotPerformed.UseVisualStyleBackColor = true;
			this.butEhrNotPerformed.Visible = false;
			this.butEhrNotPerformed.Click += new System.EventHandler(this.butEhrNotPerformed_Click);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.butHash);
			this.groupBox5.Controls.Add(this.butEncryption);
			this.groupBox5.Location = new System.Drawing.Point(702, 170);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(104, 83);
			this.groupBox5.TabIndex = 26;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Tools";
			// 
			// butVaccines
			// 
			this.butVaccines.Location = new System.Drawing.Point(712, 262);
			this.butVaccines.Name = "butVaccines";
			this.butVaccines.Size = new System.Drawing.Size(84, 23);
			this.butVaccines.TabIndex = 27;
			this.butVaccines.Text = "Vaccines";
			this.butVaccines.UseVisualStyleBackColor = true;
			this.butVaccines.Click += new System.EventHandler(this.butVaccines_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 18);
			this.label1.TabIndex = 28;
			this.label1.Text = "Provider for this patient:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 18);
			this.label2.TabIndex = 29;
			this.label2.Text = "Provider logged on:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelProvPat
			// 
			this.labelProvPat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelProvPat.ForeColor = System.Drawing.Color.DarkRed;
			this.labelProvPat.Location = new System.Drawing.Point(135, 8);
			this.labelProvPat.Name = "labelProvPat";
			this.labelProvPat.Size = new System.Drawing.Size(426, 18);
			this.labelProvPat.TabIndex = 30;
			this.labelProvPat.Text = "Abbr - ProvLName, ProvFName";
			this.labelProvPat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelProvUser
			// 
			this.labelProvUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelProvUser.ForeColor = System.Drawing.Color.DarkRed;
			this.labelProvUser.Location = new System.Drawing.Point(135, 29);
			this.labelProvUser.Name = "labelProvUser";
			this.labelProvUser.Size = new System.Drawing.Size(426, 18);
			this.labelProvUser.TabIndex = 31;
			this.labelProvUser.Text = "Abbr - ProvLName, ProvFName";
			this.labelProvUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butPatList
			// 
			this.butPatList.Location = new System.Drawing.Point(712, 291);
			this.butPatList.Name = "butPatList";
			this.butPatList.Size = new System.Drawing.Size(84, 23);
			this.butPatList.TabIndex = 32;
			this.butPatList.Text = "Patient List";
			this.butPatList.UseVisualStyleBackColor = true;
			this.butPatList.Click += new System.EventHandler(this.butPatList_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 53);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(688, 32);
			this.label3.TabIndex = 33;
			this.label3.Text = resources.GetString("label3.Text");
			// 
			// butLabPanelLOINC
			// 
			this.butLabPanelLOINC.Location = new System.Drawing.Point(712, 347);
			this.butLabPanelLOINC.Name = "butLabPanelLOINC";
			this.butLabPanelLOINC.Size = new System.Drawing.Size(84, 23);
			this.butLabPanelLOINC.TabIndex = 35;
			this.butLabPanelLOINC.Text = "LOINC Lab";
			this.butLabPanelLOINC.UseVisualStyleBackColor = true;
			this.butLabPanelLOINC.Visible = false;
			this.butLabPanelLOINC.Click += new System.EventHandler(this.butLabPanelLOINC_Click);
			// 
			// butAmendments
			// 
			this.butAmendments.Location = new System.Drawing.Point(712, 376);
			this.butAmendments.Name = "butAmendments";
			this.butAmendments.Size = new System.Drawing.Size(84, 23);
			this.butAmendments.TabIndex = 36;
			this.butAmendments.Text = "Amendments";
			this.butAmendments.UseVisualStyleBackColor = true;
			this.butAmendments.Visible = false;
			this.butAmendments.Click += new System.EventHandler(this.butAmendments_Click);
			// 
			// butEncounters
			// 
			this.butEncounters.Location = new System.Drawing.Point(712, 434);
			this.butEncounters.Name = "butEncounters";
			this.butEncounters.Size = new System.Drawing.Size(84, 23);
			this.butEncounters.TabIndex = 39;
			this.butEncounters.Text = "Encounters";
			this.butEncounters.UseVisualStyleBackColor = true;
			this.butEncounters.Visible = false;
			this.butEncounters.Click += new System.EventHandler(this.butEncounters_Click);
			// 
			// butInterventions
			// 
			this.butInterventions.Location = new System.Drawing.Point(712, 463);
			this.butInterventions.Name = "butInterventions";
			this.butInterventions.Size = new System.Drawing.Size(84, 23);
			this.butInterventions.TabIndex = 40;
			this.butInterventions.Text = "Interventions";
			this.butInterventions.UseVisualStyleBackColor = true;
			this.butInterventions.Visible = false;
			this.butInterventions.Click += new System.EventHandler(this.butInterventions_Click);
			// 
			// gridMu
			// 
			this.gridMu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridMu.HScrollVisible = false;
			this.gridMu.Location = new System.Drawing.Point(6, 88);
			this.gridMu.Name = "gridMu";
			this.gridMu.ScrollValue = 0;
			this.gridMu.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridMu.Size = new System.Drawing.Size(688, 573);
			this.gridMu.TabIndex = 24;
			this.gridMu.Title = "Stage 1 Meaningful Use for this patient";
			this.gridMu.TranslationName = null;
			this.gridMu.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMu_CellClick);
			// 
			// FormEHR
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(817, 674);
			this.Controls.Add(this.but2014CQM);
			this.Controls.Add(this.butInterventions);
			this.Controls.Add(this.butEncounters);
			this.Controls.Add(this.butEhrNotPerformed);
			this.Controls.Add(this.butAmendments);
			this.Controls.Add(this.butLabPanelLOINC);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butPatList);
			this.Controls.Add(this.labelProvUser);
			this.Controls.Add(this.labelProvPat);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butVaccines);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.gridMu);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormEHR";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "EHR";
			this.Load += new System.EventHandler(this.FormEHR_Load);
			this.Shown += new System.EventHandler(this.FormEHR_Shown);
			this.groupBox4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Button butMeasures;
		private System.Windows.Forms.Button butHash;
		private System.Windows.Forms.Button butEncryption;
		private System.Windows.Forms.Button butQuality;
		private OpenDental.UI.ODGrid gridMu;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Button butVaccines;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelProvPat;
		private System.Windows.Forms.Label labelProvUser;
		private System.Windows.Forms.Button butPatList;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button butLabPanelLOINC;
		private System.Windows.Forms.Button butAmendments;
		private System.Windows.Forms.Button butEhrNotPerformed;
		private System.Windows.Forms.Button but2014CQM;
		private System.Windows.Forms.Button butEncounters;
		private System.Windows.Forms.Button butInterventions;
	}
}