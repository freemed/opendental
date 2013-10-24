namespace OpenDental {
	partial class FormEhrMeasureEventEdit {
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
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.textMoreInfo = new System.Windows.Forms.TextBox();
			this.labelDateTime = new System.Windows.Forms.Label();
			this.textDateTime = new System.Windows.Forms.TextBox();
			this.textType = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Location = new System.Drawing.Point(405,180);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,23);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(486,180);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,23);
			this.butCancel.TabIndex = 10;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textMoreInfo
			// 
			this.textMoreInfo.Location = new System.Drawing.Point(199,70);
			this.textMoreInfo.Multiline = true;
			this.textMoreInfo.Name = "textMoreInfo";
			this.textMoreInfo.Size = new System.Drawing.Size(330,83);
			this.textMoreInfo.TabIndex = 0;
			// 
			// labelDateTime
			// 
			this.labelDateTime.Location = new System.Drawing.Point(81,19);
			this.labelDateTime.Name = "labelDateTime";
			this.labelDateTime.Size = new System.Drawing.Size(116,17);
			this.labelDateTime.TabIndex = 19;
			this.labelDateTime.Text = "Date Time";
			this.labelDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateTime
			// 
			this.textDateTime.Location = new System.Drawing.Point(199,17);
			this.textDateTime.Name = "textDateTime";
			this.textDateTime.ReadOnly = true;
			this.textDateTime.Size = new System.Drawing.Size(207,20);
			this.textDateTime.TabIndex = 18;
			// 
			// textType
			// 
			this.textType.Location = new System.Drawing.Point(199,43);
			this.textType.Name = "textType";
			this.textType.ReadOnly = true;
			this.textType.Size = new System.Drawing.Size(207,20);
			this.textType.TabIndex = 20;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(81,45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116,17);
			this.label1.TabIndex = 21;
			this.label1.Text = "Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(4,72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(192,54);
			this.label2.TabIndex = 22;
			this.label2.Text = "Documentation of cessation intervention, including whether it was counseling or p" +
    "harmacologic.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormEhrMeasureEventEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(573,215);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textType);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDateTime);
			this.Controls.Add(this.labelDateTime);
			this.Controls.Add(this.textMoreInfo);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormEhrMeasureEventEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "EhrMeasureEvent Edit";
			this.Load += new System.EventHandler(this.FormEhrMeasureEventEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.TextBox textMoreInfo;
		private System.Windows.Forms.Label labelDateTime;
		private System.Windows.Forms.TextBox textDateTime;
		private System.Windows.Forms.TextBox textType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}