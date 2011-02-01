namespace OpenDental{
	partial class FormCreditCardEdit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreditCardEdit));
			this.label3 = new System.Windows.Forms.Label();
			this.textCardNumber = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textExpDate = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textNameOnCard = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textSecurityCode = new System.Windows.Forms.TextBox();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textZip = new System.Windows.Forms.TextBox();
			this.textState = new System.Windows.Forms.TextBox();
			this.labelST = new System.Windows.Forms.Label();
			this.labelAddress = new System.Windows.Forms.Label();
			this.labelCity = new System.Windows.Forms.Label();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.labelZip = new System.Windows.Forms.Label();
			this.textCity = new System.Windows.Forms.TextBox();
			this.butDelete = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textType = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12,9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,16);
			this.label3.TabIndex = 8;
			this.label3.Text = "Card Number";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textCardNumber
			// 
			this.textCardNumber.Location = new System.Drawing.Point(12,28);
			this.textCardNumber.Name = "textCardNumber";
			this.textCardNumber.Size = new System.Drawing.Size(240,20);
			this.textCardNumber.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(276,51);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84,16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Exp (MMYY)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textExpDate
			// 
			this.textExpDate.Location = new System.Drawing.Point(279,70);
			this.textExpDate.Name = "textExpDate";
			this.textExpDate.Size = new System.Drawing.Size(71,20);
			this.textExpDate.TabIndex = 3;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12,93);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,16);
			this.label5.TabIndex = 12;
			this.label5.Text = "Name On Card";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textNameOnCard
			// 
			this.textNameOnCard.Location = new System.Drawing.Point(12,112);
			this.textNameOnCard.Name = "textNameOnCard";
			this.textNameOnCard.Size = new System.Drawing.Size(240,20);
			this.textNameOnCard.TabIndex = 4;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(276,9);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(84,16);
			this.label6.TabIndex = 14;
			this.label6.Text = "Security Code";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textSecurityCode
			// 
			this.textSecurityCode.Location = new System.Drawing.Point(279,28);
			this.textSecurityCode.Name = "textSecurityCode";
			this.textSecurityCode.Size = new System.Drawing.Size(71,20);
			this.textSecurityCode.TabIndex = 2;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(194,285);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 10;
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
			this.butCancel.Location = new System.Drawing.Point(275,285);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 11;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(12,240);
			this.textZip.MaxLength = 100;
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(136,20);
			this.textZip.TabIndex = 8;
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(279,199);
			this.textState.MaxLength = 100;
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(71,20);
			this.textState.TabIndex = 7;
			// 
			// labelST
			// 
			this.labelST.Location = new System.Drawing.Point(279,182);
			this.labelST.Name = "labelST";
			this.labelST.Size = new System.Drawing.Size(71,14);
			this.labelST.TabIndex = 65;
			this.labelST.Text = "ST";
			this.labelST.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelAddress
			// 
			this.labelAddress.Location = new System.Drawing.Point(12,135);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(99,16);
			this.labelAddress.TabIndex = 63;
			this.labelAddress.Text = "Address";
			this.labelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelCity
			// 
			this.labelCity.Location = new System.Drawing.Point(12,177);
			this.labelCity.Name = "labelCity";
			this.labelCity.Size = new System.Drawing.Size(75,19);
			this.labelCity.TabIndex = 64;
			this.labelCity.Text = "City";
			this.labelCity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(12,154);
			this.textAddress.MaxLength = 100;
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(338,20);
			this.textAddress.TabIndex = 5;
			// 
			// labelZip
			// 
			this.labelZip.Location = new System.Drawing.Point(12,222);
			this.labelZip.Name = "labelZip";
			this.labelZip.Size = new System.Drawing.Size(96,16);
			this.labelZip.TabIndex = 66;
			this.labelZip.Text = "Zip";
			this.labelZip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(12,199);
			this.textCity.MaxLength = 100;
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(240,20);
			this.textCity.TabIndex = 6;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(15,285);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,24);
			this.butDelete.TabIndex = 69;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,51);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,16);
			this.label1.TabIndex = 70;
			this.label1.Text = "Credit Card Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textType
			// 
			this.textType.Location = new System.Drawing.Point(12,70);
			this.textType.Name = "textType";
			this.textType.Size = new System.Drawing.Size(240,20);
			this.textType.TabIndex = 71;
			// 
			// FormCreditCardEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(362,321);
			this.Controls.Add(this.textType);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textZip);
			this.Controls.Add(this.textState);
			this.Controls.Add(this.labelST);
			this.Controls.Add(this.labelAddress);
			this.Controls.Add(this.labelCity);
			this.Controls.Add(this.textAddress);
			this.Controls.Add(this.labelZip);
			this.Controls.Add(this.textCity);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.textSecurityCode);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textNameOnCard);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textExpDate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textCardNumber);
			this.Controls.Add(this.label3);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormCreditCardEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Credit Card Edit";
			this.Load += new System.EventHandler(this.FormPayConnect_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textCardNumber;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textExpDate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textNameOnCard;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textSecurityCode;
		private System.Drawing.Printing.PrintDocument pd2;
		private UI.Button butOK;
		private UI.Button butCancel;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.Label labelST;
		private System.Windows.Forms.Label labelAddress;
		private System.Windows.Forms.Label labelCity;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.Label labelZip;
		private System.Windows.Forms.TextBox textCity;
		private UI.Button butDelete;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textType;
	}
}