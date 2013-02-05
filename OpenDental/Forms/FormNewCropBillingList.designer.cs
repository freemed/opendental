namespace OpenDental {
	partial class FormNewCropBillingList {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewCropBillingList));
			this.butClose = new System.Windows.Forms.Button();
			this.gridBillingList = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Location = new System.Drawing.Point(855,640);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,23);
			this.butClose.TabIndex = 1;
			this.butClose.Text = "Close";
			this.butClose.UseVisualStyleBackColor = true;
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// gridBillingList
			// 
			this.gridBillingList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridBillingList.HScrollVisible = false;
			this.gridBillingList.Location = new System.Drawing.Point(12,12);
			this.gridBillingList.Name = "gridBillingList";
			this.gridBillingList.ScrollValue = 0;
			this.gridBillingList.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridBillingList.Size = new System.Drawing.Size(918,622);
			this.gridBillingList.TabIndex = 0;
			this.gridBillingList.Title = null;
			this.gridBillingList.TranslationName = null;
			// 
			// FormNewCropBillingList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(942,673);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.gridBillingList);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(950,700);
			this.Name = "FormNewCropBillingList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "NewCrop Billing List";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormBillingList_Load);
			this.Resize += new System.EventHandler(this.FormBillingList_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.ODGrid gridBillingList;
		private System.Windows.Forms.Button butClose;
	}
}