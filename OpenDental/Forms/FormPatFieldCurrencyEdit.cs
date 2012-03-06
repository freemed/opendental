using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormPatFieldCurrencyEdit:Form {
		public bool IsNew;
		private PatField Field;

		public FormPatFieldCurrencyEdit(PatField field) {
			InitializeComponent();
			Lan.F(this);
			Field=field;
		}

		private void FormPatFieldCurrencyEdit_Load(object sender,EventArgs e) {
			labelName.Text=Field.FieldName;
			textFieldCurrency.Text=Field.FieldValue;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textFieldCurrency.errorProvider1.GetError(textFieldCurrency)!="") {
				MsgBox.Show(this,"Invalid currency");
				return;
			}
			if(Field.FieldValue==""){//if blank, then delete
				if(IsNew) {
					DialogResult=DialogResult.Cancel;
					return;
				}
				PatFields.Delete(Field);
				DialogResult=DialogResult.OK;
				return;
			}
			Field.FieldValue=textFieldCurrency.Text;
			if(IsNew){
				PatFields.Insert(Field);
			}
			else{
				PatFields.Update(Field);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}