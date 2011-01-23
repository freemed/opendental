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
	public partial class FormTaskSetup:Form {
		private List<Userod> UserList;
		private List<Userod> UserListOld;
		private List<TaskList> TrunkList;

		public FormTaskSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormTaskSetup_Load(object sender,EventArgs e) {
			UserList=Userods.GetNotHidden();
			UserListOld=Userods.GetNotHidden();
			TrunkList=TaskLists.RefreshMainTrunk(Security.CurUser.UserNum);
			listMain.Items.Add(Lan.g(this,"none"));
			for(int i=0;i<TrunkList.Count;i++){
				listMain.Items.Add(TrunkList[i].Descript);
			}
			FillGrid();
		}

		private void FillGrid(){
			//doesn't refresh from db because nothing actually gets saved until we hit the OK button.
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableTaskSetup","User"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTaskSetup","Inbox"),100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<UserList.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(UserList[i].UserName);
				row.Cells.Add(GetDescription(UserList[i].TaskListInBox));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private string GetDescription(long taskListNum) {
			if(taskListNum==0){
				return "";
			}
			for(int i=0;i<TrunkList.Count;i++){
				if(TrunkList[i].TaskListNum==taskListNum){
					return TrunkList[i].Descript;
				}
			}
			return "";
		}

		private void butSet_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select a user first.");
				return;
			}
			if(listMain.SelectedIndex==-1){
				MsgBox.Show(this,"Please select an item from the list first.");
				return;
			}
			if(listMain.SelectedIndex==0){
				UserList[gridMain.GetSelectedIndex()].TaskListInBox=0;
			}
			else{
				UserList[gridMain.GetSelectedIndex()].TaskListInBox=TrunkList[listMain.SelectedIndex-1].TaskListNum;
			}
			FillGrid();
			listMain.SelectedIndex=-1;
		}

		private void butOK_Click(object sender,EventArgs e) {
			bool changed=false;
			for(int i=0;i<UserList.Count;i++){
				if(UserList[i].TaskListInBox!=UserListOld[i].TaskListInBox){
					Userods.Update(UserList[i]);
					changed=true;
				}
			}
			if(changed){
				DataValid.SetInvalid(InvalidType.Security);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}