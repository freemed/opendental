using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormClaimPayEdit : System.Windows.Forms.Form{
		private OpenDental.ValidDouble textAmount;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.TextBox textBankBranch;
		private System.Windows.Forms.TextBox textCheckNum;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private OpenDental.TableClaimPaySplits tb2;
		private System.ComponentModel.Container components = null;
		//private bool ControlDown;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.CheckBox checkShowUn;
		private OpenDental.UI.Button butDelete;
		private double splitTot;
		///<summary>The list of splits to display in the grid.</summary>
		private ClaimPaySplit[] splits;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		private System.Windows.Forms.TextBox textCarrierName;
		private System.Windows.Forms.Label label7;
		private ClaimPayment ClaimPaymentCur;
		///<summary>Set this externally.</summary>
		public int OriginatingClaimNum;

		///<summary></summary>
		public FormClaimPayEdit(ClaimPayment claimPaymentCur){
			InitializeComponent();// Required for Windows Form Designer support
			ClaimPaymentCur=claimPaymentCur;
			tb2.CellClicked += new OpenDental.ContrTable.CellEventHandler(tb2_CellClicked);
			tb2.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tb2_CellDoubleClicked);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClaimPayEdit));
			this.textAmount = new OpenDental.ValidDouble();
			this.textDate = new OpenDental.ValidDate();
			this.textBankBranch = new System.Windows.Forms.TextBox();
			this.textCheckNum = new System.Windows.Forms.TextBox();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.tb2 = new OpenDental.TableClaimPaySplits();
			this.checkShowUn = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.textCarrierName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(668,56);
			this.textAmount.Name = "textAmount";
			this.textAmount.ReadOnly = true;
			this.textAmount.Size = new System.Drawing.Size(58,20);
			this.textAmount.TabIndex = 44;
			this.textAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(668,36);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(68,20);
			this.textDate.TabIndex = 0;
			this.textDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBankBranch
			// 
			this.textBankBranch.Location = new System.Drawing.Point(668,96);
			this.textBankBranch.MaxLength = 25;
			this.textBankBranch.Name = "textBankBranch";
			this.textBankBranch.Size = new System.Drawing.Size(100,20);
			this.textBankBranch.TabIndex = 2;
			// 
			// textCheckNum
			// 
			this.textCheckNum.Location = new System.Drawing.Point(668,76);
			this.textCheckNum.MaxLength = 25;
			this.textCheckNum.Name = "textCheckNum";
			this.textCheckNum.Size = new System.Drawing.Size(100,20);
			this.textCheckNum.TabIndex = 1;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(558,174);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(324,70);
			this.textNote.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(570,40);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96,16);
			this.label6.TabIndex = 37;
			this.label6.Text = "Payment Date";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(571,60);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(95,16);
			this.label5.TabIndex = 36;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(575,78);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(90,16);
			this.label4.TabIndex = 35;
			this.label4.Text = "Check #";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(576,99);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(91,16);
			this.label3.TabIndex = 34;
			this.label3.Text = "Bank-Branch";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(560,154);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(132,16);
			this.label2.TabIndex = 33;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(803,631);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(803,593);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 6;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// tb2
			// 
			this.tb2.BackColor = System.Drawing.SystemColors.Window;
			this.tb2.Location = new System.Drawing.Point(8,20);
			this.tb2.Name = "tb2";
			this.tb2.ScrollValue = 1;
			this.tb2.SelectedIndices = new int[0];
			this.tb2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tb2.Size = new System.Drawing.Size(539,634);
			this.tb2.TabIndex = 49;
			// 
			// checkShowUn
			// 
			this.checkShowUn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowUn.Location = new System.Drawing.Point(564,373);
			this.checkShowUn.Name = "checkShowUn";
			this.checkShowUn.Size = new System.Drawing.Size(215,24);
			this.checkShowUn.TabIndex = 4;
			this.checkShowUn.Text = "Show Unattached";
			this.checkShowUn.Click += new System.EventHandler(this.checkShowUn_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(562,632);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(129,35);
			this.label1.TabIndex = 51;
			this.label1.Text = "(Deletes this Check, but not any splits)";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(565,597);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(92,26);
			this.butDelete.TabIndex = 52;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(668,14);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(209,21);
			this.comboClinic.TabIndex = 92;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(579,18);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(86,14);
			this.labelClinic.TabIndex = 91;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCarrierName
			// 
			this.textCarrierName.Location = new System.Drawing.Point(668,116);
			this.textCarrierName.MaxLength = 25;
			this.textCarrierName.Name = "textCarrierName";
			this.textCarrierName.Size = new System.Drawing.Size(212,20);
			this.textCarrierName.TabIndex = 93;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(558,119);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(109,16);
			this.label7.TabIndex = 94;
			this.label7.Text = "Carrier Name";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormClaimPayEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(902,676);
			this.Controls.Add(this.textCarrierName);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkShowUn);
			this.Controls.Add(this.tb2);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.textBankBranch);
			this.Controls.Add(this.textCheckNum);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimPayEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Insurance Claim Check";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormClaimPayEdit_Closing);
			this.Load += new System.EventHandler(this.FormClaimPayEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClaimPayEdit_Load(object sender, System.EventArgs e) {
			if(IsNew){
				//ClaimPaymentCur=new ClaimPayment();
				//ClaimPayments.InsertCur();//assigns a ClaimPaymentNum for use below
				checkShowUn.Checked=true;
			}
			if(PrefB.GetBool("EasyNoClinics")){
				comboClinic.Visible=false;
				labelClinic.Visible=false;
			}
			comboClinic.Items.Clear();
			comboClinic.Items.Add(Lan.g(this,"none"));
			comboClinic.SelectedIndex=0;
			for(int i=0;i<Clinics.List.Length;i++){
				comboClinic.Items.Add(Clinics.List[i].Description);
				if(Clinics.List[i].ClinicNum==ClaimPaymentCur.ClinicNum){
					comboClinic.SelectedIndex=i+1;
				}
			}
			//if(ClaimPaymentCur.CheckDate.Year < 1880){
			//	textDate.Text=DateTime.Today.ToShortDateString();
			//}
			//else
			textDate.Text=ClaimPaymentCur.CheckDate.ToShortDateString();
			//textAmount is handled in FillTable
			textCheckNum.Text=ClaimPaymentCur.CheckNum;
			textBankBranch.Text=ClaimPaymentCur.BankBranch;
			textCarrierName.Text=ClaimPaymentCur.CarrierName;
			textNote.Text=ClaimPaymentCur.Note;
			FillTable();
			if(IsNew){
				tb2.SetSelected(true);
				splitTot=0;
				for(int i=0;i<tb2.SelectedIndices.Length;i++){
					splitTot+=splits[tb2.SelectedIndices[i]].InsPayAmt;//Claims.ListQueue
				}
				textAmount.Text=splitTot.ToString("F");
			}
		}

		private void FillTable(){
			splits=Claims.RefreshByCheck(ClaimPaymentCur.ClaimPaymentNum,checkShowUn.Checked);
			tb2.ResetRows(splits.Length);
			tb2.SetGridColor(Color.LightGray);
			splitTot=0;
			for (int i=0;i<splits.Length;i++){
				tb2.Cell[0,i]=splits[i].DateClaim.ToShortDateString();
				tb2.Cell[1,i]=splits[i].ProvAbbr;
				tb2.Cell[2,i]=splits[i].PatName;
				tb2.Cell[3,i]=splits[i].Carrier;
				tb2.Cell[4,i]=splits[i].FeeBilled.ToString("F");
				tb2.Cell[5,i]=splits[i].InsPayAmt.ToString("F");
				if(splits[i].ClaimPaymentNum==ClaimPaymentCur.ClaimPaymentNum){
					tb2.SetSelected(i,true);
					splitTot+=splits[i].InsPayAmt;
				}
				if(splits[i].ClaimNum==OriginatingClaimNum){
					for(int j=0;j<tb2.MaxCols;j++){
						tb2.FontBold[j,i]=true;
					}
				}
			}
			tb2.LayoutTables();
			textAmount.Text=splitTot.ToString("F");
		}

		private void tb2_CellClicked(object sender, CellEventArgs e){
			splitTot=0;
			for (int i=0;i<tb2.SelectedIndices.Length;i++){
				splitTot+=splits[tb2.SelectedIndices[i]].InsPayAmt;
			}
			textAmount.Text=splitTot.ToString("F");
		}

		private void tb2_CellDoubleClicked(object sender, CellEventArgs e){

		}

		private void checkShowUn_Click(object sender, System.EventArgs e) {
			FillTable();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete this insurance check?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			try{
				ClaimPayments.Delete(ClaimPaymentCur);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDate.errorProvider1.GetError(textDate)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(tb2.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"At least one item must be selected, or use the delete button."));	
				return;
			}
			if(comboClinic.SelectedIndex==0){
				ClaimPaymentCur.ClinicNum=0;
			}
			else{
				ClaimPaymentCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			}
			ClaimPaymentCur.CheckAmt=PIn.PDouble(textAmount.Text);
			ClaimPaymentCur.CheckDate=PIn.PDate(textDate.Text);
			ClaimPaymentCur.CheckNum=textCheckNum.Text;
			ClaimPaymentCur.BankBranch=textBankBranch.Text;
			ClaimPaymentCur.CarrierName=textCarrierName.Text;
			ClaimPaymentCur.Note=textNote.Text;
			try{
				ClaimPayments.Update(ClaimPaymentCur);//error thrown if trying to change amount and already attached to a deposit.
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			//this could be optimized to only save changes.
			//Would require a starting AL to compare to.
			//But this isn't bad, since changes all saved at the very end
			ArrayList ALselected=new ArrayList();
			for(int i=0;i<tb2.SelectedIndices.Length;i++){
				ALselected.Add(tb2.SelectedIndices[i]);
			}
			for(int i=0;i<splits.Length;i++){
				if(ALselected.Contains(i)){//row is selected
					ClaimProcs.SetForClaim(splits[i].ClaimNum,ClaimPaymentCur.ClaimPaymentNum,
						ClaimPaymentCur.CheckDate,true);
				}
				else{//row not selected
					ClaimProcs.SetForClaim(splits[i].ClaimNum,ClaimPaymentCur.ClaimPaymentNum,
						ClaimPaymentCur.CheckDate,false);
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void FormClaimPayEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){//cancel
				//ClaimProcs never saved in the first place
				ClaimPayments.Delete(ClaimPaymentCur);
			}
		}



	}
}













