namespace OpenDental{
	partial class FormTxtMsgEdit {
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
			this.textWirelessPhone = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textMessage = new OpenDental.ODtextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textWirelessPhone
			// 
			this.textWirelessPhone.Location = new System.Drawing.Point(107,21);
			this.textWirelessPhone.Name = "textWirelessPhone";
			this.textWirelessPhone.Size = new System.Drawing.Size(128,20);
			this.textWirelessPhone.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89,20);
			this.label1.TabIndex = 6;
			this.label1.Text = "Phone Number";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(15,46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86,20);
			this.label2.TabIndex = 6;
			this.label2.Text = "Text Message";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMessage
			// 
			this.textMessage.Location = new System.Drawing.Point(107,46);
			this.textMessage.Multiline = true;
			this.textMessage.Name = "textMessage";
			this.textMessage.QuickPasteType = OpenDentBusiness.QuickPasteType.TxtMsg;
			this.textMessage.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textMessage.Size = new System.Drawing.Size(186,83);
			this.textMessage.TabIndex = 4;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(176,152);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&Send";
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
			this.butCancel.Location = new System.Drawing.Point(257,152);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormTxtMsgEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(344,188);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textWirelessPhone);
			this.Controls.Add(this.textMessage);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormTxtMsgEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Text Message";
			this.Load += new System.EventHandler(this.FormTxtMsgEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private ODtextBox textMessage;
		private System.Windows.Forms.TextBox textWirelessPhone;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}