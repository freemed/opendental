namespace OpenDental.Eclaims {
	partial class FormCCDPrint {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCCDPrint));
			this.printPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
			this.butBack = new System.Windows.Forms.Button();
			this.butFwd = new System.Windows.Forms.Button();
			this.labelPage = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// printPreviewControl1
			// 
			this.printPreviewControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.printPreviewControl1.Location = new System.Drawing.Point(0,0);
			this.printPreviewControl1.Name = "printPreviewControl1";
			this.printPreviewControl1.Size = new System.Drawing.Size(888,657);
			this.printPreviewControl1.TabIndex = 0;
			// 
			// butBack
			// 
			this.butBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butBack.Location = new System.Drawing.Point(355,662);
			this.butBack.Name = "butBack";
			this.butBack.Size = new System.Drawing.Size(75,23);
			this.butBack.TabIndex = 1;
			this.butBack.Text = "back";
			this.butBack.UseVisualStyleBackColor = true;
			this.butBack.Click += new System.EventHandler(this.butBack_Click);
			// 
			// butFwd
			// 
			this.butFwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butFwd.Location = new System.Drawing.Point(478,662);
			this.butFwd.Name = "butFwd";
			this.butFwd.Size = new System.Drawing.Size(75,23);
			this.butFwd.TabIndex = 2;
			this.butFwd.Text = "forward";
			this.butFwd.UseVisualStyleBackColor = true;
			this.butFwd.Click += new System.EventHandler(this.butFwd_Click);
			// 
			// labelPage
			// 
			this.labelPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelPage.AutoSize = true;
			this.labelPage.Location = new System.Drawing.Point(442,667);
			this.labelPage.Name = "labelPage";
			this.labelPage.Size = new System.Drawing.Size(24,13);
			this.labelPage.TabIndex = 3;
			this.labelPage.Text = "0/0";
			// 
			// FormCCDPrint
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(888,690);
			this.Controls.Add(this.labelPage);
			this.Controls.Add(this.butFwd);
			this.Controls.Add(this.butBack);
			this.Controls.Add(this.printPreviewControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormCCDPrint";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormCCDPrint_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PrintPreviewControl printPreviewControl1;
		private System.Windows.Forms.Button butBack;
		private System.Windows.Forms.Button butFwd;
		private System.Windows.Forms.Label labelPage;
	}
}

