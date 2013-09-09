namespace OpenDental{
	partial class FormRxAlertEdit {
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
			this.labelName = new System.Windows.Forms.Label();
			this.textMessage = new System.Windows.Forms.TextBox();
			this.labelMessage = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textName = new System.Windows.Forms.TextBox();
			this.textRxName = new System.Windows.Forms.TextBox();
			this.labelRx = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.groupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelName
			// 
			this.labelName.Location = new System.Drawing.Point(67, 18);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(241, 20);
			this.labelName.TabIndex = 7;
			this.labelName.Text = "If the patient already has this Allergy";
			this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMessage
			// 
			this.textMessage.Location = new System.Drawing.Point(312, 161);
			this.textMessage.Multiline = true;
			this.textMessage.Name = "textMessage";
			this.textMessage.Size = new System.Drawing.Size(308, 91);
			this.textMessage.TabIndex = 8;
			// 
			// labelMessage
			// 
			this.labelMessage.Location = new System.Drawing.Point(11, 158);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Size = new System.Drawing.Size(297, 20);
			this.labelMessage.TabIndex = 9;
			this.labelMessage.Text = "Or this alternate custom message may be shown instead";
			this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(12, 292);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(85, 24);
			this.butDelete.TabIndex = 4;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(529, 292);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(610, 292);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textName
			// 
			this.textName.Location = new System.Drawing.Point(312, 18);
			this.textName.Name = "textName";
			this.textName.ReadOnly = true;
			this.textName.Size = new System.Drawing.Size(308, 20);
			this.textName.TabIndex = 10;
			// 
			// textRxName
			// 
			this.textRxName.Location = new System.Drawing.Point(303, 19);
			this.textRxName.Name = "textRxName";
			this.textRxName.ReadOnly = true;
			this.textRxName.Size = new System.Drawing.Size(308, 20);
			this.textRxName.TabIndex = 12;
			// 
			// labelRx
			// 
			this.labelRx.Location = new System.Drawing.Point(5, 19);
			this.labelRx.Name = "labelRx";
			this.labelRx.Size = new System.Drawing.Size(294, 20);
			this.labelRx.TabIndex = 11;
			this.labelRx.Text = "This Rx is entered";
			this.labelRx.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(56, 130);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(252, 20);
			this.label1.TabIndex = 13;
			this.label1.Text = "Then the user will see a default alert message.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.textRxName);
			this.groupBox.Controls.Add(this.labelRx);
			this.groupBox.Location = new System.Drawing.Point(9, 44);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(636, 75);
			this.groupBox.TabIndex = 16;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "And then,";
			// 
			// FormRxAlertEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(697, 328);
			this.Controls.Add(this.groupBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textName);
			this.Controls.Add(this.labelMessage);
			this.Controls.Add(this.textMessage);
			this.Controls.Add(this.labelName);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormRxAlertEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Rx Alert Edit";
			this.Load += new System.EventHandler(this.FormRxAlertEdit_Load);
			this.groupBox.ResumeLayout(false);
			this.groupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private UI.Button butDelete;
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.TextBox textMessage;
		private System.Windows.Forms.Label labelMessage;
		private System.Windows.Forms.TextBox textName;
		private System.Windows.Forms.TextBox textRxName;
		private System.Windows.Forms.Label labelRx;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox;
	}
}