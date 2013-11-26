namespace OpenDental {
	partial class FormEhrPatientSmoking {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEhrPatientSmoking));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.comboSmokeStatus = new System.Windows.Forms.ComboBox();
			this.butAssessed = new System.Windows.Forms.Button();
			this.butIntervention = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboAssessmentType = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.butSnomed = new System.Windows.Forms.Button();
			this.textDateAssessed = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(615, 412);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 23);
			this.butCancel.TabIndex = 8;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Location = new System.Drawing.Point(534, 412);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 23);
			this.butOK.TabIndex = 9;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13, 71);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 17);
			this.label2.TabIndex = 11;
			this.label2.Text = "Smoking Status";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboSmokeStatus
			// 
			this.comboSmokeStatus.FormattingEnabled = true;
			this.comboSmokeStatus.Location = new System.Drawing.Point(117, 71);
			this.comboSmokeStatus.MaxDropDownItems = 30;
			this.comboSmokeStatus.Name = "comboSmokeStatus";
			this.comboSmokeStatus.Size = new System.Drawing.Size(260, 21);
			this.comboSmokeStatus.TabIndex = 12;
			this.comboSmokeStatus.SelectionChangeCommitted += new System.EventHandler(this.comboSmokeStatus_SelectionChangeCommitted);
			// 
			// butAssessed
			// 
			this.butAssessed.Location = new System.Drawing.Point(117, 107);
			this.butAssessed.Name = "butAssessed";
			this.butAssessed.Size = new System.Drawing.Size(75, 23);
			this.butAssessed.TabIndex = 17;
			this.butAssessed.Text = "Assessed";
			this.butAssessed.UseVisualStyleBackColor = true;
			this.butAssessed.Click += new System.EventHandler(this.butAssessed_Click);
			// 
			// butIntervention
			// 
			this.butIntervention.Location = new System.Drawing.Point(198, 107);
			this.butIntervention.Name = "butIntervention";
			this.butIntervention.Size = new System.Drawing.Size(75, 23);
			this.butIntervention.TabIndex = 20;
			this.butIntervention.Text = "Intervention";
			this.butIntervention.UseVisualStyleBackColor = true;
			this.butIntervention.Click += new System.EventHandler(this.butIntervention_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(413, 67);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(209, 30);
			this.label1.TabIndex = 24;
			this.label1.Text = "The patient\'s current smoking status.  Used for calculating MU measures.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(279, 109);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(411, 18);
			this.label4.TabIndex = 25;
			this.label4.Text = "The history is used for calculating Tabacco Assessment and Cessation CQMs";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboAssessmentType
			// 
			this.comboAssessmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAssessmentType.DropDownWidth = 350;
			this.comboAssessmentType.FormattingEnabled = true;
			this.comboAssessmentType.Location = new System.Drawing.Point(117, 44);
			this.comboAssessmentType.Name = "comboAssessmentType";
			this.comboAssessmentType.Size = new System.Drawing.Size(260, 21);
			this.comboAssessmentType.TabIndex = 27;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(13, 44);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 17);
			this.label5.TabIndex = 26;
			this.label5.Text = "Assessment Type";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butSnomed
			// 
			this.butSnomed.Location = new System.Drawing.Point(383, 71);
			this.butSnomed.Name = "butSnomed";
			this.butSnomed.Size = new System.Drawing.Size(24, 21);
			this.butSnomed.TabIndex = 28;
			this.butSnomed.Text = "...";
			this.butSnomed.UseVisualStyleBackColor = true;
			this.butSnomed.Click += new System.EventHandler(this.butSnomed_Click);
			// 
			// textDateAssessed
			// 
			this.textDateAssessed.Location = new System.Drawing.Point(117, 18);
			this.textDateAssessed.Name = "textDateAssessed";
			this.textDateAssessed.ReadOnly = true;
			this.textDateAssessed.Size = new System.Drawing.Size(140, 20);
			this.textDateAssessed.TabIndex = 29;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(13, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 17);
			this.label6.TabIndex = 30;
			this.label6.Text = "Date Assessed";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(117, 136);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(573, 263);
			this.gridMain.TabIndex = 19;
			this.gridMain.Title = "History";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// FormEhrPatientSmoking
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(702, 447);
			this.Controls.Add(this.textDateAssessed);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.butSnomed);
			this.Controls.Add(this.comboAssessmentType);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butIntervention);
			this.Controls.Add(this.butAssessed);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.comboSmokeStatus);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormEhrPatientSmoking";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Patient Smoking";
			this.Load += new System.EventHandler(this.FormPatientSmoking_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboSmokeStatus;
		private System.Windows.Forms.Button butAssessed;
		private OpenDental.UI.ODGrid gridMain;
		private System.Windows.Forms.Button butIntervention;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboAssessmentType;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button butSnomed;
		private System.Windows.Forms.TextBox textDateAssessed;
		private System.Windows.Forms.Label label6;
	}
}