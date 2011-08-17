namespace OpenDental{
	partial class FormClaimPayList {
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
			this.labelFromDate = new System.Windows.Forms.Label();
			this.labelToDate = new System.Windows.Forms.Label();
			this.labelClinic = new System.Windows.Forms.Label();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butRefresh = new OpenDental.UI.Button();
			this.textDateTo = new OpenDental.ValidDate();
			this.butClose = new OpenDental.UI.Button();
			this.textDateFrom = new OpenDental.ValidDate();
			this.SuspendLayout();
			// 
			// labelFromDate
			// 
			this.labelFromDate.Location = new System.Drawing.Point(24,18);
			this.labelFromDate.Name = "labelFromDate";
			this.labelFromDate.Size = new System.Drawing.Size(77,14);
			this.labelFromDate.TabIndex = 11;
			this.labelFromDate.Text = "From Date";
			this.labelFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelToDate
			// 
			this.labelToDate.Location = new System.Drawing.Point(206,17);
			this.labelToDate.Name = "labelToDate";
			this.labelToDate.Size = new System.Drawing.Size(77,14);
			this.labelToDate.TabIndex = 12;
			this.labelToDate.Text = "To Date";
			this.labelToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(419,19);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(91,14);
			this.labelClinic.TabIndex = 22;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(512,15);
			this.comboClinic.MaxDropDownItems = 40;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(181,21);
			this.comboClinic.TabIndex = 23;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,42);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(851,526);
			this.gridMain.TabIndex = 4;
			this.gridMain.Title = "Claim Payments (EOBs)";
			this.gridMain.TranslationName = null;
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(779,12);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(84,24);
			this.butRefresh.TabIndex = 24;
			this.butRefresh.Text = "&Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(286,15);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(94,20);
			this.textDateTo.TabIndex = 14;
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(788,576);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(104,15);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(94,20);
			this.textDateFrom.TabIndex = 13;
			// 
			// FormClaimPayList
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(888,612);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.textDateTo);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.textDateFrom);
			this.Controls.Add(this.labelToDate);
			this.Controls.Add(this.labelFromDate);
			this.Name = "FormClaimPayList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Claim Payment (EOB) List";
			this.Load += new System.EventHandler(this.FormClaimPayList_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.Label labelFromDate;
		private System.Windows.Forms.Label labelToDate;
		private ValidDate textDateFrom;
		private ValidDate textDateTo;
		private System.Windows.Forms.Label labelClinic;
		private System.Windows.Forms.ComboBox comboClinic;
		private UI.Button butRefresh;
	}
}