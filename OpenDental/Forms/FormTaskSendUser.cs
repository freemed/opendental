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
	public partial class FormTaskSendUser:Form {
		private List<TaskList> FilteredList;
		///<summary>If OK, then this will contain the selected TaskListNum</summary>
		public long TaskListNum;

		public FormTaskSendUser() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormTaskSendUser_Load(object sender,EventArgs e) {
			List<Userod> UserList=Userods.GetNotHidden();
			List<TaskList> TrunkList=TaskLists.RefreshMainTrunk();
			FilteredList=new List<TaskList>();
			for(int i=0;i<UserList.Count;i++){
				if(UserList[i].TaskListInBox==0){
					continue;
				}
				for(int t=0;t<TrunkList.Count;t++){
					if(TrunkList[t].TaskListNum==UserList[i].TaskListInBox){
						FilteredList.Add(TrunkList[t]);
						listMain.Items.Add(TrunkList[t].Descript);
					}
				}
			}
		}

		private void listMain_DoubleClick(object sender,EventArgs e) {
			if(listMain.SelectedIndex==-1){
				return;
			}
			TaskListNum=FilteredList[listMain.SelectedIndex].TaskListNum;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(listMain.SelectedIndex==-1){
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			TaskListNum=FilteredList[listMain.SelectedIndex].TaskListNum;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		
	}
}