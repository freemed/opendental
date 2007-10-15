using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormFinanceCharges : System.Windows.Forms.Form{
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radio30;
		private System.Windows.Forms.RadioButton radio90;
		private System.Windows.Forms.RadioButton radio60;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private OpenDental.ValidNum textAPR;
		private System.ComponentModel.Container components = null;
		private ArrayList ALPosIndices;
		private ValidDate textDateLastRun;
		private Label label5;
		private OpenDental.UI.Button butUndo;
		private GroupBox groupBox2;
		private ValidDate textDateUndo;
		private Label label6;
		private ListBox listBillType;
		private Label label7;
		private int adjType;

		///<summary></summary>
		public FormFinanceCharges(){
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

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFinanceCharges));
			this.textDate = new OpenDental.ValidDate();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radio30 = new System.Windows.Forms.RadioButton();
			this.radio90 = new System.Windows.Forms.RadioButton();
			this.radio60 = new System.Windows.Forms.RadioButton();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textAPR = new OpenDental.ValidNum();
			this.textDateLastRun = new OpenDental.ValidDate();
			this.label5 = new System.Windows.Forms.Label();
			this.butUndo = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textDateUndo = new OpenDental.ValidDate();
			this.label6 = new System.Windows.Forms.Label();
			this.listBillType = new System.Windows.Forms.ListBox();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(171,42);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(78,20);
			this.textDate.TabIndex = 15;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(15,46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(154,14);
			this.label1.TabIndex = 20;
			this.label1.Text = "Date of new charges";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radio30);
			this.groupBox1.Controls.Add(this.radio90);
			this.groupBox1.Controls.Add(this.radio60);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(154,98);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(167,98);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Age of Account";
			// 
			// radio30
			// 
			this.radio30.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio30.Location = new System.Drawing.Point(16,24);
			this.radio30.Name = "radio30";
			this.radio30.Size = new System.Drawing.Size(104,16);
			this.radio30.TabIndex = 1;
			this.radio30.Text = "Over 30 Days";
			// 
			// radio90
			// 
			this.radio90.Checked = true;
			this.radio90.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio90.Location = new System.Drawing.Point(16,70);
			this.radio90.Name = "radio90";
			this.radio90.Size = new System.Drawing.Size(104,18);
			this.radio90.TabIndex = 3;
			this.radio90.TabStop = true;
			this.radio90.Text = "Over 90 Days";
			// 
			// radio60
			// 
			this.radio60.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio60.Location = new System.Drawing.Point(16,46);
			this.radio60.Name = "radio60";
			this.radio60.Size = new System.Drawing.Size(104,18);
			this.radio60.TabIndex = 2;
			this.radio60.Text = "Over 60 Days";
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
			this.butCancel.Location = new System.Drawing.Point(588,380);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 19;
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
			this.butOK.Location = new System.Drawing.Point(588,346);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 18;
			this.butOK.Text = "Run";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(89,76);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80,14);
			this.label2.TabIndex = 22;
			this.label2.Text = "APR";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(215,76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(12,14);
			this.label3.TabIndex = 23;
			this.label3.Text = "%";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(233,76);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(144,14);
			this.label4.TabIndex = 24;
			this.label4.Text = "(For Example: 18)";
			// 
			// textAPR
			// 
			this.textAPR.Location = new System.Drawing.Point(171,72);
			this.textAPR.MaxVal = 255;
			this.textAPR.MinVal = 0;
			this.textAPR.Name = "textAPR";
			this.textAPR.Size = new System.Drawing.Size(42,20);
			this.textAPR.TabIndex = 26;
			// 
			// textDateLastRun
			// 
			this.textDateLastRun.Location = new System.Drawing.Point(171,16);
			this.textDateLastRun.Name = "textDateLastRun";
			this.textDateLastRun.ReadOnly = true;
			this.textDateLastRun.Size = new System.Drawing.Size(78,20);
			this.textDateLastRun.TabIndex = 27;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12,20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(157,14);
			this.label5.TabIndex = 28;
			this.label5.Text = "Date last run";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butUndo
			// 
			this.butUndo.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUndo.Autosize = true;
			this.butUndo.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUndo.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUndo.CornerRadius = 4F;
			this.butUndo.Location = new System.Drawing.Point(113,48);
			this.butUndo.Name = "butUndo";
			this.butUndo.Size = new System.Drawing.Size(78,25);
			this.butUndo.TabIndex = 30;
			this.butUndo.Text = "Undo";
			this.butUndo.Click += new System.EventHandler(this.butUndo_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textDateUndo);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.butUndo);
			this.groupBox2.Location = new System.Drawing.Point(58,318);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(263,87);
			this.groupBox2.TabIndex = 31;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Undo finance charges";
			// 
			// textDateUndo
			// 
			this.textDateUndo.Location = new System.Drawing.Point(113,19);
			this.textDateUndo.Name = "textDateUndo";
			this.textDateUndo.ReadOnly = true;
			this.textDateUndo.Size = new System.Drawing.Size(78,20);
			this.textDateUndo.TabIndex = 31;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16,23);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(95,14);
			this.label6.TabIndex = 32;
			this.label6.Text = "Date to undo";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listBillType
			// 
			this.listBillType.Location = new System.Drawing.Point(388,34);
			this.listBillType.Name = "listBillType";
			this.listBillType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBillType.Size = new System.Drawing.Size(158,186);
			this.listBillType.TabIndex = 32;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(387,16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(214,16);
			this.label7.TabIndex = 33;
			this.label7.Text = "Only apply to these Billing Types";
			// 
			// FormFinanceCharges
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(692,440);
			this.Controls.Add(this.listBillType);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.textDateLastRun);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textAPR);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFinanceCharges";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Finance Charges";
			this.Load += new System.EventHandler(this.FormFinanceCharges_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormFinanceCharges_Load(object sender, System.EventArgs e) {
			if(PrefB.GetInt("FinanceChargeAdjustmentType")==0){
				MsgBox.Show(this,"No finance charge adjustment type has been set.  Please go to Setup | Misc to fix this.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(PIn.PDate(PrefB.GetString("DateLastAging")) < DateTime.Today){
				if(MsgBox.Show(this,true,"You must update aging first.")){//OK
					FormAging FormA=new FormAging();
					FormA.ShowDialog();
				}
				else{
					DialogResult=DialogResult.Cancel;
					return;
				}
			}
			if(DefB.Short[(int)DefCat.BillingTypes].Length==0){//highly unlikely that this would happen
				MsgBox.Show(this,"No billing types have been set up or are visible.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			textDateLastRun.Text=PrefB.GetDate("FinanceChargeLastRun").ToShortDateString();
			textDateUndo.Text=PrefB.GetDate("FinanceChargeLastRun").ToShortDateString();
			textDate.Text=DateTime.Today.ToShortDateString();		
			textAPR.MaxVal=100;
			textAPR.MinVal=0;
			textAPR.Text=PrefB.GetString("FinanceChargeAPR");
			for(int i=0;i<DefB.Short[(int)DefCat.BillingTypes].Length;i++) {
				listBillType.Items.Add(DefB.Short[(int)DefCat.BillingTypes][i].ItemName);
				listBillType.SetSelected(i,true);
			}
		}

		private void butUndo_Click(object sender,EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Undo all finance charges for ")+textDateUndo.Text+"?","",MessageBoxButtons.OKCancel)
				!=DialogResult.OK) 
			{
				return;
			}
			int rowsAffected=Adjustments.UndoFinanceCharges(PIn.PDate(textDateUndo.Text));
			MessageBox.Show(Lan.g(this,"Adjustments deleted: ")+rowsAffected.ToString());
			FormAging FormA=new FormAging();
			FormA.SupressSameDateWarning=true;
			FormA.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Finance Charges undo. Date "+textDateUndo.Text);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDate.errorProvider1.GetError(textDate)!=""
				|| textAPR.errorProvider1.GetError(textAPR)!=""){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			DateTime date=PIn.PDate(textDate.Text);
			if(PrefB.GetDate("FinanceChargeLastRun").AddDays(25)>date) {
				if(!MsgBox.Show(this,true,"Warning.  Finance charges should not be run more than once per month.  Continue?")){
					return;
				}
			}
			if(listBillType.SelectedIndices.Count==0){
				MsgBox.Show(this,"Please select at least one billing type first.");
				return;
			}
			if(PIn.PInt(textAPR.Text) < 2){
				if(!MsgBox.Show(this,true,"The APR is much lower than normal. Do you wish to proceed?")){
					return;   
				}
			}			
			//Patients.ResetAging();
			//Ledgers.UpdateFinanceCharges(PIn.PDate(textDate.Text));
			PatAging[] AgingList=Patients.GetAgingList();
			double OverallBalance;
			int rowsAffected=0;
			bool billingMatch;
			for(int i=0;i<AgingList.Length;i++){
				OverallBalance=0;//this WILL NOT be the same as the patient's total balance
				if(radio30.Checked){
					OverallBalance=AgingList[i].Bal_31_60+AgingList[i].Bal_61_90+AgingList[i].BalOver90;
				}
				if(radio60.Checked){
					OverallBalance=AgingList[i].Bal_61_90+AgingList[i].BalOver90;
				}
				if(radio90.Checked){
					OverallBalance=AgingList[i].BalOver90;
				}
				if(OverallBalance <= 0){
					continue;
				}
				billingMatch=false;
				for(int b=0;b<listBillType.SelectedIndices.Count;b++){
					if(DefB.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndices[b]].DefNum==AgingList[i].BillingType){
						billingMatch=true;
						break;
					}
				}
				if(!billingMatch){
					continue;
				}
				Adjustment AdjustmentCur=new Adjustment();
				AdjustmentCur.PatNum=AgingList[i].PatNum;
				//AdjustmentCur.DateEntry=PIn.PDate(textDate.Text);//automatically handled
				AdjustmentCur.AdjDate=date;
				AdjustmentCur.ProcDate=date;
				AdjustmentCur.AdjType=PrefB.GetInt("FinanceChargeAdjustmentType");
				AdjustmentCur.AdjNote="Finance Charge";
				AdjustmentCur.AdjAmt=Math.Round(((PIn.PDouble(textAPR.Text) * .01d / 12d) * OverallBalance),2);
				AdjustmentCur.ProvNum=AgingList[i].PriProv;
				Adjustments.InsertOrUpdate(AdjustmentCur,true);
				rowsAffected++;
			}
			if(Prefs.UpdateString("FinanceChargeAPR",textAPR.Text)
				| Prefs.UpdateString("FinanceChargeLastRun",POut.PDate(date,false)))
			{
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			MessageBox.Show(Lan.g(this,"Finance Charges Added: ")+rowsAffected.ToString());
			FormAging FormA=new FormAging();
			FormA.SupressSameDateWarning=true;
			FormA.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	}
}
