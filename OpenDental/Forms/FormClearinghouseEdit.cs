using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormClearinghouseEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.CheckBox checkIsDefault;
		private System.Windows.Forms.TextBox textPayors;
		private System.Windows.Forms.Label label3;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.TextBox textExportPath;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textReceiverID;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox comboFormat;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textResponsePath;
		private System.Windows.Forms.ComboBox comboCommBridge;
		private System.Windows.Forms.TextBox textClientProgram;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textModemPort;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textLoginID;
		private System.Windows.Forms.Label label15;
		///<summary>Set this externally before opening the form</summary>
		public Clearinghouse ClearinghouseCur;

		///<summary></summary>
		public FormClearinghouseEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			Lan.C(this, new System.Windows.Forms.Control[]
			{
				this.textBox2
			});
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClearinghouseEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textExportPath = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.checkIsDefault = new System.Windows.Forms.CheckBox();
			this.textPayors = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textReceiverID = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textResponsePath = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.comboFormat = new System.Windows.Forms.ComboBox();
			this.comboCommBridge = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textClientProgram = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.textModemPort = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.textLoginID = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
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
			this.butCancel.Location = new System.Drawing.Point(570,527);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(78,26);
			this.butCancel.TabIndex = 13;
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
			this.butOK.Location = new System.Drawing.Point(471,527);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(78,26);
			this.butOK.TabIndex = 12;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(177,34);
			this.textDescription.MaxLength = 255;
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(226,20);
			this.textDescription.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(4,125);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(172,17);
			this.label2.TabIndex = 6;
			this.label2.Text = "Claim Export Path";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textExportPath
			// 
			this.textExportPath.Location = new System.Drawing.Point(177,122);
			this.textExportPath.MaxLength = 255;
			this.textExportPath.Name = "textExportPath";
			this.textExportPath.Size = new System.Drawing.Size(317,20);
			this.textExportPath.TabIndex = 4;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(24,37);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(151,17);
			this.label6.TabIndex = 14;
			this.label6.Text = "Description";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(35,527);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(90,26);
			this.butDelete.TabIndex = 24;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// checkIsDefault
			// 
			this.checkIsDefault.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsDefault.Location = new System.Drawing.Point(27,257);
			this.checkIsDefault.Name = "checkIsDefault";
			this.checkIsDefault.Size = new System.Drawing.Size(163,17);
			this.checkIsDefault.TabIndex = 10;
			this.checkIsDefault.Text = "Is Default";
			this.checkIsDefault.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayors
			// 
			this.textPayors.Location = new System.Drawing.Point(177,276);
			this.textPayors.MaxLength = 255;
			this.textPayors.Multiline = true;
			this.textPayors.Name = "textPayors";
			this.textPayors.Size = new System.Drawing.Size(377,135);
			this.textPayors.TabIndex = 11;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(25,279);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(151,17);
			this.label1.TabIndex = 95;
			this.label1.Text = "Payors";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBox2
			// 
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Location = new System.Drawing.Point(178,417);
			this.textBox2.MaxLength = 255;
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(378,60);
			this.textBox2.TabIndex = 96;
			this.textBox2.Text = "The list of payor IDs should be separated by commas with no spaces or other punct" +
    "uation.  For instance: 01234,23456 is valid.  You do not have to enter any payor" +
    " ID\'s for a default Clearinghouse.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(196,258);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(366,15);
			this.label3.TabIndex = 97;
			this.label3.Text = "(if this is your main clearinghouse for most claims)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(25,168);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(151,17);
			this.label4.TabIndex = 98;
			this.label4.Text = "Format";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textReceiverID
			// 
			this.textReceiverID.Location = new System.Drawing.Point(177,56);
			this.textReceiverID.MaxLength = 255;
			this.textReceiverID.Name = "textReceiverID";
			this.textReceiverID.Size = new System.Drawing.Size(96,20);
			this.textReceiverID.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24,59);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(151,17);
			this.label5.TabIndex = 101;
			this.label5.Text = "Clearinghouse ID (ISA08)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textResponsePath
			// 
			this.textResponsePath.Location = new System.Drawing.Point(177,144);
			this.textResponsePath.MaxLength = 255;
			this.textResponsePath.Name = "textResponsePath";
			this.textResponsePath.Size = new System.Drawing.Size(317,20);
			this.textResponsePath.TabIndex = 5;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(4,147);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(172,17);
			this.label10.TabIndex = 107;
			this.label10.Text = "Report Path";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(177,100);
			this.textPassword.MaxLength = 255;
			this.textPassword.Name = "textPassword";
			this.textPassword.Size = new System.Drawing.Size(96,20);
			this.textPassword.TabIndex = 3;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(24,103);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(151,17);
			this.label11.TabIndex = 109;
			this.label11.Text = "Password";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboFormat
			// 
			this.comboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFormat.Location = new System.Drawing.Point(177,166);
			this.comboFormat.Name = "comboFormat";
			this.comboFormat.Size = new System.Drawing.Size(145,21);
			this.comboFormat.TabIndex = 6;
			// 
			// comboCommBridge
			// 
			this.comboCommBridge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCommBridge.Location = new System.Drawing.Point(177,189);
			this.comboCommBridge.MaxDropDownItems = 20;
			this.comboCommBridge.Name = "comboCommBridge";
			this.comboCommBridge.Size = new System.Drawing.Size(145,21);
			this.comboCommBridge.TabIndex = 7;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(24,192);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(151,17);
			this.label7.TabIndex = 111;
			this.label7.Text = "Comm Bridge";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textClientProgram
			// 
			this.textClientProgram.Location = new System.Drawing.Point(177,234);
			this.textClientProgram.MaxLength = 255;
			this.textClientProgram.Name = "textClientProgram";
			this.textClientProgram.Size = new System.Drawing.Size(317,20);
			this.textClientProgram.TabIndex = 9;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(4,237);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(172,17);
			this.label8.TabIndex = 114;
			this.label8.Text = "Launch Client Program";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(42,8);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(639,20);
			this.label12.TabIndex = 115;
			this.label12.Text = "Not all values are required by each clearinghouse.  You should read the manual to" +
    " see how to fill this out.";
			// 
			// textModemPort
			// 
			this.textModemPort.Location = new System.Drawing.Point(177,212);
			this.textModemPort.MaxLength = 255;
			this.textModemPort.Name = "textModemPort";
			this.textModemPort.Size = new System.Drawing.Size(32,20);
			this.textModemPort.TabIndex = 8;
			this.textModemPort.Visible = false;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(4,215);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(172,17);
			this.label13.TabIndex = 117;
			this.label13.Text = "Modem Port (1-4)";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.label13.Visible = false;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(213,216);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(267,17);
			this.label14.TabIndex = 118;
			this.label14.Text = "(only if dialup)";
			this.label14.Visible = false;
			// 
			// textLoginID
			// 
			this.textLoginID.Location = new System.Drawing.Point(177,78);
			this.textLoginID.MaxLength = 255;
			this.textLoginID.Name = "textLoginID";
			this.textLoginID.Size = new System.Drawing.Size(96,20);
			this.textLoginID.TabIndex = 2;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(24,81);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(151,17);
			this.label15.TabIndex = 120;
			this.label15.Text = "Login ID";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormClearinghouseEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(703,573);
			this.Controls.Add(this.textLoginID);
			this.Controls.Add(this.textModemPort);
			this.Controls.Add(this.textClientProgram);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.textResponsePath);
			this.Controls.Add(this.textReceiverID);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textPayors);
			this.Controls.Add(this.textExportPath);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.comboCommBridge);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.comboFormat);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkIsDefault);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClearinghouseEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Clearinghouse";
			this.Load += new System.EventHandler(this.FormClearinghouseEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClearinghouseEdit_Load(object sender, System.EventArgs e) {
			textDescription.Text=ClearinghouseCur.Description;
			textReceiverID.Text=ClearinghouseCur.ReceiverID;
			//textSenderID.Text=ClearinghouseCur.SenderID;
			textLoginID.Text=ClearinghouseCur.LoginID;
			textPassword.Text=ClearinghouseCur.Password;
			textExportPath.Text=ClearinghouseCur.ExportPath;
			textResponsePath.Text=ClearinghouseCur.ResponsePath;
			for(int i=0;i<Enum.GetNames(typeof(ElectronicClaimFormat)).Length;i++){
				comboFormat.Items.Add(Enum.GetNames(typeof(ElectronicClaimFormat))[i]);
			}
			comboFormat.SelectedIndex=(int)ClearinghouseCur.Eformat;
			for(int i=0;i<Enum.GetNames(typeof(EclaimsCommBridge)).Length;i++){
				comboCommBridge.Items.Add(Enum.GetNames(typeof(EclaimsCommBridge))[i]);
			}
			comboCommBridge.SelectedIndex=(int)ClearinghouseCur.CommBridge;
			textModemPort.Text=ClearinghouseCur.ModemPort.ToString();
			textClientProgram.Text=ClearinghouseCur.ClientProgram;
			checkIsDefault.Checked=ClearinghouseCur.IsDefault;
			textPayors.Text=ClearinghouseCur.Payors;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete this Clearinghouse?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			Clearinghouses.Delete(ClearinghouseCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDescription.Text==""){
				MessageBox.Show(Lan.g(this,"Clearinghouse name cannot be blank."));
				return;
			}
			if(textExportPath.Text!="" && !textExportPath.Text.EndsWith(""+Path.DirectorySeparatorChar)){
				MsgBox.Show(this,"Paths must end in "+Path.DirectorySeparatorChar);
				return;
			}
			if(textResponsePath.Text!="" && !textResponsePath.Text.EndsWith(""+Path.DirectorySeparatorChar)){
				MsgBox.Show(this,"Paths must end in "+Path.DirectorySeparatorChar);
				return;
			}
			if(!Directory.Exists(textExportPath.Text)){
				if(MessageBox.Show("Export path does not exist. Continue anyway?",""
					,MessageBoxButtons.OKCancel)!=DialogResult.OK)
				{
					return;
				}
			}
			if(comboFormat.SelectedIndex==0){
				if(MessageBox.Show("Format not selected. Claims will not send. Continue anyway?",""
					,MessageBoxButtons.OKCancel)!=DialogResult.OK)
				{
					return;
				}
			}
			if(comboFormat.SelectedIndex==(int)ElectronicClaimFormat.X12){
				if(textReceiverID.Text!="BCBSGA"
					&& textReceiverID.Text!="100000"//Medicaid of GA
					&& textReceiverID.Text!="0135WCH00"//WebMD
					&& textReceiverID.Text!="330989922"//WebClaim
					&& textReceiverID.Text!="RECS"
					&& textReceiverID.Text!="AOS"
					&& textReceiverID.Text!="PostnTrack"
					)
				{
					if(!MsgBox.Show(this,true,"Clearinghouse ID not recognized. Continue anyway?")){
						return;
					}
				}
			}
			ClearinghouseCur.Description=textDescription.Text;
			ClearinghouseCur.ReceiverID=textReceiverID.Text;
			//ClearinghouseCur.SenderID=textSenderID.Text;
			ClearinghouseCur.LoginID=textLoginID.Text;
			ClearinghouseCur.Password=textPassword.Text;
			ClearinghouseCur.ExportPath=textExportPath.Text;
			ClearinghouseCur.ResponsePath=textResponsePath.Text;
			ClearinghouseCur.Eformat=(ElectronicClaimFormat)(comboFormat.SelectedIndex);
			ClearinghouseCur.CommBridge=(EclaimsCommBridge)(comboCommBridge.SelectedIndex);
			ClearinghouseCur.ModemPort=PIn.PInt(textModemPort.Text);
			ClearinghouseCur.ClientProgram=textClientProgram.Text;
			ClearinghouseCur.IsDefault=checkIsDefault.Checked;
			ClearinghouseCur.Payors=textPayors.Text;
			if(IsNew){
				Clearinghouses.Insert(ClearinghouseCur);
			}
			else{
				Clearinghouses.Update(ClearinghouseCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


		

		

		

		

		

		

		


	}
}





















