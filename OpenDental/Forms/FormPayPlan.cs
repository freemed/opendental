using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDental.ReportingOld2;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormPayPlan : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox2;
		private OpenDental.ValidDate textDate;
		private OpenDental.ValidDouble textAmount;
		private OpenDental.ValidDate textDateFirstPay;
		private OpenDental.ValidDouble textAPR;
		private OpenDental.ValidNum textTerm;
		private OpenDental.UI.Button butPrint;
		private System.Windows.Forms.TextBox textGuarantor;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butGoToGuar;
		private OpenDental.UI.Button butGoToPat;
		private System.Windows.Forms.TextBox textPatient;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.GroupBox groupBox3;
		private OpenDental.ValidDouble textDownPayment;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.Label label12;
		/// <summary>Go to the specified patnum.  Upon dialog close, if this number is not 0, then patients.Cur will be changed to this new patnum, and Account refreshed to the new patient.</summary>
		public long GotoPatNum;
		private System.Windows.Forms.Label label13;
		//private double amtPaid;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textTotalCost;
		private System.Windows.Forms.Label label10;
		private OpenDental.UI.Button butDelete;
		private OpenDental.ODtextBox textNote;
		private Patient PatCur;
		private System.Windows.Forms.TextBox textAccumulatedDue;
		private OpenDental.UI.Button butCreateSched;
		private OpenDental.ValidDouble textPeriodPayment;
		private System.Windows.Forms.RadioButton radioMonthly;
		private System.Windows.Forms.RadioButton radioQuarterly;
		private PayPlan PayPlanCur;
		private OpenDental.UI.Button butChangeGuar;
		private System.Windows.Forms.TextBox textInsPlan;
		private OpenDental.UI.Button butChangePlan;
		private System.Windows.Forms.CheckBox checkIns;
		private System.Windows.Forms.Label labelGuarantor;
		private System.Windows.Forms.Label labelInsPlan;
		///<summary>Only used for new payment plan.  Pass in the starting amount.</summary>
		public double TotalAmt;
		///<summary>Family for the patient of this payplan.  Used to display insurance info.</summary>
		private Family FamCur;
		///<summary>Used to display insurance info.</summary>
		private List <InsPlan> InsPlanList;
		private OpenDental.UI.ODGrid gridCharges;
		private OpenDental.UI.Button butClear;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.TextBox textAmtPaid;
		private System.Windows.Forms.TextBox textPrincPaid;
		private System.Windows.Forms.Label label14;
		//private List<PayPlanCharge> ChargeList;
		private double AmtPaid;
		private DataTable table;
		private double TotPrinc;
		private double TotInt;
		private Label label1;
		private ValidDouble textCompletedAmt;
		private Label label3;
		private OpenDental.UI.Button butPickProv;
		private ComboBox comboProv;
		private ComboBox comboClinic;
		private Label labelClinic;
		private Label label16;
		private GroupBox groupBox1;
		private double TotPrincInt;

		///<summary>The supplied payment plan should already have been saved in the database.</summary>
		public FormPayPlan(Patient patCur,PayPlan payPlanCur){
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			PatCur=patCur.Copy();
			PayPlanCur=payPlanCur.Copy();
			FamCur=Patients.GetFamily(PatCur.PatNum);
			InsPlanList=InsPlans.RefreshForFam(FamCur);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPayPlan));
			this.labelGuarantor = new System.Windows.Forms.Label();
			this.textGuarantor = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioQuarterly = new System.Windows.Forms.RadioButton();
			this.radioMonthly = new System.Windows.Forms.RadioButton();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textTotalCost = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.textPatient = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.label12 = new System.Windows.Forms.Label();
			this.textAmtPaid = new System.Windows.Forms.TextBox();
			this.textAccumulatedDue = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.textInsPlan = new System.Windows.Forms.TextBox();
			this.labelInsPlan = new System.Windows.Forms.Label();
			this.checkIns = new System.Windows.Forms.CheckBox();
			this.textPrincPaid = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboProv = new System.Windows.Forms.ComboBox();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butPickProv = new OpenDental.UI.Button();
			this.textCompletedAmt = new OpenDental.ValidDouble();
			this.butAdd = new OpenDental.UI.Button();
			this.butClear = new OpenDental.UI.Button();
			this.butChangePlan = new OpenDental.UI.Button();
			this.gridCharges = new OpenDental.UI.ODGrid();
			this.textNote = new OpenDental.ODtextBox();
			this.butDelete = new OpenDental.UI.Button();
			this.butGoToPat = new OpenDental.UI.Button();
			this.butGoToGuar = new OpenDental.UI.Button();
			this.textDate = new OpenDental.ValidDate();
			this.butChangeGuar = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textAPR = new OpenDental.ValidDouble();
			this.textPeriodPayment = new OpenDental.ValidDouble();
			this.textTerm = new OpenDental.ValidNum();
			this.textDownPayment = new OpenDental.ValidDouble();
			this.textDateFirstPay = new OpenDental.ValidDate();
			this.textAmount = new OpenDental.ValidDouble();
			this.butCreateSched = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelGuarantor
			// 
			this.labelGuarantor.Location = new System.Drawing.Point(28,32);
			this.labelGuarantor.Name = "labelGuarantor";
			this.labelGuarantor.Size = new System.Drawing.Size(126,17);
			this.labelGuarantor.TabIndex = 2;
			this.labelGuarantor.Text = "Guarantor";
			this.labelGuarantor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textGuarantor
			// 
			this.textGuarantor.Location = new System.Drawing.Point(156,32);
			this.textGuarantor.Name = "textGuarantor";
			this.textGuarantor.ReadOnly = true;
			this.textGuarantor.Size = new System.Drawing.Size(199,20);
			this.textGuarantor.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(21,190);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(133,17);
			this.label2.TabIndex = 5;
			this.label2.Text = "Date of Agreement";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(5,14);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(134,17);
			this.label4.TabIndex = 10;
			this.label4.Text = "Total Amount";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(5,36);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(135,17);
			this.label5.TabIndex = 12;
			this.label5.Text = "Date of First Payment";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(3,80);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(138,17);
			this.label6.TabIndex = 14;
			this.label6.Text = "APR (for example 0 or 18)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8,40);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(122,17);
			this.label7.TabIndex = 16;
			this.label7.Text = "Payment Amt";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(7,16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(124,17);
			this.label8.TabIndex = 19;
			this.label8.Text = "Number of Payments";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioQuarterly);
			this.groupBox2.Controls.Add(this.radioMonthly);
			this.groupBox2.Controls.Add(this.textAPR);
			this.groupBox2.Controls.Add(this.groupBox3);
			this.groupBox2.Controls.Add(this.textDownPayment);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.textDateFirstPay);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.textAmount);
			this.groupBox2.Controls.Add(this.butCreateSched);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(14,210);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(355,170);
			this.groupBox2.TabIndex = 22;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Terms";
			// 
			// radioQuarterly
			// 
			this.radioQuarterly.Location = new System.Drawing.Point(243,120);
			this.radioQuarterly.Name = "radioQuarterly";
			this.radioQuarterly.Size = new System.Drawing.Size(104,17);
			this.radioQuarterly.TabIndex = 44;
			this.radioQuarterly.Text = "Quarterly";
			// 
			// radioMonthly
			// 
			this.radioMonthly.Checked = true;
			this.radioMonthly.Location = new System.Drawing.Point(243,103);
			this.radioMonthly.Name = "radioMonthly";
			this.radioMonthly.Size = new System.Drawing.Size(104,17);
			this.radioMonthly.TabIndex = 43;
			this.radioMonthly.TabStop = true;
			this.radioMonthly.Text = "Monthly";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.textPeriodPayment);
			this.groupBox3.Controls.Add(this.textTerm);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(9,101);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(225,64);
			this.groupBox3.TabIndex = 23;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Either";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(4,59);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(136,17);
			this.label11.TabIndex = 21;
			this.label11.Text = "Down Payment";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTotalCost
			// 
			this.textTotalCost.Location = new System.Drawing.Point(156,385);
			this.textTotalCost.Name = "textTotalCost";
			this.textTotalCost.ReadOnly = true;
			this.textTotalCost.Size = new System.Drawing.Size(85,20);
			this.textTotalCost.TabIndex = 35;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(19,385);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(134,17);
			this.label15.TabIndex = 34;
			this.label15.Text = "Total Cost of Loan";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPatient
			// 
			this.textPatient.Location = new System.Drawing.Point(156,10);
			this.textPatient.Name = "textPatient";
			this.textPatient.ReadOnly = true;
			this.textPatient.Size = new System.Drawing.Size(199,20);
			this.textPatient.TabIndex = 25;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(30,10);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(124,17);
			this.label9.TabIndex = 24;
			this.label9.Text = "Patient";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(22,431);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(133,17);
			this.label12.TabIndex = 30;
			this.label12.Text = "Paid so far";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAmtPaid
			// 
			this.textAmtPaid.Location = new System.Drawing.Point(156,429);
			this.textAmtPaid.Name = "textAmtPaid";
			this.textAmtPaid.ReadOnly = true;
			this.textAmtPaid.Size = new System.Drawing.Size(85,20);
			this.textAmtPaid.TabIndex = 31;
			// 
			// textAccumulatedDue
			// 
			this.textAccumulatedDue.Location = new System.Drawing.Point(156,407);
			this.textAccumulatedDue.Name = "textAccumulatedDue";
			this.textAccumulatedDue.ReadOnly = true;
			this.textAccumulatedDue.Size = new System.Drawing.Size(85,20);
			this.textAccumulatedDue.TabIndex = 33;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(20,409);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(135,17);
			this.label13.TabIndex = 32;
			this.label13.Text = "Accumulated Due";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(23,507);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(148,17);
			this.label10.TabIndex = 37;
			this.label10.Text = "Note";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textInsPlan
			// 
			this.textInsPlan.Location = new System.Drawing.Point(156,167);
			this.textInsPlan.Name = "textInsPlan";
			this.textInsPlan.ReadOnly = true;
			this.textInsPlan.Size = new System.Drawing.Size(199,20);
			this.textInsPlan.TabIndex = 43;
			// 
			// labelInsPlan
			// 
			this.labelInsPlan.Location = new System.Drawing.Point(21,167);
			this.labelInsPlan.Name = "labelInsPlan";
			this.labelInsPlan.Size = new System.Drawing.Size(132,17);
			this.labelInsPlan.TabIndex = 42;
			this.labelInsPlan.Text = "Insurance Plan";
			this.labelInsPlan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIns
			// 
			this.checkIns.Location = new System.Drawing.Point(156,148);
			this.checkIns.Name = "checkIns";
			this.checkIns.Size = new System.Drawing.Size(268,18);
			this.checkIns.TabIndex = 46;
			this.checkIns.Text = "Use for tracking expected insurance payments";
			this.checkIns.Click += new System.EventHandler(this.checkIns_Click);
			// 
			// textPrincPaid
			// 
			this.textPrincPaid.Location = new System.Drawing.Point(156,451);
			this.textPrincPaid.Name = "textPrincPaid";
			this.textPrincPaid.ReadOnly = true;
			this.textPrincPaid.Size = new System.Drawing.Size(85,20);
			this.textPrincPaid.TabIndex = 56;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(22,453);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(133,17);
			this.label14.TabIndex = 55;
			this.label14.Text = "Principal paid so far";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4,475);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(151,17);
			this.label1.TabIndex = 57;
			this.label1.Text = "Tx Completed Amt";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(244,474);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(180,40);
			this.label3.TabIndex = 59;
			this.label3.Text = "This should usually match the total amount of the pay plan.";
			// 
			// comboProv
			// 
			this.comboProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProv.Location = new System.Drawing.Point(142,14);
			this.comboProv.MaxDropDownItems = 30;
			this.comboProv.Name = "comboProv";
			this.comboProv.Size = new System.Drawing.Size(158,21);
			this.comboProv.TabIndex = 169;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(142,39);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(177,21);
			this.comboClinic.TabIndex = 167;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(26,41);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(114,16);
			this.labelClinic.TabIndex = 168;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(41,18);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(100,16);
			this.label16.TabIndex = 166;
			this.label16.Text = "Provider";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboClinic);
			this.groupBox1.Controls.Add(this.butPickProv);
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.comboProv);
			this.groupBox1.Controls.Add(this.labelClinic);
			this.groupBox1.Location = new System.Drawing.Point(14,76);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(347,65);
			this.groupBox1.TabIndex = 171;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Same for all charges";
			// 
			// butPickProv
			// 
			this.butPickProv.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPickProv.Autosize = false;
			this.butPickProv.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickProv.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickProv.CornerRadius = 2F;
			this.butPickProv.Location = new System.Drawing.Point(301,14);
			this.butPickProv.Name = "butPickProv";
			this.butPickProv.Size = new System.Drawing.Size(18,21);
			this.butPickProv.TabIndex = 170;
			this.butPickProv.Text = "...";
			// 
			// textCompletedAmt
			// 
			this.textCompletedAmt.Location = new System.Drawing.Point(156,473);
			this.textCompletedAmt.Name = "textCompletedAmt";
			this.textCompletedAmt.Size = new System.Drawing.Size(85,20);
			this.textCompletedAmt.TabIndex = 58;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(435,611);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(84,24);
			this.butAdd.TabIndex = 54;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butClear.Autosize = true;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClear.CornerRadius = 4F;
			this.butClear.Location = new System.Drawing.Point(534,611);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(99,24);
			this.butClear.TabIndex = 53;
			this.butClear.Text = "Clear Schedule";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// butChangePlan
			// 
			this.butChangePlan.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChangePlan.Autosize = true;
			this.butChangePlan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChangePlan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChangePlan.CornerRadius = 4F;
			this.butChangePlan.Location = new System.Drawing.Point(354,166);
			this.butChangePlan.Name = "butChangePlan";
			this.butChangePlan.Size = new System.Drawing.Size(75,22);
			this.butChangePlan.TabIndex = 44;
			this.butChangePlan.Text = "C&hange";
			this.butChangePlan.Click += new System.EventHandler(this.butChangePlan_Click);
			// 
			// gridCharges
			// 
			this.gridCharges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridCharges.HScrollVisible = false;
			this.gridCharges.Location = new System.Drawing.Point(435,9);
			this.gridCharges.Name = "gridCharges";
			this.gridCharges.ScrollValue = 0;
			this.gridCharges.Size = new System.Drawing.Size(536,596);
			this.gridCharges.TabIndex = 41;
			this.gridCharges.Title = "Amortization Schedule";
			this.gridCharges.TranslationName = "PayPlanAmortization";
			this.gridCharges.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridCharges_CellDoubleClick);
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(22,528);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.PayPlan;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(392,121);
			this.textNote.TabIndex = 40;
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
			this.butDelete.Location = new System.Drawing.Point(22,660);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(84,24);
			this.butDelete.TabIndex = 38;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butGoToPat
			// 
			this.butGoToPat.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butGoToPat.Autosize = true;
			this.butGoToPat.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butGoToPat.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butGoToPat.CornerRadius = 4F;
			this.butGoToPat.Location = new System.Drawing.Point(354,9);
			this.butGoToPat.Name = "butGoToPat";
			this.butGoToPat.Size = new System.Drawing.Size(75,22);
			this.butGoToPat.TabIndex = 27;
			this.butGoToPat.Text = "&Go To";
			this.butGoToPat.Click += new System.EventHandler(this.butGoToPat_Click);
			// 
			// butGoToGuar
			// 
			this.butGoToGuar.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butGoToGuar.Autosize = true;
			this.butGoToGuar.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butGoToGuar.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butGoToGuar.CornerRadius = 4F;
			this.butGoToGuar.Location = new System.Drawing.Point(354,31);
			this.butGoToGuar.Name = "butGoToGuar";
			this.butGoToGuar.Size = new System.Drawing.Size(75,22);
			this.butGoToGuar.TabIndex = 23;
			this.butGoToGuar.Text = "Go &To";
			this.butGoToGuar.Click += new System.EventHandler(this.butGoTo_Click);
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(156,189);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(85,20);
			this.textDate.TabIndex = 7;
			// 
			// butChangeGuar
			// 
			this.butChangeGuar.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChangeGuar.Autosize = true;
			this.butChangeGuar.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChangeGuar.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChangeGuar.CornerRadius = 4F;
			this.butChangeGuar.Location = new System.Drawing.Point(354,53);
			this.butChangeGuar.Name = "butChangeGuar";
			this.butChangeGuar.Size = new System.Drawing.Size(75,22);
			this.butChangeGuar.TabIndex = 4;
			this.butChangeGuar.Text = "C&hange";
			this.butChangeGuar.Click += new System.EventHandler(this.butChangeGuar_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(787,660);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 1;
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
			this.butCancel.Location = new System.Drawing.Point(880,660);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textAPR
			// 
			this.textAPR.Location = new System.Drawing.Point(142,78);
			this.textAPR.Name = "textAPR";
			this.textAPR.Size = new System.Drawing.Size(47,20);
			this.textAPR.TabIndex = 15;
			// 
			// textPeriodPayment
			// 
			this.textPeriodPayment.Location = new System.Drawing.Point(133,39);
			this.textPeriodPayment.Name = "textPeriodPayment";
			this.textPeriodPayment.Size = new System.Drawing.Size(85,20);
			this.textPeriodPayment.TabIndex = 17;
			this.textPeriodPayment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textPeriodPayment_KeyPress);
			// 
			// textTerm
			// 
			this.textTerm.Location = new System.Drawing.Point(133,17);
			this.textTerm.MaxVal = 255;
			this.textTerm.MinVal = 0;
			this.textTerm.Name = "textTerm";
			this.textTerm.Size = new System.Drawing.Size(47,20);
			this.textTerm.TabIndex = 18;
			this.textTerm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTerm_KeyPress);
			// 
			// textDownPayment
			// 
			this.textDownPayment.Location = new System.Drawing.Point(142,56);
			this.textDownPayment.Name = "textDownPayment";
			this.textDownPayment.Size = new System.Drawing.Size(85,20);
			this.textDownPayment.TabIndex = 22;
			// 
			// textDateFirstPay
			// 
			this.textDateFirstPay.Location = new System.Drawing.Point(142,34);
			this.textDateFirstPay.Name = "textDateFirstPay";
			this.textDateFirstPay.Size = new System.Drawing.Size(85,20);
			this.textDateFirstPay.TabIndex = 13;
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(142,13);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(85,20);
			this.textAmount.TabIndex = 11;
			this.textAmount.Validating += new System.ComponentModel.CancelEventHandler(this.textAmount_Validating);
			// 
			// butCreateSched
			// 
			this.butCreateSched.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCreateSched.Autosize = true;
			this.butCreateSched.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCreateSched.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCreateSched.CornerRadius = 4F;
			this.butCreateSched.Location = new System.Drawing.Point(242,140);
			this.butCreateSched.Name = "butCreateSched";
			this.butCreateSched.Size = new System.Drawing.Size(99,24);
			this.butCreateSched.TabIndex = 42;
			this.butCreateSched.Text = "Create Schedule";
			this.butCreateSched.Click += new System.EventHandler(this.butCreateSched_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(563,660);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(85,24);
			this.butPrint.TabIndex = 20;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// FormPayPlan
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(974,698);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textCompletedAmt);
			this.Controls.Add(this.textPrincPaid);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClear);
			this.Controls.Add(this.checkIns);
			this.Controls.Add(this.butChangePlan);
			this.Controls.Add(this.textInsPlan);
			this.Controls.Add(this.labelInsPlan);
			this.Controls.Add(this.gridCharges);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textAccumulatedDue);
			this.Controls.Add(this.textAmtPaid);
			this.Controls.Add(this.butGoToPat);
			this.Controls.Add(this.textPatient);
			this.Controls.Add(this.butGoToGuar);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.butChangeGuar);
			this.Controls.Add(this.textGuarantor);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelGuarantor);
			this.Controls.Add(this.textTotalCost);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.butPrint);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPayPlan";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Payment Plan";
			this.Load += new System.EventHandler(this.FormPayPlan_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormPayPlan_Closing);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPayPlan_Load(object sender, System.EventArgs e) {
			textPatient.Text=Patients.GetLim(PayPlanCur.PatNum).GetNameLF();
			textGuarantor.Text=Patients.GetLim(PayPlanCur.Guarantor).GetNameLF();
			for(int i=0;i<ProviderC.List.Length;i++) {
				comboProv.Items.Add(ProviderC.List[i].GetLongDesc());
				if(IsNew && ProviderC.List[i].ProvNum==PatCur.PriProv) {//new payment plans default to pri prov
					comboProv.SelectedIndex=i;
				}
				//but if not new, then the provider will be selected in FillCharges().
			}
			if(PrefC.GetBool(PrefName.EasyNoClinics)) {
				labelClinic.Visible=false;
				comboClinic.Visible=false;
			}
			else {
				comboClinic.Items.Add("none");
				//we don't want to do this.  The -1 indicates to pull clinic from charges on first loop
				//comboClinic.SelectedIndex=0;//if not new, then clinic can be changed in the FillCharges()
				for(int i=0;i<Clinics.List.Length;i++) {
					comboClinic.Items.Add(Clinics.List[i].Description);
					if(IsNew && Clinics.List[i].ClinicNum==PatCur.ClinicNum) {//new payment plans default to pat clinic
						comboClinic.SelectedIndex=i+1;
					}
				}
			}
			textDate.Text=PayPlanCur.PayPlanDate.ToShortDateString();
			textAPR.Text=PayPlanCur.APR.ToString();
			AmtPaid=PayPlans.GetAmtPaid(PayPlanCur.PayPlanNum);
			textAmtPaid.Text=AmtPaid.ToString("n");
			textCompletedAmt.Text=PayPlanCur.CompletedAmt.ToString("n");
			textNote.Text=PayPlanCur.Note;
			if(PayPlanCur.PlanNum==0){
				labelInsPlan.Visible=false;
				textInsPlan.Visible=false;
				butChangePlan.Visible=false;
			}
			else{
				textInsPlan.Text=InsPlans.GetDescript(PayPlanCur.PlanNum,FamCur,InsPlanList);
				checkIns.Checked=true;
				labelGuarantor.Visible=false;
				textGuarantor.Visible=false;
				butGoToGuar.Visible=false;
				butChangeGuar.Visible=false;
			}
			FillCharges();
		}

		/// <summary>Called 5 times.  This also fills prov and clinic based on the first charge if not new.</summary>
		private void FillCharges(){
			table=AccountModules.GetPayPlanAmort(PayPlanCur.PayPlanNum).Tables["payplanamort"];
			gridCharges.BeginUpdate();
			gridCharges.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g("PayPlanAmortization","Date"),65,HorizontalAlignment.Right);
			gridCharges.Columns.Add(col);
			col=new ODGridColumn(Lan.g("PayPlanAmortization","Description"),220);
			gridCharges.Columns.Add(col);
			col=new ODGridColumn(Lan.g("PayPlanAmortization","Charges"),60,HorizontalAlignment.Right);
			gridCharges.Columns.Add(col);
			col=new ODGridColumn(Lan.g("PayPlanAmortization","Credits"),60,HorizontalAlignment.Right);
			gridCharges.Columns.Add(col);
			col=new ODGridColumn(Lan.g("PayPlanAmortization","Balance"),60,HorizontalAlignment.Right);
			gridCharges.Columns.Add(col);
			col=new ODGridColumn("",147);//filler
			gridCharges.Columns.Add(col);
			gridCharges.Rows.Clear();
			UI.ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["date"].ToString());
				row.Cells.Add(table.Rows[i]["description"].ToString());
				row.Cells.Add(table.Rows[i]["charges"].ToString());
				row.Cells.Add(table.Rows[i]["credits"].ToString());
				row.Cells.Add(table.Rows[i]["balance"].ToString());
				row.Cells.Add("");
				row.ColorText=Color.FromArgb(PIn.Int(table.Rows[i]["colorText"].ToString()));
				if(i<table.Rows.Count-1//not the last row
					&& ((DateTime)table.Rows[i]["DateTime"]).Date<=DateTime.Today
					&& ((DateTime)table.Rows[i+1]["DateTime"]).Date>DateTime.Today)
				{
					row.ColorLborder=Color.Black;
					row.Cells[4].Bold=YN.Yes;
				}
				gridCharges.Rows.Add(row);
			}
			//The code below is not very efficient, but it doesn't matter
			//List<PayPlanCharge> ChargeListAll=PayPlanCharges.Refresh(PayPlanCur.Guarantor);
			List<PayPlanCharge> ChargeList=PayPlanCharges.GetForPayPlan(PayPlanCur.PayPlanNum);
			TotPrinc=0;
			TotInt=0;
			for(int i=0;i<ChargeList.Count;i++){
				TotPrinc+=ChargeList[i].Principal;
				TotInt+=ChargeList[i].Interest;
			}
			TotPrincInt=TotPrinc+TotInt;
			if(IsNew && ChargeList.Count==0){
				textAmount.Text=TotalAmt.ToString("n");
			}
			else{
				textAmount.Text=TotPrinc.ToString("n");
			}
			textTotalCost.Text=TotPrincInt.ToString("n");
			if(ChargeList.Count>0){
				textDateFirstPay.Text=ChargeList[0].ChargeDate.ToShortDateString();
			}
			else{
				textDateFirstPay.Text="";
			}
			gridCharges.EndUpdate();
			textAccumulatedDue.Text=PayPlans.GetAccumDue(PayPlanCur.PayPlanNum,ChargeList).ToString("n");
			textPrincPaid.Text=PayPlans.GetPrincPaid(AmtPaid,PayPlanCur.PayPlanNum,ChargeList).ToString("n");
			if(!IsNew && ChargeList.Count>1) {
				if(comboProv.SelectedIndex==-1) {//This avoids resetting the combo every time FillCharges is run.
					comboProv.SelectedIndex=Providers.GetIndex(ChargeList[0].ProvNum);//could still be -1
				}
				if(!PrefC.GetBool(PrefName.EasyNoClinics) && comboClinic.SelectedIndex==-1) {
					if(ChargeList[0].ClinicNum==0){
						comboClinic.SelectedIndex=0;
					}
					else{
						comboClinic.SelectedIndex=Clinics.GetIndex(ChargeList[0].ClinicNum)+1;
					}
				}
			}
		}

		private void butGoToPat_Click(object sender, System.EventArgs e) {
			if(!SaveData()){
				return;
			}
			GotoPatNum=PayPlanCur.PatNum;
			DialogResult=DialogResult.OK;
		}

		private void butGoTo_Click(object sender, System.EventArgs e) {
			if(!SaveData()){
				return;
			}
			GotoPatNum=PayPlanCur.Guarantor;
			DialogResult=DialogResult.OK;
		}

		private void butChangeGuar_Click(object sender, System.EventArgs e) {
			if(PayPlans.GetAmtPaid(PayPlanCur.PayPlanNum)!=0){
				MsgBox.Show(this,"Not allowed to change the guarantor because payments are attached.");
				return;
			}
			if(table.Rows.Count>0){
				MsgBox.Show(this,"Not allowed to change the guarantor without first clearing the amortization schedule.");
				return;
			}
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.SelectionModeOnly=true;
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK){
				return;
			}
			PayPlanCur.Guarantor=FormPS.SelectedPatNum;
			textGuarantor.Text=Patients.GetLim(PayPlanCur.Guarantor).GetNameLF();
		}

		private void checkIns_Click(object sender, System.EventArgs e) {
			if(PayPlans.GetAmtPaid(PayPlanCur.PayPlanNum)!=0){
				MsgBox.Show(this,"Not allowed because payments are attached.");
				checkIns.Checked=!checkIns.Checked;
				return;
			}
			if(table.Rows.Count>0){
				MsgBox.Show(this,"Not allowed without first clearing the amortization schedule.");
				checkIns.Checked=!checkIns.Checked;
				return;
			}
			if(checkIns.Checked){
				FormInsPlanSelect FormI=new FormInsPlanSelect(PayPlanCur.PatNum);
				FormI.ShowDialog();
				if(FormI.DialogResult==DialogResult.Cancel){
					checkIns.Checked=false;
					return;
				}
				PayPlanCur.PlanNum=FormI.SelectedPlan.PlanNum;
				PayPlanCur.Guarantor=PayPlanCur.PatNum;
				textInsPlan.Text=InsPlans.GetDescript(PayPlanCur.PlanNum,FamCur,InsPlanList);
				labelGuarantor.Visible=false;
				textGuarantor.Visible=false;
				butGoToGuar.Visible=false;
				butChangeGuar.Visible=false;
				labelInsPlan.Visible=true;
				textInsPlan.Visible=true;
				butChangePlan.Visible=true;
			}
			else{//not insurance
				PayPlanCur.Guarantor=PayPlanCur.PatNum;
				textGuarantor.Text=Patients.GetLim(PayPlanCur.Guarantor).GetNameLF();
				PayPlanCur.PlanNum=0;
				labelGuarantor.Visible=true;
				textGuarantor.Visible=true;
				butGoToGuar.Visible=true;
				butChangeGuar.Visible=true;
				labelInsPlan.Visible=false;
				textInsPlan.Visible=false;
				butChangePlan.Visible=false;
			}
		}

		private void textAmount_Validating(object sender,CancelEventArgs e) {
			if(textCompletedAmt.Text!=""){
				if(MsgBox.Show(this,true,"Change Tx Completed Amt to match?")){
					textCompletedAmt.Text=textAmount.Text;
				}
			}
		}

		private void butChangePlan_Click(object sender, System.EventArgs e) {
			FormInsPlanSelect FormI=new FormInsPlanSelect(PayPlanCur.PatNum);
			FormI.ShowDialog();
			if(FormI.DialogResult==DialogResult.Cancel){
				return;
			}
			PayPlanCur.PlanNum=FormI.SelectedPlan.PlanNum;
			textInsPlan.Text=InsPlans.GetDescript(PayPlanCur.PlanNum,Patients.GetFamily(PayPlanCur.PatNum),new List <InsPlan> ());
		}

		private void textTerm_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			textPeriodPayment.Text="";
		}

		private void textPeriodPayment_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			textTerm.Text="";
		}

		private void butCreateSched_Click(object sender, System.EventArgs e) {
			//this is also where the terms get saved
			if(  textDate.errorProvider1.GetError(textDate)!=""
				|| textAmount.errorProvider1.GetError(textAmount)!=""
				|| textDateFirstPay.errorProvider1.GetError(textDateFirstPay)!=""
				|| textDownPayment.errorProvider1.GetError(textDownPayment)!=""
				|| textAPR.errorProvider1.GetError(textAPR)!=""
				|| textTerm.errorProvider1.GetError(textTerm)!=""
				|| textPeriodPayment.errorProvider1.GetError(textPeriodPayment)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(textAmount.Text=="" || PIn.Double(textAmount.Text)==0){
				MsgBox.Show(this,"Please enter an amount first.");
				return;
			}
			if(textDateFirstPay.Text==""){
				textDateFirstPay.Text=DateTime.Today.ToShortDateString();
			}
			if(textDownPayment.Text==""){
				textDownPayment.Text="0";
			}
			if(textAPR.Text==""){
				textAPR.Text="0";
			}
			if(textTerm.Text=="" && textPeriodPayment.Text==""){
				MsgBox.Show(this,"Please enter a term or payment amount first.");
				return;
			}
			if(textTerm.Text=="" && PIn.Double(textPeriodPayment.Text)==0){
				MsgBox.Show(this,"Payment cannot be 0.");
				return;
			}
			if(textPeriodPayment.Text=="" && PIn.Long(textTerm.Text)<1){
				MsgBox.Show(this,"Term cannot be less than 1.");
				return;
			}
			if(table.Rows.Count>0){
				if(!MsgBox.Show(this,true,"Replace existing amortization schedule?")){
					return;
				}
				PayPlanCharges.DeleteAllInPlan(PayPlanCur.PayPlanNum);
			}
			PayPlanCharge ppCharge;
			//down payment
			double downpayment=PIn.Double(textDownPayment.Text);
			if(downpayment!=0){
				ppCharge=new PayPlanCharge();
				ppCharge.PayPlanNum=PayPlanCur.PayPlanNum;
				ppCharge.Guarantor=PayPlanCur.Guarantor;
				ppCharge.PatNum=PayPlanCur.PatNum;
				ppCharge.ChargeDate=DateTime.Today;
				ppCharge.Interest=0;
				ppCharge.Principal=downpayment;
				ppCharge.Note=Lan.g(this,"Downpayment");
				ppCharge.ProvNum=PatCur.PriProv;//will be changed at the end.
				ppCharge.ClinicNum=PatCur.ClinicNum;//will be changed at the end.
				PayPlanCharges.Insert(ppCharge);
			}
			double principal=PIn.Double(textAmount.Text)-PIn.Double(textDownPayment.Text);
			double APR=PIn.Double(textAPR.Text);
			double periodRate;
			double periodPayment;
			if(APR==0){
				periodRate=0;
			}
			else{
				if(radioMonthly.Checked){
					periodRate=APR/100/12;
				}
				else{
					periodRate=APR/100/4;
				}
			}
			if(textTerm.Text!=""){//Use term to determine period payment
				double term=PIn.Double(textTerm.Text);
				if(APR==0){
					periodPayment=principal/term;
				}
				else{
					periodPayment=principal*periodRate/(1-Math.Pow(1+periodRate,-term));
				}
			}
			else{//Use period payment supplied
				periodPayment=PIn.Double(textPeriodPayment.Text);
			}
			double tempP=principal;//the principal which will be decreased to zero in the loop.  Includes many decimal places.
			double roundedP=principal;//This is used to make sure that last item can handle the .01 rounding error. 2 decimal places
			int roundDec=CultureInfo.CurrentCulture.NumberFormat.NumberDecimalDigits;
			DateTime firstDate=PIn.Date(textDateFirstPay.Text);
			int countCharges=0;
			while(tempP!=0 && countCharges<100){//the 100 limit prevents infinite loop
				ppCharge=new PayPlanCharge();
				ppCharge.PayPlanNum=PayPlanCur.PayPlanNum;
				ppCharge.Guarantor=PayPlanCur.Guarantor;
				ppCharge.PatNum=PayPlanCur.PatNum;
				if(radioMonthly.Checked){
					ppCharge.ChargeDate=firstDate.AddMonths(1*countCharges);
				}
				else{
					ppCharge.ChargeDate=firstDate.AddMonths(3*countCharges);
				}
				ppCharge.Interest=Math.Round((tempP*periodRate),roundDec);//2 decimals
				ppCharge.Principal=periodPayment-ppCharge.Interest;//many decimals, but same on each payment, so rounding not noticeable.
				ppCharge.ProvNum=PatCur.PriProv;
				if(tempP<-.03){//tempP is a significantly negative number, so this charge does not get added.
					//the negative amount instead gets subtracted from the previous charge entered.
					//List<PayPlanCharge> ChargeListAll=PayPlanCharges.Refresh(PayPlanCur.Guarantor);
					List<PayPlanCharge> ChargeList=PayPlanCharges.GetForPayPlan(PayPlanCur.PayPlanNum);
					ppCharge=ChargeList[ChargeList.Count-1].Copy();
					ppCharge.Principal+=tempP;
					PayPlanCharges.Update(ppCharge);
					break;
				}
				tempP-=ppCharge.Principal;  
				roundedP-=Math.Round(ppCharge.Principal,roundDec);
				if(tempP<.02 && tempP>-.02){//we are on the last loop since # so close to zero
					//We might alter this principal by a few cents to make them all match
					ppCharge.Principal+=roundedP;
					//and alter the interest by the opposite amount to keep the payment the same.
					//So in the end, the pennies got absorbed by changing the interest.
					if(APR!=0){
						ppCharge.Interest-=roundedP;
					}
					tempP=0;//this will prevent another loop
				}
				PayPlanCharges.Insert(ppCharge);
				countCharges++;
			}
			FillCharges();
			textNote.Text+=DateTime.Today.ToShortDateString()
				+" - Date of Agreement: "+textDate.Text
				+", Total Amount: "+textAmount.Text
				+", APR: "+textAPR.Text
				+", Total Cost of Loan: "+textTotalCost.Text;
		}

		private void gridCharges_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(table.Rows[e.Row]["PayPlanChargeNum"].ToString()!="0"){
				PayPlanCharge charge=PayPlanCharges.GetOne(PIn.Long(table.Rows[e.Row]["PayPlanChargeNum"].ToString()));
				FormPayPlanChargeEdit FormP=new FormPayPlanChargeEdit(charge);
				FormP.ShowDialog();
				if(FormP.DialogResult==DialogResult.Cancel){
					return;
				}
			}
			else if(table.Rows[e.Row]["PayNum"].ToString()!="0"){
				Payment pay=Payments.GetPayment(PIn.Long(table.Rows[e.Row]["PayNum"].ToString()));
				/*if(pay.PayType==0){//provider income transfer. I don't think this is possible, but you never know.
					FormProviderIncTrans FormPIT=new FormProviderIncTrans();
					FormPIT.PatNum=PatCur.PatNum;
					FormPIT.PaymentCur=pay;
					FormPIT.IsNew=false;
					FormPIT.ShowDialog();
					if(FormPIT.DialogResult==DialogResult.Cancel){
						return;
					}
				}
				else{*/
				FormPayment FormPayment2=new FormPayment(PatCur,FamCur,pay);
				FormPayment2.IsNew=false;
				FormPayment2.ShowDialog();
				if(FormPayment2.DialogResult==DialogResult.Cancel){
					return;
				}
				//}
			}
			FillCharges();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			PayPlanCharge ppCharge=new PayPlanCharge();
			ppCharge.PayPlanNum=PayPlanCur.PayPlanNum;
			ppCharge.Guarantor=PayPlanCur.Guarantor;
			ppCharge.ChargeDate=DateTime.Today;
			ppCharge.ProvNum=PatCur.PriProv;//will be changed at the end.
			ppCharge.ClinicNum=PatCur.ClinicNum;//will be changed at the end.
			FormPayPlanChargeEdit FormP=new FormPayPlanChargeEdit(ppCharge);
			FormP.IsNew=true;
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.Cancel){
				return;
			}
			FillCharges();
		}

		private void butClear_Click(object sender, System.EventArgs e) {
			if(!MsgBox.Show(this,true,"Clear all charges from amortization schedule?")){
				return;
			}
			PayPlanCharges.DeleteAllInPlan(PayPlanCur.PayPlanNum);
			FillCharges();
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			if(!SaveData()){
				return;
			}
			ReportLikeCrystal report=new ReportLikeCrystal();
			report.AddTitle("Payment Plan Terms");
			report.AddSubTitle(PrefC.GetString(PrefName.PracticeTitle));
			report.AddSubTitle(DateTime.Today.ToShortDateString());
			string sectName="Report Header";
			Section section=report.Sections["Report Header"];
			//int sectIndex=report.Sections.GetIndexOfKind(AreaSectionKind.ReportHeader);
			Size size=new Size(300,20);//big enough for any text
			Font font=new Font("Tahoma",9);
			ContentAlignment alignL=ContentAlignment.MiddleLeft;
			ContentAlignment alignR=ContentAlignment.MiddleRight;
			int yPos=140;
			int space=30;
			int x1=175;
			int x2=275;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Patient",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,textPatient.Text,font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Guarantor",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,textGuarantor.Text,font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Date of Agreement",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,PayPlanCur.PayPlanDate.ToString("d"),font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Principal",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,TotPrinc.ToString("n"),font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Annual Percentage Rate",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,PayPlanCur.APR.ToString("f1"),font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Total Finance Charges",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,TotInt.ToString("n"),font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Total Cost of Loan",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,TotPrincInt.ToString("n"),font,alignR));
			yPos+=space;
			section.Height=yPos+30;
			report.AddColumn("ChargeDate",80,FieldValueType.Date);
			//move the first column more to the middle
			report.GetLastRO(ReportObjectKind.TextObject).Location=new Point(150,0);
			report.GetLastRO(ReportObjectKind.TextObject).StaticText="Date";
			report.GetLastRO(ReportObjectKind.FieldObject).Location=new Point(150,0);
			report.AddColumn("Description",150,FieldValueType.String);
			report.AddColumn("Charges",70,FieldValueType.Number);
			report.AddColumn("Credits",70,FieldValueType.Number);
			report.AddColumn("Balance",70,FieldValueType.String);
			//report.AddColumn("Note",300,FieldValueType.String);
			//report.GetLastRO(ReportObjectKind.TextObject).Location=new Point(report.GetLastRO(ReportObjectKind.TextObject).Location.X+20,0);
			report.GetLastRO(ReportObjectKind.TextObject).TextAlign=ContentAlignment.MiddleRight;
			report.GetLastRO(ReportObjectKind.FieldObject).TextAlign=ContentAlignment.MiddleRight;
			//report.Query="SELECT ChargeDate,Principal,Interest,Principal+Interest,Note "
			//	+"FROM payplancharge WHERE PayPlanNum="+POut.PInt(PayPlanCur.PayPlanNum)
			//	+" ORDER BY ChargeDate";
			//if(!report.SubmitQuery()){
			//	return;
			//}
			//report.Sections[
			DataTable tbl=new DataTable();
			tbl.Columns.Add("date");
			tbl.Columns.Add("description");
			tbl.Columns.Add("charges");
			tbl.Columns.Add("credits");
			tbl.Columns.Add("balance");
			DataRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=tbl.NewRow();
				row["date"]=table.Rows[i]["date"].ToString();
				row["description"]=table.Rows[i]["description"].ToString();
				row["charges"]=table.Rows[i]["charges"].ToString();
				row["credits"]=table.Rows[i]["credits"].ToString();
				row["balance"]=table.Rows[i]["balance"].ToString();
				tbl.Rows.Add(row);
			}
			report.ReportTable=tbl;
			//yPos+=60;
			report.ReportObjects.Add(new ReportObject
				("Report Footer",new Point(x1,70),size,"Signature of Guarantor:",font,alignL));
			FormReportLikeCrystal FormR=new FormReportLikeCrystal(report);
			FormR.ShowDialog();
		}

		private void pd2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			int xPos=15;//starting pos
			int yPos=(int)27.5;//starting pos
			e.Graphics.DrawString("Payment Plan Truth in Lending Statement"
				,new Font("Arial",8),Brushes.Black,(float)xPos,(float)yPos);
      //e.Graphics.DrawImage(imageTemp,xPos,yPos);
		}

		///<summary></summary>
		private bool SaveData(){
			if(textDate.errorProvider1.GetError(textDate)!=""
				|| textAPR.errorProvider1.GetError(textAPR)!=""
				|| textCompletedAmt.errorProvider1.GetError(textCompletedAmt)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}
			if(table.Rows.Count==0) {
				MsgBox.Show(this,"An amortization schedule must be created first.");
				return false;
			}
			if(comboProv.SelectedIndex==-1) {
				MsgBox.Show(this,"A provider must be selected first.");
				return false;
			}
			if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
				if(comboClinic.SelectedIndex==-1) {
					MsgBox.Show(this,"A clinic must be selected first.");
					return false;
				}
			}
			if(textAPR.Text==""){
				textAPR.Text="0";
			}
			//PatNum not editable.
			//Guarantor set already
			PayPlanCur.PayPlanDate=PIn.Date(textDate.Text);
			PayPlanCur.APR=PIn.Double(textAPR.Text);
			PayPlanCur.Note=textNote.Text;
			PayPlanCur.CompletedAmt=PIn.Double(textCompletedAmt.Text);
			//PlanNum set already
			PayPlans.Update(PayPlanCur);//always saved to db before opening this form
			long provNum=ProviderC.List[comboProv.SelectedIndex].ProvNum;//already verified that there's a provider selected
			long clinicNum=0;
			if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
				if(comboClinic.SelectedIndex==0) {
					clinicNum=0;
				}
				else {
					clinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
				}
			}
			PayPlanCharges.SetProvAndClinic(PayPlanCur.PayPlanNum,provNum,clinicNum);
			return true;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(!MsgBox.Show(this,true,"Delete payment plan?")){
				return;
			}
			//later improvement if needed: possibly prevent deletion of some charges like older ones.
			try{
				PayPlans.Delete(PayPlanCur);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e){
			if(!SaveData()){
				return;
			}
      DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormPayPlan_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK){
				return;
			}
			if(IsNew){
				try{
					PayPlans.Delete(PayPlanCur);
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
					e.Cancel=true;
					return;
				}
			}
		}

		
		

		

		

		

		

		

		

		

		

		

		

		
		

		

		

		

		

		
	

		

		


	}
}





















