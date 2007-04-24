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
			this.adacode = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.description = new System.Windows.Forms.TextBox();
			this.okbutton = new System.Windows.Forms.Button();
			this.cancelbutton = new System.Windows.Forms.Button();
			this.deletebutton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// adacode
			// 
			this.adacode.Location = new System.Drawing.Point(31,41);
			this.adacode.Name = "adacode";
			this.adacode.Size = new System.Drawing.Size(291,20);
			this.adacode.TabIndex = 6;
			this.adacode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.adacode_KeyPress);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(34,67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60,13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Description";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(34,24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57,13);
			this.label1.TabIndex = 8;
			this.label1.Text = "ADA Code";
			// 
			// description
			// 
			this.description.Location = new System.Drawing.Point(31,85);
			this.description.Name = "description";
			this.description.Size = new System.Drawing.Size(291,20);
			this.description.TabIndex = 7;
			this.description.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.description_KeyPress);
			// 
			// okbutton
			// 
			this.okbutton.Location = new System.Drawing.Point(229,122);
			this.okbutton.Name = "okbutton";
			this.okbutton.Size = new System.Drawing.Size(75,23);
			this.okbutton.TabIndex = 10;
			this.okbutton.Text = "OK";
			this.okbutton.UseVisualStyleBackColor = true;
			this.okbutton.Enter += new System.EventHandler(this.okbutton_Enter);
			// 
			// cancelbutton
			// 
			this.cancelbutton.Location = new System.Drawing.Point(148,122);
			this.cancelbutton.Name = "cancelbutton";
			this.cancelbutton.Size = new System.Drawing.Size(75,23);
			this.cancelbutton.TabIndex = 11;
			this.cancelbutton.Text = "Cancel";
			this.cancelbutton.UseVisualStyleBackColor = true;
			this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
			// 
			// deletebutton
			// 
			this.deletebutton.Image = global::OpenDental.Properties.Resources.deleteX;
			this.deletebutton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.deletebutton.Location = new System.Drawing.Point(52,122);
			this.deletebutton.Name = "deletebutton";
			this.deletebutton.Size = new System.Drawing.Size(90,23);
			this.deletebutton.TabIndex = 12;
			this.deletebutton.Text = "Delete";
			this.deletebutton.UseVisualStyleBackColor = true;
			this.deletebutton.Click += new System.EventHandler(this.deletebutton_Click);
			// 
			// FormLicenseToolEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(355,184);
			this.Controls.Add(this.deletebutton);
			this.Controls.Add(this.cancelbutton);
			this.Controls.Add(this.okbutton);
			this.Controls.Add(this.adacode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.description);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormLicenseToolEdit";
			this.Text = "Procedure Code Edit";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox adacode;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox description;
		private System.Windows.Forms.Button okbutton;
		private System.Windows.Forms.Button cancelbutton;
		private System.Windows.Forms.Button deletebutton;
	}
}