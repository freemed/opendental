namespace OpenDental{
	partial class FormTestLatency {
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
			this.label1 = new System.Windows.Forms.Label();
			this.textAverage = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textMax = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textMin = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.butRun = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.textTests = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10,74);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(172,17);
			this.label1.TabIndex = 5;
			this.label1.Text = "Average round trip to database";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAverage
			// 
			this.textAverage.Location = new System.Drawing.Point(185,73);
			this.textAverage.Name = "textAverage";
			this.textAverage.ReadOnly = true;
			this.textAverage.Size = new System.Drawing.Size(55,20);
			this.textAverage.TabIndex = 6;
			this.textAverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(242,74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,17);
			this.label2.TabIndex = 7;
			this.label2.Text = "ms.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(242,100);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,17);
			this.label3.TabIndex = 10;
			this.label3.Text = "ms.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textMax
			// 
			this.textMax.Location = new System.Drawing.Point(185,99);
			this.textMax.Name = "textMax";
			this.textMax.ReadOnly = true;
			this.textMax.Size = new System.Drawing.Size(55,20);
			this.textMax.TabIndex = 9;
			this.textMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(10,100);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(172,17);
			this.label4.TabIndex = 8;
			this.label4.Text = "Max round trip to database";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(242,48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,17);
			this.label5.TabIndex = 13;
			this.label5.Text = "ms.";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textMin
			// 
			this.textMin.Location = new System.Drawing.Point(185,47);
			this.textMin.Name = "textMin";
			this.textMin.ReadOnly = true;
			this.textMin.Size = new System.Drawing.Size(55,20);
			this.textMin.TabIndex = 12;
			this.textMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(10,48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(172,17);
			this.label6.TabIndex = 11;
			this.label6.Text = "Min round trip to database";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butRun
			// 
			this.butRun.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butRun.Autosize = true;
			this.butRun.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRun.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRun.CornerRadius = 4F;
			this.butRun.Location = new System.Drawing.Point(12,164);
			this.butRun.Name = "butRun";
			this.butRun.Size = new System.Drawing.Size(75,24);
			this.butRun.TabIndex = 4;
			this.butRun.Text = "Run Test";
			this.butRun.Click += new System.EventHandler(this.butRun_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(274,164);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// textTests
			// 
			this.textTests.Location = new System.Drawing.Point(185,12);
			this.textTests.Name = "textTests";
			this.textTests.Size = new System.Drawing.Size(55,20);
			this.textTests.TabIndex = 14;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(11,13);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(172,17);
			this.label7.TabIndex = 15;
			this.label7.Text = "Number of tests";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormTestLatency
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(361,200);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textTests);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textMin);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textMax);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textAverage);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butRun);
			this.Controls.Add(this.butClose);
			this.Name = "FormTestLatency";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Roudtrip latency";
			this.Load += new System.EventHandler(this.FormTestLatency_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private UI.Button butRun;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textAverage;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textMax;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textMin;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textTests;
		private System.Windows.Forms.Label label7;
	}
}