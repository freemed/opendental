using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpDepositSlip : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.ListBox listPayType;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.CheckBox checkBoxIns;
		private OpenDental.UI.Button butNone;
		private OpenDental.UI.Button butAll;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		private System.Windows.Forms.MonthCalendar monthCal1;
		private System.Windows.Forms.MonthCalendar monthCal2;
		private System.Windows.Forms.Label label1;
		private FormQuery FormQuery2;
		
		///<summary></summary>
		public FormRpDepositSlip(){
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpDepositSlip));
			this.listPayType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.checkBoxIns = new System.Windows.Forms.CheckBox();
			this.butNone = new OpenDental.UI.Button();
			this.butAll = new OpenDental.UI.Button();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.monthCal1 = new System.Windows.Forms.MonthCalendar();
			this.monthCal2 = new System.Windows.Forms.MonthCalendar();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listPayType
			// 
			this.listPayType.Location = new System.Drawing.Point(474,59);
			this.listPayType.Name = "listPayType";
			this.listPayType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listPayType.Size = new System.Drawing.Size(134,173);
			this.listPayType.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(474,43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127,23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Payment Type";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(533,301);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 6;
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
			this.butCancel.Location = new System.Drawing.Point(533,335);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "&Cancel";
			// 
			// checkBoxIns
			// 
			this.checkBoxIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBoxIns.Location = new System.Drawing.Point(472,11);
			this.checkBoxIns.Name = "checkBoxIns";
			this.checkBoxIns.Size = new System.Drawing.Size(154,24);
			this.checkBoxIns.TabIndex = 3;
			this.checkBoxIns.Text = "Include Insurance Checks";
			// 
			// butNone
			// 
			this.butNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNone.Autosize = true;
			this.butNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNone.CornerRadius = 4F;
			this.butNone.Location = new System.Drawing.Point(542,237);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(66,26);
			this.butNone.TabIndex = 5;
			this.butNone.Text = "&None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(474,237);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(66,26);
			this.butAll.TabIndex = 8;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(113,15);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(198,21);
			this.comboClinic.TabIndex = 92;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(7,18);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(102,14);
			this.labelClinic.TabIndex = 91;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// monthCal1
			// 
			this.monthCal1.Location = new System.Drawing.Point(24,61);
			this.monthCal1.MaxSelectionCount = 1;
			this.monthCal1.Name = "monthCal1";
			this.monthCal1.TabIndex = 93;
			// 
			// monthCal2
			// 
			this.monthCal2.Location = new System.Drawing.Point(258,61);
			this.monthCal2.MaxSelectionCount = 1;
			this.monthCal2.Name = "monthCal2";
			this.monthCal2.TabIndex = 94;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(195,64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71,23);
			this.label1.TabIndex = 95;
			this.label1.Text = "TO";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// FormRpDepositSlip
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(643,400);
			this.Controls.Add(this.monthCal2);
			this.Controls.Add(this.monthCal1);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.checkBoxIns);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.listPayType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpDepositSlip";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Deposit Slip";
			this.Load += new System.EventHandler(this.FormDepositSlip_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDepositSlip_Load(object sender, System.EventArgs e) {
			if(PrefB.GetBool("EasyNoClinics")){
				comboClinic.Visible=false;
				labelClinic.Visible=false;
			}
			comboClinic.Items.Clear();
			comboClinic.Items.Add(Lan.g(this,"none"));
			comboClinic.SelectedIndex=0;
			for(int i=0;i<Clinics.List.Length;i++){
				comboClinic.Items.Add(Clinics.List[i].Description);
			}
			monthCal1.SelectionStart=DateTime.Today;
			monthCal2.SelectionStart=DateTime.Today;
			//textDateFrom.Text=DateTime.Today.ToShortDateString();
			//textDateTo.Text=DateTime.Today.ToShortDateString();
			for(int i=0;i<DefB.Short[(int)DefCat.PaymentTypes].Length;i++){
				this.listPayType.Items.Add(DefB.Short[(int)DefCat.PaymentTypes][i].ItemName);
				listPayType.SetSelected(i,true);
			}
			checkBoxIns.Checked=true;
		}

		/*private void butToday_Click(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
		}

		private void butLeft_Click(object sender, System.EventArgs e) {
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			DateTime dateTo=PIn.PDate(textDateTo.Text);
			textDateFrom.Text=dateFrom.AddDays(-1).ToShortDateString();
			textDateTo.Text=dateTo.AddDays(-1).ToShortDateString();
		}

		private void butRight_Click(object sender, System.EventArgs e) {
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			DateTime dateTo=PIn.PDate(textDateTo.Text);
			textDateFrom.Text=dateFrom.AddDays(1).ToShortDateString();
			textDateTo.Text=dateTo.AddDays(1).ToShortDateString();
		}*/

		private void butOK_Click(object sender, System.EventArgs e) {
/*
SELECT PayDate,CONCAT(patient.LName,',',patient.FName,' ',patient.MiddleI),CheckNum,BankBranch,PayAmt
FROM payment,patient
WHERE payment.PatNum=patient.PatNum
GROUP BY PayDate
UNION
SELECT CheckDate,insplan.Carrier,CheckNum,BankBranch,CheckAmt
FROM claimproc,claimpayment,insplan
WHERE claimproc.ClaimPaymentNum=claimpayment.ClaimPaymentNum && claimproc.PlanNum=insplan.PlanNum 
GROUP BY CheckDate
ORDER BY PayDate
*/

/*
SELECT PayDate,CONCAT(patient.LName,', ',patient.FName,' ',
patient.MiddleI) AS plfname,'                          ',PayType,
PayNum,CheckNum,BankBranch,PayAmt 
FROM payment,patient WHERE 
payment.PatNum = patient.PatNum AND (
PayType = '69'
) AND PayDate = '2005-01-10'
UNION SELECT CheckDate,CONCAT(patient.LName,', ',patient.FName,' ',
patient.MiddleI) AS plfname,CarrierName,'Ins',
claimpayment.ClaimPaymentNum,
CheckNum,BankBranch,CheckAmt
FROM claimpayment,claimproc,insplan,carrier,patient
WHERE claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum
AND claimproc.PlanNum = insplan.PlanNum
AND claimproc.PatNum=patient.PatNum
AND insplan.CarrierNum = carrier.CarrierNum
AND (claimproc.status = '1' OR claimproc.status = '4')
AND CheckDate = '2005-01-10'
ORDER BY PayDate, plfname
*/
			if(listPayType.SelectedIndices.Count==0
				&& !checkBoxIns.Checked)
			{
				MessageBox.Show("Must either select a payment type and/or include insurance checks.");
				return;
			}
			Queries.CurReport=new ReportOld();
			string cmd="";
			//if(checkBoxIns.Checked){
			cmd="SELECT PayDate,CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),"
				+"patient.MiddleI) AS plfname,'                          ',PayType,"
				+"PayNum,CheckNum,BankBranch,PayAmt "
				+"FROM payment,patient WHERE ";//added plfname,paynum spk 4/14/04
			if(listPayType.SelectedIndices.Count==0){ 
				cmd+="1=0 ";//none
			}
			else{
				cmd+="payment.PatNum = patient.PatNum AND (";
				for(int i=0;i<listPayType.SelectedIndices.Count;i++){
					if(i>0) cmd+=" OR "; 
					cmd+="PayType = '"
						+DefB.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndices[i]].DefNum+"'";
				}
				cmd+=
					") AND PayDate >= "+POut.PDate(monthCal1.SelectionStart)+" "
					+"AND PayDate <= "+POut.PDate(monthCal2.SelectionStart)+" ";
				if(!PrefB.GetBool("EasyNoClinics")){
					if(comboClinic.SelectedIndex==0){
						cmd+="AND payment.ClinicNum=0 ";
					}
					else{
						cmd+="AND payment.ClinicNum="
							+POut.PInt(Clinics.List[comboClinic.SelectedIndex-1].ClinicNum)+" ";
					}
				}
      }
			if(checkBoxIns.Checked){
				cmd+="UNION SELECT CheckDate,CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),"
					+"patient.MiddleI) AS plfname,CarrierName,'Ins',"
					+"claimpayment.ClaimPaymentNum,"
					+"CheckNum,BankBranch,CheckAmt "//spk added claimpaymentnum
					//+"claimpayment.ClaimPaymentNum "
					+"FROM claimpayment,claimproc,insplan,carrier,patient "
					+"WHERE claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum "
					+"AND claimproc.PlanNum = insplan.PlanNum "
					+"AND claimproc.PatNum=patient.PatNum "
					+"AND insplan.CarrierNum = carrier.CarrierNum "
					+"AND (claimproc.status = '1' OR claimproc.status = '4') "
					+"AND CheckDate >= "+POut.PDate(monthCal1.SelectionStart)+" "
					+"AND CheckDate <= "+POut.PDate(monthCal2.SelectionStart)+" ";//added plfname,spk 4/30/04
				if(!PrefB.GetBool("EasyNoClinics")){
					if(comboClinic.SelectedIndex==0){
						cmd+="AND claimpayment.ClinicNum=0 ";
					}
					else{
						cmd+="AND claimpayment.ClinicNum="
							+POut.PInt(Clinics.List[comboClinic.SelectedIndex-1].ClinicNum)+" ";
					}
				}
				cmd+="GROUP BY claimpayment.ClaimPaymentNum ";
				//MessageBox.Show(Queries.CurReport.Query);
      }
			//cmd+="ORDER BY PayDate, plfname";//FIXME:UNION-ORDER-BY
			cmd+="ORDER BY 1, 2";
			Queries.CurReport.Query=cmd;
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();
			Queries.CurReport.Title="Deposit Slip";
			Queries.CurReport.SubTitle=new string[3];
			if(!PrefB.GetBool("EasyNoClinics")){
				Queries.CurReport.SubTitle=new string[4];
				if(comboClinic.SelectedIndex==0){
					Queries.CurReport.SubTitle[3]=Lan.g(this,"Clinic")+": none";
				}
				else{
					Queries.CurReport.SubTitle[3]=Lan.g(this,"Clinic")+": "
						+Clinics.List[comboClinic.SelectedIndex-1].Description;
				}
			}
			Queries.CurReport.SubTitle[0]=((Pref)PrefB.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=monthCal1.SelectionStart.ToShortDateString()+" - "
				+monthCal2.SelectionStart.ToShortDateString();
			if(listPayType.SelectedIndices.Count>0)  {
			  Queries.CurReport.SubTitle[2]="Payment Type(s): ";
					for(int i=0;i<listPayType.SelectedIndices.Count;i++){
						if(i>0) Queries.CurReport.SubTitle[2]+=", ";
						Queries.CurReport.SubTitle[2]
							+=DefB.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndices[i]].ItemName;
					}
				if(checkBoxIns.Checked)
					Queries.CurReport.SubTitle[2]+=" Insurance Claim Checks";
			}
			else  {
				if(checkBoxIns.Checked)  {
				 Queries.CurReport.SubTitle[2]="Payment Type: Insurance Claim Checks";
			  }
			}
			Queries.CurReport.ColPos=new int[9];
			Queries.CurReport.ColCaption=new string[8];
			Queries.CurReport.ColAlign=new HorizontalAlignment[8];
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=100;
			Queries.CurReport.ColPos[2]=210;
			Queries.CurReport.ColPos[3]=320;
			Queries.CurReport.ColPos[4]=420;
			Queries.CurReport.ColPos[5]=480;
			Queries.CurReport.ColPos[6]=590;
			Queries.CurReport.ColPos[7]=680;
			Queries.CurReport.ColPos[8]=760;
			Queries.CurReport.ColCaption[0]="Date";
			Queries.CurReport.ColCaption[1]="Patient";
			Queries.CurReport.ColCaption[2]="Carrier";
			Queries.CurReport.ColCaption[3]="Type";
			//this column can be eliminated when the new reporting framework is complete:
			Queries.CurReport.ColCaption[4]="Pay #";
			Queries.CurReport.ColCaption[5]="Check Number";
			Queries.CurReport.ColCaption[6]="Bank-Branch";
			Queries.CurReport.ColCaption[7]="Amount";
			//Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[7]=HorizontalAlignment.Right;
			if(PrefB.GetBool("EasyNoClinics") || comboClinic.SelectedIndex==0){
				Queries.CurReport.Summary=new string[3];
				Queries.CurReport.Summary[0]="For Deposit to Account of "+((Pref)PrefB.HList["PracticeTitle"]).ValueString;
				Queries.CurReport.Summary[2]="Account number: "+((Pref)PrefB.HList["PracticeBankNumber"]).ValueString;
			}
			else{
				Queries.CurReport.Summary=new string[3];
				Queries.CurReport.Summary[0]="For Deposit to Account of "+Clinics.List[comboClinic.SelectedIndex-1].Description;
				Queries.CurReport.Summary[2]="Account number: "+Clinics.List[comboClinic.SelectedIndex-1].BankNumber;
			}
			FormQuery2.ShowDialog();

			DialogResult=DialogResult.OK;
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			for(int i=0;i<listPayType.Items.Count;i++){
				listPayType.SetSelected(i,true);
			}
			checkBoxIns.Checked=true;
		}

		private void butNone_Click(object sender, System.EventArgs e) {
			listPayType.ClearSelected();
			checkBoxIns.Checked=false;
		}

		

		



	}
}
