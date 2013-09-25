using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormUserPick:Form {
		private List<Userod> shortList;
		///<summary>If this form closes with OK, then this value will be filled.</summary>
		public long SelectedUserNum;

		public FormUserPick() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormUserPick_Load(object sender,EventArgs e) {
			shortList=UserodC.ShortList;
			for(int i=0;i<shortList.Count;i++) {
				listUser.Items.Add(shortList[i]);
			}
		}

		private void listUser_DoubleClick(object sender,EventArgs e) {
			if(listUser.SelectedIndex==-1) {
				return;
			}
			if(!Security.IsAuthorized(Permissions.TaskEdit,true) && Userods.GetInbox(shortList[listUser.SelectedIndex].UserNum)!=0) {
				MsgBox.Show(this,"Please select a user that does not have an inbox.");
				return;
			}
			SelectedUserNum=shortList[listUser.SelectedIndex].UserNum;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(listUser.SelectedIndex==-1) {
				MsgBox.Show(this,"Please pick a user first.");
				return;
			}
			if(!Security.IsAuthorized(Permissions.TaskEdit,true) && Userods.GetInbox(shortList[listUser.SelectedIndex].UserNum)!=0) {
				MsgBox.Show(this,"Please select a user that does not have an inbox.");
				return;
			}
			SelectedUserNum=shortList[listUser.SelectedIndex].UserNum;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}