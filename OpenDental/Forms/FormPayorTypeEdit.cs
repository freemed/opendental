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
	public partial class FormPayorTypeEdit:Form {
		public bool IsNew;
		private PayorType PayorTypeCur;

		public FormPayorTypeEdit(PayorType payorType) {
			InitializeComponent();
			Lan.F(this);
			PayorTypeCur=payorType;
		}

		private void FormPayorTypeEdit_Load(object sender,EventArgs e) {
			for(int i=0;i<Sops.Listt.Count;i++) {
				comboSopCode.Items.Add(Sops.Listt[i].Description);
				if(PayorTypeCur.SopCode==Sops.Listt[i].SopCode) {
					comboSopCode.SelectedIndex=i;
				}
			}
			textDate.Text=PayorTypeCur.DateStart.ToShortDateString();
			textNote.Text=PayorTypeCur.Note;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,true,"Delete entry?")) {
				return;
			}
			PayorTypes.Delete(PayorTypeCur.PayorTypeNum);
			DialogResult=DialogResult.OK;//Causes grid to refresh in case this amendment is not new.
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDate.errorProvider1.GetError(textDate)!="") {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textDate.Text=="") {
				MsgBox.Show(this,"Please enter a date.");
				return;
			}
			if(comboSopCode.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select an Sop Code.");
				return;
			}
			PayorTypeCur.SopCode=Sops.Listt[comboSopCode.SelectedIndex].SopCode;
			PayorTypeCur.Note=textNote.Text;
			PayorTypeCur.DateStart=PIn.Date(textDate.Text);
			if(IsNew) {
				PayorTypes.Insert(PayorTypeCur);
			}
			else {
				PayorTypes.Update(PayorTypeCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}
