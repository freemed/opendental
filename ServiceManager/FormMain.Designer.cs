namespace ServiceManager {
	partial class FormMain {
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textStatus = new System.Windows.Forms.TextBox();
			this.butInstall = new System.Windows.Forms.Button();
			this.butUninstall = new System.Windows.Forms.Button();
			this.butStart = new System.Windows.Forms.Button();
			this.butStop = new System.Windows.Forms.Button();
			this.butRefresh = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butRefresh);
			this.groupBox1.Controls.Add(this.butStop);
			this.groupBox1.Controls.Add(this.butStart);
			this.groupBox1.Controls.Add(this.butUninstall);
			this.groupBox1.Controls.Add(this.butInstall);
			this.groupBox1.Controls.Add(this.textStatus);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(12,34);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(394,97);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "OpenDentalHL7";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(463,18);
			this.label1.TabIndex = 1;
			this.label1.Text = "This tool currently manages one service:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6,22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48,18);
			this.label2.TabIndex = 2;
			this.label2.Text = "Status";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textStatus
			// 
			this.textStatus.Location = new System.Drawing.Point(58,21);
			this.textStatus.Name = "textStatus";
			this.textStatus.ReadOnly = true;
			this.textStatus.Size = new System.Drawing.Size(217,20);
			this.textStatus.TabIndex = 3;
			// 
			// butInstall
			// 
			this.butInstall.Location = new System.Drawing.Point(18,52);
			this.butInstall.Name = "butInstall";
			this.butInstall.Size = new System.Drawing.Size(75,23);
			this.butInstall.TabIndex = 0;
			this.butInstall.Text = "Install";
			this.butInstall.UseVisualStyleBackColor = true;
			this.butInstall.Click += new System.EventHandler(this.butInstall_Click);
			// 
			// butUninstall
			// 
			this.butUninstall.Location = new System.Drawing.Point(99,52);
			this.butUninstall.Name = "butUninstall";
			this.butUninstall.Size = new System.Drawing.Size(75,23);
			this.butUninstall.TabIndex = 1;
			this.butUninstall.Text = "Uninstall";
			this.butUninstall.UseVisualStyleBackColor = true;
			this.butUninstall.Click += new System.EventHandler(this.butUninstall_Click);
			// 
			// butStart
			// 
			this.butStart.Location = new System.Drawing.Point(180,52);
			this.butStart.Name = "butStart";
			this.butStart.Size = new System.Drawing.Size(75,23);
			this.butStart.TabIndex = 2;
			this.butStart.Text = "Start";
			this.butStart.UseVisualStyleBackColor = true;
			this.butStart.Click += new System.EventHandler(this.butStart_Click);
			// 
			// butStop
			// 
			this.butStop.Location = new System.Drawing.Point(261,52);
			this.butStop.Name = "butStop";
			this.butStop.Size = new System.Drawing.Size(75,23);
			this.butStop.TabIndex = 3;
			this.butStop.Text = "Stop";
			this.butStop.UseVisualStyleBackColor = true;
			this.butStop.Click += new System.EventHandler(this.butStop_Click);
			// 
			// butRefresh
			// 
			this.butRefresh.Location = new System.Drawing.Point(279,19);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(75,23);
			this.butRefresh.TabIndex = 4;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.UseVisualStyleBackColor = true;
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(430,156);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Service Manager";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butStop;
		private System.Windows.Forms.Button butStart;
		private System.Windows.Forms.Button butUninstall;
		private System.Windows.Forms.Button butInstall;
		private System.Windows.Forms.TextBox textStatus;
		private System.Windows.Forms.Button butRefresh;
	}
}

