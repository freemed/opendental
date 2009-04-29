using System;
using System.Data;
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
		private GroupBox groupBox1;
		private RadioButton radioPatient;
		private RadioButton radioCheck;
		private CheckBox checkAllTypes;
		private ListBox listTypes;
		private CheckBox checkIns;
		private CheckBox checkAllProv;
		//private FormQuery FormQuery2;

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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioPatient = new System.Windows.Forms.RadioButton();
			this.radioCheck = new System.Windows.Forms.RadioButton();
			this.checkAllTypes = new System.Windows.Forms.CheckBox();
			this.listTypes = new System.Windows.Forms.ListBox();
			this.checkIns = new System.Windows.Forms.CheckBox();
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
			this.butCancel.Location = new System.Drawing.Point(702,468);
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
			this.butOK.Location = new System.Drawing.Point(702,433);
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
			this.listProv.Size = new System.Drawing.Size(163,199);
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
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioPatient);
			this.groupBox1.Controls.Add(this.radioCheck);
			this.groupBox1.Location = new System.Drawing.Point(31,263);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(161,72);
			this.groupBox1.TabIndex = 44;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Group By";
			// 
			// radioPatient
			// 
			this.radioPatient.Location = new System.Drawing.Point(8,43);
			this.radioPatient.Name = "radioPatient";
			this.radioPatient.Size = new System.Drawing.Size(104,18);
			this.radioPatient.TabIndex = 1;
			this.radioPatient.Text = "Patient";
			this.radioPatient.UseVisualStyleBackColor = true;
			// 
			// radioCheck
			// 
			this.radioCheck.Checked = true;
			this.radioCheck.Location = new System.Drawing.Point(8,19);
			this.radioCheck.Name = "radioCheck";
			this.radioCheck.Size = new System.Drawing.Size(104,18);
			this.radioCheck.TabIndex = 0;
			this.radioCheck.TabStop = true;
			this.radioCheck.Text = "Check";
			this.radioCheck.UseVisualStyleBackColor = true;
			// 
			// checkAllTypes
			// 
			this.checkAllTypes.Checked = true;
			this.checkAllTypes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAllTypes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllTypes.Location = new System.Drawing.Point(286,262);
			this.checkAllTypes.Name = "checkAllTypes";
			this.checkAllTypes.Size = new System.Drawing.Size(177,16);
			this.checkAllTypes.TabIndex = 47;
			this.checkAllTypes.Text = "All patient payment types";
			this.checkAllTypes.Click += new System.EventHandler(this.checkAllTypes_Click);
			// 
			// listTypes
			// 
			this.listTypes.Location = new System.Drawing.Point(285,282);
			this.listTypes.Name = "listTypes";
			this.listTypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listTypes.Size = new System.Drawing.Size(163,199);
			this.listTypes.TabIndex = 46;
			// 
			// checkIns
			// 
			this.checkIns.Checked = true;
			this.checkIns.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIns.Location = new System.Drawing.Point(286,243);
			this.checkIns.Name = "checkIns";
			this.checkIns.Size = new System.Drawing.Size(177,16);
			this.checkIns.TabIndex = 48;
			this.checkIns.Text = "Insurance payments";
			// 
			// FormRpPaySheet
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(817,507);
			this.Controls.Add(this.checkIns);
			this.Controls.Add(this.checkAllTypes);
			this.Controls.Add(this.listTypes);
			this.Controls.Add(this.groupBox1);
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
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPaymentSheet_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;
			for(int i=0;i<ProviderC.List.Length;i++) {
				listProv.Items.Add(ProviderC.List[i].GetLongDesc());
			}
			checkAllProv.Checked=true;
			listProv.Visible=false;
			for(int i=0;i<DefC.Short[(int)DefCat.PaymentTypes].Length;i++) {
				listTypes.Items.Add(DefC.Short[(int)DefCat.PaymentTypes][i].ItemName);
			}
			listTypes.Visible=false;
		}

		private void checkAllProv_Click(object sender,EventArgs e) {
			if(checkAllProv.Checked){
				listProv.Visible=false;
			}
			else{
				listProv.Visible=true;
			}
		}

		private void checkAllTypes_Click(object sender,EventArgs e) {
			if(checkAllTypes.Checked){
				listTypes.Visible=false;
			}
			else{
				listTypes.Visible=true;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!checkAllProv.Checked && listProv.SelectedIndices.Count==0) {
				MsgBox.Show(this,"At least one provider must be selected.");
				return;
			}
			if(!checkAllTypes.Checked && listTypes.SelectedIndices.Count==0 && !checkIns.Checked) {
				MsgBox.Show(this,"At least one type must be selected.");
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
					whereProv+="claimproc.ProvNum = "+POut.PInt(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
				}
				whereProv+=")";
			}
			string queryIns=
				@"SELECT claimproc.DateCP,CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI) lfname,
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
				+"AND claimpayment.CheckDate <= "+POut.PDate(date2.SelectionStart)+" ";
			if(radioPatient.Checked){
				queryIns+="GROUP BY patient.PatNum,claimproc.ClaimPaymentNum,provider.ProvNum";
			}
			else{
				queryIns+="GROUP BY claimproc.ClaimPaymentNum,provider.ProvNum";
			}
			queryIns+=" ORDER BY claimproc.DateCP";
			if(!checkIns.Checked){
				queryIns+=" LIMIT 0";
			}
			//patient payments-----------------------------------------------------------------------------------------
			if(checkAllProv.Checked){
				whereProv="";
			}
			else{
				whereProv="AND (";
				for(int i=0;i<listProv.SelectedIndices.Count;i++) {
					if(i>0) {
						whereProv+="OR ";
					}
					whereProv+="paysplit.ProvNum = "+POut.PInt(ProviderC.List[listProv.SelectedIndices[i]].ProvNum)+" ";
				}
				whereProv+=")";
			}
			string queryPat=
				@"SELECT paysplit.DatePay,CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI) AS lfname,
				payment.PayType,provider.Abbr,payment.CheckNum,
				SUM(paysplit.SplitAmt) amt, payment.PayNum,ItemName 
				FROM paysplit
				LEFT JOIN payment ON payment.PayNum=paysplit.PayNum 
				LEFT JOIN patient ON patient.PatNum=paysplit.PatNum
				LEFT JOIN provider ON provider.ProvNum=paysplit.ProvNum
				LEFT JOIN definition ON definition.DefNum=payment.PayType 
				WHERE 1 "
				+whereProv+" "
				+"AND paysplit.DatePay >= "+POut.PDate(date1.SelectionStart)+" "
				+"AND paysplit.DatePay <= "+POut.PDate(date2.SelectionStart)+" ";
			if(listTypes.SelectedIndices.Count>0){
				queryPat+="AND (";
				for(int i=0;i<listTypes.SelectedIndices.Count;i++) {
					if(i>0) {
						queryPat+="OR ";
					}
					queryPat+="payment.PayType = "+POut.PInt(DefC.Short[(int)DefCat.PaymentTypes][listTypes.SelectedIndices[i]].DefNum)+" ";
				}
				queryPat+=") ";
			}
			if(radioPatient.Checked){
				queryPat+="GROUP BY paysplit.DatePay,paysplit.PayNum,patient.PatNum,provider.ProvNum";
			}
			else{
				queryPat+="GROUP BY paysplit.DatePay,paysplit.PayNum,provider.ProvNum";
			}
			queryPat+=" ORDER BY paysplit.DatePay";
			if(!checkAllTypes.Checked && listTypes.SelectedIndices.Count==0){
				queryPat+=" LIMIT 0";
			}
			DataTable tableIns=Reports.GetTable(queryIns);
			DataTable tablePat=Reports.GetTable(queryPat);
			FormReport FormR=new FormReport();
			FormR.SourceRdlString=Properties.Resources.PaymentsRDL;
			FormR.RdlReport.DataSets["Data"].SetData(tableIns);
			FormR.RdlReport.DataSets["DataPatPay"].SetData(tablePat);
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;		
		}

		

		

		

		

	}
}
