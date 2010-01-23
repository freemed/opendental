namespace OpenDental{
	partial class FormEtrans270EBraw {
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
			this.label1 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.textRaw = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(14,13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(449,18);
			this.label1.TabIndex = 4;
			this.label1.Text = "Raw benefit information as received from the insurance company";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(16,166);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(401,258);
			this.gridMain.TabIndex = 21;
			this.gridMain.Title = "EB (benefit) Elements";
			this.gridMain.TranslationName = "FormEtrans270EBraw";
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(472,401);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// textRaw
			// 
			this.textRaw.Location = new System.Drawing.Point(16,34);
			this.textRaw.Name = "textRaw";
			this.textRaw.ReadOnly = true;
			this.textRaw.Size = new System.Drawing.Size(401,126);
			this.textRaw.TabIndex = 23;
			this.textRaw.Text = "";
			this.textRaw.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.textRaw_LinkClicked);
			// 
			// FormEtrans270EBraw
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(572,452);
			this.Controls.Add(this.textRaw);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butClose);
			this.Name = "FormEtrans270EBraw";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Raw Benefit Info";
			this.Load += new System.EventHandler(this.FormEtrans270EBraw_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.ODGrid gridMain;
		private System.Windows.Forms.RichTextBox textRaw;
	}
}