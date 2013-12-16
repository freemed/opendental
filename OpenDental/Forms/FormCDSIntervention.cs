using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormCDSIntervention:Form {
		///<summary>This should be set to the result from EhrTriggers.TriggerMatch.</summary>
		public List<CDSIntervention> ListCDSI;
		/////<summary>This should be set to the result from EhrTriggers.TriggerMatch.  Key is a string that contains the message to be displayed.  
		/////The value is a list of objects to be passed to form infobutton.</summary>
		//public Dictionary<string,List<object>> DictEhrTriggerResults;
		///<summary>Used for assembling the Interventions, values set using ShowIfRequired().</summary>
		private DataTable _table;

		public FormCDSIntervention() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormCDSIntervention_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("",18);//infobutton
			col.ImageList=imageListInfoButton;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Conditions",300);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Instructions",400);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Bibliography",120);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<_table.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(_table.Rows[i][0].ToString());//infobutton
				row.Cells.Add(_table.Rows[i][1].ToString());//Trigger Text
				row.Cells.Add(_table.Rows[i][2].ToString());//TriggerInstructions
				row.Cells.Add(_table.Rows[i][3].ToString());//Bibliography
				row.Tag=(List<object>)_table.Rows[i][4];//List of objects to be sent to FormInfobutton;
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			if(e.Col!=0) {
				return;//not infobutton column
			}
			FormInfobutton FormIB=new FormInfobutton();
			FormIB.ListObjects=(List<object>)gridMain.Rows[e.Row].Tag;
			FormIB.ShowDialog();
		}

		public void ShowIfRequired() {
			ShowIfRequired(true);
		}

		///<summary>Run after assigning value to DictEhrTriggerResults.  FormCDSIntervention will display if needed, otherwise Dialogresult will be null.</summary>
		public void ShowIfRequired(bool showCancelButton) {
			if(ListCDSI==null || ListCDSI.Count==0) {
				DialogResult=DialogResult.Cancel;
				return;//No interventions matched.
			}
			_table=new DataTable();
			_table.Columns.Add("");//infobutton
			_table.Columns.Add("");//Conditions = Description and match conditions
			_table.Columns.Add("");//Instructions
			_table.Columns.Add("");//Bibliographic information
			_table.Columns.Add("",typeof(List<object>));//Used to store the list of matched objects to later be passed to formInfobutton.
			foreach(CDSIntervention cdsi in ListCDSI) {
				DataRow row=_table.NewRow();
				row[0]="0";
				row[1]=cdsi.InterventionMessage;
				row[2]=cdsi.EhrTrigger.Instructions;
				row[3]=cdsi.EhrTrigger.Bibliography;
				row[4]=cdsi.TriggerObjects;
				_table.Rows.Add(row);
			}
			if(_table.Rows.Count==0) {
				DialogResult=DialogResult.Cancel;
				return;//should never happen
			}
			butCancel.Visible=showCancelButton;
			this.ShowDialog();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Abort;
		}

	}
}