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
	
	public partial class FormAutoNoteEdit:Form {
		public bool IsNew;
		public string autoNoteToEdit;
		List<AutoNoteControl> ControlsList;
		List<AutoNote> AutoNoteList;

		public FormAutoNoteEdit() {
			InitializeComponent();
			Lan.F(this);
		}		

		private void FormAutoNoteEdit_Load(object sender,EventArgs e) {
			//todo: fill controls on form
			if (IsNew!=true) {
				string controlOptions;
				AutoNoteList=AutoNotes.AutoNoteEdit(autoNoteToEdit);
				textBoxAutoNoteName.Text=AutoNoteList[0].AutoNoteName;
				controlOptions=AutoNoteList[0].ControlsToInc;
				string[] lines=controlOptions.Split(new char[] { '|' });
				for (int i=0; i<lines.Length; i++) {
					if (lines[i].ToString()!="") {
						ControlsList=AutoNoteControls.ControlNumToName(lines[i].ToString());
						listBoxControlsToIncl.Items.Add(ControlsList[0].Descript);
					}
				}
				//todo fill the listBoxControlToIncl
			}
				fillListBoxControls();
			
		}

		private void listBoxControlsToIncl_SelectedIndexChanged(object sender, EventArgs e) {
			
		}

		private void listBoxControls_SelectedIndexChanged(object sender, EventArgs e) {
			//Loads all the control info into the control viewer
			string controlOptions;
			ControlsList = AutoNoteControls.RefreshControlEdit(listBoxControls.SelectedItem.ToString());
			textBoxDescriptControl.Text=ControlsList[0].Descript;
			textBoxLabelControl.Text=ControlsList[0].ControlLabel;
			textBoxTextControl.Text=ControlsList[0].MultiLineText;
			textBoxTextPrefaceControl.Text=ControlsList[0].PrefaceText;
			
			listBoxOptionsControl.Items.Clear();
			controlOptions=ControlsList[0].ControlOptions;
			string[] lines=controlOptions.Split(new char[] {'|'});
			for (int i=0; i<lines.Length; i++) {
				listBoxOptionsControl.Items.Add(lines[i].ToString());
			}
			
		}

		private void butCreateControl_Click(object sender,EventArgs e) {
			//should launch FormAutoNoteControlEdit
			FormAutoNoteControlEdit form=new FormAutoNoteControlEdit(false, null);
			form.ShowDialog();
		}

		private void butEditControl_Click(object sender,EventArgs e) {
			//should launch FormAutoNoteControlEdit
			//todo
		}

		private void butOK_Click(object sender,EventArgs e) {
			//Save changes to database here
			//Saves the items in the listboxControlsToIncl in a array that will be passed on 
			if (textBoxAutoNoteName.Text!="") {
				if (listBoxControlsToIncl.Items.Count!=0) {
					if (IsNew==true) {
						string[] controlsToInc;
						int ArraySize;
						controlsToInc = new string[listBoxControlsToIncl.Items.Count];
						ArraySize = listBoxControlsToIncl.Items.Count;
						for (int i = 0; i<listBoxControlsToIncl.Items.Count; i++) {
							if (listBoxControlsToIncl.Items[i].ToString()!="") {
								ControlsList=AutoNoteControls.ControlNameToNum(listBoxControlsToIncl.Items[i].ToString());
								controlsToInc[i]=ControlsList[0].AutoNoteControlNum.ToString();
							}
						}
						AutoNotes.Insert(textBoxAutoNoteName.Text.ToString(), controlsToInc, ArraySize);
						this.Close();
					}
					else {
						string[] controlsToInc;
						int ArraySize;
						controlsToInc = new string[listBoxControlsToIncl.Items.Count];
						ArraySize = listBoxControlsToIncl.Items.Count;						
						for (int i = 0; i<listBoxControlsToIncl.Items.Count; i++) {
							if (listBoxControlsToIncl.Items[i].ToString()!="") {
								ControlsList=AutoNoteControls.ControlNameToNum(listBoxControlsToIncl.Items[i].ToString());
								controlsToInc[i]=ControlsList[0].AutoNoteControlNum.ToString();
							}
						}
						AutoNotes.AutoNoteUpdate(autoNoteToEdit, textBoxAutoNoteName.Text.ToString(), controlsToInc, ArraySize);
						this.Close();
					}
				}
				else {
					MessageBox.Show(Lan.g(this, "Please add some Controls to the Auto Note"));
				}
			}
			else {
				MessageBox.Show(Lan.g(this, "Please enter a Name"));
			}
		}

		private void butCancel_Click(object sender,EventArgs e) {
			this.Close();
		}

		/// <summary>
		/// Refreshes the listBoxControls
		/// </summary>
		private void fillListBoxControls() {
			listBoxControls.Items.Clear();
			ControlsList=AutoNoteControls.Refresh();
			for (int i=0; i<ControlsList.Count; i++) {
			listBoxControls.Items.Add(ControlsList[i].Descript);
			}
		}


		private void FormAutoNoteEdit_Activated(object sender, EventArgs e) {
			fillListBoxControls();
		}

		private void listBoxControls_MouseDoubleClick(object sender, MouseEventArgs e) {//Adds the selected item to the ListboxControlToInc
			if (listBoxControls.SelectedIndex!=-1) {
				listBoxControlsToIncl.Items.Add(listBoxControls.SelectedItem);
			}
		}

		private void listBoxControlsToIncl_MouseDoubleClick(object sender, MouseEventArgs e) {//Removes the selected item from the list
			if (listBoxControlsToIncl.SelectedIndex!=-1) {
				listBoxControlsToIncl.Items.Remove(listBoxControls.SelectedIndex);
			}
		}

		private void listBoxOptionsControl_SelectedIndexChanged(object sender, EventArgs e) {

		}	


	}
}