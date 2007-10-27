using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormUpdateSetup : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private TextBox textWebsitePath;
		private Label label3;
		private TextBox textServer;
		private Label label1;
		private TextBox textRegKey;
		private Label label2;
		private Label label4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormUpdateSetup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUpdateSetup));
			this.textWebsitePath = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textServer = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textRegKey = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textWebsitePath
			// 
			this.textWebsitePath.Location = new System.Drawing.Point(192,32);
			this.textWebsitePath.Name = "textWebsitePath";
			this.textWebsitePath.Size = new System.Drawing.Size(434,20);
			this.textWebsitePath.TabIndex = 36;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12,33);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(180,19);
			this.label3.TabIndex = 37;
			this.label3.Text = "Website Path for Updates";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textServer
			// 
			this.textServer.Location = new System.Drawing.Point(192,67);
			this.textServer.Name = "textServer";
			this.textServer.Size = new System.Drawing.Size(434,20);
			this.textServer.TabIndex = 38;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,68);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(180,19);
			this.label1.TabIndex = 39;
			this.label1.Text = "Server";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textRegKey
			// 
			this.textRegKey.Font = new System.Drawing.Font("Courier New",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textRegKey.Location = new System.Drawing.Point(192,103);
			this.textRegKey.Name = "textRegKey";
			this.textRegKey.Size = new System.Drawing.Size(193,20);
			this.textRegKey.TabIndex = 40;
			this.textRegKey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textRegKey_KeyUp);
			this.textRegKey.TextChanged += new System.EventHandler(this.textRegKey_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12,104);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(180,19);
			this.label2.TabIndex = 41;
			this.label2.Text = "Registration Key";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(391,103);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(265,19);
			this.label4.TabIndex = 42;
			this.label4.Text = "Valid for one office ONLY.  This is tracked.";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(553,222);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
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
			this.butCancel.Location = new System.Drawing.Point(553,263);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormUpdateSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(680,314);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textRegKey);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textServer);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textWebsitePath);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormUpdateSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Update Setup";
			this.Load += new System.EventHandler(this.FormUpdateSetup_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormUpdateSetup_Load(object sender,EventArgs e) {
			textWebsitePath.Text=PrefB.GetString("UpdateWebsitePath");
			textServer.Text=PrefB.GetString("UpdateServerAddress");
			string regkey=PrefB.GetString("RegistrationKey");
			if(regkey.Length==16){
				textRegKey.Text=regkey.Substring(0,4)+"-"+regkey.Substring(4,4)+"-"+regkey.Substring(8,4)+"-"+regkey.Substring(12,4);
			}
			else{
				textRegKey.Text=regkey;
			}
		}

		private void textRegKey_KeyUp(object sender,KeyEventArgs e) {
			int cursor=textRegKey.SelectionStart;
			//textRegKey.Text=textRegKey.Text.ToUpper();
			int length=textRegKey.Text.Length;
			if(Regex.IsMatch(textRegKey.Text,@"^[A-Z0-9]{5}$")) {
				textRegKey.Text=textRegKey.Text.Substring(0,4)+"-"+textRegKey.Text.Substring(4);
			}
			else if(Regex.IsMatch(textRegKey.Text,@"^[A-Z0-9]{4}-[A-Z0-9]{5}$")) {
				textRegKey.Text=textRegKey.Text.Substring(0,9)+"-"+textRegKey.Text.Substring(9);
			}
			else if(Regex.IsMatch(textRegKey.Text,@"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{5}$")) {
				textRegKey.Text=textRegKey.Text.Substring(0,14)+"-"+textRegKey.Text.Substring(14);
			}
			if(textRegKey.Text.Length>length) {
				cursor++;
			}
			textRegKey.SelectionStart=cursor;
		}

		private void textRegKey_TextChanged(object sender,EventArgs e) {
			int cursor=textRegKey.SelectionStart;
			textRegKey.Text=textRegKey.Text.ToUpper();
			textRegKey.SelectionStart=cursor;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textRegKey.Text!="" && !Regex.IsMatch(textRegKey.Text,@"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$")){
				MsgBox.Show(this,"Invalid registration key format.");
				return;
			}
			string regkey=textRegKey.Text.Substring(0,4)+textRegKey.Text.Substring(5,4)
				+textRegKey.Text.Substring(10,4)+textRegKey.Text.Substring(15,4);
			if(Prefs.UpdateString("UpdateWebsitePath",textWebsitePath.Text)
				| Prefs.UpdateString("UpdateServerAddress",textServer.Text)
				| Prefs.UpdateString("RegistrationKey",regkey))
			{
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		


	}
}





















