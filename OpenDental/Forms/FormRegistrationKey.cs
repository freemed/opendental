using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormRegistrationKey : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private Label label1;
		private TextBox textKey1;
		private TextBox textKey2;
		private TextBox textKey3;
		private TextBox textKey4;
		private Label label2;
		private CheckBox checkAgree;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private string prevKey1Text="";
		private string prevKey2Text="";
		private string prevKey3Text="";
		private RichTextBox richTextAgreement;
		private string prevKey4Text="";

		///<summary></summary>
		public FormRegistrationKey()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegistrationKey));
			this.label1 = new System.Windows.Forms.Label();
			this.textKey1 = new System.Windows.Forms.TextBox();
			this.textKey2 = new System.Windows.Forms.TextBox();
			this.textKey3 = new System.Windows.Forms.TextBox();
			this.textKey4 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkAgree = new System.Windows.Forms.CheckBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.richTextAgreement = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(164,6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(266,19);
			this.label1.TabIndex = 2;
			this.label1.Text = "Registration Key";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textKey1
			// 
			this.textKey1.Location = new System.Drawing.Point(164,29);
			this.textKey1.Name = "textKey1";
			this.textKey1.Size = new System.Drawing.Size(62,20);
			this.textKey1.TabIndex = 0;
			this.textKey1.WordWrap = false;
			this.textKey1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textKey1_KeyPress);
			this.textKey1.TextChanged += new System.EventHandler(this.textKey1_TextChanged);
			// 
			// textKey2
			// 
			this.textKey2.Location = new System.Drawing.Point(232,29);
			this.textKey2.Name = "textKey2";
			this.textKey2.Size = new System.Drawing.Size(62,20);
			this.textKey2.TabIndex = 1;
			this.textKey2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textKey2_KeyPress);
			this.textKey2.TextChanged += new System.EventHandler(this.textKey2_TextChanged);
			// 
			// textKey3
			// 
			this.textKey3.Location = new System.Drawing.Point(300,29);
			this.textKey3.Name = "textKey3";
			this.textKey3.Size = new System.Drawing.Size(62,20);
			this.textKey3.TabIndex = 2;
			this.textKey3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textKey3_KeyPress);
			this.textKey3.TextChanged += new System.EventHandler(this.textKey3_TextChanged);
			// 
			// textKey4
			// 
			this.textKey4.Location = new System.Drawing.Point(368,29);
			this.textKey4.Name = "textKey4";
			this.textKey4.Size = new System.Drawing.Size(62,20);
			this.textKey4.TabIndex = 3;
			this.textKey4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textKey4_KeyPress);
			this.textKey4.TextChanged += new System.EventHandler(this.textKey4_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(26,66);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(150,13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Licence Agreement";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkAgree
			// 
			this.checkAgree.Location = new System.Drawing.Point(26,559);
			this.checkAgree.Name = "checkAgree";
			this.checkAgree.Size = new System.Drawing.Size(373,17);
			this.checkAgree.TabIndex = 7;
			this.checkAgree.Text = "I agree to the terms of the above license agreement in its entirety.";
			this.checkAgree.UseVisualStyleBackColor = true;
			this.checkAgree.CheckedChanged += new System.EventHandler(this.checkAgree_CheckedChanged);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Enabled = false;
			this.butOK.Location = new System.Drawing.Point(546,557);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 4;
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
			this.butCancel.Location = new System.Drawing.Point(627,557);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// richTextAgreement
			// 
			this.richTextAgreement.Location = new System.Drawing.Point(26,82);
			this.richTextAgreement.Name = "richTextAgreement";
			this.richTextAgreement.Size = new System.Drawing.Size(675,465);
			this.richTextAgreement.TabIndex = 8;
			this.richTextAgreement.Text = "";
			// 
			// FormRegistrationKey
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(720,597);
			this.Controls.Add(this.richTextAgreement);
			this.Controls.Add(this.checkAgree);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textKey1);
			this.Controls.Add(this.textKey4);
			this.Controls.Add(this.textKey3);
			this.Controls.Add(this.textKey2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRegistrationKey";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Registration Key";
			this.Load += new System.EventHandler(this.FormRegistrationKey_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRegistrationKey_Load(object sender,EventArgs e) {
			string key=PrefB.GetString("RegistrationKey");
			if(key!=null && key.Length==16){
				textKey1.Text=key.Substring(0,4);
				textKey2.Text=key.Substring(4,4);
				textKey3.Text=key.Substring(8,4);
				textKey4.Text=key.Substring(12,4);
			}
			richTextAgreement.Rtf=Properties.Resources.CDT_Content_End_User_License;  //LoadFile(
		}

		public static bool ValidateKey(string keystr){
			return CDT.Class1.ValidateKey(keystr);
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			string keyattempt=textKey1.Text+textKey2.Text+textKey3.Text+textKey4.Text;
			if(!ValidateKey(keyattempt)){
				MsgBox.Show(this,"Invalid registration key.");
				return;
			}
			Prefs.UpdateString("RegistrationKey",keyattempt);
			//prefs refresh automatically in the calling class anyway.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void checkAgree_CheckedChanged(object sender,EventArgs e) {
			butOK.Enabled=checkAgree.Checked;
		}

		private void textKey1_TextChanged(object sender,EventArgs e) {
			if(textKey1.Text.Length==4){
				textKey2.Select();
			}else if(textKey1.Text.Length>4){
				textKey1.Text=prevKey1Text;
				textKey1.Invalidate();
				textKey2.Select();
			}
			prevKey1Text=textKey1.Text;
		}

		private void textKey2_TextChanged(object sender,EventArgs e) {
			if(textKey2.Text.Length==4) {
				textKey3.Select();
			}else if(textKey2.Text.Length>4) {
				textKey2.Text=prevKey2Text;
				textKey2.Invalidate();
				textKey3.Select();
			}
			prevKey2Text=textKey2.Text;
		}

		private void textKey3_TextChanged(object sender,EventArgs e) {
			if(textKey3.Text.Length==4) {
				textKey4.Select();
			}else if(textKey3.Text.Length>4) {
				textKey3.Text=prevKey3Text;
				textKey3.Invalidate();
				textKey4.Select();
			}
			prevKey3Text=textKey3.Text;
		}

		private void textKey4_TextChanged(object sender,EventArgs e) {
			if(textKey4.Text.Length>4) {
				textKey4.Text=prevKey4Text;
				textKey4.Invalidate();
			}
			prevKey4Text=textKey4.Text;
		}

		private void textKey1_KeyPress(object sender,KeyPressEventArgs e) {
			e.KeyChar=(""+e.KeyChar).ToUpper()[0];
		}

		private void textKey2_KeyPress(object sender,KeyPressEventArgs e) {
			e.KeyChar=(""+e.KeyChar).ToUpper()[0];
		}

		private void textKey3_KeyPress(object sender,KeyPressEventArgs e) {
			e.KeyChar=(""+e.KeyChar).ToUpper()[0];
		}

		private void textKey4_KeyPress(object sender,KeyPressEventArgs e) {
			e.KeyChar=(""+e.KeyChar).ToUpper()[0];
		}

	}
}





















