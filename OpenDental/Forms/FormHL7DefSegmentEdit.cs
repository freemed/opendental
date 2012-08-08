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
	public partial class FormHL7DefSegmentEdit:System.Windows.Forms.Form {
		public HL7DefSegment HL7DefSegCur;
		public bool IsHL7DefInternal;

		///<summary></summary>
		public FormHL7DefSegmentEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormHL7DefSegmentEdit_Load(object sender,EventArgs e) {
			FillGrid();
			for(int i=0;i<Enum.GetNames(typeof(SegmentNameHL7)).Length;i++) {
				comboSegmentName.Items.Add(Lan.g("enumSegmentNameHL7",Enum.GetName(typeof(SegmentNameHL7),i).ToString()));
			}
			if(HL7DefSegCur!=null) {
				comboSegmentName.SelectedIndex=(int)HL7DefSegCur.SegmentName;
				textItemOrder.Text=HL7DefSegCur.ItemOrder.ToString();
				checkCanRepeat.Checked=HL7DefSegCur.CanRepeat;
				checkIsOptional.Checked=HL7DefSegCur.IsOptional;
				textNote.Text=HL7DefSegCur.Note;
			}
			if(IsHL7DefInternal) {
				butOK.Enabled=false;
				butDelete.Enabled=false;
				labelDelete.Visible=true;
				butAdd.Enabled=false;
			}
		}

		private void FillGrid() {
			if(!IsHL7DefInternal && !HL7DefSegCur.IsNew) {
				HL7DefSegCur.hl7DefFields=HL7DefFields.GetForDefSegment(HL7DefSegCur.HL7DefSegmentNum);
			}
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Field"),140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Type"),40);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Order"),40,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Table ID"),75);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			if(HL7DefSegCur!=null && HL7DefSegCur.hl7DefFields!=null) {
				for(int i=0;i<HL7DefSegCur.hl7DefFields.Count;i++) {
					ODGridRow row=new ODGridRow();
					row.Cells.Add(HL7DefSegCur.hl7DefFields[i].FieldName);
					row.Cells.Add(Lan.g("enumDataTypeHL7",HL7DefSegCur.hl7DefFields[i].DataType.ToString()));
					row.Cells.Add(HL7DefSegCur.hl7DefFields[i].OrdinalPos.ToString());
					row.Cells.Add(HL7DefSegCur.hl7DefFields[i].TableId);
					gridMain.Rows.Add(row);
				}
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormHL7DefFieldEdit FormS=new FormHL7DefFieldEdit();
			FormS.HL7DefFieldCur=HL7DefSegCur.hl7DefFields[e.Row];
			FormS.IsHL7DefInternal=IsHL7DefInternal;
			FormS.ShowDialog();
			FillGrid();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete Segment?")) {
				return;
			}
			for(int f=0;f<HL7DefSegCur.hl7DefFields.Count;f++) {
				HL7DefFields.Delete(HL7DefSegCur.hl7DefFields[f].HL7DefFieldNum);
			}
			HL7DefSegments.Delete(HL7DefSegCur.HL7DefSegmentNum);
			DialogResult=DialogResult.OK;
		}

		private void butAdd_Click(object sender,EventArgs e) {
			if(HL7DefSegCur.IsNew) {
				HL7DefSegments.Insert(HL7DefSegCur);
				HL7DefSegCur.IsNew=false;
			}
			FormHL7DefFieldEdit FormS=new FormHL7DefFieldEdit();
			FormS.HL7DefFieldCur=new HL7DefField();
			FormS.HL7DefFieldCur.HL7DefSegmentNum=HL7DefSegCur.HL7DefSegmentNum;
			FormS.HL7DefFieldCur.IsNew=true;
			FormS.IsHL7DefInternal=false;
			FormS.ShowDialog();
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			//not enabled if internal
			if(textItemOrder.errorProvider1.GetError(textItemOrder)!="") {
				MsgBox.Show(this,"Please fix data entry error first.");
				return;
			}
			HL7DefSegCur.SegmentName=(SegmentNameHL7)comboSegmentName.SelectedIndex;
			HL7DefSegCur.ItemOrder=PIn.Int(textItemOrder.Text);
			HL7DefSegCur.CanRepeat=checkCanRepeat.Checked;
			HL7DefSegCur.IsOptional=checkIsOptional.Checked;
			HL7DefSegCur.Note=textNote.Text;
			if(HL7DefSegCur.IsNew) {
				HL7DefSegments.Insert(HL7DefSegCur);
			}
			else {
				HL7DefSegments.Update(HL7DefSegCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}