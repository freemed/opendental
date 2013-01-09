namespace OpenDental{
	partial class FormEmailAddresses {
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
			this.butAdd = new OpenDental.UI.Button();
			this.butSetDefault = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(18,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(391,434);
			this.gridMain.TabIndex = 4;
			this.gridMain.Title = "Email Addresses";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(435,136);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,24);
			this.butAdd.TabIndex = 3;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butSetDefault
			// 
			this.butSetDefault.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSetDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSetDefault.Autosize = true;
			this.butSetDefault.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetDefault.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetDefault.CornerRadius = 4F;
			this.butSetDefault.Location = new System.Drawing.Point(435,56);
			this.butSetDefault.Name = "butSetDefault";
			this.butSetDefault.Size = new System.Drawing.Size(75,24);
			this.butSetDefault.TabIndex = 3;
			this.butSetDefault.Text = "Set Default";
			this.butSetDefault.Click += new System.EventHandler(this.butSetDefault_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(435,422);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormEmailAddresses
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(529,464);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butSetDefault);
			this.Controls.Add(this.butClose);
			this.Name = "FormEmailAddresses";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Email Addresses";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEmailAddresses_FormClosing);
			this.Load += new System.EventHandler(this.FormEmailAddresses_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butSetDefault;
		private OpenDental.UI.Button butClose;
		private UI.ODGrid gridMain;
		private UI.Button butAdd;
	}
}