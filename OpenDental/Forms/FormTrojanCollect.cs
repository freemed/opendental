using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

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
		public int PatNum;
		private Patient patCur;
		private Patient guarCur;
		private Employer empCur;

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
			this.butHelp = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(44,15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(238,16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Financially Responsible Person:";
			// 
			// labelGuarantor
			// 
			this.labelGuarantor.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelGuarantor.Location = new System.Drawing.Point(50,37);
			this.labelGuarantor.Name = "labelGuarantor";
			this.labelGuarantor.Size = new System.Drawing.Size(226,16);
			this.labelGuarantor.TabIndex = 4;
			this.labelGuarantor.Text = "Joe Smith";
			// 
			// labelAddress
			// 
			this.labelAddress.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelAddress.Location = new System.Drawing.Point(50,55);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(226,16);
			this.labelAddress.TabIndex = 5;
			this.labelAddress.Text = "123 E St.";
			// 
			// labelCityStZip
			// 
			this.labelCityStZip.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelCityStZip.Location = new System.Drawing.Point(50,73);
			this.labelCityStZip.Name = "labelCityStZip";
			this.labelCityStZip.Size = new System.Drawing.Size(226,16);
			this.labelCityStZip.TabIndex = 6;
			this.labelCityStZip.Text = "Los Angeles, CA 20212";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(282,37);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45,16);
			this.label4.TabIndex = 7;
			this.label4.Text = "SS#:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(282,55);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(45,16);
			this.label5.TabIndex = 8;
			this.label5.Text = "DOB:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(282,73);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(45,16);
			this.label6.TabIndex = 9;
			this.label6.Text = "Phone:";
			// 
			// labelSSN
			// 
			this.labelSSN.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelSSN.Location = new System.Drawing.Point(324,37);
			this.labelSSN.Name = "labelSSN";
			this.labelSSN.Size = new System.Drawing.Size(144,16);
			this.labelSSN.TabIndex = 10;
			this.labelSSN.Text = "123-12-1234";
			// 
			// labelDOB
			// 
			this.labelDOB.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelDOB.Location = new System.Drawing.Point(324,55);
			this.labelDOB.Name = "labelDOB";
			this.labelDOB.Size = new System.Drawing.Size(155,16);
			this.labelDOB.TabIndex = 11;
			this.labelDOB.Text = "01/10/1980";
			// 
			// labelPhone
			// 
			this.labelPhone.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelPhone.Location = new System.Drawing.Point(324,73);
			this.labelPhone.Name = "labelPhone";
			this.labelPhone.Size = new System.Drawing.Size(155,16);
			this.labelPhone.TabIndex = 12;
			this.labelPhone.Text = "(310)555-1212";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(44,103);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(62,16);
			this.label10.TabIndex = 13;
			this.label10.Text = "Employer:";
			// 
			// labelEmpPhone
			// 
			this.labelEmpPhone.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelEmpPhone.Location = new System.Drawing.Point(101,121);
			this.labelEmpPhone.Name = "labelEmpPhone";
			this.labelEmpPhone.Size = new System.Drawing.Size(249,16);
			this.labelEmpPhone.TabIndex = 15;
			this.labelEmpPhone.Text = "(310)665-5544";
			// 
			// labelEmployer
			// 
			this.labelEmployer.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelEmployer.Location = new System.Drawing.Point(101,103);
			this.labelEmployer.Name = "labelEmployer";
			this.labelEmployer.Size = new System.Drawing.Size(249,16);
			this.labelEmployer.TabIndex = 14;
			this.labelEmployer.Text = "Ace, Inc.";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(44,150);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(62,16);
			this.label13.TabIndex = 16;
			this.label13.Text = "Patient:";
			// 
			// labelPatient
			// 
			this.labelPatient.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelPatient.Location = new System.Drawing.Point(59,168);
			this.labelPatient.Name = "labelPatient";
			this.labelPatient.Size = new System.Drawing.Size(243,16);
			this.labelPatient.TabIndex = 17;
			this.labelPatient.Text = "Mary Smith";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(44,223);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(103,16);
			this.label15.TabIndex = 19;
			this.label15.Text = "Amount of debt";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(44,201);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(103,16);
			this.label16.TabIndex = 18;
			this.label16.Text = "Delinquency Date";
			// 
			// textDate
			// 
			this.textDate.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textDate.Location = new System.Drawing.Point(144,198);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(92,20);
			this.textDate.TabIndex = 20;
			this.textDate.Text = "01/25/2007";
			// 
			// textAmount
			// 
			this.textAmount.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textAmount.Location = new System.Drawing.Point(144,220);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(70,20);
			this.textAmount.TabIndex = 21;
			this.textAmount.Text = "123.45";
			this.textAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(44,258);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(103,16);
			this.label17.TabIndex = 22;
			this.label17.Text = "Transaction type:";
			// 
			// radioDiplomatic
			// 
			this.radioDiplomatic.Checked = true;
			this.radioDiplomatic.Location = new System.Drawing.Point(144,257);
			this.radioDiplomatic.Name = "radioDiplomatic";
			this.radioDiplomatic.Size = new System.Drawing.Size(83,16);
			this.radioDiplomatic.TabIndex = 23;
			this.radioDiplomatic.TabStop = true;
			this.radioDiplomatic.Text = "Diplomatic";
			this.radioDiplomatic.UseVisualStyleBackColor = true;
			// 
			// radioFirm
			// 
			this.radioFirm.Location = new System.Drawing.Point(227,257);
			this.radioFirm.Name = "radioFirm";
			this.radioFirm.Size = new System.Drawing.Size(55,16);
			this.radioFirm.TabIndex = 24;
			this.radioFirm.Text = "Firm";
			this.radioFirm.UseVisualStyleBackColor = true;
			// 
			// radioSkip
			// 
			this.radioSkip.Location = new System.Drawing.Point(281,257);
			this.radioSkip.Name = "radioSkip";
			this.radioSkip.Size = new System.Drawing.Size(60,16);
			this.radioSkip.TabIndex = 25;
			this.radioSkip.Text = "Skip";
			this.radioSkip.UseVisualStyleBackColor = true;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(44,292);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(193,16);
			this.label18.TabIndex = 26;
			this.label18.Text = "Trojan Collection Services password";
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(234,289);
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
			// butHelp
			// 
			this.butHelp.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butHelp.Autosize = true;
			this.butHelp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHelp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHelp.CornerRadius = 4F;
			this.butHelp.Location = new System.Drawing.Point(391,324);
			this.butHelp.Name = "butHelp";
			this.butHelp.Size = new System.Drawing.Size(69,26);
			this.butHelp.TabIndex = 2;
			this.butHelp.Text = "Help";
			this.butHelp.Click += new System.EventHandler(this.butHelp_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(47,324);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(182,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "OK Send Transaction to Trojan";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(275,324);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormTrojanCollect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(491,370);
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
			patCur=Patients.GetPat(PatNum);
			guarCur=Patients.GetPat(patCur.Guarantor);
			if(guarCur.EmployerNum==0){
				empCur=null;
			}
			else{
				empCur=Employers.GetEmployer(guarCur.EmployerNum);
			}
			if(guarCur.LName.Length==0){
				MessageBox.Show("Missing guarantor last name.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(guarCur.FName.Length==0) {
				MessageBox.Show("Missing guarantor first name.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!Regex.IsMatch(guarCur.SSN,@"^\d{9}$")) {
				MessageBox.Show("Guarantor SSN must be exactly 9 digits.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(guarCur.Address.Length==0) {
				MessageBox.Show("Missing guarantor address.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(guarCur.City.Length==0) {
				MessageBox.Show("Missing guarantor city.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(guarCur.State.Length!=2) {
				MessageBox.Show("Guarantor state must be 2 characters.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(guarCur.Zip.Length<5) {
				MessageBox.Show("Invalid guarantor zip.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			labelGuarantor.Text=guarCur.GetNameFL();
			labelAddress.Text=guarCur.Address;
			if(guarCur.Address2!=""){
				labelAddress.Text+=", "+guarCur.Address2;
			}
			labelCityStZip.Text=guarCur.City+", "+guarCur.State+" "+guarCur.Zip;
			labelSSN.Text=guarCur.SSN.Substring(0,3)+"-"+guarCur.SSN.Substring(3,2)+"-"+guarCur.SSN.Substring(5,4);
			if(guarCur.Birthdate.Year<1880){
				labelDOB.Text="";
			}
			else{
				labelDOB.Text=guarCur.Birthdate.ToString("MM/dd/yyyy");
			}
			labelPhone.Text=Clip(guarCur.HmPhone,13);
			if(empCur==null){
				labelEmployer.Text="";
				labelEmpPhone.Text="";
			}
			else{
				labelEmployer.Text=empCur.EmpName;
				labelEmpPhone.Text=empCur.Phone;
			}
			labelPatient.Text=patCur.GetNameFL();
			string command=@"SELECT MAX(ProcDate) FROM procedurelog,patient
				WHERE patient.PatNum=procedurelog.PatNum
				AND patient.Guarantor="+POut.PInt(guarCur.PatNum);
			DataTable table=General.GetTable(command);
			DateTime lastProcDate;
			if(table.Rows.Count==0){
				lastProcDate=DateTime.MinValue;//this should never happen
			}
			else{
				lastProcDate=PIn.PDate(table.Rows[0][0].ToString());
			}
			command=@"SELECT MAX(DatePay) FROM paysplit,patient
				WHERE patient.PatNum=paysplit.PatNum
				AND patient.Guarantor="+POut.PInt(guarCur.PatNum);
			table=General.GetTable(command);
			DateTime lastPayDate;
			if(table.Rows.Count==0) {
				lastPayDate=DateTime.MinValue;
			}
			else {
				lastPayDate=PIn.PDate(table.Rows[0][0].ToString());
			}
			if(lastPayDate>lastProcDate){
				textDate.Text=lastPayDate.ToString("MM/dd/yyyy");
			}
			else{
				textDate.Text=lastProcDate.ToString("MM/dd/yyyy");
			}
			textAmount.Text=guarCur.BalTotal.ToString("F2");
			textPassword.Text=PrefB.GetString("TrojanExpressCollectPassword");
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			FormTrojanCollectSetup FormT=new FormTrojanCollectSetup();
			FormT.ShowDialog();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			double amtDebt=PIn.PDouble(textAmount.Text);
			if(amtDebt==0){
				MessageBox.Show("Please fill in an amount.");
				return;
			}
			if(amtDebt<25) {
				MessageBox.Show("Amount of debt must be at least $25.00.");
				return;
			}
			if(amtDebt>9999999.00) {//limit 10 char
				MessageBox.Show("Amount of debt is unreasonably large.");
				return;
			}
			DateTime dateDelinquency=PIn.PDate(textDate.Text);
			if(dateDelinquency.Year<1950) {
				MessageBox.Show("Date is not valid.");
				return;
			}
			if(dateDelinquency>DateTime.Today) {
				MessageBox.Show("Date cannot be a future date.");
				return;
			}
			if(!Regex.IsMatch(textPassword.Text,@"^[A-Z]{2}\d{4}$")) {
				MessageBox.Show("Password is not in correct format. Must be like this: AB1234");
				return;
			}
			if(textPassword.Text!=PrefB.GetString("TrojanExpressCollectPassword")){//user changed password
				Prefs.UpdateString("TrojanExpressCollectPassword",textPassword.Text);
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			Cursor=Cursors.WaitCursor;
			string folderPath=PrefB.GetString("TrojanExpressCollectPath");
			if(folderPath==""){
				Cursor=Cursors.Default;
				MessageBox.Show("Export folder has not been setup yet.  Please go to Setup at the top of this window.");
				return;
			}
			if(!folderPath.EndsWith("\\")){
				folderPath+="\\";
			}
			if(!File.Exists(folderPath+"TROBEN.HB")){
				Cursor=Cursors.Default;
				MessageBox.Show("The Trojan Communicator is not installed or is not configured for the folder: "
					+folderPath+".  Please contact Trojan Software Support at 800-451-9723 x1 or x2");
				return;
			}
			File.Delete(folderPath+"TROBEN.HB");
			FileSystemWatcher watcher=new FileSystemWatcher(folderPath,"TROBEN.HB");
			WaitForChangedResult waitResult=watcher.WaitForChanged(WatcherChangeTypes.Created,10000);
			if(waitResult.TimedOut){
				Cursor=Cursors.Default;
				MessageBox.Show("The Trojan Communicator is not running. Please check it.");
				return;
			}
			int billingType=PrefB.GetInt("TrojanExpressCollectBillingType");
			if(billingType==0){
				Cursor=Cursors.Default;
				MessageBox.Show("Billing type has not been setup yet.  Please go to Setup at the top of this window.");
				return;
			}
			StringBuilder str=new StringBuilder();
			if(radioDiplomatic.Checked){
				str.Append("D*");
			}
			else if(radioFirm.Checked) {
				str.Append("F*");
			}
			else if(radioSkip.Checked) {
				str.Append("S*");
			}
			str.Append(Clip(patCur.LName,18)+"*");
			str.Append(Clip(patCur.FName,18)+"*");
			str.Append(Clip(patCur.MiddleI,1)+"*");
			str.Append(Clip(guarCur.LName,18)+"*");//validated
			str.Append(Clip(guarCur.FName,18)+"*");//validated
			str.Append(Clip(guarCur.MiddleI,1)+"*");
			str.Append(guarCur.SSN.Substring(0,3)+"-"+guarCur.SSN.Substring(3,2)+"-"+guarCur.SSN.Substring(5,4)+"*");//validated
			if(guarCur.Birthdate.Year<1880){
				str.Append("*");
			}
			else{
				str.Append(guarCur.Birthdate.ToString("MM/dd/yyyy")+"*");
			}
			str.Append(Clip(guarCur.HmPhone,13)+"*");
			if(empCur==null){
				str.Append("**");
			}
			else{
				str.Append(Clip(empCur.EmpName,35)+"*");
				str.Append(Clip(empCur.Phone,13)+"*");
			}
			string address=guarCur.Address;//validated
			if(guarCur.Address2!=""){
				address+=", "+guarCur.Address2;
			}
			str.Append(Clip(address,30)+"*");
			str.Append(Clip(guarCur.City,20)+"*");//validated
			str.Append(Clip(guarCur.State,2)+"*");//validated
			str.Append(Clip(guarCur.Zip,5)+"*");//validated
			str.Append(amtDebt.ToString("F2")+"*");//validated
			str.Append(dateDelinquency.ToString("MM/dd/yyyy")+"*");//validated
			str.Append(textPassword.Text+"*");//validated
			str.Append(Clip(Security.CurUser.UserName,25)+"\r\n");//There is always a logged in user
			string command="SELECT ValueString FROM preference WHERE PrefName='TrojanExpressCollectPreviousFileNumber'";
			DataTable table=General.GetTable(command);
			int previousNum=PIn.PInt(table.Rows[0][0].ToString());
			int thisNum=previousNum+1;
			command="UPDATE preference SET ValueString='"+POut.PInt(thisNum)+"' WHERE PrefName='TrojanExpressCollectPreviousFileNumber'"
				+" AND ValueString='"+POut.PInt(previousNum)+"'";
			int result=General.NonQ(command);
			while(result!=1){//someone else sent one at the same time
				previousNum++;
				thisNum++;
				command="UPDATE preference SET ValueString='"+POut.PInt(thisNum)+"' WHERE PrefName='TrojanExpressCollectPreviousFileNumber'"
					+" AND ValueString='"+POut.PInt(previousNum)+"'";
				result=General.NonQ(command);
			}
			string outputFile="CT"+thisNum.ToString().PadLeft(6,'0')+".TRO";
			File.AppendAllText(folderPath+outputFile,str.ToString());
			watcher=new FileSystemWatcher(folderPath,outputFile);
			waitResult=watcher.WaitForChanged(WatcherChangeTypes.Deleted,10000);
			if(waitResult.TimedOut) {
				Cursor=Cursors.Default;
				MessageBox.Show("Warning!! Request was not sent to Trojan within the 10 second limit.");
				return;
			}
			command="UPDATE patient SET BillingType="+POut.PInt(billingType)+" WHERE Guarantor="+POut.PInt(patCur.Guarantor);
			General.NonQ(command);
			Cursor=Cursors.Default;
			DialogResult=DialogResult.OK;
		}

		///<summary>Clips the input string to the specified length.  Also strips out any *, tabs, newlines, etc.</summary>
		private string Clip(string inputstr,int length){
			string retval=inputstr.Replace("*","");
			retval=retval.Replace("\r","");
			retval=retval.Replace("\n","");
			retval=retval.Replace("\t","");
			if(retval.Length>length){
				return retval.Substring(0,length);
			}
			return retval;
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





















