namespace OpenDental{
	partial class FormMobile {
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
			this.textPath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.labelValid = new System.Windows.Forms.Label();
			this.butSync = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.butFullSync = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textPath
			// 
			this.textPath.Location = new System.Drawing.Point(66,31);
			this.textPath.Name = "textPath";
			this.textPath.Size = new System.Drawing.Size(566,20);
			this.textPath.TabIndex = 3;
			this.textPath.TextChanged += new System.EventHandler(this.textPath_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6,31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58,18);
			this.label1.TabIndex = 4;
			this.label1.Text = "Path";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelValid
			// 
			this.labelValid.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelValid.ForeColor = System.Drawing.Color.DarkRed;
			this.labelValid.Location = new System.Drawing.Point(64,10);
			this.labelValid.Name = "labelValid";
			this.labelValid.Size = new System.Drawing.Size(157,18);
			this.labelValid.TabIndex = 6;
			this.labelValid.Text = "Path is not valid";
			this.labelValid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butSync
			// 
			this.butSync.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSync.Autosize = true;
			this.butSync.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSync.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSync.CornerRadius = 4F;
			this.butSync.Enabled = false;
			this.butSync.Location = new System.Drawing.Point(399,106);
			this.butSync.Name = "butSync";
			this.butSync.Size = new System.Drawing.Size(68,24);
			this.butSync.TabIndex = 5;
			this.butSync.Text = "Sync";
			this.butSync.Click += new System.EventHandler(this.butSync_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(557,106);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butFullSync
			// 
			this.butFullSync.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFullSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butFullSync.Autosize = true;
			this.butFullSync.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFullSync.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFullSync.CornerRadius = 4F;
			this.butFullSync.Enabled = false;
			this.butFullSync.Location = new System.Drawing.Point(272,106);
			this.butFullSync.Name = "butFullSync";
			this.butFullSync.Size = new System.Drawing.Size(68,24);
			this.butFullSync.TabIndex = 14;
			this.butFullSync.Text = "Full Sync";
			this.butFullSync.Click += new System.EventHandler(this.butFullSync_Click);
			// 
			// FormMobile
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(657,150);
			this.Controls.Add(this.butFullSync);
			this.Controls.Add(this.labelValid);
			this.Controls.Add(this.butSync);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textPath);
			this.Controls.Add(this.butClose);
			this.Name = "FormMobile";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Mobile Sync";
			this.Load += new System.EventHandler(this.FormMobile_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMobile_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textPath;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butSync;
		private System.Windows.Forms.Label labelValid;
		private OpenDental.UI.Button butFullSync;
	}
}