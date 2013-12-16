namespace OpenDental{
	partial class FormCDSSetup {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCDSSetup));
			this.butClose = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.radioGroup = new System.Windows.Forms.RadioButton();
			this.radioUser = new System.Windows.Forms.RadioButton();
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
			this.butClose.Location = new System.Drawing.Point(647, 493);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 16;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = true;
			this.gridMain.Location = new System.Drawing.Point(12, 54);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(710, 433);
			this.gridMain.TabIndex = 60;
			this.gridMain.Title = "Users";
			this.gridMain.TranslationName = "TableSecurity";
			// 
			// radioGroup
			// 
			this.radioGroup.AutoCheck = false;
			this.radioGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioGroup.Location = new System.Drawing.Point(12, 30);
			this.radioGroup.Name = "radioGroup";
			this.radioGroup.Size = new System.Drawing.Size(158, 18);
			this.radioGroup.TabIndex = 62;
			this.radioGroup.Text = "by Group";
			this.radioGroup.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// radioUser
			// 
			this.radioUser.AutoCheck = false;
			this.radioUser.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUser.Location = new System.Drawing.Point(12, 8);
			this.radioUser.Name = "radioUser";
			this.radioUser.Size = new System.Drawing.Size(91, 22);
			this.radioUser.TabIndex = 61;
			this.radioUser.Text = "by User";
			// 
			// FormCDSSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(734, 529);
			this.Controls.Add(this.radioGroup);
			this.Controls.Add(this.radioUser);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormCDSSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Clinical Decision Support Setup";
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.RadioButton radioGroup;
		private System.Windows.Forms.RadioButton radioUser;
	}
}