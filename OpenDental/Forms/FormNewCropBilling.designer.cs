namespace OpenDental {
	partial class FormNewCropBilling {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewCropBilling));
			this.gridBillingList = new OpenDental.UI.ODGrid();
			this.textBillingXmlPath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.butLoad = new OpenDental.UI.Button();
			this.butBrowse = new OpenDental.UI.Button();
			this.butProcess = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// gridBillingList
			// 
			this.gridBillingList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridBillingList.HScrollVisible = false;
			this.gridBillingList.Location = new System.Drawing.Point(12,59);
			this.gridBillingList.Name = "gridBillingList";
			this.gridBillingList.ScrollValue = 0;
			this.gridBillingList.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridBillingList.Size = new System.Drawing.Size(958,604);
			this.gridBillingList.TabIndex = 0;
			this.gridBillingList.Title = "Providers Using NewCrop";
			this.gridBillingList.TranslationName = null;
			// 
			// textBillingXmlPath
			// 
			this.textBillingXmlPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBillingXmlPath.Location = new System.Drawing.Point(12,32);
			this.textBillingXmlPath.Name = "textBillingXmlPath";
			this.textBillingXmlPath.Size = new System.Drawing.Size(826,20);
			this.textBillingXmlPath.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(812,16);
			this.label1.TabIndex = 9;
			this.label1.Text = "Billing.xml file path. Must be downloaded from NewCrop customer portal. See Wiki " +
    "for instructions. Use the Load button to populate the grid after a file is selec" +
    "ted.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "Billing.xml";
			// 
			// butLoad
			// 
			this.butLoad.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butLoad.Autosize = true;
			this.butLoad.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLoad.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLoad.CornerRadius = 4F;
			this.butLoad.Location = new System.Drawing.Point(895,30);
			this.butLoad.Name = "butLoad";
			this.butLoad.Size = new System.Drawing.Size(75,23);
			this.butLoad.TabIndex = 12;
			this.butLoad.Text = "Load";
			this.butLoad.UseVisualStyleBackColor = true;
			this.butLoad.Click += new System.EventHandler(this.butLoad_Click);
			// 
			// butBrowse
			// 
			this.butBrowse.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butBrowse.Autosize = true;
			this.butBrowse.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowse.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowse.CornerRadius = 4F;
			this.butBrowse.Location = new System.Drawing.Point(844,30);
			this.butBrowse.Name = "butBrowse";
			this.butBrowse.Size = new System.Drawing.Size(32,23);
			this.butBrowse.TabIndex = 11;
			this.butBrowse.Text = "...";
			this.butBrowse.UseVisualStyleBackColor = true;
			this.butBrowse.Click += new System.EventHandler(this.butBrowse_Click);
			// 
			// butProcess
			// 
			this.butProcess.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butProcess.Autosize = true;
			this.butProcess.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butProcess.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butProcess.CornerRadius = 4F;
			this.butProcess.Location = new System.Drawing.Point(814,669);
			this.butProcess.Name = "butProcess";
			this.butProcess.Size = new System.Drawing.Size(75,23);
			this.butProcess.TabIndex = 3;
			this.butProcess.Text = "Process";
			this.butProcess.UseVisualStyleBackColor = true;
			this.butProcess.Click += new System.EventHandler(this.butProcess_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(895,669);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,23);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.UseVisualStyleBackColor = true;
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormNewCropBilling
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(982,707);
			this.Controls.Add(this.butLoad);
			this.Controls.Add(this.butBrowse);
			this.Controls.Add(this.textBillingXmlPath);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butProcess);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.gridBillingList);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(950,700);
			this.Name = "FormNewCropBilling";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "NewCrop Billing";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormBillingList_Load);
			this.Resize += new System.EventHandler(this.FormBillingList_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.ODGrid gridBillingList;
		private UI.Button butClose;
		private UI.Button butProcess;
		private UI.Button butBrowse;
		private System.Windows.Forms.TextBox textBillingXmlPath;
		private System.Windows.Forms.Label label1;
		private UI.Button butLoad;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}