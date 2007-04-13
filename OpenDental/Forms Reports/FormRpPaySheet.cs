using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpPaySheet : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butAllProv;
		private ListBox listProv;
		private Label label1;
		private OpenDental.UI.Button butAllTypes;
		private OpenDental.UI.Button butNone;
		private CheckBox checkIncludeIns;
		private ListBox listPayType;
		private Label label2;
		private RadioButton radioPatient;
		private RadioButton radioCheck;
		private GroupBox groupBox1;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpPaySheet(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpPaySheet));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.butAllProv = new OpenDental.UI.Button();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butAllTypes = new OpenDental.UI.Button();
			this.butNone = new OpenDental.UI.Button();
			this.checkIncludeIns = new System.Windows.Forms.CheckBox();
			this.listPayType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.radioPatient = new System.Windows.Forms.RadioButton();
			this.radioCheck = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(602,433);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(602,398);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(285,36);
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(31,36);
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(212,44);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(72,23);
			this.labelTO.TabIndex = 22;
			this.labelTO.Text = "TO";
			this.labelTO.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butAllProv
			// 
			this.butAllProv.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAllProv.Autosize = true;
			this.butAllProv.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAllProv.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAllProv.CornerRadius = 4F;
			this.butAllProv.Location = new System.Drawing.Point(515,191);
			this.butAllProv.Name = "butAllProv";
			this.butAllProv.Size = new System.Drawing.Size(75,26);
			this.butAllProv.TabIndex = 37;
			this.butAllProv.Text = "&All";
			this.butAllProv.Click += new System.EventHandler(this.butAllProv_Click);
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(514,36);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(163,147);
			this.listProv.TabIndex = 36;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(514,16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104,16);
			this.label1.TabIndex = 35;
			this.label1.Text = "Providers";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butAllTypes
			// 
			this.butAllTypes.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAllTypes.Autosize = true;
			this.butAllTypes.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAllTypes.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAllTypes.CornerRadius = 4F;
			this.butAllTypes.Location = new System.Drawing.Point(287,433);
			this.butAllTypes.Name = "butAllTypes";
			this.butAllTypes.Size = new System.Drawing.Size(66,26);
			this.butAllTypes.TabIndex = 42;
			this.butAllTypes.Text = "&All";
			this.butAllTypes.Click += new System.EventHandler(this.butAllTypes_Click);
			// 
			// butNone
			// 
			this.butNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNone.Autosize = true;
			this.butNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNone.CornerRadius = 4F;
			this.butNone.Location = new System.Drawing.Point(355,433);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(66,26);
			this.butNone.TabIndex = 41;
			this.butNone.Text = "&None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// checkIncludeIns
			// 
			this.checkIncludeIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIncludeIns.Location = new System.Drawing.Point(50,255);
			this.checkIncludeIns.Name = "checkIncludeIns";
			this.checkIncludeIns.Size = new System.Drawing.Size(154,24);
			this.checkIncludeIns.TabIndex = 38;
			this.checkIncludeIns.Text = "Include Insurance Checks";
			// 
			// listPayType
			// 
			this.listPayType.Location = new System.Drawing.Point(287,255);
			this.listPayType.Name = "listPayType";
			this.listPayType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listPayType.Size = new System.Drawing.Size(134,173);
			this.listPayType.TabIndex = 39;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(284,229);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(154,23);
			this.label2.TabIndex = 40;
			this.label2.Text = "Patient Payment Types";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// radioPatient
			// 
			this.radioPatient.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioPatient.Location = new System.Drawing.Point(19,19);
			this.radioPatient.Name = "radioPatient";
			this.radioPatient.Size = new System.Drawing.Size(123,18);
			this.radioPatient.TabIndex = 0;
			this.radioPatient.Text = "Patient";
			// 
			// radioCheck
			// 
			this.radioCheck.Checked = true;
			this.radioCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioCheck.Location = new System.Drawing.Point(19,40);
			this.radioCheck.Name = "radioCheck";
			this.radioCheck.Size = new System.Drawing.Size(133,18);
			this.radioCheck.TabIndex = 1;
			this.radioCheck.TabStop = true;
			this.radioCheck.Text = "Check";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioCheck);
			this.groupBox1.Controls.Add(this.radioPatient);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(31,297);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(176,65);
			this.groupBox1.TabIndex = 24;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Group Insurance Checks By";
			// 
			// FormRpPaySheet
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(717,472);
			this.Controls.Add(this.butAllTypes);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.checkIncludeIns);
			this.Controls.Add(this.listPayType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butAllProv);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.labelTO);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpPaySheet";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Daily Payments Report";
			this.Load += new System.EventHandler(this.FormPaymentSheet_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPaymentSheet_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;
			for(int i=0;i<Providers.List.Length;i++) {
				listProv.Items.Add(Providers.List[i].Abbr+" - "+Providers.List[i].LName+", "+Providers.List[i].FName);
				listProv.SetSelected(i,true);
			}
			for(int i=0;i<DefB.Short[(int)DefCat.PaymentTypes].Length;i++) {
				listPayType.Items.Add(DefB.Short[(int)DefCat.PaymentTypes][i].ItemName);
				listPayType.SetSelected(i,true);
			}
			checkIncludeIns.Checked=true;
		}

		private void butAllProv_Click(object sender,EventArgs e) {
			for(int i=0;i<listProv.Items.Count;i++) {
				listProv.SetSelected(i,true);
			}
		}

		private void butAllTypes_Click(object sender,EventArgs e) {
			for(int i=0;i<listPayType.Items.Count;i++) {
				listPayType.SetSelected(i,true);
			}
		}

		private void butNone_Click(object sender,EventArgs e) {
			listPayType.SelectedIndex=-1;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listProv.SelectedIndices.Count==0) {
				MsgBox.Show(this,"At least one provider must be selected.");
				return;
			}
			if(listPayType.SelectedIndices.Count==0 && !checkIncludeIns.Checked) {
				MsgBox.Show(this,"Must either select a payment type and/or include insurance checks.");
				return;
			}
			string whereProv="(";
			for(int i=0;i<listProv.SelectedIndices.Count;i++) {
				if(i>0) {
					whereProv+="OR ";
				}
				whereProv+="paysplit.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
			}
			whereProv+=")";
			string whereType="(";
			for(int i=0;i<listPayType.SelectedIndices.Count;i++) {
				if(i>0) {
					whereType+="OR ";
				}
				whereType+="payment.PayType = '"
					+POut.PInt(DefB.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndices[i]].DefNum)+"' ";
			}
			whereType+=")";
			Queries.CurReport=new ReportOld();
			Queries.CurReport.Query="(SELECT "
				+"paysplit.DatePay AS mydate,"//0. Date
				+"CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI) AS plfname,"//1. name
				+"'                                                 ',"//2. Carrier. this is long so union won't get trunc.
				+"payment.PayType,"//3. paytype
				+"provider.Abbr,"//4. Prov
				+"payment.CheckNum,"//5. CheckNum
				+"SUM(paysplit.SplitAmt) $amt, "//6. amt
				+"payment.PayNum "//7. PayNum. Not visible
				+"FROM payment,patient,provider,paysplit "
				+"WHERE ";
			if(listPayType.SelectedIndices.Count==0){ 
				Queries.CurReport.Query+="1=0 ";//none
			}
			else{
				Queries.CurReport.Query+=
					"payment.PayNum=paysplit.PayNum "
					+"AND patient.PatNum=paysplit.PatNum "
					+"AND provider.ProvNum=paysplit.ProvNum "
					+"AND "+whereProv+" "
					+"AND "+whereType+" "
					+"AND paysplit.DatePay >= "+POut.PDate(date1.SelectionStart)+" "
					+"AND paysplit.DatePay <= "+POut.PDate(date2.SelectionStart)+" ";
			}
			Queries.CurReport.Query+="GROUP BY payment.PayNum,patient.PatNum,provider.ProvNum)";
			if(checkIncludeIns.Checked){
				whereProv="(";
				for(int i=0;i<listProv.SelectedIndices.Count;i++) {
					if(i>0) {
						whereProv+="OR ";
					}
					whereProv+="claimproc.ProvNum = '"
						+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
				}
				whereProv+=")";
				Queries.CurReport.Query+=
					" UNION ("
					+"SELECT claimproc.DateCP AS mydate,"//0. Date
					+"CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI) AS plfname,"//1. Name
					+"carrier.CarrierName,"//2. Carrier
					+"'',"//3. PayType
					+"provider.Abbr,"//4. Prov
					+"claimpayment.CheckNum,"//5. CheckNum
					+"SUM(claimproc.InsPayAmt), "//6. Amt
					+"claimproc.ClaimNum "//7. Num(not visible)
					+"FROM claimproc,insplan,patient,carrier,provider,claimpayment "
					+"WHERE claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum "
					+"AND provider.ProvNum=claimproc.ProvNum "
					+"AND claimproc.PlanNum = insplan.PlanNum "
					+"AND claimproc.PatNum = patient.PatNum "
					+"AND carrier.CarrierNum = insplan.CarrierNum "
					+"AND "+whereProv+" "
					+"AND (claimproc.Status=1 OR claimproc.Status=4) "//received or supplemental
					+"AND claimpayment.CheckDate >= "+POut.PDate(date1.SelectionStart)+" "
					+"AND claimpayment.CheckDate <= "+POut.PDate(date2.SelectionStart)+" ";
				if(radioPatient.Checked){//by patient
					Queries.CurReport.Query+="GROUP BY claimproc.ClaimNum,patient.PatNum,provider.ProvNum ";
				}
				else{//by check
					Queries.CurReport.Query+="GROUP BY claimproc.ClaimPaymentNum,provider.ProvNum ";
				}
				Queries.CurReport.Query+=
					")";//end of union
			}//insurance section
			Queries.CurReport.Query+=
				//" ORDER BY mydate,PayType,plfname";//FIXME:UNION-ORDER-BY
				" ORDER BY 1,4,2";
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();			
			Queries.CurReport.Title="Daily Payments";
			Queries.CurReport.SubTitle=new string[5];
			Queries.CurReport.SubTitle[0]=((Pref)PrefB.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=date1.SelectionStart.ToString("d")+" - "+date2.SelectionStart.ToString("d");
			Queries.CurReport.SubTitle[2]=Lan.g(this,"Patient Payment Type(s): ");
			if(listPayType.SelectedIndices.Count==listPayType.Items.Count){
				Queries.CurReport.SubTitle[2]+=Lan.g(this,"All");
			}
			else if(listPayType.SelectedIndices.Count==0){
				Queries.CurReport.SubTitle[2]+=Lan.g(this,"None");
			}
			else{
				for(int i=0;i<listPayType.SelectedIndices.Count;i++) {
					if(i>0){
						Queries.CurReport.SubTitle[2]+=", ";
					}
					Queries.CurReport.SubTitle[2]
						+=DefB.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndices[i]].ItemName;
				}
			}
			Queries.CurReport.SubTitle[3]=Lan.g(this,"Insurance Payments: ");
			if(checkIncludeIns.Checked) {
				Queries.CurReport.SubTitle[3]+="Included";
			}
			else{
				Queries.CurReport.SubTitle[3]+="Not included";
			}
			Queries.CurReport.SubTitle[4]=Lan.g(this,"Providers: ");
			if(listProv.SelectedIndices.Count==listProv.Items.Count) {
				Queries.CurReport.SubTitle[4]+=Lan.g(this,"All");
			}
			else{
				for(int i=0;i<listProv.SelectedIndices.Count;i++) {
					if(i>0) {
						Queries.CurReport.SubTitle[4]+=", ";
					}
					Queries.CurReport.SubTitle[4]+=Providers.List[listProv.SelectedIndices[i]].Abbr;
				}
			}
			Queries.CurReport.ColPos=new int[9];
			Queries.CurReport.ColCaption=new string[8];
			Queries.CurReport.ColAlign=new HorizontalAlignment[8];
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=100;
			Queries.CurReport.ColPos[2]=200;
			Queries.CurReport.ColPos[3]=380;
			Queries.CurReport.ColPos[4]=470;
			Queries.CurReport.ColPos[5]=540;
			Queries.CurReport.ColPos[6]=640;
			Queries.CurReport.ColPos[7]=700;
			Queries.CurReport.ColPos[8]=900;//off the right edge
			Queries.CurReport.ColCaption[0]="Date";
			Queries.CurReport.ColCaption[1]="Patient Name";
			Queries.CurReport.ColCaption[2]="Carrier";
			Queries.CurReport.ColCaption[3]="Payment Type";
			Queries.CurReport.ColCaption[4]="Provider";
			Queries.CurReport.ColCaption[5]="Check #";
			Queries.CurReport.ColCaption[6]="Amount";
			Queries.CurReport.ColCaption[7]="Payment #";//not shown
			Queries.CurReport.ColAlign[6]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[7]=HorizontalAlignment.Right;
			Queries.CurReport.Summary=new string[0];
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;		
		}

		

		

	}
}
