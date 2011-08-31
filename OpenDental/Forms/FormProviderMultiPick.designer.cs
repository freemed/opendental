namespace OpenDental{
	partial class FormProvidersMultiPick {
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
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butProvHygenist = new OpenDental.UI.Button();
			this.butProvDentist = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(410,589);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(410,630);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(362,642);
			this.gridMain.TabIndex = 14;
			this.gridMain.Title = "Providers";
			this.gridMain.TranslationName = null;
			// 
			// butProvHygenist
			// 
			this.butProvHygenist.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butProvHygenist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butProvHygenist.Autosize = true;
			this.butProvHygenist.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butProvHygenist.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butProvHygenist.CornerRadius = 4F;
			this.butProvHygenist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butProvHygenist.Location = new System.Drawing.Point(410,149);
			this.butProvHygenist.Name = "butProvHygenist";
			this.butProvHygenist.Size = new System.Drawing.Size(75,24);
			this.butProvHygenist.TabIndex = 90;
			this.butProvHygenist.Text = "Hygienists";
			this.butProvHygenist.Click += new System.EventHandler(this.butProvHygenist_Click);
			// 
			// butProvDentist
			// 
			this.butProvDentist.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butProvDentist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butProvDentist.Autosize = true;
			this.butProvDentist.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butProvDentist.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butProvDentist.CornerRadius = 4F;
			this.butProvDentist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butProvDentist.Location = new System.Drawing.Point(410,108);
			this.butProvDentist.Name = "butProvDentist";
			this.butProvDentist.Size = new System.Drawing.Size(75,24);
			this.butProvDentist.TabIndex = 89;
			this.butProvDentist.Text = "Dentists";
			this.butProvDentist.Click += new System.EventHandler(this.butProvDentist_Click);
			// 
			// FormProvidersMultiPick
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(514,670);
			this.Controls.Add(this.butProvHygenist);
			this.Controls.Add(this.butProvDentist);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.KeyPreview = true;
			this.Name = "FormProvidersMultiPick";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Providers";
			this.Load += new System.EventHandler(this.FormProvidersMultiPick_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private UI.ODGrid gridMain;
		private UI.Button butProvHygenist;
		private UI.Button butProvDentist;
	}
}