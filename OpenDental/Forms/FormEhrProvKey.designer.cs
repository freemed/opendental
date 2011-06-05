namespace OpenDental{
	partial class FormEhrProvKey {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEhrProvKey));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textEhrKey = new System.Windows.Forms.TextBox();
			this.labelEhrKey = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textLName = new System.Windows.Forms.TextBox();
			this.textFName = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
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
			this.butOK.Location = new System.Drawing.Point(276,211);
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
			this.butCancel.Location = new System.Drawing.Point(366,211);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textEhrKey
			// 
			this.textEhrKey.Location = new System.Drawing.Point(122,156);
			this.textEhrKey.MaxLength = 15;
			this.textEhrKey.Name = "textEhrKey";
			this.textEhrKey.Size = new System.Drawing.Size(161,20);
			this.textEhrKey.TabIndex = 105;
			// 
			// labelEhrKey
			// 
			this.labelEhrKey.Location = new System.Drawing.Point(34,160);
			this.labelEhrKey.Name = "labelEhrKey";
			this.labelEhrKey.Size = new System.Drawing.Size(88,14);
			this.labelEhrKey.TabIndex = 106;
			this.labelEhrKey.Text = "EHR Key";
			this.labelEhrKey.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(422,89);
			this.label1.TabIndex = 107;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(122,110);
			this.textLName.MaxLength = 100;
			this.textLName.Name = "textLName";
			this.textLName.ReadOnly = true;
			this.textLName.Size = new System.Drawing.Size(161,20);
			this.textLName.TabIndex = 108;
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(122,133);
			this.textFName.MaxLength = 100;
			this.textFName.Name = "textFName";
			this.textFName.ReadOnly = true;
			this.textFName.Size = new System.Drawing.Size(161,20);
			this.textFName.TabIndex = 109;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(-12,114);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(132,14);
			this.label10.TabIndex = 111;
			this.label10.Text = "Last Name";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(-6,137);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(127,14);
			this.label8.TabIndex = 110;
			this.label8.Text = "First Name";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormEhrProvKey
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(453,247);
			this.Controls.Add(this.textLName);
			this.Controls.Add(this.textFName);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textEhrKey);
			this.Controls.Add(this.labelEhrKey);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormEhrProvKey";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "EHR Provider Key";
			this.Load += new System.EventHandler(this.FormEhrProvKey_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textEhrKey;
		private System.Windows.Forms.Label labelEhrKey;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.TextBox textFName;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label8;
	}
}