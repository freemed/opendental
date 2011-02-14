namespace OpenDental{
	partial class FormPayConnect {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPayConnect));
			this.label1 = new System.Windows.Forms.Label();
			this.textAmount = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textCardNumber = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textExpDate = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textNameOnCard = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textSecurityCode = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textZipCode = new System.Windows.Forms.TextBox();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.radioSale = new System.Windows.Forms.RadioButton();
			this.radioAuthorization = new System.Windows.Forms.RadioButton();
			this.radioVoid = new System.Windows.Forms.RadioButton();
			this.radioReturn = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textRefNumber = new System.Windows.Forms.TextBox();
			this.labelRefNumber = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(253,150);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Amount";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(253,169);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(139,20);
			this.textAmount.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(15,66);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,16);
			this.label3.TabIndex = 8;
			this.label3.Text = "Card Number";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textCardNumber
			// 
			this.textCardNumber.Location = new System.Drawing.Point(15,85);
			this.textCardNumber.Name = "textCardNumber";
			this.textCardNumber.Size = new System.Drawing.Size(217,20);
			this.textCardNumber.TabIndex = 1;
			this.textCardNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textCardNumber_KeyPress);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(15,108);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(150,16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Expiration (MMYY)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textExpDate
			// 
			this.textExpDate.Location = new System.Drawing.Point(15,127);
			this.textExpDate.Name = "textExpDate";
			this.textExpDate.Size = new System.Drawing.Size(136,20);
			this.textExpDate.TabIndex = 2;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(15,150);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,16);
			this.label5.TabIndex = 12;
			this.label5.Text = "Name On Card";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textNameOnCard
			// 
			this.textNameOnCard.Location = new System.Drawing.Point(15,169);
			this.textNameOnCard.Name = "textNameOnCard";
			this.textNameOnCard.Size = new System.Drawing.Size(217,20);
			this.textNameOnCard.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(253,66);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100,16);
			this.label6.TabIndex = 14;
			this.label6.Text = "Security Code";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textSecurityCode
			// 
			this.textSecurityCode.Location = new System.Drawing.Point(253,85);
			this.textSecurityCode.Name = "textSecurityCode";
			this.textSecurityCode.Size = new System.Drawing.Size(71,20);
			this.textSecurityCode.TabIndex = 4;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(253,108);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100,16);
			this.label7.TabIndex = 16;
			this.label7.Text = "Zip Code";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textZipCode
			// 
			this.textZipCode.Location = new System.Drawing.Point(253,127);
			this.textZipCode.Name = "textZipCode";
			this.textZipCode.Size = new System.Drawing.Size(139,20);
			this.textZipCode.TabIndex = 5;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(308,217);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 17;
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
			this.butCancel.Location = new System.Drawing.Point(308,258);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 18;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// radioSale
			// 
			this.radioSale.AutoSize = true;
			this.radioSale.Checked = true;
			this.radioSale.Location = new System.Drawing.Point(6,19);
			this.radioSale.Name = "radioSale";
			this.radioSale.Size = new System.Drawing.Size(46,17);
			this.radioSale.TabIndex = 19;
			this.radioSale.TabStop = true;
			this.radioSale.Text = "Sale";
			this.radioSale.UseVisualStyleBackColor = true;
			this.radioSale.Click += new System.EventHandler(this.radioSale_Click);
			// 
			// radioAuthorization
			// 
			this.radioAuthorization.AutoSize = true;
			this.radioAuthorization.Location = new System.Drawing.Point(58,19);
			this.radioAuthorization.Name = "radioAuthorization";
			this.radioAuthorization.Size = new System.Drawing.Size(47,17);
			this.radioAuthorization.TabIndex = 20;
			this.radioAuthorization.Text = "Auth";
			this.radioAuthorization.UseVisualStyleBackColor = true;
			this.radioAuthorization.Click += new System.EventHandler(this.radioAuthorization_Click);
			// 
			// radioVoid
			// 
			this.radioVoid.AutoSize = true;
			this.radioVoid.Location = new System.Drawing.Point(111,19);
			this.radioVoid.Name = "radioVoid";
			this.radioVoid.Size = new System.Drawing.Size(46,17);
			this.radioVoid.TabIndex = 21;
			this.radioVoid.Text = "Void";
			this.radioVoid.UseVisualStyleBackColor = true;
			this.radioVoid.Click += new System.EventHandler(this.radioVoid_Click);
			// 
			// radioReturn
			// 
			this.radioReturn.AutoSize = true;
			this.radioReturn.Location = new System.Drawing.Point(163,19);
			this.radioReturn.Name = "radioReturn";
			this.radioReturn.Size = new System.Drawing.Size(57,17);
			this.radioReturn.TabIndex = 22;
			this.radioReturn.Text = "Return";
			this.radioReturn.UseVisualStyleBackColor = true;
			this.radioReturn.Click += new System.EventHandler(this.radioReturn_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioSale);
			this.groupBox1.Controls.Add(this.radioReturn);
			this.groupBox1.Controls.Add(this.radioAuthorization);
			this.groupBox1.Controls.Add(this.radioVoid);
			this.groupBox1.Location = new System.Drawing.Point(12,12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(236,51);
			this.groupBox1.TabIndex = 23;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Transaction Type";
			// 
			// textRefNumber
			// 
			this.textRefNumber.Location = new System.Drawing.Point(253,39);
			this.textRefNumber.Name = "textRefNumber";
			this.textRefNumber.Size = new System.Drawing.Size(114,20);
			this.textRefNumber.TabIndex = 24;
			this.textRefNumber.Visible = false;
			// 
			// labelRefNumber
			// 
			this.labelRefNumber.Location = new System.Drawing.Point(254,20);
			this.labelRefNumber.Name = "labelRefNumber";
			this.labelRefNumber.Size = new System.Drawing.Size(100,16);
			this.labelRefNumber.TabIndex = 25;
			this.labelRefNumber.Text = "Ref Number";
			this.labelRefNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelRefNumber.Visible = false;
			// 
			// FormPayConnect
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(408,309);
			this.Controls.Add(this.labelRefNumber);
			this.Controls.Add(this.textRefNumber);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.textZipCode);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textSecurityCode);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textNameOnCard);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textExpDate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textCardNumber);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormPayConnect";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Pay Connect Payment Information";
			this.Load += new System.EventHandler(this.FormPayConnect_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textAmount;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textCardNumber;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textExpDate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textNameOnCard;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textSecurityCode;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textZipCode;
		private System.Drawing.Printing.PrintDocument pd2;
		private UI.Button butOK;
		private UI.Button butCancel;
		private System.Windows.Forms.RadioButton radioSale;
		private System.Windows.Forms.RadioButton radioAuthorization;
		private System.Windows.Forms.RadioButton radioVoid;
		private System.Windows.Forms.RadioButton radioReturn;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textRefNumber;
		private System.Windows.Forms.Label labelRefNumber;
	}
}