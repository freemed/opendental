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
		//string controlToEdit;
		//private List<AutoNote> AutoNoteList;
		private List<AutoNoteControl> ControlList;
		public FormAutoNoteControlEdit() {
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

		private void FormAutoNoteControlEdit_Load(object sender, EventArgs e) {
			string controlOptions;
			//use ControlCur in the section below, not ControlList[0]
			/*
			comboBoxControlType.SelectedItem=ControlList[0].ControlType;
			textBoxControlDescript.Text=ControlList[0].Descript;
			textBoxControlLabel.Text=ControlList[0].ControlLabel;
			textBoxTextPreface.Text=ControlList[0].PrefaceText;
			textBoxMultiLineText.Text=ControlList[0].MultiLineText;
			controlOptions=ControlList[0].ControlOptions;
			string[] lines=controlOptions.Split(new char[] { '|' });
			for (int i=0; i<lines.Length; i++) {
				if (lines[i].ToString() !="") {
					listBoxControlOptions.Items.Add(lines[i].ToString());
				} 
			}					
			*/
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

		private void butOK_Click(object sender,EventArgs e) {//Saves the control info into a new row
			//If this form is creating a new control the code below is executed
			//Too much nesting.  Please see how OK_Click was rearranged in FormAutoNoteEdit.
			//There should not be two big sections like this based on IsNew.
			//Most of this will be identical, whether IsNew or not.  Only one line at the end will be different
			/*
			if(edit!=true) {
				bool IsUsed=AutoNoteControls.ControlNameUsed(textBoxControlDescript.Text.ToString(),null);
				if(IsUsed!=true) {
					if(textBoxControlDescript.Text.ToString()!="" && comboBoxControlType.SelectedIndex!=-1) {
						string type=comboBoxControlType.Text.ToString();
						string descript=textBoxControlDescript.Text.ToString();
						string label=textBoxControlLabel.Text.ToString();
						string preface=textBoxTextPreface.Text.ToString();
						string multiLine=textBoxMultiLineText.Text.ToString();
						string[] controlOptions;
						int arraySize = listBoxControlOptions.Items.Count;
						controlOptions = new string[listBoxControlOptions.Items.Count];
						for(int i = 0;i<listBoxControlOptions.Items.Count;i++) {
							controlOptions[i] = listBoxControlOptions.Items[i].ToString();
						}
						//This is VERY wrong.  The only parameter should be an AutoNoteControl object.
						AutoNoteControls.Insert(type,descript,label,preface,multiLine,controlOptions,arraySize);
						this.Close();
					}
					else {
						MessageBox.Show(Lan.g(this,"Please make sure that the Name and Type field are not blank"));
					}
				}
				else {
					MessageBox.Show(Lan.g(this,"This name is already used please choose a different name"));
				}
			}
			//If this form is editing a control the code below is used
			else {
				bool isused=AutoNoteControls.ControlNameUsed(textBoxControlDescript.Text.ToString(),textBoxControlDescript.Text.ToString());
				if(isused!=true) {
					if(textBoxControlDescript.Text.ToString()!="" && comboBoxControlType.SelectedIndex!=-1) {
						string type=comboBoxControlType.SelectedItem.ToString();
						string descript=textBoxControlDescript.Text.ToString();
						string label=textBoxControlLabel.Text.ToString();
						string preface=textBoxTextPreface.Text.ToString();
						string multiLine=textBoxMultiLineText.Text.ToString();
						string[] controlOptions;
						int arraySize = listBoxControlOptions.Items.Count;
						controlOptions = new string[listBoxControlOptions.Items.Count];
						for(int i = 0;i<listBoxControlOptions.Items.Count;i++) {
							controlOptions[i] = listBoxControlOptions.Items[i].ToString();
						}
						//This is VERY wrong.  The only parameter should be an AutoNoteControl object.
						AutoNoteControls.ControlUpdate(controlToEdit,type,descript,label,preface,multiLine,controlOptions,arraySize);
						this.Close();
					}
					else {
						MessageBox.Show(Lan.g(this,"Please make sure that the Name and Type field are not blank"));
					}
				}
				else {
					MessageBox.Show(Lan.g(this,"This name is already used please choose a different name"));
				}
			}*/
		}		
		
	}
}