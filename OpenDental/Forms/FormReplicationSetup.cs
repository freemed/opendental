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
	public partial class FormReplicationSetup:Form {
		public FormReplicationSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormReplicationSetup_Load(object sender,EventArgs e) {
			checkRandomPrimaryKeys.Checked=PrefC.GetBool("RandomPrimaryKeys");
			if(checkRandomPrimaryKeys.Checked) {
				//not allowed to uncheck it
				checkRandomPrimaryKeys.Enabled=false;
			}
			FillGrid();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("FormReplicationSetup","Server Name or Descript"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormReplicationSetup","MySQL server_id"),100);
			gridMain.Columns.Add(col);
			 
			gridMain.Rows.Clear();
			ODGridRow row;
			/*
			for(int i=0;i<List.Length;i++){
				row=new ODGridRow();
				row.Cells.Add("");
				row.Cells.Add("");
			  
				gridMain.Rows.Add(row);
			}*/
			gridMain.EndUpdate();
		}


		private void checkRandomPrimaryKeys_Click(object sender,System.EventArgs e) {
			if(checkRandomPrimaryKeys.Checked) {
				if(MessageBox.Show("Are you absolutely sure you want to enable random primary keys?\r\n"
					+"Advantages:\r\n"
					+"Multiple servers can stay synchronized using merge replication.\r\n"
					+"Realtime connection between servers not required.\r\n"
					+"Data can be entered on all servers and synchronized later.\r\n"
					+"Disadvantages:\r\n"
					+"Slightly slower.\r\n"
					+"Difficult to set up.\r\n"
					+"Primary keys much longer, so not as user friendly.","",MessageBoxButtons.OKCancel)==DialogResult.Cancel) 
				{
					checkRandomPrimaryKeys.Checked=false;
					return;
				}
				//immediately make the change
				Prefs.UpdateBool("RandomPrimaryKeys",true);
				DataValid.SetInvalid(InvalidType.Prefs);
			}
			else{//user just unchecked the box
				//this would only happen if the user had just enabled and then changed their mind
				//usually, the checkbox is disabled to prevent changing back
				Prefs.UpdateBool("RandomPrimaryKeys",false);
				DataValid.SetInvalid(InvalidType.Prefs);
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {

		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

	

		
		
	}
}