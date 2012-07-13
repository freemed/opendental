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
			for(int i=1;i<Enum.GetNames(typeof(MessageTypeHL7)).Length;i++) {
				comboMsgType.Items.Add(Lan.g("enumMessageTypeHL7",Enum.GetName(typeof(MessageTypeHL7), i).ToString()));
			}
			for(int i=0;i<Enum.GetNames(typeof(EventTypeHL7)).Length;i++) {
				comboEventType.Items.Add(Lan.g("enumEventTypeHL7",Enum.GetName(typeof(EventTypeHL7),i).ToString()));
			}
			comboMsgType.SelectedIndex=(int)HL7DefMes.MessageType-1;
			comboEventType.SelectedIndex=(int)HL7DefMes.EventType;
			if(HL7DefMes.InOrOut==InOutHL7.Incoming) {
				radioButtonIn.Checked=true;
				textItemOrder.Visible=false;
				labelItemOrder.Visible=false;
				labelItemOrderDesc.Visible=false;
			}
			else {//Must be outgoing
				radioButtonOut.Checked=true;
				textItemOrder.Text=HL7DefMes.ItemOrder.ToString();
				textItemOrder.Visible=true;
				labelItemOrder.Visible=true;
				labelItemOrderDesc.Visible=true;
			}
			textNote.Text=HL7DefMes.Note;
			FillGridMain();
		}

		private void FillGridMain() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Segment Name"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Item Order"),75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Can Repeat"),75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"IsOptional"),75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			for(int i=0;i<HL7DefMes.hl7DefSegments.Count;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(HL7DefMes.hl7DefSegments[i].SegmentName.ToString());
				row.Cells.Add(HL7DefMes.hl7DefSegments[i].ItemOrder.ToString());
				row.Cells.Add(HL7DefMes.hl7DefSegments[i].CanRepeat.ToString());
				row.Cells.Add(HL7DefMes.hl7DefSegments[i].IsOptional.ToString());
				row.Cells.Add(HL7DefMes.hl7DefSegments[i].Note);
				row.Tag=HL7DefMes.hl7DefSegments[i];
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void radioButtonIn_Selected(object sender,System.EventArgs e) {
			textItemOrder.Visible=false;
			labelItemOrder.Visible=false;
			labelItemOrderDesc.Visible=false;
		}

		private void radioButtonOut_Selected(object sender,System.EventArgs e) {
			textItemOrder.Text=HL7DefMes.ItemOrder.ToString();
			textItemOrder.Visible=true;
			labelItemOrder.Visible=true;
			labelItemOrderDesc.Visible=true;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormHL7DefSegmentEdit FormS=new FormHL7DefSegmentEdit();
			FormS.HL7DefSeg=(HL7DefSegment)gridMain.Rows[e.Row].Tag;
			FormS.ShowDialog();
			FillGridMain();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}