namespace OpenDental{
	partial class FormPhoneNumbersManage {
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
			this.label1 = new System.Windows.Forms.Label();
			this.textName = new System.Windows.Forms.TextBox();
			this.textWork = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textHome = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textWireless = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.listOther = new System.Windows.Forms.ListBox();
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
			this.butOK.Enabled = false;
			this.butOK.Location = new System.Drawing.Point(625,442);
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
			this.butCancel.Location = new System.Drawing.Point(625,483);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16,23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(123,16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Office Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textName
			// 
			this.textName.Location = new System.Drawing.Point(141,20);
			this.textName.Name = "textName";
			this.textName.ReadOnly = true;
			this.textName.Size = new System.Drawing.Size(425,20);
			this.textName.TabIndex = 5;
			// 
			// textWork
			// 
			this.textWork.Location = new System.Drawing.Point(141,46);
			this.textWork.Name = "textWork";
			this.textWork.Size = new System.Drawing.Size(199,20);
			this.textWork.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16,49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(123,16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Work Phone";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textHome
			// 
			this.textHome.Location = new System.Drawing.Point(141,72);
			this.textHome.Name = "textHome";
			this.textHome.Size = new System.Drawing.Size(199,20);
			this.textHome.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16,75);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(123,16);
			this.label3.TabIndex = 8;
			this.label3.Text = "Home Phone";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textWireless
			// 
			this.textWireless.Location = new System.Drawing.Point(141,98);
			this.textWireless.Name = "textWireless";
			this.textWireless.Size = new System.Drawing.Size(199,20);
			this.textWireless.TabIndex = 11;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16,101);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(123,16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Wireless Phone";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16,128);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(123,16);
			this.label5.TabIndex = 12;
			this.label5.Text = "Other Numbers";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listOther
			// 
			this.listOther.FormattingEnabled = true;
			this.listOther.Location = new System.Drawing.Point(141,126);
			this.listOther.Name = "listOther";
			this.listOther.Size = new System.Drawing.Size(199,290);
			this.listOther.TabIndex = 13;
			// 
			// FormPhoneNumbersManage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(725,534);
			this.Controls.Add(this.listOther);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textWireless);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textHome);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textWork);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormPhoneNumbersManage";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Phone Numbers";
			this.Load += new System.EventHandler(this.FormPhoneNumbersManage_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textName;
		private System.Windows.Forms.TextBox textWork;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textHome;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textWireless;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ListBox listOther;
	}
}