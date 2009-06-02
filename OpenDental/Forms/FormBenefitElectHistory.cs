using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormBenefitElectHistory:Form {
		private List<Etrans> list;
		private int PlanNum;

		public FormBenefitElectHistory(int planNum) {
			InitializeComponent();
			Lan.F(this);
			PlanNum=planNum;
		}

		private void FormBenefitElectHistory_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			list=Etranss.GetList270ForPlan(PlanNum);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Response"),100);
			gridMain.Columns.Add(col);
			 
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<list.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(list[i].DateTimeTrans.ToShortDateString());
				row.Cells.Add(list[i].Note);
			  
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEtrans270Edit formE=new FormEtrans270Edit();
			formE.EtransCur=list[e.Row];
			formE.ShowDialog();
			FillGrid();
		}

		//private void butOK_Click(object sender,EventArgs e) {
		//	DialogResult=DialogResult.OK;
		//}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}