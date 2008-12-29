using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormSheetPicker:Form {
		public SheetTypeEnum SheetType;
		private List<SheetDef> listSheets;

		public FormSheetPicker() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetPicker_Load(object sender,EventArgs e) {
			listSheets=SheetDefs.GetCustomForType(SheetType);
			labelSheetType.Text=Lan.g("enumSheetTypeEnum",SheetType.ToString());
			for(int i=0;i<listSheets.Count;i++){
				listMain.Items.Add(listSheets[i].Description);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}