namespace OpenDental{
	partial class FormAutomationConditionEdit {
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
			this.labelCompareString = new System.Windows.Forms.Label();
			this.textCompareString = new System.Windows.Forms.TextBox();
			this.labelCompareField = new System.Windows.Forms.Label();
			this.labelComparison = new System.Windows.Forms.Label();
			this.listCompareField = new System.Windows.Forms.ListBox();
			this.listComparison = new System.Windows.Forms.ListBox();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// labelCompareString
			// 
			this.labelCompareString.Location = new System.Drawing.Point(397,20);
			this.labelCompareString.Name = "labelCompareString";
			this.labelCompareString.Size = new System.Drawing.Size(147,20);
			this.labelCompareString.TabIndex = 4;
			this.labelCompareString.Text = "Text";
			this.labelCompareString.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textCompareString
			// 
			this.textCompareString.Location = new System.Drawing.Point(397,43);
			this.textCompareString.Name = "textCompareString";
			this.textCompareString.Size = new System.Drawing.Size(316,20);
			this.textCompareString.TabIndex = 5;
			// 
			// labelCompareField
			// 
			this.labelCompareField.Location = new System.Drawing.Point(24,20);
			this.labelCompareField.Name = "labelCompareField";
			this.labelCompareField.Size = new System.Drawing.Size(175,20);
			this.labelCompareField.TabIndex = 31;
			this.labelCompareField.Text = "Field";
			this.labelCompareField.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelComparison
			// 
			this.labelComparison.Location = new System.Drawing.Point(231,20);
			this.labelComparison.Name = "labelComparison";
			this.labelComparison.Size = new System.Drawing.Size(138,20);
			this.labelComparison.TabIndex = 32;
			this.labelComparison.Text = "Comparison";
			this.labelComparison.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listCompareField
			// 
			this.listCompareField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listCompareField.FormattingEnabled = true;
			this.listCompareField.Location = new System.Drawing.Point(24,43);
			this.listCompareField.Name = "listCompareField";
			this.listCompareField.Size = new System.Drawing.Size(181,212);
			this.listCompareField.TabIndex = 71;
			// 
			// listComparison
			// 
			this.listComparison.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listComparison.FormattingEnabled = true;
			this.listComparison.Location = new System.Drawing.Point(234,43);
			this.listComparison.Name = "listComparison";
			this.listComparison.Size = new System.Drawing.Size(138,212);
			this.listComparison.TabIndex = 72;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(24,302);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,24);
			this.butDelete.TabIndex = 69;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(638,261);
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
			this.butCancel.Location = new System.Drawing.Point(638,302);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormAutomationConditionEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(738,353);
			this.Controls.Add(this.listComparison);
			this.Controls.Add(this.listCompareField);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.labelComparison);
			this.Controls.Add(this.labelCompareField);
			this.Controls.Add(this.textCompareString);
			this.Controls.Add(this.labelCompareString);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormAutomationConditionEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Automation Condition Edit";
			this.Load += new System.EventHandler(this.FormAutomationConditionEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label labelCompareString;
		private System.Windows.Forms.TextBox textCompareString;
		private System.Windows.Forms.Label labelCompareField;
		private System.Windows.Forms.Label labelComparison;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.ListBox listCompareField;
		private System.Windows.Forms.ListBox listComparison;
	}
}