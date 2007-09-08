using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormTrojanCollect : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butHelp;
		private Label label1;
		private Label labelGuarantor;
		private Label labelAddress;
		private Label labelCityStZip;
		private Label label4;
		private Label label5;
		private Label label6;
		private Label labelSSN;
		private Label labelDOB;
		private Label labelPhone;
		private Label label10;
		private Label labelEmpPhone;
		private Label labelEmployer;
		private Label label13;
		private Label labelPatient;
		private Label label15;
		private Label label16;
		private TextBox textDate;
		private TextBox textAmount;
		private Label label17;
		private RadioButton radioDiplomatic;
		private RadioButton radioFirm;
		private RadioButton radioSkip;
		private Label label18;
		private TextBox textPassword;
		private MainMenu mainMenu1;
		private MenuItem menuItemSetup;
		private IContainer components;

		///<summary></summary>
		public FormTrojanCollect()
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTrojanCollect));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butHelp = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.labelGuarantor = new System.Windows.Forms.Label();
			this.labelAddress = new System.Windows.Forms.Label();
			this.labelCityStZip = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.labelSSN = new System.Windows.Forms.Label();
			this.labelDOB = new System.Windows.Forms.Label();
			this.labelPhone = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.labelEmpPhone = new System.Windows.Forms.Label();
			this.labelEmployer = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.labelPatient = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.textDate = new System.Windows.Forms.TextBox();
			this.textAmount = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.radioDiplomatic = new System.Windows.Forms.RadioButton();
			this.radioFirm = new System.Windows.Forms.RadioButton();
			this.radioSkip = new System.Windows.Forms.RadioButton();
			this.label18 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemSetup = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(275,338);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(47,338);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(182,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "OK Send Transaction to Trojan";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butHelp
			// 
			this.butHelp.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butHelp.Autosize = true;
			this.butHelp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHelp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHelp.CornerRadius = 4F;
			this.butHelp.Location = new System.Drawing.Point(391,338);
			this.butHelp.Name = "butHelp";
			this.butHelp.Size = new System.Drawing.Size(69,26);
			this.butHelp.TabIndex = 2;
			this.butHelp.Text = "Help";
			this.butHelp.Click += new System.EventHandler(this.butHelp_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(44,24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(238,16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Financially Responsible Person:";
			// 
			// labelGuarantor
			// 
			this.labelGuarantor.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelGuarantor.Location = new System.Drawing.Point(50,46);
			this.labelGuarantor.Name = "labelGuarantor";
			this.labelGuarantor.Size = new System.Drawing.Size(226,16);
			this.labelGuarantor.TabIndex = 4;
			this.labelGuarantor.Text = "Joe Smith";
			// 
			// labelAddress
			// 
			this.labelAddress.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelAddress.Location = new System.Drawing.Point(50,64);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(226,16);
			this.labelAddress.TabIndex = 5;
			this.labelAddress.Text = "123 E St.";
			// 
			// labelCityStZip
			// 
			this.labelCityStZip.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelCityStZip.Location = new System.Drawing.Point(50,82);
			this.labelCityStZip.Name = "labelCityStZip";
			this.labelCityStZip.Size = new System.Drawing.Size(226,16);
			this.labelCityStZip.TabIndex = 6;
			this.labelCityStZip.Text = "Los Angeles, CA 20212";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(282,46);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45,16);
			this.label4.TabIndex = 7;
			this.label4.Text = "SS#:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(282,64);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(45,16);
			this.label5.TabIndex = 8;
			this.label5.Text = "DOB:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(282,82);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(45,16);
			this.label6.TabIndex = 9;
			this.label6.Text = "Phone:";
			// 
			// labelSSN
			// 
			this.labelSSN.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelSSN.Location = new System.Drawing.Point(324,46);
			this.labelSSN.Name = "labelSSN";
			this.labelSSN.Size = new System.Drawing.Size(144,16);
			this.labelSSN.TabIndex = 10;
			this.labelSSN.Text = "123-12-1234";
			// 
			// labelDOB
			// 
			this.labelDOB.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelDOB.Location = new System.Drawing.Point(324,64);
			this.labelDOB.Name = "labelDOB";
			this.labelDOB.Size = new System.Drawing.Size(155,16);
			this.labelDOB.TabIndex = 11;
			this.labelDOB.Text = "01/10/1980";
			// 
			// labelPhone
			// 
			this.labelPhone.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelPhone.Location = new System.Drawing.Point(324,82);
			this.labelPhone.Name = "labelPhone";
			this.labelPhone.Size = new System.Drawing.Size(155,16);
			this.labelPhone.TabIndex = 12;
			this.labelPhone.Text = "(310)555-1212";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(44,114);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(62,16);
			this.label10.TabIndex = 13;
			this.label10.Text = "Employer:";
			// 
			// labelEmpPhone
			// 
			this.labelEmpPhone.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelEmpPhone.Location = new System.Drawing.Point(101,132);
			this.labelEmpPhone.Name = "labelEmpPhone";
			this.labelEmpPhone.Size = new System.Drawing.Size(249,16);
			this.labelEmpPhone.TabIndex = 15;
			this.labelEmpPhone.Text = "(310)665-5544";
			// 
			// labelEmployer
			// 
			this.labelEmployer.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelEmployer.Location = new System.Drawing.Point(101,114);
			this.labelEmployer.Name = "labelEmployer";
			this.labelEmployer.Size = new System.Drawing.Size(249,16);
			this.labelEmployer.TabIndex = 14;
			this.labelEmployer.Text = "Ace, Inc.";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(44,162);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(62,16);
			this.label13.TabIndex = 16;
			this.label13.Text = "Patient:";
			// 
			// labelPatient
			// 
			this.labelPatient.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelPatient.Location = new System.Drawing.Point(59,180);
			this.labelPatient.Name = "labelPatient";
			this.labelPatient.Size = new System.Drawing.Size(243,16);
			this.labelPatient.TabIndex = 17;
			this.labelPatient.Text = "Mary Smith";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(44,232);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(103,16);
			this.label15.TabIndex = 19;
			this.label15.Text = "Amount of debt";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(44,210);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(103,16);
			this.label16.TabIndex = 18;
			this.label16.Text = "Delinquency Date";
			// 
			// textDate
			// 
			this.textDate.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textDate.Location = new System.Drawing.Point(144,207);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(92,20);
			this.textDate.TabIndex = 20;
			this.textDate.Text = "01/25/2007";
			// 
			// textAmount
			// 
			this.textAmount.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textAmount.Location = new System.Drawing.Point(144,229);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(70,20);
			this.textAmount.TabIndex = 21;
			this.textAmount.Text = "123.45";
			this.textAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(44,267);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(103,16);
			this.label17.TabIndex = 22;
			this.label17.Text = "Transaction type:";
			// 
			// radioDiplomatic
			// 
			this.radioDiplomatic.Location = new System.Drawing.Point(144,266);
			this.radioDiplomatic.Name = "radioDiplomatic";
			this.radioDiplomatic.Size = new System.Drawing.Size(83,16);
			this.radioDiplomatic.TabIndex = 23;
			this.radioDiplomatic.TabStop = true;
			this.radioDiplomatic.Text = "Diplomatic";
			this.radioDiplomatic.UseVisualStyleBackColor = true;
			// 
			// radioFirm
			// 
			this.radioFirm.Location = new System.Drawing.Point(227,266);
			this.radioFirm.Name = "radioFirm";
			this.radioFirm.Size = new System.Drawing.Size(55,16);
			this.radioFirm.TabIndex = 24;
			this.radioFirm.TabStop = true;
			this.radioFirm.Text = "Firm";
			this.radioFirm.UseVisualStyleBackColor = true;
			// 
			// radioSkip
			// 
			this.radioSkip.Location = new System.Drawing.Point(281,266);
			this.radioSkip.Name = "radioSkip";
			this.radioSkip.Size = new System.Drawing.Size(60,16);
			this.radioSkip.TabIndex = 25;
			this.radioSkip.TabStop = true;
			this.radioSkip.Text = "Skip";
			this.radioSkip.UseVisualStyleBackColor = true;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(44,301);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(193,16);
			this.label18.TabIndex = 26;
			this.label18.Text = "Trojan Collection Services password";
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(234,298);
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '*';
			this.textPassword.Size = new System.Drawing.Size(68,20);
			this.textPassword.TabIndex = 27;
			this.textPassword.Text = "123456";
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
			// FormTrojanCollect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(491,385);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.radioSkip);
			this.Controls.Add(this.radioFirm);
			this.Controls.Add(this.radioDiplomatic);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.labelPatient);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.labelEmpPhone);
			this.Controls.Add(this.labelEmployer);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.labelPhone);
			this.Controls.Add(this.labelDOB);
			this.Controls.Add(this.labelSSN);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.labelCityStZip);
			this.Controls.Add(this.labelAddress);
			this.Controls.Add(this.labelGuarantor);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butHelp);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Name = "FormTrojanCollect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Send a Collection Transaction To Trojan";
			this.Load += new System.EventHandler(this.FormTrojanCollect_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormTrojanCollect_Load(object sender,EventArgs e) {

		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			FormTrojanCollectSetup FormT=new FormTrojanCollectSetup();
			FormT.ShowDialog();
		}

		private void butOK_Click(object sender, System.EventArgs e) {

			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butHelp_Click(object sender,EventArgs e) {
			FormTrojanHelp FormH=new FormTrojanHelp();
			FormH.ShowDialog();
		}

		


	}
}





















