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

		public UserControlPhonePanel() {
			InitializeComponent();
		}

		private void UserControlPhonePanel_Load(object sender,EventArgs e) {
			timer1.Enabled=true;
			FillEmps();
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
			col=new ODGridColumn(Lan.g("TableEmpClock","Phone"),70);
			gridEmp.Columns.Add(col);
			gridEmp.Rows.Clear();
			UI.ODGridRow row;
			tablePhone=Employees.GetPhoneTable();
			for(int i=0;i<tablePhone.Rows.Count;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(tablePhone.Rows[i]["Extension"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["EmployeeName"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["ClockStatus"].ToString());
				row.Cells.Add(tablePhone.Rows[i]["Description"].ToString());
				row.ColorBackG=Color.FromArgb(PIn.PInt(tablePhone.Rows[i]["ColorBar"].ToString()));
				row.ColorText=Color.FromArgb(PIn.PInt(tablePhone.Rows[i]["ColorText"].ToString()));
				/*
					EmployeeListSorted[i].PhoneExt.ToString());
				row.Cells.Add(EmployeeListSorted[i].FName);
				isOff=IsOff(EmployeeListSorted[i].EmployeeNum);
				isBusy=IsBusy(EmployeeListSorted[i].PhoneExt);
				if(isOff){
					row.Cells.Add("off");
					row.ColorText=Color.Gray;
				}
				else{
					row.Cells.Add(EmployeeListSorted[i].ClockStatus);
				}
				row.Cells.Add(GetPhoneDescription(EmployeeListSorted[i].PhoneExt));
				if(isBusy){
					row.ColorBackG=Color.Salmon;
				}
				else if(EmployeeListSorted[i].ClockStatus=="Working"){
					row.ColorBackG=Color.FromArgb(153,220,153);
				}*/
				gridEmp.Rows.Add(row);
			}
			gridEmp.EndUpdate();
			/*listStatus.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(TimeClockStatus)).Length;i++){
				listStatus.Items.Add(Lan.g("enumTimeClockStatus",Enum.GetNames(typeof(TimeClockStatus))[i]));
			}
			for(int i=0;i<Employees.ListShort.Length;i++){
				if(Employees.ListShort[i].EmployeeNum==Security.CurUser.EmployeeNum){
					SelectEmpI(i);
					return;
				}
			}*/
			gridEmp.SetSelected(false);
		}

		/*private bool IsBusy(int phoneExt){
			for(int i=0;i<tablePhone.Rows.Count;i++){
				if(tablePhone.Rows[i]["Extension"].ToString()==phoneExt.ToString()){
					if(tablePhone.Rows[i]["ExtStatus"].ToString()=="1"){
						return true;
					}
					else{
						return false;
					}
				}
			}
			return false;
		}

		private string GetPhoneDescription(int phoneExt){
			for(int i=0;i<tablePhone.Rows.Count;i++){
				if(tablePhone.Rows[i]["Extension"].ToString()==phoneExt.ToString()){
					return tablePhone.Rows[i]["Description"].ToString();
				}
			}
			return "";
		}

		private bool IsOff(int employeeNum){
			if(schedList==null){
				return false;
			}
			for(int i=0;i<schedList.Count;i++){
				if(schedList[i].EmployeeNum!=employeeNum){
					continue;
				}
				//if(schedList[i].
				//entry found
				return false;
			}
			return true;
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			schedList=Schedules.GetDayList(DateTime.Today);
			Employees.Refresh();
			FillEmps();
		}*/

		private void timer1_Tick(object sender,EventArgs e) {
			//For now, happens once per second regardless of phone activity.
			//This might need improvement.
			FillEmps();
		}

	}
}
