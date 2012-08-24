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
		private List<FieldNameAndType> FieldNameList;
		public bool IsHL7DefInternal;

		///<summary></summary>
		public FormHL7DefFieldEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormHL7DefFieldEdit_Load(object sender,EventArgs e) {
			FieldNameList=new List<FieldNameAndType>();
			FillFieldNameList();
			for(int i=0;i<FieldNameList.Count;i++) {
				listFieldNames.Items.Add(FieldNameList[i].Name);
				if(HL7DefFieldCur!=null && HL7DefFieldCur.FieldName==FieldNameList[i].Name) {
					listFieldNames.SelectedIndex=i;
					comboDataType.Enabled=false;
				}
			}
			if(HL7DefFieldCur!=null && HL7DefFieldCur.FixedText.Length>0) {
				textFixedText.Text=HL7DefFieldCur.FixedText;
				comboDataType.Enabled=true;
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

		private class FieldNameAndType {
			public string Name;
			public DataTypeHL7 DataType;

			public FieldNameAndType(string name,DataTypeHL7 datatype) {
				Name=name;
				DataType=datatype;
			}
		}

		private void FillFieldNameList() {
			FieldNameList.Add(new FieldNameAndType("apt.AptNum",DataTypeHL7.CX));
			FieldNameList.Add(new FieldNameAndType("apt.lengthStartEnd",DataTypeHL7.TQ));
			FieldNameList.Add(new FieldNameAndType("apt.Note",DataTypeHL7.CWE));
			FieldNameList.Add(new FieldNameAndType("dateTime.Now",DataTypeHL7.DTM));
			FieldNameList.Add(new FieldNameAndType("eventType",DataTypeHL7.ID));
			FieldNameList.Add(new FieldNameAndType("guar.addressCityStateZip",DataTypeHL7.XAD));
			FieldNameList.Add(new FieldNameAndType("guar.birthdateTime",DataTypeHL7.DTM));
			FieldNameList.Add(new FieldNameAndType("guar.ChartNumber",DataTypeHL7.CX));
			FieldNameList.Add(new FieldNameAndType("guar.Gender",DataTypeHL7.IS));
			FieldNameList.Add(new FieldNameAndType("guar.HmPhone",DataTypeHL7.XTN));
			FieldNameList.Add(new FieldNameAndType("guar.nameLFM",DataTypeHL7.XPN));
			FieldNameList.Add(new FieldNameAndType("guar.PatNum",DataTypeHL7.CX));
			FieldNameList.Add(new FieldNameAndType("guar.SSN",DataTypeHL7.ST));
			FieldNameList.Add(new FieldNameAndType("guar.WkPhone",DataTypeHL7.XTN));
			FieldNameList.Add(new FieldNameAndType("messageType",DataTypeHL7.MSG));
			FieldNameList.Add(new FieldNameAndType("pat.addressCityStateZip",DataTypeHL7.XAD));
			FieldNameList.Add(new FieldNameAndType("pat.birthdateTime",DataTypeHL7.DTM));
			FieldNameList.Add(new FieldNameAndType("pat.ChartNumber",DataTypeHL7.CX));
			FieldNameList.Add(new FieldNameAndType("pat.FeeSched",DataTypeHL7.ST));
			FieldNameList.Add(new FieldNameAndType("pat.Gender",DataTypeHL7.IS));
			FieldNameList.Add(new FieldNameAndType("pat.HmPhone",DataTypeHL7.XTN));
			FieldNameList.Add(new FieldNameAndType("pat.nameLFM",DataTypeHL7.XPN));
			FieldNameList.Add(new FieldNameAndType("pat.PatNum",DataTypeHL7.CX));
			FieldNameList.Add(new FieldNameAndType("pat.Position",DataTypeHL7.CWE));
			FieldNameList.Add(new FieldNameAndType("pat.Race",DataTypeHL7.CWE));
			FieldNameList.Add(new FieldNameAndType("pat.SSN",DataTypeHL7.ST));
			FieldNameList.Add(new FieldNameAndType("pat.WkPhone",DataTypeHL7.XTN));
			FieldNameList.Add(new FieldNameAndType("pdfDescription",DataTypeHL7.ST));
			FieldNameList.Add(new FieldNameAndType("pdfDataAsBase64",DataTypeHL7.ST));
			FieldNameList.Add(new FieldNameAndType("proc.DiagnosticCode",DataTypeHL7.CWE));
			FieldNameList.Add(new FieldNameAndType("proc.procDateTime",DataTypeHL7.DTM));
			FieldNameList.Add(new FieldNameAndType("proc.ProcFee",DataTypeHL7.CP));
			FieldNameList.Add(new FieldNameAndType("proc.toothSurfRange",DataTypeHL7.CNE));
			FieldNameList.Add(new FieldNameAndType("proccode.ProcCode",DataTypeHL7.CNE));
			FieldNameList.Add(new FieldNameAndType("prov.provIdNameLFM",DataTypeHL7.XCN));
			FieldNameList.Add(new FieldNameAndType("prov.provIdName",DataTypeHL7.XCN));
			FieldNameList.Add(new FieldNameAndType("separators^~\\&",DataTypeHL7.ST));
			FieldNameList.Add(new FieldNameAndType("sequenceNum",DataTypeHL7.SI));
		}

		private void textFixedText_KeyUp(object sender,EventArgs e) {
			listFieldNames.ClearSelected();
			comboDataType.Enabled=true;
		}

		private void listFieldNames_Click(object sender,EventArgs e) {
			textFixedText.Clear();
			comboDataType.SelectedIndex=(int)FieldNameList[listFieldNames.SelectedIndex].DataType;
			comboDataType.Enabled=false;
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
			if(listFieldNames.SelectedIndex>-1) {
				HL7DefFieldCur.FieldName=listFieldNames.SelectedItem.ToString();
				HL7DefFieldCur.FixedText="";
			}
			else {
				HL7DefFieldCur.FieldName="";
				HL7DefFieldCur.FixedText=textFixedText.Text;
			}
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
