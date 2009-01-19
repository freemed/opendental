using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormAutoNoteControlEdit:Form {
		public bool IsNew;
		///<summary>The current AutoNoteControl that is being edited, whether new or not.</summary>
		public AutoNoteControl ControlCur;

		public FormAutoNoteControlEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAutoNoteControlEdit_Load(object sender,EventArgs e) {
			textBoxControlDescript.Text=ControlCur.Descript;
			textBoxControlLabel.Text=ControlCur.ControlLabel;
			comboType.Items.Clear();
			comboType.Items.Add("Text");
			comboType.Items.Add("OneResponse");
			comboType.SelectedItem=ControlCur.ControlType;
			textOptions.Text=ControlCur.ControlOptions;
		}

		private void comboBoxControlType_SelectedIndexChanged(object sender,EventArgs e) {
			switch(comboType.SelectedItem.ToString()) {
				case "Text":
					labelResponses.Text=Lan.g(this,"Default text");
					break;
				case "OneResponse":
					labelResponses.Text=Lan.g(this,"Possible responses (one line per item)");
					break;
				
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Completely delete this prompt?  It will not be available from any AutoNote.")) {
				return;
			}
			AutoNoteControls.Delete(ControlCur.AutoNoteControlNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textBoxControlDescript.Text.ToString()=="" 
				|| comboType.SelectedIndex==-1) 
			{
				MessageBox.Show(Lan.g(this,"Please make sure that the Description and Type are not blank"));
				return;
			}
			ControlCur.Descript=textBoxControlDescript.Text.ToString();
			ControlCur.ControlLabel=textBoxControlLabel.Text.ToString();
			ControlCur.ControlType=comboType.SelectedItem.ToString();
			ControlCur.ControlOptions=textOptions.Text;
			if(IsNew) {
				AutoNoteControls.Insert(ControlCur);
			}
			else {
				AutoNoteControls.Update(ControlCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	}
}