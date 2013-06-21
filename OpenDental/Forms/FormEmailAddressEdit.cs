using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormEmailAddressEdit:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textSMTPserver;
		private System.Windows.Forms.TextBox textSender;
		private System.Windows.Forms.TextBox textBox1;
		private TextBox textUsername;
		private Label label3;
		private TextBox textPassword;
		private Label label4;
		private TextBox textPort;
		private Label label5;
		private Label label6;
		private CheckBox checkSSL;
		private Label label7;
		private System.ComponentModel.Container components = null;
		private UI.Button butDelete;
		public EmailAddress EmailAddressCur;
		private GroupBox groupOutgoing;
		private GroupBox groupIncoming;
		private TextBox textSMTPserverIncoming;
		private Label label8;
		private Label label10;
		private TextBox textPortIncoming;
		private TextBox textBox5;
		private Label label11;
		public bool IsNew;

		///<summary></summary>
		public FormEmailAddressEdit() {
			InitializeComponent();
			Lan.F(this);
			Lan.C(this, new System.Windows.Forms.Control[]
			{
				textBox1,
				textBox5
			});
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEmailAddressEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textSMTPserver = new System.Windows.Forms.TextBox();
			this.textSender = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textUsername = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textPort = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.checkSSL = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.groupOutgoing = new System.Windows.Forms.GroupBox();
			this.groupIncoming = new System.Windows.Forms.GroupBox();
			this.textSMTPserverIncoming = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.textPortIncoming = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.groupOutgoing.SuspendLayout();
			this.groupIncoming.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(478,384);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(397,384);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 6;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(173,16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Outgoing SMTP Server";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6,145);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(177,20);
			this.label2.TabIndex = 3;
			this.label2.Text = "E-mail address of sender";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSMTPserver
			// 
			this.textSMTPserver.Location = new System.Drawing.Point(185,19);
			this.textSMTPserver.Name = "textSMTPserver";
			this.textSMTPserver.Size = new System.Drawing.Size(218,20);
			this.textSMTPserver.TabIndex = 0;
			// 
			// textSender
			// 
			this.textSender.Location = new System.Drawing.Point(185,142);
			this.textSender.Name = "textSender";
			this.textSender.Size = new System.Drawing.Size(340,20);
			this.textSender.TabIndex = 5;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(187,42);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(198,74);
			this.textBox1.TabIndex = 16;
			this.textBox1.Text = "smtp.comcast.net\r\nmailhost.mycompany.com \r\nmail.mycompany.com\r\nsmtp.gmail.com\r\nor" +
    " similar...";
			// 
			// textUsername
			// 
			this.textUsername.Location = new System.Drawing.Point(197,6);
			this.textUsername.Name = "textUsername";
			this.textUsername.Size = new System.Drawing.Size(218,20);
			this.textUsername.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(97,9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98,20);
			this.label3.TabIndex = 18;
			this.label3.Text = "Username";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(197,26);
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '*';
			this.textPassword.Size = new System.Drawing.Size(218,20);
			this.textPassword.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(18,29);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(177,20);
			this.label4.TabIndex = 20;
			this.label4.Text = "Password";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPort
			// 
			this.textPort.Location = new System.Drawing.Point(185,114);
			this.textPort.Name = "textPort";
			this.textPort.Size = new System.Drawing.Size(56,20);
			this.textPort.TabIndex = 3;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(9,117);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(174,20);
			this.label5.TabIndex = 22;
			this.label5.Text = "Outgoing Port";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(247,117);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(251,20);
			this.label6.TabIndex = 23;
			this.label6.Text = "Usually 587.  Sometimes 25 or 465.";
			// 
			// checkSSL
			// 
			this.checkSSL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkSSL.Location = new System.Drawing.Point(21,48);
			this.checkSSL.Name = "checkSSL";
			this.checkSSL.Size = new System.Drawing.Size(190,17);
			this.checkSSL.TabIndex = 4;
			this.checkSSL.Text = "Use SSL";
			this.checkSSL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkSSL.UseVisualStyleBackColor = true;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(217,49);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(251,20);
			this.label7.TabIndex = 25;
			this.label7.Text = "For Gmail and some others";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(12,384);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,24);
			this.butDelete.TabIndex = 6;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// groupOutgoing
			// 
			this.groupOutgoing.Controls.Add(this.textSMTPserver);
			this.groupOutgoing.Controls.Add(this.label1);
			this.groupOutgoing.Controls.Add(this.label2);
			this.groupOutgoing.Controls.Add(this.label6);
			this.groupOutgoing.Controls.Add(this.textSender);
			this.groupOutgoing.Controls.Add(this.textPort);
			this.groupOutgoing.Controls.Add(this.textBox1);
			this.groupOutgoing.Controls.Add(this.label5);
			this.groupOutgoing.Location = new System.Drawing.Point(12,73);
			this.groupOutgoing.Name = "groupOutgoing";
			this.groupOutgoing.Size = new System.Drawing.Size(541,180);
			this.groupOutgoing.TabIndex = 26;
			this.groupOutgoing.TabStop = false;
			this.groupOutgoing.Text = "Outgoing Email Settings";
			// 
			// groupIncoming
			// 
			this.groupIncoming.Controls.Add(this.textSMTPserverIncoming);
			this.groupIncoming.Controls.Add(this.label8);
			this.groupIncoming.Controls.Add(this.label10);
			this.groupIncoming.Controls.Add(this.textPortIncoming);
			this.groupIncoming.Controls.Add(this.textBox5);
			this.groupIncoming.Controls.Add(this.label11);
			this.groupIncoming.Location = new System.Drawing.Point(12,259);
			this.groupIncoming.Name = "groupIncoming";
			this.groupIncoming.Size = new System.Drawing.Size(541,116);
			this.groupIncoming.TabIndex = 27;
			this.groupIncoming.TabStop = false;
			this.groupIncoming.Text = "Incoming Email Settings";
			// 
			// textSMTPserverIncoming
			// 
			this.textSMTPserverIncoming.Location = new System.Drawing.Point(185,19);
			this.textSMTPserverIncoming.Name = "textSMTPserverIncoming";
			this.textSMTPserverIncoming.Size = new System.Drawing.Size(218,20);
			this.textSMTPserverIncoming.TabIndex = 0;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(12,22);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(173,16);
			this.label8.TabIndex = 2;
			this.label8.Text = "Incoming SMTP Server";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(247,91);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(251,20);
			this.label10.TabIndex = 23;
			this.label10.Text = "Usually 110.  Sometimes 995.";
			// 
			// textPortIncoming
			// 
			this.textPortIncoming.Location = new System.Drawing.Point(185,88);
			this.textPortIncoming.Name = "textPortIncoming";
			this.textPortIncoming.Size = new System.Drawing.Size(56,20);
			this.textPortIncoming.TabIndex = 3;
			// 
			// textBox5
			// 
			this.textBox5.BackColor = System.Drawing.SystemColors.Control;
			this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox5.Location = new System.Drawing.Point(187,42);
			this.textBox5.Multiline = true;
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(198,48);
			this.textBox5.TabIndex = 16;
			this.textBox5.Text = "pop.secureserver.net\r\npop.gmail.com\r\nor similar...";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(9,91);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(174,20);
			this.label11.TabIndex = 22;
			this.label11.Text = "Incoming Port";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormEmailAddressEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(569,424);
			this.Controls.Add(this.groupIncoming);
			this.Controls.Add(this.groupOutgoing);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.checkSSL);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.textUsername);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.label3);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEmailAddressEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit E-mail Address";
			this.Load += new System.EventHandler(this.FormEmailAddress_Load);
			this.groupOutgoing.ResumeLayout(false);
			this.groupOutgoing.PerformLayout();
			this.groupIncoming.ResumeLayout(false);
			this.groupIncoming.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormEmailAddress_Load(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.SecurityAdmin,true)){
				textPassword.PasswordChar='*';
			}
			if(EmailAddressCur!=null) {
				textSMTPserver.Text=EmailAddressCur.SMTPserver;
				textUsername.Text=EmailAddressCur.EmailUsername;
				textPassword.Text=EmailAddressCur.EmailPassword;
				textPort.Text=EmailAddressCur.ServerPort.ToString();
				checkSSL.Checked=EmailAddressCur.UseSSL;
				textSender.Text=EmailAddressCur.SenderAddress;
				textSMTPserverIncoming.Text=EmailAddressCur.SMTPserverIncoming;
				textPortIncoming.Text=EmailAddressCur.ServerPortIncoming.ToString();
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete this email address?")){
				return;
			}
			EmailAddresses.Delete(EmailAddressCur.EmailAddressNum);
			DialogResult=DialogResult.OK;//OK triggers a refresh for the grid.
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			EmailAddressCur.SMTPserver=PIn.String(textSMTPserver.Text);
			EmailAddressCur.EmailUsername=PIn.String(textUsername.Text);
			EmailAddressCur.EmailPassword=PIn.String(textPassword.Text);
			try{
			  EmailAddressCur.ServerPort=PIn.Int(textPort.Text);
			}
			catch{
			  MsgBox.Show(this,"invalid outgoing port number.");
			}
			EmailAddressCur.UseSSL=checkSSL.Checked;
			EmailAddressCur.SenderAddress=PIn.String(textSender.Text);
			EmailAddressCur.SMTPserverIncoming=PIn.String(textSMTPserverIncoming.Text);
			try {
				EmailAddressCur.ServerPortIncoming=PIn.Int(textPortIncoming.Text);
			}
			catch {
				MsgBox.Show(this,"invalid incoming port number.");
			}
			if(IsNew) {
				EmailAddresses.Insert(EmailAddressCur);
			}
			else {
				EmailAddresses.Update(EmailAddressCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
