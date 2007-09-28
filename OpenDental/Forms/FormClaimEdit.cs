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
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormClaimEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label labelNote;
		private System.Windows.Forms.GroupBox groupProsth;
		private System.Windows.Forms.Label labelMissingTeeth;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox textPreAuth;
		private OpenDental.ValidDate textDateRec;
		private OpenDental.ValidDate textDateSent;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private OpenDental.UI.Button butOK;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.RadioButton radioProsthN;
		private System.Windows.Forms.RadioButton radioProsthR;
		private System.Windows.Forms.RadioButton radioProsthI;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textPlan;
		private System.Windows.Forms.TextBox textClaimFee;
		private OpenDental.ValidDouble textDedApplied;
		private OpenDental.ValidDouble textInsPayAmt;
		private OpenDental.ValidDate textPriorDate;
		//private double ClaimFee;
		//private double PriInsPayEstSubtotal;
		//private double SecInsPayEstSubtotal;
		//private double PriInsPayEst;
		//private double SecInsPayEst;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.GroupBox groupOrtho;
		private System.Windows.Forms.Label labelOrthoRemainM;
		private System.Windows.Forms.CheckBox checkIsOrtho;
		private OpenDental.ValidNum textOrthoRemainM;
		private OpenDental.ValidDate textOrthoDate;
		private System.Windows.Forms.Label labelOrthoDate;
		//private FormClaimSupplemental FormCS=new FormClaimSupplemental();
		private System.Windows.Forms.Label labelPreAuthNum;
		private System.Windows.Forms.Label labelDateService;
		private OpenDental.UI.Button butSupp;
		private System.Windows.Forms.ComboBox comboProvBill;
		private System.Windows.Forms.ComboBox comboProvTreat;
		private System.Windows.Forms.ListBox listClaimStatus;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.ListBox listClaimType;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox comboPatRelat;
		private System.Windows.Forms.ComboBox comboPatRelat2;
		private System.Windows.Forms.TextBox textPlan2;
		private OpenDental.UI.Button butRecalc;
		private System.Windows.Forms.TextBox textInsPayEst;
		private OpenDental.ValidDouble textWriteOff;
		private OpenDental.UI.Button butPreview;
		private OpenDental.UI.Button butPrint;
		private OpenDental.UI.Button butOtherNone;
		private System.Windows.Forms.Label labelRadiographs;
		private OpenDental.ValidNum textRadiographs;
		private OpenDental.UI.Button butOtherCovChange;
		//private double DedAdjPerc;
		private ClaimProc[] ClaimProcsForClaim;
		///<summary>All claimprocs for the patient. Used to calculate remaining benefits, etc.</summary>
		private ClaimProc[] ClaimProcList;
		private OpenDental.ODtextBox textNote;
		/// <summary>List of all procedures for this patient.  Used to get descriptions, etc.</summary>
		private Procedure[] ProcList;
		private Patient PatCur;
		private Family FamCur;
		private InsPlan[] PlanList;
		private OpenDental.ValidDate textDateService;
		private DataTable tablePayments;
			//ClaimPayment[] ClaimPaymentList;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		private OpenDental.UI.Button butLabel;
		private OpenDental.UI.Button butSplit;
		//private User user;
		private bool notAuthorized;
		private PatPlan[] PatPlanList;
		private OpenDental.UI.ODGrid gridProc;
		private OpenDental.UI.Button butSend;
		private Claim ClaimCur;
		private GroupBox groupValueCodes;
		private Label label15;
		private Label label14;
		private Label label13;
		private Label label12;
		private Label label11;
		private Label label22;
		private Label label25;
		private TextBox textVC9Amount;
		private TextBox textVC6Amount;
		private TextBox textVC3Amount;
		private TextBox textVC0Amount;
		private TextBox textVC9Code;
		private TextBox textVC6Code;
		private TextBox textVC3Code;
		private TextBox textVC0Code;
		private Label label26;
		private Label label30;
		private Label label31;
		private Label label32;
		private Label label33;
		private Label label34;
		private Label label35;
		private TextBox textVC10Amount;
		private TextBox textVC7Amount;
		private TextBox textVC4Amount;
		private TextBox textVC1Amount;
		private TextBox textVC10Code;
		private TextBox textVC7Code;
		private TextBox textVC4Code;
		private TextBox textVC1Code;
		private Label label17;
		private Label label19;
		private Label label23;
		private Label label24;
		private Label label28;
		private Label label29;
		private Label label27;
		private TextBox textVC11Amount;
		private TextBox textVC8Amount;
		private TextBox textVC5Amount;
		private TextBox textVC2Amount;
		private TextBox textVC11Code;
		private TextBox textVC8Code;
		private TextBox textVC5Code;
		private TextBox textVC2Code;
		private Label label36;
		private Label label37;
		private Label label38;
		private Label label39;
		private Label label40;
		private Label label41;
		private ClaimForm ClaimFormCur;
		private Label label20;
		private Panel panelBottomEdge;
		private Panel panelRightEdge;
		private TabControl tabMain;
		private TabPage tabUB04;
		private TabPage tabGeneral;
		private ODGrid gridPay;
		private Label label7;
		private OpenDental.UI.Button butCheckAdd;
		private GroupBox groupEnterPayment;
		private OpenDental.UI.Button butPaySupp;
		private OpenDental.UI.Button butPayTotal;
		private OpenDental.UI.Button butPayProc;
		private TextBox textReasonUnder;
		private Label label4;
		private Label label42;
		private ComboBox comboAccident;
		private Label label43;
		private ComboBox comboEmployRelated;
		private TextBox textAccidentST;
		private ComboBox comboPlaceService;
		private ValidDate textAccidentDate;
		private TextBox textRefProv;
		private Label label44;
		private OpenDental.UI.Button butReferralSelect;
		private OpenDental.UI.Button butReferralNone;
		private Label label45;
		private Label label46;
		private TextBox textRefNum;
		private Label label47;
		private Label label48;
		private Label label49;
		private OpenDental.UI.Button butReferralEdit;
		private GroupBox groupBox1;
		private TextBox textCode10;
		private TextBox textCode9;
		private TextBox textCode8;
		private TextBox textCode7;
		private TextBox textCode6;
		private TextBox textCode5;
		private TextBox textCode4;
		private TextBox textCode3;
		private TextBox textCode2;
		private TextBox textCode1;
		private TextBox textCode0;
		private Label label60;
		private Label label59;
		private Label label58;
		private Label label57;
		private Label label56;
		private Label label55;
		private Label label54;
		private Label label53;
		private Label label52;
		private Label label51;
		private Label label50;
		private ArrayList ClaimValCodes;
		private GroupBox groupBox4;
		private ClaimCondCodes CurCondCodes;

		///<summary></summary>
		public FormClaimEdit(Claim claimCur, Patient patCur,Family famCur){
			PatCur=patCur;
			FamCur=famCur;
			ClaimCur=claimCur;
			if(ClaimCur.ClaimForm != 0){
				ClaimFormCur=ClaimForms.GetClaimForm(ClaimCur.ClaimForm);
				ClaimValCodes=ClaimValCodeLog.GetValCodes(ClaimCur);
				CurCondCodes=ClaimCondCodeLog.GetAllCodes(ClaimCur.ClaimNum);
			}
			InitializeComponent();// Required for Windows Form Designer support
			//tbPay.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPay_CellDoubleClicked);
			//tbProc.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbProc_CellClicked);
			//tbPay.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPay_CellClicked);
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClaimEdit));
			this.label3 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.labelDateService = new System.Windows.Forms.Label();
			this.labelPreAuthNum = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.labelNote = new System.Windows.Forms.Label();
			this.groupProsth = new System.Windows.Forms.GroupBox();
			this.labelMissingTeeth = new System.Windows.Forms.Label();
			this.textPriorDate = new OpenDental.ValidDate();
			this.label18 = new System.Windows.Forms.Label();
			this.radioProsthN = new System.Windows.Forms.RadioButton();
			this.radioProsthR = new System.Windows.Forms.RadioButton();
			this.radioProsthI = new System.Windows.Forms.RadioButton();
			this.textInsPayEst = new System.Windows.Forms.TextBox();
			this.textPreAuth = new System.Windows.Forms.TextBox();
			this.textClaimFee = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textPlan = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.groupOrtho = new System.Windows.Forms.GroupBox();
			this.textOrthoDate = new OpenDental.ValidDate();
			this.labelOrthoDate = new System.Windows.Forms.Label();
			this.textOrthoRemainM = new OpenDental.ValidNum();
			this.checkIsOrtho = new System.Windows.Forms.CheckBox();
			this.labelOrthoRemainM = new System.Windows.Forms.Label();
			this.comboProvBill = new System.Windows.Forms.ComboBox();
			this.comboProvTreat = new System.Windows.Forms.ComboBox();
			this.listClaimStatus = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.listClaimType = new System.Windows.Forms.ListBox();
			this.label9 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.comboPatRelat = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.butOtherNone = new OpenDental.UI.Button();
			this.butOtherCovChange = new OpenDental.UI.Button();
			this.comboPatRelat2 = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textPlan2 = new System.Windows.Forms.TextBox();
			this.labelRadiographs = new System.Windows.Forms.Label();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.groupValueCodes = new System.Windows.Forms.GroupBox();
			this.textVC11Amount = new System.Windows.Forms.TextBox();
			this.textVC8Amount = new System.Windows.Forms.TextBox();
			this.textVC5Amount = new System.Windows.Forms.TextBox();
			this.textVC2Amount = new System.Windows.Forms.TextBox();
			this.textVC11Code = new System.Windows.Forms.TextBox();
			this.textVC8Code = new System.Windows.Forms.TextBox();
			this.textVC5Code = new System.Windows.Forms.TextBox();
			this.textVC2Code = new System.Windows.Forms.TextBox();
			this.label36 = new System.Windows.Forms.Label();
			this.label37 = new System.Windows.Forms.Label();
			this.label38 = new System.Windows.Forms.Label();
			this.label39 = new System.Windows.Forms.Label();
			this.label40 = new System.Windows.Forms.Label();
			this.label41 = new System.Windows.Forms.Label();
			this.textVC10Amount = new System.Windows.Forms.TextBox();
			this.textVC7Amount = new System.Windows.Forms.TextBox();
			this.textVC4Amount = new System.Windows.Forms.TextBox();
			this.textVC1Amount = new System.Windows.Forms.TextBox();
			this.textVC10Code = new System.Windows.Forms.TextBox();
			this.textVC7Code = new System.Windows.Forms.TextBox();
			this.textVC4Code = new System.Windows.Forms.TextBox();
			this.textVC1Code = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.textVC9Amount = new System.Windows.Forms.TextBox();
			this.textVC6Amount = new System.Windows.Forms.TextBox();
			this.textVC3Amount = new System.Windows.Forms.TextBox();
			this.textVC0Amount = new System.Windows.Forms.TextBox();
			this.textVC9Code = new System.Windows.Forms.TextBox();
			this.textVC6Code = new System.Windows.Forms.TextBox();
			this.textVC3Code = new System.Windows.Forms.TextBox();
			this.textVC0Code = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.label34 = new System.Windows.Forms.Label();
			this.label35 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.panelBottomEdge = new System.Windows.Forms.Panel();
			this.panelRightEdge = new System.Windows.Forms.Panel();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.label42 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label45 = new System.Windows.Forms.Label();
			this.textRefProv = new System.Windows.Forms.TextBox();
			this.butReferralEdit = new OpenDental.UI.Button();
			this.label47 = new System.Windows.Forms.Label();
			this.butReferralNone = new OpenDental.UI.Button();
			this.butReferralSelect = new OpenDental.UI.Button();
			this.textRefNum = new System.Windows.Forms.TextBox();
			this.label46 = new System.Windows.Forms.Label();
			this.comboAccident = new System.Windows.Forms.ComboBox();
			this.textRadiographs = new OpenDental.ValidNum();
			this.label43 = new System.Windows.Forms.Label();
			this.comboEmployRelated = new System.Windows.Forms.ComboBox();
			this.textAccidentST = new System.Windows.Forms.TextBox();
			this.textNote = new OpenDental.ODtextBox();
			this.comboPlaceService = new System.Windows.Forms.ComboBox();
			this.textAccidentDate = new OpenDental.ValidDate();
			this.label48 = new System.Windows.Forms.Label();
			this.label49 = new System.Windows.Forms.Label();
			this.label44 = new System.Windows.Forms.Label();
			this.tabUB04 = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label60 = new System.Windows.Forms.Label();
			this.label59 = new System.Windows.Forms.Label();
			this.label58 = new System.Windows.Forms.Label();
			this.label57 = new System.Windows.Forms.Label();
			this.label56 = new System.Windows.Forms.Label();
			this.label55 = new System.Windows.Forms.Label();
			this.label54 = new System.Windows.Forms.Label();
			this.label53 = new System.Windows.Forms.Label();
			this.label52 = new System.Windows.Forms.Label();
			this.label51 = new System.Windows.Forms.Label();
			this.label50 = new System.Windows.Forms.Label();
			this.textCode10 = new System.Windows.Forms.TextBox();
			this.textCode9 = new System.Windows.Forms.TextBox();
			this.textCode8 = new System.Windows.Forms.TextBox();
			this.textCode7 = new System.Windows.Forms.TextBox();
			this.textCode6 = new System.Windows.Forms.TextBox();
			this.textCode5 = new System.Windows.Forms.TextBox();
			this.textCode4 = new System.Windows.Forms.TextBox();
			this.textCode3 = new System.Windows.Forms.TextBox();
			this.textCode2 = new System.Windows.Forms.TextBox();
			this.textCode1 = new System.Windows.Forms.TextBox();
			this.textCode0 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.groupEnterPayment = new System.Windows.Forms.GroupBox();
			this.butPaySupp = new OpenDental.UI.Button();
			this.butPayTotal = new OpenDental.UI.Button();
			this.butPayProc = new OpenDental.UI.Button();
			this.textReasonUnder = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.gridPay = new OpenDental.UI.ODGrid();
			this.butCheckAdd = new OpenDental.UI.Button();
			this.butSend = new OpenDental.UI.Button();
			this.gridProc = new OpenDental.UI.ODGrid();
			this.butSplit = new OpenDental.UI.Button();
			this.butLabel = new OpenDental.UI.Button();
			this.textDateService = new OpenDental.ValidDate();
			this.textWriteOff = new OpenDental.ValidDouble();
			this.textInsPayAmt = new OpenDental.ValidDouble();
			this.textDedApplied = new OpenDental.ValidDouble();
			this.textDateSent = new OpenDental.ValidDate();
			this.textDateRec = new OpenDental.ValidDate();
			this.butPreview = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.butRecalc = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butSupp = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupProsth.SuspendLayout();
			this.groupOrtho.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupValueCodes.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.tabUB04.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupEnterPayment.SuspendLayout();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(241, 98);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(94, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "Billing Dentist";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(2, 135);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(108, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Date Received";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(5, 114);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(104, 16);
			this.label8.TabIndex = 7;
			this.label8.Text = "DateSent";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelDateService
			// 
			this.labelDateService.Location = new System.Drawing.Point(3, 94);
			this.labelDateService.Name = "labelDateService";
			this.labelDateService.Size = new System.Drawing.Size(107, 16);
			this.labelDateService.TabIndex = 8;
			this.labelDateService.Text = "Date of Service";
			this.labelDateService.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelPreAuthNum
			// 
			this.labelPreAuthNum.Location = new System.Drawing.Point(230, 142);
			this.labelPreAuthNum.Name = "labelPreAuthNum";
			this.labelPreAuthNum.Size = new System.Drawing.Size(107, 16);
			this.labelPreAuthNum.TabIndex = 11;
			this.labelPreAuthNum.Text = "PreAuth Number";
			this.labelPreAuthNum.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(6, 40);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(157, 16);
			this.label16.TabIndex = 16;
			this.label16.Text = "Prior Date of Placement";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelNote
			// 
			this.labelNote.Location = new System.Drawing.Point(626, 3);
			this.labelNote.Name = "labelNote";
			this.labelNote.Size = new System.Drawing.Size(299, 16);
			this.labelNote.TabIndex = 19;
			this.labelNote.Text = "Claim Note (this will show on the claim when submitted)";
			// 
			// groupProsth
			// 
			this.groupProsth.BackColor = System.Drawing.SystemColors.Window;
			this.groupProsth.Controls.Add(this.labelMissingTeeth);
			this.groupProsth.Controls.Add(this.textPriorDate);
			this.groupProsth.Controls.Add(this.label18);
			this.groupProsth.Controls.Add(this.radioProsthN);
			this.groupProsth.Controls.Add(this.radioProsthR);
			this.groupProsth.Controls.Add(this.radioProsthI);
			this.groupProsth.Controls.Add(this.label16);
			this.groupProsth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupProsth.Location = new System.Drawing.Point(3, 3);
			this.groupProsth.Name = "groupProsth";
			this.groupProsth.Size = new System.Drawing.Size(286, 114);
			this.groupProsth.TabIndex = 9;
			this.groupProsth.TabStop = false;
			this.groupProsth.Text = "Crown, Bridge, or Denture";
			// 
			// labelMissingTeeth
			// 
			this.labelMissingTeeth.Location = new System.Drawing.Point(3, 77);
			this.labelMissingTeeth.Name = "labelMissingTeeth";
			this.labelMissingTeeth.Size = new System.Drawing.Size(280, 32);
			this.labelMissingTeeth.TabIndex = 28;
			this.labelMissingTeeth.Text = "For bridges, dentures, and partials, missing teeth must have been correctly enter" +
				"ed in the Chart module. ";
			// 
			// textPriorDate
			// 
			this.textPriorDate.Location = new System.Drawing.Point(168, 36);
			this.textPriorDate.Name = "textPriorDate";
			this.textPriorDate.Size = new System.Drawing.Size(66, 20);
			this.textPriorDate.TabIndex = 3;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(6, 60);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(246, 18);
			this.label18.TabIndex = 29;
			this.label18.Text = "(Might need a note. Might need to attach x-ray)";
			// 
			// radioProsthN
			// 
			this.radioProsthN.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioProsthN.Location = new System.Drawing.Point(12, 18);
			this.radioProsthN.Name = "radioProsthN";
			this.radioProsthN.Size = new System.Drawing.Size(46, 16);
			this.radioProsthN.TabIndex = 0;
			this.radioProsthN.Text = "No";
			this.radioProsthN.Click += new System.EventHandler(this.radioProsthN_Click);
			// 
			// radioProsthR
			// 
			this.radioProsthR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioProsthR.Location = new System.Drawing.Point(132, 18);
			this.radioProsthR.Name = "radioProsthR";
			this.radioProsthR.Size = new System.Drawing.Size(104, 16);
			this.radioProsthR.TabIndex = 2;
			this.radioProsthR.Text = "Replacement";
			this.radioProsthR.Click += new System.EventHandler(this.radioProsthR_Click);
			// 
			// radioProsthI
			// 
			this.radioProsthI.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioProsthI.Location = new System.Drawing.Point(64, 18);
			this.radioProsthI.Name = "radioProsthI";
			this.radioProsthI.Size = new System.Drawing.Size(64, 16);
			this.radioProsthI.TabIndex = 1;
			this.radioProsthI.Text = "Initial";
			this.radioProsthI.Click += new System.EventHandler(this.radioProsthI_Click);
			// 
			// textInsPayEst
			// 
			this.textInsPayEst.Location = new System.Drawing.Point(494, 363);
			this.textInsPayEst.Name = "textInsPayEst";
			this.textInsPayEst.ReadOnly = true;
			this.textInsPayEst.Size = new System.Drawing.Size(65, 20);
			this.textInsPayEst.TabIndex = 40;
			this.textInsPayEst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPreAuth
			// 
			this.textPreAuth.Location = new System.Drawing.Point(336, 138);
			this.textPreAuth.Name = "textPreAuth";
			this.textPreAuth.Size = new System.Drawing.Size(170, 20);
			this.textPreAuth.TabIndex = 1;
			// 
			// textClaimFee
			// 
			this.textClaimFee.Location = new System.Drawing.Point(364, 363);
			this.textClaimFee.Name = "textClaimFee";
			this.textClaimFee.ReadOnly = true;
			this.textClaimFee.Size = new System.Drawing.Size(65, 20);
			this.textClaimFee.TabIndex = 51;
			this.textClaimFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(244, 366);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 15);
			this.label1.TabIndex = 50;
			this.label1.Text = "Totals";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPlan
			// 
			this.textPlan.Location = new System.Drawing.Point(8, 20);
			this.textPlan.Name = "textPlan";
			this.textPlan.ReadOnly = true;
			this.textPlan.Size = new System.Drawing.Size(253, 20);
			this.textPlan.TabIndex = 1;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(234, 119);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(102, 15);
			this.label21.TabIndex = 93;
			this.label21.Text = "Treating Dentist";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupOrtho
			// 
			this.groupOrtho.BackColor = System.Drawing.SystemColors.Window;
			this.groupOrtho.Controls.Add(this.textOrthoDate);
			this.groupOrtho.Controls.Add(this.labelOrthoDate);
			this.groupOrtho.Controls.Add(this.textOrthoRemainM);
			this.groupOrtho.Controls.Add(this.checkIsOrtho);
			this.groupOrtho.Controls.Add(this.labelOrthoRemainM);
			this.groupOrtho.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupOrtho.Location = new System.Drawing.Point(308, 3);
			this.groupOrtho.Name = "groupOrtho";
			this.groupOrtho.Size = new System.Drawing.Size(192, 102);
			this.groupOrtho.TabIndex = 11;
			this.groupOrtho.TabStop = false;
			this.groupOrtho.Text = "Ortho";
			// 
			// textOrthoDate
			// 
			this.textOrthoDate.Location = new System.Drawing.Point(115, 36);
			this.textOrthoDate.Name = "textOrthoDate";
			this.textOrthoDate.Size = new System.Drawing.Size(66, 20);
			this.textOrthoDate.TabIndex = 1;
			// 
			// labelOrthoDate
			// 
			this.labelOrthoDate.Location = new System.Drawing.Point(5, 40);
			this.labelOrthoDate.Name = "labelOrthoDate";
			this.labelOrthoDate.Size = new System.Drawing.Size(109, 16);
			this.labelOrthoDate.TabIndex = 104;
			this.labelOrthoDate.Text = "Date of Placement";
			this.labelOrthoDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textOrthoRemainM
			// 
			this.textOrthoRemainM.Location = new System.Drawing.Point(116, 59);
			this.textOrthoRemainM.MaxVal = 255;
			this.textOrthoRemainM.MinVal = 0;
			this.textOrthoRemainM.Name = "textOrthoRemainM";
			this.textOrthoRemainM.Size = new System.Drawing.Size(39, 20);
			this.textOrthoRemainM.TabIndex = 2;
			// 
			// checkIsOrtho
			// 
			this.checkIsOrtho.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsOrtho.Location = new System.Drawing.Point(12, 16);
			this.checkIsOrtho.Name = "checkIsOrtho";
			this.checkIsOrtho.Size = new System.Drawing.Size(90, 18);
			this.checkIsOrtho.TabIndex = 0;
			this.checkIsOrtho.Text = "Is For Ortho";
			// 
			// labelOrthoRemainM
			// 
			this.labelOrthoRemainM.Location = new System.Drawing.Point(3, 60);
			this.labelOrthoRemainM.Name = "labelOrthoRemainM";
			this.labelOrthoRemainM.Size = new System.Drawing.Size(112, 18);
			this.labelOrthoRemainM.TabIndex = 102;
			this.labelOrthoRemainM.Text = "Months Remaining";
			this.labelOrthoRemainM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboProvBill
			// 
			this.comboProvBill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvBill.Location = new System.Drawing.Point(336, 94);
			this.comboProvBill.Name = "comboProvBill";
			this.comboProvBill.Size = new System.Drawing.Size(100, 21);
			this.comboProvBill.TabIndex = 97;
			// 
			// comboProvTreat
			// 
			this.comboProvTreat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvTreat.Location = new System.Drawing.Point(336, 116);
			this.comboProvTreat.Name = "comboProvTreat";
			this.comboProvTreat.Size = new System.Drawing.Size(100, 21);
			this.comboProvTreat.TabIndex = 99;
			// 
			// listClaimStatus
			// 
			this.listClaimStatus.Location = new System.Drawing.Point(111, 8);
			this.listClaimStatus.Name = "listClaimStatus";
			this.listClaimStatus.Size = new System.Drawing.Size(120, 82);
			this.listClaimStatus.TabIndex = 103;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 14);
			this.label2.TabIndex = 104;
			this.label2.Text = "Claim Status";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listClaimType
			// 
			this.listClaimType.ForeColor = System.Drawing.SystemColors.WindowText;
			this.listClaimType.Location = new System.Drawing.Point(336, 24);
			this.listClaimType.Name = "listClaimType";
			this.listClaimType.Size = new System.Drawing.Size(98, 69);
			this.listClaimType.TabIndex = 108;
			this.listClaimType.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listClaimType_MouseUp);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(239, 26);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(95, 17);
			this.label9.TabIndex = 109;
			this.label9.Text = "Claim Type";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.comboPatRelat);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.textPlan);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(523, 2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(269, 70);
			this.groupBox2.TabIndex = 110;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Insurance Plan";
			// 
			// comboPatRelat
			// 
			this.comboPatRelat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPatRelat.Location = new System.Drawing.Point(90, 43);
			this.comboPatRelat.Name = "comboPatRelat";
			this.comboPatRelat.Size = new System.Drawing.Size(151, 21);
			this.comboPatRelat.TabIndex = 3;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 46);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(85, 17);
			this.label5.TabIndex = 2;
			this.label5.Text = "Relationship";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.butOtherNone);
			this.groupBox3.Controls.Add(this.butOtherCovChange);
			this.groupBox3.Controls.Add(this.comboPatRelat2);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.textPlan2);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(523, 73);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(269, 85);
			this.groupBox3.TabIndex = 111;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Other Coverage";
			// 
			// butOtherNone
			// 
			this.butOtherNone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOtherNone.Autosize = true;
			this.butOtherNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOtherNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOtherNone.CornerRadius = 4F;
			this.butOtherNone.Location = new System.Drawing.Point(183, 10);
			this.butOtherNone.Name = "butOtherNone";
			this.butOtherNone.Size = new System.Drawing.Size(78, 23);
			this.butOtherNone.TabIndex = 5;
			this.butOtherNone.Text = "None";
			this.butOtherNone.Click += new System.EventHandler(this.butOtherNone_Click);
			// 
			// butOtherCovChange
			// 
			this.butOtherCovChange.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOtherCovChange.Autosize = true;
			this.butOtherCovChange.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOtherCovChange.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOtherCovChange.CornerRadius = 4F;
			this.butOtherCovChange.Location = new System.Drawing.Point(101, 10);
			this.butOtherCovChange.Name = "butOtherCovChange";
			this.butOtherCovChange.Size = new System.Drawing.Size(78, 23);
			this.butOtherCovChange.TabIndex = 4;
			this.butOtherCovChange.Text = "Change";
			this.butOtherCovChange.Click += new System.EventHandler(this.butOtherCovChange_Click);
			// 
			// comboPatRelat2
			// 
			this.comboPatRelat2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPatRelat2.Location = new System.Drawing.Point(90, 57);
			this.comboPatRelat2.Name = "comboPatRelat2";
			this.comboPatRelat2.Size = new System.Drawing.Size(151, 21);
			this.comboPatRelat2.TabIndex = 3;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(6, 60);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(84, 17);
			this.label10.TabIndex = 2;
			this.label10.Text = "Relationship";
			// 
			// textPlan2
			// 
			this.textPlan2.Location = new System.Drawing.Point(8, 34);
			this.textPlan2.Name = "textPlan2";
			this.textPlan2.ReadOnly = true;
			this.textPlan2.Size = new System.Drawing.Size(253, 20);
			this.textPlan2.TabIndex = 1;
			// 
			// labelRadiographs
			// 
			this.labelRadiographs.Location = new System.Drawing.Point(648, 128);
			this.labelRadiographs.Name = "labelRadiographs";
			this.labelRadiographs.Size = new System.Drawing.Size(85, 18);
			this.labelRadiographs.TabIndex = 117;
			this.labelRadiographs.Text = "Radiographs";
			this.labelRadiographs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(336, 2);
			this.comboClinic.MaxDropDownItems = 100;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(126, 21);
			this.comboClinic.TabIndex = 121;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(235, 6);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(98, 16);
			this.labelClinic.TabIndex = 120;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupValueCodes
			// 
			this.groupValueCodes.Controls.Add(this.textVC11Amount);
			this.groupValueCodes.Controls.Add(this.textVC8Amount);
			this.groupValueCodes.Controls.Add(this.textVC5Amount);
			this.groupValueCodes.Controls.Add(this.textVC2Amount);
			this.groupValueCodes.Controls.Add(this.textVC11Code);
			this.groupValueCodes.Controls.Add(this.textVC8Code);
			this.groupValueCodes.Controls.Add(this.textVC5Code);
			this.groupValueCodes.Controls.Add(this.textVC2Code);
			this.groupValueCodes.Controls.Add(this.label36);
			this.groupValueCodes.Controls.Add(this.label37);
			this.groupValueCodes.Controls.Add(this.label38);
			this.groupValueCodes.Controls.Add(this.label39);
			this.groupValueCodes.Controls.Add(this.label40);
			this.groupValueCodes.Controls.Add(this.label41);
			this.groupValueCodes.Controls.Add(this.textVC10Amount);
			this.groupValueCodes.Controls.Add(this.textVC7Amount);
			this.groupValueCodes.Controls.Add(this.textVC4Amount);
			this.groupValueCodes.Controls.Add(this.textVC1Amount);
			this.groupValueCodes.Controls.Add(this.textVC10Code);
			this.groupValueCodes.Controls.Add(this.textVC7Code);
			this.groupValueCodes.Controls.Add(this.textVC4Code);
			this.groupValueCodes.Controls.Add(this.textVC1Code);
			this.groupValueCodes.Controls.Add(this.label17);
			this.groupValueCodes.Controls.Add(this.label19);
			this.groupValueCodes.Controls.Add(this.label23);
			this.groupValueCodes.Controls.Add(this.label24);
			this.groupValueCodes.Controls.Add(this.label28);
			this.groupValueCodes.Controls.Add(this.label29);
			this.groupValueCodes.Controls.Add(this.label27);
			this.groupValueCodes.Controls.Add(this.label26);
			this.groupValueCodes.Controls.Add(this.label25);
			this.groupValueCodes.Controls.Add(this.textVC9Amount);
			this.groupValueCodes.Controls.Add(this.textVC6Amount);
			this.groupValueCodes.Controls.Add(this.textVC3Amount);
			this.groupValueCodes.Controls.Add(this.textVC0Amount);
			this.groupValueCodes.Controls.Add(this.textVC9Code);
			this.groupValueCodes.Controls.Add(this.textVC6Code);
			this.groupValueCodes.Controls.Add(this.textVC3Code);
			this.groupValueCodes.Controls.Add(this.textVC0Code);
			this.groupValueCodes.Controls.Add(this.label22);
			this.groupValueCodes.Controls.Add(this.label15);
			this.groupValueCodes.Controls.Add(this.label14);
			this.groupValueCodes.Controls.Add(this.label13);
			this.groupValueCodes.Controls.Add(this.label12);
			this.groupValueCodes.Controls.Add(this.label11);
			this.groupValueCodes.Location = new System.Drawing.Point(22, 94);
			this.groupValueCodes.Name = "groupValueCodes";
			this.groupValueCodes.Size = new System.Drawing.Size(434, 114);
			this.groupValueCodes.TabIndex = 130;
			this.groupValueCodes.TabStop = false;
			this.groupValueCodes.Text = "Value Codes";
			// 
			// textVC11Amount
			// 
			this.textVC11Amount.Location = new System.Drawing.Point(343, 90);
			this.textVC11Amount.Name = "textVC11Amount";
			this.textVC11Amount.Size = new System.Drawing.Size(66, 20);
			this.textVC11Amount.TabIndex = 56;
			this.textVC11Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textVC8Amount
			// 
			this.textVC8Amount.Location = new System.Drawing.Point(343, 71);
			this.textVC8Amount.Name = "textVC8Amount";
			this.textVC8Amount.Size = new System.Drawing.Size(66, 20);
			this.textVC8Amount.TabIndex = 55;
			this.textVC8Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textVC5Amount
			// 
			this.textVC5Amount.Location = new System.Drawing.Point(343, 52);
			this.textVC5Amount.Name = "textVC5Amount";
			this.textVC5Amount.Size = new System.Drawing.Size(66, 20);
			this.textVC5Amount.TabIndex = 54;
			this.textVC5Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textVC2Amount
			// 
			this.textVC2Amount.Location = new System.Drawing.Point(343, 33);
			this.textVC2Amount.Name = "textVC2Amount";
			this.textVC2Amount.Size = new System.Drawing.Size(66, 20);
			this.textVC2Amount.TabIndex = 53;
			this.textVC2Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textVC11Code
			// 
			this.textVC11Code.Location = new System.Drawing.Point(313, 90);
			this.textVC11Code.MaxLength = 2;
			this.textVC11Code.Name = "textVC11Code";
			this.textVC11Code.Size = new System.Drawing.Size(26, 20);
			this.textVC11Code.TabIndex = 52;
			this.textVC11Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textVC8Code
			// 
			this.textVC8Code.Location = new System.Drawing.Point(313, 71);
			this.textVC8Code.MaxLength = 2;
			this.textVC8Code.Name = "textVC8Code";
			this.textVC8Code.Size = new System.Drawing.Size(26, 20);
			this.textVC8Code.TabIndex = 51;
			this.textVC8Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textVC5Code
			// 
			this.textVC5Code.Location = new System.Drawing.Point(313, 52);
			this.textVC5Code.MaxLength = 2;
			this.textVC5Code.Name = "textVC5Code";
			this.textVC5Code.Size = new System.Drawing.Size(26, 20);
			this.textVC5Code.TabIndex = 50;
			this.textVC5Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textVC2Code
			// 
			this.textVC2Code.Location = new System.Drawing.Point(313, 33);
			this.textVC2Code.MaxLength = 2;
			this.textVC2Code.Name = "textVC2Code";
			this.textVC2Code.Size = new System.Drawing.Size(26, 20);
			this.textVC2Code.TabIndex = 49;
			this.textVC2Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(355, 18);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(42, 13);
			this.label36.TabIndex = 48;
			this.label36.Text = "amount";
			// 
			// label37
			// 
			this.label37.AutoSize = true;
			this.label37.Location = new System.Drawing.Point(311, 18);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(31, 13);
			this.label37.TabIndex = 47;
			this.label37.Text = "code";
			// 
			// label38
			// 
			this.label38.AutoSize = true;
			this.label38.Location = new System.Drawing.Point(292, 94);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(13, 13);
			this.label38.TabIndex = 46;
			this.label38.Text = "d";
			// 
			// label39
			// 
			this.label39.AutoSize = true;
			this.label39.Location = new System.Drawing.Point(292, 75);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(13, 13);
			this.label39.TabIndex = 45;
			this.label39.Text = "c";
			// 
			// label40
			// 
			this.label40.AutoSize = true;
			this.label40.Location = new System.Drawing.Point(292, 56);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(13, 13);
			this.label40.TabIndex = 44;
			this.label40.Text = "b";
			// 
			// label41
			// 
			this.label41.AutoSize = true;
			this.label41.Location = new System.Drawing.Point(292, 37);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(13, 13);
			this.label41.TabIndex = 43;
			this.label41.Text = "a";
			// 
			// textVC10Amount
			// 
			this.textVC10Amount.Location = new System.Drawing.Point(203, 89);
			this.textVC10Amount.Name = "textVC10Amount";
			this.textVC10Amount.Size = new System.Drawing.Size(66, 20);
			this.textVC10Amount.TabIndex = 42;
			this.textVC10Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textVC7Amount
			// 
			this.textVC7Amount.Location = new System.Drawing.Point(203, 70);
			this.textVC7Amount.Name = "textVC7Amount";
			this.textVC7Amount.Size = new System.Drawing.Size(66, 20);
			this.textVC7Amount.TabIndex = 41;
			this.textVC7Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textVC4Amount
			// 
			this.textVC4Amount.Location = new System.Drawing.Point(203, 51);
			this.textVC4Amount.Name = "textVC4Amount";
			this.textVC4Amount.Size = new System.Drawing.Size(66, 20);
			this.textVC4Amount.TabIndex = 40;
			this.textVC4Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textVC1Amount
			// 
			this.textVC1Amount.Location = new System.Drawing.Point(203, 32);
			this.textVC1Amount.Name = "textVC1Amount";
			this.textVC1Amount.Size = new System.Drawing.Size(66, 20);
			this.textVC1Amount.TabIndex = 39;
			this.textVC1Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textVC10Code
			// 
			this.textVC10Code.Location = new System.Drawing.Point(173, 89);
			this.textVC10Code.MaxLength = 2;
			this.textVC10Code.Name = "textVC10Code";
			this.textVC10Code.Size = new System.Drawing.Size(26, 20);
			this.textVC10Code.TabIndex = 38;
			this.textVC10Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textVC7Code
			// 
			this.textVC7Code.Location = new System.Drawing.Point(173, 70);
			this.textVC7Code.MaxLength = 2;
			this.textVC7Code.Name = "textVC7Code";
			this.textVC7Code.Size = new System.Drawing.Size(26, 20);
			this.textVC7Code.TabIndex = 37;
			this.textVC7Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textVC4Code
			// 
			this.textVC4Code.Location = new System.Drawing.Point(173, 51);
			this.textVC4Code.MaxLength = 2;
			this.textVC4Code.Name = "textVC4Code";
			this.textVC4Code.Size = new System.Drawing.Size(26, 20);
			this.textVC4Code.TabIndex = 36;
			this.textVC4Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textVC1Code
			// 
			this.textVC1Code.Location = new System.Drawing.Point(173, 32);
			this.textVC1Code.MaxLength = 2;
			this.textVC1Code.Name = "textVC1Code";
			this.textVC1Code.Size = new System.Drawing.Size(26, 20);
			this.textVC1Code.TabIndex = 35;
			this.textVC1Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(215, 17);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(42, 13);
			this.label17.TabIndex = 34;
			this.label17.Text = "amount";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(171, 17);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(31, 13);
			this.label19.TabIndex = 33;
			this.label19.Text = "code";
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(152, 93);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(13, 13);
			this.label23.TabIndex = 32;
			this.label23.Text = "d";
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(152, 74);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(13, 13);
			this.label24.TabIndex = 31;
			this.label24.Text = "c";
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Location = new System.Drawing.Point(152, 55);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(13, 13);
			this.label28.TabIndex = 30;
			this.label28.Text = "b";
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(152, 36);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(13, 13);
			this.label29.TabIndex = 29;
			this.label29.Text = "a";
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(292, 16);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(19, 13);
			this.label27.TabIndex = 28;
			this.label27.Text = "41";
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(149, 16);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(19, 13);
			this.label26.TabIndex = 27;
			this.label26.Text = "40";
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(12, 16);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(19, 13);
			this.label25.TabIndex = 18;
			this.label25.Text = "39";
			// 
			// textVC9Amount
			// 
			this.textVC9Amount.Location = new System.Drawing.Point(66, 88);
			this.textVC9Amount.Name = "textVC9Amount";
			this.textVC9Amount.Size = new System.Drawing.Size(66, 20);
			this.textVC9Amount.TabIndex = 17;
			this.textVC9Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textVC6Amount
			// 
			this.textVC6Amount.Location = new System.Drawing.Point(66, 69);
			this.textVC6Amount.Name = "textVC6Amount";
			this.textVC6Amount.Size = new System.Drawing.Size(66, 20);
			this.textVC6Amount.TabIndex = 16;
			this.textVC6Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textVC3Amount
			// 
			this.textVC3Amount.Location = new System.Drawing.Point(66, 50);
			this.textVC3Amount.Name = "textVC3Amount";
			this.textVC3Amount.Size = new System.Drawing.Size(66, 20);
			this.textVC3Amount.TabIndex = 15;
			this.textVC3Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textVC0Amount
			// 
			this.textVC0Amount.Location = new System.Drawing.Point(66, 31);
			this.textVC0Amount.Name = "textVC0Amount";
			this.textVC0Amount.Size = new System.Drawing.Size(66, 20);
			this.textVC0Amount.TabIndex = 14;
			this.textVC0Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textVC9Code
			// 
			this.textVC9Code.Location = new System.Drawing.Point(36, 88);
			this.textVC9Code.MaxLength = 2;
			this.textVC9Code.Name = "textVC9Code";
			this.textVC9Code.Size = new System.Drawing.Size(26, 20);
			this.textVC9Code.TabIndex = 13;
			this.textVC9Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textVC6Code
			// 
			this.textVC6Code.Location = new System.Drawing.Point(36, 69);
			this.textVC6Code.MaxLength = 2;
			this.textVC6Code.Name = "textVC6Code";
			this.textVC6Code.Size = new System.Drawing.Size(26, 20);
			this.textVC6Code.TabIndex = 12;
			this.textVC6Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textVC3Code
			// 
			this.textVC3Code.Location = new System.Drawing.Point(36, 50);
			this.textVC3Code.MaxLength = 2;
			this.textVC3Code.Name = "textVC3Code";
			this.textVC3Code.Size = new System.Drawing.Size(26, 20);
			this.textVC3Code.TabIndex = 11;
			this.textVC3Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textVC0Code
			// 
			this.textVC0Code.Location = new System.Drawing.Point(36, 31);
			this.textVC0Code.MaxLength = 2;
			this.textVC0Code.Name = "textVC0Code";
			this.textVC0Code.Size = new System.Drawing.Size(26, 20);
			this.textVC0Code.TabIndex = 10;
			this.textVC0Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(78, 16);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(42, 13);
			this.label22.TabIndex = 7;
			this.label22.Text = "amount";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(34, 16);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(31, 13);
			this.label15.TabIndex = 4;
			this.label15.Text = "code";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(15, 92);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(13, 13);
			this.label14.TabIndex = 3;
			this.label14.Text = "d";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(15, 73);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(13, 13);
			this.label13.TabIndex = 2;
			this.label13.Text = "c";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(15, 54);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(13, 13);
			this.label12.TabIndex = 1;
			this.label12.Text = "b";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(15, 35);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(13, 13);
			this.label11.TabIndex = 0;
			this.label11.Text = "a";
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.Location = new System.Drawing.Point(358, 16);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(42, 13);
			this.label30.TabIndex = 48;
			this.label30.Text = "amount";
			// 
			// label31
			// 
			this.label31.AutoSize = true;
			this.label31.Location = new System.Drawing.Point(314, 16);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(31, 13);
			this.label31.TabIndex = 47;
			this.label31.Text = "code";
			// 
			// label32
			// 
			this.label32.AutoSize = true;
			this.label32.Location = new System.Drawing.Point(295, 92);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(13, 13);
			this.label32.TabIndex = 46;
			this.label32.Text = "d";
			// 
			// label33
			// 
			this.label33.AutoSize = true;
			this.label33.Location = new System.Drawing.Point(295, 73);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(13, 13);
			this.label33.TabIndex = 45;
			this.label33.Text = "c";
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Location = new System.Drawing.Point(295, 54);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(13, 13);
			this.label34.TabIndex = 44;
			this.label34.Text = "b";
			// 
			// label35
			// 
			this.label35.AutoSize = true;
			this.label35.Location = new System.Drawing.Point(295, 35);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(13, 13);
			this.label35.TabIndex = 43;
			this.label35.Text = "a";
			// 
			// label20
			// 
			this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label20.Location = new System.Drawing.Point(716, 890);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(215, 30);
			this.label20.TabIndex = 92;
			this.label20.Text = "(does not cancel payment edits)";
			this.label20.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// panelBottomEdge
			// 
			this.panelBottomEdge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelBottomEdge.Location = new System.Drawing.Point(300, 700);
			this.panelBottomEdge.Name = "panelBottomEdge";
			this.panelBottomEdge.Size = new System.Drawing.Size(682, 1);
			this.panelBottomEdge.TabIndex = 131;
			this.panelBottomEdge.Visible = false;
			// 
			// panelRightEdge
			// 
			this.panelRightEdge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelRightEdge.Location = new System.Drawing.Point(982, 200);
			this.panelRightEdge.Name = "panelRightEdge";
			this.panelRightEdge.Size = new System.Drawing.Size(1, 500);
			this.panelRightEdge.TabIndex = 132;
			this.panelRightEdge.Visible = false;
			// 
			// tabMain
			// 
			this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.tabMain.Controls.Add(this.tabGeneral);
			this.tabMain.Controls.Add(this.tabUB04);
			this.tabMain.Location = new System.Drawing.Point(2, 478);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(976, 421);
			this.tabMain.TabIndex = 133;
			// 
			// tabGeneral
			// 
			this.tabGeneral.AutoScroll = true;
			this.tabGeneral.BackColor = System.Drawing.Color.Transparent;
			this.tabGeneral.Controls.Add(this.label42);
			this.tabGeneral.Controls.Add(this.groupBox4);
			this.tabGeneral.Controls.Add(this.comboAccident);
			this.tabGeneral.Controls.Add(this.textRadiographs);
			this.tabGeneral.Controls.Add(this.label43);
			this.tabGeneral.Controls.Add(this.labelRadiographs);
			this.tabGeneral.Controls.Add(this.groupProsth);
			this.tabGeneral.Controls.Add(this.comboEmployRelated);
			this.tabGeneral.Controls.Add(this.groupOrtho);
			this.tabGeneral.Controls.Add(this.labelNote);
			this.tabGeneral.Controls.Add(this.textAccidentST);
			this.tabGeneral.Controls.Add(this.textNote);
			this.tabGeneral.Controls.Add(this.comboPlaceService);
			this.tabGeneral.Controls.Add(this.textAccidentDate);
			this.tabGeneral.Controls.Add(this.label48);
			this.tabGeneral.Controls.Add(this.label49);
			this.tabGeneral.Controls.Add(this.label44);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(968, 395);
			this.tabGeneral.TabIndex = 2;
			this.tabGeneral.Text = "General";
			this.tabGeneral.UseVisualStyleBackColor = true;
			// 
			// label42
			// 
			this.label42.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label42.Location = new System.Drawing.Point(17, 184);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(117, 17);
			this.label42.TabIndex = 143;
			this.label42.Text = "Accident Related";
			this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label45);
			this.groupBox4.Controls.Add(this.textRefProv);
			this.groupBox4.Controls.Add(this.butReferralEdit);
			this.groupBox4.Controls.Add(this.label47);
			this.groupBox4.Controls.Add(this.butReferralNone);
			this.groupBox4.Controls.Add(this.butReferralSelect);
			this.groupBox4.Controls.Add(this.textRefNum);
			this.groupBox4.Controls.Add(this.label46);
			this.groupBox4.Location = new System.Drawing.Point(308, 119);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(313, 148);
			this.groupBox4.TabIndex = 118;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Claim Referral";
			// 
			// label45
			// 
			this.label45.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label45.Location = new System.Drawing.Point(13, 16);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(282, 36);
			this.label45.TabIndex = 133;
			this.label45.Text = "Only enter referring provider and referral number if required by your insurance c" +
				"arrier.";
			// 
			// textRefProv
			// 
			this.textRefProv.BackColor = System.Drawing.SystemColors.Window;
			this.textRefProv.Location = new System.Drawing.Point(122, 55);
			this.textRefProv.Name = "textRefProv";
			this.textRefProv.ReadOnly = true;
			this.textRefProv.Size = new System.Drawing.Size(175, 20);
			this.textRefProv.TabIndex = 139;
			// 
			// butReferralEdit
			// 
			this.butReferralEdit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butReferralEdit.Autosize = true;
			this.butReferralEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butReferralEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butReferralEdit.CornerRadius = 4F;
			this.butReferralEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.butReferralEdit.Location = new System.Drawing.Point(240, 81);
			this.butReferralEdit.Name = "butReferralEdit";
			this.butReferralEdit.Size = new System.Drawing.Size(57, 25);
			this.butReferralEdit.TabIndex = 144;
			this.butReferralEdit.Text = "Edit";
			this.butReferralEdit.Click += new System.EventHandler(this.butReferralEdit_Click);
			// 
			// label47
			// 
			this.label47.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label47.Location = new System.Drawing.Point(17, 57);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(99, 14);
			this.label47.TabIndex = 131;
			this.label47.Text = "Referring Provider";
			this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butReferralNone
			// 
			this.butReferralNone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butReferralNone.Autosize = true;
			this.butReferralNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butReferralNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butReferralNone.CornerRadius = 4F;
			this.butReferralNone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.butReferralNone.Location = new System.Drawing.Point(122, 81);
			this.butReferralNone.Name = "butReferralNone";
			this.butReferralNone.Size = new System.Drawing.Size(57, 25);
			this.butReferralNone.TabIndex = 135;
			this.butReferralNone.Text = "&None";
			this.butReferralNone.Click += new System.EventHandler(this.butReferralNone_Click);
			// 
			// butReferralSelect
			// 
			this.butReferralSelect.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butReferralSelect.Autosize = true;
			this.butReferralSelect.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butReferralSelect.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butReferralSelect.CornerRadius = 4F;
			this.butReferralSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.butReferralSelect.Location = new System.Drawing.Point(181, 81);
			this.butReferralSelect.Name = "butReferralSelect";
			this.butReferralSelect.Size = new System.Drawing.Size(57, 25);
			this.butReferralSelect.TabIndex = 138;
			this.butReferralSelect.Text = "Select";
			this.butReferralSelect.Click += new System.EventHandler(this.butReferralSelect_Click);
			// 
			// textRefNum
			// 
			this.textRefNum.Location = new System.Drawing.Point(122, 112);
			this.textRefNum.Name = "textRefNum";
			this.textRefNum.Size = new System.Drawing.Size(175, 20);
			this.textRefNum.TabIndex = 127;
			// 
			// label46
			// 
			this.label46.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label46.Location = new System.Drawing.Point(26, 114);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(90, 18);
			this.label46.TabIndex = 132;
			this.label46.Text = "Referral Number";
			this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboAccident
			// 
			this.comboAccident.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAccident.FormattingEnabled = true;
			this.comboAccident.Location = new System.Drawing.Point(139, 183);
			this.comboAccident.Name = "comboAccident";
			this.comboAccident.Size = new System.Drawing.Size(101, 21);
			this.comboAccident.TabIndex = 142;
			// 
			// textRadiographs
			// 
			this.textRadiographs.ForeColor = System.Drawing.SystemColors.WindowText;
			this.textRadiographs.Location = new System.Drawing.Point(739, 126);
			this.textRadiographs.MaxVal = 255;
			this.textRadiographs.MinVal = 0;
			this.textRadiographs.Name = "textRadiographs";
			this.textRadiographs.Size = new System.Drawing.Size(39, 20);
			this.textRadiographs.TabIndex = 116;
			this.textRadiographs.Text = "0";
			// 
			// label43
			// 
			this.label43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label43.Location = new System.Drawing.Point(17, 231);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(117, 17);
			this.label43.TabIndex = 134;
			this.label43.Text = "Accident State";
			this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboEmployRelated
			// 
			this.comboEmployRelated.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboEmployRelated.FormattingEnabled = true;
			this.comboEmployRelated.Location = new System.Drawing.Point(139, 159);
			this.comboEmployRelated.Name = "comboEmployRelated";
			this.comboEmployRelated.Size = new System.Drawing.Size(150, 21);
			this.comboEmployRelated.TabIndex = 141;
			// 
			// textAccidentST
			// 
			this.textAccidentST.Location = new System.Drawing.Point(139, 230);
			this.textAccidentST.Name = "textAccidentST";
			this.textAccidentST.Size = new System.Drawing.Size(30, 20);
			this.textAccidentST.TabIndex = 129;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(629, 22);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Claim;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(319, 68);
			this.textNote.TabIndex = 118;
			// 
			// comboPlaceService
			// 
			this.comboPlaceService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPlaceService.FormattingEnabled = true;
			this.comboPlaceService.Location = new System.Drawing.Point(139, 132);
			this.comboPlaceService.Name = "comboPlaceService";
			this.comboPlaceService.Size = new System.Drawing.Size(150, 21);
			this.comboPlaceService.TabIndex = 140;
			// 
			// textAccidentDate
			// 
			this.textAccidentDate.Location = new System.Drawing.Point(139, 207);
			this.textAccidentDate.Name = "textAccidentDate";
			this.textAccidentDate.Size = new System.Drawing.Size(75, 20);
			this.textAccidentDate.TabIndex = 128;
			// 
			// label48
			// 
			this.label48.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label48.Location = new System.Drawing.Point(17, 133);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(117, 17);
			this.label48.TabIndex = 136;
			this.label48.Text = "Place of Service";
			this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label49
			// 
			this.label49.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label49.Location = new System.Drawing.Point(17, 160);
			this.label49.Name = "label49";
			this.label49.Size = new System.Drawing.Size(117, 17);
			this.label49.TabIndex = 137;
			this.label49.Text = "Employment Related";
			this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label44
			// 
			this.label44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label44.Location = new System.Drawing.Point(17, 208);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(117, 17);
			this.label44.TabIndex = 130;
			this.label44.Text = "Accident Date";
			this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabUB04
			// 
			this.tabUB04.AutoScroll = true;
			this.tabUB04.BackColor = System.Drawing.Color.Transparent;
			this.tabUB04.Controls.Add(this.groupBox1);
			this.tabUB04.Controls.Add(this.groupValueCodes);
			this.tabUB04.Location = new System.Drawing.Point(4, 22);
			this.tabUB04.Name = "tabUB04";
			this.tabUB04.Padding = new System.Windows.Forms.Padding(3);
			this.tabUB04.Size = new System.Drawing.Size(968, 395);
			this.tabUB04.TabIndex = 0;
			this.tabUB04.Text = "Medical-UB04";
			this.tabUB04.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label60);
			this.groupBox1.Controls.Add(this.label59);
			this.groupBox1.Controls.Add(this.label58);
			this.groupBox1.Controls.Add(this.label57);
			this.groupBox1.Controls.Add(this.label56);
			this.groupBox1.Controls.Add(this.label55);
			this.groupBox1.Controls.Add(this.label54);
			this.groupBox1.Controls.Add(this.label53);
			this.groupBox1.Controls.Add(this.label52);
			this.groupBox1.Controls.Add(this.label51);
			this.groupBox1.Controls.Add(this.label50);
			this.groupBox1.Controls.Add(this.textCode10);
			this.groupBox1.Controls.Add(this.textCode9);
			this.groupBox1.Controls.Add(this.textCode8);
			this.groupBox1.Controls.Add(this.textCode7);
			this.groupBox1.Controls.Add(this.textCode6);
			this.groupBox1.Controls.Add(this.textCode5);
			this.groupBox1.Controls.Add(this.textCode4);
			this.groupBox1.Controls.Add(this.textCode3);
			this.groupBox1.Controls.Add(this.textCode2);
			this.groupBox1.Controls.Add(this.textCode1);
			this.groupBox1.Controls.Add(this.textCode0);
			this.groupBox1.Location = new System.Drawing.Point(22, 21);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(433, 67);
			this.groupBox1.TabIndex = 131;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Condition Codes";
			// 
			// label60
			// 
			this.label60.AutoSize = true;
			this.label60.Location = new System.Drawing.Point(398, 19);
			this.label60.Name = "label60";
			this.label60.Size = new System.Drawing.Size(19, 13);
			this.label60.TabIndex = 78;
			this.label60.Text = "28";
			// 
			// label59
			// 
			this.label59.AutoSize = true;
			this.label59.Location = new System.Drawing.Point(360, 19);
			this.label59.Name = "label59";
			this.label59.Size = new System.Drawing.Size(19, 13);
			this.label59.TabIndex = 77;
			this.label59.Text = "27";
			// 
			// label58
			// 
			this.label58.AutoSize = true;
			this.label58.Location = new System.Drawing.Point(322, 19);
			this.label58.Name = "label58";
			this.label58.Size = new System.Drawing.Size(19, 13);
			this.label58.TabIndex = 76;
			this.label58.Text = "26";
			// 
			// label57
			// 
			this.label57.AutoSize = true;
			this.label57.Location = new System.Drawing.Point(284, 19);
			this.label57.Name = "label57";
			this.label57.Size = new System.Drawing.Size(19, 13);
			this.label57.TabIndex = 75;
			this.label57.Text = "25";
			// 
			// label56
			// 
			this.label56.AutoSize = true;
			this.label56.Location = new System.Drawing.Point(246, 19);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(19, 13);
			this.label56.TabIndex = 74;
			this.label56.Text = "24";
			// 
			// label55
			// 
			this.label55.AutoSize = true;
			this.label55.Location = new System.Drawing.Point(209, 19);
			this.label55.Name = "label55";
			this.label55.Size = new System.Drawing.Size(19, 13);
			this.label55.TabIndex = 73;
			this.label55.Text = "23";
			// 
			// label54
			// 
			this.label54.AutoSize = true;
			this.label54.Location = new System.Drawing.Point(170, 19);
			this.label54.Name = "label54";
			this.label54.Size = new System.Drawing.Size(19, 13);
			this.label54.TabIndex = 72;
			this.label54.Text = "22";
			// 
			// label53
			// 
			this.label53.AutoSize = true;
			this.label53.Location = new System.Drawing.Point(132, 19);
			this.label53.Name = "label53";
			this.label53.Size = new System.Drawing.Size(19, 13);
			this.label53.TabIndex = 71;
			this.label53.Text = "21";
			// 
			// label52
			// 
			this.label52.AutoSize = true;
			this.label52.Location = new System.Drawing.Point(94, 19);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(19, 13);
			this.label52.TabIndex = 70;
			this.label52.Text = "20";
			// 
			// label51
			// 
			this.label51.AutoSize = true;
			this.label51.Location = new System.Drawing.Point(56, 19);
			this.label51.Name = "label51";
			this.label51.Size = new System.Drawing.Size(19, 13);
			this.label51.TabIndex = 69;
			this.label51.Text = "19";
			// 
			// label50
			// 
			this.label50.AutoSize = true;
			this.label50.Location = new System.Drawing.Point(18, 19);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(19, 13);
			this.label50.TabIndex = 68;
			this.label50.Text = "18";
			// 
			// textCode10
			// 
			this.textCode10.Location = new System.Drawing.Point(394, 37);
			this.textCode10.MaxLength = 2;
			this.textCode10.Name = "textCode10";
			this.textCode10.Size = new System.Drawing.Size(26, 20);
			this.textCode10.TabIndex = 67;
			this.textCode10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCode9
			// 
			this.textCode9.Location = new System.Drawing.Point(356, 37);
			this.textCode9.MaxLength = 2;
			this.textCode9.Name = "textCode9";
			this.textCode9.Size = new System.Drawing.Size(26, 20);
			this.textCode9.TabIndex = 66;
			this.textCode9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCode8
			// 
			this.textCode8.Location = new System.Drawing.Point(318, 37);
			this.textCode8.MaxLength = 2;
			this.textCode8.Name = "textCode8";
			this.textCode8.Size = new System.Drawing.Size(26, 20);
			this.textCode8.TabIndex = 65;
			this.textCode8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCode7
			// 
			this.textCode7.Location = new System.Drawing.Point(280, 37);
			this.textCode7.MaxLength = 2;
			this.textCode7.Name = "textCode7";
			this.textCode7.Size = new System.Drawing.Size(26, 20);
			this.textCode7.TabIndex = 64;
			this.textCode7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCode6
			// 
			this.textCode6.Location = new System.Drawing.Point(242, 37);
			this.textCode6.MaxLength = 2;
			this.textCode6.Name = "textCode6";
			this.textCode6.Size = new System.Drawing.Size(26, 20);
			this.textCode6.TabIndex = 63;
			this.textCode6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCode5
			// 
			this.textCode5.Location = new System.Drawing.Point(205, 37);
			this.textCode5.MaxLength = 2;
			this.textCode5.Name = "textCode5";
			this.textCode5.Size = new System.Drawing.Size(26, 20);
			this.textCode5.TabIndex = 62;
			this.textCode5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCode4
			// 
			this.textCode4.Location = new System.Drawing.Point(166, 37);
			this.textCode4.MaxLength = 2;
			this.textCode4.Name = "textCode4";
			this.textCode4.Size = new System.Drawing.Size(26, 20);
			this.textCode4.TabIndex = 61;
			this.textCode4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCode3
			// 
			this.textCode3.Location = new System.Drawing.Point(128, 37);
			this.textCode3.MaxLength = 2;
			this.textCode3.Name = "textCode3";
			this.textCode3.Size = new System.Drawing.Size(26, 20);
			this.textCode3.TabIndex = 60;
			this.textCode3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCode2
			// 
			this.textCode2.Location = new System.Drawing.Point(90, 37);
			this.textCode2.MaxLength = 2;
			this.textCode2.Name = "textCode2";
			this.textCode2.Size = new System.Drawing.Size(26, 20);
			this.textCode2.TabIndex = 59;
			this.textCode2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCode1
			// 
			this.textCode1.Location = new System.Drawing.Point(52, 37);
			this.textCode1.MaxLength = 2;
			this.textCode1.Name = "textCode1";
			this.textCode1.Size = new System.Drawing.Size(26, 20);
			this.textCode1.TabIndex = 58;
			this.textCode1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textCode0
			// 
			this.textCode0.Location = new System.Drawing.Point(14, 37);
			this.textCode0.MaxLength = 2;
			this.textCode0.Name = "textCode0";
			this.textCode0.Size = new System.Drawing.Size(26, 20);
			this.textCode0.TabIndex = 57;
			this.textCode0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(557, 421);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(154, 54);
			this.label7.TabIndex = 133;
			this.label7.Text = "Don\'t create a new check until payments for all claims have been entered.";
			// 
			// groupEnterPayment
			// 
			this.groupEnterPayment.BackColor = System.Drawing.SystemColors.Control;
			this.groupEnterPayment.Controls.Add(this.butPaySupp);
			this.groupEnterPayment.Controls.Add(this.butPayTotal);
			this.groupEnterPayment.Controls.Add(this.butPayProc);
			this.groupEnterPayment.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupEnterPayment.Location = new System.Drawing.Point(801, 12);
			this.groupEnterPayment.Name = "groupEnterPayment";
			this.groupEnterPayment.Size = new System.Drawing.Size(133, 107);
			this.groupEnterPayment.TabIndex = 132;
			this.groupEnterPayment.TabStop = false;
			this.groupEnterPayment.Text = "Enter Payment";
			// 
			// butPaySupp
			// 
			this.butPaySupp.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPaySupp.Autosize = true;
			this.butPaySupp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPaySupp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPaySupp.CornerRadius = 4F;
			this.butPaySupp.Location = new System.Drawing.Point(17, 78);
			this.butPaySupp.Name = "butPaySupp";
			this.butPaySupp.Size = new System.Drawing.Size(99, 23);
			this.butPaySupp.TabIndex = 102;
			this.butPaySupp.Text = "S&upplemental";
			this.butPaySupp.Click += new System.EventHandler(this.butPaySupp_Click);
			// 
			// butPayTotal
			// 
			this.butPayTotal.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPayTotal.Autosize = true;
			this.butPayTotal.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPayTotal.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPayTotal.CornerRadius = 4F;
			this.butPayTotal.Location = new System.Drawing.Point(17, 16);
			this.butPayTotal.Name = "butPayTotal";
			this.butPayTotal.Size = new System.Drawing.Size(99, 23);
			this.butPayTotal.TabIndex = 100;
			this.butPayTotal.Text = "&Total";
			this.butPayTotal.Click += new System.EventHandler(this.butPayTotal_Click);
			// 
			// butPayProc
			// 
			this.butPayProc.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPayProc.Autosize = true;
			this.butPayProc.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPayProc.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPayProc.CornerRadius = 4F;
			this.butPayProc.Location = new System.Drawing.Point(17, 41);
			this.butPayProc.Name = "butPayProc";
			this.butPayProc.Size = new System.Drawing.Size(99, 23);
			this.butPayProc.TabIndex = 101;
			this.butPayProc.Text = "&By Procedure";
			this.butPayProc.Click += new System.EventHandler(this.butPayProc_Click);
			// 
			// textReasonUnder
			// 
			this.textReasonUnder.Location = new System.Drawing.Point(763, 418);
			this.textReasonUnder.MaxLength = 255;
			this.textReasonUnder.Multiline = true;
			this.textReasonUnder.Name = "textReasonUnder";
			this.textReasonUnder.Size = new System.Drawing.Size(215, 57);
			this.textReasonUnder.TabIndex = 130;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(762, 389);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(213, 26);
			this.label4.TabIndex = 131;
			this.label4.Text = "Reasons underpaid:  (shows on patient bill)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// gridPay
			// 
			this.gridPay.HScrollVisible = false;
			this.gridPay.Location = new System.Drawing.Point(2, 389);
			this.gridPay.Name = "gridPay";
			this.gridPay.ScrollValue = 0;
			this.gridPay.Size = new System.Drawing.Size(549, 86);
			this.gridPay.TabIndex = 135;
			this.gridPay.Title = "Insurance Checks";
			this.gridPay.TranslationName = "TableClaimPay";
			this.gridPay.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPay_CellClick);
			this.gridPay.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPay_CellDoubleClick);
			// 
			// butCheckAdd
			// 
			this.butCheckAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCheckAdd.Autosize = true;
			this.butCheckAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCheckAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCheckAdd.CornerRadius = 4F;
			this.butCheckAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butCheckAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCheckAdd.Location = new System.Drawing.Point(557, 389);
			this.butCheckAdd.Name = "butCheckAdd";
			this.butCheckAdd.Size = new System.Drawing.Size(114, 26);
			this.butCheckAdd.TabIndex = 134;
			this.butCheckAdd.Text = "Create C&heck";
			this.butCheckAdd.Click += new System.EventHandler(this.butCheckAdd_Click);
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.CornerRadius = 4F;
			this.butSend.Image = ((System.Drawing.Image)(resources.GetObject("butSend.Image")));
			this.butSend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSend.Location = new System.Drawing.Point(601, 923);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(86, 26);
			this.butSend.TabIndex = 130;
			this.butSend.Text = "Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// gridProc
			// 
			this.gridProc.HScrollVisible = false;
			this.gridProc.Location = new System.Drawing.Point(2, 159);
			this.gridProc.Name = "gridProc";
			this.gridProc.ScrollValue = 0;
			this.gridProc.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridProc.Size = new System.Drawing.Size(977, 200);
			this.gridProc.TabIndex = 128;
			this.gridProc.Title = "Procedures";
			this.gridProc.TranslationName = "TableClaimProc";
			this.gridProc.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridProc_CellClick);
			this.gridProc.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridProc_CellDoubleClick);
			// 
			// butSplit
			// 
			this.butSplit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSplit.Autosize = true;
			this.butSplit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSplit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSplit.CornerRadius = 4F;
			this.butSplit.Location = new System.Drawing.Point(818, 134);
			this.butSplit.Name = "butSplit";
			this.butSplit.Size = new System.Drawing.Size(99, 23);
			this.butSplit.TabIndex = 127;
			this.butSplit.Text = "Split Claim";
			this.butSplit.Click += new System.EventHandler(this.butSplit_Click);
			// 
			// butLabel
			// 
			this.butLabel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butLabel.Autosize = true;
			this.butLabel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabel.CornerRadius = 4F;
			this.butLabel.Image = global::OpenDental.Properties.Resources.butLabel;
			this.butLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabel.Location = new System.Drawing.Point(326, 923);
			this.butLabel.Name = "butLabel";
			this.butLabel.Size = new System.Drawing.Size(81, 26);
			this.butLabel.TabIndex = 126;
			this.butLabel.Text = "Label";
			this.butLabel.Click += new System.EventHandler(this.butLabel_Click);
			// 
			// textDateService
			// 
			this.textDateService.Location = new System.Drawing.Point(111, 91);
			this.textDateService.Name = "textDateService";
			this.textDateService.Size = new System.Drawing.Size(82, 20);
			this.textDateService.TabIndex = 119;
			// 
			// textWriteOff
			// 
			this.textWriteOff.Location = new System.Drawing.Point(624, 363);
			this.textWriteOff.Name = "textWriteOff";
			this.textWriteOff.ReadOnly = true;
			this.textWriteOff.Size = new System.Drawing.Size(65, 20);
			this.textWriteOff.TabIndex = 113;
			this.textWriteOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textInsPayAmt
			// 
			this.textInsPayAmt.Location = new System.Drawing.Point(559, 363);
			this.textInsPayAmt.Name = "textInsPayAmt";
			this.textInsPayAmt.ReadOnly = true;
			this.textInsPayAmt.Size = new System.Drawing.Size(65, 20);
			this.textInsPayAmt.TabIndex = 6;
			this.textInsPayAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDedApplied
			// 
			this.textDedApplied.Location = new System.Drawing.Point(429, 363);
			this.textDedApplied.Name = "textDedApplied";
			this.textDedApplied.ReadOnly = true;
			this.textDedApplied.Size = new System.Drawing.Size(65, 20);
			this.textDedApplied.TabIndex = 4;
			this.textDedApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDateSent
			// 
			this.textDateSent.Location = new System.Drawing.Point(111, 112);
			this.textDateSent.Name = "textDateSent";
			this.textDateSent.Size = new System.Drawing.Size(82, 20);
			this.textDateSent.TabIndex = 6;
			// 
			// textDateRec
			// 
			this.textDateRec.Location = new System.Drawing.Point(111, 133);
			this.textDateRec.Name = "textDateRec";
			this.textDateRec.Size = new System.Drawing.Size(82, 20);
			this.textDateRec.TabIndex = 7;
			// 
			// butPreview
			// 
			this.butPreview.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPreview.Autosize = true;
			this.butPreview.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPreview.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPreview.CornerRadius = 4F;
			this.butPreview.Image = global::OpenDental.Properties.Resources.butPreview;
			this.butPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPreview.Location = new System.Drawing.Point(413, 923);
			this.butPreview.Name = "butPreview";
			this.butPreview.Size = new System.Drawing.Size(92, 26);
			this.butPreview.TabIndex = 115;
			this.butPreview.Text = "P&review";
			this.butPreview.Click += new System.EventHandler(this.butPreview_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(510, 923);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(86, 26);
			this.butPrint.TabIndex = 114;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.ButPrint_Click);
			// 
			// butRecalc
			// 
			this.butRecalc.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRecalc.Autosize = true;
			this.butRecalc.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRecalc.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRecalc.CornerRadius = 4F;
			this.butRecalc.Location = new System.Drawing.Point(762, 361);
			this.butRecalc.Name = "butRecalc";
			this.butRecalc.Size = new System.Drawing.Size(148, 25);
			this.butRecalc.TabIndex = 112;
			this.butRecalc.Text = "Recalculate &Estimates";
			this.butRecalc.Click += new System.EventHandler(this.butRecalc_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(5, 923);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(91, 26);
			this.butDelete.TabIndex = 106;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butSupp
			// 
			this.butSupp.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSupp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSupp.Autosize = true;
			this.butSupp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSupp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSupp.CornerRadius = 4F;
			this.butSupp.Location = new System.Drawing.Point(138, 923);
			this.butSupp.Name = "butSupp";
			this.butSupp.Size = new System.Drawing.Size(113, 26);
			this.butSupp.TabIndex = 95;
			this.butSupp.Text = "Supplemental Info";
			this.butSupp.Visible = false;
			this.butSupp.Click += new System.EventHandler(this.butSupp_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(865, 923);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 15;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(778, 923);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 14;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormClaimEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(984, 956);
			this.Controls.Add(this.textReasonUnder);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.gridPay);
			this.Controls.Add(this.butCheckAdd);
			this.Controls.Add(this.tabMain);
			this.Controls.Add(this.panelRightEdge);
			this.Controls.Add(this.panelBottomEdge);
			this.Controls.Add(this.groupEnterPayment);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.gridProc);
			this.Controls.Add(this.butSplit);
			this.Controls.Add(this.butLabel);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.textDateService);
			this.Controls.Add(this.textWriteOff);
			this.Controls.Add(this.textInsPayEst);
			this.Controls.Add(this.textInsPayAmt);
			this.Controls.Add(this.textClaimFee);
			this.Controls.Add(this.textDedApplied);
			this.Controls.Add(this.textPreAuth);
			this.Controls.Add(this.textDateSent);
			this.Controls.Add(this.textDateRec);
			this.Controls.Add(this.butPreview);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butRecalc);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butSupp);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.listClaimType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listClaimStatus);
			this.Controls.Add(this.comboProvTreat);
			this.Controls.Add(this.comboProvBill);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.labelPreAuthNum);
			this.Controls.Add(this.labelDateService);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label8);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Claim";
			this.Shown += new System.EventHandler(this.FormClaimEdit_Shown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormClaimEdit_Closing);
			this.Load += new System.EventHandler(this.FormClaimEdit_Load);
			this.groupProsth.ResumeLayout(false);
			this.groupProsth.PerformLayout();
			this.groupOrtho.ResumeLayout(false);
			this.groupOrtho.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupValueCodes.ResumeLayout(false);
			this.groupValueCodes.PerformLayout();
			this.tabMain.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.tabGeneral.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.tabUB04.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupEnterPayment.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClaimEdit_Shown(object sender,EventArgs e) {
			if(!IsNew) {
				return;
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)!="CA") {//en-CA or fr-CA
				return;
			}
			//The rest is just Canadian.
			ToothInitial[] ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			ArrayList missingAL=ToothInitials.GetMissingOrHiddenTeeth(ToothInitialList);
			List<CanadianExtract> missingList=new List<CanadianExtract>();
			CanadianExtract canext;
			for(int i=0;i<missingAL.Count;i++) {
				canext=new CanadianExtract();
				canext.ClaimNum=ClaimCur.ClaimNum;//redundant
				canext.ToothNum=(string)missingAL[i];
				missingList.Add(canext);
			}
			CanadianClaim canclaim=CanadianClaims.Insert(ClaimCur.ClaimNum,missingList);
			FormClaimCanadian FormC=new FormClaimCanadian();
			FormC.ClaimCur=ClaimCur;
			FormC.CanCur=canclaim;
			FormC.ShowDialog();
		}
		
		private void FormClaimEdit_Load(object sender, System.EventArgs e) {
			if(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height<this.Height){
				this.Height=System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				butSupp.Visible=true;
				groupProsth.Visible=false;
				labelMissingTeeth.Visible=false;
				labelRadiographs.Visible=false;
				textRadiographs.Visible=false;
				textOrthoDate.Visible=false;
				labelOrthoDate.Visible=false;
				labelOrthoRemainM.Visible=false;
				textOrthoRemainM.Visible=false;
				labelNote.Text="Claim Note (will only show on printed claims)";
				//a ClaimCanadian object will not be loaded or created for existing claims unless user clicks supplemental info button.
			}
			if(IsNew){
				//butPayWizard.Enabled=false;
			}
			else if(ClaimCur.ClaimStatus=="S" || ClaimCur.ClaimStatus=="R"){//sent or received
				if(!Security.IsAuthorized(Permissions.ClaimsSentEdit)){
					butOK.Enabled=false;
					butDelete.Enabled=false;
					//butPrint.Enabled=false;
					notAuthorized=true;
					groupEnterPayment.Enabled=false;
					gridPay.Enabled=false;
					gridProc.Enabled=false;
					listClaimStatus.Enabled=false;
					butCheckAdd.Enabled=false;
				}
			}
			if(ClaimCur.ClaimType=="PreAuth"){
				labelPreAuthNum.Visible=false;
				textPreAuth.Visible=false;
				textDateService.Visible=false;
				labelDateService.Visible=false;
				label20.Visible=false;//warning when delete
				textReasonUnder.Visible=false;
				label4.Visible=false;//reason under
				butPayTotal.Visible=false;	
				butSplit.Visible=false;
      }
			if(PrefB.GetBool("EasyNoClinics")){
				labelClinic.Visible=false;
				comboClinic.Visible=false;
			}
			listClaimType.Items.Add(Lan.g(this,"Primary"));
			listClaimType.Items.Add(Lan.g(this,"Secondary"));
			listClaimType.Items.Add(Lan.g(this,"PreAuth"));
			listClaimType.Items.Add(Lan.g(this,"Other"));
			listClaimType.Items.Add(Lan.g(this,"Capitation"));
			listClaimStatus.Items.Add(Lan.g(this,"Unsent"));
			listClaimStatus.Items.Add(Lan.g(this,"Hold until Pri received"));
			listClaimStatus.Items.Add(Lan.g(this,"Waiting to Send"));//2
			listClaimStatus.Items.Add(Lan.g(this,"Probably Sent"));
			listClaimStatus.Items.Add(Lan.g(this,"Sent - Verified"));
			listClaimStatus.Items.Add(Lan.g(this,"Received"));
			string[] enumRelat=Enum.GetNames(typeof(Relat));
			for(int i=0;i<enumRelat.Length;i++){;
				comboPatRelat.Items.Add(Lan.g("enumRelat",enumRelat[i]));
				comboPatRelat2.Items.Add(Lan.g("enumRelat",enumRelat[i]));
			}
      Claims.Refresh(PatCur.PatNum); 
      ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			ProcList=Procedures.Refresh(PatCur.PatNum);
			PlanList=InsPlans.Refresh(FamCur);
			PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			//this section used to be "supplemental"---------------------------------------------------------------------------------
			textRefNum.Text=ClaimCur.RefNumString;
			string[] enumPlaceOfService=Enum.GetNames(typeof(PlaceOfService));
			for(int i=0;i<enumPlaceOfService.Length;i++) {
				comboPlaceService.Items.Add(Lan.g("enumPlaceOfService",enumPlaceOfService[i]));
			}
			comboPlaceService.SelectedIndex=(int)ClaimCur.PlaceService;
			string[] enumYN=Enum.GetNames(typeof(YN));
			for(int i=0;i<enumYN.Length;i++) {
				comboEmployRelated.Items.Add(Lan.g("enumYN",enumYN[i]));
			}
			comboEmployRelated.SelectedIndex=(int)ClaimCur.EmployRelated;
			comboAccident.Items.Add(Lan.g(this,"No"));
			comboAccident.Items.Add(Lan.g(this,"Auto"));
			comboAccident.Items.Add(Lan.g(this,"Employment"));
			comboAccident.Items.Add(Lan.g(this,"Other"));
			switch(ClaimCur.AccidentRelated) {
				case "":
					comboAccident.SelectedIndex=0;
					break;
				case "A":
					comboAccident.SelectedIndex=1;
					break;
				case "E":
					comboAccident.SelectedIndex=2;
					break;
				case "O":
					comboAccident.SelectedIndex=3;
					break;
			}
			if(ClaimCur.AccidentDate.Year<1880) {
				textAccidentDate.Text="";
			}
			else {
				textAccidentDate.Text=ClaimCur.AccidentDate.ToShortDateString();
			}
			textAccidentST.Text=ClaimCur.AccidentST;
			textRefProv.Text=Referrals.GetNameLF(ClaimCur.ReferringProv);
			if(ClaimCur.ReferringProv==0){
				butReferralEdit.Enabled=false;
			}
			else{
				butReferralEdit.Enabled=true;
			}
			FillForm();			
		}

		///<summary></summary>
		public void FillForm(){
			this.Text=Lan.g(this,"Edit Claim")+" - "+PatCur.GetNameLF();
			if(ClaimValCodes!=null){
				for(int i=0;i<ClaimValCodes.Count;i++){
					ClaimValCode vc = (ClaimValCode)ClaimValCodes[i];
					TextBox code = (TextBox)Controls.Find("textVC" + i + "Code", true)[0];
					code.Text=vc.ValCode.ToString();
					TextBox amount = (TextBox)Controls.Find("textVC" + i + "Amount", true)[0];
					amount.Text=vc.ValAmount.ToString();
				}
			}
			if(CurCondCodes!=null && CurCondCodes.ClaimNum!=0){
				textCode0.Text=CurCondCodes.Code0.ToString();
				textCode1.Text=CurCondCodes.Code1.ToString();
				textCode2.Text=CurCondCodes.Code2.ToString();
				textCode3.Text=CurCondCodes.Code3.ToString();
				textCode4.Text=CurCondCodes.Code4.ToString();
				textCode5.Text=CurCondCodes.Code5.ToString();
				textCode6.Text=CurCondCodes.Code6.ToString();
				textCode7.Text=CurCondCodes.Code7.ToString();
				textCode8.Text=CurCondCodes.Code8.ToString();
				textCode9.Text=CurCondCodes.Code9.ToString();
				textCode10.Text=CurCondCodes.Code10.ToString();
			}
			if(ClaimCur.DateService.Year<1880)
				textDateService.Text="";
			else
				textDateService.Text=ClaimCur.DateService.ToShortDateString();
			if(ClaimCur.DateSent.Year<1880)
				textDateSent.Text="";
			else
				textDateSent.Text=ClaimCur.DateSent.ToShortDateString();
			if(ClaimCur.DateReceived.Year<1880)
				textDateRec.Text="";
			else
				textDateRec.Text=ClaimCur.DateReceived.ToShortDateString();
			switch(ClaimCur.ClaimStatus){
				case "U"://unsent
					listClaimStatus.SelectedIndex=0;
					break;
				case "H"://hold until pri received
					listClaimStatus.SelectedIndex=1;
					break;
				case "W"://waiting to be sent
					listClaimStatus.SelectedIndex=2;
					break;
				case "P"://probably sent
					listClaimStatus.SelectedIndex=3;
					break;
				case "S"://sent-verified
					listClaimStatus.SelectedIndex=4;
					break;
				case "R"://received
					listClaimStatus.SelectedIndex=5;
					break;
			}
			comboClinic.Items.Clear();
			comboClinic.Items.Add(Lan.g(this,"none"));
			comboClinic.SelectedIndex=0;
			for(int i=0;i<Clinics.List.Length;i++){
				comboClinic.Items.Add(Clinics.List[i].Description);
				if(Clinics.List[i].ClinicNum==ClaimCur.ClinicNum)
					comboClinic.SelectedIndex=i+1;
			}
			switch(ClaimCur.ClaimType){
				case "P":
					listClaimType.SelectedIndex=0;
					break;
				case "S":
					listClaimType.SelectedIndex=1;
					break;
				case "PreAuth":
					listClaimType.SelectedIndex=2;
					break;
				case "Other":
					listClaimType.SelectedIndex=3;
					break;
				case "Cap":
					listClaimType.SelectedIndex=4;
					break;
			}
			comboProvBill.Items.Clear();
			for(int i=0;i<Providers.List.Length;i++){
				comboProvBill.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==ClaimCur.ProvBill)
					comboProvBill.SelectedIndex=i;
			}
			if(comboProvBill.Items.Count>0 && comboProvBill.SelectedIndex==-1)
				comboProvBill.SelectedIndex=0;
			comboProvTreat.Items.Clear();
			for(int i=0;i<Providers.List.Length;i++){
				comboProvTreat.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==ClaimCur.ProvTreat)
					comboProvTreat.SelectedIndex=i;
			}
			if(comboProvTreat.Items.Count>0 && comboProvTreat.SelectedIndex==-1)
				comboProvTreat.SelectedIndex=0;
			textPreAuth.Text=ClaimCur.PreAuthString;
			textPlan.Text=InsPlans.GetDescript(ClaimCur.PlanNum,FamCur,PlanList);
			comboPatRelat.SelectedIndex=(int)ClaimCur.PatRelat;
			textPlan2.Text=InsPlans.GetDescript(ClaimCur.PlanNum2,FamCur,PlanList);
			comboPatRelat2.SelectedIndex=(int)ClaimCur.PatRelat2;
			if(textPlan2.Text==""){
				comboPatRelat2.Visible=false;
				label10.Visible=false;
			}
			else{
				comboPatRelat2.Visible=true;
				label10.Visible=true;
			}
			switch(ClaimCur.IsProsthesis){
				case "N"://no
					radioProsthN.Checked=true;
					break;
				case "I"://initial
					radioProsthI.Checked=true;
					break;
				case "R"://replacement
					radioProsthR.Checked=true;
					break;
			}
			if(ClaimCur.PriorDate.Year < 1860)
				textPriorDate.Text="";
			else
				textPriorDate.Text=ClaimCur.PriorDate.ToShortDateString();
			textReasonUnder.Text=ClaimCur.ReasonUnderPaid;
			textNote.Text=ClaimCur.ClaimNote;
			checkIsOrtho.Checked=ClaimCur.IsOrtho;
			textOrthoRemainM.Text=ClaimCur.OrthoRemainM.ToString();
			if(ClaimCur.OrthoDate.Year < 1860)
				textOrthoDate.Text="";
			else
				textOrthoDate.Text=ClaimCur.OrthoDate.ToShortDateString();
			textRadiographs.Text=ClaimCur.Radiographs.ToString();
			FillGrids();
		}

		private void listClaimType_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			//not allowed to change claim type
			switch(ClaimCur.ClaimType){
				case "P":
					listClaimType.SelectedIndex=0;
					break;
				case "S":
					listClaimType.SelectedIndex=1;
					break;
				case "PreAuth":
					listClaimType.SelectedIndex=2;
					break;
				case "Other":
					listClaimType.SelectedIndex=3;
					break;
				case "Cap":
					listClaimType.SelectedIndex=4;
					break;
			}
		}

		private void butRecalc_Click(object sender, System.EventArgs e) {
			if(!ClaimIsValid()){
				return;
			}
			Benefit[] benefitList=Benefits.Refresh(PatPlanList);
			bool isFamMax=Benefits.GetIsFamMax(benefitList,ClaimCur.PlanNum);
			bool isFamDed=Benefits.GetIsFamDed(benefitList,ClaimCur.PlanNum);
			if(isFamMax || isFamDed){
				ClaimProc[] claimProcsFam=ClaimProcs.RefreshFam(ClaimCur.PlanNum);
				Claims.CalculateAndUpdate(claimProcsFam,ProcList,PlanList,ClaimCur,PatPlanList,benefitList);
			}
			else{
				Claims.CalculateAndUpdate(ClaimProcList,ProcList,PlanList,ClaimCur,PatPlanList,benefitList);
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		private void FillGrids(){
			//must run claimprocs.refresh separately beforehand
			//also recalculates totals because user might have changed certain items.
			double claimFee=0;
			double dedApplied=0;
			double insPayEst=0;
			double insPayAmt=0;
			double writeOff=0;
			gridProc.BeginUpdate();
			gridProc.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableClaimProc","#"),25);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Date"),70);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Prov"),50);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Code"),50);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Tth"),35);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Description"),130);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Fee Billed"),65,HorizontalAlignment.Right);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Deduct"),65,HorizontalAlignment.Right);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Ins Est"),65,HorizontalAlignment.Right);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Ins Pay"),65,HorizontalAlignment.Right);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","WriteOff"),65,HorizontalAlignment.Right);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Status"),50,HorizontalAlignment.Center);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Pmt"),40,HorizontalAlignment.Center);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Remarks"),145);
			gridProc.Columns.Add(col);			 
			gridProc.Rows.Clear();
			ODGridRow row;
			Procedure ProcCur;
			ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,ClaimCur.ClaimNum);
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				row=new ODGridRow();
				if(ClaimProcsForClaim[i].LineNumber==0){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(ClaimProcsForClaim[i].LineNumber.ToString());
				}
				row.Cells.Add(ClaimProcsForClaim[i].ProcDate.ToShortDateString());
				row.Cells.Add(Providers.GetAbbr(((ClaimProc)ClaimProcsForClaim[i]).ProvNum));
				if(ClaimProcsForClaim[i].ProcNum==0) {
					row.Cells.Add("");//code
					row.Cells.Add("");//tooth
					if(ClaimProcsForClaim[i].Status==ClaimProcStatus.NotReceived)
						row.Cells.Add(Lan.g(this,"Estimate"));
					else
						row.Cells.Add(Lan.g(this,"Total Payment"));
				}
				else {
					ProcCur=Procedures.GetProc(ProcList,ClaimProcsForClaim[i].ProcNum);
					row.Cells.Add(ClaimProcsForClaim[i].CodeSent);
					row.Cells.Add(Tooth.ToInternat(ProcCur.ToothNum));
					row.Cells.Add(ProcedureCodes.GetProcCode(ProcCur.CodeNum).Descript);
				}
				row.Cells.Add(ClaimProcsForClaim[i].FeeBilled.ToString("F"));
				row.Cells.Add(ClaimProcsForClaim[i].DedApplied.ToString("F"));
				row.Cells.Add(ClaimProcsForClaim[i].InsPayEst.ToString("F"));
				row.Cells.Add(ClaimProcsForClaim[i].InsPayAmt.ToString("F"));
				row.Cells.Add(ClaimProcsForClaim[i].WriteOff.ToString("F"));
				switch(ClaimProcsForClaim[i].Status){
					case ClaimProcStatus.Received:
						row.Cells.Add("Recd");
						break;
					case ClaimProcStatus.NotReceived:
						row.Cells.Add("");
						break;
					//adjustment would never show here
					case ClaimProcStatus.Preauth:
						row.Cells.Add("PreA");
						break;
					case ClaimProcStatus.Supplemental:
						row.Cells.Add("Supp");
						break;
					case ClaimProcStatus.CapClaim:
						row.Cells.Add("Cap");
						break;
					case ClaimProcStatus.Estimate:
						row.Cells.Add("");
						MessageBox.Show("error. Estimate loaded.");
						break;
					case ClaimProcStatus.CapEstimate:
						row.Cells.Add("");
						MessageBox.Show("error. CapEstimate loaded.");
						break;
					case ClaimProcStatus.CapComplete:
						row.Cells.Add("");
						MessageBox.Show("error. CapComplete loaded.");
						break;
					//Estimate would never show here
					//Cap would never show here
					default:
						row.Cells.Add("");
						break;
				}
				if(ClaimProcsForClaim[i].ClaimPaymentNum>0){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
				row.Cells.Add(ClaimProcsForClaim[i].Remarks);
				claimFee+=ClaimProcsForClaim[i].FeeBilled;
				dedApplied+=ClaimProcsForClaim[i].DedApplied;
				insPayEst+=ClaimProcsForClaim[i].InsPayEst;
				insPayAmt+=ClaimProcsForClaim[i].InsPayAmt;
				//if(ClaimProcsForClaim[i].Status==ClaimProcStatus.Received
				//	|| ClaimProcsForClaim[i].Status==ClaimProcStatus.Supplemental) {
					writeOff+=ClaimProcsForClaim[i].WriteOff;
				//}
				gridProc.Rows.Add(row);
			}
			gridProc.EndUpdate();
			if(ClaimCur.ClaimType=="Cap"){
				//zero out ins info if Cap.  This keeps it from affecting the balance.  It could be slightly improved later if there is enough demand to show the inspayamt in the Account module.
				insPayEst=0;
				insPayAmt=0;
			}
			ClaimCur.ClaimFee=claimFee;
			ClaimCur.DedApplied=dedApplied;
			ClaimCur.InsPayEst=insPayEst;
			ClaimCur.InsPayAmt=insPayAmt;
			ClaimCur.WriteOff=writeOff;
			textClaimFee.Text=ClaimCur.ClaimFee.ToString("F");
			textDedApplied.Text=ClaimCur.DedApplied.ToString("F");
			textInsPayEst.Text=ClaimCur.InsPayEst.ToString("F");
			textInsPayAmt.Text=ClaimCur.InsPayAmt.ToString("F");
			textWriteOff.Text=writeOff.ToString("F");
			//payments
			gridPay.BeginUpdate();
			gridPay.Columns.Clear();
			col=new ODGridColumn(Lan.g("TableClaimPay","Date"),70);
			gridPay.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPay","Amount"),80,HorizontalAlignment.Right);
			gridPay.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPay","Check Num"),100);
			gridPay.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPay","Bank/Branch"),100);
			gridPay.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPay","Note"),180);
			gridPay.Columns.Add(col);
			gridPay.Rows.Clear();
			tablePayments=ClaimPayments.GetForClaim(ClaimCur.ClaimNum);
			for(int i=0;i<tablePayments.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(tablePayments.Rows[i]["checkDate"].ToString());
				row.Cells.Add(tablePayments.Rows[i]["amount"].ToString());
				row.Cells.Add(tablePayments.Rows[i]["CheckNum"].ToString());
				row.Cells.Add(tablePayments.Rows[i]["BankBranch"].ToString());
				row.Cells.Add(tablePayments.Rows[i]["Note"].ToString());
				gridPay.Rows.Add(row);
			}
			gridPay.EndUpdate();
		}

		private void gridProc_CellClick(object sender,ODGridClickEventArgs e) {
			if(gridPay.GetSelectedIndex()==-1){
				return;
			}
			gridPay.SetSelected(false);
		}
		
		private void gridProc_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//ClaimProcs.Cur=(ClaimProc)ClaimProcs.ForClaim[e.Row];
			if(!MsgBox.Show(this,true,"If you are trying to enter payment information, please use the payments buttons at the upper right.  Then, don't forget to finish by creating the check using the button below this section. You should probably click cancel unless you are just editing estimates. Continue anyway?")){
				return;
			}
			FormClaimProc FormCP=new FormClaimProc(ClaimProcsForClaim[e.Row],null,FamCur,PlanList);
			FormCP.IsInClaim=true;

			FormCP.ShowDialog();
			if(FormCP.DialogResult!=DialogResult.OK){
				return;
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		private void gridPay_CellClick(object sender,ODGridClickEventArgs e) {
			if(e.Row>tablePayments.Rows.Count-1){
				return;//prevents crash after deleting a check?
			}
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				if(ClaimProcsForClaim[i].ClaimPaymentNum.ToString()==tablePayments.Rows[e.Row]["ClaimPaymentNum"].ToString())
					gridProc.SetSelected(i,true);
				else
					gridProc.SetSelected(i,false);
			}
		}

		private void gridPay_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			int tempClaimNum=ClaimCur.ClaimNum;
			//remember that the claimpayment.List is not entirely accurate
			ClaimPayment claimPaymentCur=ClaimPayments.GetOne(PIn.PInt(tablePayments.Rows[e.Row]["ClaimPaymentNum"].ToString()));
			FormClaimPayEdit FormCPE=new FormClaimPayEdit(claimPaymentCur);
			FormCPE.OriginatingClaimNum=ClaimCur.ClaimNum;
			FormCPE.ShowDialog();
			Claims.Refresh(PatCur.PatNum);
			ClaimCur=((Claim)Claims.HList[tempClaimNum]);
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		private void butOtherCovChange_Click(object sender, System.EventArgs e) {
			FormInsPlanSelect FormIPS=new FormInsPlanSelect(PatCur.PatNum);
			FormIPS.ShowDialog();
			if(FormIPS.DialogResult!=DialogResult.OK){
				return;
			}
			ClaimCur.PlanNum2=FormIPS.SelectedPlan.PlanNum;
			textPlan2.Text=InsPlans.GetDescript(ClaimCur.PlanNum2,FamCur,PlanList);
			if(textPlan2.Text==""){
				comboPatRelat2.Visible=false;
				label10.Visible=false;
			}
			else{
				comboPatRelat2.Visible=true;
				label10.Visible=true;
			}
		}

		private void butOtherNone_Click(object sender, System.EventArgs e) {
			ClaimCur.PlanNum2=0;
			ClaimCur.PatRelat2=Relat.Self;
			textPlan2.Text="";
			comboPatRelat2.Visible=false;
			label10.Visible=false;
		}

		private void butPayTotal_Click(object sender, System.EventArgs e) {
			//preauths are only allowed "payment" entry by procedure since a total would be meaningless
			if(ClaimCur.ClaimType=="PreAuth"){
				MessageBox.Show(Lan.g(this,"PreAuthorizations can only be entered by procedure."));
				return;
			}
			if(ClaimCur.ClaimType=="Cap"){
				if(MessageBox.Show(Lan.g(this,"If you enter by total, the insurance payment will affect the patient balance.  It is recommended to enter by procedure instead.  Continue anyway?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				return;
			}
			Double dedEst=0;
			Double payEst=0;
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				if(ClaimProcsForClaim[i].Status!=ClaimProcStatus.NotReceived){
					continue;
				}
				if(ClaimProcsForClaim[i].ProcNum==0){
					continue;//also ignore non-procedures.
				}
				//ClaimProcs.Cur=ClaimProcs.ForClaim[i];
				dedEst+=ClaimProcsForClaim[i].DedApplied;
				payEst+=ClaimProcsForClaim[i].InsPayEst;
			}
			ClaimProc ClaimProcCur=new ClaimProc();
			//ClaimProcs.Cur.ProcNum 
			ClaimProcCur.ClaimNum=ClaimCur.ClaimNum;
			ClaimProcCur.PatNum=ClaimCur.PatNum;
			ClaimProcCur.ProvNum=ClaimCur.ProvTreat;
			//ClaimProcs.Cur.FeeBilled
			//ClaimProcs.Cur.InsPayEst
			ClaimProcCur.DedApplied=dedEst;
			ClaimProcCur.Status=ClaimProcStatus.Received;
			ClaimProcCur.InsPayAmt=payEst;
			//remarks
			//ClaimProcs.Cur.ClaimPaymentNum
			ClaimProcCur.PlanNum=ClaimCur.PlanNum;
			ClaimProcCur.DateCP=DateTime.Today;
			ClaimProcCur.ProcDate=ClaimCur.DateService;
			ClaimProcCur.DateEntry=DateTime.Now;//will get set anyway
			ClaimProcs.Insert(ClaimProcCur);
			FormClaimProc FormCP=new FormClaimProc(ClaimProcCur,null,FamCur,PlanList);
			FormCP.IsInClaim=true;
			FormCP.ShowDialog();
			if(FormCP.DialogResult!=DialogResult.OK){
				ClaimProcs.Delete(ClaimProcCur);
			}
			else{
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					if(ClaimProcsForClaim[i].Status!=ClaimProcStatus.NotReceived){
						continue;
					}
					//ClaimProcs.Cur=ClaimProcs.ForClaim[i];
					ClaimProcsForClaim[i].Status=ClaimProcStatus.Received;
					if(ClaimProcsForClaim[i].DedApplied>0){
						ClaimProcsForClaim[i].InsPayEst+=ClaimProcsForClaim[i].DedApplied;
						ClaimProcsForClaim[i].DedApplied=0;//because ded will show as part of payment now.
					}
					ClaimProcsForClaim[i].DateEntry=DateTime.Now;//the date is was switched to rec'd
					ClaimProcs.Update(ClaimProcsForClaim[i]);
				}
			}
			listClaimStatus.SelectedIndex=5;//Received
			if(textDateRec.Text==""){
				textDateRec.Text=DateTime.Today.ToShortDateString();
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		private void butPayProc_Click(object sender, System.EventArgs e) {
			//this will work for regular claims and for preauths.
			//it will enter edit mode if it can only find received procs not attached to payments yet.
			if(gridProc.SelectedIndices.Length==0){
				//first, autoselect rows if not received:
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					if(ClaimProcsForClaim[i].Status==ClaimProcStatus.NotReceived
						&& ClaimProcsForClaim[i].ProcNum>0){//and is procedure
						gridProc.SetSelected(i,true);
					}
				}
			}
			if(gridProc.SelectedIndices.Length==0){
				//then, autoselect rows if not paid on:
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					if(ClaimProcsForClaim[i].ClaimPaymentNum==0
						&& ClaimProcsForClaim[i].ProcNum>0){//and is procedure
						gridProc.SetSelected(i,true);
					}
				}
			}
			if(gridProc.SelectedIndices.Length==0){
				//if still no rows selected
				MessageBox.Show(Lan.g(this,"All procedures in the list have already been paid."));
				return;
			}
			bool allAreProcs=true;
			for(int i=0;i<gridProc.SelectedIndices.Length;i++){
				if(ClaimProcsForClaim[gridProc.SelectedIndices[i]].ProcNum==0)
					allAreProcs=false;
			}
			if(!allAreProcs){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			FormClaimPayTotal FormCPT=new FormClaimPayTotal(PatCur,FamCur,PlanList);
			FormCPT.ClaimProcsToEdit=new ClaimProc[gridProc.SelectedIndices.Length];
			for(int i=0;i<gridProc.SelectedIndices.Length;i++){
				//copy selected claimprocs to temporary array for editing.
				//no changes to the database will be made within that form.
				FormCPT.ClaimProcsToEdit[i]=ClaimProcsForClaim[gridProc.SelectedIndices[i]];
				if(ClaimCur.ClaimType=="PreAuth"){
					FormCPT.ClaimProcsToEdit[i].Status=ClaimProcStatus.Preauth;
				}
				else if(ClaimCur.ClaimType=="Cap"){
					;//do nothing.  The claimprocstatus will remain Capitation.
				}
				else{
					FormCPT.ClaimProcsToEdit[i].Status=ClaimProcStatus.Received;
					FormCPT.ClaimProcsToEdit[i].DateEntry=DateTime.Now;//date is was set rec'd
				}
				FormCPT.ClaimProcsToEdit[i].InsPayAmt=FormCPT.ClaimProcsToEdit[i].InsPayEst;
				FormCPT.ClaimProcsToEdit[i].DateCP=DateTime.Today;
			}
			FormCPT.ShowDialog();
			if(FormCPT.DialogResult!=DialogResult.OK){
				return;
			}
			//save changes now
			for(int i=0;i<FormCPT.ClaimProcsToEdit.Length;i++){
				ClaimProcs.Update(FormCPT.ClaimProcsToEdit[i]);
			}
			listClaimStatus.SelectedIndex=5;//Received
			if(textDateRec.Text==""){
				textDateRec.Text=DateTime.Today.ToShortDateString();
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		private void butPaySupp_Click(object sender, System.EventArgs e) {
			if(gridProc.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"This is only for additional payments on procedures already marked received.  Please highlight procedures first."));
				return;
			}
			bool allAreRecd=true;
			for(int i=0;i<gridProc.SelectedIndices.Length;i++){
				if(ClaimProcsForClaim[gridProc.SelectedIndices[i]].Status!=ClaimProcStatus.Received)
					allAreRecd=false;
			}
			if(!allAreRecd){
				MessageBox.Show(Lan.g(this,"All selected procedures must be status received."));
				return;
			}
			for(int i=0;i<gridProc.SelectedIndices.Length;i++){
				ClaimProc ClaimProcCur=ClaimProcsForClaim[gridProc.SelectedIndices[i]];
				ClaimProcCur.FeeBilled=0;
				ClaimProcCur.ClaimPaymentNum=0;//no payment attached
				//claimprocnum will be overwritten
				ClaimProcCur.DedApplied=0;
				ClaimProcCur.InsPayAmt=0;
				ClaimProcCur.InsPayEst=0;
				ClaimProcCur.Remarks="";
				ClaimProcCur.Status=ClaimProcStatus.Supplemental;
				ClaimProcCur.WriteOff=0;
				ClaimProcs.Insert(ClaimProcCur);//this inserts a copy of the original with the changes as above.
			}
//fix: need to debug the recalculation feature to take this status into account.
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		private void butSplit_Click(object sender, System.EventArgs e) {
			if(!ClaimIsValid())
				return;
			UpdateClaim();
			if(gridProc.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please highlight procedures first."));
				return;
			}
			for(int i=0;i<gridProc.SelectedIndices.Length;i++){
				if(ClaimProcsForClaim[gridProc.SelectedIndices[i]].ProcNum==0){
					MessageBox.Show(Lan.g(this,"Only procedures can be selected."));
					return;
				}
				if(ClaimProcsForClaim[gridProc.SelectedIndices[i]].InsPayAmt!=0){
					MessageBox.Show(Lan.g(this,"All selected procedures must have zero insurance payment amounts."));
					return;
				}
			}
			Claim newClaim=ClaimCur.Copy();
			Claims.Insert(newClaim);
			//now this claim has been precisely duplicated, except it has a new ClaimNum.  So there are no attached claimprocs.
			for(int i=0;i<gridProc.SelectedIndices.Length;i++){
				ClaimProcsForClaim[gridProc.SelectedIndices[i]].ClaimNum=newClaim.ClaimNum;
				ClaimProcs.Update(ClaimProcsForClaim[gridProc.SelectedIndices[i]]);//moves it to the new claim
			}
			//now, set Claims.Cur back to the originalClaim.  The new claim will now show on the account.
			//ClaimCur=originalClaim;//.Copy()
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		//private void butCheckDelete_Click(object sender, System.EventArgs e) {
		
		//}

		///<summary>Creates insurance check</summary>
		private void butCheckAdd_Click(object sender, System.EventArgs e) {
			bool existsReceived=false;
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				if((ClaimProcsForClaim[i].Status==ClaimProcStatus.Received
					|| ClaimProcsForClaim[i].Status==ClaimProcStatus.Supplemental)
					&& ClaimProcsForClaim[i].InsPayAmt!=0)
				{
					existsReceived=true;
				}
			}
			if(!existsReceived){
				MessageBox.Show(Lan.g(this,"There are no valid received payments for this claim."));
				return;
			}
			int tempClaimNum=ClaimCur.ClaimNum;
			ClaimPayment ClaimPaymentCur=new ClaimPayment();
			ClaimPaymentCur.CheckDate=DateTime.Today;
			ClaimPaymentCur.ClinicNum=PatCur.ClinicNum;
			ClaimPaymentCur.CarrierName=Carriers.GetName(InsPlans.GetPlan(ClaimCur.PlanNum,PlanList).CarrierNum);
			ClaimPayments.Insert(ClaimPaymentCur);
			FormClaimPayEdit FormCPE=new FormClaimPayEdit(ClaimPaymentCur);
			FormCPE.OriginatingClaimNum=ClaimCur.ClaimNum;
			FormCPE.IsNew=true;
			FormCPE.ShowDialog();
			Claims.Refresh(PatCur.PatNum);
			ClaimCur=((Claim)Claims.HList[tempClaimNum]);
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		private void radioProsthN_Click(object sender, System.EventArgs e) {
			ClaimCur.IsProsthesis="N";
		}

		private void radioProsthI_Click(object sender, System.EventArgs e) {
			ClaimCur.IsProsthesis="I";
		}

		private void radioProsthR_Click(object sender, System.EventArgs e) {
			ClaimCur.IsProsthesis="R";
		}

		private void butSupp_Click(object sender, System.EventArgs e) {
			//only visible if Canadian
			CanadianClaim canclaim=CanadianClaims.GetForClaim(ClaimCur.ClaimNum);
			if(canclaim==null){
				ToothInitial[] ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
				ArrayList missingAL=ToothInitials.GetMissingOrHiddenTeeth(ToothInitialList);
				List<CanadianExtract> missingList=new List<CanadianExtract>();
				CanadianExtract canext;
				for(int i=0;i<missingAL.Count;i++) {
					canext=new CanadianExtract();
					canext.ClaimNum=ClaimCur.ClaimNum;//redundant
					canext.ToothNum=(string)missingAL[i];
					missingList.Add(canext);
				}
				canclaim=CanadianClaims.Insert(ClaimCur.ClaimNum,missingList);
			}
			FormClaimCanadian FormC=new FormClaimCanadian();
			FormC.ClaimCur=ClaimCur;
			FormC.CanCur=canclaim;
			FormC.ShowDialog();
		}

		private void butReferralNone_Click(object sender,EventArgs e) {
			textRefProv.Text="";
			ClaimCur.ReferringProv=0;
			butReferralEdit.Enabled=false;
		}

		private void butReferralSelect_Click(object sender,EventArgs e) {
			FormReferralSelect FormR=new FormReferralSelect();
			FormR.IsSelectionMode=true;
			FormR.ShowDialog();
			if(FormR.DialogResult==DialogResult.OK) {
				ClaimCur.ReferringProv=FormR.SelectedReferral.ReferralNum;
				textRefProv.Text=Referrals.GetNameLF(FormR.SelectedReferral.ReferralNum);
				butReferralEdit.Enabled=true;
			}
		}

		private void butReferralEdit_Click(object sender,EventArgs e) {
			//only enabled if ClaimCur.ReferringProv!=0
			Referral refer=Referrals.GetReferral(ClaimCur.ReferringProv);
			if(refer==null){
				MsgBox.Show(this,"Referral not found.");
				textRefProv.Text="";
				ClaimCur.ReferringProv=0;
				butReferralEdit.Enabled=false;
			}
			FormReferralEdit FormR=new FormReferralEdit(refer);
			FormR.ShowDialog();
			if(FormR.DialogResult==DialogResult.OK){
				//it's impossible to delete referral from that window.
				Referrals.Refresh();
				textRefProv.Text=Referrals.GetNameLF(refer.ReferralNum);
			}
		}

		private void butLabel_Click(object sender, System.EventArgs e) {
			LabelSingle label=new LabelSingle();
			PrintDocument pd=new PrintDocument();//only used to pass printerName
			if(!Printers.SetPrinter(pd,PrintSituation.LabelSingle)){
				return;
			}
			//ask if print secondary?
			InsPlan planCur=InsPlans.GetPlan(ClaimCur.PlanNum,PlanList);
			Carrier carrierCur=Carriers.GetCarrier(planCur.CarrierNum);
			label.PrintIns(carrierCur,pd.PrinterSettings.PrinterName);
		}

		private void butPreview_Click(object sender, System.EventArgs e) {
			if(!ClaimIsValid())
				return;
			UpdateClaim();
			FormClaimPrint FormCP=new FormClaimPrint();
			FormCP.ThisPatNum=ClaimCur.PatNum;
			FormCP.ThisClaimNum=ClaimCur.ClaimNum;
			FormCP.PrintImmediately=false;
			FormCP.ShowDialog();
			if(FormCP.DialogResult==DialogResult.OK) {
				//status will have changed to sent.
				ClaimCur=Claims.GetClaim(ClaimCur.ClaimNum);
			}
			Claims.Refresh(PatCur.PatNum); 
      ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillForm();		
		}

		private void ButPrint_Click(object sender,System.EventArgs e) {
			if(!ClaimIsValid())
				return;
			UpdateClaim();
			PrintDocument pd=new PrintDocument();
			if(!Printers.SetPrinter(pd,PrintSituation.Claim)) {
				return;
			}
			FormClaimPrint FormCP=new FormClaimPrint();
			FormCP.ThisPatNum=ClaimCur.PatNum;
			FormCP.ThisClaimNum=ClaimCur.ClaimNum;
			if(!FormCP.PrintImmediate(pd.PrinterSettings.PrinterName,pd.PrinterSettings.Copies)) {
				return;
			}
			Etranss.SetClaimSentOrPrinted(ClaimCur.ClaimNum,ClaimCur.PatNum,0,EtransType.ClaimPrinted,"",0);
			//ClaimCur.ClaimStatus="S";
			//ClaimCur.DateSent=DateTime.Today;
			//Claims.Update(ClaimCur);
			DialogResult=DialogResult.OK;
		}

		private void butSend_Click(object sender,EventArgs e) {
			if(!ClaimIsValid())
				return;
			UpdateClaim();
			ClaimSendQueueItem[] listQueue=Claims.GetQueueList(ClaimCur.ClaimNum);
			if(listQueue[0].NoSendElect) {
				MsgBox.Show(this,"This carrier is marked to not receive e-claims.");
				//Later: we need to let user send anyway, using all 0's for electronic id.
				return;
			}
			string missingData=Eclaims.Eclaims.GetMissingData(listQueue[0]);
			if(missingData!=""){
				MessageBox.Show(Lan.g(this,"Cannot send claim until missing data is fixed:")+"\r\n"+missingData);
				return;
			}
			Cursor=Cursors.WaitCursor;
			List<ClaimSendQueueItem> queueItems=new List<ClaimSendQueueItem>();
			queueItems.Add(listQueue[0]);
			Eclaims.Eclaims.SendBatches(queueItems);//this also calls SetClaimSentOrPrinted which creates the etrans entry.
			Cursor=Cursors.Default;
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;//jump straight to Closing, where the claimprocs will be changed
				return;
			}
			if(!ClaimIsValid())
				return;
			UpdateClaim();
			bool paymentIsAttached=false;
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				if(ClaimProcsForClaim[i].ClaimPaymentNum>0){
					paymentIsAttached=true;
				}
			}
			if(paymentIsAttached){
				MessageBox.Show(Lan.g(this,"You cannot delete this claim while any insurance checks are attached.  You will have to detach all insurance checks first."));
				return;
			}
			if(ClaimCur.ClaimStatus=="R"){
				MessageBox.Show(Lan.g(this,"You cannot delete this claim while status is Received.  You will have to change the status first."));
				return;
			}
      if(ClaimCur.ClaimType=="PreAuth"){
				if(MessageBox.Show(Lan.g(this,"Delete PreAuthorization?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
			}
			else{
				if(MessageBox.Show(Lan.g(this,"Delete Claim?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
			}
			Procedure proc;
			if(ClaimCur.ClaimType=="PreAuth"//all preauth claimprocs are just duplicates
				|| ClaimCur.ClaimType=="Cap"){//all cap claimprocs are just duplicates
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					//ClaimProcs.Cur=ClaimProcs.ForClaim[i];
					ClaimProcs.Delete(ClaimProcsForClaim[i]);
				}
			}
			else{//all other claim types use original estimate claimproc.
				Benefit[] benList=Benefits.Refresh(PatPlanList);
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					//ClaimProcs.Cur=ClaimProcs.ForClaim[i];
					if(ClaimProcsForClaim[i].Status==ClaimProcStatus.Supplemental//supplementals are duplicate
						|| ClaimProcsForClaim[i].ProcNum==0)//total payments get deleted
					{
						ClaimProcs.Delete(ClaimProcsForClaim[i]);
						continue;
					}
					//so only changed back to estimate if attached to a proc
					ClaimProcsForClaim[i].Status=ClaimProcStatus.Estimate;
					ClaimProcsForClaim[i].ClaimNum=0;
					proc=Procedures.GetProc(ProcList,ClaimProcsForClaim[i].ProcNum);
					if(ClaimCur.ClaimType=="P" && PatPlanList.Length>0){
						ClaimProcs.ComputeBaseEst(ClaimProcsForClaim[i],proc,PriSecTot.Pri,PlanList,PatPlanList,benList);
					}
					else if(ClaimCur.ClaimType=="S" && PatPlanList.Length>1){
						ClaimProcs.ComputeBaseEst(ClaimProcsForClaim[i],proc,PriSecTot.Sec,PlanList,PatPlanList,benList);
					}
					ClaimProcsForClaim[i].InsPayEst=0;
					ClaimProcs.Update(ClaimProcsForClaim[i]);
				}
			}
      Claims.Delete(ClaimCur);
	  SecurityLogs.MakeLogEntry(Permissions.ClaimsSentEdit,ClaimCur.PatNum,
		Lan.g(this,"Delete Ins Claim") + " - " + PatCur.GetNameLF());

      DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!ClaimIsValid())
				return;
			//if status is received, all claimprocs must also be received.
			if(listClaimStatus.SelectedIndex==5){
				bool allReceived=true;
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					if(((ClaimProc)ClaimProcsForClaim[i]).Status==ClaimProcStatus.NotReceived){
						allReceived=false;
					}
				}
				if(!allReceived){
					if(MessageBox.Show(Lan.g(this,"All items will be marked received.  Continue?")
						,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
					return;
					for(int i=0;i<ClaimProcsForClaim.Length;i++){
						if(ClaimProcsForClaim[i].Status==ClaimProcStatus.NotReceived){
							//ClaimProcs.Cur=(ClaimProc)ClaimProcs.ForClaim[i];
							ClaimProcsForClaim[i].Status=ClaimProcStatus.Received;
							ClaimProcsForClaim[i].DateEntry=DateTime.Now;//date it was set rec'd
							ClaimProcs.Update(ClaimProcsForClaim[i]);
						}
					}
				}
			}
			//if status is received and there is no received date
			if(listClaimStatus.SelectedIndex==5 && textDateRec.Text==""){
				textDateRec.Text=DateTime.Today.ToShortDateString();
			}
			UpdateClaim();
			if(listClaimStatus.SelectedIndex==2){//waiting to send
				ClaimSendQueueItem[] listQueue=Claims.GetQueueList(ClaimCur.ClaimNum);
				if(listQueue[0].NoSendElect) {
					DialogResult=DialogResult.OK;
					return;
				}
				string missingData=Eclaims.Eclaims.GetMissingData(listQueue[0]);
				if(missingData!="") {
					MessageBox.Show(Lan.g(this,"Cannot send claim until missing data is fixed:")+"\r\n"+missingData);
					DialogResult=DialogResult.OK;
					return;
				}
				//if(MsgBox.Show(this,true,"Send electronic claim immediately?")){
				//	List<ClaimSendQueueItem> queueItems=new List<ClaimSendQueueItem>();
				//	queueItems.Add(listQueue[0]);
				//	Eclaims.Eclaims.SendBatches(queueItems);//this also calls SetClaimSentOrPrinted which creates the etrans entry.
				//}
			}
			if(listClaimStatus.SelectedIndex==5){//Received
				Payment PaymentCur=new Payment();
				PaymentCur.PayDate=DateTime.Today;
				PaymentCur.PatNum=PatCur.PatNum;
				Payments.Insert(PaymentCur);
				FormProviderIncTrans FormPIT=new FormProviderIncTrans();
				FormPIT.IsNew=true;
				FormPIT.PaymentCur=PaymentCur;
				FormPIT.PatNum=PatCur.PatNum;
				FormPIT.ShowDialog();
			}
			DialogResult=DialogResult.OK;
		}
		
		private bool ClaimIsValid(){
			if(  textDateService.errorProvider1.GetError(textDateSent)!=""
				|| textDateSent.errorProvider1.GetError(textDateSent)!=""
				|| textDateRec.errorProvider1.GetError(textDateRec)!=""
				|| textPriorDate.errorProvider1.GetError(textPriorDate)!=""
				|| textDedApplied.errorProvider1.GetError(textDedApplied)!=""
				|| textInsPayAmt.errorProvider1.GetError(textInsPayAmt)!=""
				|| textOrthoDate.errorProvider1.GetError(textOrthoDate)!=""
				|| textRadiographs.errorProvider1.GetError(textRadiographs)!=""
				|| textAccidentDate.errorProvider1.GetError(textAccidentDate)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			if(textDateService.Text=="" && ClaimCur.ClaimType!="PreAuth"){
				MsgBox.Show(this,"Please enter a date of service");
				return false;
			}
			return true;
		}

		///<summary>Updates this claim to the database.</summary>
		private void UpdateClaim(){
			if(notAuthorized){
				return;
			}
			//patnum
			ClaimCur.DateService=PIn.PDate(textDateService.Text);
			if(textDateSent.Text=="")
				ClaimCur.DateSent=DateTime.MinValue;
			else ClaimCur.DateSent=PIn.PDate(textDateSent.Text);
			switch(listClaimStatus.SelectedIndex){
				case 0:
					ClaimCur.ClaimStatus="U";
					break;
				case 1:
					ClaimCur.ClaimStatus="H";
					break;
				case 2:
					ClaimCur.ClaimStatus="W";
					break;
				case 3:
					ClaimCur.ClaimStatus="P";
					break;
				case 4:
					ClaimCur.ClaimStatus="S";
					break;
				case 5:
					ClaimCur.ClaimStatus="R";
					break;
			}
			if(textDateRec.Text=="")
				ClaimCur.DateReceived=DateTime.MinValue;
			else ClaimCur.DateReceived=PIn.PDate(textDateRec.Text);
			//planNum
			//patRelats will always be selected
			ClaimCur.PatRelat=(Relat)comboPatRelat.SelectedIndex;
			ClaimCur.PatRelat2=(Relat)comboPatRelat2.SelectedIndex;
			if(comboProvTreat.SelectedIndex!=-1)
				ClaimCur.ProvTreat=Providers.List[comboProvTreat.SelectedIndex].ProvNum;
			ClaimCur.PreAuthString=textPreAuth.Text;
			//isprosthesis handled earlier
			ClaimCur.PriorDate=PIn.PDate(textPriorDate.Text);
			ClaimCur.ReasonUnderPaid=textReasonUnder.Text;
			ClaimCur.ClaimNote=textNote.Text;
			//ispreauth
			if(comboProvBill.SelectedIndex!=-1)
				ClaimCur.ProvBill=Providers.List[comboProvBill.SelectedIndex].ProvNum;
			ClaimCur.IsOrtho=checkIsOrtho.Checked;
			ClaimCur.OrthoRemainM=PIn.PInt(textOrthoRemainM.Text);
			ClaimCur.OrthoDate=PIn.PDate(textOrthoDate.Text);
			ClaimCur.Radiographs=PIn.PInt(textRadiographs.Text);
			ClaimCur.RefNumString=textRefNum.Text;
			ClaimCur.PlaceService=(PlaceOfService)comboPlaceService.SelectedIndex;
			ClaimCur.EmployRelated=(YN)comboEmployRelated.SelectedIndex;
			switch(comboAccident.SelectedIndex) {
				case 0:
					ClaimCur.AccidentRelated="";
					break;
				case 1:
					ClaimCur.AccidentRelated="A";
					break;
				case 2:
					ClaimCur.AccidentRelated="E";
					break;
				case 3:
					ClaimCur.AccidentRelated="O";
					break;
			}
			ClaimCur.AccidentDate=PIn.PDate(textAccidentDate.Text);
			ClaimCur.AccidentST=textAccidentST.Text;
			if(comboClinic.SelectedIndex==0)//none
				ClaimCur.ClinicNum=0;
			else
				ClaimCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			Claims.Update(ClaimCur);
			if(ClaimValCodes!=null){
				for(int i=0;i<ClaimValCodes.Count;i++){ //update existing Value Code pairs
					ClaimValCode vc = (ClaimValCode)ClaimValCodes[i];
					TextBox code = (TextBox)Controls.Find("textVC" + i + "Code", true)[0];
					vc.ValCode=code.Text.ToString();
					TextBox amount = (TextBox)Controls.Find("textVC" + i + "Amount", true)[0];
					string amt = amount.Text;
					if(amt=="")
						amt = "0";
					vc.ValAmount=Double.Parse(amt);
				}
				for(int i=(ClaimValCodes.Count);i<12;i++){ //add new Value Code pairs
					ClaimValCode vc = new ClaimValCode();
					TextBox code = (TextBox)Controls.Find("textVC" + i + "Code", true)[0];
					vc.ValCode=code.Text.ToString();
					TextBox amount = (TextBox)Controls.Find("textVC" + i + "Amount", true)[0];
					string amt = amount.Text;
					if(amt=="")
						amt = "0";
					vc.ValAmount=Double.Parse(amt);
					vc.ClaimNum=ClaimCur.ClaimNum;
					vc.ClaimValCodeLogNum=0;
					if(vc.ValCode!="" || vc.ValAmount!=0){
						ClaimValCodes.Add(vc);
					}
				}
				ClaimValCodeLog.Update(ClaimValCodes);
			}
			//if(ClaimCur.ClaimStatus=="S"){
				//SecurityLogs.MakeLogEntry("Claims Sent Edit",Claims.cmd.CommandText,user);
			//}
		}

		//cancel does not cancel in some circumstances because cur gets updated in some areas.
		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormClaimEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					if(ClaimProcsForClaim[i].Status==ClaimProcStatus.CapClaim){
						ClaimProcs.Delete(ClaimProcsForClaim[i]);
					}
					else if(ClaimProcsForClaim[i].Status==ClaimProcStatus.NotReceived){
						ClaimProcsForClaim[i].Status=ClaimProcStatus.Estimate;
						ClaimProcsForClaim[i].ClaimNum=0;
						ClaimProcsForClaim[i].InsPayEst=0;
						ClaimProcs.Update(ClaimProcsForClaim[i]);
					}
				}
				Claims.Delete(ClaimCur);//does not do any validation.  Also deletes the claimcanadian.
			}
		}

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

	}
}
