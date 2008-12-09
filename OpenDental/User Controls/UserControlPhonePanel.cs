using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class UserControlPhonePanel:UserControl {
		DataTable tablePhone;
		///<summary>When the GoToChanged event fires, this tells us which patnum.</summary>
		public int GotoPatNum;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when user wants to go to a patient or related object.")]
		public event EventHandler GoToChanged=null;
		private int rowI;

		public UserControlPhonePanel() {
			InitializeComponent();
		}

		private void UserControlPhonePanel_Load(object sender,EventArgs e) {
			timer1.Enabled=true;
			FillEmps();
		}

		protected void OnGoToChanged() {
			if(GoToChanged!=null) {
				GoToChanged(this,new EventArgs());
			}
		}

		private void FillEmps(){
			gridEmp.BeginUpdate();
			gridEmp.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g("TableEmpClock","Ext"),25);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","Employee"),60);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","Status"),50);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","Phone"),50);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","InOut"),35);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","Customer"),90);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","Time"),70);
			gridEmp.Columns.Add(col);
			gridEmp.Rows.Clear();
			UI.ODGridRow row;
			tablePhone=Employees.GetPhoneTable();
			DateTime dateTimeStart;
			TimeSpan span;
			DateTime timeOfDay;//because TimeSpan does not have good formatting.
			for(int i=0;i<tablePhone.Rows.Count;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(tablePhone.Rows[i]["Extension"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["EmployeeName"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["ClockStatus"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["Description"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["InOrOut"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["CustomerNumber"].ToString());
				dateTimeStart=PIn.PDateT(tablePhone.Rows[i]["DateTimeStart"].ToString());
				if(dateTimeStart.Date==DateTime.Today){
					span=DateTime.Now-dateTimeStart;
					timeOfDay=DateTime.Today+span;
					row.Cells.Add(timeOfDay.ToString("H:mm:ss"));
				}
				else{
					row.Cells.Add("");
				}
				row.ColorBackG=Color.FromArgb(PIn.PInt(tablePhone.Rows[i]["ColorBar"].ToString()));
				row.ColorText=Color.FromArgb(PIn.PInt(tablePhone.Rows[i]["ColorText"].ToString()));
				gridEmp.Rows.Add(row);
			}
			gridEmp.EndUpdate();
			gridEmp.SetSelected(false);
		}

		/*private void FillMetrics(){
			gridMetrics.BeginUpdate();
			gridMetrics.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g("TablePhoneMetrics","Description"),40);
			gridMetrics.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePhoneMetrics","#"),60);
			gridMetrics.Columns.Add(col);
			gridMetrics.Rows.Clear();
			UI.ODGridRow row;
			tablePhone=Employees.GetPhoneMetricTable();
			for(int i=0;i<tablePhone.Rows.Count;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(tablePhone.Rows[i]["Description"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["MetricVal"].ToString());
				row.ColorText=Color.FromArgb(PIn.PInt(tablePhone.Rows[i]["ColorText"].ToString()));
				gridMetrics.Rows.Add(row);
			}
			gridMetrics.EndUpdate();
			gridMetrics.SetSelected(false);
		}*/

		private void timer1_Tick(object sender,EventArgs e) {
			//For now, happens once per 1.6 seconds regardless of phone activity.
			//This might need improvement.
			FillEmps();
		}

		private void butOverride_Click(object sender,EventArgs e) {
			FormPhoneOverrides FormO=new FormPhoneOverrides();
			FormO.ShowDialog();
		}

		private void gridEmp_CellClick(object sender,ODGridClickEventArgs e) {
			if((e.Button & MouseButtons.Right)==MouseButtons.Right){
				return;
			}
			int patNum=PIn.PInt(tablePhone.Rows[e.Row]["PatNum"].ToString());
			GotoPatNum=patNum;
			OnGoToChanged();
		}

		private void menuItemManage_Click(object sender,EventArgs e) {
			/*
			int patNum=PIn.PInt(tablePhone.Rows[rowI]["PatNum"].ToString());
			if(patNum==0){

			}
			FormPhoneNumbersManage FormM=new FormPhoneNumbersManage();
			FormM.PatNum=patNum;
			FormM.ShowDialog();*/
		}

		private void menuItemAdd_Click(object sender,EventArgs e) {
			/*
			int patNum=
				//PIn.PInt(tablePhone.Rows[rowI]["PatNum"].ToString());
			PhoneNumber ph=new PhoneNumber();
			ph.PatNum=patNum;
			ph.PhoneNumberVal=tablePhone.Rows[rowI]["PatNum"].ToString();
			PhoneNumbers.WriteObject(ph);
			FormPhoneNumbersManage FormM=new FormPhoneNumbersManage();
			FormM.PatNum=patNum;
			FormM.ShowDialog();*/
		}

		private void gridEmp_MouseUp(object sender,MouseEventArgs e) {
			rowI=gridEmp.PointToRow(e.Y);
			if(e.Button==MouseButtons.Right) {
				contextMenuStrip1.Show(gridEmp,e.Location);
			}
		}

	}
}
