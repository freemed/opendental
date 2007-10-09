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
		private ListBox listProv;
		private Label label1;
		private CheckBox checkAllProv;
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
			this.listProv = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkAllProv = new System.Windows.Forms.CheckBox();
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
			this.butCancel.Location = new System.Drawing.Point(602,344);
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
			this.butOK.Location = new System.Drawing.Point(602,309);
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
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(516,55);
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
			// checkAllProv
			// 
			this.checkAllProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllProv.Location = new System.Drawing.Point(517,35);
			this.checkAllProv.Name = "checkAllProv";
			this.checkAllProv.Size = new System.Drawing.Size(95,16);
			this.checkAllProv.TabIndex = 43;
			this.checkAllProv.Text = "All";
			this.checkAllProv.Click += new System.EventHandler(this.checkAllProv_Click);
			// 
			// FormRpPaySheet
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(717,383);
			this.Controls.Add(this.checkAllProv);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label1);
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
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPaymentSheet_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;
			for(int i=0;i<Providers.List.Length;i++) {
				listProv.Items.Add(Providers.List[i].Abbr+" - "+Providers.List[i].LName+", "+Providers.List[i].FName);
				//listProv.SetSelected(i,true);
			}
			checkAllProv.Checked=true;
			listProv.Visible=false;
			//for(int i=0;i<DefB.Short[(int)DefCat.PaymentTypes].Length;i++) {
			//	listPayType.Items.Add(DefB.Short[(int)DefCat.PaymentTypes][i].ItemName);
			//	listPayType.SetSelected(i,true);
			//}
			//checkIncludeIns.Checked=true;
		}

		private void checkAllProv_Click(object sender,EventArgs e) {
			if(checkAllProv.Checked){
				listProv.Visible=false;
			}
			else{
				listProv.Visible=true;
			}
		}
		/*
		private void butAllTypes_Click(object sender,EventArgs e) {
			for(int i=0;i<listPayType.Items.Count;i++) {
				listPayType.SetSelected(i,true);
			}
		}

		private void butNone_Click(object sender,EventArgs e) {
			listPayType.SelectedIndex=-1;
		}*/

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!checkAllProv.Checked && listProv.SelectedIndices.Count==0) {
				MsgBox.Show(this,"At least one provider must be selected.");
				return;
			}
			string whereProv;
			if(checkAllProv.Checked) {
				whereProv="";
			}
			else {
				whereProv="AND (";
				for(int i=0;i<listProv.SelectedIndices.Count;i++) {
					if(i>0) {
						whereProv+="OR ";
					}
					whereProv+="claimproc.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
				}
				whereProv+=")";
			}
			string queryIns=@"SELECT claimproc.DateCP,CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI) lfname,
				carrier.CarrierName,provider.Abbr,claimpayment.CheckNum,SUM(claimproc.InsPayAmt) amt,claimproc.ClaimNum 
				FROM claimproc,insplan,patient,carrier,provider,claimpayment 
				WHERE claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum 
				AND provider.ProvNum=claimproc.ProvNum 
				AND claimproc.PlanNum = insplan.PlanNum 
				AND claimproc.PatNum = patient.PatNum 
				AND carrier.CarrierNum = insplan.CarrierNum "
				+whereProv+" "
				+"AND (claimproc.Status=1 OR claimproc.Status=4) " //received or supplemental
				+"AND claimpayment.CheckDate >= "+POut.PDate(date1.SelectionStart)+" "
				+"AND claimpayment.CheckDate <= "+POut.PDate(date2.SelectionStart)+" "
				+"GROUP BY claimproc.ClaimPaymentNum,provider.ProvNum";
			if(checkAllProv.Checked){
				whereProv="";
			}
			else{
				whereProv="AND (";
				for(int i=0;i<listProv.SelectedIndices.Count;i++) {
					if(i>0) {
						whereProv+="OR ";
					}
					whereProv+="paysplit.ProvNum = '"
						+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
				}
				whereProv+=")";
			}
			string queryPat=@"SELECT paysplit.DatePay,CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI) AS lfname,
				payment.PayType,provider.Abbr,payment.CheckNum,
				SUM(paysplit.SplitAmt) amt, payment.PayNum,ItemName 
				FROM payment,patient,provider,paysplit,definition
				WHERE payment.PayNum=paysplit.PayNum 
				AND patient.PatNum=paysplit.PatNum 
				AND provider.ProvNum=paysplit.ProvNum
				AND definition.DefNum=payment.PayType "
				+whereProv+" "
				+"AND paysplit.DatePay >= "+POut.PDate(date1.SelectionStart)+" "
				+"AND paysplit.DatePay <= "+POut.PDate(date2.SelectionStart)+" "
				+"GROUP BY payment.PayNum,patient.PatNum,provider.ProvNum)";
			string sourceRDL=Properties.Resources.PaymentsRDL;
			
			//more code to go here

			FormReport FormR=new FormReport();
			FormR.SourceRdlString=sourceRDL;
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;		
		}

		

		

		

	}
}
