using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormProvEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label labelColor;
		private System.Windows.Forms.Button butColor;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox checkIsSecondary;
		private System.Windows.Forms.ListBox listFeeSched;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textAbbr;
		private System.Windows.Forms.TextBox textStateLicense;
		private System.Windows.Forms.TextBox textSSN;
		private System.Windows.Forms.TextBox textMI;
		private System.Windows.Forms.TextBox textFName;
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.TextBox textDEANum;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private System.Windows.Forms.CheckBox checkIsHidden;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.ListBox listSpecialty;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioSSN;
		private System.Windows.Forms.RadioButton radioTIN;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textMedicaidID;
		private System.Windows.Forms.GroupBox groupBox2;
		private OpenDental.TableProvIdent tbProvIdent;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.CheckBox checkSigOnFile;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Button butOutlineColor;
		private System.Windows.Forms.TextBox textSuffix;
		private System.Windows.Forms.ComboBox comboSchoolClass;
		private System.Windows.Forms.Label labelSchoolClass;
		///<summary>Provider Identifiers showing in the list for this provider.</summary>
		private ProviderIdent[] ListProvIdent;
		private System.Windows.Forms.Label labelNPI;
		private System.Windows.Forms.TextBox textNationalProvID;
		private TextBox textCanadianOfficeNum;
		private Label labelCanadianOfficeNum;
		public Provider ProvCur;

		///<summary></summary>
		public FormProvEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.F(this);
			//ProvCur=provCur;
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				labelNPI.Text=Lan.g(this,"CDA Number");
			}
			else{
				labelCanadianOfficeNum.Visible=false;
				textCanadianOfficeNum.Visible=false;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProvEdit));
			this.checkIsHidden = new System.Windows.Forms.CheckBox();
			this.labelColor = new System.Windows.Forms.Label();
			this.butColor = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.checkIsSecondary = new System.Windows.Forms.CheckBox();
			this.listFeeSched = new System.Windows.Forms.ListBox();
			this.listSpecialty = new System.Windows.Forms.ListBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textAbbr = new System.Windows.Forms.TextBox();
			this.textStateLicense = new System.Windows.Forms.TextBox();
			this.textSSN = new System.Windows.Forms.TextBox();
			this.textSuffix = new System.Windows.Forms.TextBox();
			this.textMI = new System.Windows.Forms.TextBox();
			this.textFName = new System.Windows.Forms.TextBox();
			this.textLName = new System.Windows.Forms.TextBox();
			this.textDEANum = new System.Windows.Forms.TextBox();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioTIN = new System.Windows.Forms.RadioButton();
			this.radioSSN = new System.Windows.Forms.RadioButton();
			this.checkSigOnFile = new System.Windows.Forms.CheckBox();
			this.textMedicaidID = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.tbProvIdent = new OpenDental.TableProvIdent();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butAdd = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.butOutlineColor = new System.Windows.Forms.Button();
			this.comboSchoolClass = new System.Windows.Forms.ComboBox();
			this.labelSchoolClass = new System.Windows.Forms.Label();
			this.textNationalProvID = new System.Windows.Forms.TextBox();
			this.labelNPI = new System.Windows.Forms.Label();
			this.textCanadianOfficeNum = new System.Windows.Forms.TextBox();
			this.labelCanadianOfficeNum = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkIsHidden
			// 
			this.checkIsHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsHidden.Location = new System.Drawing.Point(136,360);
			this.checkIsHidden.Name = "checkIsHidden";
			this.checkIsHidden.Size = new System.Drawing.Size(158,24);
			this.checkIsHidden.TabIndex = 12;
			this.checkIsHidden.Text = "Hidden";
			// 
			// labelColor
			// 
			this.labelColor.Location = new System.Drawing.Point(6,387);
			this.labelColor.Name = "labelColor";
			this.labelColor.Size = new System.Drawing.Size(129,16);
			this.labelColor.TabIndex = 10;
			this.labelColor.Text = "Appointment Color";
			this.labelColor.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butColor
			// 
			this.butColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butColor.Location = new System.Drawing.Point(136,384);
			this.butColor.Name = "butColor";
			this.butColor.Size = new System.Drawing.Size(30,20);
			this.butColor.TabIndex = 13;
			this.butColor.Click += new System.EventHandler(this.butColor_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10,12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(125,14);
			this.label1.TabIndex = 12;
			this.label1.Text = "Abbreviation";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(-1,212);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(137,14);
			this.label3.TabIndex = 14;
			this.label3.Text = "State License Number";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(526,14);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(116,14);
			this.label5.TabIndex = 16;
			this.label5.Text = "Specialty";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(369,14);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(105,14);
			this.label6.TabIndex = 17;
			this.label6.Text = "Fee Schedule";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(28,81);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(102,14);
			this.label7.TabIndex = 18;
			this.label7.Text = "MI";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8,58);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(127,14);
			this.label8.TabIndex = 19;
			this.label8.Text = "First Name";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(5,104);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(130,14);
			this.label9.TabIndex = 20;
			this.label9.Text = "Suffix (DMD,DDS)";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(2,35);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(132,14);
			this.label10.TabIndex = 21;
			this.label10.Text = "Last Name";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8,235);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(126,14);
			this.label11.TabIndex = 22;
			this.label11.Text = "DEA Number";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkIsSecondary
			// 
			this.checkIsSecondary.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsSecondary.Location = new System.Drawing.Point(136,325);
			this.checkIsSecondary.Name = "checkIsSecondary";
			this.checkIsSecondary.Size = new System.Drawing.Size(155,17);
			this.checkIsSecondary.TabIndex = 10;
			this.checkIsSecondary.Text = "Secondary Provider (Hyg)";
			// 
			// listFeeSched
			// 
			this.listFeeSched.Location = new System.Drawing.Point(371,31);
			this.listFeeSched.Name = "listFeeSched";
			this.listFeeSched.Size = new System.Drawing.Size(108,173);
			this.listFeeSched.TabIndex = 13;
			// 
			// listSpecialty
			// 
			this.listSpecialty.Items.AddRange(new object[] {
            "Dental General Practice",
            "Dental Hygienist",
            "Endodontics",
            "Pediatric Dentistry",
            "Periodontics",
            "Prosthodontics",
            "Orthodontics",
            "Denturist",
            "Surgery, Oral & Maxillofacial",
            "Dental Assistant",
            "Dental Laboratory Technician",
            "Pathology, Oral & MaxFac",
            "Public Health",
            "Radiology"});
			this.listSpecialty.Location = new System.Drawing.Point(526,31);
			this.listSpecialty.Name = "listSpecialty";
			this.listSpecialty.Size = new System.Drawing.Size(154,186);
			this.listSpecialty.TabIndex = 17;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(742,580);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 15;
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
			this.butCancel.Location = new System.Drawing.Point(742,613);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 16;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textAbbr
			// 
			this.textAbbr.Location = new System.Drawing.Point(136,8);
			this.textAbbr.MaxLength = 5;
			this.textAbbr.Name = "textAbbr";
			this.textAbbr.Size = new System.Drawing.Size(58,20);
			this.textAbbr.TabIndex = 0;
			// 
			// textStateLicense
			// 
			this.textStateLicense.Location = new System.Drawing.Point(136,208);
			this.textStateLicense.MaxLength = 15;
			this.textStateLicense.Name = "textStateLicense";
			this.textStateLicense.Size = new System.Drawing.Size(100,20);
			this.textStateLicense.TabIndex = 5;
			// 
			// textSSN
			// 
			this.textSSN.Location = new System.Drawing.Point(8,52);
			this.textSSN.Name = "textSSN";
			this.textSSN.Size = new System.Drawing.Size(100,20);
			this.textSSN.TabIndex = 2;
			// 
			// textSuffix
			// 
			this.textSuffix.Location = new System.Drawing.Point(136,100);
			this.textSuffix.MaxLength = 100;
			this.textSuffix.Name = "textSuffix";
			this.textSuffix.Size = new System.Drawing.Size(104,20);
			this.textSuffix.TabIndex = 4;
			// 
			// textMI
			// 
			this.textMI.Location = new System.Drawing.Point(136,77);
			this.textMI.MaxLength = 100;
			this.textMI.Name = "textMI";
			this.textMI.Size = new System.Drawing.Size(63,20);
			this.textMI.TabIndex = 3;
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(136,54);
			this.textFName.MaxLength = 100;
			this.textFName.Name = "textFName";
			this.textFName.Size = new System.Drawing.Size(161,20);
			this.textFName.TabIndex = 2;
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(136,31);
			this.textLName.MaxLength = 100;
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(161,20);
			this.textLName.TabIndex = 1;
			// 
			// textDEANum
			// 
			this.textDEANum.Location = new System.Drawing.Point(136,231);
			this.textDEANum.MaxLength = 15;
			this.textDEANum.Name = "textDEANum";
			this.textDEANum.Size = new System.Drawing.Size(100,20);
			this.textDEANum.TabIndex = 6;
			// 
			// colorDialog1
			// 
			this.colorDialog1.FullOpen = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioTIN);
			this.groupBox1.Controls.Add(this.radioSSN);
			this.groupBox1.Controls.Add(this.textSSN);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(128,125);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(156,80);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "SSN or TIN (no dashes)";
			// 
			// radioTIN
			// 
			this.radioTIN.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioTIN.Location = new System.Drawing.Point(9,34);
			this.radioTIN.Name = "radioTIN";
			this.radioTIN.Size = new System.Drawing.Size(135,15);
			this.radioTIN.TabIndex = 1;
			this.radioTIN.Text = "TIN";
			this.radioTIN.Click += new System.EventHandler(this.radioTIN_Click);
			// 
			// radioSSN
			// 
			this.radioSSN.Checked = true;
			this.radioSSN.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioSSN.Location = new System.Drawing.Point(9,17);
			this.radioSSN.Name = "radioSSN";
			this.radioSSN.Size = new System.Drawing.Size(104,14);
			this.radioSSN.TabIndex = 0;
			this.radioSSN.TabStop = true;
			this.radioSSN.Text = "SSN";
			this.radioSSN.Click += new System.EventHandler(this.radioSSN_Click);
			// 
			// checkSigOnFile
			// 
			this.checkSigOnFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSigOnFile.Location = new System.Drawing.Point(136,343);
			this.checkSigOnFile.Name = "checkSigOnFile";
			this.checkSigOnFile.Size = new System.Drawing.Size(121,20);
			this.checkSigOnFile.TabIndex = 11;
			this.checkSigOnFile.Text = "Signature on File";
			// 
			// textMedicaidID
			// 
			this.textMedicaidID.Location = new System.Drawing.Point(136,254);
			this.textMedicaidID.MaxLength = 20;
			this.textMedicaidID.Name = "textMedicaidID";
			this.textMedicaidID.Size = new System.Drawing.Size(100,20);
			this.textMedicaidID.TabIndex = 7;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(5,258);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(130,14);
			this.label13.TabIndex = 42;
			this.label13.Text = "Medicaid ID";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tbProvIdent
			// 
			this.tbProvIdent.BackColor = System.Drawing.SystemColors.Window;
			this.tbProvIdent.Location = new System.Drawing.Point(7,58);
			this.tbProvIdent.Name = "tbProvIdent";
			this.tbProvIdent.ScrollValue = 211;
			this.tbProvIdent.SelectedIndices = new int[0];
			this.tbProvIdent.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbProvIdent.Size = new System.Drawing.Size(319,88);
			this.tbProvIdent.TabIndex = 43;
			this.tbProvIdent.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.tbProvIdent_CellDoubleClicked);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.butAdd);
			this.groupBox2.Controls.Add(this.butDelete);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.tbProvIdent);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(128,481);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(496,157);
			this.groupBox2.TabIndex = 16;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Supplemental Provider Identifiers";
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(358,59);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(90,26);
			this.butAdd.TabIndex = 46;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(358,94);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(90,26);
			this.butDelete.TabIndex = 45;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10,20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(481,32);
			this.label2.TabIndex = 44;
			this.label2.Text = "This is where you store provider ID\'s assigned by individual insurance companies," +
    " especially BC/BS.";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(6,412);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(129,16);
			this.label14.TabIndex = 45;
			this.label14.Text = "Highlight Outline Color";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butOutlineColor
			// 
			this.butOutlineColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butOutlineColor.Location = new System.Drawing.Point(136,409);
			this.butOutlineColor.Name = "butOutlineColor";
			this.butOutlineColor.Size = new System.Drawing.Size(30,20);
			this.butOutlineColor.TabIndex = 14;
			this.butOutlineColor.Click += new System.EventHandler(this.butOutlineColor_Click);
			// 
			// comboSchoolClass
			// 
			this.comboSchoolClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSchoolClass.Location = new System.Drawing.Point(136,437);
			this.comboSchoolClass.MaxDropDownItems = 30;
			this.comboSchoolClass.Name = "comboSchoolClass";
			this.comboSchoolClass.Size = new System.Drawing.Size(130,21);
			this.comboSchoolClass.TabIndex = 15;
			// 
			// labelSchoolClass
			// 
			this.labelSchoolClass.Location = new System.Drawing.Point(9,440);
			this.labelSchoolClass.Name = "labelSchoolClass";
			this.labelSchoolClass.Size = new System.Drawing.Size(125,16);
			this.labelSchoolClass.TabIndex = 89;
			this.labelSchoolClass.Text = "Dental School Class";
			this.labelSchoolClass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textNationalProvID
			// 
			this.textNationalProvID.Location = new System.Drawing.Point(136,276);
			this.textNationalProvID.MaxLength = 20;
			this.textNationalProvID.Name = "textNationalProvID";
			this.textNationalProvID.Size = new System.Drawing.Size(100,20);
			this.textNationalProvID.TabIndex = 8;
			// 
			// labelNPI
			// 
			this.labelNPI.Location = new System.Drawing.Point(5,280);
			this.labelNPI.Name = "labelNPI";
			this.labelNPI.Size = new System.Drawing.Size(130,14);
			this.labelNPI.TabIndex = 92;
			this.labelNPI.Text = "National Provider ID";
			this.labelNPI.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCanadianOfficeNum
			// 
			this.textCanadianOfficeNum.Location = new System.Drawing.Point(136,298);
			this.textCanadianOfficeNum.MaxLength = 20;
			this.textCanadianOfficeNum.Name = "textCanadianOfficeNum";
			this.textCanadianOfficeNum.Size = new System.Drawing.Size(100,20);
			this.textCanadianOfficeNum.TabIndex = 9;
			this.textCanadianOfficeNum.Validating += new System.ComponentModel.CancelEventHandler(this.textCanadianOfficeNum_Validating);
			// 
			// labelCanadianOfficeNum
			// 
			this.labelCanadianOfficeNum.Location = new System.Drawing.Point(5,302);
			this.labelCanadianOfficeNum.Name = "labelCanadianOfficeNum";
			this.labelCanadianOfficeNum.Size = new System.Drawing.Size(130,14);
			this.labelCanadianOfficeNum.TabIndex = 94;
			this.labelCanadianOfficeNum.Text = "Office Number";
			this.labelCanadianOfficeNum.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormProvEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(849,668);
			this.Controls.Add(this.textCanadianOfficeNum);
			this.Controls.Add(this.labelCanadianOfficeNum);
			this.Controls.Add(this.textNationalProvID);
			this.Controls.Add(this.labelNPI);
			this.Controls.Add(this.comboSchoolClass);
			this.Controls.Add(this.labelSchoolClass);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.butOutlineColor);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.textMedicaidID);
			this.Controls.Add(this.textDEANum);
			this.Controls.Add(this.textLName);
			this.Controls.Add(this.textFName);
			this.Controls.Add(this.textMI);
			this.Controls.Add(this.textSuffix);
			this.Controls.Add(this.textStateLicense);
			this.Controls.Add(this.textAbbr);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.checkSigOnFile);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listSpecialty);
			this.Controls.Add(this.listFeeSched);
			this.Controls.Add(this.checkIsSecondary);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkIsHidden);
			this.Controls.Add(this.labelColor);
			this.Controls.Add(this.butColor);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProvEdit";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Provider";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProvEdit_Closing);
			this.Load += new System.EventHandler(this.FormProvEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormProvEdit_Load(object sender, System.EventArgs e) {
			//if(IsNew){
			//	Providers.Cur.SigOnFile=true;
			//	Providers.InsertCur();
				//one field handled from previous form
			//}
			if(PrefB.GetBool("EasyHideDentalSchools")){
				labelSchoolClass.Visible=false;
				comboSchoolClass.Visible=false;
			}
			textAbbr.Text=ProvCur.Abbr;
			textLName.Text=ProvCur.LName;
			textFName.Text=ProvCur.FName;
			textMI.Text=ProvCur.MI;
			textSuffix.Text=ProvCur.Suffix;
			textSSN.Text=ProvCur.SSN;
			if(ProvCur.UsingTIN){
				radioTIN.Checked=true;
			}
			else {
				radioSSN.Checked=true;
			}
			textStateLicense.Text=ProvCur.StateLicense;
			textDEANum.Text=ProvCur.DEANum;
			//textBlueCrossID.Text=ProvCur.BlueCrossID;
			textMedicaidID.Text=ProvCur.MedicaidID;
			textNationalProvID.Text=ProvCur.NationalProvID;
			textCanadianOfficeNum.Text=ProvCur.CanadianOfficeNum;
			checkIsSecondary.Checked=ProvCur.IsSecondary;
			checkSigOnFile.Checked=ProvCur.SigOnFile;
			checkIsHidden.Checked=ProvCur.IsHidden;
			butColor.BackColor=ProvCur.ProvColor;
			butOutlineColor.BackColor=ProvCur.OutlineColor;
			comboSchoolClass.Items.Add(Lan.g(this,"none"));
			comboSchoolClass.SelectedIndex=0;
			for(int i=0;i<SchoolClasses.List.Length;i++){
				comboSchoolClass.Items.Add(SchoolClasses.List[i].GradYear.ToString()+"-"+SchoolClasses.List[i].Descript);
				if(SchoolClasses.List[i].SchoolClassNum==ProvCur.SchoolClassNum)
					comboSchoolClass.SelectedIndex=i+1;
			}
			for(int i=0;i<DefB.Short[(int)DefCat.FeeSchedNames].Length;i++){
				this.listFeeSched.Items.Add(DefB.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(DefB.Short[(int)DefCat.FeeSchedNames][i].DefNum==ProvCur.FeeSched){
					listFeeSched.SelectedIndex=i;
				}
			}
			if(listFeeSched.SelectedIndex<0){
				listFeeSched.SelectedIndex=0;
			}
			listSpecialty.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(DentalSpecialty)).Length;i++){
				listSpecialty.Items.Add(Lan.g("enumDentalSpecialty",Enum.GetNames(typeof(DentalSpecialty))[i]));
			}
			listSpecialty.SelectedIndex=(int)ProvCur.Specialty;
			FillProvIdent();
		}

		private void textCanadianOfficeNum_Validating(object sender,CancelEventArgs e) {
			if(textCanadianOfficeNum.Text==""){
				return;
			}
			if(textCanadianOfficeNum.Text.Length==4) {
				return;
			}
			if(!MsgBox.Show(this,true,"Office Number should be 4 characters.  Continue anyway?")){
				e.Cancel=true;
			}
		}

		private void butColor_Click(object sender, System.EventArgs e) {
			colorDialog1.Color=butColor.BackColor;
			colorDialog1.ShowDialog();
			butColor.BackColor=colorDialog1.Color;
		}

		private void butOutlineColor_Click(object sender, System.EventArgs e) {
			colorDialog1.Color=butOutlineColor.BackColor;
			colorDialog1.ShowDialog();
			butOutlineColor.BackColor=colorDialog1.Color;
		}

		private void radioSSN_Click(object sender, System.EventArgs e) {
			ProvCur.UsingTIN=false;
		}

		private void radioTIN_Click(object sender, System.EventArgs e) {
			ProvCur.UsingTIN=true;
		}

		private void FillProvIdent(){
			ProviderIdents.Refresh();
			ListProvIdent=ProviderIdents.GetForProv(ProvCur.ProvNum);
			tbProvIdent.ResetRows(ListProvIdent.Length);
			tbProvIdent.SetGridColor(Color.Gray);
			for(int i=0;i<ListProvIdent.Length;i++){
				tbProvIdent.Cell[0,i]=ListProvIdent[i].PayorID;
				tbProvIdent.Cell[1,i]=ListProvIdent[i].SuppIDType.ToString();
				tbProvIdent.Cell[2,i]=ListProvIdent[i].IDNumber;
			}
			tbProvIdent.LayoutTables();
		}

		private void tbProvIdent_CellDoubleClicked(object sender, OpenDental.CellEventArgs e) {
			FormProviderIdentEdit FormP=new FormProviderIdentEdit();
			FormP.ProvIdentCur=ListProvIdent[e.Row];
			FormP.ShowDialog();
			FillProvIdent();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormProviderIdentEdit FormP=new FormProviderIdentEdit();
			FormP.ProvIdentCur=new ProviderIdent();
			FormP.ProvIdentCur.ProvNum=ProvCur.ProvNum;
			FormP.IsNew=true;
			FormP.ShowDialog();
			FillProvIdent();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(tbProvIdent.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete the selected Provider Identifier?"),"",
				MessageBoxButtons.OKCancel)!=DialogResult.OK)
			{
				return;
			}
			ProviderIdents.Delete(ListProvIdent[tbProvIdent.SelectedRow]);
			FillProvIdent();
		}

		private void butOK_Click(object sender, System.EventArgs e){
			if(textAbbr.Text==""){
				MessageBox.Show(Lan.g(this,"Abbreviation not allowed to be blank."));
				return;
			}
			if(textSSN.Text.Contains("-")){
				MsgBox.Show(this,"SSN/TIN not allowed to have dash.");
				return;
			}
			ProvCur.Abbr=textAbbr.Text;
			ProvCur.LName=textLName.Text;
			ProvCur.FName=textFName.Text;
			ProvCur.MI=textMI.Text;
			ProvCur.Suffix=textSuffix.Text;
			ProvCur.SSN=textSSN.Text;
			ProvCur.StateLicense=textStateLicense.Text;
			ProvCur.DEANum=textDEANum.Text;
			//ProvCur.BlueCrossID=textBlueCrossID.Text;
			ProvCur.MedicaidID=textMedicaidID.Text;
			ProvCur.NationalProvID=textNationalProvID.Text;
			ProvCur.CanadianOfficeNum=textCanadianOfficeNum.Text;
			ProvCur.IsSecondary=checkIsSecondary.Checked;
			ProvCur.SigOnFile=checkSigOnFile.Checked;
			ProvCur.IsHidden=checkIsHidden.Checked;
			ProvCur.ProvColor=butColor.BackColor;
			ProvCur.OutlineColor=butOutlineColor.BackColor;
			if(comboSchoolClass.SelectedIndex==0)//none
				ProvCur.SchoolClassNum=0;
			else
				ProvCur.SchoolClassNum=SchoolClasses.List[comboSchoolClass.SelectedIndex-1].SchoolClassNum;
			if(listFeeSched.SelectedIndex!=-1)
				ProvCur.FeeSched=DefB.Short[(int)DefCat.FeeSchedNames][listFeeSched.SelectedIndex].DefNum;
			ProvCur.Specialty=(DentalSpecialty)listSpecialty.SelectedIndex;
			if(IsNew){
				Providers.Insert(ProvCur);
			}
			else{
				Providers.Update(ProvCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormProvEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				//UserPermissions.DeleteAllForProv(Providers.Cur.ProvNum);
				ProviderIdents.DeleteAllForProv(ProvCur.ProvNum);
				Providers.Delete(ProvCur);
			}
		}

		

	

		

		


	}
}




