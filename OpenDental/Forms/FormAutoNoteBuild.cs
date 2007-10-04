using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormAutoNoteBuild : Form {
		///<summary>The current AutoNoteControl that is being edited, whether new or not.</summary>
		public AutoNoteControl ControlCur;
		public AutoNote AutoNoteCur;

		public FormAutoNoteBuild() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAutoNoteBuild_Load(object sender, EventArgs e) {
			FillList();
		}

		private void FillList() {
			listBoxAutoNotes.Items.Clear();
			AutoNotes.Refresh();
			for (int i=0; i<AutoNotes.Listt.Count; i++) {
				listBoxAutoNotes.Items.Add(AutoNotes.Listt[i].AutoNoteName);
			}
		}

		private void DrawControls() {
			int TextBoxCount=0;
			int ComboBoxCount=0;
			for (int i=0; i<listBoxControls.Items.Count; i++) {
				AutoNoteControls.RefreshControl(listBoxControls.Items[i].ToString());
				ControlCur=AutoNoteControls.Listt[0];
				switch (ControlCur.ControlType) {
					case "TextBox":
						TextBoxCount++;
						break;
					case "MultiLineTextBox":
						TextBoxCount++;
						break;
					case "ComboBox":
						ComboBoxCount++;
						break;
				}
			}
			System.Windows.Forms.ComboBox[] comboBox=new ComboBox[ComboBoxCount];
			System.Windows.Forms.TextBox[] textBox=new TextBox[TextBoxCount];
			System.Windows.Forms.Label[] label=new Label[listBoxControls.Items.Count];
			for (int l=0; l<listBoxControls.Items.Count; l++) {
				AutoNoteControls.RefreshControl(listBoxControls.Items[l].ToString());
				ControlCur=AutoNoteControls.Listt[0];
				label[l]=new Label();
				label[l].AutoSize=true;
				label[l].Text=ControlCur.ControlLabel;
			}
			int CurText=0;
			int CurCombo=0;
			int spacing=140;
			for (int l=0; l<listBoxControls.Items.Count; l++) {
				AutoNoteControls.RefreshControl(listBoxControls.Items[l].ToString());
				ControlCur=AutoNoteControls.Listt[0];
				switch (ControlCur.ControlType) {
					case "TextBox":
						textBox[CurText]=new TextBox();
						textBox[CurText].TabIndex=this.Controls.Count;
						textBox[CurText].Location=new Point(label[l].Text.Length * 6 + 15, spacing);
						this.Controls.Add(textBox[CurText]);
						label[l].Location=new Point(10, spacing);
						spacing=spacing+30;
						CurText++;
						break;
					case "MultiLineTextBox":
						textBox[CurText]=new TextBox();
						textBox[CurText].Multiline=true;
						textBox[CurText].Size=new Size(177, 67);
						textBox[CurText].Location=new Point(label[l].Text.Length * 6 + 15, spacing);
						textBox[CurText].Text=ControlCur.MultiLineText;
						this.Controls.Add(textBox[CurText]);
						label[l].Location=new Point(10, spacing);
						spacing=spacing+77;
						CurText++;
						break;
					case "ComboBox":
						comboBox[CurCombo]=new ComboBox();
						string[] lines=ControlCur.ControlOptions.Split(new char[] { ',' });
						for (int i=0; i<lines.Length; i++) {
							comboBox[CurCombo].Items.Add(lines[i]);
						}
						comboBox[CurCombo].AutoCompleteMode=AutoCompleteMode.SuggestAppend;
						comboBox[CurCombo].AutoCompleteSource=AutoCompleteSource.ListItems;
						this.Controls.Add(comboBox[CurCombo]);
						label[l].Location=new Point(10, spacing);
						comboBox[CurCombo].Location=new Point(label[l].Text.Length * 6 + 15, spacing);
						spacing=spacing+30;
						CurCombo++;
						break;
				}
				for (int xl = 0; xl <listBoxControls.Items.Count; xl++) {
					this.Controls.Add(label[xl]);
				}
			}
			butOK.Visible=true;
			butOK.Location=new Point(330, spacing);
		}

		private void listBoxAutoNotes_SelectedIndexChanged(object sender, EventArgs e) {
			for (int x = 0; x <= this.Controls.Count - 1; x++) {

				if (this.Controls[x].Name.ToString() != "listBoxAutoNotes" &&
						this.Controls[x].Name.ToString() != "butOK" &&
						this.Controls[x].Name.ToString() != "listBoxControls") {
					this.Controls.RemoveAt(x);
					x--;
				}
			}

			if (listBoxAutoNotes.SelectedIndex==-1) {
				return;
			}
			listBoxControls.Items.Clear();
			AutoNoteCur=AutoNotes.Listt[listBoxAutoNotes.SelectedIndex];
			string[] lines=AutoNoteCur.ControlsToInc.Split(new char[] { ',' });
			for (int i=0; i<lines.Length; i++) {
				if (lines[i].ToString()!="") {
					listBoxControls.Items.Add(lines[i].ToString());
				}
			}
			DrawControls();
		}

		private void butOK_Click(object sender, EventArgs e) {
			string AutoNoteOutput="";
			int currentControl=0;
			for (int x=0; x<this.Controls.Count; x++) {
				if (this.Controls[x].Name.ToString() != "listBoxAutoNotes" &&
						this.Controls[x].Name.ToString() != "butOK" &&
						this.Controls[x].Name.ToString() != "listBoxControls") {					
						string Text="";
						string prefaceText="";
						if (currentControl<listBoxControls.Items.Count) {
							if (this.Controls[x].Text!="") {
								AutoNoteControls.RefreshControl(listBoxControls.Items[currentControl].ToString());
								ControlCur=AutoNoteControls.Listt[0];
								prefaceText=ControlCur.PrefaceText;
								Text=this.Controls[x].Text;
								AutoNoteOutput=AutoNoteOutput+" "+prefaceText+" "+Text;
							}
							currentControl++;
						}
				}
			}
			AutoNoteCur.AutoNoteOutput=AutoNoteOutput;
			this.DialogResult=DialogResult.OK;
		}
	}
}