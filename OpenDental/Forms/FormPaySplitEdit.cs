using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormPaySplitEdit.
	/// </summary>
	public class FormPaySplitEdit : System.Windows.Forms.Form
	{
		private OpenDental.UI.Button ButCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butRemainder;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ListBox listProvider;
		private System.Windows.Forms.ListBox listPatient;
		private System.Windows.Forms.Label labelAmount;
		private OpenDental.ValidDouble textAmount;
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.Label labelRemainder;
		///<summary>The value needed to make the splits balance.</summary>
		public double Remain;
		private System.Windows.Forms.CheckBox checkPayPlan;
		///<summary></summary>
		public PaySplit PaySplitCur;
		private OpenDental.UI.Button butDelete;
		private OpenDental.ValidDate textProcDate;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label1;
		private OpenDental.ValidDate textDatePay;
		private System.Windows.Forms.TextBox textPatient;
		private System.Windows.Forms.CheckBox checkPatOtherFam;
		private System.Windows.Forms.GroupBox groupPatient;
		private System.Windows.Forms.GroupBox groupProcedure;
		private OpenDental.UI.Button butAttach;
		private OpenDental.UI.Button butDetach;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textProcFee;
		private System.Windows.Forms.TextBox textProcInsPaid;
		private System.Windows.Forms.TextBox textProcInsEst;
		private System.Windows.Forms.TextBox textProcAdj;
		private System.Windows.Forms.TextBox textProcPrevPaid;
		private System.Windows.Forms.Label labelProcRemain;
		private System.Windows.Forms.TextBox textProcDate2;
		private System.Windows.Forms.TextBox textProcDescription;
		private System.Windows.Forms.TextBox textProcProv;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textProcTooth;
		private System.Windows.Forms.TextBox textProcPaidHere;
		//private Patient PatCur;
		private Family FamCur;
		//<summary>Used if changing the Patient from another family.</summary>
		//private int OriginalPatNum;
		private double ProcFee;
		private double ProcInsPaid;
		private double ProcInsEst;
		private double ProcAdj;
		private double ProcPrevPaid;
		private System.Windows.Forms.Label label15;
		private OpenDental.ValidDate textDateEntry;
		private double ProcPaidHere;


		///<summary></summary>
		public FormPaySplitEdit(Family famCur){//PaySplit paySplitCur,Family famCur){
			InitializeComponent();
			FamCur=famCur;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPaySplitEdit));
			this.ButCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butRemainder = new OpenDental.UI.Button();
			this.labelRemainder = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.listProvider = new System.Windows.Forms.ListBox();
			this.listPatient = new System.Windows.Forms.ListBox();
			this.textAmount = new OpenDental.ValidDouble();
			this.labelAmount = new System.Windows.Forms.Label();
			this.checkPayPlan = new System.Windows.Forms.CheckBox();
			this.butDelete = new OpenDental.UI.Button();
			this.textProcDate = new OpenDental.ValidDate();
			this.label7 = new System.Windows.Forms.Label();
			this.textDatePay = new OpenDental.ValidDate();
			this.label1 = new System.Windows.Forms.Label();
			this.textPatient = new System.Windows.Forms.TextBox();
			this.checkPatOtherFam = new System.Windows.Forms.CheckBox();
			this.groupPatient = new System.Windows.Forms.GroupBox();
			this.groupProcedure = new System.Windows.Forms.GroupBox();
			this.textProcTooth = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.textProcProv = new System.Windows.Forms.TextBox();
			this.textProcDescription = new System.Windows.Forms.TextBox();
			this.textProcDate2 = new System.Windows.Forms.TextBox();
			this.labelProcRemain = new System.Windows.Forms.Label();
			this.textProcPaidHere = new System.Windows.Forms.TextBox();
			this.textProcPrevPaid = new System.Windows.Forms.TextBox();
			this.textProcAdj = new System.Windows.Forms.TextBox();
			this.textProcInsEst = new System.Windows.Forms.TextBox();
			this.textProcInsPaid = new System.Windows.Forms.TextBox();
			this.textProcFee = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butDetach = new OpenDental.UI.Button();
			this.butAttach = new OpenDental.UI.Button();
			this.textDateEntry = new OpenDental.ValidDate();
			this.label15 = new System.Windows.Forms.Label();
			this.groupPatient.SuspendLayout();
			this.groupProcedure.SuspendLayout();
			this.SuspendLayout();
			// 
			// ButCancel
			// 
			this.ButCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.ButCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButCancel.Autosize = true;
			this.ButCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.ButCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.ButCancel.CornerRadius = 4F;
			this.ButCancel.Location = new System.Drawing.Point(660,496);
			this.ButCancel.Name = "ButCancel";
			this.ButCancel.Size = new System.Drawing.Size(75,26);
			this.ButCancel.TabIndex = 6;
			this.ButCancel.Text = "&Cancel";
			this.ButCancel.Click += new System.EventHandler(this.ButCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(660,466);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 5;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butRemainder
			// 
			this.butRemainder.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRemainder.Autosize = true;
			this.butRemainder.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRemainder.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRemainder.CornerRadius = 4F;
			this.butRemainder.Location = new System.Drawing.Point(5,304);
			this.butRemainder.Name = "butRemainder";
			this.butRemainder.Size = new System.Drawing.Size(92,26);
			this.butRemainder.TabIndex = 7;
			this.butRemainder.Text = "&Remainder";
			this.butRemainder.Visible = false;
			this.butRemainder.Click += new System.EventHandler(this.butRemainder_Click);
			// 
			// labelRemainder
			// 
			this.labelRemainder.Location = new System.Drawing.Point(5,336);
			this.labelRemainder.Name = "labelRemainder";
			this.labelRemainder.Size = new System.Drawing.Size(119,88);
			this.labelRemainder.TabIndex = 5;
			this.labelRemainder.Text = "The Remainder button will calculate the value needed to make the splits balance.";
			this.labelRemainder.Visible = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(245,9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(95,16);
			this.label5.TabIndex = 10;
			this.label5.Text = "Provider";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listProvider
			// 
			this.listProvider.Location = new System.Drawing.Point(246,26);
			this.listProvider.Name = "listProvider";
			this.listProvider.Size = new System.Drawing.Size(92,108);
			this.listProvider.TabIndex = 2;
			// 
			// listPatient
			// 
			this.listPatient.Location = new System.Drawing.Point(11,34);
			this.listPatient.Name = "listPatient";
			this.listPatient.Size = new System.Drawing.Size(192,108);
			this.listPatient.TabIndex = 3;
			this.listPatient.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listPatient_MouseDown);
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(129,94);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(77,20);
			this.textAmount.TabIndex = 1;
			this.textAmount.Validating += new System.ComponentModel.CancelEventHandler(this.textAmount_Validating);
			// 
			// labelAmount
			// 
			this.labelAmount.Location = new System.Drawing.Point(23,96);
			this.labelAmount.Name = "labelAmount";
			this.labelAmount.Size = new System.Drawing.Size(104,16);
			this.labelAmount.TabIndex = 15;
			this.labelAmount.Text = "Amount";
			this.labelAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkPayPlan
			// 
			this.checkPayPlan.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPayPlan.Location = new System.Drawing.Point(255,497);
			this.checkPayPlan.Name = "checkPayPlan";
			this.checkPayPlan.Size = new System.Drawing.Size(198,18);
			this.checkPayPlan.TabIndex = 20;
			this.checkPayPlan.Text = "Attached to Payment Plan";
			this.checkPayPlan.Click += new System.EventHandler(this.checkPayPlan_Click);
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
			this.butDelete.Location = new System.Drawing.Point(48,496);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(85,26);
			this.butDelete.TabIndex = 21;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textProcDate
			// 
			this.textProcDate.Location = new System.Drawing.Point(129,70);
			this.textProcDate.Name = "textProcDate";
			this.textProcDate.Size = new System.Drawing.Size(92,20);
			this.textProcDate.TabIndex = 25;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(24,73);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104,16);
			this.label7.TabIndex = 24;
			this.label7.Text = "Split Date";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDatePay
			// 
			this.textDatePay.Location = new System.Drawing.Point(129,46);
			this.textDatePay.Name = "textDatePay";
			this.textDatePay.ReadOnly = true;
			this.textDatePay.Size = new System.Drawing.Size(92,20);
			this.textDatePay.TabIndex = 22;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0,48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(127,16);
			this.label1.TabIndex = 23;
			this.label1.Text = "Payment Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPatient
			// 
			this.textPatient.Location = new System.Drawing.Point(11,33);
			this.textPatient.Name = "textPatient";
			this.textPatient.Size = new System.Drawing.Size(238,20);
			this.textPatient.TabIndex = 111;
			// 
			// checkPatOtherFam
			// 
			this.checkPatOtherFam.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPatOtherFam.Location = new System.Drawing.Point(11,15);
			this.checkPatOtherFam.Name = "checkPatOtherFam";
			this.checkPatOtherFam.Size = new System.Drawing.Size(192,17);
			this.checkPatOtherFam.TabIndex = 110;
			this.checkPatOtherFam.TabStop = false;
			this.checkPatOtherFam.Text = "Is from another family";
			this.checkPatOtherFam.Click += new System.EventHandler(this.checkPatOtherFam_Click);
			// 
			// groupPatient
			// 
			this.groupPatient.Controls.Add(this.listPatient);
			this.groupPatient.Controls.Add(this.textPatient);
			this.groupPatient.Controls.Add(this.checkPatOtherFam);
			this.groupPatient.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupPatient.Location = new System.Drawing.Point(356,8);
			this.groupPatient.Name = "groupPatient";
			this.groupPatient.Size = new System.Drawing.Size(265,157);
			this.groupPatient.TabIndex = 112;
			this.groupPatient.TabStop = false;
			this.groupPatient.Text = "Patient";
			// 
			// groupProcedure
			// 
			this.groupProcedure.Controls.Add(this.textProcTooth);
			this.groupProcedure.Controls.Add(this.label14);
			this.groupProcedure.Controls.Add(this.textProcProv);
			this.groupProcedure.Controls.Add(this.textProcDescription);
			this.groupProcedure.Controls.Add(this.textProcDate2);
			this.groupProcedure.Controls.Add(this.labelProcRemain);
			this.groupProcedure.Controls.Add(this.textProcPaidHere);
			this.groupProcedure.Controls.Add(this.textProcPrevPaid);
			this.groupProcedure.Controls.Add(this.textProcAdj);
			this.groupProcedure.Controls.Add(this.textProcInsEst);
			this.groupProcedure.Controls.Add(this.textProcInsPaid);
			this.groupProcedure.Controls.Add(this.textProcFee);
			this.groupProcedure.Controls.Add(this.label13);
			this.groupProcedure.Controls.Add(this.label12);
			this.groupProcedure.Controls.Add(this.label11);
			this.groupProcedure.Controls.Add(this.label10);
			this.groupProcedure.Controls.Add(this.label9);
			this.groupProcedure.Controls.Add(this.label8);
			this.groupProcedure.Controls.Add(this.label6);
			this.groupProcedure.Controls.Add(this.label4);
			this.groupProcedure.Controls.Add(this.label3);
			this.groupProcedure.Controls.Add(this.label2);
			this.groupProcedure.Controls.Add(this.butDetach);
			this.groupProcedure.Controls.Add(this.butAttach);
			this.groupProcedure.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupProcedure.Location = new System.Drawing.Point(130,208);
			this.groupProcedure.Name = "groupProcedure";
			this.groupProcedure.Size = new System.Drawing.Size(559,225);
			this.groupProcedure.TabIndex = 113;
			this.groupProcedure.TabStop = false;
			this.groupProcedure.Text = "Procedure";
			// 
			// textProcTooth
			// 
			this.textProcTooth.Location = new System.Drawing.Point(115,95);
			this.textProcTooth.Name = "textProcTooth";
			this.textProcTooth.ReadOnly = true;
			this.textProcTooth.Size = new System.Drawing.Size(43,20);
			this.textProcTooth.TabIndex = 46;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(9,98);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(104,16);
			this.label14.TabIndex = 45;
			this.label14.Text = "Tooth";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textProcProv
			// 
			this.textProcProv.Location = new System.Drawing.Point(115,75);
			this.textProcProv.Name = "textProcProv";
			this.textProcProv.ReadOnly = true;
			this.textProcProv.Size = new System.Drawing.Size(76,20);
			this.textProcProv.TabIndex = 44;
			// 
			// textProcDescription
			// 
			this.textProcDescription.Location = new System.Drawing.Point(115,115);
			this.textProcDescription.Name = "textProcDescription";
			this.textProcDescription.ReadOnly = true;
			this.textProcDescription.Size = new System.Drawing.Size(241,20);
			this.textProcDescription.TabIndex = 43;
			// 
			// textProcDate2
			// 
			this.textProcDate2.Location = new System.Drawing.Point(115,55);
			this.textProcDate2.Name = "textProcDate2";
			this.textProcDate2.ReadOnly = true;
			this.textProcDate2.Size = new System.Drawing.Size(76,20);
			this.textProcDate2.TabIndex = 42;
			// 
			// labelProcRemain
			// 
			this.labelProcRemain.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelProcRemain.Location = new System.Drawing.Point(462,175);
			this.labelProcRemain.Name = "labelProcRemain";
			this.labelProcRemain.Size = new System.Drawing.Size(73,18);
			this.labelProcRemain.TabIndex = 41;
			this.labelProcRemain.Text = "$90.00";
			this.labelProcRemain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textProcPaidHere
			// 
			this.textProcPaidHere.Location = new System.Drawing.Point(461,148);
			this.textProcPaidHere.Name = "textProcPaidHere";
			this.textProcPaidHere.ReadOnly = true;
			this.textProcPaidHere.Size = new System.Drawing.Size(76,20);
			this.textProcPaidHere.TabIndex = 40;
			this.textProcPaidHere.Text = "0.00";
			this.textProcPaidHere.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textProcPrevPaid
			// 
			this.textProcPrevPaid.Location = new System.Drawing.Point(461,128);
			this.textProcPrevPaid.Name = "textProcPrevPaid";
			this.textProcPrevPaid.ReadOnly = true;
			this.textProcPrevPaid.Size = new System.Drawing.Size(76,20);
			this.textProcPrevPaid.TabIndex = 39;
			this.textProcPrevPaid.Text = "0.00";
			this.textProcPrevPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textProcAdj
			// 
			this.textProcAdj.Location = new System.Drawing.Point(461,108);
			this.textProcAdj.Name = "textProcAdj";
			this.textProcAdj.ReadOnly = true;
			this.textProcAdj.Size = new System.Drawing.Size(76,20);
			this.textProcAdj.TabIndex = 38;
			this.textProcAdj.Text = "10.00";
			this.textProcAdj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textProcInsEst
			// 
			this.textProcInsEst.Location = new System.Drawing.Point(461,88);
			this.textProcInsEst.Name = "textProcInsEst";
			this.textProcInsEst.ReadOnly = true;
			this.textProcInsEst.Size = new System.Drawing.Size(76,20);
			this.textProcInsEst.TabIndex = 37;
			this.textProcInsEst.Text = "55.00";
			this.textProcInsEst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textProcInsPaid
			// 
			this.textProcInsPaid.Location = new System.Drawing.Point(461,68);
			this.textProcInsPaid.Name = "textProcInsPaid";
			this.textProcInsPaid.ReadOnly = true;
			this.textProcInsPaid.Size = new System.Drawing.Size(76,20);
			this.textProcInsPaid.TabIndex = 36;
			this.textProcInsPaid.Text = "45.00";
			this.textProcInsPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textProcFee
			// 
			this.textProcFee.Location = new System.Drawing.Point(461,48);
			this.textProcFee.Name = "textProcFee";
			this.textProcFee.ReadOnly = true;
			this.textProcFee.Size = new System.Drawing.Size(76,20);
			this.textProcFee.TabIndex = 35;
			this.textProcFee.Text = "200.00";
			this.textProcFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(353,150);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(104,16);
			this.label13.TabIndex = 34;
			this.label13.Text = "- Paid Here";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(353,176);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(104,16);
			this.label12.TabIndex = 33;
			this.label12.Text = "= Remaining";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(330,130);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(127,16);
			this.label11.TabIndex = 32;
			this.label11.Text = "- Previously Paid";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(353,110);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(104,16);
			this.label10.TabIndex = 31;
			this.label10.Text = "- Adjustments";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(353,90);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(104,16);
			this.label9.TabIndex = 30;
			this.label9.Text = "- Ins Est";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(353,70);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(104,16);
			this.label8.TabIndex = 29;
			this.label8.Text = "- Ins Paid";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(353,50);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(104,16);
			this.label6.TabIndex = 28;
			this.label6.Text = "Fee";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9,78);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104,16);
			this.label4.TabIndex = 27;
			this.label4.Text = "Provider";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(9,118);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104,16);
			this.label3.TabIndex = 26;
			this.label3.Text = "Description";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8,57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104,16);
			this.label2.TabIndex = 25;
			this.label2.Text = "Procedure Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butDetach
			// 
			this.butDetach.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDetach.Autosize = true;
			this.butDetach.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDetach.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDetach.CornerRadius = 4F;
			this.butDetach.Location = new System.Drawing.Point(99,21);
			this.butDetach.Name = "butDetach";
			this.butDetach.Size = new System.Drawing.Size(75,25);
			this.butDetach.TabIndex = 9;
			this.butDetach.Text = "Detach";
			this.butDetach.Click += new System.EventHandler(this.butDetach_Click);
			// 
			// butAttach
			// 
			this.butAttach.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAttach.Autosize = true;
			this.butAttach.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAttach.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAttach.CornerRadius = 4F;
			this.butAttach.Location = new System.Drawing.Point(12,21);
			this.butAttach.Name = "butAttach";
			this.butAttach.Size = new System.Drawing.Size(75,25);
			this.butAttach.TabIndex = 8;
			this.butAttach.Text = "Attach";
			this.butAttach.Click += new System.EventHandler(this.butAttach_Click);
			// 
			// textDateEntry
			// 
			this.textDateEntry.Location = new System.Drawing.Point(129,22);
			this.textDateEntry.Name = "textDateEntry";
			this.textDateEntry.ReadOnly = true;
			this.textDateEntry.Size = new System.Drawing.Size(92,20);
			this.textDateEntry.TabIndex = 114;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(1,24);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(127,16);
			this.label15.TabIndex = 115;
			this.label15.Text = "Entry Date";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormPaySplitEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(762,541);
			this.Controls.Add(this.textDateEntry);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.groupProcedure);
			this.Controls.Add(this.groupPatient);
			this.Controls.Add(this.textProcDate);
			this.Controls.Add(this.textDatePay);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.butRemainder);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.ButCancel);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkPayPlan);
			this.Controls.Add(this.labelAmount);
			this.Controls.Add(this.listProvider);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.labelRemainder);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(0,400);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPaySplitEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Payment Split";
			this.Load += new System.EventHandler(this.FormPaySplitEdit_Load);
			this.groupPatient.ResumeLayout(false);
			this.groupPatient.PerformLayout();
			this.groupProcedure.ResumeLayout(false);
			this.groupProcedure.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPaySplitEdit_Load(object sender, System.EventArgs e) {
			if(PaySplitCur==null) {
				MessageBox.Show("Split cannot be null.");//just for debugging
			}
			textDateEntry.Text=PaySplitCur.DateEntry.ToShortDateString();
			textDatePay.Text=PaySplitCur.DatePay.ToShortDateString();
			textProcDate.Text=PaySplitCur.ProcDate.ToShortDateString();
			textAmount.Text=PaySplitCur.SplitAmt.ToString("F");
			for(int i=0;i<Providers.List.Length;i++){
				listProvider.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==PaySplitCur.ProvNum)
					listProvider.SelectedIndex=i;
			}
			if(PaySplitCur.PayPlanNum==0){
				checkPayPlan.Checked=false;
			}
			else{
				checkPayPlan.Checked=true;
			}
			FillPatient();
			FillProcedure();
		}

		private void butRemainder_Click(object sender, System.EventArgs e) {
			textAmount.Text=Remain.ToString("F");
		}

		///<summary>PaySplit.Patient is one value that is always kept in synch with the display.  If program changes PaySplit.Patient, then it will run this method to update the display.  If user changes display, then _MouseDown is run to update the PaySplit.Patient.</summary>
		private void FillPatient(){
			listPatient.Items.Clear();
			for(int i=0;i<FamCur.List.Length;i++){
				listPatient.Items.Add(FamCur.GetNameInFamLFI(i));
				if(PaySplitCur.PatNum==FamCur.List[i].PatNum){
					listPatient.SelectedIndex=i;
				}
			}
			//this can happen if user unchecks the "Is From Other Fam" box. Need to reset.
			if(PaySplitCur.PatNum==0){
				listPatient.SelectedIndex=0;
				//the initial patient will be the first patient in the family, usually guarantor
				PaySplitCur.PatNum=FamCur.List[0].PatNum;
			}
			if(listPatient.SelectedIndex==-1){//patient not in family
				checkPatOtherFam.Checked=true;
				textPatient.Visible=true;
				listPatient.Visible=false;
				textPatient.Text=Patients.GetLim(PaySplitCur.PatNum).GetNameLF();
			}
			else{//show the family list that was just filled
				checkPatOtherFam.Checked=false;
				textPatient.Visible=false;
				listPatient.Visible=true;
			}
		}

		private void checkPatOtherFam_Click(object sender, System.EventArgs e) {
			//this happens after the check change has been registered
			if(checkPatOtherFam.Checked){
				FormPatientSelect FormPS=new FormPatientSelect();
				FormPS.SelectionModeOnly=true;//this will cause a change in the patNum only
				FormPS.ShowDialog();
				if(FormPS.DialogResult!=DialogResult.OK){
					checkPatOtherFam.Checked=false;
					return;
				}
				PaySplitCur.PatNum=FormPS.SelectedPatNum;
			}
			else{//switch to family view
				PaySplitCur.PatNum=0;//this will reset the selected patient to current patient
			}
			FillPatient();
		}

		private void listPatient_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listPatient.SelectedIndex==-1){
				return;
			}
			PaySplitCur.PatNum=FamCur.List[listPatient.SelectedIndex].PatNum;
		}

		private void FillProcedure(){
			if(PaySplitCur.ProcNum==0){
				textProcDate2.Text="";
				textProcProv.Text="";
				textProcTooth.Text="";
				textProcDescription.Text="";
				ProcFee=0;
				textProcFee.Text="";
				ProcInsPaid=0;
				textProcInsPaid.Text="";
				ProcInsEst=0;
				textProcInsEst.Text="";
				ProcAdj=0;
				textProcAdj.Text="";
				ProcPrevPaid=0;
				textProcPrevPaid.Text="";
				ProcPaidHere=0;
				textProcPaidHere.Text="";
				labelProcRemain.Text="";
				//butAttach.Enabled=true;
				//butDetach.Enabled=false;
				//ComputeProcTotals();
				return;
			}
			Procedure ProcCur=Procedures.GetOneProc(PaySplitCur.ProcNum,false);
			ClaimProc[] ClaimProcList=ClaimProcs.Refresh(ProcCur.PatNum);
			Adjustment[] AdjustmentList=Adjustments.Refresh(ProcCur.PatNum);
			PaySplit[] PaySplitList=PaySplits.Refresh(ProcCur.PatNum);
			textProcDate.Text=ProcCur.ProcDate.ToShortDateString();
			textProcDate2.Text=ProcCur.ProcDate.ToShortDateString();
			textProcProv.Text=Providers.GetAbbr(ProcCur.ProvNum);
			textProcTooth.Text=Tooth.ToInternat(ProcCur.ToothNum);
			textProcDescription.Text=ProcedureCodes.GetProcCode(ProcCur.CodeNum).Descript;
			ProcFee=ProcCur.ProcFee;
			ProcInsPaid=-ClaimProcs.ProcInsPay(ClaimProcList,ProcCur.ProcNum);
			ProcInsEst=-ClaimProcs.ProcEstNotReceived(ClaimProcList,ProcCur.ProcNum);
			ProcAdj=Adjustments.GetTotForProc(ProcCur.ProcNum,AdjustmentList);
			//next line will still work even if IsNew
			ProcPrevPaid=-PaySplits.GetTotForProc(ProcCur.ProcNum,PaySplitList,PaySplitCur.SplitNum);
			textProcFee.Text=ProcFee.ToString("F");
			if(ProcInsPaid==0){
				textProcInsPaid.Text="";
			}
			else{
				textProcInsPaid.Text=ProcInsPaid.ToString("F");
			}
			if(ProcInsEst==0){
				textProcInsEst.Text="";
			}
			else{
				textProcInsEst.Text=ProcInsEst.ToString("F");
			}
			if(ProcAdj==0){
				textProcAdj.Text="";
			}
			else{
				textProcAdj.Text=ProcAdj.ToString("F");
			}
			if(ProcPrevPaid==0){
				textProcPrevPaid.Text="";
			}
			else{
				textProcPrevPaid.Text=ProcPrevPaid.ToString("F");
			}
			ComputeProcTotals();
			//butAttach.Enabled=false;
			//butDetach.Enabled=true;
		}

		///<summary>Does not alter any of the proc amounts except PaidHere and Remaining.</summary>
		private void ComputeProcTotals(){
			ProcPaidHere=0;
			if(textAmount.errorProvider1.GetError(textAmount)==""){
				ProcPaidHere=-PIn.PDouble(textAmount.Text);	
			}
			if(ProcPaidHere==0){
				textProcPaidHere.Text="";
			}
			else{
				textProcPaidHere.Text=ProcPaidHere.ToString("F");
			}
			//most of these are negative values, so add
			double remain=
				ProcFee
				+ProcInsPaid
				+ProcInsEst
				+ProcAdj
				+ProcPrevPaid
				+ProcPaidHere;
			labelProcRemain.Text=remain.ToString("c");
		}

		private void textAmount_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			//can not use textAmount_TextChanged without redesigning the validDouble control
			ComputeProcTotals();
		}

		private void butAttach_Click(object sender, System.EventArgs e) {
			FormProcSelect FormPS=new FormProcSelect(PaySplitCur.PatNum);
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK){
				return;
			}
			PaySplitCur.ProcNum=FormPS.SelectedProcNum;
			FillProcedure();
		}

		private void butDetach_Click(object sender, System.EventArgs e) {
			PaySplitCur.ProcNum=0;
			FillProcedure();
		}

		private void checkPayPlan_Click(object sender, System.EventArgs e) {
			if(checkPayPlan.Checked){
				if(checkPatOtherFam.Checked){//prevents a bug.
					checkPayPlan.Checked=false;
					return;
				}
				PayPlan[] planListAll=PayPlans.Refresh(FamCur.List[listPatient.SelectedIndex].PatNum,0);
				PayPlan[] payPlanList=PayPlans.GetListOneType(planListAll,false);
				if(payPlanList.Length==0){//no valid plans
					MsgBox.Show(this,"The selected patient is not the guarantor for any payment plans.");
					checkPayPlan.Checked=false;
					return;
				}
				if(payPlanList.Length==1){ //if there is only one valid payplan
					PaySplitCur.PayPlanNum=payPlanList[0].PayPlanNum;
					return;
				}
				//more than one valid PayPlan
				PayPlanCharge[] chargeList=PayPlanCharges.Refresh(FamCur.List[listPatient.SelectedIndex].PatNum);
				FormPayPlanSelect FormPPS=new FormPayPlanSelect(payPlanList,chargeList);
				//FormPPS.ValidPlans=payPlanList;
				FormPPS.ShowDialog();
				if(FormPPS.DialogResult==DialogResult.Cancel){
					checkPayPlan.Checked=false;
					return;
				}
				PaySplitCur.PayPlanNum=payPlanList[FormPPS.IndexSelected].PayPlanNum;
			}
			else{//payPlan unchecked
				PaySplitCur.PayPlanNum=0;
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(!MsgBox.Show(this,true,"Delete Item?")) {
				return;
			}
			PaySplitCur=null;
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textAmount.errorProvider1.GetError(textAmount)!=""
				|| textDatePay.errorProvider1.GetError(textDatePay)!=""
				|| textProcDate.errorProvider1.GetError(textProcDate)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(textAmount.Text==""){
				MsgBox.Show(this,"Please enter an amount.");	
				return;
			}
			PaySplitCur.DatePay=PIn.PDate(textDatePay.Text);//gets overwritten anyway
			PaySplitCur.ProcDate=PIn.PDate(textProcDate.Text);
			PaySplitCur.SplitAmt=PIn.PDouble(textAmount.Text);
			if(listProvider.SelectedIndex!=-1)
				PaySplitCur.ProvNum=Providers.List[listProvider.SelectedIndex].ProvNum;
			//if(!checkPatOtherFam.Checked){
				//This is still needed because it might be zero:
			//	PaySplitCur.PatNum=FamCur.List[listPatient.SelectedIndex].PatNum;
			//}
			//PayPlanNum already handled
			//PaySplitCur.InsertOrUpdate(IsNew);
			DialogResult=DialogResult.OK;
		}

		private void ButCancel_Click(object sender, System.EventArgs e) {
			if(IsNew) {
				PaySplitCur=null;
			}
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

		

		

		


	}
}
