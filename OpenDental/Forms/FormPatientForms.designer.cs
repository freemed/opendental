namespace OpenDental{
	partial class FormPatientForms {
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
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butSheets = new OpenDental.UI.Button();
			this.butImage = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
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
			this.gridMain.Size = new System.Drawing.Size(512,477);
			this.gridMain.TabIndex = 4;
			this.gridMain.Title = "Patient Forms";
			this.gridMain.TranslationName = null;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(450,509);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.butImage);
			this.groupBox1.Controls.Add(this.butSheets);
			this.groupBox1.Location = new System.Drawing.Point(12,493);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(161,46);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Setup";
			// 
			// butSheets
			// 
			this.butSheets.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSheets.Autosize = true;
			this.butSheets.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSheets.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSheets.CornerRadius = 4F;
			this.butSheets.Location = new System.Drawing.Point(7,16);
			this.butSheets.Name = "butSheets";
			this.butSheets.Size = new System.Drawing.Size(69,24);
			this.butSheets.TabIndex = 3;
			this.butSheets.Text = "Sheets";
			// 
			// butImage
			// 
			this.butImage.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butImage.Autosize = true;
			this.butImage.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butImage.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butImage.CornerRadius = 4F;
			this.butImage.Location = new System.Drawing.Point(82,16);
			this.butImage.Name = "butImage";
			this.butImage.Size = new System.Drawing.Size(72,24);
			this.butImage.TabIndex = 4;
			this.butImage.Text = "Image Cats";
			// 
			// FormPatientForms
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(535,542);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butCancel);
			this.Name = "FormPatientForms";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Patient Forms";
			this.Load += new System.EventHandler(this.FormPatientForms_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.ODGrid gridMain;
		private System.Windows.Forms.GroupBox groupBox1;
		private OpenDental.UI.Button butSheets;
		private OpenDental.UI.Button butImage;
	}
}