namespace CentralManager{
	partial class FormCentralConnectionEdit {
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
			this.textServiceURI = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textOdUser = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textOdPassword = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.butDelete = new System.Windows.Forms.Button();
			this.textServerName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textDatabaseName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textMySqlUser = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textMySqlPassword = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkWebServiceIsEcw = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textItemOrder = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label11 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Location = new System.Drawing.Point(520,494);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 5;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(601,494);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textServiceURI
			// 
			this.textServiceURI.Location = new System.Drawing.Point(120,19);
			this.textServiceURI.Name = "textServiceURI";
			this.textServiceURI.Size = new System.Drawing.Size(524,20);
			this.textServiceURI.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(6,22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(111,17);
			this.label2.TabIndex = 200;
			this.label2.Text = "Remote URI";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textOdUser
			// 
			this.textOdUser.Location = new System.Drawing.Point(120,59);
			this.textOdUser.Name = "textOdUser";
			this.textOdUser.Size = new System.Drawing.Size(165,20);
			this.textOdUser.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(6,62);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111,17);
			this.label1.TabIndex = 202;
			this.label1.Text = "OD User Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textOdPassword
			// 
			this.textOdPassword.Location = new System.Drawing.Point(120,85);
			this.textOdPassword.Name = "textOdPassword";
			this.textOdPassword.Size = new System.Drawing.Size(165,20);
			this.textOdPassword.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(6,88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(111,17);
			this.label3.TabIndex = 204;
			this.label3.Text = "OD Password";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butDelete
			// 
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(13,494);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,24);
			this.butDelete.TabIndex = 205;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textServerName
			// 
			this.textServerName.Location = new System.Drawing.Point(120,20);
			this.textServerName.Name = "textServerName";
			this.textServerName.Size = new System.Drawing.Size(190,20);
			this.textServerName.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(6,23);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111,17);
			this.label4.TabIndex = 207;
			this.label4.Text = "Server Name";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDatabaseName
			// 
			this.textDatabaseName.Location = new System.Drawing.Point(120,46);
			this.textDatabaseName.Name = "textDatabaseName";
			this.textDatabaseName.Size = new System.Drawing.Size(190,20);
			this.textDatabaseName.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label5.Location = new System.Drawing.Point(6,49);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(111,17);
			this.label5.TabIndex = 209;
			this.label5.Text = "Database";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMySqlUser
			// 
			this.textMySqlUser.Location = new System.Drawing.Point(120,72);
			this.textMySqlUser.Name = "textMySqlUser";
			this.textMySqlUser.Size = new System.Drawing.Size(190,20);
			this.textMySqlUser.TabIndex = 2;
			// 
			// label6
			// 
			this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label6.Location = new System.Drawing.Point(6,75);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(111,17);
			this.label6.TabIndex = 211;
			this.label6.Text = "MySql User";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMySqlPassword
			// 
			this.textMySqlPassword.Location = new System.Drawing.Point(120,98);
			this.textMySqlPassword.Name = "textMySqlPassword";
			this.textMySqlPassword.Size = new System.Drawing.Size(190,20);
			this.textMySqlPassword.TabIndex = 3;
			// 
			// label7
			// 
			this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label7.Location = new System.Drawing.Point(6,101);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(111,17);
			this.label7.TabIndex = 213;
			this.label7.Text = "MySql Password";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textMySqlPassword);
			this.groupBox1.Controls.Add(this.textServerName);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textMySqlUser);
			this.groupBox1.Controls.Add(this.textDatabaseName);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Location = new System.Drawing.Point(15,12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(341,134);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Direct Database Connection";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkWebServiceIsEcw);
			this.groupBox2.Controls.Add(this.textServiceURI);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(15,176);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(661,65);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Web Service Connection";
			// 
			// checkWebServiceIsEcw
			// 
			this.checkWebServiceIsEcw.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkWebServiceIsEcw.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkWebServiceIsEcw.Location = new System.Drawing.Point(2,43);
			this.checkWebServiceIsEcw.Name = "checkWebServiceIsEcw";
			this.checkWebServiceIsEcw.Size = new System.Drawing.Size(131,18);
			this.checkWebServiceIsEcw.TabIndex = 1;
			this.checkWebServiceIsEcw.Text = "Using eClinicalWorks";
			this.checkWebServiceIsEcw.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkWebServiceIsEcw.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label8.Location = new System.Drawing.Point(21,382);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(111,17);
			this.label8.TabIndex = 217;
			this.label8.Text = "Order";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(135,405);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(368,61);
			this.textNote.TabIndex = 4;
			// 
			// label9
			// 
			this.label9.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label9.Location = new System.Drawing.Point(21,408);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(111,17);
			this.label9.TabIndex = 219;
			this.label9.Text = "Note";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textItemOrder
			// 
			this.textItemOrder.Location = new System.Drawing.Point(135,379);
			this.textItemOrder.Name = "textItemOrder";
			this.textItemOrder.Size = new System.Drawing.Size(57,20);
			this.textItemOrder.TabIndex = 3;
			// 
			// label10
			// 
			this.label10.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label10.Location = new System.Drawing.Point(15,155);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(111,17);
			this.label10.TabIndex = 220;
			this.label10.Text = "OR (but not both)";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Controls.Add(this.textOdUser);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.textOdPassword);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Location = new System.Drawing.Point(15,251);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(341,119);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Open Dental Credentials";
			// 
			// label11
			// 
			this.label11.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label11.Location = new System.Drawing.Point(48,21);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(238,32);
			this.label11.TabIndex = 218;
			this.label11.Text = "Required if using Web Service connection.  Optional for direct connection.";
			// 
			// FormCentralConnectionEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(688,530);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.textItemOrder);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormCentralConnectionEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Connection";
			this.Load += new System.EventHandler(this.FormCentralConnectionEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.TextBox textServiceURI;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textOdUser;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textOdPassword;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.TextBox textServerName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textDatabaseName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textMySqlUser;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textMySqlPassword;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textItemOrder;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox checkWebServiceIsEcw;
	}
}