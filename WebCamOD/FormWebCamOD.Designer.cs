namespace WebCamOD {
	partial class FormWebCamOD {
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
			if(vidCapt!=null){
				vidCapt.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.pictBoxVideo = new System.Windows.Forms.PictureBox();
			this.timerPhoneWebCam = new System.Windows.Forms.Timer(this.components);
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictBoxVideo)).BeginInit();
			this.SuspendLayout();
			// 
			// pictBoxVideo
			// 
			this.pictBoxVideo.Location = new System.Drawing.Point(133,66);
			this.pictBoxVideo.Name = "pictBoxVideo";
			this.pictBoxVideo.Size = new System.Drawing.Size(64,48);
			this.pictBoxVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictBoxVideo.TabIndex = 0;
			this.pictBoxVideo.TabStop = false;
			// 
			// timerPhoneWebCam
			// 
			this.timerPhoneWebCam.Interval = 1600;
			this.timerPhoneWebCam.Tick += new System.EventHandler(this.timerPhoneWebCam_Tick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10,1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(313,58);
			this.label1.TabIndex = 1;
			this.label1.Text = "Please leave this window open at night.  Use \'Switch User\' to lock out of Windows" +
    " without shutting this window down.  Video will continue to be captured for secu" +
    "rity purposes.\r\nThanks, Jordan";
			// 
			// FormWebCamOD
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(335,123);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictBoxVideo);
			this.Name = "FormWebCamOD";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WebCamOD";
			this.Load += new System.EventHandler(this.FormWebCamOD_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictBoxVideo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictBoxVideo;
		private System.Windows.Forms.Timer timerPhoneWebCam;
		private System.Windows.Forms.Label label1;
	}
}

