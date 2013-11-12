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
		///<summary>This should be set to the result from EhrTriggers.TriggerMatch.  Key is a string that contains the message to be displayed.  
		///The value is a list of objects to be passed to form infobutton.</summary>
		public SortedDictionary<string,List<object>> DictEhrTriggerResults;
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
			col=new ODGridColumn("Conditions",90);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<_table.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(_table.Rows[i][0].ToString());//infobutton
				row.Cells.Add(_table.Rows[i][1].ToString());
				row.Tag=(List<object>)_table.Rows[i][2];
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			if(e.Col!=0) {
				return;//not infobutton
			}
			FormInfobutton FormIB=new FormInfobutton();
			FormIB.ListObjects=(List<object>)gridMain.Rows[e.Row].Tag;
			FormIB.ShowDialog();
		}

		///<summary>Run after assigning value to DictEhrTriggerResults.  FormCDSIntervention will display if needed, otherwise Dialogresult will be null.</summary>
		public void ShowIfRequired() {
			_table=new DataTable();
			_table.Columns.Add("");//infobutton
			_table.Columns.Add("");//Description and match conditions
			_table.Columns.Add("",typeof(List<object>));//Used to store the lsit of matched objects to later be passed to formInfobutton.
			//test data---
			DataRow row=_table.NewRow();
			row[0]="0";
			List<object> lobj=new List<object>();
			Random rand=new Random();
			lobj.Add(ICD9s.GetOne(rand.Next(8000)));//condition 1
			lobj.Add(Snomeds.GetOne(rand.Next(400000)));//condition 2
			row[1]="Description of the trigger goes here.\r\n  -"+((ICD9)lobj[0]).ICD9Code.PadRight(15)+":"+((ICD9)lobj[0]).Description+"\r\n  -"+((Snomed)lobj[1]).SnomedCode.PadRight(15)+":"+((Snomed)lobj[1]).Description;
			row[2]=lobj;
			_table.Rows.Add(row);
			this.ShowDialog();
			return;

			//Testing above this line, code below this line is the "actual" code.
			if(DictEhrTriggerResults==null || DictEhrTriggerResults.Count==0) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			_table.Columns.Add("");//infobutton
			_table.Columns.Add("");//Description and match conditions
			_table.Columns.Add("");//Used to store the lsit of matched objects to later be passed to formInfobutton.
			foreach(KeyValuePair<string,List<object>> kvp in DictEhrTriggerResults) {
				//DataRow row=_table.NewRow();
				row[0]="0";
				row[1]=kvp.Key;
				row[2]=kvp.Value;
				_table.Rows.Add(row);
			}
			if(_table.Rows.Count==0) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			this.ShowDialog();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}