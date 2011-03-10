namespace OpenDental{
	partial class FormXchargeTrans {
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
			this.labelCashBackAmt = new System.Windows.Forms.Label();
			this.comboTransType = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new OpenDental.UI.Button();
			this.textCashBackAmt = new ODR.ValidDouble();
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
			this.butOK.Location = new System.Drawing.Point(180,140);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// labelCashBackAmt
			// 
			this.labelCashBackAmt.Location = new System.Drawing.Point(28,80);
			this.labelCashBackAmt.Name = "labelCashBackAmt";
			this.labelCashBackAmt.Size = new System.Drawing.Size(154,16);
			this.labelCashBackAmt.TabIndex = 60;
			this.labelCashBackAmt.Text = "Cash Back Amount";
			this.labelCashBackAmt.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.labelCashBackAmt.Visible = false;
			// 
			// comboTransType
			// 
			this.comboTransType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboTransType.FormattingEnabled = true;
			this.comboTransType.Location = new System.Drawing.Point(30,50);
			this.comboTransType.MaxDropDownItems = 25;
			this.comboTransType.Name = "comboTransType";
			this.comboTransType.Size = new System.Drawing.Size(225,21);
			this.comboTransType.TabIndex = 57;
			this.comboTransType.SelectionChangeCommitted += new System.EventHandler(this.comboTransType_SelectionChangeCommitted);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28,31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(154,16);
			this.label1.TabIndex = 58;
			this.label1.Text = "Transaction Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// button1
			// 
			this.button1.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Autosize = true;
			this.button1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button1.CornerRadius = 4F;
			this.button1.Location = new System.Drawing.Point(-265,-191);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75,26);
			this.button1.TabIndex = 56;
			this.button1.Text = "&OK";
			// 
			// textCashBackAmt
			// 
			this.textCashBackAmt.Location = new System.Drawing.Point(30,99);
			this.textCashBackAmt.Name = "textCashBackAmt";
			this.textCashBackAmt.Size = new System.Drawing.Size(100,20);
			this.textCashBackAmt.TabIndex = 61;
			this.textCashBackAmt.Visible = false;
			// 
			// FormXchargeTrans
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(282,191);
			this.Controls.Add(this.textCashBackAmt);
			this.Controls.Add(this.labelCashBackAmt);
			this.Controls.Add(this.comboTransType);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.butOK);
			this.Name = "FormXchargeTrans";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "X-Charge Transaction Types";
			this.Load += new System.EventHandler(this.FormXchargeTrans_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label labelCashBackAmt;
		private System.Windows.Forms.ComboBox comboTransType;
		private System.Windows.Forms.Label label1;
		private UI.Button button1;
		private ODR.ValidDouble textCashBackAmt;
	}
}