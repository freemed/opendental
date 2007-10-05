/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
//#define TRIALONLY
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
///<summary>All this dialog does is set the patnum and it is up to the calling form to do an immediate refresh, or possibly just change the patnum back to what it was.  So the other patient fields must remain intact during all logic in this form, especially if SelectionModeOnly.</summary>
	public class FormPatientSelect : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;
		private Patients Patients;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butAddPt;
		/// <summary>Use when you want to specify a patient without changing the current patient.  If true, then the Add Patient button will not be visible.</summary>
		public bool SelectionModeOnly;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.TextBox textFName;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.TextBox textHmPhone;
		private System.Windows.Forms.Label labelMore;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox checkHideInactive;
		private System.Windows.Forms.GroupBox groupAddPt;
		private System.Windows.Forms.CheckBox checkGuarantors;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textPatNum;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textChartNumber;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textSSN;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ListBox listBillingTypes;
		private System.Windows.Forms.CheckBox checkUseSearch;
		private System.Windows.Forms.GroupBox groupBox1;
		private OpenDental.UI.Button butSearch;
		///<summary>When closing the form, this indicates whether a new patient was added from within this form.</summary>
		public bool NewPatientAdded;
		///<summary>Only used when double clicking blank area in Appts. Sets this value to the currently selected pt.  That patient will come up on the screen already selected and user just has to click OK. Or they can select a different pt or add a new pt.  If 0, then no initial patient is selected.</summary>
		public int InitialPatNum;
		private DataTable PtDataTable;
		private OpenDental.UI.ODGrid gridMain;
		private OpenDental.User_Controls.ContrKeyboard contrKeyboard1;
		///<summary>When closing the form, this will hold the value of the newly selected PatNum.</summary>
		public int SelectedPatNum;
		private CheckBox checkShowArchived;
		private TextBox selectedTxtBox;

		///<summary></summary>
		public FormPatientSelect(){
			InitializeComponent();//required first
			//tb2.CellClicked += new OpenDental.ContrTable.CellEventHandler(tb2_CellClicked);
			//tb2.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tb2_CellDoubleClicked);
			Patients=new Patients();
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if (components != null){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPatientSelect));
			this.textLName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupAddPt = new System.Windows.Forms.GroupBox();
			this.butAddPt = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkShowArchived = new System.Windows.Forms.CheckBox();
			this.textChartNumber = new System.Windows.Forms.TextBox();
			this.listBillingTypes = new System.Windows.Forms.ListBox();
			this.textSSN = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.textPatNum = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textState = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textCity = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.checkGuarantors = new System.Windows.Forms.CheckBox();
			this.checkHideInactive = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textHmPhone = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textFName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.labelMore = new System.Windows.Forms.Label();
			this.checkUseSearch = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butSearch = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.contrKeyboard1 = new OpenDental.User_Controls.ContrKeyboard();
			this.groupAddPt.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(135,38);
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(90,20);
			this.textLName.TabIndex = 0;
			this.textLName.Enter += new System.EventHandler(this.textBox_Enter);
			this.textLName.Leave += new System.EventHandler(this.textBox_Leave);
			this.textLName.TextChanged += new System.EventHandler(this.textLName_TextChanged);
			this.textLName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textLName_KeyDown);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(31,41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103,12);
			this.label1.TabIndex = 3;
			this.label1.Text = "Last Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupAddPt
			// 
			this.groupAddPt.Controls.Add(this.butAddPt);
			this.groupAddPt.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupAddPt.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.groupAddPt.Location = new System.Drawing.Point(699,557);
			this.groupAddPt.Name = "groupAddPt";
			this.groupAddPt.Size = new System.Drawing.Size(237,47);
			this.groupAddPt.TabIndex = 1;
			this.groupAddPt.TabStop = false;
			this.groupAddPt.Text = "Add New Family:";
			// 
			// butAddPt
			// 
			this.butAddPt.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddPt.Autosize = true;
			this.butAddPt.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddPt.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddPt.CornerRadius = 4F;
			this.butAddPt.Location = new System.Drawing.Point(68,18);
			this.butAddPt.Name = "butAddPt";
			this.butAddPt.Size = new System.Drawing.Size(75,23);
			this.butAddPt.TabIndex = 0;
			this.butAddPt.Text = "&Add";
			this.butAddPt.Click += new System.EventHandler(this.butAddPt_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(775,626);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76,26);
			this.butOK.TabIndex = 2;
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
			this.butCancel.Location = new System.Drawing.Point(857,626);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76,26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkShowArchived);
			this.groupBox2.Controls.Add(this.textChartNumber);
			this.groupBox2.Controls.Add(this.listBillingTypes);
			this.groupBox2.Controls.Add(this.textSSN);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.textPatNum);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.textState);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.textCity);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.checkGuarantors);
			this.groupBox2.Controls.Add(this.checkHideInactive);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.textAddress);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.textHmPhone);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.textFName);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.textLName);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(699,107);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(237,363);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Search by:";
			// 
			// checkShowArchived
			// 
			this.checkShowArchived.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowArchived.Location = new System.Drawing.Point(11,341);
			this.checkShowArchived.Name = "checkShowArchived";
			this.checkShowArchived.Size = new System.Drawing.Size(161,17);
			this.checkShowArchived.TabIndex = 25;
			this.checkShowArchived.Text = "Show Archived/Deceased";
			this.checkShowArchived.CheckedChanged += new System.EventHandler(this.checkShowArchived_CheckedChanged);
			// 
			// textChartNumber
			// 
			this.textChartNumber.Location = new System.Drawing.Point(135,198);
			this.textChartNumber.Name = "textChartNumber";
			this.textChartNumber.Size = new System.Drawing.Size(90,20);
			this.textChartNumber.TabIndex = 8;
			this.textChartNumber.Enter += new System.EventHandler(this.textBox_Enter);
			this.textChartNumber.TextChanged += new System.EventHandler(this.textChartNumber_TextChanged);
			this.textChartNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textChartNumber_KeyDown);
			// 
			// listBillingTypes
			// 
			this.listBillingTypes.Location = new System.Drawing.Point(86,220);
			this.listBillingTypes.Name = "listBillingTypes";
			this.listBillingTypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBillingTypes.Size = new System.Drawing.Size(138,56);
			this.listBillingTypes.TabIndex = 9;
			this.listBillingTypes.SelectedIndexChanged += new System.EventHandler(this.listBillingTypes_SelectedIndexChanged);
			// 
			// textSSN
			// 
			this.textSSN.Location = new System.Drawing.Point(135,158);
			this.textSSN.Name = "textSSN";
			this.textSSN.Size = new System.Drawing.Size(90,20);
			this.textSSN.TabIndex = 6;
			this.textSSN.Enter += new System.EventHandler(this.textBox_Enter);
			this.textSSN.TextChanged += new System.EventHandler(this.textSSN_TextChanged);
			this.textSSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textSSN_KeyDown);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(34,162);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(101,12);
			this.label12.TabIndex = 24;
			this.label12.Text = "SSN";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(30,226);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(52,46);
			this.label11.TabIndex = 21;
			this.label11.Text = "Billing Types";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(37,202);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(99,12);
			this.label10.TabIndex = 20;
			this.label10.Text = "Chart Number";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPatNum
			// 
			this.textPatNum.Location = new System.Drawing.Point(135,178);
			this.textPatNum.Name = "textPatNum";
			this.textPatNum.Size = new System.Drawing.Size(90,20);
			this.textPatNum.TabIndex = 7;
			this.textPatNum.Enter += new System.EventHandler(this.textBox_Enter);
			this.textPatNum.TextChanged += new System.EventHandler(this.textPatNum_TextChanged);
			this.textPatNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textPatNum_KeyDown);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(35,182);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(101,12);
			this.label9.TabIndex = 18;
			this.label9.Text = "Patient Number";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(135,138);
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(90,20);
			this.textState.TabIndex = 5;
			this.textState.Enter += new System.EventHandler(this.textBox_Enter);
			this.textState.TextChanged += new System.EventHandler(this.textState_TextChanged);
			this.textState.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textState_KeyDown);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(34,142);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100,12);
			this.label8.TabIndex = 16;
			this.label8.Text = "State";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(135,118);
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(90,20);
			this.textCity.TabIndex = 4;
			this.textCity.Enter += new System.EventHandler(this.textBox_Enter);
			this.textCity.TextChanged += new System.EventHandler(this.textCity_TextChanged);
			this.textCity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textCity_KeyDown);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(34,120);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(98,14);
			this.label7.TabIndex = 14;
			this.label7.Text = "City";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkGuarantors
			// 
			this.checkGuarantors.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkGuarantors.Location = new System.Drawing.Point(11,305);
			this.checkGuarantors.Name = "checkGuarantors";
			this.checkGuarantors.Size = new System.Drawing.Size(163,17);
			this.checkGuarantors.TabIndex = 10;
			this.checkGuarantors.Text = "Guarantors Only";
			this.checkGuarantors.CheckedChanged += new System.EventHandler(this.checkGuarantors_CheckedChanged);
			// 
			// checkHideInactive
			// 
			this.checkHideInactive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHideInactive.Location = new System.Drawing.Point(11,323);
			this.checkHideInactive.Name = "checkHideInactive";
			this.checkHideInactive.Size = new System.Drawing.Size(161,17);
			this.checkHideInactive.TabIndex = 11;
			this.checkHideInactive.Text = "Hide Inactive Patients";
			this.checkHideInactive.CheckedChanged += new System.EventHandler(this.checkHideInactive_CheckedChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4,18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(200,16);
			this.label6.TabIndex = 10;
			this.label6.Text = "Hint: enter values in multiple boxes.";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(135,98);
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(90,20);
			this.textAddress.TabIndex = 3;
			this.textAddress.Enter += new System.EventHandler(this.textBox_Enter);
			this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
			this.textAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAddress_KeyDown);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(34,101);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,12);
			this.label5.TabIndex = 9;
			this.label5.Text = "Address";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textHmPhone
			// 
			this.textHmPhone.Location = new System.Drawing.Point(135,78);
			this.textHmPhone.Name = "textHmPhone";
			this.textHmPhone.Size = new System.Drawing.Size(90,20);
			this.textHmPhone.TabIndex = 2;
			this.textHmPhone.Enter += new System.EventHandler(this.textBox_Enter);
			this.textHmPhone.TextChanged += new System.EventHandler(this.textHmPhone_TextChanged);
			this.textHmPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textHmPhone_KeyDown);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6,80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(129,16);
			this.label4.TabIndex = 7;
			this.label4.Text = "Phone (any)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(135,58);
			this.textFName.Name = "textFName";
			this.textFName.Size = new System.Drawing.Size(90,20);
			this.textFName.TabIndex = 1;
			this.textFName.Enter += new System.EventHandler(this.textBox_Enter);
			this.textFName.TextChanged += new System.EventHandler(this.textFName_TextChanged);
			this.textFName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textFName_KeyDown);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(34,62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,12);
			this.label3.TabIndex = 5;
			this.label3.Text = "First Name";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelMore
			// 
			this.labelMore.Location = new System.Drawing.Point(696,646);
			this.labelMore.Name = "labelMore";
			this.labelMore.Size = new System.Drawing.Size(89,16);
			this.labelMore.TabIndex = 5;
			this.labelMore.Text = "(more)";
			this.labelMore.Visible = false;
			// 
			// checkUseSearch
			// 
			this.checkUseSearch.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkUseSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkUseSearch.Location = new System.Drawing.Point(13,44);
			this.checkUseSearch.Name = "checkUseSearch";
			this.checkUseSearch.Size = new System.Drawing.Size(184,29);
			this.checkUseSearch.TabIndex = 6;
			this.checkUseSearch.Text = "Use Search Button (may take longer to load)";
			this.checkUseSearch.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkUseSearch.Click += new System.EventHandler(this.checkUseSearch_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butSearch);
			this.groupBox1.Controls.Add(this.checkUseSearch);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(699,476);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(237,76);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Search behavior";
			// 
			// butSearch
			// 
			this.butSearch.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSearch.Autosize = true;
			this.butSearch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearch.CornerRadius = 4F;
			this.butSearch.Location = new System.Drawing.Point(68,17);
			this.butSearch.Name = "butSearch";
			this.butSearch.Size = new System.Drawing.Size(75,23);
			this.butSearch.TabIndex = 7;
			this.butSearch.Text = "&Search";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = true;
			this.gridMain.Location = new System.Drawing.Point(3,2);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(665,659);
			this.gridMain.TabIndex = 9;
			this.gridMain.Title = "Select Patient";
			this.gridMain.TranslationName = "FormPatientSelect";
			this.gridMain.WrapText = false;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			this.gridMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridMain_KeyDown);
			// 
			// contrKeyboard1
			// 
			this.contrKeyboard1.Location = new System.Drawing.Point(669,0);
			this.contrKeyboard1.Name = "contrKeyboard1";
			this.contrKeyboard1.Size = new System.Drawing.Size(275,100);
			this.contrKeyboard1.TabIndex = 10;
			this.contrKeyboard1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.contrKeyboard1_MouseDown);
			this.contrKeyboard1.KeyClick += new OpenDental.User_Controls.KeyboardClickEventHandler(this.contrKeyboard1_KeyClick);
			// 
			// FormPatientSelect
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(944,668);
			this.Controls.Add(this.contrKeyboard1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.labelMore);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.groupAddPt);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPatientSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Patient";
			this.Load += new System.EventHandler(this.FormSelectPatient_Load);
			this.groupAddPt.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

        private Patient preselectedPatient;
        public Patient PreselectedPatient {
            get { return preselectedPatient; }
        }

        public void PreselectPatient(Patient value) {
            preselectedPatient = value;
            textLName.Text = value.LName;
            textFName.Text = value.FName;
            textCity.Text = value.City;
            butSearch_Click(this, EventArgs.Empty);
        }

		///<summary></summary>
		public void FormSelectPatient_Load(object sender, System.EventArgs e){
			if(SelectionModeOnly){
				groupAddPt.Visible=false;
			}
			for(int i=0;i<DefB.Short[(int)DefCat.BillingTypes].Length;i++){
				listBillingTypes.Items.Add(DefB.Short[(int)DefCat.BillingTypes][i].ItemName);
			}
			FillSearchOption();
			SetGridCols();
			if(InitialPatNum!=0){
				Patient iPatient=Patients.GetLim(InitialPatNum);
				textLName.Text=iPatient.LName;
				FillGrid();
				/*if(grid2.CurrentRowIndex>-1){
					grid2.UnSelect(grid2.CurrentRowIndex);
				}
				for(int i=0;i<PtDataTable.Rows.Count;i++){
					if(PIn.PInt(PtDataTable.Rows[i][0].ToString())==InitialPatNum){
						grid2.CurrentRowIndex=i;
						grid2.Select(i);
						break;
					}
				}*/
				gridMain.SetSelected(false);
				for(int i=0;i<PtDataTable.Rows.Count;i++){
					if(PIn.PInt(PtDataTable.Rows[i][0].ToString())==InitialPatNum) {
						gridMain.SetSelected(i,true);
						break;
					}
				}
				return;
			}
			if(!checkUseSearch.Checked){
				FillGrid();
			}
		}

		private void FillSearchOption(){
			checkUseSearch.Checked=PrefB.GetBool("PatientSelectUsesSearchButton");
			if(checkUseSearch.Checked)
				butSearch.Enabled=true;
			else
				butSearch.Enabled=false;
		}

		private void SetGridCols(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("LastName",75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("First Name",75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("MI",25);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Pref Name",60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Age",30);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("SSN",65);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Hm Phone",90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Wk Phone",90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("PatNum",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("ChartNum",60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Address",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Status",65);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Bill Type",90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("City",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("State",55);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Pri Prov",85);
			gridMain.Columns.Add(col);
			gridMain.EndUpdate();
		}

		private void textBox_Enter(object sender,EventArgs e) {
			selectedTxtBox=(TextBox)sender;
		}

		private void textBox_Leave(object sender,EventArgs e) {
			//selectedTxtBox=null;
		}

		private void contrKeyboard1_MouseDown(object sender,MouseEventArgs e) {
			//this happens before contrKeyboard gets focus
			/*foreach(Control control in this.Controls) {
				if(control.){
					if(control.GetType()==typeof(TextBox)) {
						selectedTxtBox=(TextBox)control;
					}
				}
			}*/
		}

		private void contrKeyboard1_KeyClick(object sender,OpenDental.User_Controls.KeyboardClickEventArgs e) {
			//MessageBox.Show(contrKeyboard1.CanFocus.ToString());
			//get the control with focus
			/*Control ctrl=null;
			foreach(Control control in this.Controls) {
				if(control.Focused) {
					ctrl=control;
				}
			}*/
			if(selectedTxtBox==null) {
				return;
			}
			//if(ctrl.GetType()!=typeof(TextBox)) {
			//	return;
			//}
			//this is all quick and dirty, totally ignoring the cursor position
			if(e.KeyData==Keys.Back) {
				if(selectedTxtBox.Text.Length>0) {
					selectedTxtBox.Text=selectedTxtBox.Text.Remove(selectedTxtBox.Text.Length-1);
				}
			}
			else {
				if(selectedTxtBox.Text.Length==0){
					selectedTxtBox.Text=selectedTxtBox.Text+e.Txt;
				}
				else{
					selectedTxtBox.Text=selectedTxtBox.Text+e.Txt.ToLower();
				}
			}
			selectedTxtBox.Focus();
			selectedTxtBox.Select(selectedTxtBox.Text.Length,0);//the end
		}

		private void textLName_TextChanged(object sender, System.EventArgs e){
			OnDataEntered();
		}

		private void textFName_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textHmPhone_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textWkPhone_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textAddress_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textCity_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textState_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textSSN_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textPatNum_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textChartNumber_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void listBillingTypes_SelectedIndexChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void checkGuarantors_CheckedChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void checkHideInactive_CheckedChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void checkShowArchived_CheckedChanged(object sender,EventArgs e) {
			OnDataEntered();
		}

		private void checkUseSearch_Click(object sender, System.EventArgs e) {
			Prefs.UpdateBool("PatientSelectUsesSearchButton",checkUseSearch.Checked);
			Prefs.Refresh();
			//simply not important enough to send an update to the other computers.
			FillSearchOption();
			if(!checkUseSearch.Checked)
				FillGrid();
		}

		private void butSearch_Click(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void OnDataEntered(){
			if(!checkUseSearch.Checked){
				FillGrid();
			}
		}

		private void FillGrid(){
			int[] selectedBillingTypes=new int[listBillingTypes.SelectedIndices.Count];
			for(int i=0;i<selectedBillingTypes.Length;i++){
				selectedBillingTypes[i]
					=DefB.Short[(int)DefCat.BillingTypes][listBillingTypes.SelectedIndices[i]].DefNum;
			}
			PtDataTable=Patients.GetPtDataTable(!checkUseSearch.Checked,textLName.Text,textFName.Text,textHmPhone.Text,
				textAddress.Text,checkHideInactive.Checked,textCity.Text,textState.Text,
				textSSN.Text,textPatNum.Text,textChartNumber.Text,selectedBillingTypes,
				checkGuarantors.Checked,checkShowArchived.Checked,Security.CurUser.ClinicNum);
			if(!checkUseSearch.Checked && PtDataTable.Rows.Count==36){
				//if limit and there are more rows
				labelMore.Visible=true;
			}
			else{
				labelMore.Visible=false;
			}
			//grid2.SetDataBinding(PtDataTable,"");
			//if(PtDataTable.Rows.Count>grid2.CurrentCell.RowNumber){
			//	grid2.Select(grid2.CurrentCell.RowNumber);
			//}
			//int selectedRow=gridMain.GetSelectedIndex();
			gridMain.BeginUpdate();
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<PtDataTable.Rows.Count;i++){
				//Order in PtDataTable: PatNum,LName,FName,MiddleI,Preferred,Birthdate,SSN,HmPhone,WkPhone 0-8
				//,Address,PatStatus,BillingType,ChartNumber,City,State,PriProv 9-15
				row=new ODGridRow();				
				row.Cells.Add(PtDataTable.Rows[i][1].ToString());//LastName				
				row.Cells.Add(PtDataTable.Rows[i][2].ToString());//First Name			
				row.Cells.Add(PtDataTable.Rows[i][3].ToString());//MI
				row.Cells.Add(PtDataTable.Rows[i][4].ToString());//Pref Name			
				row.Cells.Add(PtDataTable.Rows[i][5].ToString());//Age
				row.Cells.Add(PtDataTable.Rows[i][6].ToString());//SSN
				row.Cells.Add(PtDataTable.Rows[i][7].ToString());//Hm Phone
				row.Cells.Add(PtDataTable.Rows[i][8].ToString());//Wk Phone
				row.Cells.Add(PtDataTable.Rows[i][0].ToString());//PatNum
				row.Cells.Add(PtDataTable.Rows[i][12].ToString());//ChartNum
				row.Cells.Add(PtDataTable.Rows[i][9].ToString());//Address
				row.Cells.Add(PtDataTable.Rows[i][10].ToString());//Status
				row.Cells.Add(PtDataTable.Rows[i][11].ToString());//Bill Type
				row.Cells.Add(PtDataTable.Rows[i][13].ToString());//City
				row.Cells.Add(PtDataTable.Rows[i][14].ToString());//State
				row.Cells.Add(PtDataTable.Rows[i][15].ToString());//PriProv
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.SetSelected(0,true);
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			PatSelected();
		}
		private void OnArrowsUpDown(System.Windows.Forms.KeyEventArgs e){
			if (e.KeyCode == Keys.Up){
				gridMain_KeyDown(this,e);
				//gridMain.KeyDown;
			}
		}
		private void FormSelectPatient_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			OnArrowsUpDown(e);
		}

		///<summary>Remember, this button is not even visible if SelectionModeOnly.</summary>
		private void butAddPt_Click(object sender, System.EventArgs e){
			#if(TRIALONLY)
				MsgBox.Show(this,"Trial version.  Maximum 30 patients");
				if(Patients.GetNumberPatients()>30){
					MsgBox.Show(this,"Maximum reached");
					return;
				}
			#endif
			if(textLName.Text=="" && textFName.Text=="" && textChartNumber.Text==""){
				MessageBox.Show(Lan.g(this,"Not allowed to add a new patient until you have done a search to see if that patient already exists. Hint: just type a few letters into the Last Name box above.")); 
				return;
			}
			Patient PatCur=new Patient();
			if(textLName.Text.Length>1){//eg Sp
				PatCur.LName=textLName.Text.Substring(0,1).ToUpper()+textLName.Text.Substring(1);
			}
			if(textFName.Text.Length>1){
				PatCur.FName=textFName.Text.Substring(0,1).ToUpper()+textFName.Text.Substring(1);
			}
			PatCur.PatStatus=PatientStatus.Patient;
			Patients.Insert(PatCur,false);
			Patient PatOld=PatCur.Copy();
			PatCur.Guarantor=PatCur.PatNum;
			Patients.Update(PatCur,PatOld);
			Family FamCur=Patients.GetFamily(PatCur.PatNum);
			FormPatientEdit FormPE=new FormPatientEdit(PatCur,FamCur);
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.OK){
				NewPatientAdded=true;
				SelectedPatNum=PatCur.PatNum;
				DialogResult=DialogResult.OK;
			}
		}

		private void PatSelected(){
			//SelectedPatNum=PIn.PInt(PtDataTable.Rows[grid2.CurrentRowIndex][0].ToString());
			SelectedPatNum=PIn.PInt(PtDataTable.Rows[gridMain.GetSelectedIndex()][0].ToString());
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e){
			if(gridMain.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select a patient first.");
				return;
			}
			//if(grid2.CurrentRowIndex!=-1){
			PatSelected();
			//}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


		private void textLName_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up | e.KeyCode==Keys.Down) 
				gridMain_KeyDown(sender, e);
				gridMain.Refresh();
		}

		private void textFName_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up | e.KeyCode==Keys.Down) 
				gridMain_KeyDown(sender, e);
				gridMain.Refresh();
		}

		private void gridMain_KeyDown(object sender,KeyEventArgs e) {
		}


		private void textHmPhone_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up | e.KeyCode==Keys.Down) 
				gridMain_KeyDown(sender, e);
				gridMain.Refresh();
		}

		private void textAddress_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up | e.KeyCode==Keys.Down) 
				gridMain_KeyDown(sender, e);
				gridMain.Refresh();
		}

		private void textCity_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up | e.KeyCode==Keys.Down) 
				gridMain_KeyDown(sender, e);
				gridMain.Refresh();
		}

		private void textState_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up | e.KeyCode==Keys.Down) 
				gridMain_KeyDown(sender, e);
				gridMain.Refresh();
		}

		private void textSSN_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up | e.KeyCode==Keys.Down) 
				gridMain_KeyDown(sender, e);
				gridMain.Refresh();
		}

		private void textPatNum_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up | e.KeyCode==Keys.Down) 
				gridMain_KeyDown(sender, e);
				gridMain.Refresh();
		}

		private void textChartNumber_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up | e.KeyCode==Keys.Down) 
				gridMain_KeyDown(sender, e);
				gridMain.Refresh();
		}
	}
}
