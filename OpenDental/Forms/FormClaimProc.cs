using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormClaimProcEdit.
	/// </summary>
	public class FormClaimProc : System.Windows.Forms.Form
	{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label label9;
		private OpenDental.ValidDouble textInsPayAmt;
		private System.Windows.Forms.TextBox textRemarks;
		private IContainer components;
		private OpenDental.ValidDouble textWriteOff;
		private OpenDental.ValidDouble textInsPayEst;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textInsPlan;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox checkNoBillIns;
		private System.Windows.Forms.TextBox textPercentage;
		private OpenDental.ValidDouble textCopayAmt;
		private OpenDental.ValidNumber textPercentOverride;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.TextBox textCodeSent;
		private OpenDental.ValidDouble textFeeBilled;
		private OpenDental.ValidDouble textDedApplied;
		private System.Windows.Forms.RadioButton radioEstimate;
		private System.Windows.Forms.RadioButton radioClaim;
		private OpenDental.ValidDate textDateCP;
		private OpenDental.ValidDouble textAllowedOverride;
		private OpenDental.ValidDouble textPaidOtherIns;
		private System.Windows.Forms.TextBox textFee;
		//public bool IsNew;
		///<summary>Set to true if this claimProc is accessed from within a claim or from within FormClaimPayTotal. This changes the behavior of the form, allowing more freedom with fields that are also totalled for entire claim.  This freedom is normally restricted so that claim totals will stay synchronized with individual claimprocs.  If true, it will still save changes to db, even though this is duplicated effort in FormClaimPayTotal.</summary>
		public bool IsInClaim;
		private System.Windows.Forms.Label labelDedApplied;
		private System.Windows.Forms.Panel panelEstimateInfo;
		private System.Windows.Forms.Label labelNotInClaim;
		private System.Windows.Forms.Label labelAttachedToCheck;
		private System.Windows.Forms.GroupBox groupClaimInfo;
		private System.Windows.Forms.Label labelInsPayAmt;
		private System.Windows.Forms.Label labelInsPayEst;
		private System.Windows.Forms.Label labelPaidOtherIns;
		private System.Windows.Forms.Label labelCopayAmt;
		private System.Windows.Forms.Label labelWriteOff;
		private System.Windows.Forms.Label labelFee;
		private System.Windows.Forms.Label labelCodeSent;
		private System.Windows.Forms.Label labelFeeBilled;
		private System.Windows.Forms.Label labelRemarks;
		///<summary>True if this is a procedure, and false if only a claim total.</summary>
		private bool IsProc;
		///<summary>Stores the procedure for this ClaimProc if applicable</summary>
		//private Procedure procCur;
		private ClaimProc ClaimProcCur;
		///<summary>If user hits cancel, then the claimproc is reset using this.</summary>
		private ClaimProc ClaimProcOld;
		private OpenDental.ValidDouble textCopayOverride;
		private System.Windows.Forms.Panel panelClaimExtras;
		///<summary>The procedure to which this claimproc is attached.</summary>
		private Procedure proc;
		private System.Windows.Forms.GroupBox groupClaim;
		private OpenDental.ValidDate textProcDate;
		private System.Windows.Forms.Label labelProcDate;
		private Family FamCur;
		private Patient PatCur;
		private List <InsPlan> PlanList;
		private System.Windows.Forms.Label labelCarrierAllowed;
		private System.Windows.Forms.TextBox textCarrierAllowed;
		private OpenDental.UI.Button butUpdateAllowed;
		///<summary>Set this to true if user does not have permission to edit procedure.</summary>
		public bool NoPermissionProc;
		private OpenDental.ValidDate textDateEntry;
		private System.Windows.Forms.Label labelDateEntry;
		private ToolTip toolTip1;
		private Label labelFeeSched;
		private Label label2;
		private TextBox textPPOFeeSched;
		private Label label7;
		private TextBox textSubstCode;
		private TextBox textFeeSched;
		private TextBox textAllowedFeeSched;
		private Label label8;
		private GroupBox groupAllowed;
		private Label label10;
		private ValidDouble textDedEstOverride;
		private ValidDouble textDedEst;
		private Label label11;
		private ValidDouble textInsEstTotal;
		private Label label17;
		private ValidDouble textInsEstTotalOverride;
		private ValidNumber textPaidOtherInsOverride;
		private ValidDouble textPatPortion1;
		private Label labelPatPortion1;
		private ValidDouble textPatPortion2;
		private Label labelPatPortion2;
		private ValidDouble textBaseEst;
		private Label label3;
		private Label label5;
		private TextBox textEstimateNote;
		private double CarrierAllowedAmount;
		private InsPlan Plan;
		private long PatPlanNum;
		private List<Benefit> BenefitList;
		private List<ClaimProcHist> HistList;
		private List<ClaimProcHist> LoopList;
		private List<PatPlan> PatPlanList;
		///<summary>This value is obtained by a query when this window first opens.  It only includes estimates, not actual payments or pending payments.  Will be 0 if this is a primary estimate.</summary>
		private double PaidOtherInsTotal;
		private ValidDouble textWriteOffEstOverride;
		private ValidDouble textWriteOffEst;
		private Label labelWriteOffEst;
		private double PaidOtherInsBaseEst;
		private TextBox textClinic;
		private Label labelClinic;
		private OpenDental.UI.Button butPickProv;
		private ComboBox comboProvider;
		private ComboBox comboStatus;
		///<summary>This value is obtained by a query when this window first opens.  It includes both actual writeoffs and estimated writeoffs.  Will be 0 if this is a primary estimate.</summary>
		private double WriteOffOtherIns;

		///<summary>procCur can be null if not editing from within an actual procedure.</summary>
		public FormClaimProc(ClaimProc claimProcCur,Procedure procCur,Family famCur,Patient patCur,List <InsPlan> planList,List<ClaimProcHist> histList,ref List<ClaimProcHist> loopList,List<PatPlan> patPlanList){
			ClaimProcCur=claimProcCur;
			ClaimProcOld=ClaimProcCur.Copy();
			proc=procCur;
			FamCur=famCur;
			PatCur=patCur;
			PlanList=planList;
			HistList=histList;
			LoopList=loopList;
			PatPlanList=patPlanList;
			InitializeComponent();// Required for Windows Form Designer support
			//can't use Lan.F because of complexity of label use
			Lan.C(this, new System.Windows.Forms.Control[]
				{
					this,
					this.label1,
					this.label9,
					this.label30,
					this.labelProcDate,
					this.label28,
					this.label29,
					this.groupClaim,
					this.radioEstimate,
					this.radioClaim,
					this.labelCodeSent,
					this.labelFeeBilled,
					this.labelRemarks,
					this.labelNotInClaim,
					this.checkNoBillIns,
					this.labelFee,
					this.labelCopayAmt,
					this.label4,
					this.groupClaimInfo,
					this.labelDedApplied,
					this.labelPaidOtherIns,
					this.labelInsPayEst,
					this.labelInsPayAmt,
					this.labelWriteOff,
					this.labelDateEntry
					//this.butRecalc
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butDelete,
			});
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClaimProc));
			this.labelInsPayAmt = new System.Windows.Forms.Label();
			this.labelRemarks = new System.Windows.Forms.Label();
			this.textRemarks = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.labelWriteOff = new System.Windows.Forms.Label();
			this.labelInsPayEst = new System.Windows.Forms.Label();
			this.labelNotInClaim = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textInsPlan = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textPercentage = new System.Windows.Forms.TextBox();
			this.labelCopayAmt = new System.Windows.Forms.Label();
			this.checkNoBillIns = new System.Windows.Forms.CheckBox();
			this.labelFee = new System.Windows.Forms.Label();
			this.textFee = new System.Windows.Forms.TextBox();
			this.label28 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.label30 = new System.Windows.Forms.Label();
			this.labelCodeSent = new System.Windows.Forms.Label();
			this.textCodeSent = new System.Windows.Forms.TextBox();
			this.labelFeeBilled = new System.Windows.Forms.Label();
			this.labelDedApplied = new System.Windows.Forms.Label();
			this.labelPaidOtherIns = new System.Windows.Forms.Label();
			this.groupClaim = new System.Windows.Forms.GroupBox();
			this.labelAttachedToCheck = new System.Windows.Forms.Label();
			this.radioClaim = new System.Windows.Forms.RadioButton();
			this.radioEstimate = new System.Windows.Forms.RadioButton();
			this.panelClaimExtras = new System.Windows.Forms.Panel();
			this.textFeeBilled = new OpenDental.ValidDouble();
			this.panelEstimateInfo = new System.Windows.Forms.Panel();
			this.textWriteOffEstOverride = new OpenDental.ValidDouble();
			this.textWriteOffEst = new OpenDental.ValidDouble();
			this.labelWriteOffEst = new System.Windows.Forms.Label();
			this.textEstimateNote = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBaseEst = new OpenDental.ValidDouble();
			this.label3 = new System.Windows.Forms.Label();
			this.textPatPortion1 = new OpenDental.ValidDouble();
			this.labelPatPortion1 = new System.Windows.Forms.Label();
			this.textPaidOtherInsOverride = new OpenDental.ValidNumber();
			this.textInsEstTotalOverride = new OpenDental.ValidDouble();
			this.textInsEstTotal = new OpenDental.ValidDouble();
			this.label17 = new System.Windows.Forms.Label();
			this.groupAllowed = new System.Windows.Forms.GroupBox();
			this.textAllowedOverride = new OpenDental.ValidDouble();
			this.label10 = new System.Windows.Forms.Label();
			this.textAllowedFeeSched = new System.Windows.Forms.TextBox();
			this.textSubstCode = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.butUpdateAllowed = new OpenDental.UI.Button();
			this.labelCarrierAllowed = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textCarrierAllowed = new System.Windows.Forms.TextBox();
			this.textPPOFeeSched = new System.Windows.Forms.TextBox();
			this.textDedEst = new OpenDental.ValidDouble();
			this.textPaidOtherIns = new OpenDental.ValidDouble();
			this.textFeeSched = new System.Windows.Forms.TextBox();
			this.labelFeeSched = new System.Windows.Forms.Label();
			this.textCopayOverride = new OpenDental.ValidDouble();
			this.label11 = new System.Windows.Forms.Label();
			this.textCopayAmt = new OpenDental.ValidDouble();
			this.textDedEstOverride = new OpenDental.ValidDouble();
			this.textPercentOverride = new OpenDental.ValidNumber();
			this.groupClaimInfo = new System.Windows.Forms.GroupBox();
			this.textPatPortion2 = new OpenDental.ValidDouble();
			this.labelPatPortion2 = new System.Windows.Forms.Label();
			this.textWriteOff = new OpenDental.ValidDouble();
			this.textInsPayEst = new OpenDental.ValidDouble();
			this.textInsPayAmt = new OpenDental.ValidDouble();
			this.textDedApplied = new OpenDental.ValidDouble();
			this.labelProcDate = new System.Windows.Forms.Label();
			this.labelDateEntry = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.textDateEntry = new OpenDental.ValidDate();
			this.textProcDate = new OpenDental.ValidDate();
			this.textDateCP = new OpenDental.ValidDate();
			this.butDelete = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textClinic = new System.Windows.Forms.TextBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.butPickProv = new OpenDental.UI.Button();
			this.comboProvider = new System.Windows.Forms.ComboBox();
			this.comboStatus = new System.Windows.Forms.ComboBox();
			this.groupClaim.SuspendLayout();
			this.panelClaimExtras.SuspendLayout();
			this.panelEstimateInfo.SuspendLayout();
			this.groupAllowed.SuspendLayout();
			this.groupClaimInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelInsPayAmt
			// 
			this.labelInsPayAmt.Location = new System.Drawing.Point(31,55);
			this.labelInsPayAmt.Name = "labelInsPayAmt";
			this.labelInsPayAmt.Size = new System.Drawing.Size(129,17);
			this.labelInsPayAmt.TabIndex = 13;
			this.labelInsPayAmt.Text = "Insurance Paid";
			this.labelInsPayAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelRemarks
			// 
			this.labelRemarks.Location = new System.Drawing.Point(14,48);
			this.labelRemarks.Name = "labelRemarks";
			this.labelRemarks.Size = new System.Drawing.Size(113,37);
			this.labelRemarks.TabIndex = 14;
			this.labelRemarks.Text = "Remarks from EOB";
			this.labelRemarks.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textRemarks
			// 
			this.textRemarks.Location = new System.Drawing.Point(129,49);
			this.textRemarks.MaxLength = 255;
			this.textRemarks.Multiline = true;
			this.textRemarks.Name = "textRemarks";
			this.textRemarks.Size = new System.Drawing.Size(290,129);
			this.textRemarks.TabIndex = 15;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(51,29);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80,17);
			this.label9.TabIndex = 16;
			this.label9.Text = "Status";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelWriteOff
			// 
			this.labelWriteOff.Location = new System.Drawing.Point(31,75);
			this.labelWriteOff.Name = "labelWriteOff";
			this.labelWriteOff.Size = new System.Drawing.Size(129,17);
			this.labelWriteOff.TabIndex = 19;
			this.labelWriteOff.Text = "Write Off";
			this.labelWriteOff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelInsPayEst
			// 
			this.labelInsPayEst.Location = new System.Drawing.Point(31,36);
			this.labelInsPayEst.Name = "labelInsPayEst";
			this.labelInsPayEst.Size = new System.Drawing.Size(129,17);
			this.labelInsPayEst.TabIndex = 21;
			this.labelInsPayEst.Text = "Insurance Estimate";
			this.labelInsPayEst.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelNotInClaim
			// 
			this.labelNotInClaim.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelNotInClaim.Location = new System.Drawing.Point(118,246);
			this.labelNotInClaim.Name = "labelNotInClaim";
			this.labelNotInClaim.Size = new System.Drawing.Size(331,17);
			this.labelNotInClaim.TabIndex = 26;
			this.labelNotInClaim.Text = "Changes can only be made from within the claim.";
			this.labelNotInClaim.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9,6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(121,14);
			this.label1.TabIndex = 28;
			this.label1.Text = "Ins Plan";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textInsPlan
			// 
			this.textInsPlan.Location = new System.Drawing.Point(133,4);
			this.textInsPlan.Name = "textInsPlan";
			this.textInsPlan.ReadOnly = true;
			this.textInsPlan.Size = new System.Drawing.Size(341,20);
			this.textInsPlan.TabIndex = 29;
			this.textInsPlan.Text = "An insurance plan";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(28,219);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(138,17);
			this.label4.TabIndex = 32;
			this.label4.Text = "Percentage %";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPercentage
			// 
			this.textPercentage.Location = new System.Drawing.Point(168,218);
			this.textPercentage.Name = "textPercentage";
			this.textPercentage.ReadOnly = true;
			this.textPercentage.Size = new System.Drawing.Size(70,20);
			this.textPercentage.TabIndex = 33;
			this.textPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// labelCopayAmt
			// 
			this.labelCopayAmt.Location = new System.Drawing.Point(28,179);
			this.labelCopayAmt.Name = "labelCopayAmt";
			this.labelCopayAmt.Size = new System.Drawing.Size(138,17);
			this.labelCopayAmt.TabIndex = 37;
			this.labelCopayAmt.Text = "Patient Copay";
			this.labelCopayAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkNoBillIns
			// 
			this.checkNoBillIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoBillIns.Location = new System.Drawing.Point(580,3);
			this.checkNoBillIns.Name = "checkNoBillIns";
			this.checkNoBillIns.Size = new System.Drawing.Size(270,22);
			this.checkNoBillIns.TabIndex = 40;
			this.checkNoBillIns.Text = "Do Not Bill to This Insurance";
			this.checkNoBillIns.Click += new System.EventHandler(this.checkNoBillIns_Click);
			// 
			// labelFee
			// 
			this.labelFee.Location = new System.Drawing.Point(59,7);
			this.labelFee.Name = "labelFee";
			this.labelFee.Size = new System.Drawing.Size(107,14);
			this.labelFee.TabIndex = 58;
			this.labelFee.Text = "Fee";
			this.labelFee.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFee
			// 
			this.textFee.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textFee.Location = new System.Drawing.Point(169,8);
			this.textFee.Name = "textFee";
			this.textFee.ReadOnly = true;
			this.textFee.Size = new System.Drawing.Size(58,13);
			this.textFee.TabIndex = 59;
			this.textFee.Text = "520.00";
			this.textFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(6,120);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(125,17);
			this.label28.TabIndex = 65;
			this.label28.Text = "Payment Date";
			this.label28.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(9,164);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(121,17);
			this.label29.TabIndex = 67;
			this.label29.Text = "Description";
			this.label29.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(133,160);
			this.textDescription.Name = "textDescription";
			this.textDescription.ReadOnly = true;
			this.textDescription.Size = new System.Drawing.Size(203,20);
			this.textDescription.TabIndex = 68;
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(58,53);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(73,17);
			this.label30.TabIndex = 69;
			this.label30.Text = "Provider";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelCodeSent
			// 
			this.labelCodeSent.Location = new System.Drawing.Point(8,10);
			this.labelCodeSent.Name = "labelCodeSent";
			this.labelCodeSent.Size = new System.Drawing.Size(121,14);
			this.labelCodeSent.TabIndex = 74;
			this.labelCodeSent.Text = "Code Sent to Ins";
			this.labelCodeSent.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCodeSent
			// 
			this.textCodeSent.Location = new System.Drawing.Point(129,7);
			this.textCodeSent.Name = "textCodeSent";
			this.textCodeSent.Size = new System.Drawing.Size(77,20);
			this.textCodeSent.TabIndex = 73;
			// 
			// labelFeeBilled
			// 
			this.labelFeeBilled.Location = new System.Drawing.Point(7,30);
			this.labelFeeBilled.Name = "labelFeeBilled";
			this.labelFeeBilled.Size = new System.Drawing.Size(121,17);
			this.labelFeeBilled.TabIndex = 71;
			this.labelFeeBilled.Text = "Fee Billed to Ins";
			this.labelFeeBilled.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelDedApplied
			// 
			this.labelDedApplied.Location = new System.Drawing.Point(31,16);
			this.labelDedApplied.Name = "labelDedApplied";
			this.labelDedApplied.Size = new System.Drawing.Size(129,17);
			this.labelDedApplied.TabIndex = 76;
			this.labelDedApplied.Text = "Deductible";
			this.labelDedApplied.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelPaidOtherIns
			// 
			this.labelPaidOtherIns.Location = new System.Drawing.Point(28,240);
			this.labelPaidOtherIns.Name = "labelPaidOtherIns";
			this.labelPaidOtherIns.Size = new System.Drawing.Size(138,17);
			this.labelPaidOtherIns.TabIndex = 79;
			this.labelPaidOtherIns.Text = "Paid By Other Ins";
			this.labelPaidOtherIns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupClaim
			// 
			this.groupClaim.Controls.Add(this.labelAttachedToCheck);
			this.groupClaim.Controls.Add(this.labelNotInClaim);
			this.groupClaim.Controls.Add(this.radioClaim);
			this.groupClaim.Controls.Add(this.radioEstimate);
			this.groupClaim.Controls.Add(this.panelClaimExtras);
			this.groupClaim.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupClaim.Location = new System.Drawing.Point(14,188);
			this.groupClaim.Name = "groupClaim";
			this.groupClaim.Size = new System.Drawing.Size(460,309);
			this.groupClaim.TabIndex = 90;
			this.groupClaim.TabStop = false;
			this.groupClaim.Text = "Claim";
			// 
			// labelAttachedToCheck
			// 
			this.labelAttachedToCheck.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelAttachedToCheck.Location = new System.Drawing.Point(118,270);
			this.labelAttachedToCheck.Name = "labelAttachedToCheck";
			this.labelAttachedToCheck.Size = new System.Drawing.Size(333,29);
			this.labelAttachedToCheck.TabIndex = 27;
			this.labelAttachedToCheck.Text = "This is attached to an insurance check, so certain changes are not allowed.";
			// 
			// radioClaim
			// 
			this.radioClaim.AutoCheck = false;
			this.radioClaim.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioClaim.Location = new System.Drawing.Point(100,33);
			this.radioClaim.Name = "radioClaim";
			this.radioClaim.Size = new System.Drawing.Size(353,18);
			this.radioClaim.TabIndex = 1;
			this.radioClaim.Text = "This is part of a claim.";
			this.radioClaim.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// radioEstimate
			// 
			this.radioEstimate.AutoCheck = false;
			this.radioEstimate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEstimate.Location = new System.Drawing.Point(100,10);
			this.radioEstimate.Name = "radioEstimate";
			this.radioEstimate.Size = new System.Drawing.Size(352,22);
			this.radioEstimate.TabIndex = 0;
			this.radioEstimate.Text = "This is an estimate only. It has not been attached to a claim.";
			// 
			// panelClaimExtras
			// 
			this.panelClaimExtras.Controls.Add(this.labelRemarks);
			this.panelClaimExtras.Controls.Add(this.textRemarks);
			this.panelClaimExtras.Controls.Add(this.labelCodeSent);
			this.panelClaimExtras.Controls.Add(this.textCodeSent);
			this.panelClaimExtras.Controls.Add(this.labelFeeBilled);
			this.panelClaimExtras.Controls.Add(this.textFeeBilled);
			this.panelClaimExtras.Location = new System.Drawing.Point(4,54);
			this.panelClaimExtras.Name = "panelClaimExtras";
			this.panelClaimExtras.Size = new System.Drawing.Size(438,188);
			this.panelClaimExtras.TabIndex = 97;
			// 
			// textFeeBilled
			// 
			this.textFeeBilled.Location = new System.Drawing.Point(129,28);
			this.textFeeBilled.Name = "textFeeBilled";
			this.textFeeBilled.Size = new System.Drawing.Size(77,20);
			this.textFeeBilled.TabIndex = 72;
			// 
			// panelEstimateInfo
			// 
			this.panelEstimateInfo.Controls.Add(this.textWriteOffEstOverride);
			this.panelEstimateInfo.Controls.Add(this.textWriteOffEst);
			this.panelEstimateInfo.Controls.Add(this.labelWriteOffEst);
			this.panelEstimateInfo.Controls.Add(this.textEstimateNote);
			this.panelEstimateInfo.Controls.Add(this.label5);
			this.panelEstimateInfo.Controls.Add(this.textBaseEst);
			this.panelEstimateInfo.Controls.Add(this.label3);
			this.panelEstimateInfo.Controls.Add(this.textPatPortion1);
			this.panelEstimateInfo.Controls.Add(this.labelPatPortion1);
			this.panelEstimateInfo.Controls.Add(this.textPaidOtherInsOverride);
			this.panelEstimateInfo.Controls.Add(this.textInsEstTotalOverride);
			this.panelEstimateInfo.Controls.Add(this.textInsEstTotal);
			this.panelEstimateInfo.Controls.Add(this.label17);
			this.panelEstimateInfo.Controls.Add(this.groupAllowed);
			this.panelEstimateInfo.Controls.Add(this.textDedEst);
			this.panelEstimateInfo.Controls.Add(this.textPaidOtherIns);
			this.panelEstimateInfo.Controls.Add(this.textFeeSched);
			this.panelEstimateInfo.Controls.Add(this.labelFeeSched);
			this.panelEstimateInfo.Controls.Add(this.labelPaidOtherIns);
			this.panelEstimateInfo.Controls.Add(this.textCopayOverride);
			this.panelEstimateInfo.Controls.Add(this.label11);
			this.panelEstimateInfo.Controls.Add(this.labelFee);
			this.panelEstimateInfo.Controls.Add(this.label4);
			this.panelEstimateInfo.Controls.Add(this.textPercentage);
			this.panelEstimateInfo.Controls.Add(this.textCopayAmt);
			this.panelEstimateInfo.Controls.Add(this.labelCopayAmt);
			this.panelEstimateInfo.Controls.Add(this.textDedEstOverride);
			this.panelEstimateInfo.Controls.Add(this.textFee);
			this.panelEstimateInfo.Controls.Add(this.textPercentOverride);
			this.panelEstimateInfo.Location = new System.Drawing.Point(476,25);
			this.panelEstimateInfo.Name = "panelEstimateInfo";
			this.panelEstimateInfo.Size = new System.Drawing.Size(411,403);
			this.panelEstimateInfo.TabIndex = 94;
			// 
			// textWriteOffEstOverride
			// 
			this.textWriteOffEstOverride.Location = new System.Drawing.Point(240,298);
			this.textWriteOffEstOverride.Name = "textWriteOffEstOverride";
			this.textWriteOffEstOverride.Size = new System.Drawing.Size(70,20);
			this.textWriteOffEstOverride.TabIndex = 137;
			this.textWriteOffEstOverride.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textWriteOffEstOverride.Leave += new System.EventHandler(this.textWriteOffEstOverride_Leave);
			// 
			// textWriteOffEst
			// 
			this.textWriteOffEst.Location = new System.Drawing.Point(168,298);
			this.textWriteOffEst.Name = "textWriteOffEst";
			this.textWriteOffEst.ReadOnly = true;
			this.textWriteOffEst.Size = new System.Drawing.Size(70,20);
			this.textWriteOffEst.TabIndex = 135;
			this.textWriteOffEst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// labelWriteOffEst
			// 
			this.labelWriteOffEst.Location = new System.Drawing.Point(28,301);
			this.labelWriteOffEst.Name = "labelWriteOffEst";
			this.labelWriteOffEst.Size = new System.Drawing.Size(138,17);
			this.labelWriteOffEst.TabIndex = 136;
			this.labelWriteOffEst.Text = "Write Off Estimate";
			this.labelWriteOffEst.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textEstimateNote
			// 
			this.textEstimateNote.Location = new System.Drawing.Point(168,338);
			this.textEstimateNote.MaxLength = 255;
			this.textEstimateNote.Multiline = true;
			this.textEstimateNote.Name = "textEstimateNote";
			this.textEstimateNote.ReadOnly = true;
			this.textEstimateNote.Size = new System.Drawing.Size(239,58);
			this.textEstimateNote.TabIndex = 134;
			this.textEstimateNote.Text = "Over annual max\r\nExclusions\r\nLimitations";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(27,341);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(138,17);
			this.label5.TabIndex = 133;
			this.label5.Text = "Estimate Note";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBaseEst
			// 
			this.textBaseEst.Location = new System.Drawing.Point(168,258);
			this.textBaseEst.Name = "textBaseEst";
			this.textBaseEst.ReadOnly = true;
			this.textBaseEst.Size = new System.Drawing.Size(70,20);
			this.textBaseEst.TabIndex = 132;
			this.textBaseEst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(5,260);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(161,17);
			this.label3.TabIndex = 131;
			this.label3.Text = "BaseEst (no max or deduct)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPatPortion1
			// 
			this.textPatPortion1.Location = new System.Drawing.Point(168,318);
			this.textPatPortion1.Name = "textPatPortion1";
			this.textPatPortion1.ReadOnly = true;
			this.textPatPortion1.Size = new System.Drawing.Size(70,20);
			this.textPatPortion1.TabIndex = 130;
			this.textPatPortion1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// labelPatPortion1
			// 
			this.labelPatPortion1.Location = new System.Drawing.Point(28,320);
			this.labelPatPortion1.Name = "labelPatPortion1";
			this.labelPatPortion1.Size = new System.Drawing.Size(138,17);
			this.labelPatPortion1.TabIndex = 129;
			this.labelPatPortion1.Text = "Estimated Patient Portion";
			this.labelPatPortion1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPaidOtherInsOverride
			// 
			this.textPaidOtherInsOverride.Location = new System.Drawing.Point(240,238);
			this.textPaidOtherInsOverride.MaxVal = 255;
			this.textPaidOtherInsOverride.MinVal = 0;
			this.textPaidOtherInsOverride.Name = "textPaidOtherInsOverride";
			this.textPaidOtherInsOverride.Size = new System.Drawing.Size(70,20);
			this.textPaidOtherInsOverride.TabIndex = 128;
			this.textPaidOtherInsOverride.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textPaidOtherInsOverride.Leave += new System.EventHandler(this.textPaidOtherInsOverride_Leave);
			// 
			// textInsEstTotalOverride
			// 
			this.textInsEstTotalOverride.Location = new System.Drawing.Point(240,278);
			this.textInsEstTotalOverride.Name = "textInsEstTotalOverride";
			this.textInsEstTotalOverride.Size = new System.Drawing.Size(70,20);
			this.textInsEstTotalOverride.TabIndex = 122;
			this.textInsEstTotalOverride.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textInsEstTotalOverride.Leave += new System.EventHandler(this.textInsEstTotalOverride_Leave);
			// 
			// textInsEstTotal
			// 
			this.textInsEstTotal.Location = new System.Drawing.Point(168,278);
			this.textInsEstTotal.Name = "textInsEstTotal";
			this.textInsEstTotal.ReadOnly = true;
			this.textInsEstTotal.Size = new System.Drawing.Size(70,20);
			this.textInsEstTotal.TabIndex = 119;
			this.textInsEstTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(28,281);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(138,17);
			this.label17.TabIndex = 120;
			this.label17.Text = "Insurance Estimate";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupAllowed
			// 
			this.groupAllowed.Controls.Add(this.textAllowedOverride);
			this.groupAllowed.Controls.Add(this.label10);
			this.groupAllowed.Controls.Add(this.textAllowedFeeSched);
			this.groupAllowed.Controls.Add(this.textSubstCode);
			this.groupAllowed.Controls.Add(this.label7);
			this.groupAllowed.Controls.Add(this.label8);
			this.groupAllowed.Controls.Add(this.butUpdateAllowed);
			this.groupAllowed.Controls.Add(this.labelCarrierAllowed);
			this.groupAllowed.Controls.Add(this.label2);
			this.groupAllowed.Controls.Add(this.textCarrierAllowed);
			this.groupAllowed.Controls.Add(this.textPPOFeeSched);
			this.groupAllowed.Location = new System.Drawing.Point(5,43);
			this.groupAllowed.Name = "groupAllowed";
			this.groupAllowed.Size = new System.Drawing.Size(388,132);
			this.groupAllowed.TabIndex = 112;
			this.groupAllowed.TabStop = false;
			this.groupAllowed.Text = "Carrier Allowed Amount";
			// 
			// textAllowedOverride
			// 
			this.textAllowedOverride.Location = new System.Drawing.Point(235,107);
			this.textAllowedOverride.Name = "textAllowedOverride";
			this.textAllowedOverride.Size = new System.Drawing.Size(70,20);
			this.textAllowedOverride.TabIndex = 30;
			this.textAllowedOverride.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textAllowedOverride.Leave += new System.EventHandler(this.textAllowedOverride_Leave);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(123,85);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(254,16);
			this.label10.TabIndex = 112;
			this.label10.Text = "Edit the allowed fee schedule for this code.";
			// 
			// textAllowedFeeSched
			// 
			this.textAllowedFeeSched.Location = new System.Drawing.Point(163,58);
			this.textAllowedFeeSched.Name = "textAllowedFeeSched";
			this.textAllowedFeeSched.ReadOnly = true;
			this.textAllowedFeeSched.Size = new System.Drawing.Size(219,20);
			this.textAllowedFeeSched.TabIndex = 111;
			// 
			// textSubstCode
			// 
			this.textSubstCode.Location = new System.Drawing.Point(163,14);
			this.textSubstCode.Name = "textSubstCode";
			this.textSubstCode.ReadOnly = true;
			this.textSubstCode.Size = new System.Drawing.Size(78,20);
			this.textSubstCode.TabIndex = 107;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(34,40);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(127,14);
			this.label7.TabIndex = 108;
			this.label7.Text = "PPO Fee Schedule";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(34,62);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(127,14);
			this.label8.TabIndex = 110;
			this.label8.Text = "Allowed Fee Schedule";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butUpdateAllowed
			// 
			this.butUpdateAllowed.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUpdateAllowed.Autosize = true;
			this.butUpdateAllowed.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUpdateAllowed.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUpdateAllowed.CornerRadius = 4F;
			this.butUpdateAllowed.Location = new System.Drawing.Point(13,81);
			this.butUpdateAllowed.Name = "butUpdateAllowed";
			this.butUpdateAllowed.Size = new System.Drawing.Size(101,22);
			this.butUpdateAllowed.TabIndex = 98;
			this.butUpdateAllowed.Text = "Edit Allowed Amt";
			this.toolTip1.SetToolTip(this.butUpdateAllowed,"Edit the fee schedule that holds the fee showing in the Carrier Allowed Amt box.");
			this.butUpdateAllowed.Click += new System.EventHandler(this.butUpdateAllowed_Click);
			// 
			// labelCarrierAllowed
			// 
			this.labelCarrierAllowed.Location = new System.Drawing.Point(34,110);
			this.labelCarrierAllowed.Name = "labelCarrierAllowed";
			this.labelCarrierAllowed.Size = new System.Drawing.Size(127,14);
			this.labelCarrierAllowed.TabIndex = 101;
			this.labelCarrierAllowed.Text = "Allowed Amt";
			this.labelCarrierAllowed.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(34,17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127,14);
			this.label2.TabIndex = 104;
			this.label2.Text = "Substitution Code";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCarrierAllowed
			// 
			this.textCarrierAllowed.Location = new System.Drawing.Point(163,107);
			this.textCarrierAllowed.Name = "textCarrierAllowed";
			this.textCarrierAllowed.ReadOnly = true;
			this.textCarrierAllowed.Size = new System.Drawing.Size(70,20);
			this.textCarrierAllowed.TabIndex = 102;
			this.textCarrierAllowed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPPOFeeSched
			// 
			this.textPPOFeeSched.Location = new System.Drawing.Point(163,36);
			this.textPPOFeeSched.Name = "textPPOFeeSched";
			this.textPPOFeeSched.ReadOnly = true;
			this.textPPOFeeSched.Size = new System.Drawing.Size(219,20);
			this.textPPOFeeSched.TabIndex = 109;
			// 
			// textDedEst
			// 
			this.textDedEst.Location = new System.Drawing.Point(168,198);
			this.textDedEst.Name = "textDedEst";
			this.textDedEst.ReadOnly = true;
			this.textDedEst.Size = new System.Drawing.Size(70,20);
			this.textDedEst.TabIndex = 113;
			this.textDedEst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPaidOtherIns
			// 
			this.textPaidOtherIns.Location = new System.Drawing.Point(168,238);
			this.textPaidOtherIns.Name = "textPaidOtherIns";
			this.textPaidOtherIns.ReadOnly = true;
			this.textPaidOtherIns.Size = new System.Drawing.Size(70,20);
			this.textPaidOtherIns.TabIndex = 78;
			this.textPaidOtherIns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textFeeSched
			// 
			this.textFeeSched.Location = new System.Drawing.Point(168,23);
			this.textFeeSched.Name = "textFeeSched";
			this.textFeeSched.ReadOnly = true;
			this.textFeeSched.Size = new System.Drawing.Size(219,20);
			this.textFeeSched.TabIndex = 106;
			// 
			// labelFeeSched
			// 
			this.labelFeeSched.Location = new System.Drawing.Point(39,26);
			this.labelFeeSched.Name = "labelFeeSched";
			this.labelFeeSched.Size = new System.Drawing.Size(127,14);
			this.labelFeeSched.TabIndex = 105;
			this.labelFeeSched.Text = "Fee Schedule";
			this.labelFeeSched.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCopayOverride
			// 
			this.textCopayOverride.Location = new System.Drawing.Point(240,178);
			this.textCopayOverride.Name = "textCopayOverride";
			this.textCopayOverride.Size = new System.Drawing.Size(70,20);
			this.textCopayOverride.TabIndex = 98;
			this.textCopayOverride.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textCopayOverride.Leave += new System.EventHandler(this.textCopayOverride_Leave);
			this.textCopayOverride.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textCopayOverride_KeyUp);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(28,199);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(138,17);
			this.label11.TabIndex = 114;
			this.label11.Text = "Deductible";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCopayAmt
			// 
			this.textCopayAmt.Location = new System.Drawing.Point(168,178);
			this.textCopayAmt.Name = "textCopayAmt";
			this.textCopayAmt.ReadOnly = true;
			this.textCopayAmt.Size = new System.Drawing.Size(70,20);
			this.textCopayAmt.TabIndex = 36;
			this.textCopayAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDedEstOverride
			// 
			this.textDedEstOverride.Location = new System.Drawing.Point(240,198);
			this.textDedEstOverride.Name = "textDedEstOverride";
			this.textDedEstOverride.Size = new System.Drawing.Size(70,20);
			this.textDedEstOverride.TabIndex = 116;
			this.textDedEstOverride.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textDedEstOverride.Leave += new System.EventHandler(this.textDedEstOverride_Leave);
			// 
			// textPercentOverride
			// 
			this.textPercentOverride.Location = new System.Drawing.Point(240,218);
			this.textPercentOverride.MaxVal = 255;
			this.textPercentOverride.MinVal = 0;
			this.textPercentOverride.Name = "textPercentOverride";
			this.textPercentOverride.Size = new System.Drawing.Size(70,20);
			this.textPercentOverride.TabIndex = 45;
			this.textPercentOverride.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textPercentOverride.Leave += new System.EventHandler(this.textPercentOverride_Leave);
			// 
			// groupClaimInfo
			// 
			this.groupClaimInfo.Controls.Add(this.textPatPortion2);
			this.groupClaimInfo.Controls.Add(this.labelPatPortion2);
			this.groupClaimInfo.Controls.Add(this.textWriteOff);
			this.groupClaimInfo.Controls.Add(this.textInsPayEst);
			this.groupClaimInfo.Controls.Add(this.labelInsPayEst);
			this.groupClaimInfo.Controls.Add(this.labelInsPayAmt);
			this.groupClaimInfo.Controls.Add(this.textInsPayAmt);
			this.groupClaimInfo.Controls.Add(this.textDedApplied);
			this.groupClaimInfo.Controls.Add(this.labelDedApplied);
			this.groupClaimInfo.Controls.Add(this.labelWriteOff);
			this.groupClaimInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupClaimInfo.Location = new System.Drawing.Point(481,429);
			this.groupClaimInfo.Name = "groupClaimInfo";
			this.groupClaimInfo.Size = new System.Drawing.Size(388,120);
			this.groupClaimInfo.TabIndex = 0;
			this.groupClaimInfo.TabStop = false;
			this.groupClaimInfo.Text = "Claim Info";
			// 
			// textPatPortion2
			// 
			this.textPatPortion2.Location = new System.Drawing.Point(163,93);
			this.textPatPortion2.Name = "textPatPortion2";
			this.textPatPortion2.ReadOnly = true;
			this.textPatPortion2.Size = new System.Drawing.Size(70,20);
			this.textPatPortion2.TabIndex = 132;
			this.textPatPortion2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// labelPatPortion2
			// 
			this.labelPatPortion2.Location = new System.Drawing.Point(23,95);
			this.labelPatPortion2.Name = "labelPatPortion2";
			this.labelPatPortion2.Size = new System.Drawing.Size(138,17);
			this.labelPatPortion2.TabIndex = 131;
			this.labelPatPortion2.Text = "Estimated Patient Portion";
			this.labelPatPortion2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textWriteOff
			// 
			this.textWriteOff.Location = new System.Drawing.Point(163,73);
			this.textWriteOff.Name = "textWriteOff";
			this.textWriteOff.Size = new System.Drawing.Size(70,20);
			this.textWriteOff.TabIndex = 18;
			this.textWriteOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textWriteOff.Leave += new System.EventHandler(this.textWriteOff_Leave);
			// 
			// textInsPayEst
			// 
			this.textInsPayEst.Location = new System.Drawing.Point(163,33);
			this.textInsPayEst.Name = "textInsPayEst";
			this.textInsPayEst.Size = new System.Drawing.Size(70,20);
			this.textInsPayEst.TabIndex = 20;
			this.textInsPayEst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textInsPayEst.Leave += new System.EventHandler(this.textInsPayEst_Leave);
			// 
			// textInsPayAmt
			// 
			this.textInsPayAmt.Location = new System.Drawing.Point(163,53);
			this.textInsPayAmt.Name = "textInsPayAmt";
			this.textInsPayAmt.Size = new System.Drawing.Size(70,20);
			this.textInsPayAmt.TabIndex = 0;
			this.textInsPayAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textInsPayAmt.Leave += new System.EventHandler(this.textInsPayAmt_Leave);
			// 
			// textDedApplied
			// 
			this.textDedApplied.Location = new System.Drawing.Point(163,13);
			this.textDedApplied.Name = "textDedApplied";
			this.textDedApplied.Size = new System.Drawing.Size(70,20);
			this.textDedApplied.TabIndex = 75;
			this.textDedApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textDedApplied.Leave += new System.EventHandler(this.textDedApplied_Leave);
			// 
			// labelProcDate
			// 
			this.labelProcDate.Location = new System.Drawing.Point(6,142);
			this.labelProcDate.Name = "labelProcDate";
			this.labelProcDate.Size = new System.Drawing.Size(126,17);
			this.labelProcDate.TabIndex = 96;
			this.labelProcDate.Text = "Procedure Date";
			this.labelProcDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelDateEntry
			// 
			this.labelDateEntry.Location = new System.Drawing.Point(6,99);
			this.labelDateEntry.Name = "labelDateEntry";
			this.labelDateEntry.Size = new System.Drawing.Size(125,17);
			this.labelDateEntry.TabIndex = 99;
			this.labelDateEntry.Text = "Pay Entry Date";
			this.labelDateEntry.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDateEntry
			// 
			this.textDateEntry.Location = new System.Drawing.Point(133,95);
			this.textDateEntry.Name = "textDateEntry";
			this.textDateEntry.ReadOnly = true;
			this.textDateEntry.Size = new System.Drawing.Size(83,20);
			this.textDateEntry.TabIndex = 100;
			// 
			// textProcDate
			// 
			this.textProcDate.Location = new System.Drawing.Point(133,138);
			this.textProcDate.Name = "textProcDate";
			this.textProcDate.Size = new System.Drawing.Size(83,20);
			this.textProcDate.TabIndex = 97;
			// 
			// textDateCP
			// 
			this.textDateCP.Location = new System.Drawing.Point(133,116);
			this.textDateCP.Name = "textDateCP";
			this.textDateCP.Size = new System.Drawing.Size(83,20);
			this.textDateCP.TabIndex = 66;
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
			this.butDelete.Location = new System.Drawing.Point(18,599);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(80,24);
			this.butDelete.TabIndex = 3;
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
			this.butCancel.Location = new System.Drawing.Point(802,599);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
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
			this.butOK.Location = new System.Drawing.Point(702,599);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textClinic
			// 
			this.textClinic.Location = new System.Drawing.Point(133,73);
			this.textClinic.Name = "textClinic";
			this.textClinic.ReadOnly = true;
			this.textClinic.Size = new System.Drawing.Size(148,20);
			this.textClinic.TabIndex = 102;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(9,75);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(121,14);
			this.labelClinic.TabIndex = 101;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butPickProv
			// 
			this.butPickProv.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPickProv.Autosize = false;
			this.butPickProv.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickProv.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickProv.CornerRadius = 2F;
			this.butPickProv.Location = new System.Drawing.Point(280,50);
			this.butPickProv.Name = "butPickProv";
			this.butPickProv.Size = new System.Drawing.Size(18,20);
			this.butPickProv.TabIndex = 160;
			this.butPickProv.Text = "...";
			this.butPickProv.Click += new System.EventHandler(this.butPickProv_Click);
			// 
			// comboProvider
			// 
			this.comboProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvider.FormattingEnabled = true;
			this.comboProvider.Location = new System.Drawing.Point(133,50);
			this.comboProvider.Name = "comboProvider";
			this.comboProvider.Size = new System.Drawing.Size(145,21);
			this.comboProvider.TabIndex = 159;
			// 
			// comboStatus
			// 
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.FormattingEnabled = true;
			this.comboStatus.Location = new System.Drawing.Point(133,26);
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(145,21);
			this.comboStatus.TabIndex = 161;
			this.comboStatus.SelectionChangeCommitted += new System.EventHandler(this.comboStatus_SelectionChangeCommitted);
			// 
			// FormClaimProc
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(889,632);
			this.Controls.Add(this.comboStatus);
			this.Controls.Add(this.butPickProv);
			this.Controls.Add(this.comboProvider);
			this.Controls.Add(this.textClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.textDateEntry);
			this.Controls.Add(this.labelDateEntry);
			this.Controls.Add(this.textProcDate);
			this.Controls.Add(this.labelProcDate);
			this.Controls.Add(this.groupClaim);
			this.Controls.Add(this.groupClaimInfo);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.textDateCP);
			this.Controls.Add(this.textInsPlan);
			this.Controls.Add(this.label30);
			this.Controls.Add(this.label29);
			this.Controls.Add(this.label28);
			this.Controls.Add(this.checkNoBillIns);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.panelEstimateInfo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimProc";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Claim Procedure";
			this.Load += new System.EventHandler(this.FormClaimProcEdit_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormClaimProc_Closing);
			this.groupClaim.ResumeLayout(false);
			this.panelClaimExtras.ResumeLayout(false);
			this.panelClaimExtras.PerformLayout();
			this.panelEstimateInfo.ResumeLayout(false);
			this.panelEstimateInfo.PerformLayout();
			this.groupAllowed.ResumeLayout(false);
			this.groupAllowed.PerformLayout();
			this.groupClaimInfo.ResumeLayout(false);
			this.groupClaimInfo.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClaimProcEdit_Load(object sender, System.EventArgs e) {
			if(NoPermissionProc){//blocks users with no permission to edit procedure
				butOK.Enabled=false;
				butDelete.Enabled=false;
			}
			if(ClaimProcCur.ClaimNum>0 && !Security.IsAuthorized(Permissions.ClaimSentEdit,true)) {//attached to claim, no permission for claims.
				butOK.Enabled=false;
				butDelete.Enabled=false;
			}
			Plan=InsPlans.GetPlan(ClaimProcCur.PlanNum,PlanList);
			PatPlanNum=PatPlans.GetPatPlanNum(PatPlanList,Plan.PlanNum);
			BenefitList=null;//only fill it if proc
			PaidOtherInsTotal=ClaimProcs.GetPaidOtherInsTotal(ClaimProcCur,PatPlanList);
			PaidOtherInsBaseEst=ClaimProcs.GetPaidOtherInsBaseEst(ClaimProcCur,PatPlanList);
			WriteOffOtherIns=ClaimProcs.GetWriteOffOtherIns(ClaimProcCur,PatPlanList);
			textInsPlan.Text=InsPlans.GetDescript(ClaimProcCur.PlanNum,FamCur,PlanList);
			checkNoBillIns.Checked=ClaimProcCur.NoBillIns;
			if(ClaimProcCur.ClaimPaymentNum>0){//attached to ins check
				textDateCP.ReadOnly=true;//DateCP always the same as the payment date and can't be changed here
				if(!Security.IsAuthorized(Permissions.InsPayEdit,ClaimProcCur.DateCP)){
					butOK.Enabled=false;
				}
				textInsPayAmt.ReadOnly=true;
				//listStatus.Enabled=false;//this is handled in the mousedown event
				butDelete.Enabled=false;
			}
			else{//otherwise, we do need to allow editing DateCP so that Writeoff dates can be changed.
				labelAttachedToCheck.Visible=false;
			}
			if(ClaimProcCur.ProcNum==0){//total payment for a claim
				IsProc=false;
				textDescription.Text="Total Payment";
				textProcDate.ReadOnly=false;
			}
			else{
				IsProc=true;
				BenefitList=Benefits.RefreshForPlan(ClaimProcCur.PlanNum,PatPlanNum);
				if(proc==null){
					proc=Procedures.GetOneProc(ClaimProcCur.ProcNum,false);
				}
				textDescription.Text=ProcedureCodes.GetProcCode(proc.CodeNum).Descript;
				textProcDate.ReadOnly=true;//user not allowed to edit ProcDate unless it's for a total payment
			}
			if(ClaimProcCur.ClaimNum>0){//attached to claim
				radioClaim.Checked=true;
				checkNoBillIns.Enabled=false;
				if(IsInClaim){//(not from the procedure window)
					labelNotInClaim.Visible=false;
				}
				else{//must be accessing it from the Procedure window
					textCodeSent.ReadOnly=true;
					textFeeBilled.ReadOnly=true;
					labelNotInClaim.Visible=true;
					textDedApplied.ReadOnly=true;
					textInsPayEst.ReadOnly=true;
					textInsPayAmt.ReadOnly=true;
					textWriteOff.ReadOnly=true;
				}
				groupClaimInfo.Visible=true;
				if(ClaimProcCur.ProcNum==0){//if a total entry rather than by proc
					panelEstimateInfo.Visible=false;
					//labelPatTotal.Visible=false;
					labelInsPayAmt.Font=new Font(labelInsPayAmt.Font,FontStyle.Bold);
					labelProcDate.Visible=false;
					textProcDate.Visible=false;
					labelCodeSent.Visible=false;
					textCodeSent.Visible=false;
					labelFeeBilled.Visible=false;
					textFeeBilled.Visible=false;
				}
				else if(ClaimProcCur.Status==ClaimProcStatus.Received){
					labelInsPayAmt.Font=new Font(labelInsPayAmt.Font,FontStyle.Bold);
				}
				//butOK.Enabled=false;
				//butDelete.Enabled=false;
				//MessageBox.Show(panelEstimateInfo.Visible.ToString());
			}
			else //not attached to a claim
				if(ClaimProcCur.PlanNum>0
				&& (ClaimProcCur.Status==ClaimProcStatus.CapEstimate
				|| ClaimProcCur.Status==ClaimProcStatus.CapComplete))
			{
				//InsPlans.Cur.PlanType=="c"){//capitation proc,whether Estimate or CapComplete,never billed to ins
				foreach(System.Windows.Forms.Control control in panelEstimateInfo.Controls){
					control.Visible=false;
				}
				foreach(System.Windows.Forms.Control control in groupClaimInfo.Controls){
					control.Visible=false;
				}
				groupClaimInfo.Text="";
				labelFee.Visible=true;
				textFee.Visible=true;
				labelCopayAmt.Visible=true;
				textCopayAmt.Visible=true;
				textCopayOverride.Visible=true;
				if(ClaimProcCur.Status==ClaimProcStatus.CapEstimate) {
					labelWriteOffEst.Visible=true;
					textWriteOffEst.Visible=true;
				}
				else {//capcomplete
					labelWriteOff.Visible=true;
					textWriteOff.Visible=true;
				}
				//labelPatTotal.Visible=true;
				groupClaim.Visible=false;
				labelNotInClaim.Visible=false;
			}
			else{//estimate
				groupClaimInfo.Visible=false;
				radioEstimate.Checked=true;
				labelNotInClaim.Visible=false;
				panelClaimExtras.Visible=false;
			}
			comboStatus.Items.Clear();
			comboStatus.Items.Add(Lan.g(this,"Estimate"));
			comboStatus.Items.Add(Lan.g(this,"Not Received"));
			comboStatus.Items.Add(Lan.g(this,"Received"));
			comboStatus.Items.Add(Lan.g(this,"PreAuthorization"));
			comboStatus.Items.Add(Lan.g(this,"Supplemental"));
			comboStatus.Items.Add(Lan.g(this,"CapClaim"));
			comboStatus.Items.Add(Lan.g(this,"CapEstimate"));
			comboStatus.Items.Add(Lan.g(this,"CapComplete"));
			SetComboStatus(ClaimProcCur.Status);
			if(ClaimProcCur.Status==ClaimProcStatus.Received || ClaimProcCur.Status==ClaimProcStatus.Supplemental){
				labelDateEntry.Visible=true;
				textDateEntry.Visible=true;
			}
			else{
				labelDateEntry.Visible=false;
				textDateEntry.Visible=false;
			}
			comboProvider.Items.Clear();
			for(int i=0;i<ProviderC.List.Length;i++){
				comboProvider.Items.Add(ProviderC.List[i].Abbr);
				if(ClaimProcCur.ProvNum==ProviderC.List[i].ProvNum){
					comboProvider.SelectedIndex=i;
				}
			}
			//this is not used, because the provider might simply be hidden. See bottom of page.
			//if(listProv.SelectedIndex==-1){
			//	listProv.SelectedIndex=0;//there should always be a provider
			//}
			if(PrefC.GetBool(PrefName.EasyNoClinics)) {
				labelClinic.Visible=false;
				textClinic.Visible=false;
			}
			else {
				textClinic.Text=Clinics.GetDesc(ClaimProcCur.ClinicNum);
			}
			textDateEntry.Text=ClaimProcCur.DateEntry.ToShortDateString();
			if(ClaimProcCur.ProcDate.Year<1880){
				textProcDate.Text="";
			}
			else{
				textProcDate.Text=ClaimProcCur.ProcDate.ToShortDateString();
			}
			if(ClaimProcCur.DateCP.Year<1880){
				textDateCP.Text="";
			}
			else{
				textDateCP.Text=ClaimProcCur.DateCP.ToShortDateString();
			}
			textCodeSent.Text=ClaimProcCur.CodeSent;
			textFeeBilled.Text=ClaimProcCur.FeeBilled.ToString("n");
			textRemarks.Text=ClaimProcCur.Remarks;
			FillInitialAmounts();
			ComputeAmounts();
			//MessageBox.Show(panelEstimateInfo.Visible.ToString());
		}

		private void SetComboStatus(ClaimProcStatus status){
			switch(status){
				case ClaimProcStatus.Estimate:
					comboStatus.SelectedIndex=0;
					break;
				case ClaimProcStatus.NotReceived:
					comboStatus.SelectedIndex=1;
					break;
				case ClaimProcStatus.Received:
					comboStatus.SelectedIndex=2;
					break;
				case ClaimProcStatus.Preauth:
					comboStatus.SelectedIndex=3;
					break;
				//adjustments have a completely different user interface. Cannot access from here.
				case ClaimProcStatus.Supplemental:
					comboStatus.SelectedIndex=4;
					break;
				case ClaimProcStatus.CapClaim:
					comboStatus.SelectedIndex=5;
					break;
				case ClaimProcStatus.CapEstimate:
					comboStatus.SelectedIndex=6;
					break;
				case ClaimProcStatus.CapComplete:
					comboStatus.SelectedIndex=7;
					break;
			}
		}

		///<summary>All text boxes will be blank before this is run.  It is only run once.</summary>
		private void FillInitialAmounts(){
			if(IsProc){
				textFee.Text=proc.ProcFee.ToString("f");
				InsPlan plan=InsPlans.GetPlan(ClaimProcCur.PlanNum,PlanList);
				long insFeeSch = Fees.GetFeeSched(PatCur, PlanList, PatPlanList);
				long standFeeSch = Providers.GetProv(Patients.GetProvNum(PatCur)).FeeSched;
				if(plan.PlanType=="p"){//if ppo
					double insFee = Fees.GetAmount0(proc.CodeNum, insFeeSch);
					double standardfee=Fees.GetAmount0(proc.CodeNum,Providers.GetProv(Patients.GetProvNum(PatCur)).FeeSched);
					if(insFee>standardfee) {//if ppo fee is higher than the standard fee (unusual)
						textFeeSched.Text=FeeScheds.GetDescription(insFeeSch);
					}
					else{// show the standard fee schedule
						textFeeSched.Text=FeeScheds.GetDescription(standFeeSch);
					}
				}
				else{//otherwise, show the plan fee schedule
					textFeeSched.Text=FeeScheds.GetDescription(insFeeSch);
				}
				string stringProcCode=ProcedureCodes.GetStringProcCode(proc.CodeNum);
				//int codeNum=proc.CodeNum;
				long substCodeNum=proc.CodeNum;
				if(!plan.CodeSubstNone){
					substCodeNum=ProcedureCodes.GetSubstituteCodeNum(stringProcCode,proc.ToothNum);//for posterior composites
				}
				if(proc.CodeNum!=substCodeNum) {
					textSubstCode.Text=ProcedureCodes.GetStringProcCode(substCodeNum);
				}
				if(plan.PlanType=="p"){//if ppo
					textPPOFeeSched.Text=FeeScheds.GetDescription(plan.FeeSched);
					textAllowedFeeSched.Text="---";
				}
				else{
					textPPOFeeSched.Text="---";
					if(plan.AllowedFeeSched!=0){
						textAllowedFeeSched.Text=FeeScheds.GetDescription(plan.AllowedFeeSched);
					}
					else{
						textAllowedFeeSched.Text="---";
					}
				}
			}
			else{//not a proc
				textFee.Text="";//because this textbox starts with a value just as a placeholder
				labelFeeSched.Visible=false;
				textFeeSched.Visible=false;
				groupAllowed.Visible=false;
			}
			FillAllowed();
			if(ClaimProcCur.AllowedOverride!=-1){
				textAllowedOverride.Text=ClaimProcCur.AllowedOverride.ToString("f");
			}
			if(ClaimProcCur.CopayAmt!=-1){
				textCopayAmt.Text=ClaimProcCur.CopayAmt.ToString("f");
			}
			if(ClaimProcCur.CopayOverride!=-1){
				textCopayOverride.Text=ClaimProcCur.CopayOverride.ToString("f");
			}
			if(ClaimProcCur.DedEst > 0) {
				textDedEst.Text=ClaimProcCur.DedEst.ToString("f");
			}
			if(ClaimProcCur.DedEstOverride!=-1) {
				textDedEstOverride.Text=ClaimProcCur.DedEstOverride.ToString("f");
			}
			if(ClaimProcCur.Percentage!=-1){
				textPercentage.Text=ClaimProcCur.Percentage.ToString();
			}
			if(ClaimProcCur.PercentOverride!=-1){
				textPercentOverride.Text=ClaimProcCur.PercentOverride.ToString();
			}
			if(ClaimProcCur.PaidOtherIns!=-1){
				textPaidOtherIns.Text=ClaimProcCur.PaidOtherIns.ToString("f");
			}
			if(ClaimProcCur.PaidOtherInsOverride!=-1) {
				textPaidOtherInsOverride.Text=ClaimProcCur.PaidOtherInsOverride.ToString("f");
			}
			textBaseEst.Text=ClaimProcCur.BaseEst.ToString("f");
			if(ClaimProcCur.InsEstTotal!=-1) {
				textInsEstTotal.Text=ClaimProcCur.InsEstTotal.ToString("f");
			}
			if(ClaimProcCur.InsEstTotalOverride!=-1) {
				textInsEstTotalOverride.Text=ClaimProcCur.InsEstTotalOverride.ToString("f");
			}
			if(ClaimProcCur.WriteOffEst!=-1) {
				textWriteOffEst.Text=ClaimProcCur.WriteOffEst.ToString("f");
			}
			if(ClaimProcCur.WriteOffEstOverride!=-1) {
				textWriteOffEstOverride.Text=ClaimProcCur.WriteOffEstOverride.ToString("f");
			}
			textDedApplied.Text=ClaimProcCur.DedApplied.ToString("f");
			textInsPayEst.Text=ClaimProcCur.InsPayEst.ToString("f");
			textInsPayAmt.Text=ClaimProcCur.InsPayAmt.ToString("f");
			textWriteOff.Text=ClaimProcCur.WriteOff.ToString("f");
		}

		///<summary>Fills the carrier allowed amount.  Called from FillInitialAmounts and from butUpdateAllowed_Click</summary>
		private void FillAllowed(){
			if(IsProc){
				InsPlan plan=InsPlans.GetPlan(ClaimProcCur.PlanNum,PlanList);
				CarrierAllowedAmount=InsPlans.GetAllowed(ProcedureCodes.GetStringProcCode(proc.CodeNum),plan.FeeSched,plan.AllowedFeeSched,
					plan.CodeSubstNone,plan.PlanType,proc.ToothNum,ClaimProcCur.ProvNum);
				if(CarrierAllowedAmount==-1){
					textCarrierAllowed.Text="";
				}
				else if(CarrierAllowedAmount > proc.ProcFee) {//if the Dr's UCR is lower than the Carrier's PPO allowed.
					textCarrierAllowed.Text=proc.ProcFee.ToString("f");
				}
				else {
					textCarrierAllowed.Text=CarrierAllowedAmount.ToString("f");
				}
			}
			else{
				textCarrierAllowed.Text="";
			}
		}

		private void butUpdateAllowed_Click(object sender, System.EventArgs e) {
			InsPlan plan=InsPlans.GetPlan(ClaimProcCur.PlanNum,PlanList);
			if(plan==null){
				//this should never happen
			}
			if(plan.AllowedFeeSched==0 && plan.PlanType!="p"){
				MsgBox.Show(this,"Plan must either be a PPO type or it must have an 'Allowed' fee schedule set.");
				return;
			}
			long feeSched=-1;
			if(plan.AllowedFeeSched!=0) {
				feeSched=plan.AllowedFeeSched;
			}
			else if(plan.PlanType=="p") {
				feeSched=plan.FeeSched;
			}
			if(FeeScheds.GetIsHidden(feeSched)){
				MsgBox.Show(this,"Allowed fee schedule is hidden, so no changes can be made.");
				return;
			}
			Fee FeeCur=Fees.GetFee(proc.CodeNum,feeSched);
			FormFeeEdit FormFE=new FormFeeEdit();
			if(FeeCur==null){
				FeeCur=new Fee();
				FeeCur.FeeSched=feeSched;
				FeeCur.CodeNum=proc.CodeNum;
				Fees.Insert(FeeCur);
				FormFE.IsNew=true;
			}
			FormFE.FeeCur=FeeCur;
			FormFE.ShowDialog();
			if(FormFE.DialogResult==DialogResult.OK){
				Fees.RefreshCache();
				DataValid.SetInvalid(InvalidType.Fees);
			}
			FillAllowed();
			ComputeAmounts();//?
		}

		private void ComputeAmounts(){
			if(!AllAreValid()){
				return;
			}
			ClaimProcCur.NoBillIns=checkNoBillIns.Checked;
			if(ClaimProcCur.Status==ClaimProcStatus.CapEstimate || ClaimProcCur.Status==ClaimProcStatus.CapComplete) {
				panelEstimateInfo.Visible=true;
				groupClaimInfo.Visible=true;
			}
			else if(checkNoBillIns.Checked) {
				panelEstimateInfo.Visible=false;
				groupClaimInfo.Visible=false;
				return;
			}
			else{
				if(ClaimProcCur.ProcNum!=0){//if a total payment, then this protects panel from inadvertently
						//being set visible again.  All other situations, it's based on NoBillIns
					panelEstimateInfo.Visible=true;
				}
				if(ClaimProcCur.ClaimNum>0) {//attached to claim
					groupClaimInfo.Visible=true;
				}
				else {
					groupClaimInfo.Visible=false;
				}
			}
			if(textAllowedOverride.Text=="") {
				ClaimProcCur.AllowedOverride=-1;
			}
			else {
				ClaimProcCur.AllowedOverride=PIn.Double(textAllowedOverride.Text);
			}
			if(textCopayOverride.Text=="") {
				ClaimProcCur.CopayOverride=-1;
			}
			else {
				ClaimProcCur.CopayOverride=PIn.Double(textCopayOverride.Text);
			}
			if(textDedEstOverride.Text=="") {
				ClaimProcCur.DedEstOverride=-1;
			}
			else {
				ClaimProcCur.DedEstOverride=PIn.Double(textDedEstOverride.Text);
			}
			if(textPercentOverride.Text=="") {
				ClaimProcCur.PercentOverride=-1;
			}
			else {
				ClaimProcCur.PercentOverride=PIn.Int(textPercentOverride.Text);
			}
			if(textPaidOtherInsOverride.Text=="") {
				ClaimProcCur.PaidOtherInsOverride=-1;
			}
			else {
				ClaimProcCur.PaidOtherInsOverride=PIn.Double(textPaidOtherInsOverride.Text);
			}
			if(textInsEstTotalOverride.Text=="") {
				ClaimProcCur.InsEstTotalOverride=-1;
			}
			else {
				ClaimProcCur.InsEstTotalOverride=PIn.Double(textInsEstTotalOverride.Text);
			}
			if(textWriteOffEstOverride.Text=="") {
				ClaimProcCur.WriteOffEstOverride=-1;
			}
			else {
				ClaimProcCur.WriteOffEstOverride=PIn.Double(textWriteOffEstOverride.Text);
			}
			if(IsProc) {
				ClaimProcs.ComputeBaseEst(ClaimProcCur,proc.ProcFee,proc.ToothNum,proc.CodeNum,Plan,PatPlanNum,BenefitList,
					HistList,LoopList,PatPlanList,PaidOtherInsTotal,PaidOtherInsBaseEst,PatCur.Age,WriteOffOtherIns);
				//Paid other ins is not accurate

			}
			//else {
			//	ClaimProcs.ComputeBaseEst(ClaimProcCur,0,"",0,Plan,PatPlanNum,BenefitList,HistList,LoopList);
			//}
			if(ClaimProcCur.CopayAmt == -1) {
				textCopayAmt.Text="";
			}
			else {
				textCopayAmt.Text=ClaimProcCur.CopayAmt.ToString("f");
			}
			if(ClaimProcCur.DedEst == -1) {
				textDedEst.Text="";
			}
			else {
				textDedEst.Text=ClaimProcCur.DedEst.ToString("f");
			}
			if(ClaimProcCur.Percentage == -1) {
				textPercentage.Text="";
			}
			else {
				textPercentage.Text=ClaimProcCur.Percentage.ToString("f0");
			}
			if(ClaimProcCur.PaidOtherIns == -1) {
				textPaidOtherIns.Text="";
			}
			else {
				textPaidOtherIns.Text=ClaimProcCur.PaidOtherIns.ToString("f");
			}
			textBaseEst.Text=ClaimProcCur.BaseEst.ToString("f");
			textInsEstTotal.Text=ClaimProcCur.InsEstTotal.ToString("f");
			if(ClaimProcCur.WriteOffEst==-1) {
				textWriteOffEst.Text="";
			}
			else {
				textWriteOffEst.Text=ClaimProcCur.WriteOffEst.ToString("f");
			}
			double patPortion=0;
			if(IsProc) {
				patPortion=proc.ProcFee;
				if(ClaimProcCur.InsEstTotalOverride != -1) {
					patPortion-=ClaimProcCur.InsEstTotalOverride;
				}
				else {
					patPortion-=ClaimProcCur.InsEstTotal;
				}
				if(ClaimProcCur.WriteOffEstOverride != -1) {
					patPortion-=ClaimProcCur.WriteOffEstOverride;
				}
				else if(ClaimProcCur.WriteOffEst != -1) {
					patPortion-=ClaimProcCur.WriteOffEst;
				}
				textPatPortion1.Text=patPortion.ToString("f");
			}
			textEstimateNote.Text=ClaimProcCur.EstimateNote;
			//insurance box---------------------------------------------------------------
			if(groupClaimInfo.Visible){
				ClaimProcCur.DedApplied=PIn.Double(textDedApplied.Text);
				ClaimProcCur.InsPayEst=PIn.Double(textInsPayEst.Text);
				ClaimProcCur.InsPayAmt=PIn.Double(textInsPayAmt.Text);
				ClaimProcCur.WriteOff=Math.Abs(PIn.Double(textWriteOff.Text));//if user enters a negative, convert it to a positive.
				//for PPO's the writeoff now replaces consideration of allowed fee
				if(IsProc) {
					if(ClaimProcCur.Status==ClaimProcStatus.NotReceived) {//not received.
						patPortion=proc.ProcFee-ClaimProcCur.InsPayEst-ClaimProcCur.WriteOff;
					}
					else if(ClaimProcCur.Status==ClaimProcStatus.CapEstimate || ClaimProcCur.Status==ClaimProcStatus.CapComplete) {
						patPortion=proc.ProcFee-ClaimProcCur.WriteOff;
					}
					else {
						patPortion=proc.ProcFee-ClaimProcCur.InsPayAmt-ClaimProcCur.WriteOff;
					}
					textPatPortion2.Text=patPortion.ToString("f");
					textPatPortion1.Visible=false;
				}
			}
		}

		private void butPickProv_Click(object sender,EventArgs e) {
			FormProviderPick formp=new FormProviderPick();
			if(comboProvider.SelectedIndex > -1) {
				formp.SelectedProvNum=ProviderC.List[comboProvider.SelectedIndex].ProvNum;
			}
			formp.ShowDialog();
			if(formp.DialogResult!=DialogResult.OK) {
				return;
			}
			comboProvider.SelectedIndex=Providers.GetIndex(formp.SelectedProvNum);
		}

		private void comboStatus_SelectionChangeCommitted(object sender,EventArgs e) {
			//MessageBox.Show(listStatus.SelectedIndex.ToString());
			//new selected index will already be set
			if(ClaimProcCur.Status!=ClaimProcStatus.Estimate//not an estimate
				&& comboStatus.SelectedIndex==0)//and clicked on estimate
			{
				SetComboStatus(ClaimProcCur.Status);//no change
				return;
			}
			if(ClaimProcCur.Status==ClaimProcStatus.Estimate) {//is an estimate
				SetComboStatus(ClaimProcCur.Status);//no change
				return;
			}
			if(ClaimProcCur.Status==ClaimProcStatus.CapComplete	|| ClaimProcCur.Status==ClaimProcStatus.CapEstimate) {//is a cap procedure
				SetComboStatus(ClaimProcCur.Status);//no change
				return;
			}
			if(ClaimProcCur.ClaimPaymentNum>0) {
				SetComboStatus(ClaimProcCur.Status);//no change
				return;
			}
			switch(comboStatus.SelectedIndex) {
				case 0:
					ClaimProcCur.Status=ClaimProcStatus.Estimate;
					break;
				case 1:
					ClaimProcCur.Status=ClaimProcStatus.NotReceived;
					break;
				case 2:
					ClaimProcCur.Status=ClaimProcStatus.Received;
					break;
				case 3:
					ClaimProcCur.Status=ClaimProcStatus.Preauth;
					break;
				case 4:
					ClaimProcCur.Status=ClaimProcStatus.Supplemental;
					break;
				case 5:
					ClaimProcCur.Status=ClaimProcStatus.CapClaim;
					break;
				case 6:
					ClaimProcCur.Status=ClaimProcStatus.CapEstimate;
					break;
				case 7:
					ClaimProcCur.Status=ClaimProcStatus.CapComplete;
					break;
			}
			if(ClaimProcCur.Status==ClaimProcStatus.Received || ClaimProcCur.Status==ClaimProcStatus.Supplemental) {
				labelDateEntry.Visible=true;
				textDateEntry.Visible=true;
			}
			else {
				labelDateEntry.Visible=false;
				textDateEntry.Visible=false;
			}
		}

		private void listStatus_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			
		}

		private void checkNoBillIns_Click(object sender, System.EventArgs e) {
			ComputeAmounts();
		}

		private void textAllowedOverride_Leave(object sender,System.EventArgs e) {
			ComputeAmounts();
		}

		private void textCopayOverride_Leave(object sender,System.EventArgs e) {
			ComputeAmounts();
		}

		private void textCopayOverride_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(ClaimProcCur.Status!=ClaimProcStatus.CapEstimate
				&& ClaimProcCur.Status!=ClaimProcStatus.CapComplete){
				return;
			}
			if(textCopayAmt.errorProvider1.GetError(textCopayOverride)!=""
				){
				return;
			}
			double copay=PIn.Double(textCopayOverride.Text);
			//always a procedure
			double writeoff=proc.ProcFee-copay;
			if(writeoff<0) {
				writeoff=0;
			}
			textWriteOff.Text=writeoff.ToString("n");
		}

		private void textPercentOverride_Leave(object sender, System.EventArgs e) {
			ComputeAmounts();
		}

		private void textDedEstOverride_Leave(object sender,EventArgs e) {
			ComputeAmounts();
		}

		private void textPaidOtherInsOverride_Leave(object sender,EventArgs e) {
			ComputeAmounts();
		}

		private void textInsEstTotalOverride_Leave(object sender,EventArgs e) {
			ComputeAmounts();
		}

		private void textDedApplied_Leave(object sender,System.EventArgs e) {
			ComputeAmounts();
		}

		private void textInsPayEst_Leave(object sender, System.EventArgs e) {
			ComputeAmounts();
		}

		private void textWriteOffEstOverride_Leave(object sender,EventArgs e) {
			ComputeAmounts();
		}

		private void textInsPayAmt_Leave(object sender, System.EventArgs e) {
			ComputeAmounts();
		}

		private void textWriteOff_Leave(object sender, System.EventArgs e) {
			ComputeAmounts();
		}

		///<summary>Remember that this will never even happen unless this is just an estimate because the delete button will not be enabled.</summary>
		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete this estimate?"),""
				,MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			ClaimProcs.Delete(ClaimProcCur);
			DialogResult=DialogResult.OK;
		}

		private bool AllAreValid(){
			//disallow negative writeoffs
			if(textWriteOffEstOverride.Text!="" && PIn.Double(textWriteOffEstOverride.Text)<0) {
				textWriteOffEstOverride.errorProvider1.SetError(textWriteOffEstOverride,"Write off must be a positive number.");
			}
			else {
				textWriteOffEstOverride.errorProvider1.SetError(textWriteOffEstOverride,"");
			}
			if(  textFeeBilled.errorProvider1.GetError(textFeeBilled)!=""
				|| textAllowedOverride.errorProvider1.GetError(textAllowedOverride)!=""
				|| textCopayOverride.errorProvider1.GetError(textCopayAmt)!=""
				|| textPercentOverride.errorProvider1.GetError(textPercentOverride)!=""
				|| textDedEstOverride.errorProvider1.GetError(textDedEstOverride)!=""
				|| textPaidOtherInsOverride.errorProvider1.GetError(textPaidOtherInsOverride)!=""
				|| textInsEstTotalOverride.errorProvider1.GetError(textInsEstTotalOverride)!=""
				|| textDedApplied.errorProvider1.GetError(textDedApplied)!=""
				|| textInsPayEst.errorProvider1.GetError(textInsPayEst)!=""
				|| textWriteOffEstOverride.errorProvider1.GetError(textWriteOffEstOverride)!=""
				|| textInsPayAmt.errorProvider1.GetError(textInsPayAmt)!=""
				|| textWriteOff.errorProvider1.GetError(textWriteOff)!=""
				|| textProcDate.errorProvider1.GetError(textProcDate)!=""
				|| textDateCP.errorProvider1.GetError(textDateCP)!=""
				){
				return false;
			}
			return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//no security check here because if attached to a payment, nobody is allowed to change the date or amount anyway.
			if(!AllAreValid()){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			//status already handled
			if(comboProvider.SelectedIndex!=-1) {//if no prov selected, then that prov must simply be hidden,
				//because all claimprocs are initially created with a prov(except preauth).
				//So, in this case, don't change.
				ClaimProcCur.ProvNum=ProviderC.List[comboProvider.SelectedIndex].ProvNum;
			}
			ClaimProcCur.ProcDate=PIn.Date(textProcDate.Text);
			if(!textDateCP.ReadOnly){
				ClaimProcCur.DateCP=PIn.Date(textDateCP.Text);
			}
			ClaimProcCur.CodeSent=textCodeSent.Text;
			ClaimProcCur.FeeBilled=PIn.Double(textFeeBilled.Text);
			ClaimProcCur.Remarks=textRemarks.Text;
			//if status was changed to received, then set DateEntry
			if(ClaimProcOld.Status!=ClaimProcStatus.Received && ClaimProcOld.Status!=ClaimProcStatus.Supplemental){
				if(ClaimProcCur.Status==ClaimProcStatus.Received || ClaimProcOld.Status==ClaimProcStatus.Supplemental){
					ClaimProcCur.DateEntry=DateTime.Now;
				}
			}
			ClaimProcs.Update(ClaimProcCur);
			//there is no functionality here for insert cur, because all claimprocs are
			//created before editing.
			if(ClaimProcCur.ClaimPaymentNum>0){//attached to ins check
				//note: the amount and the date will not have been changed.
				SecurityLogs.MakeLogEntry(Permissions.InsPayEdit,ClaimProcCur.PatNum,
					Patients.GetLim(ClaimProcCur.PatNum).GetNameLF()+", "
					+Lan.g(this,"Date and amount not changed."));//I'm really not sure what they would have changed.
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormClaimProc_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK){
				return;
			}
			ClaimProcCur=ClaimProcOld.Copy();//revert back to the old ClaimProc
		}

		

	

		

		

		

		


		
		

		

		

		

		

		

	}
}

















