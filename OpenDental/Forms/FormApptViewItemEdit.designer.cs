namespace OpenDental{
	partial class FormApptViewItemEdit {
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
			this.labelBeforeTime = new System.Windows.Forms.Label();
			this.panelColor = new System.Windows.Forms.Panel();
			this.butColor = new OpenDental.UI.Button();
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
			this.butOK.Location = new System.Drawing.Point(377,147);
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
			this.butCancel.Location = new System.Drawing.Point(377,188);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// labelBeforeTime
			// 
			this.labelBeforeTime.Location = new System.Drawing.Point(14,15);
			this.labelBeforeTime.Name = "labelBeforeTime";
			this.labelBeforeTime.Size = new System.Drawing.Size(117,17);
			this.labelBeforeTime.TabIndex = 59;
			this.labelBeforeTime.Text = "Text Color";
			this.labelBeforeTime.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// panelColor
			// 
			this.panelColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))),((int)(((byte)(64)))),((int)(((byte)(64)))));
			this.panelColor.Location = new System.Drawing.Point(139,13);
			this.panelColor.Name = "panelColor";
			this.panelColor.Size = new System.Drawing.Size(24,24);
			this.panelColor.TabIndex = 60;
			// 
			// butColor
			// 
			this.butColor.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butColor.Autosize = true;
			this.butColor.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butColor.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butColor.CornerRadius = 4F;
			this.butColor.Location = new System.Drawing.Point(176,13);
			this.butColor.Name = "butColor";
			this.butColor.Size = new System.Drawing.Size(75,24);
			this.butColor.TabIndex = 61;
			this.butColor.Text = "Change";
			this.butColor.Click += new System.EventHandler(this.butColor_Click);
			// 
			// FormApptViewItemEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(477,239);
			this.Controls.Add(this.butColor);
			this.Controls.Add(this.panelColor);
			this.Controls.Add(this.labelBeforeTime);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormApptViewItemEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Appointment View Item Edit";
			this.Load += new System.EventHandler(this.FormApptViewItemEdit_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label labelBeforeTime;
		private System.Windows.Forms.Panel panelColor;
		private OpenDental.UI.Button butColor;
	}
}