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
	public partial class FormPhoneGraphDateEdit:Form {
		public DateTime DateEdit;
		private List<PhoneGraph> ListPhoneGraphsForDate;
		private List<PhoneEmpDefault> ListPhoneEmpDefaults;
		private List<Schedule> ListSchedulesForDate;

		public FormPhoneGraphDateEdit(DateTime dateEdit) {
			InitializeComponent();
			Lan.F(this);
			DateEdit=dateEdit;			
		}

		private void FormPhoneGraphDateEdit_Load(object sender,EventArgs e) {
			gridGraph.Title=DateEdit.ToShortDateString();			
			FillGrid();
		}

		private void FillGrid() {
			//get PhoneGraph entries for this date
			ListPhoneGraphsForDate=PhoneGraphs.GetAllForDate(DateEdit);
			//get current employee defaults
			ListPhoneEmpDefaults=PhoneEmpDefaults.Refresh();
			ListPhoneEmpDefaults.Sort(new PhoneEmpDefaults.PhoneEmpDefaultComparer(PhoneEmpDefaults.PhoneEmpDefaultComparer.SortBy.name));
			//get schedules
			ListSchedulesForDate=Schedules.GetDayList(DateEdit);
			long selectedEmployeeNum=-1;
			if(gridGraph.Rows.Count>=1 
				&& gridGraph.GetSelectedIndex()>=0 
				&& gridGraph.Rows[gridGraph.GetSelectedIndex()].Tag!=null 
				&& gridGraph.Rows[gridGraph.GetSelectedIndex()].Tag is PhoneEmpDefault) 
			{
					selectedEmployeeNum=((PhoneEmpDefault)gridGraph.Rows[gridGraph.GetSelectedIndex()].Tag).EmployeeNum;
			}
			gridGraph.BeginUpdate();
			gridGraph.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePhoneGraphDate","Employee"),80);
			gridGraph.Columns.Add(col); //column 0 (name - not clickable)
			col=new ODGridColumn(Lan.g("TablePhoneGraphDate","Schedule"),130);
			col.TextAlign=HorizontalAlignment.Center;
			gridGraph.Columns.Add(col); //column 1 (schedule - clickable)
			col=new ODGridColumn(Lan.g("TablePhoneGraphDate","Graph Default"),90);
			col.TextAlign=HorizontalAlignment.Center;
			gridGraph.Columns.Add(col); //column 2 (default - not clickable)
			col=new ODGridColumn(Lan.g("TablePhoneGraphDate","Set Graph Status"),110);
			col.TextAlign=HorizontalAlignment.Center;
			gridGraph.Columns.Add(col); //column 3 (set graph status - clickable)
			col=new ODGridColumn(Lan.g("TablePhoneGraphDate","Is Overridden?"),80);
			col.TextAlign=HorizontalAlignment.Center;
			gridGraph.Columns.Add(col); //column 4 (is value an overridde of default? - not clickable)
			gridGraph.Rows.Clear();
			int selectedRow=-1;
			//loop through all employee defaults and create 1 row per employee
			for(int iPED=0;iPED<ListPhoneEmpDefaults.Count;iPED++) {
				PhoneEmpDefault phoneEmpDefault=ListPhoneEmpDefaults[iPED];
				Employee employee=Employees.GetEmp(phoneEmpDefault.EmployeeNum);
				if(employee==null || employee.IsHidden) {//only deal with current employees
					continue;
				}
				List<Schedule> scheduleForEmployee=Schedules.GetForEmployee(ListSchedulesForDate,phoneEmpDefault.EmployeeNum);
				bool isGraphed=phoneEmpDefault.IsGraphed; //set default
				bool hasOverride=false;
				for(int iPG=0;iPG<ListPhoneGraphsForDate.Count;iPG++) {//we have a default, now loop through all known exceptions and find a match
					PhoneGraph phoneGraph=ListPhoneGraphsForDate[iPG];
					if(phoneEmpDefault.EmployeeNum==ListPhoneGraphsForDate[iPG].EmployeeNum) {//found a match so no op necessary for this employee
						isGraphed=phoneGraph.IsGraphed;
						hasOverride=true;
						break;
					}
				}
				ODGridRow row;
				row=new ODGridRow();
				row.Cells.Add(phoneEmpDefault.EmpName); //column 0 (name - not clickable)
				row.Cells.Add(Schedules.GetCommaDelimStringForScheds(scheduleForEmployee).Replace(", ","\r\n")); //column 1 (shift times - not clickable)
				row.Cells.Add(phoneEmpDefault.IsGraphed?"X":""); //column 2 (default - not clickable)
				row.Cells.Add(isGraphed?"X":""); //column 3 (set graph status - clickable)
				row.Cells.Add(hasOverride && isGraphed!=phoneEmpDefault.IsGraphed?"X":""); //column 4 (is overridden to IsGraphed? - not clickable)
				row.Tag=phoneEmpDefault; //store the employee for click events
				int rowIndex=gridGraph.Rows.Add(row);
				if(selectedEmployeeNum==phoneEmpDefault.EmployeeNum) {
					selectedRow=rowIndex;
				}
			}
			gridGraph.EndUpdate();
			if(selectedRow>=0) {
				gridGraph.SetSelected(selectedRow,true);
			}
		}

		private void gridGraph_CellClick(object sender,ODGridClickEventArgs e) {
			//only allow clicking on the 'Desired Graph Status' column
			if(e.Col!=3 || gridGraph.Rows[e.Row].Tag==null || !(gridGraph.Rows[e.Row].Tag is PhoneEmpDefault)) {
				return;
			}
			PhoneEmpDefault phoneEmpDefault=(PhoneEmpDefault)gridGraph.Rows[e.Row].Tag;
			bool uiGraphStatus=gridGraph.Rows[e.Row].Cells[e.Col].Text.ToLower()=="x";
			bool dbGraphStatus=PhoneEmpDefaults.GetGraphedStatusForEmployeeDate(phoneEmpDefault.EmployeeNum,DateEdit);
			if(uiGraphStatus!=dbGraphStatus) {
				MessageBox.Show(Lan.g(this,"Graph status has changed unexpectedly for employee: ")+phoneEmpDefault.EmpName+Lan.g(this,". Exit and reopen this form, and try again."));
				return;
			}
			//flip the bit in the db and reload the grid
			PhoneGraph pg=new PhoneGraph();
			pg.EmployeeNum=phoneEmpDefault.EmployeeNum;
			pg.DateEntry=DateEdit;
			pg.IsGraphed=!uiGraphStatus; //flip the bit
			PhoneGraphs.InsertOrUpdate(pg); //update the db	
			FillGrid();
		}
		
		private void butEditSchedule_Click(object sender,EventArgs e) {
			//allow user to edit this day's schedule
			FormScheduleDayEdit FormS=new FormScheduleDayEdit(DateEdit);
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			//schedules may have changed to make a log entry
			SecurityLogs.MakeLogEntry(Permissions.Schedules,0,"In 'FormPhoneGraphDateEdit', user edited daily schedule for "+DateEdit.ToShortDateString());
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			this.Close();
		}
	}
}