using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormScreenSetup:Form {
		List<SheetDef> listSheets;

		public FormScreenSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormScreenSetup_Load(object sender,EventArgs e) {
			checkUsePat.Checked=PrefC.GetBool(PrefName.PublicHealthScreeningUsePat);
			listSheets=SheetDefs.GetCustomForType(SheetTypeEnum.ExamSheet);
			for(int i=0;i<listSheets.Count;i++) {
				comboExamSheets.Items.Add(listSheets[i].Description);
				if(PrefC.GetLong(PrefName.PublicHealthScreeningSheet)==listSheets[i].SheetDefNum) {
					comboExamSheets.SelectedIndex=i;
				}
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			Prefs.UpdateBool(PrefName.PublicHealthScreeningUsePat,checkUsePat.Checked);
			Prefs.UpdateLong(PrefName.PublicHealthScreeningSheet,listSheets[comboExamSheets.SelectedIndex].SheetDefNum);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}