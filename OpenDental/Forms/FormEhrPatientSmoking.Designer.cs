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
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.comboSmokeStatus = new System.Windows.Forms.ComboBox();
			this.butAssessed = new System.Windows.Forms.Button();
			this.butCessation = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(615,374);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,23);
			this.butCancel.TabIndex = 8;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Location = new System.Drawing.Point(534,374);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,23);
			this.butOK.TabIndex = 9;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(20,25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93,17);
			this.label2.TabIndex = 11;
			this.label2.Text = "Smoking Status";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboSmokeStatus
			// 
			this.comboSmokeStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSmokeStatus.FormattingEnabled = true;
			this.comboSmokeStatus.Location = new System.Drawing.Point(117,25);
			this.comboSmokeStatus.Name = "comboSmokeStatus";
			this.comboSmokeStatus.Size = new System.Drawing.Size(213,21);
			this.comboSmokeStatus.TabIndex = 12;
			this.comboSmokeStatus.SelectionChangeCommitted += new System.EventHandler(this.comboSmokeStatus_SelectionChangeCommitted);
			// 
			// butAssessed
			// 
			this.butAssessed.Location = new System.Drawing.Point(117,65);
			this.butAssessed.Name = "butAssessed";
			this.butAssessed.Size = new System.Drawing.Size(85,23);
			this.butAssessed.TabIndex = 17;
			this.butAssessed.Text = "Assessed";
			this.butAssessed.UseVisualStyleBackColor = true;
			this.butAssessed.Click += new System.EventHandler(this.butAssessed_Click);
			// 
			// butCessation
			// 
			this.butCessation.Location = new System.Drawing.Point(211,65);
			this.butCessation.Name = "butCessation";
			this.butCessation.Size = new System.Drawing.Size(85,23);
			this.butCessation.TabIndex = 20;
			this.butCessation.Text = "Cessation";
			this.butCessation.UseVisualStyleBackColor = true;
			this.butCessation.Click += new System.EventHandler(this.butCessation_Click);
			// 
			// butDelete
			// 
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Location = new System.Drawing.Point(117,328);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(85,23);
			this.butDelete.TabIndex = 22;
			this.butDelete.Text = "Delete";
			this.butDelete.UseVisualStyleBackColor = true;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.Location = new System.Drawing.Point(208,328);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(263,31);
			this.label3.TabIndex = 23;
			this.label3.Text = "Delete any historical entries from the list above which are not accurate.";
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(117,93);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(533,229);
			this.gridMain.TabIndex = 19;
			this.gridMain.Title = "History";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// FormPatientSmoking
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(702,405);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCessation);
			this.Controls.Add(this.butAssessed);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.comboSmokeStatus);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormPatientSmoking";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Patient Smoking";
			this.Load += new System.EventHandler(this.FormPatientSmoking_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboSmokeStatus;
		private System.Windows.Forms.Button butAssessed;
		private OpenDental.UI.ODGrid gridMain;
		private System.Windows.Forms.Button butCessation;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.Label label3;
	}
}