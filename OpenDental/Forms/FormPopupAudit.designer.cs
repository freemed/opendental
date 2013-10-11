namespace OpenDental{
	partial class FormPopupAudit {
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
			this.butClose = new OpenDental.UI.Button();
			this.gridPopupAudit = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(620, 246);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// gridPopupAudit
			// 
			this.gridPopupAudit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridPopupAudit.HScrollVisible = false;
			this.gridPopupAudit.Location = new System.Drawing.Point(12, 15);
			this.gridPopupAudit.Name = "gridPopupAudit";
			this.gridPopupAudit.ScrollValue = 0;
			this.gridPopupAudit.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridPopupAudit.Size = new System.Drawing.Size(595, 255);
			this.gridPopupAudit.TabIndex = 4;
			this.gridPopupAudit.Title = "Audit Trail";
			this.gridPopupAudit.TranslationName = "TableAudit";
			this.gridPopupAudit.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPopupAudit_CellDoubleClick);
			// 
			// FormPopupAudit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(720, 297);
			this.Controls.Add(this.gridPopupAudit);
			this.Controls.Add(this.butClose);
			this.Name = "FormPopupAudit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Popup Audit";
			this.Load += new System.EventHandler(this.FormPopupAudit_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private UI.ODGrid gridPopupAudit;
	}
}