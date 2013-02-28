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
			this.gridBillingList = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.butProcess = new OpenDental.UI.Button();
			this.SuspendLayout();
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
			this.gridBillingList.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridBillingList.Size = new System.Drawing.Size(918,617);
			this.gridBillingList.TabIndex = 0;
			this.gridBillingList.Title = null;
			this.gridBillingList.TranslationName = null;
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(855,635);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,23);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.UseVisualStyleBackColor = true;
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butProcess
			// 
			this.butProcess.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butProcess.Autosize = true;
			this.butProcess.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butProcess.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butProcess.CornerRadius = 4F;
			this.butProcess.Location = new System.Drawing.Point(774,635);
			this.butProcess.Name = "butProcess";
			this.butProcess.Size = new System.Drawing.Size(75,23);
			this.butProcess.TabIndex = 3;
			this.butProcess.Text = "Process";
			this.butProcess.UseVisualStyleBackColor = true;
			this.butProcess.Click += new System.EventHandler(this.butProcess_Click);
			// 
			// FormNewCropBillingList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(942,673);
			this.Controls.Add(this.butProcess);
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
		private UI.Button butClose;
		private UI.Button butProcess;
	}
}