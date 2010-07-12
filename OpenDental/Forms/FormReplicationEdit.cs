using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormReplicationEdit:Form {
		public ReplicationServer RepServ;

		public FormReplicationEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormReplicationEdit_Load(object sender,EventArgs e) {
			textDescript.Text=RepServ.Descript;
			textServerId.Text=RepServ.ServerId.ToString();
			if(RepServ.RangeStart!=0) {
				textRangeStart.Text=RepServ.RangeStart.ToString();
			}
			if(RepServ.RangeEnd!=0) {
				textRangeEnd.Text=RepServ.RangeEnd.ToString();
			}
			textAtoZpath.Text=RepServ.AtoZpath;
			checkUpdateBlocked.Checked=RepServ.UpdateBlocked;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(RepServ.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
				return;
			}
			ReplicationServers.DeleteObject(RepServ.ReplicationServerNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDescript.Text=="") {
				//I guess we don't need to force descript to have a value
			}
			if(textServerId.errorProvider1.GetError(textServerId) != "") {
				MsgBox.Show(this,"Please fix server_id.");
				return;
			}
			int serverid=PIn.Int(textServerId.Text);
			if(serverid==0) {
				MsgBox.Show(this,"Please enter a server_id number greater than zero.");
				return;
			}
			long rangeStart=0;
			if(textRangeStart.Text != "") {
				try {
					rangeStart=long.Parse(textRangeStart.Text);
				}
				catch {
					MsgBox.Show(this,"Please fix range start.");
					return;
				}
			}
			long rangeEnd=0;
			if(textRangeEnd.Text != "") {
				try {
					rangeEnd=long.Parse(textRangeEnd.Text);
				}
				catch {
					MsgBox.Show(this,"Please fix range end.");
					return;
				}
			}
			if(rangeEnd-rangeStart<999999){
				MsgBox.Show(this,"The end of the range must be at least 999,999 greater than the start of the range");
				return;
			}
			RepServ.Descript=textDescript.Text;
			RepServ.ServerId=serverid;//will be valid and greater than 0.
			RepServ.RangeStart=rangeStart;
			RepServ.RangeEnd=rangeEnd;
			RepServ.AtoZpath=textAtoZpath.Text;
			RepServ.UpdateBlocked=checkUpdateBlocked.Checked;
			if(RepServ.IsNew) {
				ReplicationServers.Insert(RepServ);
			}
			else {
				ReplicationServers.Update(RepServ);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}