/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;
using OpenDental.Bridges;
using OpenDental.UI;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
///<summary></summary>
	public class FormInsPlan : System.Windows.Forms.Form{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label labelGroupNum;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.TextBox textPhone;
		private System.Windows.Forms.TextBox textGroupName;
		private System.Windows.Forms.TextBox textGroupNum;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.TextBox textElectID;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textCarrier;
		private OpenDental.ValidDate textDateEffect;
		private OpenDental.ValidDate textDateTerm;
		///<summary>The InsPlan is always inserted before opening this form.</summary>
		public bool IsNewPlan;
		///<summary>The PatPlan is always inserted before opening this form.</summary>
		public bool IsNewPatPlan;
		private System.Windows.Forms.TextBox textEmployer;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox textAddress2;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkRelease;
		private System.Windows.Forms.CheckBox checkNoSendElect;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.CheckBox checkClaimsUseUCR;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textSubscriber;
		private System.Windows.Forms.GroupBox groupSubscriber;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.TextBox textSubscriberID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboLinked;
		private System.Windows.Forms.TextBox textLinkedNum;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.CheckBox checkAssign;
		private ArrayList similarEmps;
		private string empOriginal;//used in the emp dropdown logic
		private System.Windows.Forms.ListBox listEmps;//displayed from within code, not designer
		private bool mouseIsInListEmps;
		private ArrayList similarCars;
		private string carOriginal;
		private System.Windows.Forms.ListBox listCars;
		private System.Windows.Forms.Label labelCitySTZip;
		private bool mouseIsInListCars;
		private System.Windows.Forms.Label labelDrop;
		private OpenDental.UI.Button butDrop;
		private System.Windows.Forms.GroupBox groupCoPay;
		private System.Windows.Forms.Label label3;
		private OpenDental.ODtextBox textSubscNote;
		private System.Windows.Forms.ComboBox comboCopay;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox comboFeeSched;
		private System.Windows.Forms.ComboBox comboClaimForm;
		private OpenDental.UI.Button butSearch;
		///<summary></summary>
		private InsPlan PlanCur;
		///<summary>This is a copy of PlanCur as it was originally when this form was openned.  It is only needed if user decides to apply changes to all identical plans.</summary>
		private InsPlan PlanCurOld;
		private System.Windows.Forms.ComboBox comboElectIDdescript;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox comboAllowedFeeSched;
		private OpenDental.UI.Button butLabel;
		private System.Windows.Forms.GroupBox groupRequestBen;
		private System.Windows.Forms.Label labelTrojanID;
		private System.Windows.Forms.TextBox textTrojanID;
		private OpenDental.UI.Button butImportTrojan;
		private System.Windows.Forms.Label labelDivisionDash;
		private System.Windows.Forms.TextBox textDivisionNo;
		private System.Windows.Forms.Label labelElectronicID;
		private System.Windows.Forms.Label labelIAP;
		private OpenDental.UI.Button butIapFind;
		private System.Windows.Forms.Label labelTrojan;
		private OpenDental.UI.Button butBenefitNotes;
		private System.Windows.Forms.CheckBox checkIsMedical;
		private System.Windows.Forms.CheckBox checkAlternateCode;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.ComboBox comboPlanType;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label36;
		private Carrier CarrierCur;
		private System.Windows.Forms.ComboBox comboRelationship;
		private System.Windows.Forms.CheckBox checkIsPending;
		private System.Windows.Forms.TextBox textPatID;
		private OpenDental.UI.Button butAdjAdd;
		private System.Windows.Forms.ListBox listAdj;
		private System.Windows.Forms.Panel panelPat;
		private PatPlan PatPlanCur;
		private ArrayList AdjAL;
		private OpenDental.ValidNumber textOrdinal;
		private OpenDental.UI.ODGrid gridBenefits;
		private TextBox textPlanNum;
		///<summary>This is the current benefit list that displays on the form.  It does not get saved to the database until this form closes.</summary>
		private List<Benefit> benefitList;//each item is a Benefit
		private List<Benefit> benefitListOld;
		private bool usesAnnivers;
		private Label label17;
		private OpenDental.UI.Button butPick;
		private CheckBox checkApplyAll;
		private ODtextBox textPlanNote;
		private Label label18;
		private OpenDental.UI.Button butEditBenefits;
		///<summary>Set to true if called from the list of insurance templates.  In this case, the planNum will be 0.  There will be no subscriber.  Benefits will be 'typical' rather than from one specific plan.  Upon saving, all similar plans will be set to be exactly the same as PlanCur.</summary>
		public bool IsForAll;
		///<summary>Set to true from FormInsPlansMerge.  In this case, the insplan is read only, because it's much more complicated to allow user to change.</summary>
		public bool IsReadOnly;
		private Def[] FeeSchedsStandard;
		private Def[] FeeSchedsCopay;
		private GroupBox groupPlan;
		private Def[] FeeSchedsAllowed;
		private Panel panelPlan;
		private Label label13;
		private ComboBox comboFilingCode;
		private GroupBox groupCarrier;
		private Label labelDentaide;
		private ValidNumber textDentaide;
		private OpenDental.UI.Button butCanadaEligibility;
		private Label labelCanadaEligibility;
		private OpenDental.UI.Button butEligibility;
		private CheckBox checkShowBaseUnits;
		private CheckBox checkDedBeforePerc;
		private Label labelViewRequestDocument;
		//<summary>This is a field that is accessed only by clicking on the button because there's not room for it otherwise.  This variable should be treated just as if it was a visible textBox.</summary>
		//private string BenefitNotes;

		///<summary>Called from ContrFamily and FormInsPlans. Must pass in both the plan and the patPlan, although patPlan can be null.  They are handled separately.</summary>
		public FormInsPlan(InsPlan planCur,PatPlan patPlanCur){
			Cursor=Cursors.WaitCursor;
			InitializeComponent();
			PlanCur=planCur;
			PatPlanCur=patPlanCur;
			listEmps=new ListBox();
			listEmps.Location=new Point(panelPlan.Left+groupPlan.Left+textEmployer.Left,
				panelPlan.Top+groupPlan.Top+textEmployer.Bottom);
			listEmps.Size=new Size(231,100);
			listEmps.Visible=false;
			listEmps.Click += new System.EventHandler(listEmps_Click);
			listEmps.DoubleClick += new System.EventHandler(listEmps_DoubleClick);
			listEmps.MouseEnter += new System.EventHandler(listEmps_MouseEnter);
			listEmps.MouseLeave += new System.EventHandler(listEmps_MouseLeave);
			Controls.Add(listEmps);
			listEmps.BringToFront();
			listCars=new ListBox();
			listCars.Location=new Point(panelPlan.Left+groupPlan.Left+groupCarrier.Left+textCarrier.Left,
				panelPlan.Top+groupPlan.Top+groupCarrier.Top+textCarrier.Bottom);
			listCars.Size=new Size(700,100);
			listCars.HorizontalScrollbar=true;
			listCars.Visible=false;
			listCars.Click += new System.EventHandler(listCars_Click);
			listCars.DoubleClick += new System.EventHandler(listCars_DoubleClick);
			listCars.MouseEnter += new System.EventHandler(listCars_MouseEnter);
			listCars.MouseLeave += new System.EventHandler(listCars_MouseLeave);
			Controls.Add(listCars);
			listCars.BringToFront();
			//tbPercentPlan.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercentPlan_CellClicked);
			//tbPercentPat.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercentPat_CellClicked);
			Lan.F(this);
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				labelCitySTZip.Text=Lan.g(this,"City,Prov,Post");   //Postal Code";
				butSearch.Visible=false;
				labelElectronicID.Text="EDI Code";
				comboElectIDdescript.Visible=false;
				labelGroupNum.Text=Lan.g(this,"Plan Num-Div");
			}
			else{
				labelDivisionDash.Visible=false;
				textDivisionNo.Visible=false;
				labelDentaide.Visible=false;
				textDentaide.Visible=false;
				labelCanadaEligibility.Visible=false;
				butCanadaEligibility.Visible=false;
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="GB"){//en-GB
				labelCitySTZip.Text=Lan.g(this,"City,Postcode");
			}
			panelPat.BackColor=DefB.Long[(int)DefCat.MiscColors][0].ItemColor;
			labelViewRequestDocument.Text="         ";
			//if(!PrefB.GetBool("CustomizedForPracticeWeb")) {
			//	butEligibility.Visible=false;
			//	labelViewRequestDocument.Visible=false;
			//}
			Cursor=Cursors.Default;
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInsPlan));
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.labelGroupNum = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.labelCitySTZip = new System.Windows.Forms.Label();
			this.labelElectronicID = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.textPhone = new System.Windows.Forms.TextBox();
			this.textGroupName = new System.Windows.Forms.TextBox();
			this.textGroupNum = new System.Windows.Forms.TextBox();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.textState = new System.Windows.Forms.TextBox();
			this.textZip = new System.Windows.Forms.TextBox();
			this.textElectID = new System.Windows.Forms.TextBox();
			this.textEmployer = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.checkAssign = new System.Windows.Forms.CheckBox();
			this.checkRelease = new System.Windows.Forms.CheckBox();
			this.checkNoSendElect = new System.Windows.Forms.CheckBox();
			this.label23 = new System.Windows.Forms.Label();
			this.checkAlternateCode = new System.Windows.Forms.CheckBox();
			this.checkClaimsUseUCR = new System.Windows.Forms.CheckBox();
			this.label14 = new System.Windows.Forms.Label();
			this.textSubscriber = new System.Windows.Forms.TextBox();
			this.groupSubscriber = new System.Windows.Forms.GroupBox();
			this.labelViewRequestDocument = new System.Windows.Forms.Label();
			this.butEligibility = new OpenDental.UI.Button();
			this.label25 = new System.Windows.Forms.Label();
			this.textSubscriberID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textDateEffect = new OpenDental.ValidDate();
			this.textDateTerm = new OpenDental.ValidDate();
			this.textSubscNote = new OpenDental.ODtextBox();
			this.comboLinked = new System.Windows.Forms.ComboBox();
			this.textLinkedNum = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboPlanType = new System.Windows.Forms.ComboBox();
			this.checkIsMedical = new System.Windows.Forms.CheckBox();
			this.textDivisionNo = new System.Windows.Forms.TextBox();
			this.labelDivisionDash = new System.Windows.Forms.Label();
			this.comboClaimForm = new System.Windows.Forms.ComboBox();
			this.comboFeeSched = new System.Windows.Forms.ComboBox();
			this.groupCoPay = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.comboAllowedFeeSched = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboCopay = new System.Windows.Forms.ComboBox();
			this.comboElectIDdescript = new System.Windows.Forms.ComboBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.butCanadaEligibility = new OpenDental.UI.Button();
			this.butBenefitNotes = new OpenDental.UI.Button();
			this.butIapFind = new OpenDental.UI.Button();
			this.butImportTrojan = new OpenDental.UI.Button();
			this.labelDrop = new System.Windows.Forms.Label();
			this.groupRequestBen = new System.Windows.Forms.GroupBox();
			this.labelCanadaEligibility = new System.Windows.Forms.Label();
			this.labelTrojan = new System.Windows.Forms.Label();
			this.labelIAP = new System.Windows.Forms.Label();
			this.labelTrojanID = new System.Windows.Forms.Label();
			this.textTrojanID = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.comboRelationship = new System.Windows.Forms.ComboBox();
			this.label31 = new System.Windows.Forms.Label();
			this.checkIsPending = new System.Windows.Forms.CheckBox();
			this.label32 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.listAdj = new System.Windows.Forms.ListBox();
			this.label35 = new System.Windows.Forms.Label();
			this.textPatID = new System.Windows.Forms.TextBox();
			this.label36 = new System.Windows.Forms.Label();
			this.panelPat = new System.Windows.Forms.Panel();
			this.textOrdinal = new OpenDental.ValidNumber();
			this.butAdjAdd = new OpenDental.UI.Button();
			this.butDrop = new OpenDental.UI.Button();
			this.textPlanNum = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.checkApplyAll = new System.Windows.Forms.CheckBox();
			this.label18 = new System.Windows.Forms.Label();
			this.groupPlan = new System.Windows.Forms.GroupBox();
			this.groupCarrier = new System.Windows.Forms.GroupBox();
			this.butSearch = new OpenDental.UI.Button();
			this.panelPlan = new System.Windows.Forms.Panel();
			this.checkShowBaseUnits = new System.Windows.Forms.CheckBox();
			this.textDentaide = new OpenDental.ValidNumber();
			this.labelDentaide = new System.Windows.Forms.Label();
			this.comboFilingCode = new System.Windows.Forms.ComboBox();
			this.label13 = new System.Windows.Forms.Label();
			this.butPick = new OpenDental.UI.Button();
			this.butEditBenefits = new OpenDental.UI.Button();
			this.textPlanNote = new OpenDental.ODtextBox();
			this.butOK = new OpenDental.UI.Button();
			this.gridBenefits = new OpenDental.UI.ODGrid();
			this.butLabel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.checkDedBeforePerc = new System.Windows.Forms.CheckBox();
			this.groupSubscriber.SuspendLayout();
			this.groupCoPay.SuspendLayout();
			this.groupRequestBen.SuspendLayout();
			this.panelPat.SuspendLayout();
			this.groupPlan.SuspendLayout();
			this.groupCarrier.SuspendLayout();
			this.panelPlan.SuspendLayout();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7,53);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,15);
			this.label5.TabIndex = 5;
			this.label5.Text = "Effective Dates";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(187,53);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(36,15);
			this.label6.TabIndex = 6;
			this.label6.Text = "To";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(5,34);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(95,15);
			this.label7.TabIndex = 7;
			this.label7.Text = "Phone";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16,203);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(95,15);
			this.label8.TabIndex = 8;
			this.label8.Text = "Group Name";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelGroupNum
			// 
			this.labelGroupNum.Location = new System.Drawing.Point(16,223);
			this.labelGroupNum.Name = "labelGroupNum";
			this.labelGroupNum.Size = new System.Drawing.Size(95,15);
			this.labelGroupNum.TabIndex = 9;
			this.labelGroupNum.Text = "Group Num";
			this.labelGroupNum.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(5,53);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(95,15);
			this.label10.TabIndex = 10;
			this.label10.Text = "Address";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelCitySTZip
			// 
			this.labelCitySTZip.Location = new System.Drawing.Point(5,93);
			this.labelCitySTZip.Name = "labelCitySTZip";
			this.labelCitySTZip.Size = new System.Drawing.Size(95,15);
			this.labelCitySTZip.TabIndex = 11;
			this.labelCitySTZip.Text = "City,ST,Zip";
			this.labelCitySTZip.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelElectronicID
			// 
			this.labelElectronicID.Location = new System.Drawing.Point(4,113);
			this.labelElectronicID.Name = "labelElectronicID";
			this.labelElectronicID.Size = new System.Drawing.Size(95,15);
			this.labelElectronicID.TabIndex = 15;
			this.labelElectronicID.Text = "Electronic ID";
			this.labelElectronicID.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(2,74);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(55,41);
			this.label28.TabIndex = 28;
			this.label28.Text = "Note";
			this.label28.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCarrier
			// 
			this.textCarrier.Font = new System.Drawing.Font("Microsoft Sans Serif",9F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textCarrier.Location = new System.Drawing.Point(102,11);
			this.textCarrier.MaxLength = 50;
			this.textCarrier.Multiline = true;
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.Size = new System.Drawing.Size(291,20);
			this.textCarrier.TabIndex = 0;
			this.textCarrier.Leave += new System.EventHandler(this.textCarrier_Leave);
			this.textCarrier.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textCarrier_KeyUp);
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(102,31);
			this.textPhone.MaxLength = 30;
			this.textPhone.Name = "textPhone";
			this.textPhone.Size = new System.Drawing.Size(157,20);
			this.textPhone.TabIndex = 0;
			this.textPhone.TextChanged += new System.EventHandler(this.textPhone_TextChanged);
			// 
			// textGroupName
			// 
			this.textGroupName.Location = new System.Drawing.Point(112,200);
			this.textGroupName.MaxLength = 50;
			this.textGroupName.Name = "textGroupName";
			this.textGroupName.Size = new System.Drawing.Size(193,20);
			this.textGroupName.TabIndex = 1;
			// 
			// textGroupNum
			// 
			this.textGroupNum.Location = new System.Drawing.Point(112,220);
			this.textGroupNum.MaxLength = 20;
			this.textGroupNum.Name = "textGroupNum";
			this.textGroupNum.Size = new System.Drawing.Size(129,20);
			this.textGroupNum.TabIndex = 2;
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(102,51);
			this.textAddress.MaxLength = 60;
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(291,20);
			this.textAddress.TabIndex = 1;
			this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(102,91);
			this.textCity.MaxLength = 40;
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(155,20);
			this.textCity.TabIndex = 3;
			this.textCity.TextChanged += new System.EventHandler(this.textCity_TextChanged);
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(257,91);
			this.textState.MaxLength = 2;
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(65,20);
			this.textState.TabIndex = 4;
			this.textState.TextChanged += new System.EventHandler(this.textState_TextChanged);
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(322,91);
			this.textZip.MaxLength = 10;
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(71,20);
			this.textZip.TabIndex = 5;
			// 
			// textElectID
			// 
			this.textElectID.Location = new System.Drawing.Point(102,111);
			this.textElectID.MaxLength = 20;
			this.textElectID.Name = "textElectID";
			this.textElectID.Size = new System.Drawing.Size(54,20);
			this.textElectID.TabIndex = 7;
			this.textElectID.Validating += new System.ComponentModel.CancelEventHandler(this.textElectID_Validating);
			// 
			// textEmployer
			// 
			this.textEmployer.Location = new System.Drawing.Point(112,26);
			this.textEmployer.MaxLength = 40;
			this.textEmployer.Name = "textEmployer";
			this.textEmployer.Size = new System.Drawing.Size(291,20);
			this.textEmployer.TabIndex = 4;
			this.textEmployer.Leave += new System.EventHandler(this.textEmployer_Leave);
			this.textEmployer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textEmployer_KeyUp);
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(33,28);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(78,15);
			this.label16.TabIndex = 73;
			this.label16.Text = "Employer";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(102,71);
			this.textAddress2.MaxLength = 60;
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.Size = new System.Drawing.Size(291,20);
			this.textAddress2.TabIndex = 2;
			this.textAddress2.TextChanged += new System.EventHandler(this.textAddress2_TextChanged);
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(5,74);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(95,15);
			this.label21.TabIndex = 79;
			this.label21.Text = "Address 2";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(15,343);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96,16);
			this.label1.TabIndex = 91;
			this.label1.Text = "Fee Schedule";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkAssign
			// 
			this.checkAssign.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAssign.Location = new System.Drawing.Point(320,50);
			this.checkAssign.Name = "checkAssign";
			this.checkAssign.Size = new System.Drawing.Size(177,20);
			this.checkAssign.TabIndex = 4;
			this.checkAssign.Text = "Assignment of Benefits";
			// 
			// checkRelease
			// 
			this.checkRelease.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRelease.Location = new System.Drawing.Point(320,32);
			this.checkRelease.Name = "checkRelease";
			this.checkRelease.Size = new System.Drawing.Size(177,20);
			this.checkRelease.TabIndex = 3;
			this.checkRelease.Text = "Release of Information";
			// 
			// checkNoSendElect
			// 
			this.checkNoSendElect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoSendElect.Location = new System.Drawing.Point(178,133);
			this.checkNoSendElect.Name = "checkNoSendElect";
			this.checkNoSendElect.Size = new System.Drawing.Size(213,17);
			this.checkNoSendElect.TabIndex = 8;
			this.checkNoSendElect.Text = "Don\'t Usually Send Electronically";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(14,367);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(95,15);
			this.label23.TabIndex = 96;
			this.label23.Text = "Claim Form";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkAlternateCode
			// 
			this.checkAlternateCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAlternateCode.Location = new System.Drawing.Point(112,293);
			this.checkAlternateCode.Name = "checkAlternateCode";
			this.checkAlternateCode.Size = new System.Drawing.Size(275,16);
			this.checkAlternateCode.TabIndex = 3;
			this.checkAlternateCode.Text = "Use Alternate Code (for some Medicaid plans)";
			// 
			// checkClaimsUseUCR
			// 
			this.checkClaimsUseUCR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimsUseUCR.Location = new System.Drawing.Point(112,309);
			this.checkClaimsUseUCR.Name = "checkClaimsUseUCR";
			this.checkClaimsUseUCR.Size = new System.Drawing.Size(275,16);
			this.checkClaimsUseUCR.TabIndex = 4;
			this.checkClaimsUseUCR.Text = "Claims show UCR fee, not billed fee";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(17,271);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(95,16);
			this.label14.TabIndex = 104;
			this.label14.Text = "Plan Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSubscriber
			// 
			this.textSubscriber.Location = new System.Drawing.Point(109,10);
			this.textSubscriber.Name = "textSubscriber";
			this.textSubscriber.ReadOnly = true;
			this.textSubscriber.Size = new System.Drawing.Size(278,20);
			this.textSubscriber.TabIndex = 109;
			// 
			// groupSubscriber
			// 
			this.groupSubscriber.Controls.Add(this.labelViewRequestDocument);
			this.groupSubscriber.Controls.Add(this.butEligibility);
			this.groupSubscriber.Controls.Add(this.checkAssign);
			this.groupSubscriber.Controls.Add(this.label25);
			this.groupSubscriber.Controls.Add(this.checkRelease);
			this.groupSubscriber.Controls.Add(this.textSubscriber);
			this.groupSubscriber.Controls.Add(this.textSubscriberID);
			this.groupSubscriber.Controls.Add(this.label2);
			this.groupSubscriber.Controls.Add(this.textDateEffect);
			this.groupSubscriber.Controls.Add(this.label5);
			this.groupSubscriber.Controls.Add(this.textDateTerm);
			this.groupSubscriber.Controls.Add(this.label6);
			this.groupSubscriber.Controls.Add(this.textSubscNote);
			this.groupSubscriber.Controls.Add(this.label28);
			this.groupSubscriber.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupSubscriber.Location = new System.Drawing.Point(468,93);
			this.groupSubscriber.Name = "groupSubscriber";
			this.groupSubscriber.Size = new System.Drawing.Size(502,175);
			this.groupSubscriber.TabIndex = 9;
			this.groupSubscriber.TabStop = false;
			this.groupSubscriber.Text = "Subscriber";
			// 
			// labelViewRequestDocument
			// 
			this.labelViewRequestDocument.Location = new System.Drawing.Point(483,11);
			this.labelViewRequestDocument.Name = "labelViewRequestDocument";
			this.labelViewRequestDocument.Size = new System.Drawing.Size(18,23);
			this.labelViewRequestDocument.TabIndex = 117;
			this.labelViewRequestDocument.Text = "labelViewRequestDocument";
			this.labelViewRequestDocument.Click += new System.EventHandler(this.labelViewRequestDocument_Click);
			// 
			// butEligibility
			// 
			this.butEligibility.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEligibility.Autosize = true;
			this.butEligibility.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEligibility.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEligibility.CornerRadius = 4F;
			this.butEligibility.Location = new System.Drawing.Point(393,10);
			this.butEligibility.Name = "butEligibility";
			this.butEligibility.Size = new System.Drawing.Size(89,21);
			this.butEligibility.TabIndex = 116;
			this.butEligibility.Text = "Check Eligibility";
			this.toolTip1.SetToolTip(this.butEligibility,"Edit all the similar plans at once");
			this.butEligibility.Click += new System.EventHandler(this.butEligibility_Click);
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(8,14);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(99,15);
			this.label25.TabIndex = 115;
			this.label25.Text = "Name";
			this.label25.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSubscriberID
			// 
			this.textSubscriberID.Location = new System.Drawing.Point(109,30);
			this.textSubscriberID.MaxLength = 20;
			this.textSubscriberID.Name = "textSubscriberID";
			this.textSubscriberID.Size = new System.Drawing.Size(129,20);
			this.textSubscriberID.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8,32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99,15);
			this.label2.TabIndex = 114;
			this.label2.Text = "Subscriber ID";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDateEffect
			// 
			this.textDateEffect.Location = new System.Drawing.Point(109,50);
			this.textDateEffect.Name = "textDateEffect";
			this.textDateEffect.Size = new System.Drawing.Size(72,20);
			this.textDateEffect.TabIndex = 1;
			// 
			// textDateTerm
			// 
			this.textDateTerm.Location = new System.Drawing.Point(227,50);
			this.textDateTerm.Name = "textDateTerm";
			this.textDateTerm.Size = new System.Drawing.Size(72,20);
			this.textDateTerm.TabIndex = 2;
			// 
			// textSubscNote
			// 
			this.textSubscNote.AcceptsReturn = true;
			this.textSubscNote.Location = new System.Drawing.Point(57,71);
			this.textSubscNote.Multiline = true;
			this.textSubscNote.Name = "textSubscNote";
			this.textSubscNote.QuickPasteType = OpenDentBusiness.QuickPasteType.InsPlan;
			this.textSubscNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textSubscNote.Size = new System.Drawing.Size(439,98);
			this.textSubscNote.TabIndex = 5;
			this.textSubscNote.Text = "1\r\n2\r\n3 lines will show here in 46 vert.\r\n4 lines will show here in 59 vert.\r\n5 l" +
    "ines in 72 vert\r\n6 lines in 85 vert\r\n7 lines in 98";
			// 
			// comboLinked
			// 
			this.comboLinked.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboLinked.Location = new System.Drawing.Point(150,240);
			this.comboLinked.MaxDropDownItems = 30;
			this.comboLinked.Name = "comboLinked";
			this.comboLinked.Size = new System.Drawing.Size(253,21);
			this.comboLinked.TabIndex = 68;
			// 
			// textLinkedNum
			// 
			this.textLinkedNum.BackColor = System.Drawing.Color.White;
			this.textLinkedNum.Location = new System.Drawing.Point(112,240);
			this.textLinkedNum.Multiline = true;
			this.textLinkedNum.Name = "textLinkedNum";
			this.textLinkedNum.ReadOnly = true;
			this.textLinkedNum.Size = new System.Drawing.Size(37,21);
			this.textLinkedNum.TabIndex = 67;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6,242);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104,17);
			this.label4.TabIndex = 66;
			this.label4.Text = "Identical Plans";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboPlanType
			// 
			this.comboPlanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPlanType.Location = new System.Drawing.Point(112,270);
			this.comboPlanType.Name = "comboPlanType";
			this.comboPlanType.Size = new System.Drawing.Size(212,21);
			this.comboPlanType.TabIndex = 2;
			this.comboPlanType.SelectionChangeCommitted += new System.EventHandler(this.comboPlanType_SelectionChangeCommitted);
			// 
			// checkIsMedical
			// 
			this.checkIsMedical.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsMedical.Location = new System.Drawing.Point(112,9);
			this.checkIsMedical.Name = "checkIsMedical";
			this.checkIsMedical.Size = new System.Drawing.Size(202,17);
			this.checkIsMedical.TabIndex = 113;
			this.checkIsMedical.Text = "Medical Insurance";
			// 
			// textDivisionNo
			// 
			this.textDivisionNo.Location = new System.Drawing.Point(259,220);
			this.textDivisionNo.MaxLength = 20;
			this.textDivisionNo.Name = "textDivisionNo";
			this.textDivisionNo.Size = new System.Drawing.Size(107,20);
			this.textDivisionNo.TabIndex = 3;
			// 
			// labelDivisionDash
			// 
			this.labelDivisionDash.Location = new System.Drawing.Point(244,224);
			this.labelDivisionDash.Name = "labelDivisionDash";
			this.labelDivisionDash.Size = new System.Drawing.Size(31,16);
			this.labelDivisionDash.TabIndex = 111;
			this.labelDivisionDash.Text = "--";
			// 
			// comboClaimForm
			// 
			this.comboClaimForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClaimForm.Location = new System.Drawing.Point(112,364);
			this.comboClaimForm.MaxDropDownItems = 30;
			this.comboClaimForm.Name = "comboClaimForm";
			this.comboClaimForm.Size = new System.Drawing.Size(212,21);
			this.comboClaimForm.TabIndex = 6;
			// 
			// comboFeeSched
			// 
			this.comboFeeSched.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFeeSched.Location = new System.Drawing.Point(112,342);
			this.comboFeeSched.MaxDropDownItems = 30;
			this.comboFeeSched.Name = "comboFeeSched";
			this.comboFeeSched.Size = new System.Drawing.Size(212,21);
			this.comboFeeSched.TabIndex = 5;
			// 
			// groupCoPay
			// 
			this.groupCoPay.Controls.Add(this.label12);
			this.groupCoPay.Controls.Add(this.comboAllowedFeeSched);
			this.groupCoPay.Controls.Add(this.label11);
			this.groupCoPay.Controls.Add(this.label3);
			this.groupCoPay.Controls.Add(this.comboCopay);
			this.groupCoPay.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupCoPay.Location = new System.Drawing.Point(5,387);
			this.groupCoPay.Name = "groupCoPay";
			this.groupCoPay.Size = new System.Drawing.Size(404,99);
			this.groupCoPay.TabIndex = 7;
			this.groupCoPay.TabStop = false;
			this.groupCoPay.Text = "Other Fee Schedules";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(1,67);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(105,27);
			this.label12.TabIndex = 111;
			this.label12.Text = "Carrier Allowed Amounts";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboAllowedFeeSched
			// 
			this.comboAllowedFeeSched.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAllowedFeeSched.Location = new System.Drawing.Point(107,67);
			this.comboAllowedFeeSched.MaxDropDownItems = 30;
			this.comboAllowedFeeSched.Name = "comboAllowedFeeSched";
			this.comboAllowedFeeSched.Size = new System.Drawing.Size(212,21);
			this.comboAllowedFeeSched.TabIndex = 1;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(1,36);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(105,26);
			this.label11.TabIndex = 109;
			this.label11.Text = "Patient Co-pay Amounts";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(1,19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(390,17);
			this.label3.TabIndex = 106;
			this.label3.Text = "Don\'t use these unless you understand how they will affect your estimates";
			// 
			// comboCopay
			// 
			this.comboCopay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCopay.Location = new System.Drawing.Point(107,37);
			this.comboCopay.MaxDropDownItems = 30;
			this.comboCopay.Name = "comboCopay";
			this.comboCopay.Size = new System.Drawing.Size(212,21);
			this.comboCopay.TabIndex = 0;
			// 
			// comboElectIDdescript
			// 
			this.comboElectIDdescript.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboElectIDdescript.Location = new System.Drawing.Point(156,111);
			this.comboElectIDdescript.MaxDropDownItems = 30;
			this.comboElectIDdescript.Name = "comboElectIDdescript";
			this.comboElectIDdescript.Size = new System.Drawing.Size(237,21);
			this.comboElectIDdescript.TabIndex = 125;
			this.comboElectIDdescript.SelectedIndexChanged += new System.EventHandler(this.comboElectIDdescript_SelectedIndexChanged);
			// 
			// butCanadaEligibility
			// 
			this.butCanadaEligibility.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCanadaEligibility.Autosize = true;
			this.butCanadaEligibility.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCanadaEligibility.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCanadaEligibility.CornerRadius = 4F;
			this.butCanadaEligibility.Location = new System.Drawing.Point(271,33);
			this.butCanadaEligibility.Name = "butCanadaEligibility";
			this.butCanadaEligibility.Size = new System.Drawing.Size(79,21);
			this.butCanadaEligibility.TabIndex = 76;
			this.butCanadaEligibility.Text = "Eligibility";
			this.toolTip1.SetToolTip(this.butCanadaEligibility,"Edit all the similar plans at once");
			this.butCanadaEligibility.Click += new System.EventHandler(this.butCanadaEligibility_Click);
			// 
			// butBenefitNotes
			// 
			this.butBenefitNotes.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBenefitNotes.Autosize = true;
			this.butBenefitNotes.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBenefitNotes.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBenefitNotes.CornerRadius = 4F;
			this.butBenefitNotes.Location = new System.Drawing.Point(378,22);
			this.butBenefitNotes.Name = "butBenefitNotes";
			this.butBenefitNotes.Size = new System.Drawing.Size(82,21);
			this.butBenefitNotes.TabIndex = 2;
			this.butBenefitNotes.Text = "View Benefits";
			this.toolTip1.SetToolTip(this.butBenefitNotes,"Edit all the similar plans at once");
			this.butBenefitNotes.Click += new System.EventHandler(this.butBenefitNotes_Click);
			// 
			// butIapFind
			// 
			this.butIapFind.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butIapFind.Autosize = true;
			this.butIapFind.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butIapFind.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butIapFind.CornerRadius = 4F;
			this.butIapFind.Location = new System.Drawing.Point(132,33);
			this.butIapFind.Name = "butIapFind";
			this.butIapFind.Size = new System.Drawing.Size(82,21);
			this.butIapFind.TabIndex = 1;
			this.butIapFind.Text = "Find Plan";
			this.toolTip1.SetToolTip(this.butIapFind,"Edit all the similar plans at once");
			this.butIapFind.Click += new System.EventHandler(this.butIapFind_Click);
			// 
			// butImportTrojan
			// 
			this.butImportTrojan.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butImportTrojan.Autosize = true;
			this.butImportTrojan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butImportTrojan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butImportTrojan.CornerRadius = 4F;
			this.butImportTrojan.Location = new System.Drawing.Point(132,10);
			this.butImportTrojan.Name = "butImportTrojan";
			this.butImportTrojan.Size = new System.Drawing.Size(82,21);
			this.butImportTrojan.TabIndex = 0;
			this.butImportTrojan.Text = "Import";
			this.toolTip1.SetToolTip(this.butImportTrojan,"Edit all the similar plans at once");
			this.butImportTrojan.Click += new System.EventHandler(this.butImportTrojan_Click);
			// 
			// labelDrop
			// 
			this.labelDrop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelDrop.Location = new System.Drawing.Point(101,72);
			this.labelDrop.Name = "labelDrop";
			this.labelDrop.Size = new System.Drawing.Size(554,15);
			this.labelDrop.TabIndex = 124;
			this.labelDrop.Text = "Drop a plan when a patient changes carriers or is no longer covered.  This does n" +
    "ot delete the plan.";
			this.labelDrop.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupRequestBen
			// 
			this.groupRequestBen.Controls.Add(this.labelCanadaEligibility);
			this.groupRequestBen.Controls.Add(this.butCanadaEligibility);
			this.groupRequestBen.Controls.Add(this.butBenefitNotes);
			this.groupRequestBen.Controls.Add(this.labelTrojan);
			this.groupRequestBen.Controls.Add(this.butIapFind);
			this.groupRequestBen.Controls.Add(this.labelIAP);
			this.groupRequestBen.Controls.Add(this.butImportTrojan);
			this.groupRequestBen.Controls.Add(this.labelTrojanID);
			this.groupRequestBen.Controls.Add(this.textTrojanID);
			this.groupRequestBen.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupRequestBen.Location = new System.Drawing.Point(468,271);
			this.groupRequestBen.Name = "groupRequestBen";
			this.groupRequestBen.Size = new System.Drawing.Size(502,59);
			this.groupRequestBen.TabIndex = 10;
			this.groupRequestBen.TabStop = false;
			this.groupRequestBen.Text = "Request Benefits";
			// 
			// labelCanadaEligibility
			// 
			this.labelCanadaEligibility.Location = new System.Drawing.Point(225,38);
			this.labelCanadaEligibility.Name = "labelCanadaEligibility";
			this.labelCanadaEligibility.Size = new System.Drawing.Size(45,15);
			this.labelCanadaEligibility.TabIndex = 77;
			this.labelCanadaEligibility.Text = "CDAnet";
			this.labelCanadaEligibility.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelTrojan
			// 
			this.labelTrojan.Location = new System.Drawing.Point(59,15);
			this.labelTrojan.Name = "labelTrojan";
			this.labelTrojan.Size = new System.Drawing.Size(71,15);
			this.labelTrojan.TabIndex = 75;
			this.labelTrojan.Text = "Trojan:";
			this.labelTrojan.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelIAP
			// 
			this.labelIAP.Location = new System.Drawing.Point(2,36);
			this.labelIAP.Name = "labelIAP";
			this.labelIAP.Size = new System.Drawing.Size(128,15);
			this.labelIAP.TabIndex = 73;
			this.labelIAP.Text = "Insurance Answers Plus:";
			this.labelIAP.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelTrojanID
			// 
			this.labelTrojanID.Location = new System.Drawing.Point(218,14);
			this.labelTrojanID.Name = "labelTrojanID";
			this.labelTrojanID.Size = new System.Drawing.Size(23,15);
			this.labelTrojanID.TabIndex = 9;
			this.labelTrojanID.Text = "ID";
			this.labelTrojanID.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textTrojanID
			// 
			this.textTrojanID.Location = new System.Drawing.Point(245,10);
			this.textTrojanID.MaxLength = 30;
			this.textTrojanID.Name = "textTrojanID";
			this.textTrojanID.Size = new System.Drawing.Size(109,20);
			this.textTrojanID.TabIndex = 8;
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label26.Location = new System.Drawing.Point(20,22);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(148,14);
			this.label26.TabIndex = 127;
			this.label26.Text = "Relationship to Subscriber";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlText;
			this.panel1.Location = new System.Drawing.Point(0,90);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(988,2);
			this.panel1.TabIndex = 128;
			// 
			// comboRelationship
			// 
			this.comboRelationship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboRelationship.Location = new System.Drawing.Point(170,18);
			this.comboRelationship.MaxDropDownItems = 30;
			this.comboRelationship.Name = "comboRelationship";
			this.comboRelationship.Size = new System.Drawing.Size(151,21);
			this.comboRelationship.TabIndex = 0;
			// 
			// label31
			// 
			this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label31.Location = new System.Drawing.Point(396,23);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(109,14);
			this.label31.TabIndex = 130;
			this.label31.Text = "Order";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIsPending
			// 
			this.checkIsPending.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsPending.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsPending.Location = new System.Drawing.Point(396,42);
			this.checkIsPending.Name = "checkIsPending";
			this.checkIsPending.Size = new System.Drawing.Size(125,16);
			this.checkIsPending.TabIndex = 3;
			this.checkIsPending.Text = "Pending";
			this.checkIsPending.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label32
			// 
			this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label32.Location = new System.Drawing.Point(5,94);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(304,19);
			this.label32.TabIndex = 134;
			this.label32.Text = "Insurance Plan Information";
			// 
			// label33
			// 
			this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label33.Location = new System.Drawing.Point(5,0);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(188,19);
			this.label33.TabIndex = 135;
			this.label33.Text = "Patient Information";
			// 
			// listAdj
			// 
			this.listAdj.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.listAdj.Items.AddRange(new object[] {
            "03/05/2001       Ins Used:  $124.00       Ded Used:  $50.00",
            "03/05/2002       Ins Used:  $0.00       Ded Used:  $50.00"});
			this.listAdj.Location = new System.Drawing.Point(613,28);
			this.listAdj.Name = "listAdj";
			this.listAdj.Size = new System.Drawing.Size(341,56);
			this.listAdj.TabIndex = 137;
			this.listAdj.DoubleClick += new System.EventHandler(this.listAdj_DoubleClick);
			// 
			// label35
			// 
			this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label35.Location = new System.Drawing.Point(613,8);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(218,17);
			this.label35.TabIndex = 138;
			this.label35.Text = "Adjustments to Insurance Benefits: ";
			this.label35.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textPatID
			// 
			this.textPatID.Font = new System.Drawing.Font("Microsoft Sans Serif",8F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textPatID.Location = new System.Drawing.Point(170,40);
			this.textPatID.MaxLength = 100;
			this.textPatID.Name = "textPatID";
			this.textPatID.Size = new System.Drawing.Size(151,20);
			this.textPatID.TabIndex = 1;
			// 
			// label36
			// 
			this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif",8F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label36.Location = new System.Drawing.Point(30,42);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(138,16);
			this.label36.TabIndex = 143;
			this.label36.Text = "Optional Patient ID";
			this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelPat
			// 
			this.panelPat.Controls.Add(this.comboRelationship);
			this.panelPat.Controls.Add(this.label33);
			this.panelPat.Controls.Add(this.textOrdinal);
			this.panelPat.Controls.Add(this.butAdjAdd);
			this.panelPat.Controls.Add(this.listAdj);
			this.panelPat.Controls.Add(this.label35);
			this.panelPat.Controls.Add(this.textPatID);
			this.panelPat.Controls.Add(this.label36);
			this.panelPat.Controls.Add(this.labelDrop);
			this.panelPat.Controls.Add(this.butDrop);
			this.panelPat.Controls.Add(this.label26);
			this.panelPat.Controls.Add(this.label31);
			this.panelPat.Controls.Add(this.checkIsPending);
			this.panelPat.Location = new System.Drawing.Point(0,0);
			this.panelPat.Name = "panelPat";
			this.panelPat.Size = new System.Drawing.Size(982,90);
			this.panelPat.TabIndex = 15;
			// 
			// textOrdinal
			// 
			this.textOrdinal.Location = new System.Drawing.Point(508,22);
			this.textOrdinal.MaxVal = 10;
			this.textOrdinal.MinVal = 1;
			this.textOrdinal.Name = "textOrdinal";
			this.textOrdinal.Size = new System.Drawing.Size(45,20);
			this.textOrdinal.TabIndex = 2;
			// 
			// butAdjAdd
			// 
			this.butAdjAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdjAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdjAdd.Autosize = true;
			this.butAdjAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdjAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdjAdd.CornerRadius = 4F;
			this.butAdjAdd.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butAdjAdd.Location = new System.Drawing.Point(895,6);
			this.butAdjAdd.Name = "butAdjAdd";
			this.butAdjAdd.Size = new System.Drawing.Size(59,21);
			this.butAdjAdd.TabIndex = 4;
			this.butAdjAdd.Text = "Add";
			this.butAdjAdd.Click += new System.EventHandler(this.butAdjAdd_Click);
			// 
			// butDrop
			// 
			this.butDrop.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDrop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDrop.Autosize = true;
			this.butDrop.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDrop.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDrop.CornerRadius = 4F;
			this.butDrop.Location = new System.Drawing.Point(7,67);
			this.butDrop.Name = "butDrop";
			this.butDrop.Size = new System.Drawing.Size(72,21);
			this.butDrop.TabIndex = 5;
			this.butDrop.Text = "Drop";
			this.butDrop.Click += new System.EventHandler(this.butDrop_Click);
			// 
			// textPlanNum
			// 
			this.textPlanNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textPlanNum.Location = new System.Drawing.Point(310,676);
			this.textPlanNum.Name = "textPlanNum";
			this.textPlanNum.Size = new System.Drawing.Size(100,20);
			this.textPlanNum.TabIndex = 148;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(7,13);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(94,15);
			this.label17.TabIndex = 152;
			this.label17.Text = "Carrier";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkApplyAll
			// 
			this.checkApplyAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApplyAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkApplyAll.Location = new System.Drawing.Point(468,679);
			this.checkApplyAll.Name = "checkApplyAll";
			this.checkApplyAll.Size = new System.Drawing.Size(323,21);
			this.checkApplyAll.TabIndex = 12;
			this.checkApplyAll.Text = "Apply changes to all identical insurance plans";
			this.checkApplyAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApplyAll.UseVisualStyleBackColor = true;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(5,563);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(399,15);
			this.label18.TabIndex = 156;
			this.label18.Text = "Plan Note.  Always applies to all similar plans, not just this one.";
			this.label18.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupPlan
			// 
			this.groupPlan.Controls.Add(this.textEmployer);
			this.groupPlan.Controls.Add(this.groupCarrier);
			this.groupPlan.Controls.Add(this.textDivisionNo);
			this.groupPlan.Controls.Add(this.checkIsMedical);
			this.groupPlan.Controls.Add(this.textGroupNum);
			this.groupPlan.Controls.Add(this.labelGroupNum);
			this.groupPlan.Controls.Add(this.label8);
			this.groupPlan.Controls.Add(this.textGroupName);
			this.groupPlan.Controls.Add(this.comboLinked);
			this.groupPlan.Controls.Add(this.textLinkedNum);
			this.groupPlan.Controls.Add(this.label16);
			this.groupPlan.Controls.Add(this.label4);
			this.groupPlan.Controls.Add(this.labelDivisionDash);
			this.groupPlan.Location = new System.Drawing.Point(0,2);
			this.groupPlan.Name = "groupPlan";
			this.groupPlan.Size = new System.Drawing.Size(425,264);
			this.groupPlan.TabIndex = 0;
			this.groupPlan.TabStop = false;
			this.groupPlan.Text = "Plan";
			// 
			// groupCarrier
			// 
			this.groupCarrier.Controls.Add(this.textPhone);
			this.groupCarrier.Controls.Add(this.textAddress);
			this.groupCarrier.Controls.Add(this.comboElectIDdescript);
			this.groupCarrier.Controls.Add(this.textElectID);
			this.groupCarrier.Controls.Add(this.butSearch);
			this.groupCarrier.Controls.Add(this.textAddress2);
			this.groupCarrier.Controls.Add(this.textZip);
			this.groupCarrier.Controls.Add(this.checkNoSendElect);
			this.groupCarrier.Controls.Add(this.label10);
			this.groupCarrier.Controls.Add(this.textCity);
			this.groupCarrier.Controls.Add(this.label7);
			this.groupCarrier.Controls.Add(this.textCarrier);
			this.groupCarrier.Controls.Add(this.labelElectronicID);
			this.groupCarrier.Controls.Add(this.label21);
			this.groupCarrier.Controls.Add(this.label17);
			this.groupCarrier.Controls.Add(this.textState);
			this.groupCarrier.Controls.Add(this.labelCitySTZip);
			this.groupCarrier.Location = new System.Drawing.Point(10,44);
			this.groupCarrier.Name = "groupCarrier";
			this.groupCarrier.Size = new System.Drawing.Size(399,155);
			this.groupCarrier.TabIndex = 154;
			this.groupCarrier.TabStop = false;
			this.groupCarrier.Text = "Carrier";
			// 
			// butSearch
			// 
			this.butSearch.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSearch.Autosize = true;
			this.butSearch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearch.CornerRadius = 4F;
			this.butSearch.Location = new System.Drawing.Point(86,132);
			this.butSearch.Name = "butSearch";
			this.butSearch.Size = new System.Drawing.Size(84,20);
			this.butSearch.TabIndex = 124;
			this.butSearch.Text = "Search IDs";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
			// 
			// panelPlan
			// 
			this.panelPlan.AutoScroll = true;
			this.panelPlan.AutoScrollMargin = new System.Drawing.Size(0,10);
			this.panelPlan.BackColor = System.Drawing.SystemColors.Control;
			this.panelPlan.Controls.Add(this.checkDedBeforePerc);
			this.panelPlan.Controls.Add(this.checkShowBaseUnits);
			this.panelPlan.Controls.Add(this.textDentaide);
			this.panelPlan.Controls.Add(this.labelDentaide);
			this.panelPlan.Controls.Add(this.comboFeeSched);
			this.panelPlan.Controls.Add(this.groupPlan);
			this.panelPlan.Controls.Add(this.comboFilingCode);
			this.panelPlan.Controls.Add(this.groupCoPay);
			this.panelPlan.Controls.Add(this.comboPlanType);
			this.panelPlan.Controls.Add(this.label13);
			this.panelPlan.Controls.Add(this.comboClaimForm);
			this.panelPlan.Controls.Add(this.label1);
			this.panelPlan.Controls.Add(this.label14);
			this.panelPlan.Controls.Add(this.label23);
			this.panelPlan.Controls.Add(this.checkAlternateCode);
			this.panelPlan.Controls.Add(this.checkClaimsUseUCR);
			this.panelPlan.Location = new System.Drawing.Point(8,116);
			this.panelPlan.Name = "panelPlan";
			this.panelPlan.Size = new System.Drawing.Size(454,438);
			this.panelPlan.TabIndex = 157;
			// 
			// checkShowBaseUnits
			// 
			this.checkShowBaseUnits.Location = new System.Drawing.Point(112,536);
			this.checkShowBaseUnits.Name = "checkShowBaseUnits";
			this.checkShowBaseUnits.Size = new System.Drawing.Size(289,17);
			this.checkShowBaseUnits.TabIndex = 162;
			this.checkShowBaseUnits.Text = "Claims show base units (Does not affect billed amount)";
			this.checkShowBaseUnits.UseVisualStyleBackColor = true;
			// 
			// textDentaide
			// 
			this.textDentaide.Location = new System.Drawing.Point(158,515);
			this.textDentaide.MaxVal = 255;
			this.textDentaide.MinVal = 0;
			this.textDentaide.Name = "textDentaide";
			this.textDentaide.Size = new System.Drawing.Size(55,20);
			this.textDentaide.TabIndex = 161;
			// 
			// labelDentaide
			// 
			this.labelDentaide.Location = new System.Drawing.Point(12,518);
			this.labelDentaide.Name = "labelDentaide";
			this.labelDentaide.Size = new System.Drawing.Size(140,19);
			this.labelDentaide.TabIndex = 160;
			this.labelDentaide.Text = "Dentaide Card Sequence";
			this.labelDentaide.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboFilingCode
			// 
			this.comboFilingCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFilingCode.Location = new System.Drawing.Point(112,491);
			this.comboFilingCode.MaxDropDownItems = 30;
			this.comboFilingCode.Name = "comboFilingCode";
			this.comboFilingCode.Size = new System.Drawing.Size(212,21);
			this.comboFilingCode.TabIndex = 159;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(10,493);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(100,19);
			this.label13.TabIndex = 158;
			this.label13.Text = "Filing Code";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butPick
			// 
			this.butPick.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPick.Autosize = true;
			this.butPick.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPick.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPick.CornerRadius = 4F;
			this.butPick.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPick.Location = new System.Drawing.Point(327,93);
			this.butPick.Name = "butPick";
			this.butPick.Size = new System.Drawing.Size(90,23);
			this.butPick.TabIndex = 153;
			this.butPick.Text = "Pick From List";
			this.butPick.Click += new System.EventHandler(this.butPick_Click);
			// 
			// butEditBenefits
			// 
			this.butEditBenefits.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEditBenefits.Autosize = true;
			this.butEditBenefits.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditBenefits.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditBenefits.CornerRadius = 4F;
			this.butEditBenefits.Location = new System.Drawing.Point(468,649);
			this.butEditBenefits.Name = "butEditBenefits";
			this.butEditBenefits.Size = new System.Drawing.Size(90,24);
			this.butEditBenefits.TabIndex = 11;
			this.butEditBenefits.Text = "Edit Benefits";
			this.butEditBenefits.Click += new System.EventHandler(this.butEditBenefits_Click);
			// 
			// textPlanNote
			// 
			this.textPlanNote.AcceptsReturn = true;
			this.textPlanNote.Location = new System.Drawing.Point(14,581);
			this.textPlanNote.Multiline = true;
			this.textPlanNote.Name = "textPlanNote";
			this.textPlanNote.QuickPasteType = OpenDentBusiness.QuickPasteType.InsPlan;
			this.textPlanNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textPlanNote.Size = new System.Drawing.Size(395,85);
			this.textPlanNote.TabIndex = 8;
			this.textPlanNote.Text = "1\r\n2\r\n3 lines will show here in 46 vert.\r\n4 lines will show here in 59 vert.\r\n5 l" +
    "ines in 72 vert\r\n6 in 85";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(801,671);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 13;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// gridBenefits
			// 
			this.gridBenefits.HScrollVisible = false;
			this.gridBenefits.Location = new System.Drawing.Point(468,334);
			this.gridBenefits.Name = "gridBenefits";
			this.gridBenefits.ScrollValue = 0;
			this.gridBenefits.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridBenefits.Size = new System.Drawing.Size(502,314);
			this.gridBenefits.TabIndex = 146;
			this.gridBenefits.Title = "Benefit Information";
			this.gridBenefits.TranslationName = "TableBenefits";
			// 
			// butLabel
			// 
			this.butLabel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butLabel.Autosize = true;
			this.butLabel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabel.CornerRadius = 4F;
			this.butLabel.Image = global::OpenDental.Properties.Resources.butLabel;
			this.butLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabel.Location = new System.Drawing.Point(131,671);
			this.butLabel.Name = "butLabel";
			this.butLabel.Size = new System.Drawing.Size(81,26);
			this.butLabel.TabIndex = 125;
			this.butLabel.Text = "Label";
			this.butLabel.Click += new System.EventHandler(this.butLabel_Click);
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
			this.butDelete.Location = new System.Drawing.Point(13,671);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81,26);
			this.butDelete.TabIndex = 112;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
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
			this.butCancel.Location = new System.Drawing.Point(896,671);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 14;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// checkDedBeforePerc
			// 
			this.checkDedBeforePerc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDedBeforePerc.Location = new System.Drawing.Point(112,325);
			this.checkDedBeforePerc.Name = "checkDedBeforePerc";
			this.checkDedBeforePerc.Size = new System.Drawing.Size(275,16);
			this.checkDedBeforePerc.TabIndex = 163;
			this.checkDedBeforePerc.Text = "Apply deductible before percentage";
			// 
			// FormInsPlan
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(982,700);
			this.Controls.Add(this.panelPlan);
			this.Controls.Add(this.butPick);
			this.Controls.Add(this.butEditBenefits);
			this.Controls.Add(this.textPlanNote);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.checkApplyAll);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textPlanNum);
			this.Controls.Add(this.gridBenefits);
			this.Controls.Add(this.panelPat);
			this.Controls.Add(this.butLabel);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label32);
			this.Controls.Add(this.groupRequestBen);
			this.Controls.Add(this.groupSubscriber);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsPlan";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Insurance Plan";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormInsPlan_Closing);
			this.Load += new System.EventHandler(this.FormInsPlan_Load);
			this.groupSubscriber.ResumeLayout(false);
			this.groupSubscriber.PerformLayout();
			this.groupCoPay.ResumeLayout(false);
			this.groupRequestBen.ResumeLayout(false);
			this.groupRequestBen.PerformLayout();
			this.panelPat.ResumeLayout(false);
			this.panelPat.PerformLayout();
			this.groupPlan.ResumeLayout(false);
			this.groupPlan.PerformLayout();
			this.groupCarrier.ResumeLayout(false);
			this.groupCarrier.PerformLayout();
			this.panelPlan.ResumeLayout(false);
			this.panelPlan.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormInsPlan_Load(object sender,System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			PlanCurOld=PlanCur.Copy();
			int patPlanNum=0;
			if(PatPlanCur!=null) {
				patPlanNum=PatPlanCur.PatPlanNum;
			}
			if(IsForAll) {
				butPick.Visible=false;//This prevents an infinite loop
				//groupRequestBen.Visible=false;//might try to make this functional later, but not now.
				//groupRequestBen:---------------------------------------------
				butImportTrojan.Visible=false;
				labelIAP.Visible=false;
				butIapFind.Visible=false;
				textTrojanID.Enabled=false;//view only
				butBenefitNotes.Visible=false;
				labelCanadaEligibility.Visible=false;
				butCanadaEligibility.Visible=false;
				//end of groupRequestBen
				groupSubscriber.Visible=false;
				checkApplyAll.Checked=true;
				checkApplyAll.Visible=false;
				benefitList=Benefits.RefreshForAll(PlanCur);
				if(IsReadOnly) {
					butOK.Enabled=false;
				}
				butDelete.Visible=false;
			}
			else {
				benefitList=Benefits.RefreshForPlan(PlanCur.PlanNum,patPlanNum);
			}
			benefitListOld=new List<Benefit>(benefitList);
#if DEBUG
			textPlanNum.Text=PlanCur.PlanNum.ToString();
#else
				textPlanNum.Visible=false;
#endif
			if(((Pref)PrefB.HList["EasyHideCapitation"]).ValueString=="1") {
				//groupCoPay.Visible=false;
				//comboCopay.Visible=false;
			}
			if(((Pref)PrefB.HList["EasyHideMedicaid"]).ValueString=="1") {
				checkAlternateCode.Visible=false;
			}
			if(((Pref)PrefB.HList["EasyHideAdvancedIns"]).ValueString=="1") {
				//textOrthoMax.Visible=false;
				//labelOrthoMax.Visible=false;
				//panelAdvancedIns.Visible=false;
				//panelSynch.Visible=false;
			}
			if(PrefB.GetBool("InsurancePlansShared")) {
				checkApplyAll.Checked=true;
			}
			if(IsNewPlan) {
				checkApplyAll.Checked=false;
				checkApplyAll.Visible=false;//because it wouldn't make sense to apply anything to "all"
			}
			Program ProgramCur=Programs.GetCur("Trojan");
			if(ProgramCur!=null && ProgramCur.Enabled) {
				textTrojanID.Text=PlanCur.TrojanID;
			}
			else {
				labelTrojan.Visible=false;
				labelTrojanID.Visible=false;
				butImportTrojan.Visible=false;
				textTrojanID.Visible=false;
			}
			ProgramCur=Programs.GetCur("IAP");
			if(ProgramCur==null || !ProgramCur.Enabled) {
				labelIAP.Visible=false;
				butIapFind.Visible=false;
			}
			if(!butIapFind.Visible && !butImportTrojan.Visible) {
				butBenefitNotes.Visible=false;
			}
			//FillPatData------------------------------
			if(PatPlanCur==null) {
				panelPat.Visible=false;
			}
			else {
				textOrdinal.Text=PatPlanCur.Ordinal.ToString();
				checkIsPending.Checked=PatPlanCur.IsPending;
				comboRelationship.Items.Clear();
				for(int i=0;i<Enum.GetNames(typeof(Relat)).Length;i++) {
					comboRelationship.Items.Add(Lan.g("enumRelat",Enum.GetNames(typeof(Relat))[i]));
					if((int)PatPlanCur.Relationship==i) {
						comboRelationship.SelectedIndex=i;
					}
				}
				textPatID.Text=PatPlanCur.PatID;
				FillPatientAdjustments();
			}
			if(!IsForAll) {
				textSubscriber.Text=Patients.GetLim(PlanCur.Subscriber).GetNameLF();
				textSubscriberID.Text=PlanCur.SubscriberID;
				if(PlanCur.DateEffective.Year < 1880)
					textDateEffect.Text="";
				else
					textDateEffect.Text=PlanCur.DateEffective.ToString("d");
				if(PlanCur.DateTerm.Year < 1880)
					textDateTerm.Text="";
				else
					textDateTerm.Text=PlanCur.DateTerm.ToString("d");
			}
			FeeSchedsStandard=DefB.GetFeeSchedList("");
			FeeSchedsCopay=DefB.GetFeeSchedList("C");
			FeeSchedsAllowed=DefB.GetFeeSchedList("A");
			Clearinghouse clearhouse=Clearinghouses.GetDefault();
			if(clearhouse==null || clearhouse.CommBridge!=EclaimsCommBridge.ClaimConnect) {
				butEligibility.Visible=false;
			}
			FillFormWithPlanCur();
			FillBenefits();
			Cursor=Cursors.Default;
		}

		///<summary>Uses PlanCur to fill out the information on the form.  Called once on startup and also if user picks a plan from template list.</summary>
		private void FillFormWithPlanCur() {
			Cursor=Cursors.WaitCursor;
			textEmployer.Text=Employers.GetName(PlanCur.EmployerNum);
			textGroupName.Text=PlanCur.GroupName;
			textGroupNum.Text=PlanCur.GroupNum;
			textDivisionNo.Text=PlanCur.DivisionNo;//only visible in Canada
			comboPlanType.Items.Clear();
			comboPlanType.Items.Add(Lan.g(this,"Category Percentage"));
			if(PlanCur.PlanType=="")
				comboPlanType.SelectedIndex=0;
			comboPlanType.Items.Add(Lan.g(this,"PPO Percentage"));
			if(PlanCur.PlanType=="p")
				comboPlanType.SelectedIndex=1;
			comboPlanType.Items.Add(Lan.g(this,"Medicaid or Flat Co-pay"));
			if(PlanCur.PlanType=="f")
				comboPlanType.SelectedIndex=2;
			if(!PrefB.GetBool("EasyHideCapitation")) {
				comboPlanType.Items.Add(Lan.g(this,"Capitation"));
				if(PlanCur.PlanType=="c")
					comboPlanType.SelectedIndex=3;
			}
			checkAlternateCode.Checked=PlanCur.UseAltCode;
			checkIsMedical.Checked=PlanCur.IsMedical;
			checkClaimsUseUCR.Checked=PlanCur.ClaimsUseUCR;
			checkDedBeforePerc.Checked=PlanCur.DedBeforePerc;
			checkShowBaseUnits.Checked=PlanCur.ShowBaseUnits;
			comboFeeSched.Items.Clear();
			comboFeeSched.Items.Add(Lan.g(this,"none"));
			comboFeeSched.SelectedIndex=0;
			for(int i=0;i<FeeSchedsStandard.Length;i++) {
				comboFeeSched.Items.Add(FeeSchedsStandard[i].ItemName);
				if(FeeSchedsStandard[i].DefNum==PlanCur.FeeSched)
					comboFeeSched.SelectedIndex=i+1;
			}
			comboCopay.Items.Clear();
			comboCopay.Items.Add(Lan.g(this,"none"));
			comboCopay.SelectedIndex=0;
			for(int i=0;i<FeeSchedsCopay.Length;i++) {
				comboCopay.Items.Add(FeeSchedsCopay[i].ItemName);
				if(FeeSchedsCopay[i].DefNum==PlanCur.CopayFeeSched)
					comboCopay.SelectedIndex=i+1;
			}
			comboAllowedFeeSched.Items.Clear();
			comboAllowedFeeSched.Items.Add(Lan.g(this,"none"));
			comboAllowedFeeSched.SelectedIndex=0;
			for(int i=0;i<FeeSchedsAllowed.Length;i++) {
				comboAllowedFeeSched.Items.Add(FeeSchedsAllowed[i].ItemName);
				if(FeeSchedsAllowed[i].DefNum==PlanCur.AllowedFeeSched)
					comboAllowedFeeSched.SelectedIndex=i+1;
			}
			comboClaimForm.Items.Clear();
			for(int i=0;i<ClaimForms.ListShort.Length;i++) {
				comboClaimForm.Items.Add(ClaimForms.ListShort[i].Description);
				if(ClaimForms.ListShort[i].ClaimFormNum==PlanCur.ClaimFormNum) {
					comboClaimForm.SelectedIndex=i;
				}
			}
			if(comboClaimForm.Items.Count>0 && comboClaimForm.SelectedIndex==-1) {
				for(int i=0;i<ClaimForms.ListShort.Length;i++) {
					if(ClaimForms.ListShort[i].ClaimFormNum==PrefB.GetInt("DefaultClaimForm")) {
						comboClaimForm.SelectedIndex=i;
					}
				}
			}
			comboFilingCode.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(InsFilingCode)).Length;i++) {
				comboFilingCode.Items.Add(Enum.GetNames(typeof(InsFilingCode))[i]);
				if((int)PlanCur.FilingCode==i) {
					comboFilingCode.SelectedIndex=i;
				}
			}
			FillCarrier(PlanCur.CarrierNum);
			int excludePlan=-1;
			if(!IsForAll){
				excludePlan=PlanCur.PlanNum;
			}
			string[] samePlans=InsPlans.GetSubscribersForSamePlans(textEmployer.Text,textGroupName.Text,textGroupNum.Text,
				textDivisionNo.Text,textCarrier.Text,checkIsMedical.Checked,excludePlan);
			textLinkedNum.Text=samePlans.Length.ToString();
			comboLinked.Items.Clear();
			for(int i=0;i<samePlans.Length;i++) {
				comboLinked.Items.Add(samePlans[i]);
			}
			if(samePlans.Length>0)
				comboLinked.SelectedIndex=0;
			checkRelease.Checked=PlanCur.ReleaseInfo;
			checkAssign.Checked=PlanCur.AssignBen;
			textPlanNote.Text=PlanCur.PlanNote;
			textSubscNote.Text=PlanCur.SubscNote;
			if(PlanCur.DentaideCardSequence==0){
				textDentaide.Text="";
			}
			else{
				textDentaide.Text=PlanCur.DentaideCardSequence.ToString();
			}
			//if(PlanCur.BenefitNotes==""){
			//	butBenefitNotes.Enabled=false;
			//}
			Cursor=Cursors.Default;
		}

		private void FillPatientAdjustments() {
			ClaimProc[] ClaimProcList=ClaimProcs.Refresh(PatPlanCur.PatNum);
			AdjAL=new ArrayList();//move selected claimprocs into ALAdj
			for(int i=0;i<ClaimProcList.Length;i++) {
				if(ClaimProcList[i].PlanNum==PlanCur.PlanNum
					&& ClaimProcList[i].Status==ClaimProcStatus.Adjustment) {
					AdjAL.Add(ClaimProcList[i]);
				}
			}
			listAdj.Items.Clear();
			string s;
			for(int i=0;i<AdjAL.Count;i++) {
				s=((ClaimProc)AdjAL[i]).ProcDate.ToShortDateString()+"       Ins Used:  "
					+((ClaimProc)AdjAL[i]).InsPayAmt.ToString("F")+"       Ded Used:  "
					+((ClaimProc)AdjAL[i]).DedApplied.ToString("F");
				listAdj.Items.Add(s);
			}
		}

		///<summary>Fills the carrier fields on the form based on the specified carrierNum.</summary>
		private void FillCarrier(int carrierNum) {
			Carrier carrier=Carriers.GetCarrier(carrierNum);
			textCarrier.Text=carrier.CarrierName;
			textPhone.Text=carrier.Phone;
			textAddress.Text=carrier.Address;
			textAddress2.Text=carrier.Address2;
			textCity.Text=carrier.City;
			textState.Text=carrier.State;
			textZip.Text=carrier.Zip;
			textElectID.Text=carrier.ElectID;
			FillPayor();
			checkNoSendElect.Checked=carrier.NoSendElect;
		}

		///<summary>Only called from FillCarrier and textElectID_Validating. Fills comboElectIDdescript as appropriate.</summary>
		private void FillPayor() {
			//textElectIDdescript.Text=ElectIDs.GetDescript(textElectID.Text);
			comboElectIDdescript.Items.Clear();
			string[] payorNames=ElectIDs.GetDescripts(textElectID.Text);
			if(payorNames.Length>1) {
				comboElectIDdescript.Items.Add("multiple payors use this ID");
			}
			for(int i=0;i<payorNames.Length;i++) {
				comboElectIDdescript.Items.Add(payorNames[i]);
			}
			if(payorNames.Length>0) {
				comboElectIDdescript.SelectedIndex=0;
			}
		}

		private void comboElectIDdescript_SelectedIndexChanged(object sender,System.EventArgs e) {
			if(comboElectIDdescript.Items.Count>0) {
				comboElectIDdescript.SelectedIndex=0;//always show the first item in the list
			}
		}

		private void comboPlanType_SelectionChangeCommitted(object sender,System.EventArgs e) {
			//MessageBox.Show(InsPlans.Cur.PlanType+","+listPlanType.SelectedIndex.ToString());
			if((PlanCur.PlanType=="" || PlanCur.PlanType=="p")
				&& (comboPlanType.SelectedIndex==2 || comboPlanType.SelectedIndex==3)) 
			{
				if(!MsgBox.Show(this,true,"This will clear all percentages. Continue?")) {
					if(PlanCur.PlanType==""){
						comboPlanType.SelectedIndex=0;
					}
					if(PlanCur.PlanType=="p") {
						comboPlanType.SelectedIndex=1;
					}
					return;
				}
				//Loop through the list backwards so i will be valid.
				for(int i=benefitList.Count-1;i>=0;i--) {
					if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Percentage) {
						benefitList.RemoveAt(i);
					}
				}
				//benefitList=new ArrayList();
				FillBenefits();
			}
			switch(comboPlanType.SelectedIndex) {
				case 0:
					PlanCur.PlanType="";
					break;
				case 1:
					PlanCur.PlanType="p";
					break;
				case 2:
					PlanCur.PlanType="f";
					break;
				case 3:
					PlanCur.PlanType="c";
					break;
			}
		}

		private void butAdjAdd_Click(object sender,System.EventArgs e) {
			ClaimProc ClaimProcCur=new ClaimProc();
			ClaimProcCur.PatNum=PatPlanCur.PatNum;
			ClaimProcCur.ProcDate=DateTime.Today;
			ClaimProcCur.Status=ClaimProcStatus.Adjustment;
			ClaimProcCur.PlanNum=PlanCur.PlanNum;
			FormInsAdj FormIA=new FormInsAdj(ClaimProcCur);
			FormIA.IsNew=true;
			FormIA.ShowDialog();
			FillPatientAdjustments();
		}

		private void listAdj_DoubleClick(object sender,System.EventArgs e) {
			if(listAdj.SelectedIndex==-1) {
				return;
			}
			FormInsAdj FormIA=new FormInsAdj((ClaimProc)AdjAL[listAdj.SelectedIndex]);
			FormIA.ShowDialog();
			FillPatientAdjustments();
		}

		///<summary></summary>
		private void butPick_Click(object sender,EventArgs e) {
			FormInsPlans FormIP=new FormInsPlans();
			FormIP.empText=textEmployer.Text;
			FormIP.carrierText=textCarrier.Text;
			FormIP.IsSelectMode=true;
			FormIP.ShowDialog();
			if(FormIP.DialogResult==DialogResult.Cancel) {
				return;
			}
			if(!IsNewPlan && !MsgBox.Show(this,true,"Are you sure you want to use the selected plan?  You should NOT use this if the patient is changing insurance.  Use the Drop button instead.")) {
				return;
			}
			//FillPlanCur();//this catches the non-synch fields
			//Non-synched fields:
			PlanCur.SubscriberID=textSubscriberID.Text;
			PlanCur.DateEffective=PIn.PDate(textDateEffect.Text);//can handle misformed dates.
			PlanCur.DateTerm=PIn.PDate(textDateTerm.Text);
			PlanCur.ReleaseInfo=checkRelease.Checked;
			PlanCur.AssignBen=checkAssign.Checked;
			PlanCur.SubscNote=textSubscNote.Text;
			//synched fields
			PlanCur.EmployerNum    =FormIP.SelectedPlan.EmployerNum;
			PlanCur.GroupName      =FormIP.SelectedPlan.GroupName;
			PlanCur.GroupNum       =FormIP.SelectedPlan.GroupNum;
			PlanCur.DivisionNo     =FormIP.SelectedPlan.DivisionNo;
			PlanCur.CarrierNum     =FormIP.SelectedPlan.CarrierNum;
			PlanCur.PlanType       =FormIP.SelectedPlan.PlanType;
			PlanCur.UseAltCode     =FormIP.SelectedPlan.UseAltCode;
			PlanCur.IsMedical      =FormIP.SelectedPlan.IsMedical;
			PlanCur.ClaimsUseUCR   =FormIP.SelectedPlan.ClaimsUseUCR;
			PlanCur.DedBeforePerc  =FormIP.SelectedPlan.DedBeforePerc;
			PlanCur.ShowBaseUnits = FormIP.SelectedPlan.ShowBaseUnits;
			PlanCur.FeeSched       =FormIP.SelectedPlan.FeeSched;
			PlanCur.CopayFeeSched  =FormIP.SelectedPlan.CopayFeeSched;
			PlanCur.ClaimFormNum   =FormIP.SelectedPlan.ClaimFormNum;
			PlanCur.AllowedFeeSched=FormIP.SelectedPlan.AllowedFeeSched;
			PlanCur.TrojanID       =FormIP.SelectedPlan.TrojanID;
			PlanCur.PlanNote       =FormIP.SelectedPlan.PlanNote;
			//PlanCur.Update();//updates to the db so that the synch info will show correctly
			FillFormWithPlanCur();
			//need to clear benefits for this plan now.  That way they won't be available as benefits of an 'identical' plan.
			Benefits.DeleteForPlan(PlanCur.PlanNum);
			benefitList=Benefits.RefreshForAll(PlanCur);//these benefits are only typical, not precise.
			//set all benefitNums to 0 to trigger having them saved as new
			for(int i=0;i<benefitList.Count;i++) {
				((Benefit)benefitList[i]).BenefitNum=0;
			}
			Benefits.UpdateList(benefitListOld,benefitList);//replaces all benefits for this plan in the database
			FillBenefits();
			//The new data on the form should be the basis for any 'changes to all'.
			PlanCurOld=PlanCur.Copy();
			benefitListOld=new List<Benefit>(benefitList);
			//It's as if we've just opened a new form now.
		}

		private void textEmployer_KeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
			//key up is used because that way it will trigger AFTER the textBox has been changed.
			if(e.KeyCode==Keys.Return) {
				listEmps.Visible=false;
				textGroupName.Focus();
				return;
			}
			if(textEmployer.Text=="") {
				listEmps.Visible=false;
				return;
			}
			if(e.KeyCode==Keys.Down) {
				if(listEmps.Items.Count==0) {
					return;
				}
				if(listEmps.SelectedIndex==-1) {
					listEmps.SelectedIndex=0;
					textEmployer.Text=listEmps.SelectedItem.ToString();
				}
				else if(listEmps.SelectedIndex==listEmps.Items.Count-1) {
					listEmps.SelectedIndex=-1;
					textEmployer.Text=empOriginal;
				}
				else {
					listEmps.SelectedIndex++;
					textEmployer.Text=listEmps.SelectedItem.ToString();
				}
				textEmployer.SelectionStart=textEmployer.Text.Length;
				return;
			}
			if(e.KeyCode==Keys.Up) {
				if(listEmps.Items.Count==0) {
					return;
				}
				if(listEmps.SelectedIndex==-1) {
					listEmps.SelectedIndex=listEmps.Items.Count-1;
					textEmployer.Text=listEmps.SelectedItem.ToString();
				}
				else if(listEmps.SelectedIndex==0) {
					listEmps.SelectedIndex=-1;
					textEmployer.Text=empOriginal;
				}
				else {
					listEmps.SelectedIndex--;
					textEmployer.Text=listEmps.SelectedItem.ToString();
				}
				textEmployer.SelectionStart=textEmployer.Text.Length;
				return;
			}
			if(textEmployer.Text.Length==1) {
				textEmployer.Text=textEmployer.Text.ToUpper();
				textEmployer.SelectionStart=1;
			}
			empOriginal=textEmployer.Text;//the original text is preserved when using up and down arrows
			listEmps.Items.Clear();
			similarEmps=Employers.GetSimilarNames(textEmployer.Text);
			for(int i=0;i<similarEmps.Count;i++) {
				listEmps.Items.Add(((Employer)similarEmps[i]).EmpName);
			}
			int h=13*similarEmps.Count+5;
			if(h > ClientSize.Height-listEmps.Top)
				h=ClientSize.Height-listEmps.Top;
			listEmps.Size=new Size(231,h);
			listEmps.Visible=true;
		}

		private void textEmployer_Leave(object sender,System.EventArgs e) {
			if(mouseIsInListEmps) {
				return;
			}
			listEmps.Visible=false;
		}

		private void listEmps_Click(object sender,System.EventArgs e) {
			textEmployer.Text=listEmps.SelectedItem.ToString();
			textEmployer.Focus();
			textEmployer.SelectionStart=textEmployer.Text.Length;
			listEmps.Visible=false;
		}

		private void listEmps_DoubleClick(object sender,System.EventArgs e) {
			//no longer used
			textEmployer.Text=listEmps.SelectedItem.ToString();
			textEmployer.Focus();
			textEmployer.SelectionStart=textEmployer.Text.Length;
			listEmps.Visible=false;
		}

		private void listEmps_MouseEnter(object sender,System.EventArgs e) {
			mouseIsInListEmps=true;
		}

		private void listEmps_MouseLeave(object sender,System.EventArgs e) {
			mouseIsInListEmps=false;
		}

		private void textCarrier_KeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
			if(e.KeyCode==Keys.Return) {
				if(listCars.SelectedIndex==-1) {
					textPhone.Focus();
				}
				else {
					FillCarrier(((Carrier)similarCars[listCars.SelectedIndex]).CarrierNum);
					textCarrier.Focus();
					textCarrier.SelectionStart=textCarrier.Text.Length;
				}
				listCars.Visible=false;
				return;
			}
			if(textCarrier.Text=="") {
				listCars.Visible=false;
				return;
			}
			if(e.KeyCode==Keys.Down) {
				if(listCars.Items.Count==0) {
					return;
				}
				if(listCars.SelectedIndex==-1) {
					listCars.SelectedIndex=0;
					textCarrier.Text=((Carrier)similarCars[listCars.SelectedIndex]).CarrierName;
				}
				else if(listCars.SelectedIndex==listCars.Items.Count-1) {
					listCars.SelectedIndex=-1;
					textCarrier.Text=carOriginal;
				}
				else {
					listCars.SelectedIndex++;
					textCarrier.Text=((Carrier)similarCars[listCars.SelectedIndex]).CarrierName;
				}
				textCarrier.SelectionStart=textCarrier.Text.Length;
				return;
			}
			if(e.KeyCode==Keys.Up) {
				if(listCars.Items.Count==0) {
					return;
				}
				if(listCars.SelectedIndex==-1) {
					listCars.SelectedIndex=listCars.Items.Count-1;
					textCarrier.Text=((Carrier)similarCars[listCars.SelectedIndex]).CarrierName;
				}
				else if(listCars.SelectedIndex==0) {
					listCars.SelectedIndex=-1;
					textCarrier.Text=carOriginal;
				}
				else {
					listCars.SelectedIndex--;
					textCarrier.Text=((Carrier)similarCars[listCars.SelectedIndex]).CarrierName;
				}
				textCarrier.SelectionStart=textCarrier.Text.Length;
				return;
			}
			if(textCarrier.Text.Length==1) {
				textCarrier.Text=textCarrier.Text.ToUpper();
				textCarrier.SelectionStart=1;
			}
			carOriginal=textCarrier.Text;//the original text is preserved when using up and down arrows
			listCars.Items.Clear();
			similarCars=Carriers.GetSimilarNames(textCarrier.Text);
			for(int i=0;i<similarCars.Count;i++) {
				listCars.Items.Add(((Carrier)similarCars[i]).CarrierName+", "
					+((Carrier)similarCars[i]).Phone+", "
					+((Carrier)similarCars[i]).Address+", "
					+((Carrier)similarCars[i]).Address2+", "
					+((Carrier)similarCars[i]).City+", "
					+((Carrier)similarCars[i]).State+", "
					+((Carrier)similarCars[i]).Zip);
			}
			int h=13*similarCars.Count+5;
			if(h > ClientSize.Height-listCars.Top)
				h=ClientSize.Height-listCars.Top;
			listCars.Size=new Size(listCars.Width,h);
			listCars.Visible=true;
		}

		private void textCarrier_Leave(object sender,System.EventArgs e) {
			if(mouseIsInListCars) {
				return;
			}
			//or if user clicked on a different text box.
			if(listCars.SelectedIndex!=-1) {
				FillCarrier(((Carrier)similarCars[listCars.SelectedIndex]).CarrierNum);
			}
			listCars.Visible=false;
		}

		private void listCars_Click(object sender,System.EventArgs e) {
			FillCarrier(((Carrier)similarCars[listCars.SelectedIndex]).CarrierNum);
			textCarrier.Focus();
			textCarrier.SelectionStart=textCarrier.Text.Length;
			listCars.Visible=false;
		}

		private void listCars_DoubleClick(object sender,System.EventArgs e) {
			//no longer used
		}

		private void listCars_MouseEnter(object sender,System.EventArgs e) {
			mouseIsInListCars=true;
		}

		private void listCars_MouseLeave(object sender,System.EventArgs e) {
			mouseIsInListCars=false;
		}

		private void textPhone_TextChanged(object sender,System.EventArgs e) {
			int cursor=textPhone.SelectionStart;
			int length=textPhone.Text.Length;
			textPhone.Text=TelephoneNumbers.AutoFormat(textPhone.Text);
			if(textPhone.Text.Length>length)
				cursor++;
			textPhone.SelectionStart=cursor;
		}

		private void textAddress_TextChanged(object sender,System.EventArgs e) {
			if(textAddress.Text.Length==1) {
				textAddress.Text=textAddress.Text.ToUpper();
				textAddress.SelectionStart=1;
			}
		}

		private void textAddress2_TextChanged(object sender,System.EventArgs e) {
			if(textAddress2.Text.Length==1) {
				textAddress2.Text=textAddress2.Text.ToUpper();
				textAddress2.SelectionStart=1;
			}
		}

		private void textCity_TextChanged(object sender,System.EventArgs e) {
			if(textCity.Text.Length==1) {
				textCity.Text=textCity.Text.ToUpper();
				textCity.SelectionStart=1;
			}
		}

		private void textState_TextChanged(object sender,System.EventArgs e) {
			if(CultureInfo.CurrentCulture.Name=="en-US" //if USA or Canada, capitalize first 2 letters
				|| CultureInfo.CurrentCulture.Name.Substring(3)=="CA") {
				if(textState.Text.Length==1 || textState.Text.Length==2) {
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=2;
				}
			}
			else {
				if(textState.Text.Length==1) {
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=1;
				}
			}
		}

		private void textElectID_Validating(object sender,System.ComponentModel.CancelEventArgs e) {
			if(textElectID.Text=="") {
				return;
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				if(!Regex.IsMatch(textElectID.Text,@"^[0-9]{6}$")) {
					if(!MsgBox.Show(this,true,"Carrier ID should be six digits long.  Continue anyway?")){
						e.Cancel=true;
						return;
					}
				}
			}
			//else{//anyplace including Canada
			string[] electIDs=ElectIDs.GetDescripts(textElectID.Text);
			if(electIDs.Length==0) {//if none found in the predefined list
				if(!Carriers.ElectIdInUse(textElectID.Text)){
					if(!MsgBox.Show(this,true,"Electronic ID not found. Continue anyway?")) {
						e.Cancel=true;
						return;
					}
				}
			}
			FillPayor();
			//}
		}

		private void butSearch_Click(object sender,System.EventArgs e) {
			FormElectIDs FormE=new FormElectIDs();
			FormE.IsSelectMode=true;
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK) {
				return;
			}
			textElectID.Text=FormE.selectedID.PayorID;
			FillPayor();
			//textElectIDdescript.Text=FormE.selectedID.CarrierName;
		}

		private void butImportTrojan_Click(object sender,System.EventArgs e) {
			if(CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Restorative)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Endodontics)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Periodontics)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Prosthodontics)==null) {
				MsgBox.Show(this,"You must first set up your insurance categories with corresponding electronic benefit categories: RoutinePreventive, Restorative, Endodontics, Periodontics, and Prosthodontics");
				return;
			}
			RegistryKey regKey=Registry.LocalMachine.OpenSubKey("Software\\TROJAN BENEFIT SERVICE");
			if(regKey==null) {//dmg Unix OS will exit here.
				MessageBox.Show("Trojan not installed properly.");
				return;
			}
			//C:\ETW
			string file=ODFileUtils.CombinePaths(regKey.GetValue("INSTALLDIR").ToString(),"Planout.txt");
			if(!File.Exists(file)) {
				MessageBox.Show(file+" not found.  You should export from Trojan first.");
				return;
			}
			usesAnnivers=false;
			bool resetFeeSched=false;//This will be set to true if a FeeSched was imported.
			int feeSchedNum=0;//and if resetFeeSched, then this is the new feesched.
			Benefit ben;
			//clear exising benefits from screen, not db:
			benefitList=new List<Benefit>();
			try {
				using(StreamReader sr=new StreamReader(file)) {
					string line;
					string[] fields;
					int percent;
					string[] splitField;//if a field is a sentence with more than one word, we can split it for analysis
					while((line=sr.ReadLine())!=null) {
						fields=line.Split(new char[] { '\t' });
						if(fields.Length!=3 && fields.Length!=4) {
							continue;
						}
						//remove any trailing or leading spaces:
						fields[0]=fields[0].Trim();
						fields[1]=fields[1].Trim();
						fields[2]=fields[2].Trim();
						if(fields.Length==4){
							fields[3]=fields[3].Trim();
						}
						if(fields[2]=="") {
							continue;
						}
						else {//as long as there is data, add it to the notes
							if(PlanCur.BenefitNotes!="") {
								PlanCur.BenefitNotes+="\r\n";
							}
							PlanCur.BenefitNotes+=fields[1]+": "+fields[2];
							if(fields.Length==4){
								PlanCur.BenefitNotes+=" "+fields[3];
							}
							//if(BenefitNotes!="") {
							//	BenefitNotes+="\r\n";
							//}
							//BenefitNotes+=fields[1]+": "+fields[2];
						}
						switch(fields[0]) {
							//default://for all rows that are not handled below
							case "TROJANID":
								textTrojanID.Text=fields[2];
								break;
							case "ENAME":
								textEmployer.Text=fields[2];
								break;
							case "PLANDESC":
								textGroupName.Text=fields[2];
								break;
							case "ELIGPHONE":
								textPhone.Text=fields[2];
								break;
							case "POLICYNO":
								textGroupNum.Text=fields[2];
								break;
							case "ECLAIMS":
								if(fields[2]=="YES") {//accepts eclaims
									checkNoSendElect.Checked=false;
								}
								else {
									checkNoSendElect.Checked=true;
								}
								break;
							case "PAYERID":
								textElectID.Text=fields[2];
								break;
							case "MAILTO":
								textCarrier.Text=fields[2];
								break;
							case "MAILTOST":
								textAddress.Text=fields[2];
								break;
							case "MAILCITYONLY":
								textCity.Text=fields[2];
								break;
							case "MAILSTATEONLY":
								textState.Text=fields[2];
								break;
							case "MAILZIPONLY":
								textZip.Text=fields[2];
								break;
							case "PLANMAX"://eg $3000 per person per year
								if(!fields[2].StartsWith("$"))
									break;
								fields[2]=fields[2].Remove(0,1);
								fields[2]=fields[2].Split(new char[] { ' ' })[0];
								if(CovCatB.ListShort.Length>0) {
									ben=new Benefit();
									ben.BenefitType=InsBenefitType.Limitations;
									ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
									ben.MonetaryAmt=PIn.PDouble(fields[2]);
									ben.PlanNum=PlanCur.PlanNum;
									ben.TimePeriod=BenefitTimePeriod.CalendarYear;
									benefitList.Add(ben.Copy());
								}
								break;
							case "PLANYR"://eg Calendar year or Anniversary year
								if(fields[2]!="Calendar year") {
									usesAnnivers=true;
									MessageBox.Show("Warning.  Plan uses Anniversary year rather than Calendar year.  Please verify the Plan Start Date.");
								}
								break;
							case "DEDUCT"://eg There is no deductible
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Deductible;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								if(!fields[2].StartsWith("$")) {
									ben.MonetaryAmt=0;
								}
								else {
									fields[2]=fields[2].Remove(0,1);
									fields[2]=fields[2].Split(new char[] { ' ' })[0];
									ben.MonetaryAmt=PIn.PDouble(fields[2]);
								}
								benefitList.Add(ben.Copy());
								break;
							case "PREV"://eg 100%
								splitField=fields[2].Split(new char[] { ' ' });
								if(splitField.Length==0 || !splitField[0].EndsWith("%")) {
									break;
								}
								splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
								percent=PIn.PInt(splitField[0]);
								if(percent<0 || percent>100) {
									break;
								}
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Percentage;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum;
								ben.Percent=percent;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								benefitList.Add(ben.Copy());
								break;
							case "BASIC":
								splitField=fields[2].Split(new char[] { ' ' });
								if(splitField.Length==0 || !splitField[0].EndsWith("%")) {
									break;
								}
								splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
								percent=PIn.PInt(splitField[0]);
								if(percent<0 || percent>100) {
									break;
								}
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Percentage;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Restorative).CovCatNum;
								ben.Percent=percent;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								benefitList.Add(ben.Copy());
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Percentage;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Endodontics).CovCatNum;
								ben.Percent=percent;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								benefitList.Add(ben.Copy());
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Percentage;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Periodontics).CovCatNum;
								ben.Percent=percent;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								benefitList.Add(ben.Copy());
								break;
							case "MAJOR":
								splitField=fields[2].Split(new char[] { ' ' });
								if(splitField.Length==0 || !splitField[0].EndsWith("%")) {
									break;
								}
								splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
								percent=PIn.PInt(splitField[0]);
								if(percent<0 || percent>100) {
									break;
								}
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Percentage;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Prosthodontics).CovCatNum;
								ben.Percent=percent;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								benefitList.Add(ben.Copy());
								//does prosthodontics include crowns?
								break;
							case "FEE":
								if(!ProcedureCodes.IsValidCode(fields[1])) {
									break;//skip
								}
								if(textTrojanID.Text==""){
									break;
								}
								feeSchedNum=Fees.ImportTrojan(fields[1],PIn.PDouble(fields[3]),textTrojanID.Text);
								//the step above probably created a new feeschedule, requiring a reset of the three listboxes.
								resetFeeSched=true;
								break;
						}
					}
				}
				File.Delete(file);
				butBenefitNotes.Enabled=true;
			}
			catch(Exception ex) {
				MessageBox.Show("Error: "+ex.Message);
			}
			if(usesAnnivers) {
				for(int i=0;i<benefitList.Count;i++) {
					if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.CalendarYear) {
						((Benefit)benefitList[i]).TimePeriod=BenefitTimePeriod.ServiceYear;
					}
				}
			}
			FillBenefits();
			if(resetFeeSched){
				FeeSchedsStandard=DefB.GetFeeSchedList("");
				FeeSchedsCopay=DefB.GetFeeSchedList("C");
				FeeSchedsAllowed=DefB.GetFeeSchedList("A");
				//if managed care, then do it a bit differently
				comboFeeSched.Items.Clear();
				comboFeeSched.Items.Add(Lan.g(this,"none"));
				comboFeeSched.SelectedIndex=0;
				for(int i=0;i<FeeSchedsStandard.Length;i++) {
					comboFeeSched.Items.Add(FeeSchedsStandard[i].ItemName);
					if(FeeSchedsStandard[i].DefNum==feeSchedNum)
						comboFeeSched.SelectedIndex=i+1;
				}
				comboCopay.Items.Clear();
				comboCopay.Items.Add(Lan.g(this,"none"));
				comboCopay.SelectedIndex=0;
				for(int i=0;i<FeeSchedsCopay.Length;i++) {
					comboCopay.Items.Add(FeeSchedsCopay[i].ItemName);
					//This will get set for managed care
					//if(FeeSchedsCopay[i].DefNum==PlanCur.CopayFeeSched)
					//	comboCopay.SelectedIndex=i+1;
				}
				comboAllowedFeeSched.Items.Clear();
				comboAllowedFeeSched.Items.Add(Lan.g(this,"none"));
				comboAllowedFeeSched.SelectedIndex=0;
				for(int i=0;i<FeeSchedsAllowed.Length;i++) {
					comboAllowedFeeSched.Items.Add(FeeSchedsAllowed[i].ItemName);
					//I would have set allowed for PPO, but we are probably going to deprecate this when we do coverage tables.
					//if(FeeSchedsAllowed[i].DefNum==PlanCur.AllowedFeeSched)
					//	comboAllowedFeeSched.SelectedIndex=i+1;
				}
			}
		}

		private void butIapFind_Click(object sender,System.EventArgs e) {
			FormIap FormI=new FormIap();
			FormI.ShowDialog();
			if(FormI.DialogResult==DialogResult.Cancel) {
				return;
			}
			Benefit ben;
			//clear exising benefits from screen, not db:
			benefitList=new List<Benefit>();
			string emp=FormI.selectedEmployer;
			string field;
			string[] splitField;//if a field is a sentence with more than one word, we can split it for analysis
			int percent;
			try {
				Iap.ReadRecord(emp);
				for(int i=1;i<122;i++) {
					field=Iap.ReadField(i);
					switch(i) {
						default:
							//do nothing
							break;
						case Iap.Employer:
							if(PlanCur.BenefitNotes!="") {
								PlanCur.BenefitNotes+="\r\n";
							}
							PlanCur.BenefitNotes+="Employer: "+field;
							textEmployer.Text=field;
							break;
						case Iap.Phone:
							PlanCur.BenefitNotes+="\r\n"+"Phone: "+field;
							break;
						case Iap.InsUnder:
							PlanCur.BenefitNotes+="\r\n"+"InsUnder: "+field;
							break;
						case Iap.Carrier:
							PlanCur.BenefitNotes+="\r\n"+"Carrier: "+field;
							textCarrier.Text=field;
							break;
						case Iap.CarrierPh:
							PlanCur.BenefitNotes+="\r\n"+"CarrierPh: "+field;
							textPhone.Text=field;
							break;
						case Iap.Group://seems to be used as groupnum
							PlanCur.BenefitNotes+="\r\n"+"Group: "+field;
							textGroupNum.Text=field;
							break;
						case Iap.MailTo://the carrier name again
							PlanCur.BenefitNotes+="\r\n"+"MailTo: "+field;
							break;
						case Iap.MailTo2://address
							PlanCur.BenefitNotes+="\r\n"+"MailTo2: "+field;
							textAddress.Text=field;
							break;
						case Iap.MailTo3://address2
							PlanCur.BenefitNotes+="\r\n"+"MailTo3: "+field;
							textAddress2.Text=field;
							break;
						case Iap.EClaims:
							PlanCur.BenefitNotes+="\r\n"+"EClaims: "+field;//this contains the PayorID at the end, but also a bunch of other drivel.
							int payorIDloc=field.LastIndexOf("Payor ID#:");
							if(payorIDloc!=-1 && field.Length>payorIDloc+10) {
								textElectID.Text=field.Substring(payorIDloc+10);
							}
							break;
						case Iap.FAXClaims:
							PlanCur.BenefitNotes+="\r\n"+"FAXClaims: "+field;
							break;
						case Iap.DMOOption:
							PlanCur.BenefitNotes+="\r\n"+"DMOOption: "+field;
							break;
						case Iap.Medical:
							PlanCur.BenefitNotes+="\r\n"+"Medical: "+field;
							break;
						case Iap.GroupNum://not used.  They seem to use the group field instead
							PlanCur.BenefitNotes+="\r\n"+"GroupNum: "+field;
							break;
						case Iap.Phone2://?
							PlanCur.BenefitNotes+="\r\n"+"Phone2: "+field;
							break;
						case Iap.Deductible:
							PlanCur.BenefitNotes+="\r\n"+"Deductible: "+field;
							if(field.StartsWith("$")) {
								splitField=field.Split(new char[] { ' ' });
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Deductible;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								ben.MonetaryAmt=PIn.PDouble(splitField[0].Remove(0,1));//removes the $
								benefitList.Add(ben.Copy());
							}
							break;
						case Iap.FamilyDed:
							PlanCur.BenefitNotes+="\r\n"+"FamilyDed: "+field;
							break;
						case Iap.Maximum:
							PlanCur.BenefitNotes+="\r\n"+"Maximum: "+field;
							if(field.StartsWith("$")) {
								splitField=field.Split(new char[] { ' ' });
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Limitations;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								ben.MonetaryAmt=PIn.PDouble(splitField[0].Remove(0,1));//removes the $
								benefitList.Add(ben.Copy());
							}
							break;
						case Iap.BenefitYear://text is too complex to parse
							PlanCur.BenefitNotes+="\r\n"+"BenefitYear: "+field;
							break;
						case Iap.DependentAge://too complex to parse
							PlanCur.BenefitNotes+="\r\n"+"DependentAge: "+field;
							break;
						case Iap.Preventive:
							PlanCur.BenefitNotes+="\r\n"+"Preventive: "+field;
							splitField=field.Split(new char[] { ' ' });
							if(splitField.Length==0 || !splitField[0].EndsWith("%")) {
								break;
							}
							splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
							percent=PIn.PInt(splitField[0]);
							if(percent<0 || percent>100) {
								break;
							}
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum;
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							break;
						case Iap.Basic:
							PlanCur.BenefitNotes+="\r\n"+"Basic: "+field;
							splitField=field.Split(new char[] { ' ' });
							if(splitField.Length==0 || !splitField[0].EndsWith("%")) {
								break;
							}
							splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
							percent=PIn.PInt(splitField[0]);
							if(percent<0 || percent>100) {
								break;
							}
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Restorative).CovCatNum;
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Endodontics).CovCatNum;
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Periodontics).CovCatNum;
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.OralSurgery).CovCatNum;
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							break;
						case Iap.Major:
							PlanCur.BenefitNotes+="\r\n"+"Major: "+field;
							splitField=field.Split(new char[] { ' ' });
							if(splitField.Length==0 || !splitField[0].EndsWith("%")) {
								break;
							}
							splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
							percent=PIn.PInt(splitField[0]);
							if(percent<0 || percent>100) {
								break;
							}
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Prosthodontics).CovCatNum;//includes crowns?
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							break;
						case Iap.InitialPlacement:
							PlanCur.BenefitNotes+="\r\n"+"InitialPlacement: "+field;
							break;
						case Iap.ExtractionClause:
							PlanCur.BenefitNotes+="\r\n"+"ExtractionClause: "+field;
							break;
						case Iap.Replacement:
							PlanCur.BenefitNotes+="\r\n"+"Replacement: "+field;
							break;
						case Iap.Other:
							PlanCur.BenefitNotes+="\r\n"+"Other: "+field;
							break;
						case Iap.Orthodontics:
							PlanCur.BenefitNotes+="\r\n"+"Orthodontics: "+field;
							splitField=field.Split(new char[] { ' ' });
							if(splitField.Length==0 || !splitField[0].EndsWith("%")) {
								break;
							}
							splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
							percent=PIn.PInt(splitField[0]);
							if(percent<0 || percent>100) {
								break;
							}
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Orthodontics).CovCatNum;
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							break;
						case Iap.Deductible2:
							PlanCur.BenefitNotes+="\r\n"+"Deductible2: "+field;
							break;
						case Iap.Maximum2://ortho Max
							PlanCur.BenefitNotes+="\r\n"+"Maximum2: "+field;
							if(field.StartsWith("$")) {
								splitField=field.Split(new char[] { ' ' });
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Limitations;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Orthodontics).CovCatNum;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								ben.MonetaryAmt=PIn.PDouble(splitField[0].Remove(0,1));//removes the $
								benefitList.Add(ben.Copy());
							}
							break;
						case Iap.PymtSchedule:
							PlanCur.BenefitNotes+="\r\n"+"PymtSchedule: "+field;
							break;
						case Iap.AgeLimit:
							PlanCur.BenefitNotes+="\r\n"+"AgeLimit: "+field;
							break;
						case Iap.SignatureonFile:
							PlanCur.BenefitNotes+="\r\n"+"SignatureonFile: "+field;
							break;
						case Iap.StandardADAForm:
							PlanCur.BenefitNotes+="\r\n"+"StandardADAForm: "+field;
							break;
						case Iap.CoordinationRule:
							PlanCur.BenefitNotes+="\r\n"+"CoordinationRule: "+field;
							break;
						case Iap.CoordinationCOB:
							PlanCur.BenefitNotes+="\r\n"+"CoordinationCOB: "+field;
							break;
						case Iap.NightguardsforBruxism:
							PlanCur.BenefitNotes+="\r\n"+"NightguardsforBruxism: "+field;
							break;
						case Iap.OcclusalAdjustments:
							PlanCur.BenefitNotes+="\r\n"+"OcclusalAdjustments: "+field;
							break;
						case Iap.XXXXXX:
							PlanCur.BenefitNotes+="\r\n"+"XXXXXX: "+field;
							break;
						case Iap.TMJNonSurgical:
							PlanCur.BenefitNotes+="\r\n"+"TMJNonSurgical: "+field;
							break;
						case Iap.Implants:
							PlanCur.BenefitNotes+="\r\n"+"Implants: "+field;
							break;
						case Iap.InfectionControl:
							PlanCur.BenefitNotes+="\r\n"+"InfectionControl: "+field;
							break;
						case Iap.Cleanings:
							PlanCur.BenefitNotes+="\r\n"+"Cleanings: "+field;
							break;
						case Iap.OralEvaluation:
							PlanCur.BenefitNotes+="\r\n"+"OralEvaluation: "+field;
							break;
						case Iap.Fluoride1200s:
							PlanCur.BenefitNotes+="\r\n"+"Fluoride1200s: "+field;
							break;
						case Iap.Code0220:
							PlanCur.BenefitNotes+="\r\n"+"Code0220: "+field;
							break;
						case Iap.Code0272_0274:
							PlanCur.BenefitNotes+="\r\n"+"Code0272_0274: "+field;
							break;
						case Iap.Code0210:
							PlanCur.BenefitNotes+="\r\n"+"Code0210: "+field;
							break;
						case Iap.Code0330:
							PlanCur.BenefitNotes+="\r\n"+"Code0330: "+field;
							break;
						case Iap.SpaceMaintainers:
							PlanCur.BenefitNotes+="\r\n"+"SpaceMaintainers: "+field;
							break;
						case Iap.EmergencyExams:
							PlanCur.BenefitNotes+="\r\n"+"EmergencyExams: "+field;
							break;
						case Iap.EmergencyTreatment:
							PlanCur.BenefitNotes+="\r\n"+"EmergencyTreatment: "+field;
							break;
						case Iap.Sealants1351:
							PlanCur.BenefitNotes+="\r\n"+"Sealants1351: "+field;
							break;
						case Iap.Fillings2100:
							PlanCur.BenefitNotes+="\r\n"+"Fillings2100: "+field;
							break;
						case Iap.Extractions:
							PlanCur.BenefitNotes+="\r\n"+"Extractions: "+field;
							break;
						case Iap.RootCanals:
							PlanCur.BenefitNotes+="\r\n"+"RootCanals: "+field;
							break;
						case Iap.MolarRootCanal:
							PlanCur.BenefitNotes+="\r\n"+"MolarRootCanal: "+field;
							break;
						case Iap.OralSurgery:
							PlanCur.BenefitNotes+="\r\n"+"OralSurgery: "+field;
							break;
						case Iap.ImpactionSoftTissue:
							PlanCur.BenefitNotes+="\r\n"+"ImpactionSoftTissue: "+field;
							break;
						case Iap.ImpactionPartialBony:
							PlanCur.BenefitNotes+="\r\n"+"ImpactionPartialBony: "+field;
							break;
						case Iap.ImpactionCompleteBony:
							PlanCur.BenefitNotes+="\r\n"+"ImpactionCompleteBony: "+field;
							break;
						case Iap.SurgicalProceduresGeneral:
							PlanCur.BenefitNotes+="\r\n"+"SurgicalProceduresGeneral: "+field;
							break;
						case Iap.PerioSurgicalPerioOsseous:
							PlanCur.BenefitNotes+="\r\n"+"PerioSurgicalPerioOsseous: "+field;
							break;
						case Iap.SurgicalPerioOther:
							PlanCur.BenefitNotes+="\r\n"+"SurgicalPerioOther: "+field;
							break;
						case Iap.RootPlaning:
							PlanCur.BenefitNotes+="\r\n"+"RootPlaning: "+field;
							break;
						case Iap.Scaling4345:
							PlanCur.BenefitNotes+="\r\n"+"Scaling4345: "+field;
							break;
						case Iap.PerioPx:
							PlanCur.BenefitNotes+="\r\n"+"PerioPx: "+field;
							break;
						case Iap.PerioComment:
							PlanCur.BenefitNotes+="\r\n"+"PerioComment: "+field;
							break;
						case Iap.IVSedation:
							PlanCur.BenefitNotes+="\r\n"+"IVSedation: "+field;
							break;
						case Iap.General9220:
							PlanCur.BenefitNotes+="\r\n"+"General9220: "+field;
							break;
						case Iap.Relines5700s:
							PlanCur.BenefitNotes+="\r\n"+"Relines5700s: "+field;
							break;
						case Iap.StainlessSteelCrowns:
							PlanCur.BenefitNotes+="\r\n"+"StainlessSteelCrowns: "+field;
							break;
						case Iap.Crowns2700s:
							PlanCur.BenefitNotes+="\r\n"+"Crowns2700s: "+field;
							break;
						case Iap.Bridges6200:
							PlanCur.BenefitNotes+="\r\n"+"Bridges6200: "+field;
							break;
						case Iap.Partials5200s:
							PlanCur.BenefitNotes+="\r\n"+"Partials5200s: "+field;
							break;
						case Iap.Dentures5100s:
							PlanCur.BenefitNotes+="\r\n"+"Dentures5100s: "+field;
							break;
						case Iap.EmpNumberXXX:
							PlanCur.BenefitNotes+="\r\n"+"EmpNumberXXX: "+field;
							break;
						case Iap.DateXXX:
							PlanCur.BenefitNotes+="\r\n"+"DateXXX: "+field;
							break;
						case Iap.Line4://city state
							PlanCur.BenefitNotes+="\r\n"+"Line4: "+field;
							field=field.Replace("  "," ");//get rid of double space before zip
							splitField=field.Split(new char[] { ' ' });
							if(splitField.Length<3) {
								break;
							}
							textCity.Text=splitField[0].Replace(",","");//gets rid of the comma on the end of city
							textState.Text=splitField[1];
							textZip.Text=splitField[2];
							break;
						case Iap.Note:
							PlanCur.BenefitNotes+="\r\n"+"Note: "+field;
							break;
						case Iap.Plan://?
							PlanCur.BenefitNotes+="\r\n"+"Plan: "+field;
							break;
						case Iap.BuildUps:
							PlanCur.BenefitNotes+="\r\n"+"BuildUps: "+field;
							break;
						case Iap.PosteriorComposites:
							PlanCur.BenefitNotes+="\r\n"+"PosteriorComposites: "+field;
							break;
					}
				}
				Iap.CloseDatabase();
				butBenefitNotes.Enabled=true;
			}
			catch(ApplicationException ex) {
				Iap.CloseDatabase();
				MessageBox.Show(ex.Message);
			}
			catch(Exception ex) {
				Iap.CloseDatabase();
				MessageBox.Show("Error: "+ex.Message);
			}
			FillBenefits();
		}

		///<summary>Only visible if Computer set to Canada.</summary>
		private void butCanadaEligibility_Click(object sender,EventArgs e) {
			if(textDentaide.errorProvider1.GetError(textDentaide)!="") {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			string result="";
			try{
				result=Eclaims.CanadianOutput.SendElegibility(textElectID.Text,PatPlanCur.PatNum,textGroupNum.Text,textDivisionNo.Text,
					textSubscriberID.Text,textPatID.Text,(Relat)comboRelationship.SelectedIndex,PlanCur.Subscriber,textDentaide.Text);
				//printout will happen in the line above.
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			PlanCur.BenefitNotes+=result;
			butBenefitNotes.Enabled=true;
		}

		///<summary>This button is always active.</summary>
		private void butBenefitNotes_Click(object sender,System.EventArgs e) {
			string otherBenNote="";
			if(PlanCur.BenefitNotes=="") {
				//try to find some other similar notes. Never includes the current plan.
				List<int> samePlans=InsPlans.GetPlanNumsOfSamePlans(textEmployer.Text,textGroupName.Text,textGroupNum.Text,
					textDivisionNo.Text,textCarrier.Text,checkIsMedical.Checked,PlanCur.PlanNum,false);
				otherBenNote=InsPlans.GetBenefitNotes(samePlans);
				if(otherBenNote=="") {
					MsgBox.Show(this,"No benefit note found.  Benefit notes are created when electronically requesting benefit information.  Store your own benefit notes in the subscriber note instead.");
					return;
				}
				MsgBox.Show(this,"This plan does not have a benefit note, but a note was found for an identical plan.  You will be able to view this note, but not change it.");
			}
			FormInsBenefitNotes FormI=new FormInsBenefitNotes();
			if(PlanCur.BenefitNotes!="") {
				FormI.BenefitNotes=PlanCur.BenefitNotes;
			}
			else {
				FormI.BenefitNotes=otherBenNote;
			}
			FormI.ShowDialog();
			if(FormI.DialogResult==DialogResult.Cancel) {
				return;
			}
			if(PlanCur.BenefitNotes!="") {
				PlanCur.BenefitNotes=FormI.BenefitNotes;
			}
		}

		private void butDelete_Click(object sender,System.EventArgs e) {
			if(IsNewPlan) {
				DialogResult=DialogResult.Cancel;//will get deleted in closing event
				return;
			}
			if(!MsgBox.Show(this,true,"Delete Plan?")) {
				return;
			}
			try {
				InsPlans.Delete(PlanCur);//checks dependencies first. Also deletes benefits,claimprocs,patplan and recomputes all estimates.
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butDrop_Click(object sender,System.EventArgs e) {
			//should we save the plan info first?  Probably not.
			PatPlans.Delete(PatPlanCur.PatPlanNum);//Estimates recomputed within Delete()
			//PlanCur.ComputeEstimatesForCur();
			DialogResult=DialogResult.OK;
		}

		private void butLabel_Click(object sender,System.EventArgs e) {
			LabelSingle label=new LabelSingle();
			PrintDocument pd=new PrintDocument();//only used to pass printerName
			if(!Printers.SetPrinter(pd,PrintSituation.LabelSingle)) {
				return;
			}
			Carrier carrier=new Carrier();
			carrier.CarrierName=textCarrier.Text;
			carrier.Phone=textPhone.Text;
			carrier.Address=textAddress.Text;
			carrier.Address2=textAddress2.Text;
			carrier.City=textCity.Text;
			carrier.State=textState.Text;
			carrier.Zip=textZip.Text;
			label.PrintIns(carrier,pd.PrinterSettings.PrinterName);
		}

		///<summary>This only fills the grid on the screen.  It does not get any data from the database.</summary>
		private void FillBenefits() {
			benefitList.Sort();
			gridBenefits.BeginUpdate();
			gridBenefits.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Pat",28);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Level",60);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Type",70);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Category",70);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("%",30);//,HorizontalAlignment.Right);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Amt",40);//,HorizontalAlignment.Right);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Time Period",80);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Quantity",115);
			gridBenefits.Columns.Add(col);
			gridBenefits.Rows.Clear();
			ODGridRow row;
			//bool allCalendarYear=true;
			//bool allServiceYear=true;
			for(int i=0;i<benefitList.Count;i++) {
				/*if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.CalendarYear){
					allServiceYear=false;
				}
				if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.ServiceYear) {
					allCalendarYear=false;
				}*/
				row=new ODGridRow();
				if(benefitList[i].PatPlanNum==0) {//attached to plan
					row.Cells.Add("");
				}
				else {
					row.Cells.Add("X");
				}
				if(benefitList[i].CoverageLevel==BenefitCoverageLevel.Individual) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(Lan.g("enumBenefitCoverageLevel",benefitList[i].CoverageLevel.ToString()));
				}
				if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Percentage) {
					row.Cells.Add("%");
				}
				else if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Limitations
					&& (((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.ServiceYear
					|| ((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.CalendarYear)
					&& ((Benefit)benefitList[i]).QuantityQualifier==BenefitQuantity.None) {//annual max
					row.Cells.Add(Lan.g(this,"Annual Max"));
				}
				else {
					row.Cells.Add(Lan.g("enumInsBenefitType",((Benefit)benefitList[i]).BenefitType.ToString()));
				}
				if(((Benefit)benefitList[i]).CodeNum==0) {
					row.Cells.Add(CovCats.GetDesc(((Benefit)benefitList[i]).CovCatNum));
				}
				else {
					row.Cells.Add(ProcedureCodes.GetProcCode(((Benefit)benefitList[i]).CodeNum).AbbrDesc);
				}
				if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Percentage) {
					row.Cells.Add(((Benefit)benefitList[i]).Percent.ToString());
				}
				else {
					row.Cells.Add("");
				}
				if(((Benefit)benefitList[i]).MonetaryAmt==0) {
					if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Deductible) {
						row.Cells.Add(((Benefit)benefitList[i]).MonetaryAmt.ToString("n0"));
					}
					else {
						row.Cells.Add("");
					}
				}
				else {
					row.Cells.Add(((Benefit)benefitList[i]).MonetaryAmt.ToString("n0"));
				}
				if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.None) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(Lan.g("enumBenefitTimePeriod",((Benefit)benefitList[i]).TimePeriod.ToString()));
				}
				if(((Benefit)benefitList[i]).Quantity>0) {
					row.Cells.Add(((Benefit)benefitList[i]).Quantity.ToString()+" "
						+Lan.g("enumBenefitQuantity",((Benefit)benefitList[i]).QuantityQualifier.ToString()));
				}
				else {
					row.Cells.Add("");
				}
				gridBenefits.Rows.Add(row);
			}
			gridBenefits.EndUpdate();
			/*if(allCalendarYear){
				checkCalendarYear.CheckState=CheckState.Checked;
			}
			else if(allServiceYear){
				checkCalendarYear.CheckState=CheckState.Unchecked;
			}
			else{
				checkCalendarYear.CheckState=CheckState.Indeterminate;
			}*/
		}

		private void butEditBenefits_Click(object sender,EventArgs e) {
			int patPlanNum=0;
			if(PatPlanCur!=null) {
				patPlanNum=PatPlanCur.PatPlanNum;
			}
			FormInsBenefits FormI=new FormInsBenefits(PlanCur.PlanNum,patPlanNum);
			FormI.OriginalBenList=benefitList;
			FormI.Note=textSubscNote.Text;
			FormI.ShowDialog();
			if(FormI.DialogResult!=DialogResult.OK) {
				return;
			}
			FillBenefits();
			textSubscNote.Text=FormI.Note;
		}

		///<summary>Gets an employerNum based on the name entered. Called from FillCur</summary>
		private void GetEmployerNum() {
			if(PlanCur.EmployerNum==0) {//no employer was previously entered.
				if(textEmployer.Text=="") {
					//no change
				}
				else {
					PlanCur.EmployerNum=Employers.GetEmployerNum(textEmployer.Text);
				}
			}
			else {//an employer was previously entered
				if(textEmployer.Text=="") {
					PlanCur.EmployerNum=0;
				}
				//if text has changed
				else if(Employers.GetName(PlanCur.EmployerNum)!=textEmployer.Text) {
					PlanCur.EmployerNum=Employers.GetEmployerNum(textEmployer.Text);
				}
			}
		}

		private void butEligibility_Click(object sender,EventArgs e) {
			Clearinghouse clearhouse=Clearinghouses.GetDefault();
			if(clearhouse==null){
				MsgBox.Show(this,"No clearinghouse is set as default.");
				return;
			}
			if(clearhouse.CommBridge==EclaimsCommBridge.ClaimConnect){
				EligibilityCheckDentalXchange();
				return;
			}
			if(clearhouse.CommBridge==EclaimsCommBridge.Tesia) {
				Eclaims.Tesia.Eligibility270();
				return;
			}
			MsgBox.Show(this,"We do not have eligibility checks functional with your default clearinghouse.");
		}
		
		#region EligibilityCheckDentalXchange
		//Added SPK/AAD 10/06 for eligibility check.-------------------------------------------------------------------------
		//PraciceWeb seems to have a partnership agreement with this clearinghouse, but we do not yet.
		private void EligibilityCheckDentalXchange() {
			Cursor = Cursors.WaitCursor;
			OpenDental.com.dentalxchange.webservices.WebServiceService DCIService 
				= new OpenDental.com.dentalxchange.webservices.WebServiceService();
			OpenDental.com.dentalxchange.webservices.Credentials DCICredential 
				= new OpenDental.com.dentalxchange.webservices.Credentials();
			OpenDental.com.dentalxchange.webservices.Request DCIRequest = new OpenDental.com.dentalxchange.webservices.Request();
			OpenDental.com.dentalxchange.webservices.Response DCIResponse = new OpenDental.com.dentalxchange.webservices.Response();
			DataTable table;
			string loginID;
			string passWord;
			string command;
			// Get Login / Password
			command = @"select loginid,password from clearinghouse where isDefault=1";
			table =General.GetTable(command);
			if(table.Rows.Count != 0) {
				loginID = PIn.PString(table.Rows[0][0].ToString());
				passWord = PIn.PString(table.Rows[0][1].ToString());
			}
			else {
				loginID = "";
				passWord = "";
			}
			if(loginID == "") {
				MessageBox.Show("ClaimConnect login ID and password are required to check eligibility.");
				Cursor = Cursors.Default;
				return;
			}
			// Set Credentials
			DCICredential.serviceID = "DCI Web Service ID: 001513";
			DCICredential.username = loginID;   // ABCuser
			DCICredential.password = passWord;  // testing1
			DCICredential.client = "Practice-Web";
			DCICredential.version = "1";
			// Set Request Document
			//textAddress.Text = PrepareEligibilityRequest();
			DCIRequest.content = PrepareEligibilityRequestDentalXchange(loginID,passWord);
			try {
				DCIResponse = DCIService.lookupEligibility(DCICredential,DCIRequest);
				//DisplayEligibilityStatus();
				ProcessEligibilityResponseDentalXchange(DCIResponse.content.ToString());
			}
			catch(Exception ex) {
				MessageBox.Show("Error : " + ex.Message);
			}
			Cursor = Cursors.Default;
		}

		private string PrepareEligibilityRequestDentalXchange(string loginID,string passWord) {
			string command;
			DataTable table;
			string infoReceiverLastName;
			string infoReceiverFirstName;
			string practiceAddress1;
			string practiceAddress2;
			string practicePhone;
			string practiceCity;
			string practiceState;
			string practiceZip;
			string renderingProviderLastName;
			string renderingProviderFirstName;
			string GenderCode;
			string TaxoCode;
			string RelationShip;
			XmlDocument doc = new XmlDocument();
			XmlNode EligNode = doc.CreateNode(XmlNodeType.Element,"EligRequest","");
			doc.AppendChild(EligNode);
			// Prepare Namespace Attribute
			XmlAttribute nameSpaceAttribute = doc.CreateAttribute("xmlns","xsi","http://www.w3.org/2000/xmlns/");
			nameSpaceAttribute.Value = "http://www.w3.org/2001/XMLSchema-instance";
			doc.DocumentElement.SetAttributeNode(nameSpaceAttribute);
			// Prepare noNamespace Schema Location Attribute
			XmlAttribute noNameSpaceSchemaLocation = doc.CreateAttribute("xsi","noNamespaceSchemaLocation","http://www.w3.org/2001/XMLSchema-instance");
			//dmg Note sure what this is for. This path will not exist on Unix and will fail. In fact, this path
			//will either not exist or be read-only on most Windows boxes, so this path specification is probably
			//a bug, but has not caused any user complaints thus far.
			noNameSpaceSchemaLocation.Value = @"D:\eligreq.xsd";
			doc.DocumentElement.SetAttributeNode(noNameSpaceSchemaLocation);
			//  Prepare AuthInfo Node
			XmlNode AuthInfoNode = doc.CreateNode(XmlNodeType.Element,"AuthInfo","");
			//  Create UserName / Password ChildNode for AuthInfoNode
			XmlNode UserName = doc.CreateNode(XmlNodeType.Element,"UserName","");
			XmlNode Password = doc.CreateNode(XmlNodeType.Element,"Password","");
			//  Set Value of UserID / Password
			UserName.InnerText = loginID;
			Password.InnerText = passWord;
			//  Append UserName / Password to AuthInfoNode
			AuthInfoNode.AppendChild(UserName);
			AuthInfoNode.AppendChild(Password);
			//  Append AuthInfoNode To EligNode
			EligNode.AppendChild(AuthInfoNode);
			//  Prepare Information Receiver Node
			XmlNode InfoReceiver = doc.CreateNode(XmlNodeType.Element,"InformationReceiver","");
			XmlNode InfoAddress = doc.CreateNode(XmlNodeType.Element,"Address","");
			XmlNode InfoAddressName = doc.CreateNode(XmlNodeType.Element,"Name","");
			XmlNode InfoAddressFirstName = doc.CreateNode(XmlNodeType.Element,"FirstName","");
			XmlNode InfoAddressLastName = doc.CreateNode(XmlNodeType.Element,"LastName","");
			// Get Provider Information
			command = @"SELECT FName,LName,Specialty FROM provider WHERE provnum=" + Convert.ToInt32(((Pref)PrefB.HList["PracticeDefaultProv"]).ValueString);
			table =General.GetTable(command);
			if(table.Rows.Count != 0) {
				infoReceiverFirstName = PIn.PString(table.Rows[0][0].ToString());
				infoReceiverLastName = PIn.PString(table.Rows[0][1].ToString());
				// Case statment for TaxoCode
				switch(PIn.PInt(table.Rows[0][2].ToString())) {
					case 1:
						TaxoCode = "124Q00000X";
						break;
					case 2:
						TaxoCode = "1223D0001X";
						break;
					case 3:
						TaxoCode = "1223E0200X";
						break;
					case 4:
						TaxoCode = "1223P0106X";
						break;
					case 5:
						TaxoCode = "1223D0008X";
						break;
					case 6:
						TaxoCode = "1223S0112X";
						break;
					case 7:
						TaxoCode = "1223X0400X";
						break;
					case 8:
						TaxoCode = "1223P0221X";
						break;
					case 9:
						TaxoCode = "1223P0300X";
						break;
					case 10:
						TaxoCode = "1223P0700X";
						break;
					default:
						TaxoCode = "1223G0001X";
						break;
				}
			}
			else {
				infoReceiverFirstName = "Unknown";
				infoReceiverLastName = "Unknown";
				TaxoCode = "Unknown";
			};
			InfoAddressFirstName.InnerText = infoReceiverLastName;
			InfoAddressLastName.InnerText = infoReceiverFirstName;
			InfoAddressName.AppendChild(InfoAddressFirstName);
			InfoAddressName.AppendChild(InfoAddressLastName);
			XmlNode InfoAddressLine1 = doc.CreateNode(XmlNodeType.Element,"AddressLine1","");
			XmlNode InfoAddressLine2 = doc.CreateNode(XmlNodeType.Element,"AddressLine2","");
			XmlNode InfoPhone = doc.CreateNode(XmlNodeType.Element,"Phone","");
			XmlNode InfoCity = doc.CreateNode(XmlNodeType.Element,"City","");
			XmlNode InfoState = doc.CreateNode(XmlNodeType.Element,"State","");
			XmlNode InfoZip = doc.CreateNode(XmlNodeType.Element,"Zip","");
			//  Populate Practioner demographic from hash table
			practiceAddress1 = ((Pref)PrefB.HList["PracticeAddress"]).ValueString;
			practiceAddress2 = ((Pref)PrefB.HList["PracticeAddress2"]).ValueString;
			// Format Phone
			if(((Pref)PrefB.HList["PracticePhone"]).ValueString.Length == 10) {
				practicePhone = ((Pref)PrefB.HList["PracticePhone"]).ValueString.Substring(0,3)
                                    + "-" + ((Pref)PrefB.HList["PracticePhone"]).ValueString.Substring(3,3)
                                    + "-" + ((Pref)PrefB.HList["PracticePhone"]).ValueString.Substring(6);
			}
			else {
				practicePhone = ((Pref)PrefB.HList["PracticePhone"]).ValueString;
			}
			practiceCity = ((Pref)PrefB.HList["PracticeCity"]).ValueString;
			practiceState = ((Pref)PrefB.HList["PracticeST"]).ValueString;
			practiceZip = ((Pref)PrefB.HList["PracticeZip"]).ValueString;
			InfoAddressLine1.InnerText = practiceAddress1;
			InfoAddressLine2.InnerText = practiceAddress2;
			InfoPhone.InnerText = practicePhone;
			InfoCity.InnerText = practiceCity;
			InfoState.InnerText = practiceState;
			InfoZip.InnerText = practiceZip;
			InfoAddress.AppendChild(InfoAddressName);
			InfoAddress.AppendChild(InfoAddressLine1);
			InfoAddress.AppendChild(InfoAddressLine2);
			InfoAddress.AppendChild(InfoPhone);
			InfoAddress.AppendChild(InfoCity);
			InfoAddress.AppendChild(InfoState);
			InfoAddress.AppendChild(InfoZip);
			InfoReceiver.AppendChild(InfoAddress);
			XmlNode InfoCredential = doc.CreateNode(XmlNodeType.Element,"Credential","");
			XmlNode InfoCredentialType = doc.CreateNode(XmlNodeType.Element,"Type","");
			XmlNode InfoCredentialValue = doc.CreateNode(XmlNodeType.Element,"Value","");
			InfoCredentialType.InnerText = "TJ";
			InfoCredentialValue.InnerText = "123456789";
			InfoCredential.AppendChild(InfoCredentialType);
			InfoCredential.AppendChild(InfoCredentialValue);
			InfoReceiver.AppendChild(InfoCredential);
			XmlNode InfoTaxonomyCode = doc.CreateNode(XmlNodeType.Element,"TaxonomyCode","");
			InfoTaxonomyCode.InnerText = TaxoCode;
			InfoReceiver.AppendChild(InfoTaxonomyCode);
			//  Append InfoReceiver To EligNode
			EligNode.AppendChild(InfoReceiver);
			//  Payer Info
			XmlNode InfoPayer = doc.CreateNode(XmlNodeType.Element,"Payer","");
			XmlNode InfoPayerNEIC = doc.CreateNode(XmlNodeType.Element,"PayerNEIC","");
			InfoPayerNEIC.InnerText = textElectID.Text;
			InfoPayer.AppendChild(InfoPayerNEIC);
			EligNode.AppendChild(InfoPayer);
			//  Patient
			XmlNode Patient = doc.CreateNode(XmlNodeType.Element,"Patient","");
			XmlNode PatientName = doc.CreateNode(XmlNodeType.Element,"Name","");
			XmlNode PatientFirstName = doc.CreateNode(XmlNodeType.Element,"FirstName","");
			XmlNode PatientLastName = doc.CreateNode(XmlNodeType.Element,"LastName","");
			XmlNode PatientDOB = doc.CreateNode(XmlNodeType.Element,"DOB","");
			XmlNode PatientSubscriber = doc.CreateNode(XmlNodeType.Element,"SubscriberID","");
			XmlNode PatientRelationship = doc.CreateNode(XmlNodeType.Element,"RelationshipCode","");
			XmlNode PatientGender = doc.CreateNode(XmlNodeType.Element,"Gender","");
			// Read Patient FName,LName,DOB, and Gender from Patient Table
			command = @"SELECT FName,LName,date_format(birthdate,'%m/%d/%Y') as BirthDate,Gender
				FROM patient WHERE patient.PatNum=" + PatPlanCur.PatNum;
			table = General.GetTable(command);
			if(table.Rows.Count != 0) {
				PatientFirstName.InnerText = PIn.PString(table.Rows[0][0].ToString());
				PatientLastName.InnerText = PIn.PString(table.Rows[0][1].ToString());
				PatientDOB.InnerText = PIn.PString(table.Rows[0][2].ToString());
				switch(comboRelationship.Text) {
					case "Self":
						RelationShip = "18";
						break;
					case "Spouse":
						RelationShip = "01";
						break;
					case "Child":
						RelationShip = "19";
						break;
					default:
						RelationShip = "34";
						break;
				}
				switch(PIn.PString(table.Rows[0][3].ToString())) {
					case "1":
						GenderCode = "F";
						break;
					default:
						GenderCode = "M";
						break;
				}
			}
			else {
				PatientFirstName.InnerText = "Unknown";
				PatientLastName.InnerText = "Unknown";
				PatientDOB.InnerText = "99/99/9999";
				RelationShip = "??";
				GenderCode = "?";
			}
			PatientName.AppendChild(PatientFirstName);
			PatientName.AppendChild(PatientLastName);
			PatientSubscriber.InnerText = textSubscriberID.Text;
			PatientRelationship.InnerText = RelationShip;
			PatientGender.InnerText = GenderCode;
			Patient.AppendChild(PatientName);
			Patient.AppendChild(PatientDOB);
			Patient.AppendChild(PatientSubscriber);
			Patient.AppendChild(PatientRelationship);
			Patient.AppendChild(PatientGender);
			EligNode.AppendChild(Patient);
			//  Subscriber
			XmlNode Subscriber = doc.CreateNode(XmlNodeType.Element,"Subscriber","");
			XmlNode SubscriberName = doc.CreateNode(XmlNodeType.Element,"Name","");
			XmlNode SubscriberFirstName = doc.CreateNode(XmlNodeType.Element,"FirstName","");
			XmlNode SubscriberLastName = doc.CreateNode(XmlNodeType.Element,"LastName","");
			XmlNode SubscriberDOB = doc.CreateNode(XmlNodeType.Element,"DOB","");
			XmlNode SubscriberSubscriber = doc.CreateNode(XmlNodeType.Element,"SubscriberID","");
			XmlNode SubscriberRelationship = doc.CreateNode(XmlNodeType.Element,"RelationshipCode","");
			XmlNode SubscriberGender = doc.CreateNode(XmlNodeType.Element,"Gender","");
			// Read Subscriber FName,LName,DOB, and Gender from Patient Table
			command = @"SELECT FName,LName,date_format(birthdate,'%m/%d/%Y') as BirthDate,Gender
				        FROM PATIENT WHERE PatNum In (SELECT Guarantor FROM 
                            PATIENT WHERE patnum = " + PatPlanCur.PatNum + ")";
			table = General.GetTable(command);
			if(table.Rows.Count != 0) {
				SubscriberFirstName.InnerText = PIn.PString(table.Rows[0][0].ToString());
				SubscriberLastName.InnerText = PIn.PString(table.Rows[0][1].ToString());
				SubscriberDOB.InnerText = PIn.PString(table.Rows[0][2].ToString());
				switch(PIn.PString(table.Rows[0][3].ToString())) {
					case "1":
						GenderCode = "F";
						break;
					default:
						GenderCode = "M";
						break;
				}
			}
			else {
				SubscriberFirstName.InnerText = "Unknown";
				SubscriberLastName.InnerText = "Unknown";
				SubscriberDOB.InnerText = "99/99/9999";
				GenderCode = "?";
			}
			SubscriberName.AppendChild(SubscriberFirstName);
			SubscriberName.AppendChild(SubscriberLastName);
			SubscriberSubscriber.InnerText = textSubscriberID.Text;
			SubscriberRelationship.InnerText = RelationShip;
			SubscriberGender.InnerText = GenderCode;
			Subscriber.AppendChild(SubscriberName);
			Subscriber.AppendChild(SubscriberDOB);
			Subscriber.AppendChild(SubscriberSubscriber);
			Subscriber.AppendChild(SubscriberRelationship);
			Subscriber.AppendChild(SubscriberGender);
			EligNode.AppendChild(Subscriber);
			//  Prepare Information Receiver Node
			XmlNode RenderingProvider = doc.CreateNode(XmlNodeType.Element,"RenderingProvider","");
			XmlNode RenderingAddress = doc.CreateNode(XmlNodeType.Element,"Address","");
			XmlNode RenderingAddressName = doc.CreateNode(XmlNodeType.Element,"Name","");
			XmlNode RenderingAddressFirstName = doc.CreateNode(XmlNodeType.Element,"FirstName","");
			XmlNode RenderingAddressLastName = doc.CreateNode(XmlNodeType.Element,"LastName","");
			// Get Rendering Provider first and lastname
			// Read Patient FName,LName,DOB, and Gender from Patient Table
			command = @"SELECT Fname,Lname from provider
                        WHERE provnum in (select priprov from 
                        patient where patnum = " + PatPlanCur.PatNum + ")";
			table = General.GetTable(command);
			if(table.Rows.Count != 0) {
				renderingProviderFirstName = PIn.PString(table.Rows[0][0].ToString());
				renderingProviderLastName = PIn.PString(table.Rows[0][1].ToString());
			}
			else {
				renderingProviderFirstName = infoReceiverFirstName;
				renderingProviderLastName = infoReceiverLastName;
			};
			RenderingAddressFirstName.InnerText = renderingProviderFirstName;
			RenderingAddressLastName.InnerText = renderingProviderLastName;
			RenderingAddressName.AppendChild(RenderingAddressFirstName);
			RenderingAddressName.AppendChild(RenderingAddressLastName);
			XmlNode RenderingAddressLine1 = doc.CreateNode(XmlNodeType.Element,"AddressLine1","");
			XmlNode RenderingAddressLine2 = doc.CreateNode(XmlNodeType.Element,"AddressLine2","");
			XmlNode RenderingPhone = doc.CreateNode(XmlNodeType.Element,"Phone","");
			XmlNode RenderingCity = doc.CreateNode(XmlNodeType.Element,"City","");
			XmlNode RenderingState = doc.CreateNode(XmlNodeType.Element,"State","");
			XmlNode RenderingZip = doc.CreateNode(XmlNodeType.Element,"Zip","");
			RenderingAddressLine1.InnerText = practiceAddress1;
			RenderingAddressLine2.InnerText = practiceAddress2;
			RenderingPhone.InnerText = practicePhone;
			RenderingCity.InnerText = practiceCity;
			RenderingState.InnerText = practiceState;
			RenderingZip.InnerText = practiceZip;
			RenderingAddress.AppendChild(RenderingAddressName);
			RenderingAddress.AppendChild(RenderingAddressLine1);
			RenderingAddress.AppendChild(RenderingAddressLine2);
			RenderingAddress.AppendChild(RenderingPhone);
			RenderingAddress.AppendChild(RenderingCity);
			RenderingAddress.AppendChild(RenderingState);
			RenderingAddress.AppendChild(RenderingZip);
			XmlNode RenderingCredential = doc.CreateNode(XmlNodeType.Element,"Credential","");
			XmlNode RenderingCredentialType = doc.CreateNode(XmlNodeType.Element,"Type","");
			XmlNode RenderingCredentialValue = doc.CreateNode(XmlNodeType.Element,"Value","");
			RenderingCredentialType.InnerText = "TJ";
			RenderingCredentialValue.InnerText = "123456789";
			RenderingCredential.AppendChild(RenderingCredentialType);
			RenderingCredential.AppendChild(RenderingCredentialValue);
			XmlNode RenderingTaxonomyCode = doc.CreateNode(XmlNodeType.Element,"TaxonomyCode","");
			RenderingTaxonomyCode.InnerText = TaxoCode;
			RenderingProvider.AppendChild(RenderingAddress);
			RenderingProvider.AppendChild(RenderingCredential);
			RenderingProvider.AppendChild(RenderingTaxonomyCode);
			//  Append RenderingProvider To EligNode
			EligNode.AppendChild(RenderingProvider);
			return doc.OuterXml;
		}

		private void ProcessEligibilityResponseDentalXchange(string DCIResponse) {
			XmlDocument doc = new XmlDocument();
			XmlNode IsEligibleNode;
			string IsEligibleStatus;
			doc.LoadXml(DCIResponse);
			IsEligibleNode = doc.SelectSingleNode("EligBenefitResponse/isEligible");
			switch(IsEligibleNode.InnerText) {
				case "0": // SPK
					IsEligibleStatus = textSubscriber.Text + " is Eligible";
					XmlNode GroupNum;
					GroupNum = doc.SelectSingleNode("EligBenefitResponse/Subscriber/Plan/GroupNum");
					textGroupNum.Text = GroupNum.InnerText;
					break;
				case "1": // SPK
					// Process Error code and Message Node AAD
					XmlNode ErrorCode;
					XmlNode ErrorMessage;
					ErrorCode = doc.SelectSingleNode("EligBenefitResponse/Response/ErrorCode");
					ErrorMessage = doc.SelectSingleNode("EligBenefitResponse/Response/ErrorMsg");
					IsEligibleStatus = textSubscriber.Text + " is Not Eligible. Error Code:";
					IsEligibleStatus += ErrorCode.InnerText + " Error Description:" + ErrorMessage.InnerText;
					break;
				default:
					IsEligibleStatus = textSubscriber.Text + " Eligibility status is Unknown";
					break;
			};
			MessageBox.Show(IsEligibleStatus);
		}

		private void labelViewRequestDocument_Click(object sender,EventArgs e) {
			DataTable table;
			string loginID;
			string passWord;
			string command;
			// Get Login / Password
			command = @"select loginid,password from clearinghouse where isDefault=1";
			table = General.GetTable(command);
			if(table.Rows.Count != 0) {
				loginID = PIn.PString(table.Rows[0][0].ToString());
				passWord = PIn.PString(table.Rows[0][1].ToString());
			}
			else {
				loginID = "";
				passWord = "";
			}
			MsgBoxCopyPaste MB = new MsgBoxCopyPaste(PrepareEligibilityRequestDentalXchange(loginID,passWord));
			MB.ShowDialog();
		}
		#endregion

		private void butOK_Click(object sender,System.EventArgs e) {
			if(textDateEffect.errorProvider1.GetError(textDateEffect)!=""
				||textDateTerm.errorProvider1.GetError(textDateTerm)!=""
				||textDentaide.errorProvider1.GetError(textDentaide)!=""
				) {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textSubscriberID.Text==""&&!IsForAll) {
				MsgBox.Show(this,"Subscriber ID not allowed to be blank.");
				return;
			}
			if(textCarrier.Text=="") {
				MsgBox.Show(this,"Carrier not allowed to be blank.");
				return;
			}
			if(PatPlanCur!=null && textOrdinal.errorProvider1.GetError(textOrdinal)!=""){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			//Subscriber: Can never be changed once a plan is created.
			PlanCur.SubscriberID=textSubscriberID.Text;
			PlanCur.DateEffective=PIn.PDate(textDateEffect.Text);
			PlanCur.DateTerm=PIn.PDate(textDateTerm.Text);
			GetEmployerNum();
			PlanCur.GroupName=textGroupName.Text;
			PlanCur.GroupNum=textGroupNum.Text;
			PlanCur.DivisionNo=textDivisionNo.Text;//only visible in Canada
			//carrier-----------------------------------------------------------------------------------------------------
			CarrierCur=new Carrier();
			CarrierCur.CarrierName=textCarrier.Text;
			CarrierCur.Phone=textPhone.Text;
			CarrierCur.Address=textAddress.Text;
			CarrierCur.Address2=textAddress2.Text;
			CarrierCur.City=textCity.Text;
			CarrierCur.State=textState.Text;
			CarrierCur.Zip=textZip.Text;
			CarrierCur.ElectID=textElectID.Text;
			CarrierCur.NoSendElect=checkNoSendElect.Checked;
			try{
				Carriers.GetCurSame(CarrierCur);
			}
			catch(ApplicationException ex){
				//the catch is just to display a message to the user.  It doesn't affect the success of the function.
				MessageBox.Show(ex.Message);
			}	
			PlanCur.CarrierNum=CarrierCur.CarrierNum;
			//plantype already handled.
			if(comboClaimForm.SelectedIndex!=-1)
				PlanCur.ClaimFormNum=ClaimForms.ListShort[comboClaimForm.SelectedIndex].ClaimFormNum;
			PlanCur.UseAltCode=checkAlternateCode.Checked;
			PlanCur.IsMedical=checkIsMedical.Checked;
			PlanCur.ClaimsUseUCR=checkClaimsUseUCR.Checked;
			PlanCur.DedBeforePerc=checkDedBeforePerc.Checked;
			PlanCur.ShowBaseUnits=checkShowBaseUnits.Checked;
			if(comboFeeSched.SelectedIndex==0)
				PlanCur.FeeSched=0;
			else
				PlanCur.FeeSched=FeeSchedsStandard[comboFeeSched.SelectedIndex-1].DefNum;
			if(comboCopay.SelectedIndex==0)
				PlanCur.CopayFeeSched=0;
			else
				PlanCur.CopayFeeSched=FeeSchedsCopay[comboCopay.SelectedIndex-1].DefNum;
			if(comboAllowedFeeSched.SelectedIndex==0)
				PlanCur.AllowedFeeSched=0;
			else
				PlanCur.AllowedFeeSched=FeeSchedsAllowed[comboAllowedFeeSched.SelectedIndex-1].DefNum;
			PlanCur.FilingCode=(InsFilingCode)comboFilingCode.SelectedIndex;
			PlanCur.DentaideCardSequence=PIn.PInt(textDentaide.Text);
			PlanCur.TrojanID=textTrojanID.Text;
			PlanCur.PlanNote=textPlanNote.Text;
			PlanCur.ReleaseInfo=checkRelease.Checked;
			PlanCur.AssignBen=checkAssign.Checked;
			PlanCur.SubscNote=textSubscNote.Text;
			//end of filling planCur.
			Cursor=Cursors.WaitCursor;
			if(PatPlanCur!=null) {
				if(PIn.PInt(textOrdinal.Text)!=PatPlanCur.Ordinal){//if ordinal changed
					PatPlans.SetOrdinal(PatPlanCur.PatPlanNum,PIn.PInt(textOrdinal.Text));
				}
				PatPlanCur.IsPending=checkIsPending.Checked;
				PatPlanCur.Relationship=(Relat)comboRelationship.SelectedIndex;
				PatPlanCur.PatID=textPatID.Text;
				PatPlans.Update(PatPlanCur);
			}
			//Update the plan(s)
			List<int> planNums=InsPlans.GetPlanNumsOfSamePlans(Employers.GetName(PlanCurOld.EmployerNum),PlanCurOld.GroupName,
				PlanCurOld.GroupNum,PlanCurOld.DivisionNo,Carriers.GetName(PlanCurOld.CarrierNum),PlanCurOld.IsMedical,
				PlanCurOld.PlanNum,true);//include the current plan.
			if(!IsForAll){
				InsPlans.Update(PlanCur);//This is done regardless of checkApplyAll. Catches fields which are not synched.
			}
			if(checkApplyAll.Checked){//also triggered when IsForAll, because box is checked but hidden
				InsPlans.UpdateForLike(PlanCurOld,PlanCur);
				bool changed=Benefits.UpdateListForIdentical(benefitListOld,benefitList,planNums);
				//if(changed){
					//for(int i=0;i<planNums.Count;i++){
						//InsPlans.ComputeEstimatesForPlan(planNums[i]);
						//Eliminated in 5.0 because of severe speed issues.
					//}
				//}
			}
			else{//also triggered when IsNew, because box is unchecked and hidden
				Benefits.UpdateList(benefitListOld,benefitList);
				InsPlans.ComputeEstimatesForPlan(PlanCur.PlanNum);
			}
			//Synchronize PlanNote----------------------------------------------------------------------------------------------
			if(planNums.Count==1 && IsForAll){
				InsPlans.UpdateNoteForPlans(planNums,PlanCur.PlanNote);
				Cursor=Cursors.Default;
				DialogResult=DialogResult.OK;
				return;
			}
			if(planNums.Count<=1){//if no similar plans
				//note has already been saved
				Cursor=Cursors.Default;
				DialogResult=DialogResult.OK;
				return;
			}
			//Get a list of all distinct notes, not including this one
			string[] notesSimilar=InsPlans.GetNotesForPlans(planNums,PlanCur.PlanNum);//excludes PlanCur (0 if IsForAll)
			bool curNoteInSimilar=false;
			for(int i=0;i<notesSimilar.Length;i++){
				if(notesSimilar[i]==PlanCur.PlanNote){
					curNoteInSimilar=true;
				}
			}
			string[] notesAll;//this has all distinct notes INCLUDING PlanCur.PlanNote
			if(curNoteInSimilar){//the curNote is alread in notesSimilar
				notesAll=new string[notesSimilar.Length];
				notesSimilar.CopyTo(notesAll,0);
			}
			else{
				notesAll=new string[notesSimilar.Length+1];
				notesSimilar.CopyTo(notesAll,1);
				notesAll[0]=PlanCur.PlanNote;//curNote will be at position 0
			}
			if(notesSimilar.Length==0){
				//probably because there are not even any other similar plans
				//note has already been saved
			}
			else if(notesSimilar.Length==1){
				if(notesSimilar[0]==PlanCur.PlanNote){//all notes are already the same
					//note has already been saved
				}
				else if(notesSimilar[0]==PlanCurOld.PlanNote){//notes were all the same until user just changed it
					//this also handles 'deleting' a note
					InsPlans.UpdateNoteForPlans(planNums,PlanCur.PlanNote);
				}
				//this note is different than the other notes
				else if(IsNewPlan){//but it's a new plan, so user simply isn't aware of difference
					//if(PlanCur.PlanNote==""){//user never entered a note for the new plan. Still must inform user of note.
					//must give user a choice of notes
					FormNotePick FormN=new FormNotePick(notesAll);
					FormN.ShowDialog();
					if(FormN.DialogResult==DialogResult.OK){
						InsPlans.UpdateNoteForPlans(planNums,FormN.SelectedNote);
					}
					//If user cancels, the note for this insplan is already saved, just no synch happens.
				}
			}
			else{//notesSimilar must be >1.  Can't really think of how this could happen except for improper conversion
				//must give user a choice of notes
				FormNotePick FormN=new FormNotePick(notesAll);
				FormN.ShowDialog();
				if(FormN.DialogResult==DialogResult.OK){
					InsPlans.UpdateNoteForPlans(planNums,FormN.SelectedNote);
				}
				//If user cancels, the note for this insplan is already saved, just no synch happens.				
			}
			Cursor=Cursors.Default;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormInsPlan_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNewPlan){//this would also be new coverage
				InsPlans.Delete(PlanCur);//also drops
			}
			else if(IsNewPatPlan){//but plan is not new
				PatPlans.Delete(PatPlanCur.PatPlanNum);//no need to check dependencies.  Maintains ordinals and recomputes estimates.
			}
		}

		

		

		

		

		

	}
}
