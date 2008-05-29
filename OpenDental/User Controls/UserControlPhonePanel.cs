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
		public UserControlPhonePanel() {
			InitializeComponent();
		}

		private void UserControlPhonePanel_Load(object sender,EventArgs e) {
			FillEmps();
		}

		private void FillEmps(){
			List<Schedule> schedList=Schedules.GetDayList(DateTime.Today);
			Employees.Refresh();
			gridEmp.BeginUpdate();
			gridEmp.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableEmpClock","Employee"),60);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","Status"),130);
			gridEmp.Columns.Add(col);
			gridEmp.Rows.Clear();
			UI.ODGridRow row;
			bool isOff;
			for(int i=0;i<Employees.ListShort.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(Employees.ListShort[i].FName);
				isOff=IsOff(Employees.ListShort[i].EmployeeNum,schedList);
				if(isOff){
					row.Cells.Add("off");
					row.ColorText=Color.Gray;
				}
				else{
					row.Cells.Add(Employees.ListShort[i].ClockStatus);
				}
				if(Employees.ListShort[i].ClockStatus=="Working"){
					row.ColorBackG=Color.FromArgb(153,220,153);
				}
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

		private bool IsOff(int employeeNum,List<Schedule> schedList){
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
			FillEmps();
		}

	}
}
