using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormAutoNoteControlEdit : Form {
		///<summary>This needs to go.</summary>
		//bool edit;
		public bool IsNew;
		///<summary>The current AutoNoteControl that is being edited, whether new or not.</summary>
		public AutoNoteControl ControlCur;
		List<AutoNoteControl> ControlList;
		//string controlToEdit;
		//private List<AutoNote> AutoNoteList;
		public FormAutoNoteControlEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAutoNoteCreateControl_Load(object sender, EventArgs e) {

		}

		private void butAdd_Click(object sender,EventArgs e) {
			if (textBoxControlOptions.Text.ToString()!="") {
			int indexOf=textBoxControlOptions.Text.IndexOf(",");
				if(indexOf!=-1){
					MessageBox.Show(Lan.g(this, "The character ',' is not allowed. Please use ';' instead"));
					return;
				}				
				listBoxControlOptions.Items.Add(textBoxControlOptions.Text);
				textBoxControlOptions.Clear();
				textBoxControlOptions.Select();
			}
		}

		private void FormAutoNoteControlEdit_Load(object sender, EventArgs e) {
			string controlOptions;			
			if (!IsNew) {
				string ControlOptions;
				comboBoxControlType.SelectedItem=ControlCur.ControlType;
				textBoxControlDescript.Text=ControlCur.Descript;
				textBoxControlLabel.Text=ControlCur.ControlLabel;
				textBoxTextPreface.Text=ControlCur.PrefaceText;
				textBoxMultiLineText.Text=ControlCur.MultiLineText;
				ControlOptions=ControlCur.ControlOptions;
				string[] lines=ControlOptions.Split(new char[] { ',' });
				for (int i=0; i<lines.Length; i++) {
					if (lines[i].ToString()!="") {
						listBoxControlOptions.Items.Add(lines[i].ToString());
					}
				}
			}
		}

		private void butCancel_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void listBoxControlOptions_DoubleClick(object sender, EventArgs e) {
			if (listBoxControlOptions.SelectedIndex!=-1) {
				listBoxControlOptions.Items.RemoveAt(listBoxControlOptions.SelectedIndex);
			}
		}

		private void comboBoxControlType_SelectedIndexChanged(object sender, EventArgs e) {
			switch (comboBoxControlType.SelectedItem.ToString()) {
				case "ComboBox":
					ControlsToAvtivateComboxBox();
					break;
				case "TextBox":
					ControlsToAvtivateTextBox();
					break;
				case "MultiLineTextBox":
					ControlToAvtivateMultiLineTextBox();
					break;
			}
		}

		/// <summary>
		/// Set visible property of the controls used to edit ComboBoxes to true
		/// </summary>
		private void ControlsToAvtivateComboxBox() {
			textBoxMultiLineText.ReadOnly=true;
			textBoxControlOptions.ReadOnly=false;
			listBoxControlOptions.Enabled=true;
			butAdd.Visible=true;			
		}
		/// <summary>
		/// Set Readonly property of the controls used to edit TextBox to true
		/// </summary>
		private void ControlsToAvtivateTextBox() {
			textBoxMultiLineText.ReadOnly=true;
			textBoxControlOptions.ReadOnly=true;
			listBoxControlOptions.Enabled=false;
			butAdd.Visible=false;
		}
		/// <summary>
		/// Set visible property of the controls used to edit MultiLineTexBox to true
		/// </summary>
		private void ControlToAvtivateMultiLineTextBox() {
			textBoxMultiLineText.ReadOnly=false;
			textBoxControlOptions.ReadOnly=true;
			listBoxControlOptions.Enabled=false;
			butAdd.Visible=false;
		}

		private void labelControlType_Click(object sender, EventArgs e) {

		}

		private void butOK_Click(object sender,EventArgs e) {
			//bool IsUsed=AutoNoteControls.ControlNameUsed(textBoxControlDescript.Text.ToString(), textBoxDescriptControl.Text.ToString());
			//if (IsUsed==true) {
			//	MessageBox.Show(Lan.g(this, "This name is already used please choose a different name"));
			//	return;
			//}
			if (textBoxControlDescript.Text.ToString()=="" || comboBoxControlType.SelectedIndex==-1) {
				MessageBox.Show(Lan.g(this, "Please make sure that the Name and Type field are not blank"));
				return;
			}			
			//converts the items in ListBoxControlOptions into a string with a comma between each option
			string ControlOptions="";
			for (int i = 0; i<listBoxControlOptions.Items.Count; i++) {
				ControlOptions = ControlOptions+listBoxControlOptions.Items[i].ToString()+",";
			}
			ControlCur.ControlType=comboBoxControlType.SelectedItem.ToString();
			ControlCur.Descript=textBoxControlDescript.Text.ToString();
			ControlCur.ControlLabel=textBoxControlLabel.Text.ToString();
			ControlCur.PrefaceText=textBoxTextPreface.Text.ToString();
			ControlCur.MultiLineText=textBoxMultiLineText.Text.ToString();
			ControlCur.ControlOptions=ControlOptions;
			if (IsNew) {
				AutoNoteControls.Insert(ControlCur);
			}
			else {
				AutoNoteControls.ControlUpdate(ControlCur);
			}
			this.DialogResult=DialogResult.OK;
			this.Close();
		}
	}
}