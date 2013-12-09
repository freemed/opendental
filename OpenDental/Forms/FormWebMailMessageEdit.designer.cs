namespace OpenDental{
	partial class FormWebMailMessageEdit {
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
			this.textTo = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textFrom = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textSubject = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.labelNotification = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemSetup = new System.Windows.Forms.MenuItem();
			this.label5 = new System.Windows.Forms.Label();
			this.comboRegardingPatient = new System.Windows.Forms.ComboBox();
			this.butPreview = new OpenDental.UI.Button();
			this.butSend = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textBody = new OpenDental.ODtextBox();
			this.SuspendLayout();
			// 
			// textTo
			// 
			this.textTo.Location = new System.Drawing.Point(119, 30);
			this.textTo.Name = "textTo";
			this.textTo.ReadOnly = true;
			this.textTo.Size = new System.Drawing.Size(305, 20);
			this.textTo.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(22, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 14);
			this.label1.TabIndex = 11;
			this.label1.Text = "To:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textFrom
			// 
			this.textFrom.Location = new System.Drawing.Point(119, 57);
			this.textFrom.Name = "textFrom";
			this.textFrom.ReadOnly = true;
			this.textFrom.Size = new System.Drawing.Size(305, 20);
			this.textFrom.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(22, 60);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(92, 14);
			this.label3.TabIndex = 13;
			this.label3.Text = "From:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(22, 112);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 14);
			this.label2.TabIndex = 13;
			this.label2.Text = "Message:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSubject
			// 
			this.textSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textSubject.Location = new System.Drawing.Point(119, 84);
			this.textSubject.Name = "textSubject";
			this.textSubject.Size = new System.Drawing.Size(670, 20);
			this.textSubject.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(22, 87);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(92, 14);
			this.label4.TabIndex = 16;
			this.label4.Text = "Subject:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelNotification
			// 
			this.labelNotification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.labelNotification.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelNotification.Location = new System.Drawing.Point(86, 391);
			this.labelNotification.Name = "labelNotification";
			this.labelNotification.Size = new System.Drawing.Size(541, 14);
			this.labelNotification.TabIndex = 17;
			this.labelNotification.Text = "Warning: Patient email is not setup properly. No notification email will be sent " +
    "to this patient.";
			this.labelNotification.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSetup});
			// 
			// menuItemSetup
			// 
			this.menuItemSetup.Index = 0;
			this.menuItemSetup.Text = "Setup";
			this.menuItemSetup.Click += new System.EventHandler(this.menuItemSetup_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(14, 6);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 14);
			this.label5.TabIndex = 19;
			this.label5.Text = "Regarding Patient:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboRegardingPatient
			// 
			this.comboRegardingPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboRegardingPatient.FormattingEnabled = true;
			this.comboRegardingPatient.Location = new System.Drawing.Point(120, 4);
			this.comboRegardingPatient.MaxDropDownItems = 30;
			this.comboRegardingPatient.Name = "comboRegardingPatient";
			this.comboRegardingPatient.Size = new System.Drawing.Size(304, 21);
			this.comboRegardingPatient.TabIndex = 0;
			// 
			// butPreview
			// 
			this.butPreview.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPreview.Autosize = true;
			this.butPreview.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPreview.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPreview.CornerRadius = 4F;
			this.butPreview.Image = global::OpenDental.Properties.Resources.butPreview;
			this.butPreview.Location = new System.Drawing.Point(44, 342);
			this.butPreview.Name = "butPreview";
			this.butPreview.Size = new System.Drawing.Size(69, 24);
			this.butPreview.TabIndex = 5;
			this.butPreview.Text = "&Cancel";
			this.butPreview.Click += new System.EventHandler(this.butPreview_Click);
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.CornerRadius = 4F;
			this.butSend.Location = new System.Drawing.Point(633, 386);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(75, 24);
			this.butSend.TabIndex = 6;
			this.butSend.Text = "&Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(714, 386);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textBody
			// 
			this.textBody.AcceptsTab = true;
			this.textBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBody.DetectUrls = false;
			this.textBody.Location = new System.Drawing.Point(119, 112);
			this.textBody.Name = "textBody";
			this.textBody.QuickPasteType = OpenDentBusiness.QuickPasteType.Email;
			this.textBody.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textBody.Size = new System.Drawing.Size(670, 254);
			this.textBody.TabIndex = 4;
			this.textBody.Text = "";
			// 
			// FormWebMailMessageEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(818, 422);
			this.Controls.Add(this.comboRegardingPatient);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.butPreview);
			this.Controls.Add(this.labelNotification);
			this.Controls.Add(this.textSubject);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBody);
			this.Controls.Add(this.textFrom);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textTo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.butCancel);
			this.Menu = this.mainMenu1;
			this.Name = "FormWebMailMessageEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Web Mail Message Edit";
			this.Load += new System.EventHandler(this.FormWebMailMessageEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butSend;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textTo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textFrom;
		private System.Windows.Forms.Label label3;
		private ODtextBox textBody;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textSubject;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelNotification;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItemSetup;
		private UI.Button butPreview;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox comboRegardingPatient;
	}
}