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
		public HL7DefSegment HL7DefSeg;
		///<summary></summary>
		public FormHL7DefSegmentEdit() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		private void FormHL7DefSegmentEdit_Load(object sender,EventArgs e) {
			FillGridMain();
			for(int i=0;i<Enum.GetNames(typeof(SegmentNameHL7)).Length;i++) {
				comboSegName.Items.Add(Lan.g("enumSegmentNameHL7",Enum.GetName(typeof(SegmentNameHL7),i).ToString()));
			}
			comboSegName.SelectedIndex=(int)HL7DefSeg.SegmentName;
			textItemOrder.Text=HL7DefSeg.ItemOrder.ToString();
			checkRepeat.Checked=HL7DefSeg.CanRepeat;
			checkOptional.Checked=HL7DefSeg.IsOptional;
			textNote.Text=HL7DefSeg.Note;
		}

		private void FillGridMain() {
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
			for(int i=0;i<HL7DefSeg.hl7DefFields.Count;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(Lan.g(this,HL7DefSeg.hl7DefFields[i].FieldName));
				row.Cells.Add(Lan.g("enumDataTypeHL7",HL7DefSeg.hl7DefFields[i].DataType.ToString()));
				row.Cells.Add(HL7DefSeg.hl7DefFields[i].OrdinalPos.ToString());
				row.Cells.Add(Lan.g(this,HL7DefSeg.hl7DefFields[i].TableId));
				row.Tag=HL7DefSeg.hl7DefFields[i];
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormHL7DefFieldEdit FormS=new FormHL7DefFieldEdit();
			FormS.HL7DefField=(HL7DefField)gridMain.Rows[e.Row].Tag;
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