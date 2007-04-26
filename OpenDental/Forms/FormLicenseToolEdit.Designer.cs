namespace OpenDental {
	partial class FormLicenseToolEdit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLicenseToolEdit));
			this.okbutton = new System.Windows.Forms.Button();
			this.cancelbutton = new System.Windows.Forms.Button();
			this.deletebutton = new System.Windows.Forms.Button();
			this.textCode = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// okbutton
			// 
			this.okbutton.Location = new System.Drawing.Point(281,122);
			this.okbutton.Name = "okbutton";
			this.okbutton.Size = new System.Drawing.Size(75,26);
			this.okbutton.TabIndex = 10;
			this.okbutton.Text = "OK";
			this.okbutton.UseVisualStyleBackColor = true;
			this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
			// 
			// cancelbutton
			// 
			this.cancelbutton.Location = new System.Drawing.Point(362,122);
			this.cancelbutton.Name = "cancelbutton";
			this.cancelbutton.Size = new System.Drawing.Size(75,26);
			this.cancelbutton.TabIndex = 11;
			this.cancelbutton.Text = "Cancel";
			this.cancelbutton.UseVisualStyleBackColor = true;
			this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
			// 
			// deletebutton
			// 
			this.deletebutton.Image = global::OpenDental.Properties.Resources.deleteX;
			this.deletebutton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.deletebutton.Location = new System.Drawing.Point(33,122);
			this.deletebutton.Name = "deletebutton";
			this.deletebutton.Size = new System.Drawing.Size(90,26);
			this.deletebutton.TabIndex = 12;
			this.deletebutton.Text = "Delete";
			this.deletebutton.UseVisualStyleBackColor = true;
			this.deletebutton.Click += new System.EventHandler(this.deletebutton_Click);
			// 
			// textCode
			// 
			this.textCode.Location = new System.Drawing.Point(33,49);
			this.textCode.Name = "textCode";
			this.textCode.Size = new System.Drawing.Size(92,20);
			this.textCode.TabIndex = 13;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(131,31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(142,16);
			this.label2.TabIndex = 16;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(31,31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94,16);
			this.label1.TabIndex = 15;
			this.label1.Text = "ADA Code";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(134,49);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(303,20);
			this.textDescription.TabIndex = 14;
			// 
			// FormLicenseToolEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(471,171);
			this.Controls.Add(this.textCode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.deletebutton);
			this.Controls.Add(this.cancelbutton);
			this.Controls.Add(this.okbutton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormLicenseToolEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Procedure Code Edit";
			this.Load += new System.EventHandler(this.FormLicenseToolEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button okbutton;
		private System.Windows.Forms.Button cancelbutton;
		private System.Windows.Forms.Button deletebutton;
		private System.Windows.Forms.TextBox textCode;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textDescription;
	}
}