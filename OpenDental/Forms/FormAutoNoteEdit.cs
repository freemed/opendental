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

	public partial class FormAutoNoteEdit : Form {
		public bool IsNew;
		public AutoNote AutoNoteCur;
		public AutoNoteControl ControlCur;
		List<AutoNoteControl> ControlsList;
		//List<AutoNote> AutoNoteList;
		/// <summary>This is set to true if the user clicks on the edit control button</summary>
		bool RefreshControlsToIncEdit=false;
		string[] ControlsToInc;

		public FormAutoNoteEdit() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAutoNoteEdit_Load(object sender, EventArgs e) {
			if (!IsNew) {
				string controls;
				textBoxAutoNoteName.Text=AutoNoteCur.AutoNoteName;
				controls=AutoNoteCur.ControlsToInc;
				string[] lines=controls.Split(new char[] { ',' });
				for (int i=0; i<lines.Length; i++) {
					if (lines[i].ToString()!="") {
						listBoxControlToIncNum.Items.Add(lines[i].ToString());
						ControlsList=AutoNoteControls.ControlNumToName(lines[i].ToString());
						listBoxControlsToIncl.Items.Add(AutoNoteControls.Listt[0].Descript);
					}
				}
			}
			fillListBoxControls();
		}

		private void listBoxControlsToIncl_SelectedIndexChanged(object sender, EventArgs e) {

		}

		private void listBoxControls_SelectedIndexChanged(object sender, EventArgs e) {
			//Loads all the control info into the control viewer
			if (listBoxControls.SelectedItem.ToString()=="") {
				return;
			}
			string controlOptions;
			//AutoNoteControls.RefreshControlEdit(listBoxControls.SelectedIndex.ToString());
			AutoNoteControls.Refresh();
			ControlCur=AutoNoteControls.Listt[listBoxControls.SelectedIndex];
			textBoxTypeControl.Text=ControlCur.ControlType;
			textBoxDescriptControl.Text=ControlCur.Descript;
			textBoxLabelControl.Text=ControlCur.ControlLabel;
			textBoxTextControl.Text=ControlCur.MultiLineText;
			textBoxTextPrefaceControl.Text=ControlCur.PrefaceText;
			listBoxOptionsControl.Items.Clear();
			controlOptions=ControlCur.ControlOptions;
			string[] lines=controlOptions.Split(new char[] { ',' });
			for (int i=0; i<lines.Length; i++) {
				listBoxOptionsControl.Items.Add(lines[i].ToString());
			}
			ControlContentViewerVisible(true);
		}

		private void ControlContentViewerVisible(bool visible) {			
				labelControl.Visible=visible;
				labelTypeControl.Visible=visible;
				labelNameControl.Visible=visible;
				labelLabelControl.Visible=visible;
				labelPrefaceText.Visible=visible;
				labelText.Visible=visible;
				textBoxTypeControl.Visible=visible;
				textBoxDescriptControl.Visible=visible;
				textBoxLabelControl.Visible=visible;
				textBoxTextPrefaceControl.Visible=visible;
				textBoxTextControl.Visible=visible;
				listBoxOptionsControl.Visible=visible;
				butEditControl.Visible=visible;
		}

		private void butCreateControl_Click(object sender, EventArgs e) {
			FormAutoNoteControlEdit form=new FormAutoNoteControlEdit();
			form.IsNew=true;
			form.ControlCur=new AutoNoteControl();
			form.ShowDialog();
			if (form.DialogResult==DialogResult.OK) {
				ControlContentViewerVisible(false);
				AutoNoteControls.Refresh();
				listBoxControls.Items.Clear();
				for (int i=0; i<AutoNoteControls.Listt.Count; i++) {
					listBoxControls.Items.Add(AutoNoteControls.Listt[i].Descript);
				}
			}
		}

		/// <summary>
		/// Refreshes the listBoxControls
		/// </summary>
		private void fillListBoxControls() {
			listBoxControls.Items.Clear();
			AutoNoteControls.Refresh();
			for (int i=0; i<AutoNoteControls.Listt.Count; i++) {
				listBoxControls.Items.Add(AutoNoteControls.Listt[i].Descript);
			}
			if (!IsNew) {
				listBoxControlsToIncl.Items.Clear();
				listBoxControlToIncNum.Items.Clear();
				string controls=AutoNoteCur.ControlsToInc;
				string[] lines=controls.Split(new char[] { ',' });
				for (int i=0; i<lines.Length; i++) {
					if (lines[i].ToString()!="") {
						listBoxControlToIncNum.Items.Add(lines[i].ToString());
						ControlsList=AutoNoteControls.ControlNumToName(lines[i].ToString());
						listBoxControlsToIncl.Items.Add(AutoNoteControls.Listt[0].Descript);
					}
				}
			}
		}

		private void butEditControl_Click(object sender, EventArgs e) {
			//should launch FormAutoNoteControlEdit
			//I did not have time to look closely at this:
			/*
			ControlsToInc=new string[listBoxControlsToIncl.Items.Count];
			for (int i=0; i<listBoxControlsToIncl.Items.Count; i++) {
				if (listBoxControlsToIncl.Items[i].ToString()!="") {
					ControlsList=AutoNoteControls.ControlNameToNum(listBoxControlsToIncl.Items[i].ToString());
					ControlsToInc[i]=ControlsList[0].AutoNoteControlNum.ToString();
				}
			}
			RefreshControlsToIncEdit=true;*/
			if (listBoxControls.SelectedIndex==-1) {
				return;
			}
			FormAutoNoteControlEdit form = new FormAutoNoteControlEdit();
			form.IsNew=false;
			form.ControlCur=AutoNoteControls.Listt[listBoxControls.SelectedIndex];
			form.ShowDialog();	
			if (form.DialogResult==DialogResult.OK) {
				ControlContentViewerVisible(false);
				AutoNoteControls.Refresh();
				listBoxControls.Items.Clear();
				for (int i=0; i<AutoNoteControls.Listt.Count; i++) {
					listBoxControls.Items.Add(AutoNoteControls.Listt[i].Descript);
				}
					listBoxControlsToIncl.Items.Clear();
					for (int i=0; i<listBoxControlToIncNum.Items.Count; i++) {
						ControlsList=AutoNoteControls.ControlNumToName(listBoxControlToIncNum.Items[i].ToString());
							listBoxControlsToIncl.Items.Add(ControlsList[0].Descript);
					}				
			}			
		}

		


		private void FormAutoNoteEdit_Activated(object sender, EventArgs e) {			
	
		}

		private void listBoxControls_MouseDoubleClick(object sender, MouseEventArgs e) {//Adds the selected item to the ListboxControlToInc
			if (listBoxControls.SelectedIndex!=-1) {
				listBoxControlsToIncl.Items.Add(listBoxControls.SelectedItem);
				listBoxControlToIncNum.Items.Add(listBoxControls.SelectedIndex+1);
			}
		}

		private void listBoxOptionsControl_SelectedIndexChanged(object sender, EventArgs e) {

		}

		private void listBoxControlsToIncl_DoubleClick(object sender, EventArgs e) {
			if (listBoxControlsToIncl.SelectedIndex!=-1) {
				listBoxControlToIncNum.Items.RemoveAt(listBoxControlsToIncl.SelectedIndex);
				listBoxControlsToIncl.Items.RemoveAt(listBoxControlsToIncl.SelectedIndex);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textBoxAutoNoteName.Text=="") {
				MsgBox.Show(this,"Please enter a name for the Auto Note into the text box");
				return;
			}
			//bool IsUsed=AutoNotes.AutoNoteNameUsed(textBoxAutoNoteName.Text.ToString(),AutoNoteCur.AutoNoteName);
			//if(IsUsed) {
			//	MsgBox.Show(this,"This name is already used please choose a different name");
			//	return;
			//}
			/*if (listBoxControlsToIncl.Items.Count==0) {
				MsgBox.Show(this, "Please add some controls to the Auto Note");
				return;
			}*/
			//Save changes to database here
			//Saves the items in the listboxControlsToIncl in a array that will be passed on 
			string controlsToIncText="";
			for (int i=0; i<listBoxControlToIncNum.Items.Count; i++) {
				if (listBoxControlsToIncl.Items[i].ToString()!="") {
					controlsToIncText = controlsToIncText + listBoxControlToIncNum.Items[i].ToString()+",";
				}
				}
			AutoNoteCur.ControlsToInc=controlsToIncText;
			AutoNoteCur.AutoNoteName=textBoxAutoNoteName.Text.ToString();
			if(IsNew) {
				AutoNotes.Insert(AutoNoteCur);
			}
			else {
				AutoNotes.Update(AutoNoteCur);
			}
			DialogResult=DialogResult.OK;
		}


		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}