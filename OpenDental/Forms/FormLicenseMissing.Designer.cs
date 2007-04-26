namespace OpenDental {
	partial class FormLicenseMissing {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLicenseMissing));
			this.butPrint = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.codeGrid = new OpenDental.UI.ODGrid();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butPrint
			// 
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(299,608);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(75,26);
			this.butPrint.TabIndex = 1;
			this.butPrint.Text = "Print";
			this.butPrint.UseVisualStyleBackColor = true;
			this.butPrint.Visible = false;
			this.butPrint.Click += new System.EventHandler(this.printbutton_Click);
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Location = new System.Drawing.Point(527,608);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.okbutton_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(590,38);
			this.label1.TabIndex = 3;
			this.label1.Text = "Some codes still need to be entered because they were used in the past.  This lis" +
    "t should help you to figure out which codes are still missing.";
			// 
			// codeGrid
			// 
			this.codeGrid.HScrollVisible = false;
			this.codeGrid.Location = new System.Drawing.Point(12,50);
			this.codeGrid.Name = "codeGrid";
			this.codeGrid.ScrollValue = 0;
			this.codeGrid.Size = new System.Drawing.Size(590,546);
			this.codeGrid.TabIndex = 0;
			this.codeGrid.Title = null;
			this.codeGrid.TranslationName = null;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12,608);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(206,23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Approximate count: ";
			// 
			// FormLicenseMissing
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(614,646);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.codeGrid);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormLicenseMissing";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Missing Procedure Codes";
			this.Load += new System.EventHandler(this.FormLicenseMissing_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.ODGrid codeGrid;
		private System.Windows.Forms.Button butPrint;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}