using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

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
		private OpenDental.TableClaimProc tbProc;
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
		private InsPlan[] PlanList;

		///<summary></summary>
		public FormClaimPayTotal(Patient patCur,Family famCur,InsPlan[] planList){
			InitializeComponent();// Required for Windows Form Designer support
			FamCur=famCur;
			PatCur=patCur;
			PlanList=planList;
			Lan.F(this);
			tbProc.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbProc_CellDoubleClicked);
			
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
			this.tbProc = new OpenDental.TableClaimProc();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textWriteOff = new OpenDental.ValidDouble();
			this.textInsPayEst = new System.Windows.Forms.TextBox();
			this.textInsPayAmt = new OpenDental.ValidDouble();
			this.textClaimFee = new System.Windows.Forms.TextBox();
			this.textDedApplied = new OpenDental.ValidDouble();
			this.label1 = new System.Windows.Forms.Label();
			this.butDeductible = new OpenDental.UI.Button();
			this.butWriteOff = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tbProc
			// 
			this.tbProc.BackColor = System.Drawing.SystemColors.Window;
			this.tbProc.Location = new System.Drawing.Point(8,19);
			this.tbProc.Name = "tbProc";
			this.tbProc.ScrollValue = 62;
			this.tbProc.SelectedIndices = new int[0];
			this.tbProc.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbProc.Size = new System.Drawing.Size(939,253);
			this.tbProc.TabIndex = 0;
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
			// textWriteOff
			// 
			this.textWriteOff.Location = new System.Drawing.Point(606,275);
			this.textWriteOff.Name = "textWriteOff";
			this.textWriteOff.ReadOnly = true;
			this.textWriteOff.Size = new System.Drawing.Size(65,20);
			this.textWriteOff.TabIndex = 119;
			this.textWriteOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
			// textClaimFee
			// 
			this.textClaimFee.Location = new System.Drawing.Point(346,275);
			this.textClaimFee.Name = "textClaimFee";
			this.textClaimFee.ReadOnly = true;
			this.textClaimFee.Size = new System.Drawing.Size(65,20);
			this.textClaimFee.TabIndex = 118;
			this.textClaimFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
			// FormClaimPayTotal
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(948,363);
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
			this.Controls.Add(this.tbProc);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimPayTotal";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Enter Payment";
			this.Load += new System.EventHandler(this.FormClaimPayTotal_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClaimPayTotal_Load(object sender, System.EventArgs e) {
			ProcList=Procedures.Refresh(PatCur.PatNum);
			FillGrid();
		}

		private void FillGrid(){
			//Nothing in this entire for is ever saved to the database.
			//also calculates totals for this "payment"
			//the payment itself is imaginary and is simply the sum of the claimprocs on this form
			double claimFee=0;
			double dedApplied=0;
			double insPayEst=0;
			double insPayAmt=0;
			double writeOff=0;
			tbProc.ResetRows(ClaimProcsToEdit.Length);
			Procedure ProcCur;
			for(int i=0;i<ClaimProcsToEdit.Length;i++){
				if(ClaimProcsToEdit[i].ProcNum==0){
					tbProc.Cell[4,i]=Lan.g(this,"Total Payment");
				}
				else{
					ProcCur=Procedures.GetProc(ProcList,ClaimProcsToEdit[i].ProcNum);
					//Procedures.CurOld=Procedures.Cur;//may not be necessary
					tbProc.Cell[2,i]=ProcedureCodes.GetProcCode(ProcCur.CodeNum).ProcCode;
					tbProc.Cell[3,i]=ProcCur.ToothNum;
					tbProc.Cell[4,i]=ProcedureCodes.GetProcCode(ProcCur.CodeNum).Descript;
				}
				tbProc.Cell[0,i]=ClaimProcsToEdit[i].ProcDate.ToShortDateString();
				tbProc.Cell[1,i]=Providers.GetAbbr(ClaimProcsToEdit[i].ProvNum);
				tbProc.Cell[5,i]=ClaimProcsToEdit[i].FeeBilled.ToString("F");
				tbProc.Cell[6,i]=ClaimProcsToEdit[i].DedApplied.ToString("F");
				tbProc.Cell[7,i]=ClaimProcsToEdit[i].InsPayEst.ToString("F");
				tbProc.Cell[8,i]=ClaimProcsToEdit[i].InsPayAmt.ToString("F");
				tbProc.Cell[9,i]=ClaimProcsToEdit[i].WriteOff.ToString("F");
				switch(ClaimProcsToEdit[i].Status){
					case ClaimProcStatus.Received:
						tbProc.Cell[10,i]="Recd";
						break;
					case ClaimProcStatus.NotReceived:
						tbProc.Cell[10,i]="";
						break;
					//adjustment would never show here
					case ClaimProcStatus.Preauth:
						tbProc.Cell[10,i]="PreA";
						break;
					case ClaimProcStatus.Supplemental:
						tbProc.Cell[10,i]="Supp";
						break;
					case ClaimProcStatus.CapClaim:
						tbProc.Cell[10,i]="Cap";
						break;
					//Estimate would never show here
					//Cap would never show here
				}
				if(ClaimProcsToEdit[i].ClaimPaymentNum>0)
					tbProc.Cell[11,i]="X";
				tbProc.Cell[12,i]=ClaimProcsToEdit[i].Remarks;
				claimFee+=ClaimProcsToEdit[i].FeeBilled;
				dedApplied+=ClaimProcsToEdit[i].DedApplied;
				insPayEst+=ClaimProcsToEdit[i].InsPayEst;
				insPayAmt+=ClaimProcsToEdit[i].InsPayAmt;
				writeOff+=ClaimProcsToEdit[i].WriteOff;
			}
			tbProc.SetGridColor(Color.LightGray);
			tbProc.LayoutTables();
			textClaimFee.Text=claimFee.ToString("F");
			textDedApplied.Text=dedApplied.ToString("F");
			textInsPayEst.Text=insPayEst.ToString("F");
			textInsPayAmt.Text=insPayAmt.ToString("F");
			textWriteOff.Text=writeOff.ToString("F");
		}

		private void tbProc_CellDoubleClicked(object sender, CellEventArgs e){
			FormClaimProc FormCP=new FormClaimProc(ClaimProcsToEdit[e.Row],null,FamCur,PlanList);
			FormCP.IsInClaim=true;
			//no need to worry about permissions here
			FormCP.ShowDialog();
			if(FormCP.DialogResult!=DialogResult.OK){
				return;
			}
			FillGrid();
		}

		private void butDeductible_Click(object sender, System.EventArgs e) {
			if(tbProc.SelectedIndices.Length!=1){
				MessageBox.Show(Lan.g(this,"Please select one procedure.  Then click this button to assign the deductible to that procedure."));
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
			ClaimProcsToEdit[tbProc.SelectedIndices[0]].DedApplied=dedAmt;
			ClaimProcsToEdit[tbProc.SelectedIndices[0]].InsPayEst-=dedAmt;
			ClaimProcsToEdit[tbProc.SelectedIndices[0]].InsPayAmt-=dedAmt;
			FillGrid();
		}

		private void butWriteOff_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Write off unpaid amount on each procedure?"),""
				,MessageBoxButtons.OKCancel)!=DialogResult.OK){
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

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		

		



	}
}







