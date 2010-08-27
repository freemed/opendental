using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormWebFormSetup:Form {
		public FormWebFormSetup() {
			InitializeComponent();
			Lan.F(this);

			butWebformBorderColor.BackColor=PrefC.GetColor(PrefName.WebFormsBorderColor);
			textBoxWebformsHeading1.Text=PrefC.GetStringSilent(PrefName.WebFormsHeading1);
			textBoxWebformsHeading2.Text=PrefC.GetStringSilent(PrefName.WebFormsHeading2);
			textboxWebHostAddress.Text=PrefC.GetString(PrefName.WebHostSynchServerURL);
		}

		private void butOK_Click(object sender,EventArgs e) {
			try {
				Prefs.UpdateLong(PrefName.WebFormsBorderColor,this.butWebformBorderColor.BackColor.ToArgb());
				Prefs.UpdateString(PrefName.WebFormsHeading1,textBoxWebformsHeading1.Text.Trim());
				Prefs.UpdateString(PrefName.WebFormsHeading2,textBoxWebformsHeading2.Text.Trim());
				Prefs.UpdateString(PrefName.WebHostSynchServerURL,textboxWebHostAddress.Text.Trim());

				// update preferences on server
				string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
				WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
				wh.Url =PrefC.GetString(PrefName.WebHostSynchServerURL);
				wh.SetPreferences(RegistrationKey,PrefC.GetColor(PrefName.WebFormsBorderColor).ToArgb(),PrefC.GetStringSilent(PrefName.WebFormsHeading1),PrefC.GetStringSilent(PrefName.WebFormsHeading2));

				DialogResult=DialogResult.OK;
			}catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butWebformBorderColor_Click(object sender,EventArgs e) {
			colorDialog1.Color=butWebformBorderColor.BackColor;
			if(colorDialog1.ShowDialog()!=DialogResult.OK) {
				return;
			}
			butWebformBorderColor.BackColor=colorDialog1.Color;
			/*
			Def DefCur=DefC.Short[(int)DefCat.MiscColors][4].Copy();
			DefCur.ItemColor=colorDialog1.Color;
			Defs.Update(DefCur);
			Cache.Refresh(InvalidType.Defs);
			localDefsChanged=true;
			gridP.SetColors();
			gridP.Invalidate();
			gridP.Focus();
			*/
		}
	}
}