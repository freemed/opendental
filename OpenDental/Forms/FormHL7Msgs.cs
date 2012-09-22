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
	public partial class FormHL7Msgs:Form {
		private long SelectedPatNum;
		private List<HL7Msg> MsgList;

		public FormHL7Msgs() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormHL7Msgs_Load(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.AddDays(-7).ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
			comboHL7Status.Items.Add(Lan.g(this,"All"));
			comboHL7Status.SelectedIndex=0;
			for(int i=0;i<Enum.GetNames(typeof(HL7MessageStatus)).Length;i++) {
				comboHL7Status.Items.Add(Enum.GetName(typeof(HL7MessageStatus),i));
			}
			SelectedPatNum=0;
			FillGrid();
		}

		private void FillGrid() {
			if(textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateEnd.errorProvider1.GetError(textDateEnd)!="")
			{
				return;
			}
			if(SelectedPatNum>0) {
				Patient pat=Patients.GetLim(SelectedPatNum);
				textPatient.Text=pat.GetNameLF();
			}
			MsgList=HL7Msgs.GetHL7Msgs(PIn.Date(textDateStart.Text),PIn.Date(textDateEnd.Text),SelectedPatNum,comboHL7Status.SelectedIndex);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"DateTime"),180);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Patient"),170);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"AptNum"),60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Status"),75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),400);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			if(MsgList!=null) {
			  for(int i=0;i<MsgList.Count;i++) {
					ODGridRow row=new ODGridRow();
					row.Cells.Add(MsgList[i].DateTStamp.ToString());
					if(MsgList[i].PatNum>0) {
						row.Cells.Add(Patients.GetLim(MsgList[i].PatNum).GetNameFL());
					}
					else {
						row.Cells.Add("");
					}
					if(MsgList[i].AptNum>0) {
						row.Cells.Add(MsgList[i].AptNum.ToString());
					}
					else {
						row.Cells.Add("");
					}
					row.Cells.Add(Enum.GetName(typeof(HL7MessageStatus),MsgList[i].HL7Status));
					row.Cells.Add(MsgList[i].Note);
					gridMain.Rows.Add(row);
				}
			}
			gridMain.EndUpdate();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butFind_Click(object sender,EventArgs e) {
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.SelectionModeOnly=true;
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK) {
				return;
			}
			SelectedPatNum=FormPS.SelectedPatNum;
			FillGrid();
		}

		private void comboHL7Status_SelectedIndexChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			FormHL7MsgEdit FormS=new FormHL7MsgEdit();
			FormS.MsgCur=MsgList[e.Row];
			FormS.ShowDialog();
			FillGrid();
		}

		private void butAll_Click(object sender,EventArgs e) {
			SelectedPatNum=0;
			textPatient.Text="";
			FillGrid();
		}

	}
}