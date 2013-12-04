namespace OpenDental{
	partial class FormPatientPortalSetup {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPatientPortalSetup));
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.textPatientPortalURL = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonGetURL = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.textOpenDentalURl = new System.Windows.Forms.TextBox();
			this.textBoxNotificationSubject = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxNotificationBody = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBoxNotification = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBoxNotification.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Location = new System.Drawing.Point(527, 616);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 23);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(609, 616);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 23);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 98);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 17);
			this.label5.TabIndex = 31;
			this.label5.Text = "Patient Portal URL";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPatientPortalURL
			// 
			this.textPatientPortalURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textPatientPortalURL.Location = new System.Drawing.Point(118, 97);
			this.textPatientPortalURL.Name = "textPatientPortalURL";
			this.textPatientPortalURL.Size = new System.Drawing.Size(566, 20);
			this.textPatientPortalURL.TabIndex = 30;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(115, 120);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(564, 171);
			this.label1.TabIndex = 39;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 17);
			this.label2.TabIndex = 40;
			this.label2.Text = "Open Dental URL";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label2.Visible = false;
			// 
			// buttonGetURL
			// 
			this.buttonGetURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonGetURL.Location = new System.Drawing.Point(609, 23);
			this.buttonGetURL.Name = "buttonGetURL";
			this.buttonGetURL.Size = new System.Drawing.Size(75, 23);
			this.buttonGetURL.TabIndex = 41;
			this.buttonGetURL.Text = "Get URL";
			this.buttonGetURL.UseVisualStyleBackColor = true;
			this.buttonGetURL.Visible = false;
			this.buttonGetURL.Click += new System.EventHandler(this.buttonGetURL_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(118, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(589, 37);
			this.label3.TabIndex = 42;
			this.label3.Text = "Only used when Open Dental is hosting your patient portal.  \r\nThis is the URL tha" +
    "t patients need to use to access their portal OR this is the URL to which any re" +
    "directs need to point.";
			this.label3.Visible = false;
			// 
			// textOpenDentalURl
			// 
			this.textOpenDentalURl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textOpenDentalURl.Location = new System.Drawing.Point(118, 25);
			this.textOpenDentalURl.Name = "textOpenDentalURl";
			this.textOpenDentalURl.Size = new System.Drawing.Size(485, 20);
			this.textOpenDentalURl.TabIndex = 43;
			this.textOpenDentalURl.Visible = false;
			// 
			// textBoxNotificationSubject
			// 
			this.textBoxNotificationSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxNotificationSubject.Location = new System.Drawing.Point(93, 19);
			this.textBoxNotificationSubject.Name = "textBoxNotificationSubject";
			this.textBoxNotificationSubject.Size = new System.Drawing.Size(570, 20);
			this.textBoxNotificationSubject.TabIndex = 45;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 20);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(78, 17);
			this.label4.TabIndex = 44;
			this.label4.Text = "Subject";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxNotificationBody
			// 
			this.textBoxNotificationBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxNotificationBody.Location = new System.Drawing.Point(93, 62);
			this.textBoxNotificationBody.Multiline = true;
			this.textBoxNotificationBody.Name = "textBoxNotificationBody";
			this.textBoxNotificationBody.Size = new System.Drawing.Size(570, 247);
			this.textBoxNotificationBody.TabIndex = 46;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(9, 62);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(75, 17);
			this.label6.TabIndex = 47;
			this.label6.Text = "Body";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBoxNotification
			// 
			this.groupBoxNotification.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxNotification.Controls.Add(this.label7);
			this.groupBoxNotification.Controls.Add(this.textBoxNotificationSubject);
			this.groupBoxNotification.Controls.Add(this.label6);
			this.groupBoxNotification.Controls.Add(this.label4);
			this.groupBoxNotification.Controls.Add(this.textBoxNotificationBody);
			this.groupBoxNotification.Location = new System.Drawing.Point(15, 294);
			this.groupBoxNotification.Name = "groupBoxNotification";
			this.groupBoxNotification.Size = new System.Drawing.Size(669, 316);
			this.groupBoxNotification.TabIndex = 48;
			this.groupBoxNotification.TabStop = false;
			this.groupBoxNotification.Text = "Notification Email Settings";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(90, 42);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(573, 17);
			this.label7.TabIndex = 48;
			this.label7.Text = "[URL] will be replaced with the value of \'Patient Portal URL\' as entered above.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormPatientPortalSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(714, 651);
			this.Controls.Add(this.groupBoxNotification);
			this.Controls.Add(this.textOpenDentalURl);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonGetURL);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textPatientPortalURL);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormPatientPortalSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Patient Portal Setup";
			this.Load += new System.EventHandler(this.FormPatientPortalSetup_Load);
			this.groupBoxNotification.ResumeLayout(false);
			this.groupBoxNotification.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textPatientPortalURL;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonGetURL;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textOpenDentalURl;
		private System.Windows.Forms.TextBox textBoxNotificationSubject;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxNotificationBody;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBoxNotification;
		private System.Windows.Forms.Label label7;

	}
}