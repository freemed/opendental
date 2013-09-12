namespace OpenDental {
	partial class FormEhrAmendments {
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
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClose = new System.Windows.Forms.Button();
			this.butAdd = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(18, 12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(470, 384);
			this.gridMain.TabIndex = 25;
			this.gridMain.Title = "Amendments";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Location = new System.Drawing.Point(500, 373);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 23);
			this.butClose.TabIndex = 26;
			this.butClose.Text = "Close";
			this.butClose.UseVisualStyleBackColor = true;
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Location = new System.Drawing.Point(500, 184);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 23);
			this.butAdd.TabIndex = 27;
			this.butAdd.Text = "Add";
			this.butAdd.UseVisualStyleBackColor = true;
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormEhrAmendments
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(587, 408);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.gridMain);
			this.Name = "FormEhrAmendments";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Amendments";
			this.Load += new System.EventHandler(this.FormEhrAmendments_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.ODGrid gridMain;
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Button butAdd;
	}
}