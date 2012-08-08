using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	/// <summary></summary>
	public partial class FormHL7DefFieldEdit:Form {
		public HL7DefField HL7DefFieldCur;
		public List<string> FieldNameList;
		public bool IsHL7DefInternal;

		///<summary></summary>
		public FormHL7DefFieldEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormHL7DefFieldEdit_Load(object sender,EventArgs e) {
			FieldNameList=new List<string>();
			FillFieldNameList();
			for(int i=0;i<FieldNameList.Count;i++) {
				listFieldNames.Items.Add(FieldNameList[i]);
				if(HL7DefFieldCur!=null && HL7DefFieldCur.FieldName==FieldNameList[i]) {
					listFieldNames.SelectedIndex=i;
				}
			}
			for(int i=0;i<Enum.GetNames(typeof(DataTypeHL7)).Length;i++) {
				comboDataType.Items.Add(Lan.g("enumDataTypeHL7",Enum.GetName(typeof(DataTypeHL7),i).ToString()));
			}
			if(HL7DefFieldCur!=null) {
				comboDataType.SelectedIndex=(int)HL7DefFieldCur.DataType;
				textItemOrder.Text=HL7DefFieldCur.OrdinalPos.ToString();
				textTableId.Text=HL7DefFieldCur.TableId;
			}
			if(IsHL7DefInternal) {
				butOK.Enabled=false;
				butDelete.Enabled=false;
				labelDelete.Visible=true;
			}
		}

		private void FillFieldNameList() {
			FieldNameList.Add("patient.PatNum");
			FieldNameList.Add("patient.ChartNum");
			FieldNameList.Add("appointment.AptNum");
			FieldNameList.Add("appointment.AptDateTime");
			FieldNameList.Add("appointment.Note");
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete Field?")) {
				return;
			}
			HL7DefFields.Delete(HL7DefFieldCur.HL7DefFieldNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//This button is disabled if IsHL7DefInternal
			if(textItemOrder.errorProvider1.GetError(textItemOrder)!="") {
				MsgBox.Show(this,"Please fix data entry error first.");
				return;
			}
			HL7DefFieldCur.DataType=(DataTypeHL7)comboDataType.SelectedIndex;
			HL7DefFieldCur.TableId=textTableId.Text;
			HL7DefFieldCur.OrdinalPos=PIn.Int(textItemOrder.Text);
			HL7DefFieldCur.FieldName=listFieldNames.SelectedItem.ToString();
			if(HL7DefFieldCur.IsNew) {
				HL7DefFields.Insert(HL7DefFieldCur);
			}
			else {
				HL7DefFields.Update(HL7DefFieldCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}
