using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormAnesthMedSuppliersEdit : Form
	{
		public FormAnesthMedSuppliersEdit()
		{
			InitializeComponent();
			Lan.F(this);
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void textPhone_TextChanged(object sender, EventArgs e)
		{
			int cursor = textPhone.SelectionStart;
			int length = textPhone.Text.Length;
			textPhone.Text = TelephoneNumbers.AutoFormat(textPhone.Text);
			if (textPhone.Text.Length > length)
				cursor++;
			textPhone.SelectionStart = cursor;
		}

		private void textSupplierName_TextChanged(object sender, EventArgs e)
		{
			int cursor = textSupplierName.SelectionStart;
			int length = textSupplierName.Text.Length;
			textSupplierName.Text = textSupplierName.Text;
			textSupplierName.SelectionStart = cursor;
		}

		private void textCity_TextChanged(object sender, EventArgs e)
		{

		}
	}
}