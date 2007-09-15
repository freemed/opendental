using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
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
		private System.Windows.Forms.PrintDialog printDialog2;
		private System.Windows.Forms.Label label12;
		/// <summary>Go to the specified patnum.  Upon dialog close, if this number is not 0, then patients.Cur will be changed to this new patnum, and Account refreshed to the new patient.</summary>
		public int GotoPatNum;
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
		private InsPlan[] InsPlanList;
		private OpenDental.UI.ODGrid gridCharges;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textStartBal;
		private System.Windows.Forms.TextBox textTotPrinc;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textTotInt;
		private System.Windows.Forms.TextBox textTotPay;
		private OpenDental.UI.Button butClear;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.TextBox textAmtPaid;
		private System.Windows.Forms.TextBox textPrincPaid;
		private System.Windows.Forms.Label label14;
		private PayPlanCharge[] ChargeList;
		private double AmtPaid;

		///<summary>The supplied payment plan should already have been saved in the database.</summary>
		public FormPayPlan(Patient patCur,PayPlan payPlanCur){
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			PatCur=patCur.Copy();
			PayPlanCur=payPlanCur.Copy();
			FamCur=Patients.GetFamily(PatCur.PatNum);
			InsPlanList=InsPlans.Refresh(FamCur);
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
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.labelGuarantor = new System.Windows.Forms.Label();
			this.textGuarantor = new System.Windows.Forms.TextBox();
			this.butChangeGuar = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.label4 = new System.Windows.Forms.Label();
			this.textAmount = new OpenDental.ValidDouble();
			this.textDateFirstPay = new OpenDental.ValidDate();
			this.label5 = new System.Windows.Forms.Label();
			this.textAPR = new OpenDental.ValidDouble();
			this.label6 = new System.Windows.Forms.Label();
			this.textPeriodPayment = new OpenDental.ValidDouble();
			this.label7 = new System.Windows.Forms.Label();
			this.textTerm = new OpenDental.ValidNum();
			this.label8 = new System.Windows.Forms.Label();
			this.butPrint = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioQuarterly = new System.Windows.Forms.RadioButton();
			this.radioMonthly = new System.Windows.Forms.RadioButton();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textDownPayment = new OpenDental.ValidDouble();
			this.label11 = new System.Windows.Forms.Label();
			this.butCreateSched = new OpenDental.UI.Button();
			this.textTotalCost = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.butGoToGuar = new OpenDental.UI.Button();
			this.butGoToPat = new OpenDental.UI.Button();
			this.textPatient = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.label12 = new System.Windows.Forms.Label();
			this.textAmtPaid = new System.Windows.Forms.TextBox();
			this.textAccumulatedDue = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.textNote = new OpenDental.ODtextBox();
			this.gridCharges = new OpenDental.UI.ODGrid();
			this.textInsPlan = new System.Windows.Forms.TextBox();
			this.labelInsPlan = new System.Windows.Forms.Label();
			this.butChangePlan = new OpenDental.UI.Button();
			this.checkIns = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textStartBal = new System.Windows.Forms.TextBox();
			this.textTotPrinc = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textTotInt = new System.Windows.Forms.TextBox();
			this.textTotPay = new System.Windows.Forms.TextBox();
			this.butClear = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.textPrincPaid = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(880,589);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(787,589);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
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
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(21,128);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(133,17);
			this.label2.TabIndex = 5;
			this.label2.Text = "Date of Agreement";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(156,127);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(85,20);
			this.textDate.TabIndex = 7;
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
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(142,13);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(85,20);
			this.textAmount.TabIndex = 11;
			// 
			// textDateFirstPay
			// 
			this.textDateFirstPay.Location = new System.Drawing.Point(142,34);
			this.textDateFirstPay.Name = "textDateFirstPay";
			this.textDateFirstPay.Size = new System.Drawing.Size(85,20);
			this.textDateFirstPay.TabIndex = 13;
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
			// textAPR
			// 
			this.textAPR.Location = new System.Drawing.Point(142,78);
			this.textAPR.Name = "textAPR";
			this.textAPR.Size = new System.Drawing.Size(47,20);
			this.textAPR.TabIndex = 15;
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
			// textPeriodPayment
			// 
			this.textPeriodPayment.Location = new System.Drawing.Point(133,40);
			this.textPeriodPayment.Name = "textPeriodPayment";
			this.textPeriodPayment.Size = new System.Drawing.Size(85,20);
			this.textPeriodPayment.TabIndex = 17;
			this.textPeriodPayment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textPeriodPayment_KeyPress);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8,41);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(122,17);
			this.label7.TabIndex = 16;
			this.label7.Text = "Payment Amt";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(7,16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(124,17);
			this.label8.TabIndex = 19;
			this.label8.Text = "Number of Payments";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.butPrint.Location = new System.Drawing.Point(563,589);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(85,26);
			this.butPrint.TabIndex = 20;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
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
			this.groupBox2.Location = new System.Drawing.Point(14,148);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(355,178);
			this.groupBox2.TabIndex = 22;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Terms";
			// 
			// radioQuarterly
			// 
			this.radioQuarterly.Location = new System.Drawing.Point(243,121);
			this.radioQuarterly.Name = "radioQuarterly";
			this.radioQuarterly.Size = new System.Drawing.Size(104,18);
			this.radioQuarterly.TabIndex = 44;
			this.radioQuarterly.Text = "Quarterly";
			// 
			// radioMonthly
			// 
			this.radioMonthly.Checked = true;
			this.radioMonthly.Location = new System.Drawing.Point(243,103);
			this.radioMonthly.Name = "radioMonthly";
			this.radioMonthly.Size = new System.Drawing.Size(104,16);
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
			this.groupBox3.Size = new System.Drawing.Size(225,68);
			this.groupBox3.TabIndex = 23;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Either";
			// 
			// textDownPayment
			// 
			this.textDownPayment.Location = new System.Drawing.Point(142,56);
			this.textDownPayment.Name = "textDownPayment";
			this.textDownPayment.Size = new System.Drawing.Size(85,20);
			this.textDownPayment.TabIndex = 22;
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
			// butCreateSched
			// 
			this.butCreateSched.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCreateSched.Autosize = true;
			this.butCreateSched.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCreateSched.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCreateSched.CornerRadius = 4F;
			this.butCreateSched.Location = new System.Drawing.Point(242,143);
			this.butCreateSched.Name = "butCreateSched";
			this.butCreateSched.Size = new System.Drawing.Size(99,25);
			this.butCreateSched.TabIndex = 42;
			this.butCreateSched.Text = "Create Schedule";
			this.butCreateSched.Click += new System.EventHandler(this.butCreateSched_Click);
			// 
			// textTotalCost
			// 
			this.textTotalCost.Location = new System.Drawing.Point(156,330);
			this.textTotalCost.Name = "textTotalCost";
			this.textTotalCost.ReadOnly = true;
			this.textTotalCost.Size = new System.Drawing.Size(85,20);
			this.textTotalCost.TabIndex = 35;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(19,330);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(134,17);
			this.label15.TabIndex = 34;
			this.label15.Text = "Total Cost of Loan";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.label12.Location = new System.Drawing.Point(22,376);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(133,17);
			this.label12.TabIndex = 30;
			this.label12.Text = "Paid so far";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAmtPaid
			// 
			this.textAmtPaid.Location = new System.Drawing.Point(156,374);
			this.textAmtPaid.Name = "textAmtPaid";
			this.textAmtPaid.ReadOnly = true;
			this.textAmtPaid.Size = new System.Drawing.Size(85,20);
			this.textAmtPaid.TabIndex = 31;
			// 
			// textAccumulatedDue
			// 
			this.textAccumulatedDue.Location = new System.Drawing.Point(156,352);
			this.textAccumulatedDue.Name = "textAccumulatedDue";
			this.textAccumulatedDue.ReadOnly = true;
			this.textAccumulatedDue.Size = new System.Drawing.Size(85,20);
			this.textAccumulatedDue.TabIndex = 33;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(20,354);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(135,17);
			this.label13.TabIndex = 32;
			this.label13.Text = "Accumulated Due";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(23,424);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(148,17);
			this.label10.TabIndex = 37;
			this.label10.Text = "Note";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
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
			this.butDelete.Location = new System.Drawing.Point(22,589);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(84,26);
			this.butDelete.TabIndex = 38;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(22,445);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.PayPlan;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(392,121);
			this.textNote.TabIndex = 40;
			// 
			// gridCharges
			// 
			this.gridCharges.HScrollVisible = false;
			this.gridCharges.Location = new System.Drawing.Point(435,31);
			this.gridCharges.Name = "gridCharges";
			this.gridCharges.ScrollValue = 0;
			this.gridCharges.Size = new System.Drawing.Size(536,379);
			this.gridCharges.TabIndex = 41;
			this.gridCharges.Title = "Amortization Schedule";
			this.gridCharges.TranslationName = "PayPlanAmortization";
			this.gridCharges.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridCharges_CellDoubleClick);
			// 
			// textInsPlan
			// 
			this.textInsPlan.Location = new System.Drawing.Point(156,100);
			this.textInsPlan.Name = "textInsPlan";
			this.textInsPlan.ReadOnly = true;
			this.textInsPlan.Size = new System.Drawing.Size(199,20);
			this.textInsPlan.TabIndex = 43;
			// 
			// labelInsPlan
			// 
			this.labelInsPlan.Location = new System.Drawing.Point(21,100);
			this.labelInsPlan.Name = "labelInsPlan";
			this.labelInsPlan.Size = new System.Drawing.Size(132,17);
			this.labelInsPlan.TabIndex = 42;
			this.labelInsPlan.Text = "Insurance Plan";
			this.labelInsPlan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butChangePlan
			// 
			this.butChangePlan.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChangePlan.Autosize = true;
			this.butChangePlan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChangePlan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChangePlan.CornerRadius = 4F;
			this.butChangePlan.Location = new System.Drawing.Point(354,99);
			this.butChangePlan.Name = "butChangePlan";
			this.butChangePlan.Size = new System.Drawing.Size(75,22);
			this.butChangePlan.TabIndex = 44;
			this.butChangePlan.Text = "C&hange";
			this.butChangePlan.Click += new System.EventHandler(this.butChangePlan_Click);
			// 
			// checkIns
			// 
			this.checkIns.Location = new System.Drawing.Point(156,81);
			this.checkIns.Name = "checkIns";
			this.checkIns.Size = new System.Drawing.Size(268,18);
			this.checkIns.TabIndex = 46;
			this.checkIns.Text = "Use for tracking expected insurance payments";
			this.checkIns.Click += new System.EventHandler(this.checkIns_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(578,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(148,17);
			this.label1.TabIndex = 47;
			this.label1.Text = "Starting Balance";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textStartBal
			// 
			this.textStartBal.Location = new System.Drawing.Point(729,9);
			this.textStartBal.Name = "textStartBal";
			this.textStartBal.ReadOnly = true;
			this.textStartBal.Size = new System.Drawing.Size(63,20);
			this.textStartBal.TabIndex = 48;
			this.textStartBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textTotPrinc
			// 
			this.textTotPrinc.Location = new System.Drawing.Point(541,416);
			this.textTotPrinc.Name = "textTotPrinc";
			this.textTotPrinc.ReadOnly = true;
			this.textTotPrinc.Size = new System.Drawing.Size(60,20);
			this.textTotPrinc.TabIndex = 50;
			this.textTotPrinc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(449,416);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88,17);
			this.label3.TabIndex = 49;
			this.label3.Text = "Totals";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTotInt
			// 
			this.textTotInt.Location = new System.Drawing.Point(600,416);
			this.textTotInt.Name = "textTotInt";
			this.textTotInt.ReadOnly = true;
			this.textTotInt.Size = new System.Drawing.Size(60,20);
			this.textTotInt.TabIndex = 51;
			this.textTotInt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textTotPay
			// 
			this.textTotPay.Location = new System.Drawing.Point(659,416);
			this.textTotPay.Name = "textTotPay";
			this.textTotPay.ReadOnly = true;
			this.textTotPay.Size = new System.Drawing.Size(60,20);
			this.textTotPay.TabIndex = 52;
			this.textTotPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClear.Autosize = true;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClear.CornerRadius = 4F;
			this.butClear.Location = new System.Drawing.Point(634,453);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(99,26);
			this.butClear.TabIndex = 53;
			this.butClear.Text = "Clear Schedule";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
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
			this.butAdd.Location = new System.Drawing.Point(535,453);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(84,26);
			this.butAdd.TabIndex = 54;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// textPrincPaid
			// 
			this.textPrincPaid.Location = new System.Drawing.Point(156,396);
			this.textPrincPaid.Name = "textPrincPaid";
			this.textPrincPaid.ReadOnly = true;
			this.textPrincPaid.Size = new System.Drawing.Size(85,20);
			this.textPrincPaid.TabIndex = 56;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(22,398);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(133,17);
			this.label14.TabIndex = 55;
			this.label14.Text = "Principal paid so far";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormPayPlan
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(974,627);
			this.Controls.Add(this.textPrincPaid);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClear);
			this.Controls.Add(this.textTotPay);
			this.Controls.Add(this.textTotInt);
			this.Controls.Add(this.textTotPrinc);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textStartBal);
			this.Controls.Add(this.label1);
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
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormPayPlan_Closing);
			this.Load += new System.EventHandler(this.FormPayPlan_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPayPlan_Load(object sender, System.EventArgs e) {
			textPatient.Text=Patients.GetLim(PayPlanCur.PatNum).GetNameLF();
			textGuarantor.Text=Patients.GetLim(PayPlanCur.Guarantor).GetNameLF();
			textDate.Text=PayPlanCur.PayPlanDate.ToShortDateString();
			textAPR.Text=PayPlanCur.APR.ToString();
			AmtPaid=PayPlans.GetAmtPaid(PayPlanCur.PayPlanNum);
			textAmtPaid.Text=AmtPaid.ToString("n");
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

		private void FillCharges(){
			PayPlanCharge[] ChargeListAll=PayPlanCharges.Refresh(PayPlanCur.Guarantor);
			ChargeList=PayPlanCharges.GetForPayPlan(PayPlanCur.PayPlanNum,ChargeListAll);
			gridCharges.BeginUpdate();
			gridCharges.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("PayPlanAmortization","#"),25,HorizontalAlignment.Center);
			gridCharges.Columns.Add(col);
			col=new ODGridColumn(Lan.g("PayPlanAmortization","Date"),65,HorizontalAlignment.Right);
			gridCharges.Columns.Add(col);
			col=new ODGridColumn(Lan.g("PayPlanAmortization","Principal"),60,HorizontalAlignment.Right);
			gridCharges.Columns.Add(col);
			col=new ODGridColumn(Lan.g("PayPlanAmortization","Interest"),60,HorizontalAlignment.Right);
			gridCharges.Columns.Add(col);
			col=new ODGridColumn(Lan.g("PayPlanAmortization","Payment"),60,HorizontalAlignment.Right);
			gridCharges.Columns.Add(col);
			col=new ODGridColumn(Lan.g("PayPlanAmortization","Balance"),60,HorizontalAlignment.Right);
			gridCharges.Columns.Add(col);
			col=new ODGridColumn(Lan.g("PayPlanAmortization","Note"),147);
			gridCharges.Columns.Add(col);
			gridCharges.Rows.Clear();
			UI.ODGridRow row;
			//double totPrinc=0;
			double totInt=0;
			double bal=0;
			for(int i=0;i<ChargeList.Length;i++){
				//totPrinc+=ChargeList[i].Principal;
				totInt+=ChargeList[i].Interest;
				bal+=ChargeList[i].Principal;
			}
			if(IsNew && ChargeList.Length==0){
				textAmount.Text=TotalAmt.ToString("n");
			}
			else{
				textAmount.Text=bal.ToString("n");
			}
			textStartBal.Text=bal.ToString("n");
			textTotPrinc.Text=bal.ToString("n");
			textTotInt.Text=totInt.ToString("n");
			textTotPay.Text=(bal+totInt).ToString("n");
			textTotalCost.Text=(bal+totInt).ToString("n");
			if(ChargeList.Length>0){
				textDateFirstPay.Text=ChargeList[0].ChargeDate.ToShortDateString();
			}
			else{
				textDateFirstPay.Text="";
			}
			for(int i=0;i<ChargeList.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add((i+1).ToString());
				row.Cells.Add(ChargeList[i].ChargeDate.ToShortDateString());
				row.Cells.Add(ChargeList[i].Principal.ToString("n"));
				row.Cells.Add(ChargeList[i].Interest.ToString("n"));
				row.Cells.Add((ChargeList[i].Principal+ChargeList[i].Interest).ToString("n"));
				bal-=ChargeList[i].Principal;
				row.Cells.Add(bal.ToString("n"));
				row.Cells.Add(ChargeList[i].Note);
				//draw a dark line above this row if dates correct
				if(i>0 && ChargeList[i].ChargeDate > DateTime.Today && ChargeList[i-1].ChargeDate <= DateTime.Today){
					gridCharges.Rows[i-1].ColorLborder=Color.Black;
				}
				gridCharges.Rows.Add(row);
			}
			gridCharges.EndUpdate();
			textAccumulatedDue.Text=PayPlans.GetAccumDue(PayPlanCur.PayPlanNum,ChargeList).ToString("n");
			textPrincPaid.Text=PayPlans.GetPrincPaid(AmtPaid,PayPlanCur.PayPlanNum,ChargeList).ToString("n");
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
			if(ChargeList.Length>0){
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
			if(ChargeList.Length>0){
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

		private void butChangePlan_Click(object sender, System.EventArgs e) {
			FormInsPlanSelect FormI=new FormInsPlanSelect(PayPlanCur.PatNum);
			FormI.ShowDialog();
			if(FormI.DialogResult==DialogResult.Cancel){
				return;
			}
			PayPlanCur.PlanNum=FormI.SelectedPlan.PlanNum;
			textInsPlan.Text=InsPlans.GetDescript(PayPlanCur.PlanNum,Patients.GetFamily(PayPlanCur.PatNum),new InsPlan[] {});
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
			if(textAmount.Text=="" || PIn.PDouble(textAmount.Text)==0){
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
			if(textTerm.Text=="" && PIn.PDouble(textPeriodPayment.Text)==0){
				MsgBox.Show(this,"Payment cannot be 0.");
				return;
			}
			if(textPeriodPayment.Text=="" && PIn.PInt(textTerm.Text)<1){
				MsgBox.Show(this,"Term cannot be less than 1.");
				return;
			}
			if(ChargeList.Length>0){
				if(!MsgBox.Show(this,true,"Replace existing amortization schedule?")){
					return;
				}
				PayPlanCharges.DeleteAllInPlan(PayPlanCur.PayPlanNum);
			}
			PayPlanCharge ppCharge;
			//down payment
			double downpayment=PIn.PDouble(textDownPayment.Text);
			if(downpayment!=0){
				ppCharge=new PayPlanCharge();
				ppCharge.PayPlanNum=PayPlanCur.PayPlanNum;
				ppCharge.Guarantor=PayPlanCur.Guarantor;
				ppCharge.PatNum=PayPlanCur.PatNum;
				ppCharge.ChargeDate=DateTime.Today;
				ppCharge.Interest=0;
				ppCharge.Principal=downpayment;
				ppCharge.Note=Lan.g(this,"Downpayment");
				ppCharge.ProvNum=PatCur.PriProv;
				try{
					PayPlanCharges.InsertOrUpdate(ppCharge,true);
				}
				catch(ApplicationException ex){
					MessageBox.Show(ex.Message);
					return;
				}
			}
			double principal=PIn.PDouble(textAmount.Text)-PIn.PDouble(textDownPayment.Text);
			double APR=PIn.PDouble(textAPR.Text);
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
				double term=PIn.PDouble(textTerm.Text);
				if(APR==0){
					periodPayment=principal/term;
				}
				else{
					periodPayment=principal*periodRate/(1-Math.Pow(1+periodRate,-term));
				}
			}
			else{//Use period payment supplied
				periodPayment=PIn.PDouble(textPeriodPayment.Text);
			}
			double tempP=principal;//the principal which will be decreased to zero in the loop.  Includes many decimal places.
			double roundedP=principal;//This is used to make sure that last item can handle the .01 rounding error. 2 decimal places
			int roundDec=CultureInfo.CurrentCulture.NumberFormat.NumberDecimalDigits;
			DateTime firstDate=PIn.PDate(textDateFirstPay.Text);
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
				
				if(tempP<-.03){//tempP is a significantly negative number, so this charge does not get added.
					//the negative amount instead gets subtracted from the previous charge entered.
					PayPlanCharge[] ChargeListAll=PayPlanCharges.Refresh(PayPlanCur.Guarantor);
					ChargeList=PayPlanCharges.GetForPayPlan(PayPlanCur.PayPlanNum,ChargeListAll);
					ppCharge=ChargeList[ChargeList.Length-1].Copy();
					ppCharge.Principal+=tempP;
					try{
						PayPlanCharges.InsertOrUpdate(ppCharge,false);
					}
					catch(ApplicationException ex){
						MessageBox.Show(ex.Message);
					}
					break;
				}
				tempP-=ppCharge.Principal;  
				roundedP-=Math.Round(ppCharge.Principal,roundDec);
				if(tempP<.02 && tempP>-.02){//we are on the last loop since # so close to zero
					//We might alter this principal by a few cents to make them all match
					ppCharge.Principal+=roundedP;
					//and alter the interest by the opposite amount to keep the payment the same.
					//So in the end, the pennies got absorbed by changing the interest.
					ppCharge.Interest-=roundedP;
					tempP=0;//this will prevent another loop
				}
				try{
					PayPlanCharges.InsertOrUpdate(ppCharge,true);
				}
				catch(ApplicationException ex){
					MessageBox.Show(ex.Message);
					break;
				}
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
			FormPayPlanChargeEdit FormP=new FormPayPlanChargeEdit(ChargeList[e.Row]);
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.Cancel){
				return;
			}
			FillCharges();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			PayPlanCharge ppCharge=new PayPlanCharge();
			ppCharge.PayPlanNum=PayPlanCur.PayPlanNum;
			ppCharge.Guarantor=PayPlanCur.Guarantor;
			ppCharge.ChargeDate=DateTime.Today;
			ppCharge.ProvNum=PatCur.PriProv;
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
			ReportOld2 report=new ReportOld2();
			report.AddTitle("Payment Plan Terms");
			report.AddSubTitle(PrefB.GetString("PracticeTitle"));
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
				(sectName,new Point(x2,yPos),size,textTotPrinc.Text,font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Annual Percentage Rate",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,PayPlanCur.APR.ToString("f1"),font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Total Finance Charges",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,textTotInt.Text,font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Total Cost of Loan",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,textTotPay.Text,font,alignR));
			yPos+=space;
			section.Height=yPos+30;
			report.AddColumn("ChargeDate",80,FieldValueType.Date);
			//move the first column more to the middle
			report.GetLastRO(ReportObjectKind.TextObject).Location=new Point(175,0);
			report.GetLastRO(ReportObjectKind.TextObject).StaticText="Date";
			report.GetLastRO(ReportObjectKind.FieldObject).Location=new Point(175,0);
			report.AddColumn("Principal",70,FieldValueType.Number);
			report.AddColumn("Interest",70,FieldValueType.Number);
			report.AddColumn("Payment",70,FieldValueType.Number);
			//report.AddColumn("Balance  //no way to do running totals yet
			report.AddColumn("Note",300,FieldValueType.String);
			report.GetLastRO(ReportObjectKind.TextObject).Location=new Point(report.GetLastRO(ReportObjectKind.TextObject).Location.X+20,0);
			report.GetLastRO(ReportObjectKind.FieldObject).Location=new Point(report.GetLastRO(ReportObjectKind.FieldObject).Location.X+20,0);
			report.Query="SELECT ChargeDate,Principal,Interest,Principal+Interest,Note "
				+"FROM payplancharge WHERE PayPlanNum="+POut.PInt(PayPlanCur.PayPlanNum)
				+" ORDER BY ChargeDate";
			if(!report.SubmitQuery()){
				return;
			}
			//yPos+=60;
			report.ReportObjects.Add(new ReportObject
				("Report Footer",new Point(x1,70),size,"Signature of Guarantor:",font,alignL));
			FormReportOld2 FormR=new FormReportOld2(report);
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
				|| textAPR.errorProvider1.GetError(textAPR)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}
			if(textAPR.Text==""){
				textAPR.Text="0";
			}
			//PatNum not editable.
			//Guarantor set already
			PayPlanCur.PayPlanDate=PIn.PDate(textDate.Text);
			PayPlanCur.APR=PIn.PDouble(textAPR.Text);
			PayPlanCur.Note=textNote.Text;
			//PlanNum set already
			try{
				PayPlans.InsertOrUpdate(PayPlanCur,false);//always saved to db before opening this form
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return false;
			}
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
			if(ChargeList.Length==0){
				MsgBox.Show(this,"You must create an amortization schedule first.");
				return;
			}
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





















