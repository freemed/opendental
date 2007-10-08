/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormPatientEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.TextBox textFName;
		private System.Windows.Forms.TextBox textMiddleI;
		private System.Windows.Forms.TextBox textPreferred;
		private System.Windows.Forms.TextBox textSSN;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.TextBox textAddress2;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.TextBox textHmPhone;
		private System.Windows.Forms.TextBox textWkPhone;
		private System.Windows.Forms.TextBox textWirelessPhone;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox textAge;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TextBox textSalutation;
		private System.Windows.Forms.TextBox textEmail;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.TextBox textCreditType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textSchool;
		private System.Windows.Forms.RadioButton radioStudentN;
		private System.Windows.Forms.RadioButton radioStudentP;
		private System.Windows.Forms.RadioButton radioStudentF;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.TextBox textChartNumber;
		//private OpenDental.ValidDate textBirthdate2;
		private OpenDental.ValidDate textBirthdate;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkSame;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ComboBox comboZip;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.GroupBox groupNotes;
		private System.Windows.Forms.CheckBox checkNotesSame;
		private System.Windows.Forms.TextBox textPatNum;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label32;
		private OpenDental.UI.Button butAuto;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.TextBox textMedicaidID;
		private System.Windows.Forms.ListBox listStatus;
		private System.Windows.Forms.ListBox listGender;
		private System.Windows.Forms.ListBox listPosition;
		private System.Windows.Forms.TextBox textEmployer;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label labelSSN;
		private System.Windows.Forms.Label labelZip;
		private System.Windows.Forms.Label labelST;
		private OpenDental.UI.Button butEditZip;
		private System.Windows.Forms.Label labelCity;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.GroupBox groupPH;
		private System.Windows.Forms.TextBox textCounty;
		private System.Windows.Forms.ComboBox comboRace;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.TextBox textGradeSchool;
		private System.Windows.Forms.ComboBox comboGradeLevel;
		private System.Windows.Forms.ComboBox comboUrgency;
		///<summary>Set true if this is a new patient. Patient must have been already inserted. If users clicks cancel, this patient will be deleted.</summary>
		public bool IsNew;
		private System.Windows.Forms.ListBox listSchools;//displays dropdown for GradeSchools
		private string schoolOriginal;
		private bool mouseIsInListSchools;
		private System.Windows.Forms.ListBox listCounties;//displays dropdown for GradeSchools
		private string countyOriginal;
		private OpenDental.ValidDate textDateFirstVisit;
		private System.Windows.Forms.Label label36;
		private bool mouseIsInListCounties;
		private OpenDental.ODtextBox textAddrNotes;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.ListBox listEmps;//displayed from within code, not designer
		private string empOriginal;//used in the emp dropdown logic
		private ArrayList similarEmps;
		private bool mouseIsInListEmps;
		///<summary>This is the object that is altered in this form.</summary>
		private Patient PatCur;
		private RefAttach[] RefList;
		private Family FamCur;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		private TextBox textTrophyFolder;
		private Label labelTrophyFolder;
		private TextBox textWard;
		private Label labelWard;
		private Label label28;
		private ComboBox comboLanguage;
		private Patient PatOld;
		private ComboBox comboBillType;
		private Label label40;
		private Label label39;
		private Label label38;
		private ComboBox comboFeeSched;
		private ComboBox comboSecProv;
		private ComboBox comboPriProv;
		private ComboBox comboContact;
		private Label label23;
		private ComboBox comboConfirm;
		private Label label24;
		private ComboBox comboRecall;
		private Label label25;
		private ValidDate textAdmitDate;
		private Label labelAdmitDate;
		private TextBox textTitle;
		private Label label26;
		///<summary>Will include the languages setup in the settings, and also the language of this patient if that language is not on the selection list.</summary>
		private List<CultureInfo> languageList;

		///<summary></summary>
		public FormPatientEdit(Patient patCur,Family famCur){
			InitializeComponent();// Required for Windows Form Designer support
			PatCur=patCur;
			FamCur=famCur;
			PatOld=patCur.Copy();
			listEmps=new ListBox();
			listEmps.Location=new Point(textEmployer.Left,textEmployer.Bottom);
			listEmps.Size=new Size(textEmployer.Width,100);
			listEmps.Visible=false;
			listEmps.Click += new System.EventHandler(listEmps_Click);
			listEmps.DoubleClick += new System.EventHandler(listEmps_DoubleClick);
			listEmps.MouseEnter += new System.EventHandler(listEmps_MouseEnter);
			listEmps.MouseLeave += new System.EventHandler(listEmps_MouseLeave);
			Controls.Add(listEmps);
			listEmps.BringToFront();
			listSchools=new ListBox();
			listSchools.Location=new Point(groupPH.Left+textGradeSchool.Left
				,groupPH.Top+textGradeSchool.Bottom);
			listSchools.Size=new Size(textGradeSchool.Width,100);
			listSchools.Visible=false;
			listSchools.Click += new System.EventHandler(listSchools_Click);
			//listSchools.DoubleClick += new System.EventHandler(listCars_DoubleClick);
			listSchools.MouseEnter += new System.EventHandler(listSchools_MouseEnter);
			listSchools.MouseLeave += new System.EventHandler(listSchools_MouseLeave);
			Controls.Add(listSchools);
			listSchools.BringToFront();
			listCounties=new ListBox();
			listCounties.Location=new Point(groupPH.Left+textCounty.Left
				,groupPH.Top+textCounty.Bottom);
			listCounties.Size=new Size(textCounty.Width,100);
			listCounties.Visible=false;
			listCounties.Click += new System.EventHandler(listCounties_Click);
			//listCounties.DoubleClick += new System.EventHandler(listCars_DoubleClick);
			listCounties.MouseEnter += new System.EventHandler(listCounties_MouseEnter);
			listCounties.MouseLeave += new System.EventHandler(listCounties_MouseLeave);
			Controls.Add(listCounties);
			listCounties.BringToFront();
			Lan.F(this);
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				labelSSN.Text="SIN";
				labelZip.Text="Postal Code";
				labelST.Text="Province";
				butEditZip.Text="Edit Postal Code";
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="GB"){//en-GB
				//labelSSN.Text="?";
				labelZip.Text="Postcode";
				labelST.Text="";//no such thing as state in GB
				butEditZip.Text="Edit Postcode";
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPatientEdit));
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.labelSSN = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.labelCity = new System.Windows.Forms.Label();
			this.labelST = new System.Windows.Forms.Label();
			this.labelZip = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.textLName = new System.Windows.Forms.TextBox();
			this.textFName = new System.Windows.Forms.TextBox();
			this.textMiddleI = new System.Windows.Forms.TextBox();
			this.textPreferred = new System.Windows.Forms.TextBox();
			this.textSSN = new System.Windows.Forms.TextBox();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.textState = new System.Windows.Forms.TextBox();
			this.textHmPhone = new System.Windows.Forms.TextBox();
			this.textWkPhone = new System.Windows.Forms.TextBox();
			this.textWirelessPhone = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.label20 = new System.Windows.Forms.Label();
			this.textAge = new System.Windows.Forms.TextBox();
			this.textSalutation = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.textEmail = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.textCreditType = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textSchool = new System.Windows.Forms.TextBox();
			this.radioStudentN = new System.Windows.Forms.RadioButton();
			this.radioStudentP = new System.Windows.Forms.RadioButton();
			this.radioStudentF = new System.Windows.Forms.RadioButton();
			this.label30 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.textChartNumber = new System.Windows.Forms.TextBox();
			this.textBirthdate = new OpenDental.ValidDate();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label40 = new System.Windows.Forms.Label();
			this.label39 = new System.Windows.Forms.Label();
			this.label38 = new System.Windows.Forms.Label();
			this.comboFeeSched = new System.Windows.Forms.ComboBox();
			this.comboSecProv = new System.Windows.Forms.ComboBox();
			this.comboPriProv = new System.Windows.Forms.ComboBox();
			this.comboBillType = new System.Windows.Forms.ComboBox();
			this.butEditZip = new OpenDental.UI.Button();
			this.textZip = new System.Windows.Forms.TextBox();
			this.comboZip = new System.Windows.Forms.ComboBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.checkSame = new System.Windows.Forms.CheckBox();
			this.groupNotes = new System.Windows.Forms.GroupBox();
			this.textAddrNotes = new OpenDental.ODtextBox();
			this.checkNotesSame = new System.Windows.Forms.CheckBox();
			this.textPatNum = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.butAuto = new OpenDental.UI.Button();
			this.textMedicaidID = new System.Windows.Forms.TextBox();
			this.label31 = new System.Windows.Forms.Label();
			this.listStatus = new System.Windows.Forms.ListBox();
			this.listGender = new System.Windows.Forms.ListBox();
			this.listPosition = new System.Windows.Forms.ListBox();
			this.textEmployer = new System.Windows.Forms.TextBox();
			this.label33 = new System.Windows.Forms.Label();
			this.groupPH = new System.Windows.Forms.GroupBox();
			this.comboUrgency = new System.Windows.Forms.ComboBox();
			this.comboGradeLevel = new System.Windows.Forms.ComboBox();
			this.textGradeSchool = new System.Windows.Forms.TextBox();
			this.label35 = new System.Windows.Forms.Label();
			this.comboRace = new System.Windows.Forms.ComboBox();
			this.textCounty = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.textDateFirstVisit = new OpenDental.ValidDate();
			this.label36 = new System.Windows.Forms.Label();
			this.label37 = new System.Windows.Forms.Label();
			this.labelClinic = new System.Windows.Forms.Label();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.textTrophyFolder = new System.Windows.Forms.TextBox();
			this.labelTrophyFolder = new System.Windows.Forms.Label();
			this.textWard = new System.Windows.Forms.TextBox();
			this.labelWard = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.comboLanguage = new System.Windows.Forms.ComboBox();
			this.comboContact = new System.Windows.Forms.ComboBox();
			this.label23 = new System.Windows.Forms.Label();
			this.comboConfirm = new System.Windows.Forms.ComboBox();
			this.label24 = new System.Windows.Forms.Label();
			this.comboRecall = new System.Windows.Forms.ComboBox();
			this.label25 = new System.Windows.Forms.Label();
			this.textAdmitDate = new OpenDental.ValidDate();
			this.labelAdmitDate = new System.Windows.Forms.Label();
			this.textTitle = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupNotes.SuspendLayout();
			this.groupPH.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8,31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(125,16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Last Name";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6,53);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(127,16);
			this.label3.TabIndex = 2;
			this.label3.Text = "First Name";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9,74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(124,16);
			this.label4.TabIndex = 3;
			this.label4.Text = "Middle Initial";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4,95);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(129,16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Preferred Name";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(62,160);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(114,17);
			this.label6.TabIndex = 5;
			this.label6.Text = "Status";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(180,161);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(106,16);
			this.label7.TabIndex = 6;
			this.label7.Text = "Gender";
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(298,162);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100,16);
			this.label8.TabIndex = 7;
			this.label8.Text = "Position";
			this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(28,257);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(105,14);
			this.label9.TabIndex = 8;
			this.label9.Text = "Birthdate";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelSSN
			// 
			this.labelSSN.Location = new System.Drawing.Point(19,279);
			this.labelSSN.Name = "labelSSN";
			this.labelSSN.Size = new System.Drawing.Size(113,14);
			this.labelSSN.TabIndex = 9;
			this.labelSSN.Text = "SS#";
			this.labelSSN.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(49,58);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(98,14);
			this.label11.TabIndex = 10;
			this.label11.Text = "Address";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(49,79);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(99,14);
			this.label12.TabIndex = 11;
			this.label12.Text = "Address2";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelCity
			// 
			this.labelCity.Location = new System.Drawing.Point(48,99);
			this.labelCity.Name = "labelCity";
			this.labelCity.Size = new System.Drawing.Size(98,14);
			this.labelCity.TabIndex = 12;
			this.labelCity.Text = "City";
			this.labelCity.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelST
			// 
			this.labelST.Location = new System.Drawing.Point(51,119);
			this.labelST.Name = "labelST";
			this.labelST.Size = new System.Drawing.Size(96,14);
			this.labelST.TabIndex = 13;
			this.labelST.Text = "ST";
			this.labelST.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelZip
			// 
			this.labelZip.Location = new System.Drawing.Point(51,140);
			this.labelZip.Name = "labelZip";
			this.labelZip.Size = new System.Drawing.Size(96,14);
			this.labelZip.TabIndex = 14;
			this.labelZip.Text = "Zip";
			this.labelZip.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(51,37);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(98,14);
			this.label16.TabIndex = 15;
			this.label16.Text = "Home Phone";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(16,425);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(118,14);
			this.label17.TabIndex = 16;
			this.label17.Text = "Work Phone";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(10,404);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(124,14);
			this.label18.TabIndex = 17;
			this.label18.Text = "Wireless Phone";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(135,29);
			this.textLName.MaxLength = 100;
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(228,20);
			this.textLName.TabIndex = 0;
			this.textLName.TextChanged += new System.EventHandler(this.textLName_TextChanged);
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(135,50);
			this.textFName.MaxLength = 100;
			this.textFName.Name = "textFName";
			this.textFName.Size = new System.Drawing.Size(228,20);
			this.textFName.TabIndex = 1;
			this.textFName.TextChanged += new System.EventHandler(this.textFName_TextChanged);
			// 
			// textMiddleI
			// 
			this.textMiddleI.Location = new System.Drawing.Point(135,71);
			this.textMiddleI.MaxLength = 100;
			this.textMiddleI.Name = "textMiddleI";
			this.textMiddleI.Size = new System.Drawing.Size(75,20);
			this.textMiddleI.TabIndex = 2;
			this.textMiddleI.TextChanged += new System.EventHandler(this.textMiddleI_TextChanged);
			// 
			// textPreferred
			// 
			this.textPreferred.Location = new System.Drawing.Point(135,92);
			this.textPreferred.MaxLength = 100;
			this.textPreferred.Name = "textPreferred";
			this.textPreferred.Size = new System.Drawing.Size(228,20);
			this.textPreferred.TabIndex = 3;
			this.textPreferred.TextChanged += new System.EventHandler(this.textPreferred_TextChanged);
			// 
			// textSSN
			// 
			this.textSSN.Location = new System.Drawing.Point(135,274);
			this.textSSN.MaxLength = 100;
			this.textSSN.Name = "textSSN";
			this.textSSN.Size = new System.Drawing.Size(82,20);
			this.textSSN.TabIndex = 9;
			this.textSSN.Validating += new System.ComponentModel.CancelEventHandler(this.textSSN_Validating);
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(149,55);
			this.textAddress.MaxLength = 100;
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(254,20);
			this.textAddress.TabIndex = 13;
			this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(149,75);
			this.textAddress2.MaxLength = 100;
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.Size = new System.Drawing.Size(253,20);
			this.textAddress2.TabIndex = 21;
			this.textAddress2.TextChanged += new System.EventHandler(this.textAddress2_TextChanged);
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(149,95);
			this.textCity.MaxLength = 100;
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(191,20);
			this.textCity.TabIndex = 22;
			this.textCity.TabStop = false;
			this.textCity.TextChanged += new System.EventHandler(this.textCity_TextChanged);
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(149,115);
			this.textState.MaxLength = 100;
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(61,20);
			this.textState.TabIndex = 23;
			this.textState.TabStop = false;
			this.textState.TextChanged += new System.EventHandler(this.textState_TextChanged);
			// 
			// textHmPhone
			// 
			this.textHmPhone.Location = new System.Drawing.Point(149,35);
			this.textHmPhone.MaxLength = 30;
			this.textHmPhone.Name = "textHmPhone";
			this.textHmPhone.Size = new System.Drawing.Size(174,20);
			this.textHmPhone.TabIndex = 12;
			this.textHmPhone.TextChanged += new System.EventHandler(this.textHmPhone_TextChanged);
			// 
			// textWkPhone
			// 
			this.textWkPhone.Location = new System.Drawing.Point(135,421);
			this.textWkPhone.MaxLength = 30;
			this.textWkPhone.Name = "textWkPhone";
			this.textWkPhone.Size = new System.Drawing.Size(174,20);
			this.textWkPhone.TabIndex = 16;
			this.textWkPhone.TextChanged += new System.EventHandler(this.textWkPhone_TextChanged);
			// 
			// textWirelessPhone
			// 
			this.textWirelessPhone.Location = new System.Drawing.Point(135,400);
			this.textWirelessPhone.MaxLength = 30;
			this.textWirelessPhone.Name = "textWirelessPhone";
			this.textWirelessPhone.Size = new System.Drawing.Size(174,20);
			this.textWirelessPhone.TabIndex = 15;
			this.textWirelessPhone.TextChanged += new System.EventHandler(this.textWirelessPhone_TextChanged);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(874,597);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 26;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(874,637);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 27;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(215,257);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(61,16);
			this.label20.TabIndex = 40;
			this.label20.Text = "Age";
			this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAge
			// 
			this.textAge.Location = new System.Drawing.Point(276,253);
			this.textAge.Name = "textAge";
			this.textAge.ReadOnly = true;
			this.textAge.Size = new System.Drawing.Size(68,20);
			this.textAge.TabIndex = 0;
			// 
			// textSalutation
			// 
			this.textSalutation.Location = new System.Drawing.Point(135,134);
			this.textSalutation.MaxLength = 100;
			this.textSalutation.Name = "textSalutation";
			this.textSalutation.Size = new System.Drawing.Size(228,20);
			this.textSalutation.TabIndex = 4;
			this.textSalutation.TextChanged += new System.EventHandler(this.textSalutation_TextChanged);
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(6,138);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(127,16);
			this.label21.TabIndex = 42;
			this.label21.Text = "Salutation (Dear __)";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textEmail
			// 
			this.textEmail.Location = new System.Drawing.Point(135,316);
			this.textEmail.MaxLength = 100;
			this.textEmail.Name = "textEmail";
			this.textEmail.Size = new System.Drawing.Size(226,20);
			this.textEmail.TabIndex = 11;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(29,321);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(104,14);
			this.label22.TabIndex = 44;
			this.label22.Text = "E-mail";
			this.label22.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(50,159);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(97,16);
			this.label27.TabIndex = 55;
			this.label27.Text = "Credit Type";
			this.label27.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCreditType
			// 
			this.textCreditType.Location = new System.Drawing.Point(149,156);
			this.textCreditType.MaxLength = 1;
			this.textCreditType.Name = "textCreditType";
			this.textCreditType.Size = new System.Drawing.Size(18,20);
			this.textCreditType.TabIndex = 15;
			this.textCreditType.TextChanged += new System.EventHandler(this.textCreditType_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(26,178);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(121,16);
			this.label1.TabIndex = 57;
			this.label1.Text = "Billing Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textSchool);
			this.groupBox2.Controls.Add(this.radioStudentN);
			this.groupBox2.Controls.Add(this.radioStudentP);
			this.groupBox2.Controls.Add(this.radioStudentF);
			this.groupBox2.Controls.Add(this.label30);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(440,418);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(382,65);
			this.groupBox2.TabIndex = 24;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Student Status if Dependent Over 19 (for Ins)";
			// 
			// textSchool
			// 
			this.textSchool.Location = new System.Drawing.Point(123,37);
			this.textSchool.MaxLength = 30;
			this.textSchool.Name = "textSchool";
			this.textSchool.Size = new System.Drawing.Size(226,20);
			this.textSchool.TabIndex = 3;
			// 
			// radioStudentN
			// 
			this.radioStudentN.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioStudentN.Location = new System.Drawing.Point(52,18);
			this.radioStudentN.Name = "radioStudentN";
			this.radioStudentN.Size = new System.Drawing.Size(108,16);
			this.radioStudentN.TabIndex = 0;
			this.radioStudentN.Text = "Nonstudent";
			this.radioStudentN.Click += new System.EventHandler(this.radioStudentN_Click);
			// 
			// radioStudentP
			// 
			this.radioStudentP.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioStudentP.Location = new System.Drawing.Point(262,18);
			this.radioStudentP.Name = "radioStudentP";
			this.radioStudentP.Size = new System.Drawing.Size(104,16);
			this.radioStudentP.TabIndex = 2;
			this.radioStudentP.Text = "Parttime";
			this.radioStudentP.Click += new System.EventHandler(this.radioStudentP_Click);
			// 
			// radioStudentF
			// 
			this.radioStudentF.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioStudentF.Location = new System.Drawing.Point(164,18);
			this.radioStudentF.Name = "radioStudentF";
			this.radioStudentF.Size = new System.Drawing.Size(98,16);
			this.radioStudentF.TabIndex = 1;
			this.radioStudentF.Text = "Fulltime";
			this.radioStudentF.Click += new System.EventHandler(this.radioStudentF_Click);
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(5,41);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(116,16);
			this.label30.TabIndex = 9;
			this.label30.Text = "College Name";
			this.label30.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(18,341);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(116,16);
			this.label29.TabIndex = 62;
			this.label29.Text = "ChartNumber";
			this.label29.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textChartNumber
			// 
			this.textChartNumber.Location = new System.Drawing.Point(135,337);
			this.textChartNumber.MaxLength = 20;
			this.textChartNumber.Name = "textChartNumber";
			this.textChartNumber.Size = new System.Drawing.Size(99,20);
			this.textChartNumber.TabIndex = 12;
			// 
			// textBirthdate
			// 
			this.textBirthdate.Location = new System.Drawing.Point(135,253);
			this.textBirthdate.Name = "textBirthdate";
			this.textBirthdate.Size = new System.Drawing.Size(82,20);
			this.textBirthdate.TabIndex = 8;
			this.textBirthdate.Validated += new System.EventHandler(this.textBirthdate_Validated);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label40);
			this.groupBox1.Controls.Add(this.label39);
			this.groupBox1.Controls.Add(this.label38);
			this.groupBox1.Controls.Add(this.comboFeeSched);
			this.groupBox1.Controls.Add(this.comboSecProv);
			this.groupBox1.Controls.Add(this.comboPriProv);
			this.groupBox1.Controls.Add(this.comboBillType);
			this.groupBox1.Controls.Add(this.textHmPhone);
			this.groupBox1.Controls.Add(this.butEditZip);
			this.groupBox1.Controls.Add(this.textZip);
			this.groupBox1.Controls.Add(this.comboZip);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.checkSame);
			this.groupBox1.Controls.Add(this.textState);
			this.groupBox1.Controls.Add(this.labelST);
			this.groupBox1.Controls.Add(this.textAddress);
			this.groupBox1.Controls.Add(this.label27);
			this.groupBox1.Controls.Add(this.textCreditType);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.labelCity);
			this.groupBox1.Controls.Add(this.textAddress2);
			this.groupBox1.Controls.Add(this.labelZip);
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.textCity);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(440,8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(424,278);
			this.groupBox1.TabIndex = 25;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Address and Phone";
			// 
			// label40
			// 
			this.label40.Location = new System.Drawing.Point(1,242);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(147,18);
			this.label40.TabIndex = 69;
			this.label40.Text = "Fee Schedule (rarely used)";
			this.label40.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label39
			// 
			this.label39.Location = new System.Drawing.Point(26,222);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(121,16);
			this.label39.TabIndex = 68;
			this.label39.Text = "Secondary Provider";
			this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label38
			// 
			this.label38.Location = new System.Drawing.Point(26,199);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(121,16);
			this.label38.TabIndex = 67;
			this.label38.Text = "Primary Provider";
			this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboFeeSched
			// 
			this.comboFeeSched.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFeeSched.FormattingEnabled = true;
			this.comboFeeSched.Location = new System.Drawing.Point(149,239);
			this.comboFeeSched.MaxDropDownItems = 30;
			this.comboFeeSched.Name = "comboFeeSched";
			this.comboFeeSched.Size = new System.Drawing.Size(198,21);
			this.comboFeeSched.TabIndex = 20;
			// 
			// comboSecProv
			// 
			this.comboSecProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSecProv.FormattingEnabled = true;
			this.comboSecProv.Location = new System.Drawing.Point(149,218);
			this.comboSecProv.MaxDropDownItems = 30;
			this.comboSecProv.Name = "comboSecProv";
			this.comboSecProv.Size = new System.Drawing.Size(198,21);
			this.comboSecProv.TabIndex = 19;
			// 
			// comboPriProv
			// 
			this.comboPriProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPriProv.FormattingEnabled = true;
			this.comboPriProv.Location = new System.Drawing.Point(149,197);
			this.comboPriProv.MaxDropDownItems = 30;
			this.comboPriProv.Name = "comboPriProv";
			this.comboPriProv.Size = new System.Drawing.Size(198,21);
			this.comboPriProv.TabIndex = 17;
			// 
			// comboBillType
			// 
			this.comboBillType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBillType.FormattingEnabled = true;
			this.comboBillType.Location = new System.Drawing.Point(149,176);
			this.comboBillType.MaxDropDownItems = 30;
			this.comboBillType.Name = "comboBillType";
			this.comboBillType.Size = new System.Drawing.Size(198,21);
			this.comboBillType.TabIndex = 16;
			// 
			// butEditZip
			// 
			this.butEditZip.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEditZip.Autosize = true;
			this.butEditZip.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditZip.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditZip.CornerRadius = 4F;
			this.butEditZip.Location = new System.Drawing.Point(349,134);
			this.butEditZip.Name = "butEditZip";
			this.butEditZip.Size = new System.Drawing.Size(76,23);
			this.butEditZip.TabIndex = 62;
			this.butEditZip.TabStop = false;
			this.butEditZip.Text = "&Edit Zip";
			this.butEditZip.Click += new System.EventHandler(this.butEditZip_Click);
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(149,135);
			this.textZip.MaxLength = 100;
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(179,20);
			this.textZip.TabIndex = 14;
			this.textZip.Validating += new System.ComponentModel.CancelEventHandler(this.textZip_Validating);
			this.textZip.TextChanged += new System.EventHandler(this.textZip_TextChanged);
			// 
			// comboZip
			// 
			this.comboZip.DropDownWidth = 198;
			this.comboZip.Location = new System.Drawing.Point(149,135);
			this.comboZip.Name = "comboZip";
			this.comboZip.Size = new System.Drawing.Size(198,21);
			this.comboZip.TabIndex = 60;
			this.comboZip.TabStop = false;
			this.comboZip.SelectionChangeCommitted += new System.EventHandler(this.comboZip_SelectionChangeCommitted);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(167,17);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(230,16);
			this.textBox1.TabIndex = 57;
			this.textBox1.TabStop = false;
			this.textBox1.Text = "Same for entire family";
			// 
			// checkSame
			// 
			this.checkSame.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSame.Location = new System.Drawing.Point(149,14);
			this.checkSame.Name = "checkSame";
			this.checkSame.Size = new System.Drawing.Size(18,21);
			this.checkSame.TabIndex = 11;
			this.checkSame.TabStop = false;
			// 
			// groupNotes
			// 
			this.groupNotes.Controls.Add(this.textAddrNotes);
			this.groupNotes.Controls.Add(this.checkNotesSame);
			this.groupNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupNotes.Location = new System.Drawing.Point(440,291);
			this.groupNotes.Name = "groupNotes";
			this.groupNotes.Size = new System.Drawing.Size(424,107);
			this.groupNotes.TabIndex = 26;
			this.groupNotes.TabStop = false;
			this.groupNotes.Text = "Address and Phone Notes";
			// 
			// textAddrNotes
			// 
			this.textAddrNotes.AcceptsReturn = true;
			this.textAddrNotes.Location = new System.Drawing.Point(128,40);
			this.textAddrNotes.Multiline = true;
			this.textAddrNotes.Name = "textAddrNotes";
			this.textAddrNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.PatAddressNote;
			this.textAddrNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textAddrNotes.Size = new System.Drawing.Size(237,59);
			this.textAddrNotes.TabIndex = 0;
			// 
			// checkNotesSame
			// 
			this.checkNotesSame.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNotesSame.Location = new System.Drawing.Point(129,20);
			this.checkNotesSame.Name = "checkNotesSame";
			this.checkNotesSame.Size = new System.Drawing.Size(247,18);
			this.checkNotesSame.TabIndex = 1;
			this.checkNotesSame.Text = "Same for entire family";
			// 
			// textPatNum
			// 
			this.textPatNum.Location = new System.Drawing.Point(135,8);
			this.textPatNum.MaxLength = 100;
			this.textPatNum.Name = "textPatNum";
			this.textPatNum.ReadOnly = true;
			this.textPatNum.Size = new System.Drawing.Size(75,20);
			this.textPatNum.TabIndex = 71;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(9,11);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(125,16);
			this.label19.TabIndex = 72;
			this.label19.Text = "Patient Number";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label32
			// 
			this.label32.Location = new System.Drawing.Point(303,341);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(88,17);
			this.label32.TabIndex = 73;
			this.label32.Text = "(if used)";
			// 
			// butAuto
			// 
			this.butAuto.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAuto.Autosize = true;
			this.butAuto.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAuto.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAuto.CornerRadius = 4F;
			this.butAuto.Location = new System.Drawing.Point(236,336);
			this.butAuto.Name = "butAuto";
			this.butAuto.Size = new System.Drawing.Size(62,22);
			this.butAuto.TabIndex = 74;
			this.butAuto.Text = "Auto";
			this.butAuto.Click += new System.EventHandler(this.butAuto_Click);
			// 
			// textMedicaidID
			// 
			this.textMedicaidID.Location = new System.Drawing.Point(135,295);
			this.textMedicaidID.MaxLength = 20;
			this.textMedicaidID.Name = "textMedicaidID";
			this.textMedicaidID.Size = new System.Drawing.Size(99,20);
			this.textMedicaidID.TabIndex = 10;
			// 
			// label31
			// 
			this.label31.Location = new System.Drawing.Point(26,300);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(106,14);
			this.label31.TabIndex = 75;
			this.label31.Text = "Medicaid ID";
			this.label31.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listStatus
			// 
			this.listStatus.Location = new System.Drawing.Point(62,177);
			this.listStatus.Name = "listStatus";
			this.listStatus.Size = new System.Drawing.Size(105,69);
			this.listStatus.TabIndex = 5;
			// 
			// listGender
			// 
			this.listGender.Location = new System.Drawing.Point(180,177);
			this.listGender.Name = "listGender";
			this.listGender.Size = new System.Drawing.Size(105,43);
			this.listGender.TabIndex = 6;
			// 
			// listPosition
			// 
			this.listPosition.Location = new System.Drawing.Point(298,178);
			this.listPosition.Name = "listPosition";
			this.listPosition.Size = new System.Drawing.Size(105,69);
			this.listPosition.TabIndex = 7;
			// 
			// textEmployer
			// 
			this.textEmployer.Location = new System.Drawing.Point(135,379);
			this.textEmployer.MaxLength = 30;
			this.textEmployer.Name = "textEmployer";
			this.textEmployer.Size = new System.Drawing.Size(226,20);
			this.textEmployer.TabIndex = 14;
			this.textEmployer.Leave += new System.EventHandler(this.textEmployer_Leave);
			this.textEmployer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textEmployer_KeyUp);
			// 
			// label33
			// 
			this.label33.Location = new System.Drawing.Point(10,383);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(124,14);
			this.label33.TabIndex = 83;
			this.label33.Text = "Employer";
			this.label33.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupPH
			// 
			this.groupPH.Controls.Add(this.comboUrgency);
			this.groupPH.Controls.Add(this.comboGradeLevel);
			this.groupPH.Controls.Add(this.textGradeSchool);
			this.groupPH.Controls.Add(this.label35);
			this.groupPH.Controls.Add(this.comboRace);
			this.groupPH.Controls.Add(this.textCounty);
			this.groupPH.Controls.Add(this.label15);
			this.groupPH.Controls.Add(this.label14);
			this.groupPH.Controls.Add(this.label13);
			this.groupPH.Controls.Add(this.label10);
			this.groupPH.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupPH.Location = new System.Drawing.Point(440,536);
			this.groupPH.Name = "groupPH";
			this.groupPH.Size = new System.Drawing.Size(378,123);
			this.groupPH.TabIndex = 23;
			this.groupPH.TabStop = false;
			this.groupPH.Text = "Public Health";
			// 
			// comboUrgency
			// 
			this.comboUrgency.BackColor = System.Drawing.SystemColors.Window;
			this.comboUrgency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboUrgency.Location = new System.Drawing.Point(161,97);
			this.comboUrgency.Name = "comboUrgency";
			this.comboUrgency.Size = new System.Drawing.Size(155,21);
			this.comboUrgency.TabIndex = 4;
			// 
			// comboGradeLevel
			// 
			this.comboGradeLevel.BackColor = System.Drawing.SystemColors.Window;
			this.comboGradeLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboGradeLevel.Location = new System.Drawing.Point(161,76);
			this.comboGradeLevel.MaxDropDownItems = 25;
			this.comboGradeLevel.Name = "comboGradeLevel";
			this.comboGradeLevel.Size = new System.Drawing.Size(155,21);
			this.comboGradeLevel.TabIndex = 3;
			// 
			// textGradeSchool
			// 
			this.textGradeSchool.AcceptsReturn = true;
			this.textGradeSchool.Location = new System.Drawing.Point(161,56);
			this.textGradeSchool.Name = "textGradeSchool";
			this.textGradeSchool.Size = new System.Drawing.Size(213,20);
			this.textGradeSchool.TabIndex = 2;
			this.textGradeSchool.Leave += new System.EventHandler(this.textGradeSchool_Leave);
			this.textGradeSchool.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textGradeSchool_KeyUp);
			// 
			// label35
			// 
			this.label35.Location = new System.Drawing.Point(10,96);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(149,17);
			this.label35.TabIndex = 6;
			this.label35.Text = "Treatment Urgency";
			this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboRace
			// 
			this.comboRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboRace.Location = new System.Drawing.Point(161,15);
			this.comboRace.MaxDropDownItems = 20;
			this.comboRace.Name = "comboRace";
			this.comboRace.Size = new System.Drawing.Size(156,21);
			this.comboRace.TabIndex = 0;
			// 
			// textCounty
			// 
			this.textCounty.AcceptsReturn = true;
			this.textCounty.Location = new System.Drawing.Point(161,36);
			this.textCounty.Name = "textCounty";
			this.textCounty.Size = new System.Drawing.Size(213,20);
			this.textCounty.TabIndex = 1;
			this.textCounty.Leave += new System.EventHandler(this.textCounty_Leave);
			this.textCounty.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textCounty_KeyUp);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(19,76);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(140,17);
			this.label15.TabIndex = 3;
			this.label15.Text = "Grade Level";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(8,56);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(151,17);
			this.label14.TabIndex = 2;
			this.label14.Text = "Site (or Grade School)";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(16,36);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(143,17);
			this.label13.TabIndex = 1;
			this.label13.Text = "County";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(18,16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(141,17);
			this.label10.TabIndex = 0;
			this.label10.Text = "Race/Ethnicity";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateFirstVisit
			// 
			this.textDateFirstVisit.Location = new System.Drawing.Point(135,358);
			this.textDateFirstVisit.Name = "textDateFirstVisit";
			this.textDateFirstVisit.Size = new System.Drawing.Size(82,20);
			this.textDateFirstVisit.TabIndex = 13;
			// 
			// label36
			// 
			this.label36.Location = new System.Drawing.Point(8,362);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(125,14);
			this.label36.TabIndex = 86;
			this.label36.Text = "Date of First Visit";
			this.label36.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label37
			// 
			this.label37.Location = new System.Drawing.Point(239,299);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(179,14);
			this.label37.TabIndex = 88;
			this.label37.Text = "(put in InsPlan too)";
			this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(45,554);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(86,14);
			this.labelClinic.TabIndex = 89;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(135,551);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(198,21);
			this.comboClinic.TabIndex = 22;
			// 
			// textTrophyFolder
			// 
			this.textTrophyFolder.Location = new System.Drawing.Point(135,508);
			this.textTrophyFolder.MaxLength = 30;
			this.textTrophyFolder.Name = "textTrophyFolder";
			this.textTrophyFolder.Size = new System.Drawing.Size(226,20);
			this.textTrophyFolder.TabIndex = 20;
			// 
			// labelTrophyFolder
			// 
			this.labelTrophyFolder.Location = new System.Drawing.Point(16,511);
			this.labelTrophyFolder.Name = "labelTrophyFolder";
			this.labelTrophyFolder.Size = new System.Drawing.Size(118,14);
			this.labelTrophyFolder.TabIndex = 90;
			this.labelTrophyFolder.Text = "Trophy Folder";
			this.labelTrophyFolder.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textWard
			// 
			this.textWard.Location = new System.Drawing.Point(135,573);
			this.textWard.MaxLength = 100;
			this.textWard.Name = "textWard";
			this.textWard.Size = new System.Drawing.Size(55,20);
			this.textWard.TabIndex = 23;
			// 
			// labelWard
			// 
			this.labelWard.Location = new System.Drawing.Point(79,578);
			this.labelWard.Name = "labelWard";
			this.labelWard.Size = new System.Drawing.Size(53,14);
			this.labelWard.TabIndex = 92;
			this.labelWard.Text = "Ward";
			this.labelWard.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(15,532);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(118,14);
			this.label28.TabIndex = 94;
			this.label28.Text = "Language";
			this.label28.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboLanguage
			// 
			this.comboLanguage.BackColor = System.Drawing.SystemColors.Window;
			this.comboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboLanguage.Location = new System.Drawing.Point(135,529);
			this.comboLanguage.MaxDropDownItems = 25;
			this.comboLanguage.Name = "comboLanguage";
			this.comboLanguage.Size = new System.Drawing.Size(226,21);
			this.comboLanguage.TabIndex = 21;
			// 
			// comboContact
			// 
			this.comboContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboContact.FormattingEnabled = true;
			this.comboContact.Location = new System.Drawing.Point(135,442);
			this.comboContact.MaxDropDownItems = 30;
			this.comboContact.Name = "comboContact";
			this.comboContact.Size = new System.Drawing.Size(174,21);
			this.comboContact.TabIndex = 17;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(1,444);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(132,16);
			this.label23.TabIndex = 96;
			this.label23.Text = "Prefer Contact Method";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboConfirm
			// 
			this.comboConfirm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboConfirm.FormattingEnabled = true;
			this.comboConfirm.Location = new System.Drawing.Point(135,464);
			this.comboConfirm.MaxDropDownItems = 30;
			this.comboConfirm.Name = "comboConfirm";
			this.comboConfirm.Size = new System.Drawing.Size(174,21);
			this.comboConfirm.TabIndex = 18;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(1,466);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(132,16);
			this.label24.TabIndex = 98;
			this.label24.Text = "Prefer Confirm Method";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboRecall
			// 
			this.comboRecall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboRecall.FormattingEnabled = true;
			this.comboRecall.Location = new System.Drawing.Point(135,486);
			this.comboRecall.MaxDropDownItems = 30;
			this.comboRecall.Name = "comboRecall";
			this.comboRecall.Size = new System.Drawing.Size(174,21);
			this.comboRecall.TabIndex = 19;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(1,488);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(132,16);
			this.label25.TabIndex = 100;
			this.label25.Text = "Prefer Recall Method";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAdmitDate
			// 
			this.textAdmitDate.Location = new System.Drawing.Point(312,573);
			this.textAdmitDate.Name = "textAdmitDate";
			this.textAdmitDate.Size = new System.Drawing.Size(82,20);
			this.textAdmitDate.TabIndex = 102;
			// 
			// labelAdmitDate
			// 
			this.labelAdmitDate.Location = new System.Drawing.Point(206,577);
			this.labelAdmitDate.Name = "labelAdmitDate";
			this.labelAdmitDate.Size = new System.Drawing.Size(104,14);
			this.labelAdmitDate.TabIndex = 101;
			this.labelAdmitDate.Text = "Admit Date";
			this.labelAdmitDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textTitle
			// 
			this.textTitle.Location = new System.Drawing.Point(135,113);
			this.textTitle.MaxLength = 100;
			this.textTitle.Name = "textTitle";
			this.textTitle.Size = new System.Drawing.Size(75,20);
			this.textTitle.TabIndex = 103;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(9,116);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(124,16);
			this.label26.TabIndex = 104;
			this.label26.Text = "Title";
			this.label26.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormPatientEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(962,673);
			this.Controls.Add(this.textTitle);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.textAdmitDate);
			this.Controls.Add(this.labelAdmitDate);
			this.Controls.Add(this.comboRecall);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.comboConfirm);
			this.Controls.Add(this.label24);
			this.Controls.Add(this.comboContact);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.comboLanguage);
			this.Controls.Add(this.label28);
			this.Controls.Add(this.textWard);
			this.Controls.Add(this.labelWard);
			this.Controls.Add(this.textTrophyFolder);
			this.Controls.Add(this.labelTrophyFolder);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.label37);
			this.Controls.Add(this.textDateFirstVisit);
			this.Controls.Add(this.textEmployer);
			this.Controls.Add(this.textMedicaidID);
			this.Controls.Add(this.textPatNum);
			this.Controls.Add(this.textBirthdate);
			this.Controls.Add(this.textChartNumber);
			this.Controls.Add(this.textEmail);
			this.Controls.Add(this.textSalutation);
			this.Controls.Add(this.textAge);
			this.Controls.Add(this.textWirelessPhone);
			this.Controls.Add(this.textWkPhone);
			this.Controls.Add(this.textSSN);
			this.Controls.Add(this.textPreferred);
			this.Controls.Add(this.textMiddleI);
			this.Controls.Add(this.textFName);
			this.Controls.Add(this.textLName);
			this.Controls.Add(this.butAuto);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label36);
			this.Controls.Add(this.groupPH);
			this.Controls.Add(this.label33);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.listPosition);
			this.Controls.Add(this.listGender);
			this.Controls.Add(this.listStatus);
			this.Controls.Add(this.label31);
			this.Controls.Add(this.label32);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.groupNotes);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label29);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label22);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.labelSSN);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPatientEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Patient Information";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormPatientEdit_Closing);
			this.Load += new System.EventHandler(this.FormPatientEdit_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupNotes.ResumeLayout(false);
			this.groupNotes.PerformLayout();
			this.groupPH.ResumeLayout(false);
			this.groupPH.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPatientEdit_Load(object sender, System.EventArgs e) {
			FillComboZip();
			checkSame.Checked=true;
			checkNotesSame.Checked=true;
			if(PatCur!=null){
				for(int i=0;i<FamCur.List.Length;i++){
					if(PatCur.HmPhone!=FamCur.List[i].HmPhone
						|| PatCur.Address!=FamCur.List[i].Address
						|| PatCur.Address2!=FamCur.List[i].Address2
						|| PatCur.City!=FamCur.List[i].City
						|| PatCur.State!=FamCur.List[i].State
						|| PatCur.Zip!=FamCur.List[i].Zip
						|| PatCur.CreditType!=FamCur.List[i].CreditType
						|| PatCur.BillingType!=FamCur.List[i].BillingType
						|| PatCur.PriProv!=FamCur.List[i].PriProv
						|| PatCur.SecProv!=FamCur.List[i].SecProv
						|| PatCur.FeeSched!=FamCur.List[i].FeeSched)
					{
						checkSame.Checked=false;
					}
					if(PatCur.AddrNote!=FamCur.List[i].AddrNote)
					{
						checkNotesSame.Checked=false;
					}
				}
			}
			textPatNum.Text=PatCur.PatNum.ToString();
			textLName.Text=PatCur.LName;
			textFName.Text=PatCur.FName;
			textMiddleI.Text=PatCur.MiddleI;
			textPreferred.Text=PatCur.Preferred;
			textTitle.Text=PatCur.Title;
			textSalutation.Text=PatCur.Salutation;
			listStatus.Items.Add(Lan.g("enumPatientStatus","Patient"));
			listStatus.Items.Add(Lan.g("enumPatientStatus","NonPatient"));
			listStatus.Items.Add(Lan.g("enumPatientStatus","Inactive"));
			listStatus.Items.Add(Lan.g("enumPatientStatus","Archived"));
			listStatus.Items.Add(Lan.g("enumPatientStatus","Deceased"));
			listGender.Items.Add(Lan.g("enumPatientGender","Male"));
			listGender.Items.Add(Lan.g("enumPatientGender","Female"));
			listGender.Items.Add(Lan.g("enumPatientGender","Unknown"));
			listPosition.Items.Add(Lan.g("enumPatientPosition","Single"));
			listPosition.Items.Add(Lan.g("enumPatientPosition","Married"));
			listPosition.Items.Add(Lan.g("enumPatientPosition","Child"));
			listPosition.Items.Add(Lan.g("enumPatientPosition","Widowed"));
			listPosition.Items.Add(Lan.g("enumPatientPosition","Divorced"));
			switch (PatCur.PatStatus){
				case PatientStatus.Patient : listStatus.SelectedIndex=0; break;
				case PatientStatus.NonPatient : listStatus.SelectedIndex=1; break;
				case PatientStatus.Inactive : listStatus.SelectedIndex=2; break;
				case PatientStatus.Archived : listStatus.SelectedIndex=3; break;
				case PatientStatus.Deceased : listStatus.SelectedIndex=4; break;}
			switch (PatCur.Gender){
				case PatientGender.Male : listGender.SelectedIndex=0; break;
				case PatientGender.Female : listGender.SelectedIndex=1; break;
				case PatientGender.Unknown : listGender.SelectedIndex=2; break;}
			switch (PatCur.Position){
				case PatientPosition.Single : listPosition.SelectedIndex=0; break;
				case PatientPosition.Married : listPosition.SelectedIndex=1; break;
				case PatientPosition.Child : listPosition.SelectedIndex=2; break;
				case PatientPosition.Widowed : listPosition.SelectedIndex=3; break;
				case PatientPosition.Divorced : listPosition.SelectedIndex=4; break;}
			if(PatCur.Birthdate.Year < 1880)
				textBirthdate.Text="";
			else
				textBirthdate.Text=PatCur.Birthdate.ToShortDateString();
			textAge.Text=PatientB.DateToAgeString(PatCur.Birthdate);
			if(CultureInfo.CurrentCulture.Name=="en-US"//if USA
				&& PatCur.SSN!=null//the null catches new patients
				&& PatCur.SSN.Length==9)//and length exactly 9 (no data gets lost in formatting)
			{
				textSSN.Text=PatCur.SSN.Substring(0,3)+"-"
					+PatCur.SSN.Substring(3,2)+"-"+PatCur.SSN.Substring(5,4);
			}
			else
				textSSN.Text=PatCur.SSN;
			textMedicaidID.Text=PatCur.MedicaidID;
			textAddress.Text=PatCur.Address;
			textAddress2.Text=PatCur.Address2;
			textCity.Text=PatCur.City;
			textState.Text=PatCur.State;
			textZip.Text=PatCur.Zip;
			textHmPhone.Text=PatCur.HmPhone;
			textWkPhone.Text=PatCur.WkPhone;
			textWirelessPhone.Text=PatCur.WirelessPhone;
			textEmail.Text=PatCur.Email;
			textCreditType.Text=PatCur.CreditType;
			if(PatCur.DateFirstVisit.Year < 1880)
				textDateFirstVisit.Text="";
			else
				textDateFirstVisit.Text=PatCur.DateFirstVisit.ToShortDateString();
			textChartNumber.Text=PatCur.ChartNumber;
			textEmployer.Text=Employers.GetName(PatCur.EmployerNum);
			//textEmploymentNote.Text=PatCur.EmploymentNote;
			languageList=new List<CultureInfo>();
			if(PrefB.GetString("LanguagesUsedByPatients")!=""){
				string[] lanstring=PrefB.GetString("LanguagesUsedByPatients").Split(',');
				for(int i=0;i<lanstring.Length;i++) {
					languageList.Add(CultureInfo.GetCultureInfo(lanstring[i]));
				}
			}
			if(PatCur.Language!="" && PatCur.Language!=null && !languageList.Contains(CultureInfo.GetCultureInfo(PatCur.Language))){
				languageList.Add(CultureInfo.GetCultureInfo(PatCur.Language));
			}
			comboLanguage.Items.Add(Lan.g(this,"none"));//regardless of how many languages are listed, the first item is "none"
			comboLanguage.SelectedIndex=0;
			for(int i=0;i<languageList.Count;i++) {
				comboLanguage.Items.Add(languageList[i].DisplayName);
				if(PatCur.Language!="" && PatCur.Language!=null){
					if(CultureInfo.GetCultureInfo(PatCur.Language)==languageList[i]) {
						comboLanguage.SelectedIndex=i+1;
					}
				}
			}
			for(int i=0;i<Providers.List.Length;i++){
				comboPriProv.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==PatCur.PriProv)
					comboPriProv.SelectedIndex=i;
			}
			if(comboPriProv.SelectedIndex==-1){
				int defaultindex=Providers.GetIndex(PrefB.GetInt("PracticeDefaultProv"));
				if(defaultindex==-1) {//default provider hidden
					comboPriProv.SelectedIndex=0;
				}
				else {
					comboPriProv.SelectedIndex=defaultindex;
				}
			}
			comboSecProv.Items.Clear();
			comboSecProv.Items.Add(Lan.g(this,"none"));
			comboSecProv.SelectedIndex=0;
			for(int i=0;i<Providers.List.Length;i++){
				comboSecProv.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==PatCur.SecProv)
					comboSecProv.SelectedIndex=i+1;
			}
			comboFeeSched.Items.Clear();
			comboFeeSched.Items.Add(Lan.g(this,"none"));
			comboFeeSched.SelectedIndex=0;
			for(int i=0;i<DefB.Short[(int)DefCat.FeeSchedNames].Length;i++){
				comboFeeSched.Items.Add(DefB.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(DefB.Short[(int)DefCat.FeeSchedNames][i].DefNum==PatCur.FeeSched)
					comboFeeSched.SelectedIndex=i+1;
			}
			//MessageBox.Show(DefB.Short[(int)DefCat.BillingTypes].Length.ToString());
			for(int i=0;i<DefB.Short[(int)DefCat.BillingTypes].Length;i++){
				comboBillType.Items.Add(DefB.Short[(int)DefCat.BillingTypes][i].ItemName);
				if(DefB.Short[(int)DefCat.BillingTypes][i].DefNum==PatCur.BillingType)
					comboBillType.SelectedIndex=i;
			}
			if(comboBillType.SelectedIndex==-1){
				comboBillType.SelectedIndex=0;
			}
			comboClinic.Items.Clear();
			comboClinic.Items.Add(Lan.g(this,"none"));
			comboClinic.SelectedIndex=0;
			for(int i=0;i<Clinics.List.Length;i++){
				comboClinic.Items.Add(Clinics.List[i].Description);
				if(Clinics.List[i].ClinicNum==PatCur.ClinicNum){
					comboClinic.SelectedIndex=i+1;
				}
			}
			switch(PatCur.StudentStatus){
				case "N"://non
				case "":
					radioStudentN.Checked=true;
					break;
				case "P"://parttime
					radioStudentP.Checked=true;
					break;
				case "F"://fulltime
					radioStudentF.Checked=true;
					break;
			}
			textSchool.Text=PatCur.SchoolName;
			textAddrNotes.Text=PatCur.AddrNote;
			if(PrefB.GetBool("EasyHidePublicHealth")){
				groupPH.Visible=false;
			}
			if(PrefB.GetBool("EasyHideMedicaid")){
				label31.Visible=false;
				label37.Visible=false;
				textMedicaidID.Visible=false;
			}
			if(PrefB.GetBool("EasyNoClinics")){
				comboClinic.Visible=false;
				labelClinic.Visible=false;
			}
			string[] enumRace=Enum.GetNames(typeof(PatientRace));
			for(int i=0;i<enumRace.Length;i++){
				comboRace.Items.Add(Lan.g("enumRace",enumRace[i]));
			}
			comboRace.SelectedIndex=(int)PatCur.Race;
			textCounty.Text=PatCur.County;
			textGradeSchool.Text=PatCur.GradeSchool;
			string[] enumGrade=Enum.GetNames(typeof(PatientGrade));
			for(int i=0;i<enumGrade.Length;i++){
				comboGradeLevel.Items.Add(Lan.g("enumGrade",enumGrade[i]));
			}
			comboGradeLevel.SelectedIndex=(int)PatCur.GradeLevel;
			string[] enumUrg=Enum.GetNames(typeof(TreatmentUrgency));
			for(int i=0;i<enumUrg.Length;i++){
				comboUrgency.Items.Add(Lan.g("enumUrg",enumUrg[i]));
			}
			comboUrgency.SelectedIndex=(int)PatCur.Urgency;
			if(Programs.IsEnabled("TrophyEnhanced")){
				textTrophyFolder.Text=PatCur.TrophyFolder;
			}
			else{
				labelTrophyFolder.Visible=false;
				textTrophyFolder.Visible=false;
			}
			if(PrefB.GetBool("EasyHideHospitals")){
				textWard.Visible=false;
				labelWard.Visible=false;
				textAdmitDate.Visible=false;
				labelAdmitDate.Visible=false;
			}
			textWard.Text=PatCur.Ward;
			for(int i=0;i<Enum.GetNames(typeof(ContactMethod)).Length;i++){
				comboContact.Items.Add(Lan.g("enumContactMethod",Enum.GetNames(typeof(ContactMethod))[i]));
				comboConfirm.Items.Add(Lan.g("enumContactMethod",Enum.GetNames(typeof(ContactMethod))[i]));
				comboRecall.Items.Add(Lan.g("enumContactMethod",Enum.GetNames(typeof(ContactMethod))[i]));
			}
			comboContact.SelectedIndex=(int)PatCur.PreferContactMethod;
			comboConfirm.SelectedIndex=(int)PatCur.PreferConfirmMethod;
			comboRecall.SelectedIndex=(int)PatCur.PreferRecallMethod;
			if(PatCur.AdmitDate.Year>1880){
				textAdmitDate.Text=PatCur.AdmitDate.ToShortDateString();
			}
			textLName.Select();
		}

		private void FillComboZip(){
			comboZip.Items.Clear();
			for(int i=0;i<ZipCodes.ALFrequent.Count;i++)
			{
				comboZip.Items.Add(((ZipCode)ZipCodes.ALFrequent[i]).ZipCodeDigits
					+"("+((ZipCode)ZipCodes.ALFrequent[i]).City+")");
			}
		}

		//private void butSecClear_Click(object sender, System.EventArgs e) {
		//	listSecProv.SelectedIndex=-1;
		//}

		//private void butClearFee_Click(object sender, System.EventArgs e) {
		//	listFeeSched.SelectedIndex=-1;
		//}

		private void textLName_TextChanged(object sender, System.EventArgs e) {
			if(textLName.Text.Length==1){
				textLName.Text=textLName.Text.ToUpper();
				textLName.SelectionStart=1;
			}
		}

		private void textFName_TextChanged(object sender, System.EventArgs e) {
			if(textFName.Text.Length==1){
				textFName.Text=textFName.Text.ToUpper();
				textFName.SelectionStart=1;
			}
		}

		private void textMiddleI_TextChanged(object sender, System.EventArgs e) {
			if(textMiddleI.Text.Length==1){
				textMiddleI.Text=textMiddleI.Text.ToUpper();
				textMiddleI.SelectionStart=1;
			}
		}

		private void textPreferred_TextChanged(object sender, System.EventArgs e) {
			if(textPreferred.Text.Length==1){
				textPreferred.Text=textPreferred.Text.ToUpper();
				textPreferred.SelectionStart=1;
			}
		}

		private void textSalutation_TextChanged(object sender, System.EventArgs e) {
			if(textSalutation.Text.Length==1){
				textSalutation.Text=textSalutation.Text.ToUpper();
				textSalutation.SelectionStart=1;
			}
		}

		private void textAddress_TextChanged(object sender, System.EventArgs e) {
			if(textAddress.Text.Length==1){
				textAddress.Text=textAddress.Text.ToUpper();
				textAddress.SelectionStart=1;
			}
		}

		private void textAddress2_TextChanged(object sender, System.EventArgs e) {
			if(textAddress2.Text.Length==1){
				textAddress2.Text=textAddress2.Text.ToUpper();
				textAddress2.SelectionStart=1;
			}
		}

		private void radioStudentN_Click(object sender, System.EventArgs e) {
			PatCur.StudentStatus="N";
		}

		private void radioStudentF_Click(object sender, System.EventArgs e) {
			PatCur.StudentStatus="F";
		}

		private void radioStudentP_Click(object sender, System.EventArgs e) {
			PatCur.StudentStatus="P";
		}

		private void textSSN_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(CultureInfo.CurrentCulture.Name!="en-US"){
				return;
			}
			//only reformats if in USA and exactly 9 digits.
			if(textSSN.Text==""){
				return;
			}
			if(textSSN.Text.Length==9){//if just numbers, try to reformat.
				bool SSNisValid=true;
				for(int i=0;i<textSSN.Text.Length;i++){
					if(!Char.IsNumber(textSSN.Text,i)){
						SSNisValid=false;
					}
				}
				if(SSNisValid){
					textSSN.Text=textSSN.Text.Substring(0,3)+"-"
						+textSSN.Text.Substring(3,2)+"-"+textSSN.Text.Substring(5,4);	
				}
			}
			if(!Regex.IsMatch(textSSN.Text,@"^\d\d\d-\d\d-\d\d\d\d$")){
				if(MessageBox.Show("SSN not valid. Continue anyway?","",MessageBoxButtons.OKCancel)
					!=DialogResult.OK)
				{
					e.Cancel=true;
				}
			}		
		}

		private void textCreditType_TextChanged(object sender, System.EventArgs e) {
			textCreditType.Text=textCreditType.Text.ToUpper();
			textCreditType.SelectionStart=1;
		}

		private void textBirthdate_Validated(object sender, System.EventArgs e) {
			if(textBirthdate.errorProvider1.GetError(textBirthdate)!=""){
				textAge.Text="";
				return;
			}
			DateTime birthdate=PIn.PDate(textBirthdate.Text);
			if(birthdate>DateTime.Today){
				birthdate=birthdate.AddYears(-100);
			}
			textAge.Text=PatientB.DateToAgeString(birthdate);// .AgeToString(Shared.DateToAge(birthdate));
		}

		private void textZip_TextChanged(object sender, System.EventArgs e) {
			comboZip.SelectedIndex=-1;
		}

		private void comboZip_SelectionChangeCommitted(object sender, System.EventArgs e) {
			//this happens when a zipcode is selected from the combobox of frequent zips.
			//The combo box is tucked under textZip because Microsoft makes stupid controls.
			textCity.Text=((ZipCode)ZipCodes.ALFrequent[comboZip.SelectedIndex]).City;
			textState.Text=((ZipCode)ZipCodes.ALFrequent[comboZip.SelectedIndex]).State;
			textZip.Text=((ZipCode)ZipCodes.ALFrequent[comboZip.SelectedIndex]).ZipCodeDigits;
		}

		private void textZip_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			//fired as soon as control loses focus.
			//it's here to validate if zip is typed in to text box instead of picked from list.
			//if(textZip.Text=="" && (textCity.Text!="" || textState.Text!="")){
			//	if(MessageBox.Show(Lan.g(this,"Delete the City and State?"),"",MessageBoxButtons.OKCancel)
			//		==DialogResult.OK){
			//		textCity.Text="";
			//		textState.Text="";
			//	}	
			//	return;
			//}
			if(textZip.Text.Length<5){
				return;
			}
			if(comboZip.SelectedIndex!=-1){
				return;
			}
			//the autofill only works if both city and state are left blank
			if(textCity.Text!="" || textState.Text!=""){
				return;
			}
			ZipCodes.GetALMatches(textZip.Text);
			if(ZipCodes.ALMatches.Count==0){
				//No match found. Must enter info for new zipcode
				ZipCode ZipCodeCur=new ZipCode();
				ZipCodeCur.ZipCodeDigits=textZip.Text;
				FormZipCodeEdit FormZE=new FormZipCodeEdit();
				FormZE.ZipCodeCur=ZipCodeCur;
				FormZE.IsNew=true;
				FormZE.ShowDialog();
				if(FormZE.DialogResult!=DialogResult.OK){
					return;
				}
				DataValid.SetInvalid(InvalidTypes.ZipCodes);//FormZipCodeEdit does not contain internal refresh
				FillComboZip();
				textCity.Text=ZipCodeCur.City;
				textState.Text=ZipCodeCur.State;
				textZip.Text=ZipCodeCur.ZipCodeDigits;
			}
			else if(ZipCodes.ALMatches.Count==1){
				//only one match found.  Use it.
				textCity.Text=((ZipCode)ZipCodes.ALMatches[0]).City;
				textState.Text=((ZipCode)ZipCodes.ALMatches[0]).State;
			}
			else{
				//multiple matches found.  Pick one
				FormZipSelect FormZS=new FormZipSelect();
				FormZS.ShowDialog();
				FillComboZip();
				if(FormZS.DialogResult!=DialogResult.OK){
					return;
				}
				DataValid.SetInvalid(InvalidTypes.ZipCodes);
				textCity.Text=FormZS.ZipSelected.City;
				textState.Text=FormZS.ZipSelected.State;
				textZip.Text=FormZS.ZipSelected.ZipCodeDigits;
			}
		}

		private void butEditZip_Click(object sender, System.EventArgs e) {
			if(textZip.Text.Length==0){
				MessageBox.Show(Lan.g(this,"Please enter a zipcode first."));
				return;
			}
			ZipCodes.GetALMatches(textZip.Text);
			if(ZipCodes.ALMatches.Count==0){
				FormZipCodeEdit FormZE=new FormZipCodeEdit();
				FormZE.ZipCodeCur=new ZipCode();
				FormZE.ZipCodeCur.ZipCodeDigits=textZip.Text;
				FormZE.IsNew=true;
				FormZE.ShowDialog();
				FillComboZip();
				if(FormZE.DialogResult!=DialogResult.OK){
					return;
				}
				DataValid.SetInvalid(InvalidTypes.ZipCodes);
				textCity.Text=FormZE.ZipCodeCur.City;
				textState.Text=FormZE.ZipCodeCur.State;
				textZip.Text=FormZE.ZipCodeCur.ZipCodeDigits;
			}
			else{
				FormZipSelect FormZS=new FormZipSelect();
				FormZS.ShowDialog();
				FillComboZip();
				if(FormZS.DialogResult!=DialogResult.OK){
					return;
				}
				//Not needed:
				//DataValid.SetInvalid(InvalidTypes.ZipCodes);
				textCity.Text=FormZS.ZipSelected.City;
				textState.Text=FormZS.ZipSelected.State;
				textZip.Text=FormZS.ZipSelected.ZipCodeDigits;
			}
		}

		private void textWirelessPhone_TextChanged(object sender, System.EventArgs e) {
			int cursor=textWirelessPhone.SelectionStart;
			int length=textWirelessPhone.Text.Length;
			textWirelessPhone.Text=TelephoneNumbers.AutoFormat(textWirelessPhone.Text);
			if(textWirelessPhone.Text.Length>length)
				cursor++;
			textWirelessPhone.SelectionStart=cursor;		
		}

		private void textWkPhone_TextChanged(object sender, System.EventArgs e) {
		 	int cursor=textWkPhone.SelectionStart;
			int length=textWkPhone.Text.Length;
			textWkPhone.Text=TelephoneNumbers.AutoFormat(textWkPhone.Text);
			if(textWkPhone.Text.Length>length)
				cursor++;
			textWkPhone.SelectionStart=cursor;		
		}

		private void textHmPhone_TextChanged(object sender, System.EventArgs e) {
		 	int cursor=textHmPhone.SelectionStart;
			int length=textHmPhone.Text.Length;
			textHmPhone.Text=TelephoneNumbers.AutoFormat(textHmPhone.Text);
			if(textHmPhone.Text.Length>length)
				cursor++;
			textHmPhone.SelectionStart=cursor;		
		}

		private void butAuto_Click(object sender, System.EventArgs e) {
			textChartNumber.Text=Patients.GetNextChartNum();
		}

		private void textCity_TextChanged(object sender, System.EventArgs e) {
			if(textCity.Text.Length==1){
				textCity.Text=textCity.Text.ToUpper();
				textCity.SelectionStart=1;
			}
		}

		private void textState_TextChanged(object sender, System.EventArgs e) {
			if(CultureInfo.CurrentCulture.Name=="en-US" //if USA or Canada, capitalize first 2 letters
				|| CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){
				if(textState.Text.Length==1 || textState.Text.Length==2){
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=2;
				}
			}
			else{
				if(textState.Text.Length==1){
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=1;
				}
			}
		}

		#region Public Health
		private void textGradeSchool_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(e.KeyCode==Keys.Return){
				listSchools.Visible=false;
				comboGradeLevel.Focus();
				return;
			}
			if(textGradeSchool.Text==""){
				listSchools.Visible=false;
				return;
			}
			if(e.KeyCode==Keys.Down){
				if(listSchools.Items.Count==0){
					return;
				}
				if(listSchools.SelectedIndex==-1){
					listSchools.SelectedIndex=0;
					textGradeSchool.Text=listSchools.SelectedItem.ToString();
				}
				else if(listSchools.SelectedIndex==listSchools.Items.Count-1){
					listSchools.SelectedIndex=-1;
					textGradeSchool.Text=schoolOriginal;
				}
				else{
					listSchools.SelectedIndex++;
					textGradeSchool.Text=listSchools.SelectedItem.ToString();
				}
				textGradeSchool.SelectionStart=textGradeSchool.Text.Length;
				return;
			}
			if(e.KeyCode==Keys.Up){
				if(listSchools.Items.Count==0){
					return;
				}
				if(listSchools.SelectedIndex==-1){
					listSchools.SelectedIndex=listSchools.Items.Count-1;
					textGradeSchool.Text=listSchools.SelectedItem.ToString();
				}
				else if(listSchools.SelectedIndex==0){
					listSchools.SelectedIndex=-1;
					textGradeSchool.Text=schoolOriginal;
				}
				else{
					listSchools.SelectedIndex--;
					textGradeSchool.Text=listSchools.SelectedItem.ToString();
				}
				textGradeSchool.SelectionStart=textGradeSchool.Text.Length;
				return;
			}
			if(textGradeSchool.Text.Length==1){
				textGradeSchool.Text=textGradeSchool.Text.ToUpper();
				textGradeSchool.SelectionStart=1;
			}
			schoolOriginal=textGradeSchool.Text;//the original text is preserved when using up and down arrows
			listSchools.Items.Clear();
			Schools.Refresh(textGradeSchool.Text);
			for(int i=0;i<Schools.List.Length;i++){
				listSchools.Items.Add(Schools.List[i].SchoolName);
			}
			int h=13*Schools.List.Length+5;
			if(h > ClientSize.Height-listSchools.Top)
				h=ClientSize.Height-listSchools.Top;
			listSchools.Size=new Size(textGradeSchool.Width,h);
			listSchools.Visible=true;
		}

		private void textGradeSchool_Leave(object sender, System.EventArgs e) {
			if(mouseIsInListSchools){
				return;
			}
			//or if user clicked on a different text box.
			if(listSchools.SelectedIndex!=-1){
				textGradeSchool.Text=Schools.List[listSchools.SelectedIndex].SchoolName;
				//InsPlans.Cur.CarrierNum=((Carrier)similarCars[listCars.SelectedIndex]).CarrierNum;
			}
			listSchools.Visible=false;
		}

		private void listSchools_Click(object sender, System.EventArgs e){
			textGradeSchool.Text=Schools.List[listSchools.SelectedIndex].SchoolName;
			//InsPlans.Cur.CarrierNum=((Carrier)similarCars[listCars.SelectedIndex]).CarrierNum;
			textGradeSchool.Focus();
			textGradeSchool.SelectionStart=textGradeSchool.Text.Length;
			listSchools.Visible=false;
		}

		private void listSchools_MouseEnter(object sender, System.EventArgs e){
			mouseIsInListSchools=true;
		}

		private void listSchools_MouseLeave(object sender, System.EventArgs e){
			mouseIsInListSchools=false;
		}

		private void textCounty_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(e.KeyCode==Keys.Return){
				listCounties.Visible=false;
				comboGradeLevel.Focus();
				return;
			}
			if(textCounty.Text==""){
				listCounties.Visible=false;
				return;
			}
			if(e.KeyCode==Keys.Down){
				if(listCounties.Items.Count==0){
					return;
				}
				if(listCounties.SelectedIndex==-1){
					listCounties.SelectedIndex=0;
					textCounty.Text=listCounties.SelectedItem.ToString();
				}
				else if(listCounties.SelectedIndex==listCounties.Items.Count-1){
					listCounties.SelectedIndex=-1;
					textCounty.Text=countyOriginal;
				}
				else{
					listCounties.SelectedIndex++;
					textCounty.Text=listCounties.SelectedItem.ToString();
				}
				textCounty.SelectionStart=textCounty.Text.Length;
				return;
			}
			if(e.KeyCode==Keys.Up){
				if(listCounties.Items.Count==0){
					return;
				}
				if(listCounties.SelectedIndex==-1){
					listCounties.SelectedIndex=listCounties.Items.Count-1;
					textCounty.Text=listCounties.SelectedItem.ToString();
				}
				else if(listCounties.SelectedIndex==0){
					listCounties.SelectedIndex=-1;
					textCounty.Text=countyOriginal;
				}
				else{
					listCounties.SelectedIndex--;
					textCounty.Text=listCounties.SelectedItem.ToString();
				}
				textCounty.SelectionStart=textCounty.Text.Length;
				return;
			}
			if(textCounty.Text.Length==1){
				textCounty.Text=textCounty.Text.ToUpper();
				textCounty.SelectionStart=1;
			}
			countyOriginal=textCounty.Text;//the original text is preserved when using up and down arrows
			listCounties.Items.Clear();
			Counties.Refresh(textCounty.Text);
			//similarSchools=
				//Carriers.GetSimilarNames(textCounty.Text);
			for(int i=0;i<Counties.List.Length;i++){
				listCounties.Items.Add(Counties.List[i].CountyName);
			}
			int h=13*Counties.List.Length+5;
			if(h > ClientSize.Height-listCounties.Top)
				h=ClientSize.Height-listCounties.Top;
			listCounties.Size=new Size(textCounty.Width,h);
			listCounties.Visible=true;
		}

		private void textCounty_Leave(object sender, System.EventArgs e) {
			if(mouseIsInListCounties){
				return;
			}
			//or if user clicked on a different text box.
			if(listCounties.SelectedIndex!=-1){
				textCounty.Text=Counties.List[listCounties.SelectedIndex].CountyName;
			}
			listCounties.Visible=false;
		}

		private void listCounties_Click(object sender, System.EventArgs e){
			textCounty.Text=Counties.List[listCounties.SelectedIndex].CountyName;
			textCounty.Focus();
			textCounty.SelectionStart=textCounty.Text.Length;
			listCounties.Visible=false;
		}

		private void listCounties_MouseEnter(object sender, System.EventArgs e){
			mouseIsInListCounties=true;
		}

		private void listCounties_MouseLeave(object sender, System.EventArgs e){
			mouseIsInListCounties=false;
		}
		#endregion

		/*private void butChangeEmp_Click(object sender, System.EventArgs e) {
			FormEmployers FormE=new FormEmployers();
			FormE.IsSelectMode=true;
			Employers.Cur=Employers.GetEmployer(PatCur.EmployerNum);
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			PatCur.EmployerNum=Employers.Cur.EmployerNum;
			textEmployer.Text=Employers.Cur.EmpName;
		}*/

		private void textEmployer_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			//key up is used because that way it will trigger AFTER the textBox has been changed.
			if(e.KeyCode==Keys.Return){
				listEmps.Visible=false;
				textWirelessPhone.Focus();
				return;
			}
			if(textEmployer.Text==""){
				listEmps.Visible=false;
				return;
			}
			if(e.KeyCode==Keys.Down){
				if(listEmps.Items.Count==0){
					return;
				}
				if(listEmps.SelectedIndex==-1){
					listEmps.SelectedIndex=0;
					textEmployer.Text=listEmps.SelectedItem.ToString();
				}
				else if(listEmps.SelectedIndex==listEmps.Items.Count-1){
					listEmps.SelectedIndex=-1;
					textEmployer.Text=empOriginal;
				}
				else{
					listEmps.SelectedIndex++;
					textEmployer.Text=listEmps.SelectedItem.ToString();
				}
				textEmployer.SelectionStart=textEmployer.Text.Length;
				return;
			}
			if(e.KeyCode==Keys.Up){
				if(listEmps.Items.Count==0){
					return;
				}
				if(listEmps.SelectedIndex==-1){
					listEmps.SelectedIndex=listEmps.Items.Count-1;
					textEmployer.Text=listEmps.SelectedItem.ToString();
				}
				else if(listEmps.SelectedIndex==0){
					listEmps.SelectedIndex=-1;
					textEmployer.Text=empOriginal;
				}
				else{
					listEmps.SelectedIndex--;
					textEmployer.Text=listEmps.SelectedItem.ToString();
				}
				textEmployer.SelectionStart=textEmployer.Text.Length;
				return;
			}
			if(textEmployer.Text.Length==1){
				textEmployer.Text=textEmployer.Text.ToUpper();
				textEmployer.SelectionStart=1;
			}
			empOriginal=textEmployer.Text;//the original text is preserved when using up and down arrows
			listEmps.Items.Clear();
			similarEmps=Employers.GetSimilarNames(textEmployer.Text);
			for(int i=0;i<similarEmps.Count;i++){
				listEmps.Items.Add(((Employer)similarEmps[i]).EmpName);
			}
			int h=13*similarEmps.Count+5;
			if(h > ClientSize.Height-listEmps.Top)
				h=ClientSize.Height-listEmps.Top;
			listEmps.Size=new Size(231,h);
			listEmps.Visible=true;
		}

		private void textEmployer_Leave(object sender, System.EventArgs e) {
			if(mouseIsInListEmps){
				return;
			}
			listEmps.Visible=false;
		}

		private void listEmps_Click(object sender, System.EventArgs e){
			textEmployer.Text=listEmps.SelectedItem.ToString();
			textEmployer.Focus();
			textEmployer.SelectionStart=textEmployer.Text.Length;
			listEmps.Visible=false;
		}

		private void listEmps_DoubleClick(object sender, System.EventArgs e){
			//no longer used
			textEmployer.Text=listEmps.SelectedItem.ToString();
			textEmployer.Focus();
			textEmployer.SelectionStart=textEmployer.Text.Length;
			listEmps.Visible=false;
		}

		private void listEmps_MouseEnter(object sender, System.EventArgs e){
			mouseIsInListEmps=true;
		}

		private void listEmps_MouseLeave(object sender, System.EventArgs e){
			mouseIsInListEmps=false;
		}

		///<summary>Gets an employerNum based on the name entered. Called from FillCur</summary>
		private void GetEmployerNum(){
			if(PatCur.EmployerNum==0){//no employer was previously entered.
				if(textEmployer.Text==""){
					//no change
				}
				else{
					PatCur.EmployerNum=Employers.GetEmployerNum(textEmployer.Text);
				}
			}
			else{//an employer was previously entered
				if(textEmployer.Text==""){
					PatCur.EmployerNum=0;
				}
				//if text has changed
				else if(Employers.GetName(PatCur.EmployerNum)!=textEmployer.Text){
					PatCur.EmployerNum=Employers.GetEmployerNum(textEmployer.Text);
				}
			}
		}
		
		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textBirthdate.errorProvider1.GetError(textBirthdate)!=""
				|| textDateFirstVisit.errorProvider1.GetError(textDateFirstVisit)!=""
				|| textAdmitDate.errorProvider1.GetError(textAdmitDate)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(textLName.Text==""){
				MessageBox.Show(Lan.g(this,"Last Name must be entered."));
				return;
			}
			//see if chartNum is a duplicate
			if(textChartNumber.Text!=""){
				//the patNum will be 0 for new
				string usedBy=Patients.ChartNumUsedBy(textChartNumber.Text,PatCur.PatNum);
				if(usedBy!=""){
					MessageBox.Show(Lan.g(this,"This chart number is already in use by:")+" "+usedBy);
					return;
				}
			}
			if(textGradeSchool.Text != "" && !Schools.DoesExist(textGradeSchool.Text)){
				MessageBox.Show(Lan.g(this,"Grade school name invalid."));
				return;
			}
			if(textCounty.Text != "" && !Counties.DoesExist(textCounty.Text)){
				MessageBox.Show(Lan.g(this,"County name invalid."));
				return;
			}
			PatCur.LName=textLName.Text;
			PatCur.FName=textFName.Text;
			PatCur.MiddleI=textMiddleI.Text;
			PatCur.Preferred=textPreferred.Text;
			PatCur.Title=textTitle.Text;
			PatCur.Salutation=textSalutation.Text;
			switch(listStatus.SelectedIndex){
				case 0: PatCur.PatStatus=PatientStatus.Patient; break;
				case 1: PatCur.PatStatus=PatientStatus.NonPatient; break;
				case 2: PatCur.PatStatus=PatientStatus.Inactive; break;
				case 3: PatCur.PatStatus=PatientStatus.Archived; break;
				case 4: PatCur.PatStatus=PatientStatus.Deceased; break;
			}
			switch(listGender.SelectedIndex){
				case 0: PatCur.Gender=PatientGender.Male; break;
				case 1: PatCur.Gender=PatientGender.Female; break;
				case 2: PatCur.Gender=PatientGender.Unknown; break;
			}
			switch(listPosition.SelectedIndex){
				case 0: PatCur.Position=PatientPosition.Single; break;
				case 1: PatCur.Position=PatientPosition.Married; break;
				case 2: PatCur.Position=PatientPosition.Child; break;
				case 3: PatCur.Position=PatientPosition.Widowed; break;
				case 4: PatCur.Position=PatientPosition.Divorced; break;
			}
			PatCur.Birthdate=PIn.PDate(textBirthdate.Text);
			if(CultureInfo.CurrentCulture.Name=="en-US"){
				if(Regex.IsMatch(textSSN.Text,@"^\d\d\d-\d\d-\d\d\d\d$")){
					PatCur.SSN=textSSN.Text.Substring(0,3)+textSSN.Text.Substring(4,2)
						+textSSN.Text.Substring(7,4);
				}
				else{
					PatCur.SSN=textSSN.Text;
				}
			}
			else{//other cultures
				PatCur.SSN=textSSN.Text;
			}
			PatCur.MedicaidID=textMedicaidID.Text;
			PatCur.WkPhone=textWkPhone.Text;
			PatCur.WirelessPhone=textWirelessPhone.Text;
			PatCur.Email=textEmail.Text;
			//PatCur.RecallInterval=PIn.PInt(textRecall.Text);
			PatCur.ChartNumber=textChartNumber.Text;
			PatCur.SchoolName=textSchool.Text;
			//address:
			PatCur.HmPhone=textHmPhone.Text;
			PatCur.Address=textAddress.Text;
			PatCur.Address2=textAddress2.Text;
			PatCur.City=textCity.Text;
			PatCur.State=textState.Text;
			PatCur.Zip=textZip.Text;
			PatCur.CreditType=textCreditType.Text;
			GetEmployerNum();
			//PatCur.EmploymentNote=textEmploymentNote.Text;
			if(comboLanguage.SelectedIndex==0){
				PatCur.Language="";
			}
			else{
				PatCur.Language=languageList[comboLanguage.SelectedIndex-1].Name;
			}
			PatCur.AddrNote=textAddrNotes.Text;
			PatCur.DateFirstVisit=PIn.PDate(textDateFirstVisit.Text);
			if(comboPriProv.SelectedIndex==-1)
				PatCur.PriProv=0;
			else{
				PatCur.PriProv=Providers.List[comboPriProv.SelectedIndex].ProvNum;
			}
			if(comboSecProv.SelectedIndex==0)
				PatCur.SecProv=0;
			else
				PatCur.SecProv=Providers.List[comboSecProv.SelectedIndex-1].ProvNum;
			if(comboFeeSched.SelectedIndex==0)
				PatCur.FeeSched=0;
			else
				PatCur.FeeSched=DefB.Short[(int)DefCat.FeeSchedNames][comboFeeSched.SelectedIndex-1].DefNum;
			if(comboBillType.SelectedIndex!=-1)
				PatCur.BillingType=DefB.Short[(int)DefCat.BillingTypes][comboBillType.SelectedIndex].DefNum;
			if(comboClinic.SelectedIndex==0){
				PatCur.ClinicNum=0;
			}
			else{
				PatCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			}
			PatCur.Race=(PatientRace)comboRace.SelectedIndex;
			PatCur.County=textCounty.Text;
			PatCur.GradeSchool=textGradeSchool.Text;
			PatCur.GradeLevel=(PatientGrade)comboGradeLevel.SelectedIndex;
			PatCur.Urgency=(TreatmentUrgency)comboUrgency.SelectedIndex;
			if(Programs.IsEnabled("TrophyEnhanced")) {
				PatCur.TrophyFolder=textTrophyFolder.Text;
			}
			PatCur.Ward=textWard.Text;
			PatCur.PreferContactMethod=(ContactMethod)comboContact.SelectedIndex;
			PatCur.PreferConfirmMethod=(ContactMethod)comboConfirm.SelectedIndex;
			PatCur.PreferRecallMethod=(ContactMethod)comboRecall.SelectedIndex;
			PatCur.AdmitDate=PIn.PDate(textAdmitDate.Text);
			if(PatCur.Guarantor==0){
				PatCur.Guarantor=PatCur.PatNum;
			}
			Patients.Update(PatCur,PatOld);
			if(checkSame.Checked){
				//might want to include a mechanism for comparing fields to be overwritten
				Patients.UpdateAddressForFam(PatCur);
			}
			if(checkNotesSame.Checked){
				Patients.UpdateNotesForFam(PatCur);
			}
			//If this patient is also a referral source,
			//keep address info synched:
			for(int i=0;i<Referrals.List.Length;i++){
				if(Referrals.List[i].PatNum==PatCur.PatNum){
					//Referrals.Cur=Referrals.List[i];
					Referrals.List[i].LName=PatCur.LName;
					Referrals.List[i].FName=PatCur.FName;
					Referrals.List[i].MName=PatCur.MiddleI;
					Referrals.List[i].Address=PatCur.Address;
					Referrals.List[i].Address2=PatCur.Address2;
					Referrals.List[i].City=PatCur.City;
					Referrals.List[i].ST=PatCur.State;
					Referrals.List[i].SSN=PatCur.SSN;
					Referrals.List[i].Zip=PatCur.Zip;
					Referrals.List[i].Telephone=TelephoneNumbers.FormatNumbersExactTen(PatCur.HmPhone);
					Referrals.List[i].EMail=PatCur.Email;
					Referrals.Update(Referrals.List[i]);
					Referrals.Refresh();
					break;
				}
			}
			//if patient is inactive, then diable any recalls
			if(PatCur.PatStatus==PatientStatus.Archived
				|| PatCur.PatStatus==PatientStatus.Deceased
				|| PatCur.PatStatus==PatientStatus.Inactive
				|| PatCur.PatStatus==PatientStatus.NonPatient)
			{
				Recall[] recalls=Recalls.GetList(new int[] {PatCur.PatNum});
				for(int i=0;i<recalls.Length;i++){
					recalls[i].IsDisabled=true;
					recalls[i].DateDue=DateTime.MinValue;
					Recalls.Update(recalls[i]);
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormPatientEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK){
				return;
			}
			if(IsNew){
				for(int i=0;i<RefList.Length;i++){
					RefAttaches.Delete(RefList[i]);
				}
				Patients.Delete(PatCur);
			}
		}

		

		

		

		

		

		

		

		

		

		

		

		

		

		
	}
}









