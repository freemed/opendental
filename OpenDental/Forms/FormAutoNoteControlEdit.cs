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
		//will be needed later
		bool edit;
		string autoNoteControlToEdit;
		private List<AutoNote> AutoNoteList;
		/// <summary>		
		/// </summary>
		/// <param name="Edit"></param>
		/// Set this true if there is a control to edit.
		/// <param name="AutoNoteControlToEdit"></param>
		/// This is the name of the Control to edit.
		/// set to null if there is no control to edit.
		public FormAutoNoteControlEdit(bool Edit, string AutoNoteControlToEdit) {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAutoNoteCreateControl_Load(object sender, EventArgs e) {

		}

		private void butAdd_Click(object sender,EventArgs e) {
			if (textBoxControlOptions.Text.ToString()!="") {
				listBoxControlOptions.Items.Add(textBoxControlOptions.Text);
				textBoxControlOptions.Clear();
				textBoxControlOptions.Select();
			}
		}

		private void butOK_Click(object sender,EventArgs e) {//Saves the control info into a new row
			bool IsUsed=AutoNoteControls.ControlNameUsed(textBoxControlDescript.Text.ToString());
			if (IsUsed!=true) {
				if (textBoxControlDescript.Text.ToString()!="" && comboBoxControlType.SelectedIndex!=-1) {
					string type=comboBoxControlType.Text.ToString();
					string descript=textBoxControlDescript.Text.ToString();
					string label=textBoxControlLabel.Text.ToString();
					string preface=textBoxTextPreface.Text.ToString();
					string multiLine=textBoxMultiLineText.Text.ToString();
					string[] controlOptions;
					int arraySize = listBoxControlOptions.Items.Count;
					controlOptions = new string[listBoxControlOptions.Items.Count];
					for (int i = 0; i<listBoxControlOptions.Items.Count; i++) {
						controlOptions[i] = listBoxControlOptions.Items[i].ToString();
					}
					AutoNoteControls.Insert(type, descript, label, preface, multiLine, controlOptions, arraySize);
					this.Close();
				}
				else {
					MessageBox.Show(Lan.g(this, "Please make sure that the Name and Type field are not blank"));
				}
			}
			else {
				MessageBox.Show(Lan.g(this,"This name already used please choose a different name"));
			}
		}		
		

		private void FormAutoNoteControlEdit_Load(object sender, EventArgs e) {
			if (edit==true) { 
		   
			}
		}

		private void butCancel_Click(object sender, EventArgs e) {
			this.Close();
		}
		
	}
}