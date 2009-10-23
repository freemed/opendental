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
		private bool changed;

		public FormReplicationSetup() {
			InitializeComponent();
			Lan.F(this);
			changed=false;
		}

		private void FormReplicationSetup_Load(object sender,EventArgs e) {
			checkRandomPrimaryKeys.Checked=PrefC.GetBool(PrefName.RandomPrimaryKeys);
			if(checkRandomPrimaryKeys.Checked) {
				//not allowed to uncheck it
				checkRandomPrimaryKeys.Enabled=false;
			}
			FillGrid();
		}

		private void FillGrid(){
			ReplicationServers.RefreshCache();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("FormReplicationSetup","Description"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormReplicationSetup","server_id"),65);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormReplicationSetup","Key Range Start"),160);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormReplicationSetup","Key Range End"),160);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormReplicationSetup","AtoZ Path"),160);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormReplicationSetup","UpdateBlocked"),100);
			gridMain.Columns.Add(col);

			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ReplicationServers.Listt.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(ReplicationServers.Listt[i].Descript);
				row.Cells.Add(ReplicationServers.Listt[i].ServerId.ToString());
				row.Cells.Add(ReplicationServers.Listt[i].RangeStart.ToString("n0"));
				row.Cells.Add(ReplicationServers.Listt[i].RangeEnd.ToString("n0"));
				row.Cells.Add(ReplicationServers.Listt[i].AtoZpath);
				row.Cells.Add(ReplicationServers.Listt[i].UpdateBlocked ? "X" : "");
				gridMain.Rows.Add(row);
			}
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
				Prefs.UpdateBool(PrefName.RandomPrimaryKeys,true);
				DataValid.SetInvalid(InvalidType.Prefs);
			}
			else{//user just unchecked the box
				//this would only happen if the user had just enabled and then changed their mind
				//usually, the checkbox is disabled to prevent changing back
				Prefs.UpdateBool(PrefName.RandomPrimaryKeys,false);
				DataValid.SetInvalid(InvalidType.Prefs);
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormReplicationEdit FormR=new FormReplicationEdit();
			FormR.RepServ=ReplicationServers.Listt[e.Row];
			FormR.ShowDialog();
			if(FormR.DialogResult!=DialogResult.OK) {
				return;
			}
			changed=true;
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormReplicationEdit FormR=new FormReplicationEdit();
			FormR.RepServ=new ReplicationServer();
			FormR.ShowDialog();
			if(FormR.DialogResult!=DialogResult.OK) {
				return;
			}
			changed=true;
			FillGrid();
		}

		private void butSetRanges_Click(object sender,EventArgs e) {
			if(ReplicationServers.Listt.Count==0) {
				MessageBox.Show(Lan.g(this,"Please add at least one replication server to the list first"));
				return;
			}
			//long serverCount=ReplicationServers.Listt.Count;
			long offset=10000;
			long span=(long.MaxValue-offset) / (long)ReplicationServers.Listt.Count;//rounds down
			long counter=offset;
			for(int i=0;i<ReplicationServers.Listt.Count;i++) {
				ReplicationServers.Listt[i].RangeStart=counter;
				counter+=span-1;
				if(i==ReplicationServers.Listt.Count-1) {
					ReplicationServers.Listt[i].RangeEnd=long.MaxValue;
				}
				else {
					ReplicationServers.Listt[i].RangeEnd=counter;
					counter+=1;
				}
				ReplicationServers.WriteObject(ReplicationServers.Listt[i]);
			}
			changed=true;
			FillGrid();
		}

		private void butTest_Click(object sender,EventArgs e) {
			int server_id=ReplicationServers.Server_id;
			string msg="";
			if(server_id==0) {
				msg="server_id not set for this server.\r\n\r\n";
			}
			else {
				msg="server_id = "+server_id.ToString()+"\r\n\r\n";
			} 
			msg+="Sample generated keys:";
			long key;
			List<long> longlist=new List<long>();
			for(int i=0;i<15;i++){
				do{
					key=ReplicationServers.GetKey("patient","PatNum");
					//unfortunately this "random" key is based on time, so we need to ensure that the result set is unique.
					//I think it takes one millisecond to get each key this way.
				}
				while(longlist.Contains(key));
				longlist.Add(key);
				msg+="\r\n"+key.ToString("n0");
			}
			MessageBox.Show(msg);
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		private void FormReplicationSetup_FormClosing(object sender,FormClosingEventArgs e) {
			if(changed) {
				DataValid.SetInvalid(InvalidType.ReplicationServers);
			}
		}

		

		

		

	

		
		
	}
}