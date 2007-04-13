using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormPractice : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBankNumber;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.TextBox textST;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.TextBox textAddress2;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textPracticeTitle;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListBox listBillType;
		private System.Windows.Forms.ListBox listProvider;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label labelPlaceService;
		private System.Windows.Forms.ListBox listPlaceService;
		private System.Windows.Forms.TextBox textPhone;
		private System.Windows.Forms.RadioButton radioInsBillingProvTreat;
		private System.Windows.Forms.RadioButton radioInsBillingProvDefault;
		private System.Windows.Forms.RadioButton radioInsBillingProvSpecific;
		private System.Windows.Forms.ComboBox comboInsBillingProv;
		private GroupBox groupSwiss;
		private TextBox textBankAddress;
		private Label label2;
		private TextBox textBankRouting;
		private Label label1;
		private System.Windows.Forms.GroupBox groupBox4;// Required designer variable.

		///<summary></summary>
		public FormPractice(){
			InitializeComponent();
			Lan.F(this);
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

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPractice));
			this.listBillType = new System.Windows.Forms.ListBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.listProvider = new System.Windows.Forms.ListBox();
			this.textBankNumber = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textPhone = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textZip = new System.Windows.Forms.TextBox();
			this.textST = new System.Windows.Forms.TextBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.textPracticeTitle = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.labelPlaceService = new System.Windows.Forms.Label();
			this.listPlaceService = new System.Windows.Forms.ListBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.comboInsBillingProv = new System.Windows.Forms.ComboBox();
			this.radioInsBillingProvSpecific = new System.Windows.Forms.RadioButton();
			this.radioInsBillingProvTreat = new System.Windows.Forms.RadioButton();
			this.radioInsBillingProvDefault = new System.Windows.Forms.RadioButton();
			this.groupSwiss = new System.Windows.Forms.GroupBox();
			this.textBankAddress = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBankRouting = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupSwiss.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBillType
			// 
			this.listBillType.Items.AddRange(new object[] {
            ""});
			this.listBillType.Location = new System.Drawing.Point(571,48);
			this.listBillType.Name = "listBillType";
			this.listBillType.Size = new System.Drawing.Size(160,147);
			this.listBillType.TabIndex = 5;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(570,28);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(154,17);
			this.label12.TabIndex = 29;
			this.label12.Text = "Default Billing Type";
			this.label12.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(453,28);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(110,16);
			this.label10.TabIndex = 26;
			this.label10.Text = "Default Provider";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listProvider
			// 
			this.listProvider.Items.AddRange(new object[] {
            ""});
			this.listProvider.Location = new System.Drawing.Point(453,48);
			this.listProvider.Name = "listProvider";
			this.listProvider.Size = new System.Drawing.Size(110,147);
			this.listProvider.TabIndex = 4;
			// 
			// textBankNumber
			// 
			this.textBankNumber.Location = new System.Drawing.Point(144,255);
			this.textBankNumber.Multiline = true;
			this.textBankNumber.Name = "textBankNumber";
			this.textBankNumber.Size = new System.Drawing.Size(269,49);
			this.textBankNumber.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(3,254);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(140,31);
			this.label4.TabIndex = 22;
			this.label4.Text = "Bank Deposit Acct Number and Info";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textPhone);
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.textZip);
			this.groupBox2.Controls.Add(this.textST);
			this.groupBox2.Controls.Add(this.textCity);
			this.groupBox2.Controls.Add(this.textAddress2);
			this.groupBox2.Controls.Add(this.textAddress);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(41,73);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(384,170);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Address";
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(103,138);
			this.textPhone.Name = "textPhone";
			this.textPhone.Size = new System.Drawing.Size(121,20);
			this.textPhone.TabIndex = 20;
			this.textPhone.TextChanged += new System.EventHandler(this.textPhone_TextChanged);
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(6,47);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(97,16);
			this.label16.TabIndex = 19;
			this.label16.Text = "Address 2";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(103,115);
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(121,20);
			this.textZip.TabIndex = 4;
			// 
			// textST
			// 
			this.textST.Location = new System.Drawing.Point(103,91);
			this.textST.Name = "textST";
			this.textST.Size = new System.Drawing.Size(127,20);
			this.textST.TabIndex = 3;
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(103,67);
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(215,20);
			this.textCity.TabIndex = 2;
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(103,43);
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.Size = new System.Drawing.Size(269,20);
			this.textAddress2.TabIndex = 1;
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(103,19);
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(269,20);
			this.textAddress.TabIndex = 0;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(3,23);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(98,14);
			this.label5.TabIndex = 3;
			this.label5.Text = "Address";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(3,71);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(98,15);
			this.label6.TabIndex = 4;
			this.label6.Text = "City";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(4,95);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(97,18);
			this.label7.TabIndex = 5;
			this.label7.Text = "ST";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(5,117);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(97,17);
			this.label8.TabIndex = 6;
			this.label8.Text = "Zip";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(6,139);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(97,17);
			this.label9.TabIndex = 7;
			this.label9.Text = "Phone";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPracticeTitle
			// 
			this.textPracticeTitle.Location = new System.Drawing.Point(144,29);
			this.textPracticeTitle.Name = "textPracticeTitle";
			this.textPracticeTitle.Size = new System.Drawing.Size(269,20);
			this.textPracticeTitle.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(50,24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96,28);
			this.label3.TabIndex = 19;
			this.label3.Text = "Dentist Name or Practice Title";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(810,395);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 7;
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(810,433);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 8;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// labelPlaceService
			// 
			this.labelPlaceService.Location = new System.Drawing.Point(735,8);
			this.labelPlaceService.Name = "labelPlaceService";
			this.labelPlaceService.Size = new System.Drawing.Size(156,37);
			this.labelPlaceService.TabIndex = 44;
			this.labelPlaceService.Text = "Default Proc Place Service";
			this.labelPlaceService.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listPlaceService
			// 
			this.listPlaceService.Location = new System.Drawing.Point(737,48);
			this.listPlaceService.Name = "listPlaceService";
			this.listPlaceService.Size = new System.Drawing.Size(145,147);
			this.listPlaceService.TabIndex = 45;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.comboInsBillingProv);
			this.groupBox4.Controls.Add(this.radioInsBillingProvSpecific);
			this.groupBox4.Controls.Add(this.radioInsBillingProvTreat);
			this.groupBox4.Controls.Add(this.radioInsBillingProvDefault);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(453,213);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(235,104);
			this.groupBox4.TabIndex = 50;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Default Insurance Billing Dentist";
			// 
			// comboInsBillingProv
			// 
			this.comboInsBillingProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboInsBillingProv.Location = new System.Drawing.Point(35,73);
			this.comboInsBillingProv.Name = "comboInsBillingProv";
			this.comboInsBillingProv.Size = new System.Drawing.Size(121,21);
			this.comboInsBillingProv.TabIndex = 3;
			// 
			// radioInsBillingProvSpecific
			// 
			this.radioInsBillingProvSpecific.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioInsBillingProvSpecific.Location = new System.Drawing.Point(17,53);
			this.radioInsBillingProvSpecific.Name = "radioInsBillingProvSpecific";
			this.radioInsBillingProvSpecific.Size = new System.Drawing.Size(186,19);
			this.radioInsBillingProvSpecific.TabIndex = 2;
			this.radioInsBillingProvSpecific.Text = "Specific Provider:";
			// 
			// radioInsBillingProvTreat
			// 
			this.radioInsBillingProvTreat.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioInsBillingProvTreat.Location = new System.Drawing.Point(17,34);
			this.radioInsBillingProvTreat.Name = "radioInsBillingProvTreat";
			this.radioInsBillingProvTreat.Size = new System.Drawing.Size(186,19);
			this.radioInsBillingProvTreat.TabIndex = 1;
			this.radioInsBillingProvTreat.Text = "Treating Provider";
			// 
			// radioInsBillingProvDefault
			// 
			this.radioInsBillingProvDefault.Checked = true;
			this.radioInsBillingProvDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioInsBillingProvDefault.Location = new System.Drawing.Point(17,16);
			this.radioInsBillingProvDefault.Name = "radioInsBillingProvDefault";
			this.radioInsBillingProvDefault.Size = new System.Drawing.Size(186,19);
			this.radioInsBillingProvDefault.TabIndex = 0;
			this.radioInsBillingProvDefault.TabStop = true;
			this.radioInsBillingProvDefault.Text = "Default Practice Provider";
			// 
			// groupSwiss
			// 
			this.groupSwiss.Controls.Add(this.textBankAddress);
			this.groupSwiss.Controls.Add(this.label2);
			this.groupSwiss.Controls.Add(this.textBankRouting);
			this.groupSwiss.Controls.Add(this.label1);
			this.groupSwiss.Location = new System.Drawing.Point(41,310);
			this.groupSwiss.Name = "groupSwiss";
			this.groupSwiss.Size = new System.Drawing.Size(384,146);
			this.groupSwiss.TabIndex = 51;
			this.groupSwiss.TabStop = false;
			this.groupSwiss.Text = "Switzerland";
			// 
			// textBankAddress
			// 
			this.textBankAddress.AcceptsReturn = true;
			this.textBankAddress.Location = new System.Drawing.Point(103,43);
			this.textBankAddress.Multiline = true;
			this.textBankAddress.Name = "textBankAddress";
			this.textBankAddress.Size = new System.Drawing.Size(269,95);
			this.textBankAddress.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(4,46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98,40);
			this.label2.TabIndex = 7;
			this.label2.Text = "Bank Name and Address";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBankRouting
			// 
			this.textBankRouting.Location = new System.Drawing.Point(103,19);
			this.textBankRouting.Name = "textBankRouting";
			this.textBankRouting.Size = new System.Drawing.Size(269,20);
			this.textBankRouting.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4,22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98,14);
			this.label1.TabIndex = 5;
			this.label1.Text = "Bank Routing";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormPractice
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(899,471);
			this.Controls.Add(this.groupSwiss);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.listPlaceService);
			this.Controls.Add(this.labelPlaceService);
			this.Controls.Add(this.textBankNumber);
			this.Controls.Add(this.textPracticeTitle);
			this.Controls.Add(this.listProvider);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.listBillType);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label3);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPractice";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Practice Info";
			this.Load += new System.EventHandler(this.FormPractice_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupSwiss.ResumeLayout(false);
			this.groupSwiss.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPractice_Load(object sender, System.EventArgs e) {
			textPracticeTitle.Text=PrefB.GetString("PracticeTitle");
			textAddress.Text=PrefB.GetString("PracticeAddress");
			textAddress2.Text=PrefB.GetString("PracticeAddress2");
			textCity.Text=PrefB.GetString("PracticeCity");
			textST.Text=PrefB.GetString("PracticeST");
			textZip.Text=PrefB.GetString("PracticeZip");
			string phone=PrefB.GetString("PracticePhone");
			if(phone.Length==10 
				&& (CultureInfo.CurrentCulture.Name=="en-US" || CultureInfo.CurrentCulture.Name.Substring(3)=="CA"))
			{
				textPhone.Text="("+phone.Substring(0,3)+")"+phone.Substring(3,3)+"-"+phone.Substring(6);
			}
			else{
				textPhone.Text=phone;
			}
			textBankNumber.Text=PrefB.GetString("PracticeBankNumber");
			if(CultureInfo.CurrentCulture.Name.EndsWith("CH")) {//CH is for switzerland. eg de-CH
				textBankRouting.Text=PrefB.GetString("BankRouting");
				textBankAddress.Text=PrefB.GetString("BankAddress");
			}
			else {
				groupSwiss.Visible=false;
			}
			listProvider.Items.Clear();
			for(int i=0;i<Providers.List.Length;i++){
				listProvider.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==PrefB.GetInt("PracticeDefaultProv")){
					listProvider.SelectedIndex=i;
				}
			}
			listBillType.Items.Clear();
			for(int i=0;i<DefB.Short[(int)DefCat.BillingTypes].Length;i++){
				listBillType.Items.Add(DefB.Short[(int)DefCat.BillingTypes][i].ItemName);
				if(DefB.Short[(int)DefCat.BillingTypes][i].DefNum==PrefB.GetInt("PracticeDefaultBillType"))
					listBillType.SelectedIndex=i;
			}
			if(PrefB.GetBool("EasyHidePublicHealth")){
				labelPlaceService.Visible=false;
				listPlaceService.Visible=false;
			}
			listPlaceService.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(PlaceOfService)).Length;i++){
				listPlaceService.Items.Add
					(Lan.g("enumPlaceOfService",Enum.GetNames(typeof(PlaceOfService))[i]));
			}
			listPlaceService.SelectedIndex=PrefB.GetInt("DefaultProcedurePlaceService");
			for(int i=0;i<Providers.List.Length;i++){
				comboInsBillingProv.Items.Add(Providers.List[i].Abbr);
			}
			if(PrefB.GetInt("InsBillingProv")==0){
				radioInsBillingProvDefault.Checked=true;//default=0
			}
			else if(PrefB.GetInt("InsBillingProv")==-1){
				radioInsBillingProvTreat.Checked=true;//treat=-1
			}
			else{
				radioInsBillingProvSpecific.Checked=true;//specific=any number >0. Foreign key to ProvNum
				comboInsBillingProv.SelectedIndex=Providers.GetIndex(PrefB.GetInt("InsBillingProv"));
			}
		}

		private void textPhone_TextChanged(object sender, System.EventArgs e) {
			int cursor=textPhone.SelectionStart;
			int length=textPhone.Text.Length;
			textPhone.Text=TelephoneNumbers.AutoFormat(textPhone.Text);
			if(textPhone.Text.Length>length)
				cursor++;
			textPhone.SelectionStart=cursor;		
		}

		//private void butTreatProv_Click(object sender, System.EventArgs e) {
		//	listBillProv.SelectedIndex=-1;
		//}

		private void butOK_Click(object sender, System.EventArgs e) {
			string phone=textPhone.Text;
			if(Application.CurrentCulture.Name=="en-US"
				|| CultureInfo.CurrentCulture.Name.Substring(3)=="CA")
			{
				phone=phone.Replace("(","");
				phone=phone.Replace(")","");
				phone=phone.Replace(" ","");
				phone=phone.Replace("-","");
				if(phone.Length!=0 && phone.Length!=10){
					MessageBox.Show(Lan.g(this,"Invalid phone"));
					return;
				}
			}
			if(radioInsBillingProvSpecific.Checked && comboInsBillingProv.SelectedIndex==-1){
				MsgBox.Show(this,"You must select a provider.");
				return;
			}
			bool changed=false;
			if( Prefs.UpdateString("PracticeTitle",textPracticeTitle.Text)
				| Prefs.UpdateString("PracticeAddress",textAddress.Text)
				| Prefs.UpdateString("PracticeAddress2",textAddress2.Text)
				| Prefs.UpdateString("PracticeCity",textCity.Text)
				| Prefs.UpdateString("PracticeST",textST.Text)
				| Prefs.UpdateString("PracticeZip",textZip.Text)
				| Prefs.UpdateString("PracticePhone",phone)
				| Prefs.UpdateString("PracticeBankNumber",textBankNumber.Text))
			{
				changed=true;
			}
			if(CultureInfo.CurrentCulture.Name.EndsWith("CH")) {//CH is for switzerland. eg de-CH
				if( Prefs.UpdateString("BankRouting",textBankRouting.Text)
					| Prefs.UpdateString("BankAddress",textBankAddress.Text))
				{
					changed=true;
				}
			}
			if(listProvider.SelectedIndex==-1//practice really needs a default prov
				&& Providers.List.Length > 0)
			{
				listProvider.SelectedIndex=0;
			}
			if(listProvider.SelectedIndex!=-1){
				if(Prefs.UpdateInt("PracticeDefaultProv",Providers.List[listProvider.SelectedIndex].ProvNum)){
					changed=true;
				}
			}
			if(listBillType.SelectedIndex!=-1){
				if(Prefs.UpdateInt("PracticeDefaultBillType"
					,DefB.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndex].DefNum))
				{
					changed=true;
				}
			}
			if(Prefs.UpdateInt("DefaultProcedurePlaceService",listPlaceService.SelectedIndex)){
				changed=true;
			}
			if(radioInsBillingProvDefault.Checked){//default=0
				if(Prefs.UpdateInt("InsBillingProv",0)){
					changed=true;
				}
			}
			else if(radioInsBillingProvTreat.Checked){//treat=-1
				if(Prefs.UpdateInt("InsBillingProv",-1)){
					changed=true;
				}
			}
			else{
				if(Prefs.UpdateInt("InsBillingProv",Providers.List[comboInsBillingProv.SelectedIndex].ProvNum)){
					changed=true;
				}
			}
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

	}
}
