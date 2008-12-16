namespace OpenDental{
	partial class FormMobile {
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
			this.butClose = new OpenDental.UI.Button();
			this.textPath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butSync = new OpenDental.UI.Button();
			this.labelValid = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textDateStart = new OpenDental.ValidDate();
			this.label5 = new System.Windows.Forms.Label();
			this.textDateEnd = new OpenDental.ValidDate();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(492,196);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// textPath
			// 
			this.textPath.Location = new System.Drawing.Point(156,37);
			this.textPath.Name = "textPath";
			this.textPath.Size = new System.Drawing.Size(258,20);
			this.textPath.TabIndex = 3;
			this.textPath.TextChanged += new System.EventHandler(this.textPath_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(54,37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,18);
			this.label1.TabIndex = 4;
			this.label1.Text = "Path";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butSync
			// 
			this.butSync.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSync.Autosize = true;
			this.butSync.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSync.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSync.CornerRadius = 4F;
			this.butSync.Enabled = false;
			this.butSync.Location = new System.Drawing.Point(334,196);
			this.butSync.Name = "butSync";
			this.butSync.Size = new System.Drawing.Size(68,24);
			this.butSync.TabIndex = 5;
			this.butSync.Text = "Sync";
			this.butSync.Click += new System.EventHandler(this.butSync_Click);
			// 
			// labelValid
			// 
			this.labelValid.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelValid.ForeColor = System.Drawing.Color.DarkRed;
			this.labelValid.Location = new System.Drawing.Point(420,37);
			this.labelValid.Name = "labelValid";
			this.labelValid.Size = new System.Drawing.Size(157,18);
			this.labelValid.TabIndex = 6;
			this.labelValid.Text = "Path is not valid";
			this.labelValid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(2,77);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(152,69);
			this.label3.TabIndex = 7;
			this.label3.Text = "The following files will be created or overwritten if they already exist.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(156,77);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(116,20);
			this.textBox2.TabIndex = 8;
			this.textBox2.Text = "patient.txt";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(156,101);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(116,20);
			this.textBox3.TabIndex = 9;
			this.textBox3.Text = "appointment.txt";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(275,102);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88,18);
			this.label4.TabIndex = 10;
			this.label4.Text = "Date Range";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(364,101);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(82,20);
			this.textDateStart.TabIndex = 11;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(446,102);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(39,18);
			this.label5.TabIndex = 12;
			this.label5.Text = "to";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(481,101);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(82,20);
			this.textDateEnd.TabIndex = 13;
			// 
			// FormMobile
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(592,247);
			this.Controls.Add(this.textDateEnd);
			this.Controls.Add(this.textDateStart);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.labelValid);
			this.Controls.Add(this.butSync);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textPath);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.label5);
			this.Name = "FormMobile";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Mobile Sync";
			this.Load += new System.EventHandler(this.FormMobile_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMobile_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textPath;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butSync;
		private System.Windows.Forms.Label labelValid;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label4;
		private ValidDate textDateStart;
		private System.Windows.Forms.Label label5;
		private ValidDate textDateEnd;
	}
}