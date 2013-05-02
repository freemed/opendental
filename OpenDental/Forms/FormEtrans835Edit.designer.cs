namespace OpenDental{
	partial class FormEtrans835Edit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEtrans835Edit));
			this.gridClaimDetails = new OpenDental.UI.ODGrid();
			this.label5 = new System.Windows.Forms.Label();
			this.textTransHandlingDesc = new System.Windows.Forms.TextBox();
			this.butRawMessage = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textPaymentMethod = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textPaymentAmount = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textCreditOrDebit = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textAcctNumEndingIn = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textDateEffective = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textCheckNumOrRefNum = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textPayerName = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textPayerID = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textPayerAddress1 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textPayerCity = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textPayerState = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.textPayerZip = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.textPayerContactInfo = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.textPayeeName = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.textPayeeIdType = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textPayeeID = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.gridProviderAdjustments = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// gridClaimDetails
			// 
			this.gridClaimDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridClaimDetails.HScrollVisible = false;
			this.gridClaimDetails.Location = new System.Drawing.Point(9,245);
			this.gridClaimDetails.Name = "gridClaimDetails";
			this.gridClaimDetails.ScrollValue = 0;
			this.gridClaimDetails.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridClaimDetails.Size = new System.Drawing.Size(964,424);
			this.gridClaimDetails.TabIndex = 0;
			this.gridClaimDetails.TabStop = false;
			this.gridClaimDetails.Title = "Claim Details";
			this.gridClaimDetails.TranslationName = "FormEtrans835Edit";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(502,1);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(114,20);
			this.label5.TabIndex = 136;
			this.label5.Text = "Trans Handling Desc";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTransHandlingDesc
			// 
			this.textTransHandlingDesc.Location = new System.Drawing.Point(616,1);
			this.textTransHandlingDesc.Name = "textTransHandlingDesc";
			this.textTransHandlingDesc.ReadOnly = true;
			this.textTransHandlingDesc.Size = new System.Drawing.Size(357,20);
			this.textTransHandlingDesc.TabIndex = 137;
			// 
			// butRawMessage
			// 
			this.butRawMessage.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRawMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRawMessage.Autosize = true;
			this.butRawMessage.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRawMessage.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRawMessage.CornerRadius = 4F;
			this.butRawMessage.Location = new System.Drawing.Point(891,121);
			this.butRawMessage.Name = "butRawMessage";
			this.butRawMessage.Size = new System.Drawing.Size(82,24);
			this.butRawMessage.TabIndex = 116;
			this.butRawMessage.Text = "Raw Message";
			this.butRawMessage.Click += new System.EventHandler(this.butRawMessage_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(898,675);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textPaymentMethod
			// 
			this.textPaymentMethod.Location = new System.Drawing.Point(616,21);
			this.textPaymentMethod.Name = "textPaymentMethod";
			this.textPaymentMethod.ReadOnly = true;
			this.textPaymentMethod.Size = new System.Drawing.Size(357,20);
			this.textPaymentMethod.TabIndex = 139;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(502,21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(114,20);
			this.label1.TabIndex = 138;
			this.label1.Text = "Payment Method";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPaymentAmount
			// 
			this.textPaymentAmount.Location = new System.Drawing.Point(616,61);
			this.textPaymentAmount.Name = "textPaymentAmount";
			this.textPaymentAmount.ReadOnly = true;
			this.textPaymentAmount.Size = new System.Drawing.Size(90,20);
			this.textPaymentAmount.TabIndex = 141;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(502,61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(114,20);
			this.label2.TabIndex = 140;
			this.label2.Text = "Payment Amount";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCreditOrDebit
			// 
			this.textCreditOrDebit.Location = new System.Drawing.Point(616,81);
			this.textCreditOrDebit.Name = "textCreditOrDebit";
			this.textCreditOrDebit.ReadOnly = true;
			this.textCreditOrDebit.Size = new System.Drawing.Size(90,20);
			this.textCreditOrDebit.TabIndex = 143;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(502,81);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(114,20);
			this.label3.TabIndex = 142;
			this.label3.Text = "Credit or Debit";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAcctNumEndingIn
			// 
			this.textAcctNumEndingIn.Location = new System.Drawing.Point(616,101);
			this.textAcctNumEndingIn.Name = "textAcctNumEndingIn";
			this.textAcctNumEndingIn.ReadOnly = true;
			this.textAcctNumEndingIn.Size = new System.Drawing.Size(90,20);
			this.textAcctNumEndingIn.TabIndex = 145;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(502,101);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(114,20);
			this.label4.TabIndex = 144;
			this.label4.Text = "Acct Num Ending In";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateEffective
			// 
			this.textDateEffective.Location = new System.Drawing.Point(616,121);
			this.textDateEffective.Name = "textDateEffective";
			this.textDateEffective.ReadOnly = true;
			this.textDateEffective.Size = new System.Drawing.Size(90,20);
			this.textDateEffective.TabIndex = 147;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(502,121);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(114,20);
			this.label6.TabIndex = 146;
			this.label6.Text = "Date Effective";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCheckNumOrRefNum
			// 
			this.textCheckNumOrRefNum.Location = new System.Drawing.Point(616,41);
			this.textCheckNumOrRefNum.Name = "textCheckNumOrRefNum";
			this.textCheckNumOrRefNum.ReadOnly = true;
			this.textCheckNumOrRefNum.Size = new System.Drawing.Size(357,20);
			this.textCheckNumOrRefNum.TabIndex = 149;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(489,41);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(127,20);
			this.label7.TabIndex = 148;
			this.label7.Text = "Check# or Reference#";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayerName
			// 
			this.textPayerName.Location = new System.Drawing.Point(127,1);
			this.textPayerName.Name = "textPayerName";
			this.textPayerName.ReadOnly = true;
			this.textPayerName.Size = new System.Drawing.Size(357,20);
			this.textPayerName.TabIndex = 151;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(0,1);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(127,20);
			this.label8.TabIndex = 150;
			this.label8.Text = "Payer Name";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayerID
			// 
			this.textPayerID.Location = new System.Drawing.Point(127,21);
			this.textPayerID.Name = "textPayerID";
			this.textPayerID.ReadOnly = true;
			this.textPayerID.Size = new System.Drawing.Size(90,20);
			this.textPayerID.TabIndex = 153;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(0,21);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(127,20);
			this.label9.TabIndex = 152;
			this.label9.Text = "Payer ID";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayerAddress1
			// 
			this.textPayerAddress1.Location = new System.Drawing.Point(127,41);
			this.textPayerAddress1.Name = "textPayerAddress1";
			this.textPayerAddress1.ReadOnly = true;
			this.textPayerAddress1.Size = new System.Drawing.Size(357,20);
			this.textPayerAddress1.TabIndex = 155;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(0,41);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(127,20);
			this.label10.TabIndex = 154;
			this.label10.Text = "Payer Address";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayerCity
			// 
			this.textPayerCity.Location = new System.Drawing.Point(127,61);
			this.textPayerCity.Name = "textPayerCity";
			this.textPayerCity.ReadOnly = true;
			this.textPayerCity.Size = new System.Drawing.Size(357,20);
			this.textPayerCity.TabIndex = 157;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(0,61);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(127,20);
			this.label11.TabIndex = 156;
			this.label11.Text = "Payer City";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayerState
			// 
			this.textPayerState.Location = new System.Drawing.Point(127,81);
			this.textPayerState.Name = "textPayerState";
			this.textPayerState.ReadOnly = true;
			this.textPayerState.Size = new System.Drawing.Size(90,20);
			this.textPayerState.TabIndex = 159;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(0,81);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(127,20);
			this.label12.TabIndex = 158;
			this.label12.Text = "Payer State";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayerZip
			// 
			this.textPayerZip.Location = new System.Drawing.Point(127,101);
			this.textPayerZip.Name = "textPayerZip";
			this.textPayerZip.ReadOnly = true;
			this.textPayerZip.Size = new System.Drawing.Size(90,20);
			this.textPayerZip.TabIndex = 161;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(0,101);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(127,20);
			this.label13.TabIndex = 160;
			this.label13.Text = "Payer Zip";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayerContactInfo
			// 
			this.textPayerContactInfo.Location = new System.Drawing.Point(127,121);
			this.textPayerContactInfo.Name = "textPayerContactInfo";
			this.textPayerContactInfo.ReadOnly = true;
			this.textPayerContactInfo.Size = new System.Drawing.Size(357,20);
			this.textPayerContactInfo.TabIndex = 163;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(0,121);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(127,20);
			this.label14.TabIndex = 162;
			this.label14.Text = "Payer Contact Info";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayeeName
			// 
			this.textPayeeName.Location = new System.Drawing.Point(127,147);
			this.textPayeeName.Name = "textPayeeName";
			this.textPayeeName.ReadOnly = true;
			this.textPayeeName.Size = new System.Drawing.Size(357,20);
			this.textPayeeName.TabIndex = 165;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(0,147);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(127,20);
			this.label15.TabIndex = 164;
			this.label15.Text = "Payee Name";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayeeIdType
			// 
			this.textPayeeIdType.Location = new System.Drawing.Point(127,167);
			this.textPayeeIdType.Name = "textPayeeIdType";
			this.textPayeeIdType.ReadOnly = true;
			this.textPayeeIdType.Size = new System.Drawing.Size(90,20);
			this.textPayeeIdType.TabIndex = 167;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(0,167);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(127,20);
			this.label16.TabIndex = 166;
			this.label16.Text = "Payee ID Type";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayeeID
			// 
			this.textPayeeID.Location = new System.Drawing.Point(127,187);
			this.textPayeeID.Name = "textPayeeID";
			this.textPayeeID.ReadOnly = true;
			this.textPayeeID.Size = new System.Drawing.Size(90,20);
			this.textPayeeID.TabIndex = 169;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(0,187);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(127,20);
			this.label17.TabIndex = 168;
			this.label17.Text = "Payee ID";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridProviderAdjustments
			// 
			this.gridProviderAdjustments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridProviderAdjustments.HScrollVisible = false;
			this.gridProviderAdjustments.Location = new System.Drawing.Point(502,147);
			this.gridProviderAdjustments.Name = "gridProviderAdjustments";
			this.gridProviderAdjustments.ScrollValue = 0;
			this.gridProviderAdjustments.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridProviderAdjustments.Size = new System.Drawing.Size(471,92);
			this.gridProviderAdjustments.TabIndex = 170;
			this.gridProviderAdjustments.TabStop = false;
			this.gridProviderAdjustments.Title = "Provider Adjustments";
			this.gridProviderAdjustments.TranslationName = "FormEtrans835Edit";
			// 
			// FormEtrans835Edit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(982,707);
			this.Controls.Add(this.gridProviderAdjustments);
			this.Controls.Add(this.textPayeeID);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.textPayeeIdType);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.textPayeeName);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.textPayerContactInfo);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.textPayerZip);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.textPayerState);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.textPayerCity);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textPayerAddress1);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textPayerID);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.textPayerName);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textCheckNumOrRefNum);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textDateEffective);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textAcctNumEndingIn);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textCreditOrDebit);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textPaymentAmount);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textPaymentMethod);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textTransHandlingDesc);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.butRawMessage);
			this.Controls.Add(this.gridClaimDetails);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(990,734);
			this.Name = "FormEtrans835Edit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Electronic EOB";
			this.Load += new System.EventHandler(this.FormEtrans835Edit_Load);
			this.Resize += new System.EventHandler(this.FormEtrans835Edit_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.ODGrid gridClaimDetails;
		private UI.Button butRawMessage;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textTransHandlingDesc;
		private System.Windows.Forms.TextBox textPaymentMethod;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textPaymentAmount;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textCreditOrDebit;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textAcctNumEndingIn;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textDateEffective;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textCheckNumOrRefNum;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textPayerName;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textPayerID;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textPayerAddress1;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textPayerCity;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textPayerState;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textPayerZip;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textPayerContactInfo;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textPayeeName;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textPayeeIdType;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox textPayeeID;
		private System.Windows.Forms.Label label17;
		private UI.ODGrid gridProviderAdjustments;
	}
}