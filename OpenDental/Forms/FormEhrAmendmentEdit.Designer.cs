namespace OpenDental {
	partial class FormEhrAmendmentEdit {
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
			this.radioIsAccepted = new System.Windows.Forms.RadioButton();
			this.radioIsDenied = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.textAmdIsScanned = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboSource = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.butView = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// radioIsAccepted
			// 
			this.radioIsAccepted.Checked = true;
			this.radioIsAccepted.Location = new System.Drawing.Point(105, 12);
			this.radioIsAccepted.Name = "radioIsAccepted";
			this.radioIsAccepted.Size = new System.Drawing.Size(71, 17);
			this.radioIsAccepted.TabIndex = 5;
			this.radioIsAccepted.TabStop = true;
			this.radioIsAccepted.Text = "Accepted";
			this.radioIsAccepted.UseVisualStyleBackColor = true;
			// 
			// radioIsDenied
			// 
			this.radioIsDenied.Location = new System.Drawing.Point(182, 12);
			this.radioIsDenied.Name = "radioIsDenied";
			this.radioIsDenied.Size = new System.Drawing.Size(59, 17);
			this.radioIsDenied.TabIndex = 6;
			this.radioIsDenied.Text = "Denied";
			this.radioIsDenied.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(29, 70);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(70, 16);
			this.label5.TabIndex = 16;
			this.label5.Text = "Description";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescription
			// 
			this.textDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textDescription.Location = new System.Drawing.Point(105, 70);
			this.textDescription.Multiline = true;
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(242, 101);
			this.textDescription.TabIndex = 17;
			// 
			// textAmdIsScanned
			// 
			this.textAmdIsScanned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textAmdIsScanned.Location = new System.Drawing.Point(27, 151);
			this.textAmdIsScanned.MaxLength = 25;
			this.textAmdIsScanned.Name = "textAmdIsScanned";
			this.textAmdIsScanned.ReadOnly = true;
			this.textAmdIsScanned.Size = new System.Drawing.Size(72, 20);
			this.textAmdIsScanned.TabIndex = 112;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Location = new System.Drawing.Point(-1, 132);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 111;
			this.label1.Text = "Scanned";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboSource
			// 
			this.comboSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSource.FormattingEnabled = true;
			this.comboSource.Location = new System.Drawing.Point(105, 43);
			this.comboSource.Name = "comboSource";
			this.comboSource.Size = new System.Drawing.Size(178, 21);
			this.comboSource.TabIndex = 114;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(13, 47);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(86, 17);
			this.label7.TabIndex = 113;
			this.label7.Text = "Source";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(324, 192);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 23);
			this.butCancel.TabIndex = 115;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Location = new System.Drawing.Point(243, 192);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 23);
			this.butOK.TabIndex = 116;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butView
			// 
			this.butView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butView.Location = new System.Drawing.Point(105, 192);
			this.butView.Name = "butView";
			this.butView.Size = new System.Drawing.Size(75, 23);
			this.butView.TabIndex = 117;
			this.butView.Text = "View";
			this.butView.UseVisualStyleBackColor = true;
			this.butView.Click += new System.EventHandler(this.butView_Click);
			// 
			// butDelete
			// 
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Location = new System.Drawing.Point(12, 192);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 23);
			this.butDelete.TabIndex = 118;
			this.butDelete.Text = "Delete";
			this.butDelete.UseVisualStyleBackColor = true;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormEhrAmendmentEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(411, 227);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butView);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.comboSource);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textAmdIsScanned);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.radioIsDenied);
			this.Controls.Add(this.radioIsAccepted);
			this.Name = "FormEhrAmendmentEdit";
			this.Text = "Edit Amendment";
			this.Load += new System.EventHandler(this.FormEhrAmendmentEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton radioIsAccepted;
		private System.Windows.Forms.RadioButton radioIsDenied;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.TextBox textAmdIsScanned;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboSource;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butView;
		private System.Windows.Forms.Button butDelete;
	}
}