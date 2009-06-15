using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormClaimPayTotal.
	/// </summary>
	public class FormClaimPayPreAuth:System.Windows.Forms.Form
	{
		private OpenDental.ValidDouble textTotal;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		///<summary></summary>
		public List<ClaimProc> ClaimProcsToEdit;
		private List<Procedure> ProcList;
		private Patient PatCur;
		private Family FamCur;
		private OpenDental.UI.ODGrid gridMain;
		private List <InsPlan> PlanList;

		///<summary></summary>
		public FormClaimPayPreAuth(Patient patCur,Family famCur,List <InsPlan> planList) {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClaimPayPreAuth));
			this.label1 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.textTotal = new OpenDental.ValidDouble();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label1.Location = new System.Drawing.Point(184,278);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84,16);
			this.label1.TabIndex = 117;
			this.label1.Text = "Total";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(8,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.OneCell;
			this.gridMain.Size = new System.Drawing.Size(661,257);
			this.gridMain.TabIndex = 125;
			this.gridMain.Title = "Procedures";
			this.gridMain.TranslationName = "TableClaimProc";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			this.gridMain.CellTextChanged += new System.EventHandler(this.gridMain_CellTextChanged);
			// 
			// textTotal
			// 
			this.textTotal.Location = new System.Drawing.Point(274,275);
			this.textTotal.Name = "textTotal";
			this.textTotal.ReadOnly = true;
			this.textTotal.Size = new System.Drawing.Size(55,20);
			this.textTotal.TabIndex = 119;
			this.textTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
			this.butCancel.Location = new System.Drawing.Point(594,331);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
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
			this.butOK.Location = new System.Drawing.Point(513,331);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormClaimPayPreAuth
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(686,366);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.textTotal);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimPayPreAuth";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Enter Estimates for PreAuth";
			this.Load += new System.EventHandler(this.FormClaimPayPreAuth_Load);
			this.Shown += new System.EventHandler(this.FormClaimPayTotal_Shown);
			this.Activated += new System.EventHandler(this.FormClaimPayTotal_Activated);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClaimPayPreAuth_Load(object sender, System.EventArgs e) {
			ProcList=Procedures.Refresh(PatCur.PatNum);
			FillGrid();
		}

		private void FormClaimPayTotal_Shown(object sender,EventArgs e) {
			InsPlan plan=InsPlans.GetPlan(ClaimProcsToEdit[0].PlanNum,PlanList);
			gridMain.SetSelected(new Point(4,0));
		}

		private void FillGrid(){
			//Changes made in this window do not get saved until after this window closes.
			//But if you double click on a row, then you will end up saving.  That shouldn't hurt anything, but could be improved.
			//also calculates totals for this "payment"
			//the payment itself is imaginary and is simply the sum of the claimprocs on this form
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g(this,"Code"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Tth"),35);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Fee"),55,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Estimate"),55,HorizontalAlignment.Right,true);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Remarks"),170,true);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			Procedure ProcCur;
			for(int i=0;i<ClaimProcsToEdit.Count;i++){
				row=new ODGridRow();
				row.Height=19;//To handle the isEditable functionality
				ProcCur=Procedures.GetProcFromList(ProcList,ClaimProcsToEdit[i].ProcNum);
				row.Cells.Add(ProcedureCodes.GetProcCode(ProcCur.CodeNum).ProcCode);
				row.Cells.Add(ProcCur.ToothNum);
				row.Cells.Add(ProcedureCodes.GetProcCode(ProcCur.CodeNum).Descript);
				row.Cells.Add(ClaimProcsToEdit[i].FeeBilled.ToString("F"));
				row.Cells.Add(ClaimProcsToEdit[i].InsPayEst.ToString("F"));
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
			List<ClaimProcHist> histList=null;
			List<ClaimProcHist> loopList=null;
			FormClaimProc FormCP=new FormClaimProc(ClaimProcsToEdit[e.Row],null,FamCur,PatCur,PlanList,histList,ref loopList);
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
			double insPayEst=0;
			for(int i=0;i<gridMain.Rows.Count;i++){
				try {
					insPayEst+=Convert.ToDouble(gridMain.Rows[i].Cells[4].Text);
				}
				catch { 
				
				}
			}
			textTotal.Text=insPayEst.ToString("F");
		}

		///<Summary>Surround with try-catch.</Summary>
		private void SaveGridChanges(){
			//validate all grid cells
			double dbl;
			for(int i=0;i<gridMain.Rows.Count;i++){
				if(gridMain.Rows[i].Cells[4].Text!=""){
					try{
						dbl=Convert.ToDouble(gridMain.Rows[i].Cells[4].Text);
					}
					catch{
						throw new ApplicationException(Lan.g(this,"Amount not valid: ")+gridMain.Rows[i].Cells[4].Text);
					}
				}
			}
			for(int i=0;i<ClaimProcsToEdit.Count;i++){
				ClaimProcsToEdit[i].InsPayEst=PIn.PDouble(gridMain.Rows[i].Cells[4].Text);
				ClaimProcsToEdit[i].Remarks=gridMain.Rows[i].Cells[5].Text;
			}
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







