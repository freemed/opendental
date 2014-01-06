namespace OpenDental {
	partial class FormEhrVaccines {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEhrVaccines));
			this.butSubmitImmunization = new System.Windows.Forms.Button();
			this.butAddVaccine = new System.Windows.Forms.Button();
			this.gridVaccine = new OpenDental.UI.ODGrid();
			this.butClose = new System.Windows.Forms.Button();
			this.butExport = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butSubmitImmunization
			// 
			this.butSubmitImmunization.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSubmitImmunization.Location = new System.Drawing.Point(569, 438);
			this.butSubmitImmunization.Name = "butSubmitImmunization";
			this.butSubmitImmunization.Size = new System.Drawing.Size(86, 23);
			this.butSubmitImmunization.TabIndex = 3;
			this.butSubmitImmunization.Text = "Submit HL7";
			this.butSubmitImmunization.UseVisualStyleBackColor = true;
			this.butSubmitImmunization.Click += new System.EventHandler(this.butSubmitImmunization_Click);
			// 
			// butAddVaccine
			// 
			this.butAddVaccine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddVaccine.Location = new System.Drawing.Point(569, 12);
			this.butAddVaccine.Name = "butAddVaccine";
			this.butAddVaccine.Size = new System.Drawing.Size(86, 23);
			this.butAddVaccine.TabIndex = 2;
			this.butAddVaccine.Text = "Add";
			this.butAddVaccine.UseVisualStyleBackColor = true;
			this.butAddVaccine.Click += new System.EventHandler(this.butAddVaccine_Click);
			// 
			// gridVaccine
			// 
			this.gridVaccine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridVaccine.HScrollVisible = false;
			this.gridVaccine.Location = new System.Drawing.Point(12, 12);
			this.gridVaccine.Name = "gridVaccine";
			this.gridVaccine.ScrollValue = 0;
			this.gridVaccine.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridVaccine.Size = new System.Drawing.Size(547, 492);
			this.gridVaccine.TabIndex = 0;
			this.gridVaccine.Title = "Vaccines";
			this.gridVaccine.TranslationName = null;
			this.gridVaccine.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridVaccine_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Location = new System.Drawing.Point(569, 481);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(86, 23);
			this.butClose.TabIndex = 4;
			this.butClose.Text = "Close";
			this.butClose.UseVisualStyleBackColor = true;
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butExport
			// 
			this.butExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butExport.Location = new System.Drawing.Point(569, 409);
			this.butExport.Name = "butExport";
			this.butExport.Size = new System.Drawing.Size(86, 23);
			this.butExport.TabIndex = 5;
			this.butExport.Text = "Export HL7";
			this.butExport.UseVisualStyleBackColor = true;
			this.butExport.Click += new System.EventHandler(this.butExport_Click);
			// 
			// FormEhrVaccines
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(663, 516);
			this.Controls.Add(this.butExport);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.butAddVaccine);
			this.Controls.Add(this.butSubmitImmunization);
			this.Controls.Add(this.gridVaccine);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormEhrVaccines";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormVaccines";
			this.Load += new System.EventHandler(this.FormVaccines_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button butSubmitImmunization;
		private System.Windows.Forms.Button butAddVaccine;
		private OpenDental.UI.ODGrid gridVaccine;
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Button butExport;
	}
}