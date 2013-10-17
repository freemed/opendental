namespace OpenDental {
	partial class FormMapAreaEdit {
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
			this.labelField1 = new System.Windows.Forms.Label();
			this.textBoxXPos = new System.Windows.Forms.TextBox();
			this.textBoxYPos = new System.Windows.Forms.TextBox();
			this.labelField2 = new System.Windows.Forms.Label();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.textBoxHeightFeet = new System.Windows.Forms.TextBox();
			this.labelHeight = new System.Windows.Forms.Label();
			this.textBoxWidthFeet = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.labelDescription = new System.Windows.Forms.Label();
			this.textBoxExtension = new System.Windows.Forms.TextBox();
			this.labelExtension = new System.Windows.Forms.Label();
			this.butDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelField1
			// 
			this.labelField1.Location = new System.Drawing.Point(7, 7);
			this.labelField1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelField1.Name = "labelField1";
			this.labelField1.Size = new System.Drawing.Size(252, 16);
			this.labelField1.TabIndex = 0;
			this.labelField1.Text = "Feet From Left Edge (XPos)";
			this.labelField1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxXPos
			// 
			this.textBoxXPos.Location = new System.Drawing.Point(263, 7);
			this.textBoxXPos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxXPos.Name = "textBoxXPos";
			this.textBoxXPos.Size = new System.Drawing.Size(92, 20);
			this.textBoxXPos.TabIndex = 0;
			this.textBoxXPos.Text = "0";
			// 
			// textBoxYPos
			// 
			this.textBoxYPos.Location = new System.Drawing.Point(263, 27);
			this.textBoxYPos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxYPos.Name = "textBoxYPos";
			this.textBoxYPos.Size = new System.Drawing.Size(92, 20);
			this.textBoxYPos.TabIndex = 1;
			this.textBoxYPos.Text = "0";
			// 
			// labelField2
			// 
			this.labelField2.Location = new System.Drawing.Point(7, 27);
			this.labelField2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelField2.Name = "labelField2";
			this.labelField2.Size = new System.Drawing.Size(252, 16);
			this.labelField2.TabIndex = 2;
			this.labelField2.Text = "Feet From Top Edge (YPos)";
			this.labelField2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.AutoSize = true;
			this.butOK.Location = new System.Drawing.Point(255, 134);
			this.butOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(45, 23);
			this.butOK.TabIndex = 7;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.AutoSize = true;
			this.butCancel.Location = new System.Drawing.Point(305, 134);
			this.butCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(50, 23);
			this.butCancel.TabIndex = 8;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textBoxHeightFeet
			// 
			this.textBoxHeightFeet.Location = new System.Drawing.Point(263, 89);
			this.textBoxHeightFeet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxHeightFeet.Name = "textBoxHeightFeet";
			this.textBoxHeightFeet.Size = new System.Drawing.Size(92, 20);
			this.textBoxHeightFeet.TabIndex = 4;
			this.textBoxHeightFeet.Text = "0";
			// 
			// labelHeight
			// 
			this.labelHeight.Location = new System.Drawing.Point(7, 89);
			this.labelHeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelHeight.Name = "labelHeight";
			this.labelHeight.Size = new System.Drawing.Size(252, 16);
			this.labelHeight.TabIndex = 8;
			this.labelHeight.Text = "Height (in feet)";
			this.labelHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxWidthFeet
			// 
			this.textBoxWidthFeet.Location = new System.Drawing.Point(263, 68);
			this.textBoxWidthFeet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxWidthFeet.Name = "textBoxWidthFeet";
			this.textBoxWidthFeet.Size = new System.Drawing.Size(92, 20);
			this.textBoxWidthFeet.TabIndex = 3;
			this.textBoxWidthFeet.Text = "0";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7, 68);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(252, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Width (in feet)";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(263, 48);
			this.textBoxDescription.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(92, 20);
			this.textBoxDescription.TabIndex = 2;
			// 
			// labelDescription
			// 
			this.labelDescription.Location = new System.Drawing.Point(7, 48);
			this.labelDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(252, 16);
			this.labelDescription.TabIndex = 10;
			this.labelDescription.Text = "Description";
			this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxExtension
			// 
			this.textBoxExtension.Location = new System.Drawing.Point(263, 109);
			this.textBoxExtension.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxExtension.Name = "textBoxExtension";
			this.textBoxExtension.Size = new System.Drawing.Size(92, 20);
			this.textBoxExtension.TabIndex = 5;
			this.textBoxExtension.Text = "0";
			// 
			// labelExtension
			// 
			this.labelExtension.Location = new System.Drawing.Point(7, 109);
			this.labelExtension.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelExtension.Name = "labelExtension";
			this.labelExtension.Size = new System.Drawing.Size(252, 16);
			this.labelExtension.TabIndex = 12;
			this.labelExtension.Text = "Extension";
			this.labelExtension.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butDelete
			// 
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.AutoSize = true;
			this.butDelete.Location = new System.Drawing.Point(10, 134);
			this.butDelete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(49, 23);
			this.butDelete.TabIndex = 6;
			this.butDelete.Text = "Delete";
			this.butDelete.UseVisualStyleBackColor = true;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormMapAreaEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(361, 163);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textBoxExtension);
			this.Controls.Add(this.labelExtension);
			this.Controls.Add(this.textBoxDescription);
			this.Controls.Add(this.labelDescription);
			this.Controls.Add(this.textBoxHeightFeet);
			this.Controls.Add(this.labelHeight);
			this.Controls.Add(this.textBoxWidthFeet);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textBoxYPos);
			this.Controls.Add(this.labelField2);
			this.Controls.Add(this.textBoxXPos);
			this.Controls.Add(this.labelField1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "FormMapAreaEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Map Area";
			this.Load += new System.EventHandler(this.FormMapAreaEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelField1;
		private System.Windows.Forms.TextBox textBoxXPos;
		private System.Windows.Forms.TextBox textBoxYPos;
		private System.Windows.Forms.Label labelField2;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.TextBox textBoxHeightFeet;
		private System.Windows.Forms.Label labelHeight;
		private System.Windows.Forms.TextBox textBoxWidthFeet;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Label labelDescription;
		private System.Windows.Forms.TextBox textBoxExtension;
		private System.Windows.Forms.Label labelExtension;
		private System.Windows.Forms.Button butDelete;
	}
}