using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FormProcCodeNew : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		///<summary></summary>
		public System.Windows.Forms.TextBox textNewCode;
		private OpenDental.UI.Button butAnother;
		private Label label2;
		public TextBox textDescription;
		private Label label3;
		private ListBox listType;
		private Label label4;
		public TextBox textAbbreviation;
		private CheckBox checkSetRecall;
		private CheckBox checkNoBillIns;
		private CheckBox checkIsHygiene;
		private CheckBox checkIsProsth;
		private Label label5;
		private ComboBox comboPaintType;
		private ComboBox comboTreatArea;
		private Label label6;
		private ComboBox comboCategory;
		private Label label7;
		private System.ComponentModel.Container components = null;
		private Label label8;
		public TextBox textCodePrevious;
		private GroupBox groupBox1;
		private Panel panel1;
		private Label label9;
		private OpenDental.UI.Button butDefault;
		public bool Changed;

		///<summary></summary>
		public FormProcCodeNew(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcCodeNew));
			this.textNewCode = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.listType = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textAbbreviation = new System.Windows.Forms.TextBox();
			this.checkSetRecall = new System.Windows.Forms.CheckBox();
			this.checkNoBillIns = new System.Windows.Forms.CheckBox();
			this.checkIsHygiene = new System.Windows.Forms.CheckBox();
			this.checkIsProsth = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.comboPaintType = new System.Windows.Forms.ComboBox();
			this.comboTreatArea = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.comboCategory = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.textCodePrevious = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label9 = new System.Windows.Forms.Label();
			this.butDefault = new OpenDental.UI.Button();
			this.butAnother = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textNewCode
			// 
			this.textNewCode.Location = new System.Drawing.Point(168,18);
			this.textNewCode.MaxLength = 15;
			this.textNewCode.Name = "textNewCode";
			this.textNewCode.Size = new System.Drawing.Size(143,20);
			this.textNewCode.TabIndex = 0;
			this.textNewCode.TextChanged += new System.EventHandler(this.textNewCode_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3,18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(163,18);
			this.label1.TabIndex = 3;
			this.label1.Text = "Procedure Code";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3,41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(163,18);
			this.label2.TabIndex = 6;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(168,41);
			this.textDescription.MaxLength = 255;
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(356,20);
			this.textDescription.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12,9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(209,18);
			this.label3.TabIndex = 7;
			this.label3.Text = "Type";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listType
			// 
			this.listType.FormattingEnabled = true;
			this.listType.Location = new System.Drawing.Point(15,30);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(218,381);
			this.listType.TabIndex = 8;
			this.listType.Click += new System.EventHandler(this.listType_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(3,64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(163,18);
			this.label4.TabIndex = 10;
			this.label4.Text = "Abbreviation";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAbbreviation
			// 
			this.textAbbreviation.Location = new System.Drawing.Point(168,64);
			this.textAbbreviation.MaxLength = 50;
			this.textAbbreviation.Name = "textAbbreviation";
			this.textAbbreviation.Size = new System.Drawing.Size(80,20);
			this.textAbbreviation.TabIndex = 2;
			// 
			// checkSetRecall
			// 
			this.checkSetRecall.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkSetRecall.Location = new System.Drawing.Point(283,148);
			this.checkSetRecall.Name = "checkSetRecall";
			this.checkSetRecall.Size = new System.Drawing.Size(164,18);
			this.checkSetRecall.TabIndex = 3;
			this.checkSetRecall.Text = "Triggers Recall";
			this.checkSetRecall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkSetRecall.UseVisualStyleBackColor = true;
			// 
			// checkNoBillIns
			// 
			this.checkNoBillIns.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkNoBillIns.Location = new System.Drawing.Point(239,169);
			this.checkNoBillIns.Name = "checkNoBillIns";
			this.checkNoBillIns.Size = new System.Drawing.Size(208,18);
			this.checkNoBillIns.TabIndex = 4;
			this.checkNoBillIns.Text = "Do not usually bill to insurance";
			this.checkNoBillIns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkNoBillIns.UseVisualStyleBackColor = true;
			// 
			// checkIsHygiene
			// 
			this.checkIsHygiene.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsHygiene.Location = new System.Drawing.Point(265,190);
			this.checkIsHygiene.Name = "checkIsHygiene";
			this.checkIsHygiene.Size = new System.Drawing.Size(182,18);
			this.checkIsHygiene.TabIndex = 5;
			this.checkIsHygiene.Text = "Is Hygiene Procedure";
			this.checkIsHygiene.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsHygiene.UseVisualStyleBackColor = true;
			// 
			// checkIsProsth
			// 
			this.checkIsProsth.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsProsth.Location = new System.Drawing.Point(283,211);
			this.checkIsProsth.Name = "checkIsProsth";
			this.checkIsProsth.Size = new System.Drawing.Size(164,18);
			this.checkIsProsth.TabIndex = 6;
			this.checkIsProsth.Text = "Is Prosthesis";
			this.checkIsProsth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsProsth.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(268,232);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(163,18);
			this.label5.TabIndex = 15;
			this.label5.Text = "Paint Type";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboPaintType
			// 
			this.comboPaintType.FormattingEnabled = true;
			this.comboPaintType.Location = new System.Drawing.Point(433,232);
			this.comboPaintType.Name = "comboPaintType";
			this.comboPaintType.Size = new System.Drawing.Size(165,21);
			this.comboPaintType.TabIndex = 7;
			// 
			// comboTreatArea
			// 
			this.comboTreatArea.FormattingEnabled = true;
			this.comboTreatArea.Location = new System.Drawing.Point(433,256);
			this.comboTreatArea.Name = "comboTreatArea";
			this.comboTreatArea.Size = new System.Drawing.Size(165,21);
			this.comboTreatArea.TabIndex = 8;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(268,256);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(163,18);
			this.label6.TabIndex = 17;
			this.label6.Text = "Treatment Area";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboCategory
			// 
			this.comboCategory.FormattingEnabled = true;
			this.comboCategory.Location = new System.Drawing.Point(433,280);
			this.comboCategory.Name = "comboCategory";
			this.comboCategory.Size = new System.Drawing.Size(165,21);
			this.comboCategory.TabIndex = 9;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(268,280);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(163,18);
			this.label7.TabIndex = 19;
			this.label7.Text = "Category";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(268,30);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(163,18);
			this.label8.TabIndex = 21;
			this.label8.Text = "Previously Entered Code";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCodePrevious
			// 
			this.textCodePrevious.Location = new System.Drawing.Point(433,30);
			this.textCodePrevious.MaxLength = 15;
			this.textCodePrevious.Name = "textCodePrevious";
			this.textCodePrevious.ReadOnly = true;
			this.textCodePrevious.Size = new System.Drawing.Size(143,20);
			this.textCodePrevious.TabIndex = 20;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textNewCode);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textDescription);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textAbbreviation);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Location = new System.Drawing.Point(265,54);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(541,91);
			this.groupBox1.TabIndex = 22;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Edit these three fields for each new code";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel1.Location = new System.Drawing.Point(6,419);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(800,2);
			this.panel1.TabIndex = 23;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(12,433);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(777,36);
			this.label9.TabIndex = 24;
			this.label9.Text = resources.GetString("label9.Text");
			this.label9.Visible = false;
			// 
			// butDefault
			// 
			this.butDefault.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butDefault.Autosize = true;
			this.butDefault.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDefault.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDefault.CornerRadius = 4F;
			this.butDefault.Location = new System.Drawing.Point(12,472);
			this.butDefault.Name = "butDefault";
			this.butDefault.Size = new System.Drawing.Size(104,26);
			this.butDefault.TabIndex = 25;
			this.butDefault.Text = "Set to Default";
			this.butDefault.Visible = false;
			this.butDefault.Click += new System.EventHandler(this.butDefault_Click);
			// 
			// butAnother
			// 
			this.butAnother.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAnother.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAnother.Autosize = true;
			this.butAnother.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAnother.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAnother.CornerRadius = 4F;
			this.butAnother.Location = new System.Drawing.Point(495,387);
			this.butAnother.Name = "butAnother";
			this.butAnother.Size = new System.Drawing.Size(114,26);
			this.butAnother.TabIndex = 10;
			this.butAnother.Text = "Add, then another";
			this.butAnother.Click += new System.EventHandler(this.butAnother_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(714,387);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 12;
			this.butCancel.Text = "&Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(624,387);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 11;
			this.butOK.Text = "Add";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormProcCodeNew
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(812,512);
			this.Controls.Add(this.butDefault);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textCodePrevious);
			this.Controls.Add(this.comboCategory);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.comboTreatArea);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.comboPaintType);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.checkIsProsth);
			this.Controls.Add(this.checkIsHygiene);
			this.Controls.Add(this.checkNoBillIns);
			this.Controls.Add(this.checkSetRecall);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butAnother);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcCodeNew";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "New Code";
			this.Load += new System.EventHandler(this.FormProcCodeNew_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		
		private void FormProcCodeNew_Load(object sender,EventArgs e) {
			ProcedureCodes.Refresh();
			listType.Items.Add(Lan.g(this,"none"));
			listType.Items.Add(Lan.g(this,"Exam"));
			listType.Items.Add(Lan.g(this,"Xray"));
			listType.Items.Add(Lan.g(this,"Prophy"));
			listType.Items.Add(Lan.g(this,"Fluoride"));
			listType.Items.Add(Lan.g(this,"Sealant"));
			listType.Items.Add(Lan.g(this,"Amalgam"));
			listType.Items.Add(Lan.g(this,"Composite, Anterior"));
			listType.Items.Add(Lan.g(this,"Composite, Posterior"));
			listType.Items.Add(Lan.g(this,"Buildup/Post"));
			listType.Items.Add(Lan.g(this,"Pulpotomy"));
			listType.Items.Add(Lan.g(this,"RCT"));
			listType.Items.Add(Lan.g(this,"SRP"));
			listType.Items.Add(Lan.g(this,"Denture"));
			listType.Items.Add(Lan.g(this,"RPD"));
			listType.Items.Add(Lan.g(this,"Denture Repair"));
			listType.Items.Add(Lan.g(this,"Reline"));
			listType.Items.Add(Lan.g(this,"Ceramic Inlay"));
			listType.Items.Add(Lan.g(this,"Metallic Inlay"));
			listType.Items.Add(Lan.g(this,"Whitening"));
			listType.Items.Add(Lan.g(this,"All-Ceramic Crown"));
			listType.Items.Add(Lan.g(this,"PFM Crown"));
			listType.Items.Add(Lan.g(this,"Full Gold Crown"));
			listType.Items.Add(Lan.g(this,"Bridge Pontic or Retainer - Ceramic"));
			listType.Items.Add(Lan.g(this,"Bridge Pontic or Retainer - PFM"));
			listType.Items.Add(Lan.g(this,"Bridge Pontic or Retainer - Metal"));
			listType.Items.Add(Lan.g(this,"Extraction"));
			listType.Items.Add(Lan.g(this,"Ortho"));
			listType.Items.Add(Lan.g(this,"Nitrous"));
			listType.SelectedIndex=0;
			for(int i=0;i<Enum.GetNames(typeof(ToothPaintingType)).Length;i++) {
				comboPaintType.Items.Add(Enum.GetNames(typeof(ToothPaintingType))[i]);
			}
			comboPaintType.SelectedIndex=(int)ToothPaintingType.None;
			for(int i=1;i<Enum.GetNames(typeof(TreatmentArea)).Length;i++) {
				comboTreatArea.Items.Add(Lan.g(this,Enum.GetNames(typeof(TreatmentArea))[i]));
			}
			comboTreatArea.SelectedIndex=(int)TreatmentArea.Mouth-1;
			for(int i=0;i<DefB.Short[(int)DefCat.ProcCodeCats].Length;i++) {
				comboCategory.Items.Add(DefB.Short[(int)DefCat.ProcCodeCats][i].ItemName);
			}
			comboCategory.SelectedIndex=0;
			textNewCode.Focus();
			textNewCode.Select(textNewCode.Text.Length,1);
		}
		
		private void textNewCode_TextChanged(object sender, System.EventArgs e) {
			if(textNewCode.Text=="d"){
				textNewCode.Text="D";
				textNewCode.SelectionStart=1;
			}
		}

		private void listType_Click(object sender,EventArgs e) {
			if(CultureInfo.CurrentCulture.Name=="en-US" && listType.SelectedIndex!=0){
				textNewCode.Text="D";
			}
			else{
				textNewCode.Text="";
			}
			textNewCode.Focus();
			textNewCode.Select(textNewCode.Text.Length,1);
			textDescription.Text="";
			textAbbreviation.Text="";
			checkSetRecall.Checked=false;
			checkNoBillIns.Checked=false;
			checkIsHygiene.Checked=false;
			checkIsProsth.Checked=false;
			comboPaintType.SelectedIndex=(int)ToothPaintingType.None;
			comboTreatArea.SelectedIndex=(int)TreatmentArea.Mouth-1;
			comboCategory.SelectedIndex=0;
			switch(listType.SelectedIndex){
				case 0://none
					break;
				case 1://Exam
					textDescription.Text="Exam";
					textAbbreviation.Text="Ex";
					checkSetRecall.Checked=true;
					comboCategory.SelectedIndex=GetCategoryIndex("Exams & Xrays");
					break;
				case 2://Xray
					textDescription.Text="Intraoral Periapical Film";
					textAbbreviation.Text="PA";
					checkIsHygiene.Checked=true;
					comboCategory.SelectedIndex=GetCategoryIndex("Exams & Xrays");
					break;
				case 3://Prophy
					textDescription.Text="Prophy, Adult";
					textAbbreviation.Text="Pro";
					checkSetRecall.Checked=true;
					checkIsHygiene.Checked=true;
					comboCategory.SelectedIndex=GetCategoryIndex("Cleanings");
					break;
				case 4://Fluoride
					textDescription.Text="Fluoride";
					textAbbreviation.Text="Flo";
					checkIsHygiene.Checked=true;
					comboCategory.SelectedIndex=GetCategoryIndex("Cleanings");
					break;
				case 5://Sealant
					textDescription.Text="Sealant";
					textAbbreviation.Text="Seal";
					checkIsHygiene.Checked=true;
					comboPaintType.SelectedIndex=(int)ToothPaintingType.Sealant;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Tooth-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Fillings");
					break;
				case 6://Amalgam
					textDescription.Text="Amalgam-1 Surf";
					textAbbreviation.Text="A1";
					comboPaintType.SelectedIndex=(int)ToothPaintingType.FillingDark;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Surf-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Fillings");
					break;
				case 7://Composite, Anterior
					textDescription.Text="Composite-1 Surf, Anterior";
					textAbbreviation.Text="C1";
					comboPaintType.SelectedIndex=(int)ToothPaintingType.FillingLight;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Surf-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Fillings");
					break;
				case 8://Composite, Posterior
					textDescription.Text="Composite-1 Surf, Posterior";
					textAbbreviation.Text="C1(P)";
					comboPaintType.SelectedIndex=(int)ToothPaintingType.FillingLight;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Surf-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Fillings");
					break;
				case 9://Buildup/Post
					textDescription.Text="Build Up";
					textAbbreviation.Text="BU";
					comboPaintType.SelectedIndex=(int)ToothPaintingType.PostBU;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Tooth-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Fillings");
					break;
				case 10://Pulpotomy
					textDescription.Text="Pulpotomy";
					textAbbreviation.Text="Pulp";
					checkNoBillIns.Checked=true;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Tooth-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Endo");
					break;
				case 11://RCT
					textDescription.Text="Root Canal, Anterior";
					textAbbreviation.Text="RCT-Ant";
					comboPaintType.SelectedIndex=(int)ToothPaintingType.RCT;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Tooth-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Endo");
					break;
				case 12://SRP
					textDescription.Text="Scaling & Root Planing, Quadrant";
					textAbbreviation.Text="SRP";
					checkSetRecall.Checked=true;
					checkIsHygiene.Checked=true;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Quad-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Perio");
					break;
				case 13://Denture
					textDescription.Text="Maxillary Denture";
					textAbbreviation.Text="MaxDent";
					checkIsProsth.Checked=true;
					comboPaintType.SelectedIndex=(int)ToothPaintingType.DentureLight;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Arch-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Dentures");
					break;
				case 14://RPD
					textDescription.Text="Maxillary RPD";
					textAbbreviation.Text="MaxRPD";
					checkIsProsth.Checked=true;
					comboPaintType.SelectedIndex=(int)ToothPaintingType.DentureLight;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.ToothRange-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Dentures");
					break;
				case 15://Denture Repair
					textDescription.Text="Repair Broken Denture";
					textAbbreviation.Text="RepairDent";
					comboCategory.SelectedIndex=GetCategoryIndex("Dentures");
					break;
				case 16://Reline
					textDescription.Text="Reline Max Denture Chairside";
					textAbbreviation.Text="RelMaxDntChair";
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Arch-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Dentures");
					break;
				case 17://Ceramic Inlay
					textDescription.Text="Ceramic Inlay-1 Surf";
					textAbbreviation.Text="CerInlay1";
					comboPaintType.SelectedIndex=(int)ToothPaintingType.FillingLight;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Surf-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Cosmetic");
					break;
				case 18://Metal Inlay
					textDescription.Text="Metallic Inlay-1 Surf";
					textAbbreviation.Text="MetInlay1";
					comboPaintType.SelectedIndex=(int)ToothPaintingType.FillingDark;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Surf-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Fillings");
					break;
				case 19://Whitening
					textDescription.Text="Whitening Tray";
					textAbbreviation.Text="White";
					checkNoBillIns.Checked=true;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Arch-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Cosmetic");
					break;
				case 20://All-Ceramic Crown
					textDescription.Text="All-Ceramic Crown";
					textAbbreviation.Text="AllCerCrn";
					checkIsProsth.Checked=true;
					comboPaintType.SelectedIndex=(int)ToothPaintingType.CrownLight;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Tooth-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Crown & Bridge");
					break;
				case 21://PFM Crown
					textDescription.Text="PFM Crown";
					textAbbreviation.Text="PFM";
					checkIsProsth.Checked=true;
					comboPaintType.SelectedIndex=(int)ToothPaintingType.CrownLight;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Tooth-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Crown & Bridge");
					break;
				case 22://Full Gold Crown
					textDescription.Text="Full Gold Crown";
					textAbbreviation.Text="FGCrn";
					checkIsProsth.Checked=true;
					comboPaintType.SelectedIndex=(int)ToothPaintingType.CrownDark;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Tooth-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Crown & Bridge");
					break;
				case 23://Bridge Pontic or Retainer - Ceramic
					textDescription.Text="Bridge Pontic, Ceramic";
					textAbbreviation.Text="PontCeram";
					checkIsProsth.Checked=true;
					comboPaintType.SelectedIndex=(int)ToothPaintingType.BridgeLight;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Tooth-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Crown & Bridge");
					break;
				case 24://Bridge Pontic or Retainer - PFM
					textDescription.Text="Bridge Pontic, PFM";
					textAbbreviation.Text="PontPFM";
					checkIsProsth.Checked=true;
					comboPaintType.SelectedIndex=(int)ToothPaintingType.BridgeLight;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Tooth-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Crown & Bridge");
					break;
				case 25://Bridge Pontic or Retainer - Metal
					textDescription.Text="Bridge Pontic, Cast Noble Metal";
					textAbbreviation.Text="PontCastNM";
					checkIsProsth.Checked=true;
					comboPaintType.SelectedIndex=(int)ToothPaintingType.BridgeDark;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Tooth-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Crown & Bridge");
					break;
				case 26://Extraction
					textDescription.Text="Extraction";
					textAbbreviation.Text="Ext";
					comboPaintType.SelectedIndex=(int)ToothPaintingType.Extraction;
					comboTreatArea.SelectedIndex=(int)TreatmentArea.Tooth-1;
					comboCategory.SelectedIndex=GetCategoryIndex("Oral Surgery");
					break;
				case 27://Ortho
					textDescription.Text="Comprehensive Ortho, Adult";
					textAbbreviation.Text="CompOrthoAdlt";
					comboCategory.SelectedIndex=GetCategoryIndex("Ortho");
					break;
				case 28://Nitrous
					textDescription.Text="Nitrous Oxide, Under 1 hour";
					textAbbreviation.Text="Nitrous30";
					comboCategory.SelectedIndex=GetCategoryIndex("Misc");
					break;
			}
		}

		///<summary>Returns the index of the category with the supplied name.  Zero if the name does not exist.</summary>
		private int GetCategoryIndex(string name){
			for(int i=0;i<DefB.Short[(int)DefCat.ProcCodeCats].Length;i++) {
				if(DefB.Short[(int)DefCat.ProcCodeCats][i].ItemName==name){
					return i;
				}
			}
			return 0;
		}

		private bool AddProc(){
			if(textNewCode.Text=="") {
				MsgBox.Show(this,"Code not allowed to be blank.");
				return false;
			}
			if(ProcedureCodes.IsValidCode(textNewCode.Text)){
				MsgBox.Show(this,"That code already exists.");
				return false;
			}
			if(textDescription.Text=="") {
				MsgBox.Show(this,"Description not allowed to be blank.");
				return false;
			}
			if(textAbbreviation.Text=="") {
				MsgBox.Show(this,"Abbreviation not allowed to be blank.");
				return false;
			}
			//ok to add code-----------------------------------------------------------------------------------
			ProcedureCode code=new ProcedureCode();
			code.ProcCode=textNewCode.Text;
			//code.ProcTime="/X/";//moved to contructor.
			//code.GraphicColor=Color.FromArgb(0);//moved to contructor.
			code.Descript=textDescription.Text;
			code.AbbrDesc=textAbbreviation.Text;
			code.SetRecall=checkSetRecall.Checked;
			code.NoBillIns=checkNoBillIns.Checked;
			code.IsHygiene=checkIsHygiene.Checked;
			code.IsProsth=checkIsProsth.Checked;
			code.PaintType=(ToothPaintingType)comboPaintType.SelectedIndex;
			code.TreatArea=(TreatmentArea)comboTreatArea.SelectedIndex+1;
			//if(comboCategory.SelectedIndex!=-1)
			code.ProcCat=DefB.Short[(int)DefCat.ProcCodeCats][comboCategory.SelectedIndex].DefNum;
			ProcedureCodes.Insert(code);
			Changed=true;
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Added Procedure Code: "+code.ProcCode);
			return true;
		}

		private void butDefault_Click(object sender,EventArgs e) {
			Changed=true;//because of add definition for new proc category
			if(MsgBox.Show(this,true,"Delete all 'T' codes which came with trial version?")){
				
			}
		}

		private void butAnother_Click(object sender,EventArgs e) {
			string previous=textNewCode.Text;
			if(AddProc()){
				ProcedureCodes.Refresh();
				if(CultureInfo.CurrentCulture.Name=="en-US" && listType.SelectedIndex!=0) {
					textNewCode.Text="D";
				}
				else {
					textNewCode.Text="";
				}
				textCodePrevious.Text=previous;
			}
			textNewCode.Focus();
			textNewCode.Select(textNewCode.Text.Length,1);
			
		}

		private void butOK_Click(object sender,System.EventArgs e) {
			if(AddProc()){
				Close();
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			Close();
		}

		

		

		

		

		
		
	}
}
