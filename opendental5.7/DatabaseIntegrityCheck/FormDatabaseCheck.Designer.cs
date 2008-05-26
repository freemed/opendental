namespace DatabaseIntegrityCheck {
	partial class FormDatabaseCheck {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDatabaseCheck));
			this.butRun = new System.Windows.Forms.Button();
			this.textDatabase = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textComputerName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textUser = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butRun
			// 
			this.butRun.Location = new System.Drawing.Point(370,171);
			this.butRun.Name = "butRun";
			this.butRun.Size = new System.Drawing.Size(75,25);
			this.butRun.TabIndex = 0;
			this.butRun.Text = "Run";
			this.butRun.UseVisualStyleBackColor = true;
			this.butRun.Click += new System.EventHandler(this.butRun_Click);
			// 
			// textDatabase
			// 
			this.textDatabase.Location = new System.Drawing.Point(179,47);
			this.textDatabase.Name = "textDatabase";
			this.textDatabase.Size = new System.Drawing.Size(154,20);
			this.textDatabase.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(59,50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118,13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Database";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(59,24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(118,13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Computer";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textComputerName
			// 
			this.textComputerName.Location = new System.Drawing.Point(179,21);
			this.textComputerName.Name = "textComputerName";
			this.textComputerName.Size = new System.Drawing.Size(154,20);
			this.textComputerName.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(59,76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(118,13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Username";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(179,73);
			this.textUser.Name = "textUser";
			this.textUser.Size = new System.Drawing.Size(154,20);
			this.textUser.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(59,102);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(118,13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Password";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(179,99);
			this.textPassword.Name = "textPassword";
			this.textPassword.Size = new System.Drawing.Size(154,20);
			this.textPassword.TabIndex = 7;
			// 
			// FormDatabaseCheck
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(457,225);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textUser);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textComputerName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDatabase);
			this.Controls.Add(this.butRun);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormDatabaseCheck";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Database Integrity Check";
			this.Load += new System.EventHandler(this.FormDatabaseCheck_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butRun;
		private System.Windows.Forms.TextBox textDatabase;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textComputerName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textUser;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textPassword;
	}
}

