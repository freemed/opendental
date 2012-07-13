using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	/// <summary></summary>
	public partial class FormHL7DefMessageEdit:System.Windows.Forms.Form {
		public HL7DefMessage HL7DefMes;
		///<summary></summary>
		public FormHL7DefMessageEdit() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		private void FormHL7DefMessageEdit_Load(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup,true)) {
				gridMain.Enabled=false;
				groupBox1.Enabled=false;
			}
			FillGridMain();
			comboMsgType.Items.Add(Lan.g(this,MessageTypeHL7.ADT.ToString()));
			comboMsgType.Items.Add(Lan.g(this,MessageTypeHL7.DFT.ToString()));
			comboMsgType.Items.Add(Lan.g(this,MessageTypeHL7.ORU.ToString()));
			comboMsgType.Items.Add(Lan.g(this,MessageTypeHL7.SIU.ToString()));
			comboMsgType.Items.Add(Lan.g(this,MessageTypeHL7.VXU.ToString()));
			comboEventType.Items.Add(Lan.g(this,EventTypeHL7.A04.ToString()));
			comboEventType.Items.Add(Lan.g(this,EventTypeHL7.S12.ToString()));
			//textDescription.Text=HL7DefMes.
		}

		private void FillGridMain() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Item Order"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Can Repeat"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"IsOptional"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			gridMain.EndUpdate();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}