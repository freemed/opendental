namespace OpenDental{
	partial class FormRequestEdit {
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
			this.label3 = new System.Windows.Forms.Label();
			this.textConnectionMessage = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.gridDisc = new OpenDental.UI.ODGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
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
			this.butOK.Location = new System.Drawing.Point(791,616);
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
			this.butCancel.Location = new System.Drawing.Point(791,646);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4,3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(95,18);
			this.label3.TabIndex = 60;
			this.label3.Text = "Description";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textConnectionMessage
			// 
			this.textConnectionMessage.AcceptsReturn = true;
			this.textConnectionMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textConnectionMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))),((int)(((byte)(229)))),((int)(((byte)(233)))));
			this.textConnectionMessage.Location = new System.Drawing.Point(101,3);
			this.textConnectionMessage.Multiline = true;
			this.textConnectionMessage.Name = "textConnectionMessage";
			this.textConnectionMessage.ReadOnly = true;
			this.textConnectionMessage.Size = new System.Drawing.Size(414,98);
			this.textConnectionMessage.TabIndex = 59;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4,103);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95,18);
			this.label1.TabIndex = 62;
			this.label1.Text = "Date Submitted";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))),((int)(((byte)(229)))),((int)(((byte)(233)))));
			this.textBox1.Location = new System.Drawing.Point(101,103);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(124,20);
			this.textBox1.TabIndex = 61;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoCheck = false;
			this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox1.Location = new System.Drawing.Point(231,103);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(127,20);
			this.checkBox1.TabIndex = 63;
			this.checkBox1.Text = "Submitted by me";
			this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(4,125);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(95,18);
			this.label2.TabIndex = 65;
			this.label2.Text = "Admin Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBox2
			// 
			this.textBox2.AcceptsReturn = true;
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))),((int)(((byte)(229)))),((int)(((byte)(233)))));
			this.textBox2.Location = new System.Drawing.Point(101,125);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(414,98);
			this.textBox2.TabIndex = 64;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(4,226);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(95,18);
			this.label4.TabIndex = 66;
			this.label4.Text = "Difficulty";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBox3
			// 
			this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))),((int)(((byte)(229)))),((int)(((byte)(233)))));
			this.textBox3.Location = new System.Drawing.Point(101,225);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(38,20);
			this.textBox3.TabIndex = 67;
			// 
			// textBox4
			// 
			this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))),((int)(((byte)(229)))),((int)(((byte)(233)))));
			this.textBox4.Location = new System.Drawing.Point(258,226);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(167,20);
			this.textBox4.TabIndex = 69;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(146,227);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(110,18);
			this.label5.TabIndex = 68;
			this.label5.Text = "Approval";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridDisc
			// 
			this.gridDisc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridDisc.HScrollVisible = false;
			this.gridDisc.Location = new System.Drawing.Point(15,249);
			this.gridDisc.Name = "gridDisc";
			this.gridDisc.ScrollValue = 0;
			this.gridDisc.Size = new System.Drawing.Size(770,421);
			this.gridDisc.TabIndex = 70;
			this.gridDisc.Title = "Discussion";
			this.gridDisc.TranslationName = null;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.textBox7);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.textBox6);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.checkBox2);
			this.groupBox1.Controls.Add(this.textBox5);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Location = new System.Drawing.Point(530,3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(339,120);
			this.groupBox1.TabIndex = 71;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "My Votes";
			// 
			// textBox5
			// 
			this.textBox5.BackColor = System.Drawing.SystemColors.Window;
			this.textBox5.Location = new System.Drawing.Point(98,16);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(38,20);
			this.textBox5.TabIndex = 69;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(27,17);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(69,18);
			this.label6.TabIndex = 68;
			this.label6.Text = "Points";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoCheck = false;
			this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox2.Location = new System.Drawing.Point(16,38);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(97,20);
			this.checkBox2.TabIndex = 70;
			this.checkBox2.Text = "Is Critical";
			this.checkBox2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// textBox6
			// 
			this.textBox6.BackColor = System.Drawing.SystemColors.Window;
			this.textBox6.Location = new System.Drawing.Point(98,59);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(63,20);
			this.textBox6.TabIndex = 72;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(3,60);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(93,18);
			this.label7.TabIndex = 71;
			this.label7.Text = "Pledge Amount";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBox7
			// 
			this.textBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))),((int)(((byte)(229)))),((int)(((byte)(233)))));
			this.textBox7.Location = new System.Drawing.Point(264,17);
			this.textBox7.Name = "textBox7";
			this.textBox7.ReadOnly = true;
			this.textBox7.Size = new System.Drawing.Size(38,20);
			this.textBox7.TabIndex = 74;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(157,18);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(105,18);
			this.label8.TabIndex = 73;
			this.label8.Text = "Points Remaining";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(4,83);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(330,34);
			this.label9.TabIndex = 75;
			this.label9.Text = "Pledges are neither required nor requested.  They are for unusual situations wher" +
    "e a feature is extremely important to you.";
			// 
			// FormRequestEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(878,682);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridDisc);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textConnectionMessage);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormRequestEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Request";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textConnectionMessage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label5;
		private OpenDental.UI.ODGrid gridDisc;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox checkBox2;
	}
}