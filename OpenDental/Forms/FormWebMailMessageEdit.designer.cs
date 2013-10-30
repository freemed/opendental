namespace OpenDental{
	partial class FormWebMailMessageEdit {
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
			this.textToAddress = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textFromAddress = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBodyText = new OpenDental.ODtextBox();
			this.butSend = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textSubject = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textToAddress
			// 
			this.textToAddress.Location = new System.Drawing.Point(96, 25);
			this.textToAddress.Name = "textToAddress";
			this.textToAddress.ReadOnly = true;
			this.textToAddress.Size = new System.Drawing.Size(328, 20);
			this.textToAddress.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 14);
			this.label1.TabIndex = 11;
			this.label1.Text = "To:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textFromAddress
			// 
			this.textFromAddress.Location = new System.Drawing.Point(96, 51);
			this.textFromAddress.Name = "textFromAddress";
			this.textFromAddress.ReadOnly = true;
			this.textFromAddress.Size = new System.Drawing.Size(328, 20);
			this.textFromAddress.TabIndex = 12;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(92, 14);
			this.label3.TabIndex = 13;
			this.label3.Text = "From:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 112);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 14);
			this.label2.TabIndex = 13;
			this.label2.Text = "Message:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBodyText
			// 
			this.textBodyText.AcceptsTab = true;
			this.textBodyText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBodyText.DetectUrls = false;
			this.textBodyText.Location = new System.Drawing.Point(96, 112);
			this.textBodyText.Name = "textBodyText";
			this.textBodyText.QuickPasteType = OpenDentBusiness.QuickPasteType.Email;
			this.textBodyText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textBodyText.Size = new System.Drawing.Size(657, 372);
			this.textBodyText.TabIndex = 14;
			this.textBodyText.Text = "";
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.CornerRadius = 4F;
			this.butSend.Location = new System.Drawing.Point(597, 496);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(75, 24);
			this.butSend.TabIndex = 3;
			this.butSend.Text = "&Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(678, 496);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textSubject
			// 
			this.textSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textSubject.Location = new System.Drawing.Point(96, 84);
			this.textSubject.Name = "textSubject";
			this.textSubject.Size = new System.Drawing.Size(657, 20);
			this.textSubject.TabIndex = 15;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(3, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(92, 14);
			this.label4.TabIndex = 16;
			this.label4.Text = "Subject:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormWebMailMessageEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(778, 529);
			this.Controls.Add(this.textSubject);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBodyText);
			this.Controls.Add(this.textFromAddress);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textToAddress);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.butCancel);
			this.Name = "FormWebMailMessageEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Web Mail Message Edit";
			this.Load += new System.EventHandler(this.FormWebMailMessageEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butSend;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textToAddress;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textFromAddress;
		private System.Windows.Forms.Label label3;
		private ODtextBox textBodyText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textSubject;
		private System.Windows.Forms.Label label4;
	}
}