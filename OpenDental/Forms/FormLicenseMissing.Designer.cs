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
			this.printbutton = new System.Windows.Forms.Button();
			this.okbutton = new System.Windows.Forms.Button();
			this.codeGrid = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// printbutton
			// 
			this.printbutton.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.printbutton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.printbutton.Location = new System.Drawing.Point(147,456);
			this.printbutton.Name = "printbutton";
			this.printbutton.Size = new System.Drawing.Size(75,23);
			this.printbutton.TabIndex = 1;
			this.printbutton.Text = "Print";
			this.printbutton.UseVisualStyleBackColor = true;
			this.printbutton.Click += new System.EventHandler(this.printbutton_Click);
			// 
			// okbutton
			// 
			this.okbutton.Location = new System.Drawing.Point(228,456);
			this.okbutton.Name = "okbutton";
			this.okbutton.Size = new System.Drawing.Size(75,23);
			this.okbutton.TabIndex = 2;
			this.okbutton.Text = "OK";
			this.okbutton.UseVisualStyleBackColor = true;
			this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
			// 
			// codeGrid
			// 
			this.codeGrid.HScrollVisible = false;
			this.codeGrid.Location = new System.Drawing.Point(12,12);
			this.codeGrid.Name = "codeGrid";
			this.codeGrid.ScrollValue = 0;
			this.codeGrid.Size = new System.Drawing.Size(440,438);
			this.codeGrid.TabIndex = 0;
			this.codeGrid.Title = null;
			this.codeGrid.TranslationName = null;
			// 
			// FormLicenseMissing
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(464,501);
			this.Controls.Add(this.okbutton);
			this.Controls.Add(this.printbutton);
			this.Controls.Add(this.codeGrid);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormLicenseMissing";
			this.Text = "Missing Procedure Codes";
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.ODGrid codeGrid;
		private System.Windows.Forms.Button printbutton;
		private System.Windows.Forms.Button okbutton;
	}
}