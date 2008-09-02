using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormRecallsPat:Form {
		public int PatNum;
		///<summary>This is just the list for the current patient.</summary>
		private List<Recall> RecallList;

		public FormRecallsPat() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormRecallsPat_Load(object sender,EventArgs e) {
			/*
			//patient may or may not have existing recalls.
			Recall recallCur=null;
			for(int i=0;i<RecallList.Count;i++){
				if(RecallList[i].PatNum==PatCur.PatNum){
					recallCur=RecallList[i];
				}
			}*/
			//for testing purposes and because synchronization might have bugs, always synch here:
			//This might add a recall.
			Recalls.Synch(PatNum);			
			FillGrid();
		}

		private void FillGrid(){
			RecallList=Recalls.GetList(PatNum);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRecallsPat","Type"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallsPat","Disabled"),60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallsPat","PreviousDate"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallsPat","Due Date"),80);
			gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g("TableRecallsPat","Sched Date"),80);
			//gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallsPat","Interval"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallsPat","Status"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallsPat","Note"),80);
			gridMain.Columns.Add(col);

			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<RecallList.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(RecallTypes.GetDescription(RecallList[i].RecallTypeNum));
				if(RecallList[i].IsDisabled){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
				if(RecallList[i].DatePrevious.Year<1880){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(RecallList[i].DatePrevious.ToShortDateString());
				}
				if(RecallList[i].DateDue.Year<1880){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(RecallList[i].DateDue.ToShortDateString());
				}
				//row.Cells.Add("");//sched
				row.Cells.Add(RecallList[i].RecallInterval.ToString());
				row.Cells.Add(DefC.GetValue(DefCat.RecallUnschedStatus,RecallList[i].RecallStatus));
				row.Cells.Add(RecallList[i].Note);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormRecallEdit FormR=new FormRecallEdit();
			FormR.RecallCur=RecallList[e.Row].Copy();
			FormR.ShowDialog();
			FillGrid();
		}

		private void checkPerio_Click(object sender,EventArgs e) {

		}

		private void butAdd_Click(object sender,EventArgs e) {
			Recall recall=new Recall();
			recall.RecallTypeNum=0;//user will have to pick
			recall.PatNum=PatNum;
			recall.RecallInterval=new Interval(0,0,6,0);
			FormRecallEdit FormRE=new FormRecallEdit();
			FormRE.IsNew=true;
			FormRE.RecallCur=recall;
			FormRE.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		

		

		
		

		
	}
}