using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormClaimPayTotal.
	/// </summary>
	public class FormClaimPayTotal : System.Windows.Forms.Form
	{
		private OpenDental.ValidDouble textWriteOff;
		private System.Windows.Forms.TextBox textInsPayEst;
		private OpenDental.ValidDouble textInsPayAmt;
		private System.Windows.Forms.TextBox textClaimFee;
		private OpenDental.ValidDouble textDedApplied;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butDeductible;
		private OpenDental.UI.Button butWriteOff;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		///<summary></summary>
		public ClaimProc[] ClaimProcsToEdit;
		private Procedure[] ProcList;
		private Patient PatCur;
		private Family FamCur;
		private OpenDental.UI.ODGrid gridMain;
		private InsPlan[] PlanList;

		///<summary></summary>
		public FormClaimPayTotal(Patient patCur,Family famCur,InsPlan[] planList){
			InitializeComponent();// Required for Windows Form Designer support
			FamCur=famCur;
			PatCur=patCur;
			PlanList=planList;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClaimPayTotal));
			this.textInsPayEst = new System.Windows.Forms.TextBox();
			this.textClaimFee = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butWriteOff = new OpenDental.UI.Button();
			this.butDeductible = new OpenDental.UI.Button();
			this.textWriteOff = new OpenDental.ValidDouble();
			this.textInsPayAmt = new OpenDental.ValidDouble();
			this.textDedApplied = new OpenDental.ValidDouble();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textInsPayEst
			// 
			this.textInsPayEst.Location = new System.Drawing.Point(476,275);
			this.textInsPayEst.Name = "textInsPayEst";
			this.textInsPayEst.ReadOnly = true;
			this.textInsPayEst.Size = new System.Drawing.Size(65,20);
			this.textInsPayEst.TabIndex = 116;
			this.textInsPayEst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textClaimFee
			// 
			this.textClaimFee.Location = new System.Drawing.Point(346,275);
			this.textClaimFee.Name = "textClaimFee";
			this.textClaimFee.ReadOnly = true;
			this.textClaimFee.Size = new System.Drawing.Size(65,20);
			this.textClaimFee.TabIndex = 118;
			this.textClaimFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label1.Location = new System.Drawing.Point(226,278);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116,16);
			this.label1.TabIndex = 117;
			this.label1.Text = "Totals";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(337,325);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(348,39);
			this.label2.TabIndex = 122;
			this.label2.Text = "Before you click OK, the Deductible and the Ins Pay amounts should exactly match " +
    "the insurance EOB.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(11,283);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116,34);
			this.label3.TabIndex = 123;
			this.label3.Text = "Assign to selected procedure:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(155,289);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(108,29);
			this.label4.TabIndex = 124;
			this.label4.Text = "On all unpaid amounts:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(8,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.OneCell;
			this.gridMain.Size = new System.Drawing.Size(939,257);
			this.gridMain.TabIndex = 125;
			this.gridMain.Title = "Procedures";
			this.gridMain.TranslationName = "TableClaimProc";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			this.gridMain.CellTextChanged += new System.EventHandler(this.gridMain_CellTextChanged);
			// 
			// butWriteOff
			// 
			this.butWriteOff.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butWriteOff.Autosize = true;
			this.butWriteOff.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butWriteOff.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butWriteOff.CornerRadius = 4F;
			this.butWriteOff.Location = new System.Drawing.Point(154,324);
			this.butWriteOff.Name = "butWriteOff";
			this.butWriteOff.Size = new System.Drawing.Size(90,25);
			this.butWriteOff.TabIndex = 121;
			this.butWriteOff.Text = "&Write Off";
			this.butWriteOff.Click += new System.EventHandler(this.butWriteOff_Click);
			// 
			// butDeductible
			// 
			this.butDeductible.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDeductible.Autosize = true;
			this.butDeductible.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDeductible.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDeductible.CornerRadius = 4F;
			this.butDeductible.Location = new System.Drawing.Point(14,324);
			this.butDeductible.Name = "butDeductible";
			this.butDeductible.Size = new System.Drawing.Size(92,25);
			this.butDeductible.TabIndex = 120;
			this.butDeductible.Text = "&Deductible";
			this.butDeductible.Click += new System.EventHandler(this.butDeductible_Click);
			// 
			// textWriteOff
			// 
			this.textWriteOff.Location = new System.Drawing.Point(606,275);
			this.textWriteOff.Name = "textWriteOff";
			this.textWriteOff.ReadOnly = true;
			this.textWriteOff.Size = new System.Drawing.Size(65,20);
			this.textWriteOff.TabIndex = 119;
			this.textWriteOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textInsPayAmt
			// 
			this.textInsPayAmt.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textInsPayAmt.Location = new System.Drawing.Point(541,275);
			this.textInsPayAmt.Name = "textInsPayAmt";
			this.textInsPayAmt.ReadOnly = true;
			this.textInsPayAmt.Size = new System.Drawing.Size(65,20);
			this.textInsPayAmt.TabIndex = 115;
			this.textInsPayAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDedApplied
			// 
			this.textDedApplied.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textDedApplied.Location = new System.Drawing.Point(411,275);
			this.textDedApplied.Name = "textDedApplied";
			this.textDedApplied.ReadOnly = true;
			this.textDedApplied.Size = new System.Drawing.Size(65,20);
			this.textDedApplied.TabIndex = 114;
			this.textDedApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(846,324);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 2;
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
			this.butOK.Location = new System.Drawing.Point(757,324);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormClaimPayTotal
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(948,363);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butWriteOff);
			this.Controls.Add(this.butDeductible);
			this.Controls.Add(this.textWriteOff);
			this.Controls.Add(this.textInsPayEst);
			this.Controls.Add(this.textInsPayAmt);
			this.Controls.Add(this.textClaimFee);
			this.Controls.Add(this.textDedApplied);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimPayTotal";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Enter Payment";
			this.Shown += new System.EventHandler(this.FormClaimPayTotal_Shown);
			this.Activated += new System.EventHandler(this.FormClaimPayTotal_Activated);
			this.Load += new System.EventHandler(this.FormClaimPayTotal_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClaimPayTotal_Load(object sender, System.EventArgs e) {
			ProcList=Procedures.Refresh(PatCur.PatNum);
			FillGrid();
		}

		private void FormClaimPayTotal_Shown(object sender,EventArgs e) {
			gridMain.SetSelected(new Point(8,0));
		}

		private void FillGrid(){
			//Changes made in this window do not get saved until after this window closes.
			//But if you double click on a row, then you will end up saving.  That shouldn't hurt anything, but could be improved.
			//also calculates totals for this "payment"
			//the payment itself is imaginary and is simply the sum of the claimprocs on this form
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableClaimProc","Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Prov"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Code"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Tth"),35);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Description"),130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Fee Billed"),65,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Deduct"),65,HorizontalAlignment.Right,true);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Ins Est"),65,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Ins Pay"),65,HorizontalAlignment.Right,true);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Writeoff"),65,HorizontalAlignment.Right,true);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Status"),50,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Pmt"),40,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimProc","Remarks"),170,true);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			Procedure ProcCur;
			for(int i=0;i<ClaimProcsToEdit.Length;i++){
				row=new ODGridRow();
				row.Height=19;//To handle the isEditable functionality
				row.Cells.Add(ClaimProcsToEdit[i].ProcDate.ToShortDateString());
				row.Cells.Add(Providers.GetAbbr(ClaimProcsToEdit[i].ProvNum));
				if(ClaimProcsToEdit[i].ProcNum==0) {
					row.Cells.Add("");
					row.Cells.Add("");
					row.Cells.Add(Lan.g(this,"Total Payment"));
				}
				else {
					ProcCur=Procedures.GetProc(ProcList,ClaimProcsToEdit[i].ProcNum);
					row.Cells.Add(ProcedureCodes.GetProcCode(ProcCur.CodeNum).ProcCode);
					row.Cells.Add(ProcCur.ToothNum);
					row.Cells.Add(ProcedureCodes.GetProcCode(ProcCur.CodeNum).Descript);
				}
				row.Cells.Add(ClaimProcsToEdit[i].FeeBilled.ToString("F"));
				row.Cells.Add(ClaimProcsToEdit[i].DedApplied.ToString("F"));
				row.Cells.Add(ClaimProcsToEdit[i].InsPayEst.ToString("F"));
				row.Cells.Add(ClaimProcsToEdit[i].InsPayAmt.ToString("F"));
				row.Cells.Add(ClaimProcsToEdit[i].WriteOff.ToString("F"));
				switch(ClaimProcsToEdit[i].Status){
					case ClaimProcStatus.Received:
						row.Cells.Add("Recd");
						break;
					case ClaimProcStatus.NotReceived:
						row.Cells.Add("");
						break;
					//adjustment would never show here
					case ClaimProcStatus.Preauth:
						row.Cells.Add("PreA");
						break;
					case ClaimProcStatus.Supplemental:
						row.Cells.Add("Supp");
						break;
					case ClaimProcStatus.CapClaim:
						row.Cells.Add("Cap");
						break;
					//Estimate would never show here
					//Cap would never show here
				}
				if(ClaimProcsToEdit[i].ClaimPaymentNum>0){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
				row.Cells.Add(ClaimProcsToEdit[i].Remarks);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			FillTotals();
		}

		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			try{
				SaveGridChanges();
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			FormClaimProc FormCP=new FormClaimProc(ClaimProcsToEdit[e.Row],null,FamCur,PlanList);
			FormCP.IsInClaim=true;
			//no need to worry about permissions here
			FormCP.ShowDialog();
			if(FormCP.DialogResult!=DialogResult.OK){
				return;
			}
			FillGrid();
			FillTotals();
		}

		private void gridMain_CellTextChanged(object sender,EventArgs e) {
			FillTotals();
		}

		///<Summary>Fails silently if text is in invalid format.</Summary>
		private void FillTotals(){
			double claimFee=0;
			double dedApplied=0;
			double insPayEst=0;
			double insPayAmt=0;
			double writeOff=0;
			double amt;
			for(int i=0;i<gridMain.Rows.Count;i++){
				claimFee+=ClaimProcsToEdit[i].FeeBilled;//5
				try{//6.deduct
					dedApplied+=Convert.ToDouble(gridMain.Rows[i].Cells[6].Text);
				}catch{}
				insPayEst+=ClaimProcsToEdit[i].InsPayEst;//7
				try{//8.inspayest
					insPayAmt+=Convert.ToDouble(gridMain.Rows[i].Cells[8].Text);
				}catch{}
				try{//9.writeoff
					writeOff+=Convert.ToDouble(gridMain.Rows[i].Cells[9].Text);
				}catch{}
			}
			textClaimFee.Text=claimFee.ToString("F");
			textDedApplied.Text=dedApplied.ToString("F");
			textInsPayEst.Text=insPayEst.ToString("F");
			textInsPayAmt.Text=insPayAmt.ToString("F");
			textWriteOff.Text=writeOff.ToString("F");
		}

		///<Summary>Surround with try-catch.</Summary>
		private void SaveGridChanges(){
			//validate all grid cells
			double dbl;
			for(int i=0;i<gridMain.Rows.Count;i++){
				if(gridMain.Rows[i].Cells[6].Text!=""){//deduct
					try{
						dbl=Convert.ToDouble(gridMain.Rows[i].Cells[6].Text);
					}
					catch{
						throw new ApplicationException(Lan.g(this,"Deductible not valid: ")+gridMain.Rows[i].Cells[6].Text);
					}
				}
				if(gridMain.Rows[i].Cells[8].Text!=""){//inspay
					try{
						dbl=Convert.ToDouble(gridMain.Rows[i].Cells[8].Text);
					}
					catch{
						throw new ApplicationException(Lan.g(this,"Ins Pay not valid: ")+gridMain.Rows[i].Cells[8].Text);
					}
				}
				if(gridMain.Rows[i].Cells[9].Text!=""){//writeoff
					try{
						dbl=Convert.ToDouble(gridMain.Rows[i].Cells[9].Text);
						if(dbl<0){
							throw new ApplicationException(Lan.g(this,"Writeoff cannot be negative: ")+gridMain.Rows[i].Cells[9].Text);
						}
					}
					catch{
						throw new ApplicationException(Lan.g(this,"Writeoff not valid: ")+gridMain.Rows[i].Cells[9].Text);
					}
				}
			}
			for(int i=0;i<ClaimProcsToEdit.Length;i++){
				ClaimProcsToEdit[i].DedApplied=PIn.PDouble(gridMain.Rows[i].Cells[6].Text);
				ClaimProcsToEdit[i].InsPayAmt=PIn.PDouble(gridMain.Rows[i].Cells[8].Text);
				ClaimProcsToEdit[i].WriteOff=PIn.PDouble(gridMain.Rows[i].Cells[9].Text);
				ClaimProcsToEdit[i].Remarks=gridMain.Rows[i].Cells[12].Text;
			}
		}

		private void butDeductible_Click(object sender, System.EventArgs e) {
			if(gridMain.SelectedCell.X==-1){
				MessageBox.Show(Lan.g(this,"Please select one procedure.  Then click this button to assign the deductible to that procedure."));
				return;
			}
			try {
				SaveGridChanges();
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			Double dedAmt=0;
			//remove the existing deductible from each procedure and move it to dedAmt.
			for(int i=0;i<ClaimProcsToEdit.Length;i++){
				if(ClaimProcsToEdit[i].DedApplied > 0){
					dedAmt+=ClaimProcsToEdit[i].DedApplied;
					ClaimProcsToEdit[i].InsPayEst+=ClaimProcsToEdit[i].DedApplied;//dedAmt might be more
					ClaimProcsToEdit[i].InsPayAmt+=ClaimProcsToEdit[i].DedApplied;
					ClaimProcsToEdit[i].DedApplied=0;
				}
			}
			if(dedAmt==0){
				MessageBox.Show(Lan.g(this,"There does not seem to be a deductible to apply.  You can still apply a deductible manually by double clicking on a procedure."));
				return;
			}
			//then move dedAmt to the selected proc
			ClaimProcsToEdit[gridMain.SelectedIndices[0]].DedApplied=dedAmt;
			ClaimProcsToEdit[gridMain.SelectedIndices[0]].InsPayEst-=dedAmt;
			ClaimProcsToEdit[gridMain.SelectedIndices[0]].InsPayAmt-=dedAmt;
			FillGrid();
		}

		private void butWriteOff_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Write off unpaid amount on each procedure?"),""
				,MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			try {
				SaveGridChanges();
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			//fix later: does not take into account other payments.
			double unpaidAmt=0;
			Procedure[] ProcList=Procedures.Refresh(PatCur.PatNum);
			for(int i=0;i<ClaimProcsToEdit.Length;i++){
				unpaidAmt=Procedures.GetProc(ProcList,ClaimProcsToEdit[i].ProcNum).ProcFee
					//((Procedure)Procedures.HList[ClaimProcsToEdit[i].ProcNum]).ProcFee
					-ClaimProcsToEdit[i].DedApplied
					-ClaimProcsToEdit[i].InsPayAmt;
				if(unpaidAmt > 0){
					ClaimProcsToEdit[i].WriteOff=unpaidAmt;
				}
			}
			FillGrid();
		}

		private void butOK_Click(object sender,System.EventArgs e) {
			try {
				SaveGridChanges();
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormClaimPayTotal_Activated(object sender,EventArgs e) {

		}

		

		

		

		

		

		



	}
}







