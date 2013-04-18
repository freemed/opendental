namespace OpenDental {
	partial class FormPatientPortal {
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
			this.components = new System.ComponentModel.Container();
			this.butGiveAccess = new System.Windows.Forms.Button();
			this.textOnlineUsername = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textPatientPortalURL = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.butCancel = new System.Windows.Forms.Button();
			this.butGenerate = new System.Windows.Forms.Button();
			this.textOnlinePassword = new System.Windows.Forms.TextBox();
			this.butOK = new System.Windows.Forms.Button();
			this.butOpen = new System.Windows.Forms.Button();
			this.butPrint = new System.Windows.Forms.Button();
			this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemSetup = new System.Windows.Forms.MenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butGiveAccess
			// 
			this.butGiveAccess.Location = new System.Drawing.Point(140, 12);
			this.butGiveAccess.Name = "butGiveAccess";
			this.butGiveAccess.Size = new System.Drawing.Size(140, 23);
			this.butGiveAccess.TabIndex = 30;
			this.butGiveAccess.Text = "Provide Online Access";
			this.butGiveAccess.UseVisualStyleBackColor = true;
			this.butGiveAccess.Click += new System.EventHandler(this.butGiveAccess_Click);
			// 
			// textOnlineUsername
			// 
			this.textOnlineUsername.Location = new System.Drawing.Point(140, 81);
			this.textOnlineUsername.Name = "textOnlineUsername";
			this.textOnlineUsername.ReadOnly = true;
			this.textOnlineUsername.Size = new System.Drawing.Size(198, 20);
			this.textOnlineUsername.TabIndex = 27;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(37, 57);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 17);
			this.label5.TabIndex = 29;
			this.label5.Text = "Patient Portal URL";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPatientPortalURL
			// 
			this.textPatientPortalURL.Location = new System.Drawing.Point(140, 54);
			this.textPatientPortalURL.Name = "textPatientPortalURL";
			this.textPatientPortalURL.ReadOnly = true;
			this.textPatientPortalURL.Size = new System.Drawing.Size(561, 20);
			this.textPatientPortalURL.TabIndex = 28;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(36, 108);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 17);
			this.label2.TabIndex = 25;
			this.label2.Text = "Online Password";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(36, 82);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 17);
			this.label4.TabIndex = 26;
			this.label4.Text = "Online Username";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(702, 168);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 23);
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butGenerate
			// 
			this.butGenerate.Location = new System.Drawing.Point(344, 106);
			this.butGenerate.Name = "butGenerate";
			this.butGenerate.Size = new System.Drawing.Size(95, 23);
			this.butGenerate.TabIndex = 34;
			this.butGenerate.Text = "Generate New";
			this.butGenerate.UseVisualStyleBackColor = true;
			this.butGenerate.Click += new System.EventHandler(this.butGenerate_Click);
			// 
			// textOnlinePassword
			// 
			this.textOnlinePassword.Location = new System.Drawing.Point(140, 107);
			this.textOnlinePassword.Name = "textOnlinePassword";
			this.textOnlinePassword.ReadOnly = true;
			this.textOnlinePassword.Size = new System.Drawing.Size(198, 20);
			this.textOnlinePassword.TabIndex = 33;
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Location = new System.Drawing.Point(621, 168);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 23);
			this.butOK.TabIndex = 0;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butOpen
			// 
			this.butOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butOpen.Location = new System.Drawing.Point(705, 52);
			this.butOpen.Name = "butOpen";
			this.butOpen.Size = new System.Drawing.Size(70, 23);
			this.butOpen.TabIndex = 36;
			this.butOpen.Text = "Open";
			this.butOpen.UseVisualStyleBackColor = true;
			this.butOpen.Click += new System.EventHandler(this.butOpen_Click);
			// 
			// butPrint
			// 
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrint.Location = new System.Drawing.Point(140, 168);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(75, 23);
			this.butPrint.TabIndex = 37;
			this.butPrint.Text = "Print";
			this.butPrint.UseVisualStyleBackColor = true;
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSetup});
			// 
			// menuItemSetup
			// 
			this.menuItemSetup.Index = 0;
			this.menuItemSetup.Text = "Setup";
			this.menuItemSetup.Click += new System.EventHandler(this.menuItemSetup_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(137, 130);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(302, 17);
			this.label1.TabIndex = 38;
			this.label1.Text = "Existing passwords will show as asterisks.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormPatientPortal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(789, 203);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butOpen);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butGenerate);
			this.Controls.Add(this.textOnlinePassword);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butGiveAccess);
			this.Controls.Add(this.textOnlineUsername);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textPatientPortalURL);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Menu = this.mainMenu;
			this.Name = "FormPatientPortal";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Patient Portal";
			this.Load += new System.EventHandler(this.FormPatientPortal_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butGiveAccess;
		private System.Windows.Forms.TextBox textOnlineUsername;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textPatientPortalURL;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butGenerate;
		private System.Windows.Forms.TextBox textOnlinePassword;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butOpen;
		private System.Windows.Forms.Button butPrint;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItemSetup;
		private System.Windows.Forms.Label label1;

	}
}