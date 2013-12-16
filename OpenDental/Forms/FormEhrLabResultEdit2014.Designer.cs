namespace OpenDental {
	partial class FormEhrLabResultEdit2014 {
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
			this.butCancel = new System.Windows.Forms.Button();
			this.butOk = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.textDateTimeTest = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textTestID = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textSnomedDescription = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textInstruction = new System.Windows.Forms.TextBox();
			this.gridAbnormalFlags = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(406, 393);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 6;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOk
			// 
			this.butOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOk.Location = new System.Drawing.Point(325, 393);
			this.butOk.Name = "butOk";
			this.butOk.Size = new System.Drawing.Size(75, 24);
			this.butOk.TabIndex = 5;
			this.butOk.Text = "Ok";
			this.butOk.UseVisualStyleBackColor = true;
			// 
			// butDelete
			// 
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Location = new System.Drawing.Point(12, 393);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 24);
			this.butDelete.TabIndex = 7;
			this.butDelete.Text = "Delete";
			this.butDelete.UseVisualStyleBackColor = true;
			// 
			// textDateTimeTest
			// 
			this.textDateTimeTest.Location = new System.Drawing.Point(189, 38);
			this.textDateTimeTest.Name = "textDateTimeTest";
			this.textDateTimeTest.Size = new System.Drawing.Size(152, 20);
			this.textDateTimeTest.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(45, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(138, 17);
			this.label3.TabIndex = 8;
			this.label3.Text = "Date Time of Analysis";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTestID
			// 
			this.textTestID.Location = new System.Drawing.Point(189, 75);
			this.textTestID.Name = "textTestID";
			this.textTestID.Size = new System.Drawing.Size(152, 20);
			this.textTestID.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(42, 75);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(141, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "LOINC";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(45, 12);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(138, 17);
			this.label8.TabIndex = 18;
			this.label8.Text = "Set ID";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(189, 12);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(152, 20);
			this.textBox1.TabIndex = 17;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(45, 156);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(138, 17);
			this.label9.TabIndex = 20;
			this.label9.Text = "Value Type";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(189, 156);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(152, 20);
			this.textBox2.TabIndex = 19;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(19, 99);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(164, 17);
			this.label10.TabIndex = 228;
			this.label10.Text = "Description";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSnomedDescription
			// 
			this.textSnomedDescription.Location = new System.Drawing.Point(189, 101);
			this.textSnomedDescription.Multiline = true;
			this.textSnomedDescription.Name = "textSnomedDescription";
			this.textSnomedDescription.Size = new System.Drawing.Size(281, 49);
			this.textSnomedDescription.TabIndex = 227;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(19, 182);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(164, 17);
			this.label4.TabIndex = 230;
			this.label4.Text = "Observed Value";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textInstruction
			// 
			this.textInstruction.Location = new System.Drawing.Point(189, 182);
			this.textInstruction.Multiline = true;
			this.textInstruction.Name = "textInstruction";
			this.textInstruction.Size = new System.Drawing.Size(281, 84);
			this.textInstruction.TabIndex = 229;
			// 
			// gridAbnormalFlags
			// 
			this.gridAbnormalFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridAbnormalFlags.HScrollVisible = false;
			this.gridAbnormalFlags.Location = new System.Drawing.Point(189, 272);
			this.gridAbnormalFlags.Name = "gridAbnormalFlags";
			this.gridAbnormalFlags.ScrollValue = 0;
			this.gridAbnormalFlags.Size = new System.Drawing.Size(281, 103);
			this.gridAbnormalFlags.TabIndex = 231;
			this.gridAbnormalFlags.Title = "Abnormal Flags";
			this.gridAbnormalFlags.TranslationName = null;
			// 
			// FormEhrLabResultEdit2014
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(497, 428);
			this.Controls.Add(this.gridAbnormalFlags);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textInstruction);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textSnomedDescription);
			this.Controls.Add(this.textTestID);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDateTimeTest);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOk);
			this.Controls.Add(this.butCancel);
			this.Name = "FormEhrLabResultEdit2014";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormLabResultEdit2014";
			this.Load += new System.EventHandler(this.FormLabResultEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOk;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.TextBox textDateTimeTest;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textTestID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textSnomedDescription;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textInstruction;
		private UI.ODGrid gridAbnormalFlags;
	}
}