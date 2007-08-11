using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.Forms {
	public partial class FormAutoNoteCreate:Form {
		public FormAutoNoteCreate() {
			InitializeComponent();
		}

		private void label1_Click(object sender,EventArgs e) {

		}



		private void butCreate_Click(object sender,EventArgs e) {
			/*databaseAutoNoteDataSet.TableAutoNoteMain.DefaultView.Sort = "MainName";
			int findResults = databaseAutoNoteDataSet.TableAutoNoteMain.DefaultView.Find(textBox1MainName.Text);
			if(findResults == -1) {
				if(textBox1MainName.Text != "") {
					DataRow newdatarow = databaseAutoNoteDataSet.TableAutoNoteMain.NewRow();
					newdatarow["MainName"] = textBox1MainName.Text;
					databaseAutoNoteDataSet.TableAutoNoteMain.Rows.Add(newdatarow);
					tableAutoNoteMainTableAdapter.Update(databaseAutoNoteDataSet.TableAutoNoteMain);

					Forms.FormAutoNoteAddVariables form = new FormAutoNoteAddVariables();
					form.Show();
					this.Close();
				}
				else {
					MessageBox.Show(Lan.g(this,"Please enter a name"));
				}
			}
			else {
				MessageBox.Show(Lan.g(this,"This Auto Note already exists please choose a different name"));
			}*/
		}

		private void buttonCancel_Click(object sender,EventArgs e) {
			this.Close();
		}

		private void tableAutoNoteMainBindingNavigatorSaveItem_Click(object sender,EventArgs e) {
			/*this.Validate();
			this.tableAutoNoteMainBindingSource.EndEdit();
			this.tableAutoNoteMainTableAdapter.Update(this.databaseAutoNoteDataSet.TableAutoNoteMain);
			*/
		}

		private void FormAutoNoteCreate_Load(object sender,EventArgs e) {
			/*
			//TODOautonote
			//This form is in desperate need of a redo unfortunately I'm unable to do this because of my lack of skill.
			//I had to add all of the editing controls onto one form because
			//I do not know how to pass on class values from one control to the other. Another bug that needs to be worked out is that there is no way of 
			//allowing the user to edit the Variable name and text for the sake of data integrity because I don't know how to change database values.
			// TODO: This line of code loads data into the 'databaseAutoNoteDataSet.TableAutoNoteVar' table. You can move, or remove it, as needed.
			this.tableAutoNoteVarTableAdapter.Fill(this.databaseAutoNoteDataSet.TableAutoNoteVar);
			// TODO: This line of code loads data into the 'databaseAutoNoteDataSet.TableAutoNoteMain' table. You can move, or remove it, as needed.
			this.tableAutoNoteMainTableAdapter.Fill(this.databaseAutoNoteDataSet.TableAutoNoteMain);
			// TODO: This line of code loads data into the 'databaseAutoNoteDataSet.TableAutoNoteMain' table. You can move, or remove it, as needed.
			this.tableAutoNoteMainTableAdapter.Fill(this.databaseAutoNoteDataSet.TableAutoNoteMain);
			Lan.F(this);
			for(int x = 0;x <= databaseAutoNoteDataSet.TableAutoNoteMain.Rows.Count - 1;x++) {
				listBox1AutoNotes.Items.Add(databaseAutoNoteDataSet.TableAutoNoteMain[x]["MainName"].ToString());
			}
			*/
		}

		private void tableAutoNoteMainBindingNavigatorSaveItem_Click_1(object sender,EventArgs e) {
			/*
			this.Validate();
			this.tableAutoNoteMainBindingSource.EndEdit();
			this.tableAutoNoteMainTableAdapter.Update(this.databaseAutoNoteDataSet.TableAutoNoteMain);
			*/
		}



		private void button1_Click(object sender,EventArgs e) {
			try {
				if(listBox1AutoNotes.SelectedIndex != -1) {
					button1Cancel.Visible = false;
					button1Create.Visible = false;
					button1EditAutoNote.Visible = false;
					label1.Visible = false;
					label2.Visible = false;
					label3.Visible = false;
					textBox1MainName.Visible = false;
					listBox1AutoNotes.Visible = false;

					button2Cancel.Visible = true;
					button2EditFinish.Visible = true;
					button2EditVar.Visible = true;
					label7.Visible = true;
					label8.Visible = true;
					listBox2AutoNoteVariables.Visible = true;
					listBox2Variables.Visible = true;
				}
				//for(int i = 0;i <= databaseAutoNoteDataSet.TableAutoNoteVar.Rows.Count - 1;i++) {
				//	listBox2Variables.Items.Add(databaseAutoNoteDataSet.TableAutoNoteVar[i]["Name"].ToString());
				//}
				System.IO.StreamReader sr = new System.IO.StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + @"\Main" + listBox1AutoNotes.SelectedItem.ToString() + @".txt");
				string srReader = sr.ReadLine();
				while(srReader != null) {
					listBox2AutoNoteVariables.Items.Add(srReader);
					srReader = sr.ReadLine();
				}

				sr.Close();
				sr.Dispose();
			}
			catch {
			}
		}

		private void buttonAddOption_Click(object sender,EventArgs e) {
			if(textBox3AddOption.Text != "") {
				listBox3Options.Items.Add(textBox3AddOption.Text);
				textBox3AddOption.Clear();
			}
		}

		private void buttonEditVar_Click(object sender,EventArgs e) {
			if(listBox2Variables.SelectedIndex != -1) {
				button2Cancel.Visible = false;
				button2EditFinish.Visible = false;
				button2EditVar.Visible = false;
				label4.Visible = false;
				label7.Visible = false;
				label8.Visible = false;
				listBox2AutoNoteVariables.Visible = false;
				listBox2Variables.Visible = false;
				listBox3Options.Visible = true;
				button3AddOption.Visible = true;
				button3EditVarDone.Visible = true;
				button3Cancel.Visible = true;
				textBox3Name.Visible = true;
				textBox3Text.Visible = true;
				textBox3AddOption.Visible = true;
				label4.Visible = true;
				label5.Visible = true;
				label6.Visible = true;
	/*			databaseAutoNoteDataSet.TableAutoNoteVar.DefaultView.Sort = "Name";
				int FindResult = databaseAutoNoteDataSet.TableAutoNoteVar.DefaultView.Find(listBox2Variables.SelectedItem.ToString());
				textBox3Name.Text = listBox2Variables.SelectedItem.ToString();
				textBox3Text.Text = databaseAutoNoteDataSet.TableAutoNoteVar[FindResult]["Text"].ToString();
				System.IO.StreamReader sr = new System.IO.StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + listBox2Variables.SelectedItem.ToString() + ".txt");
				string StreamReader = sr.ReadLine();
				while(StreamReader != null) {
					listBox3Options.Items.Add(StreamReader);
					StreamReader = sr.ReadLine();
				}
				sr.Close();
				sr.Dispose();
	 * */
			}
			else {
				MessageBox.Show(Lan.g(this,"Please choose a variable to edit"));
			}
		}

		private void buttonEditFinish_Click(object sender,EventArgs e) {

			System.IO.StreamWriter sw = new System.IO.StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"\Main" + listBox1AutoNotes.SelectedItem.ToString() + @".txt");
			for(int z = 0;z <= listBox2AutoNoteVariables.Items.Count - 1;z++) {
				sw.WriteLine(listBox2AutoNoteVariables.Items[z].ToString());
			}
			sw.Close();
			sw.Dispose();

			this.Close();
			FormAutoNoteCreate form = new FormAutoNoteCreate();
			form.Show();
		}

		private void button3_Click(object sender,EventArgs e) {
			FormAutoNoteCreate form = new FormAutoNoteCreate();
			form.Show();
			this.Close();
		}



		private void listBox2Variables_DoubleClick(object sender,EventArgs e) {
			if(listBox2Variables.SelectedIndex != -1) {
				listBox2AutoNoteVariables.Items.Add(listBox2Variables.SelectedItem);
			}
		}

		private void listBox2AutoNoteVariables_DoubleClick(object sender,EventArgs e) {
			if(listBox2AutoNoteVariables.SelectedIndex != -1) {
				listBox2AutoNoteVariables.Items.RemoveAt(listBox2AutoNoteVariables.SelectedIndex);
				;
			}
		}

		private void button2CreateVar_Click(object sender,EventArgs e) {
			/*FormAutoNoteCreateVariable form = new FormAutoNoteCreateVariable();
			form.Show();
			*/
		}


		private void button3EditVarDone_Click(object sender,EventArgs e) {

			System.IO.StreamWriter sr = new System.IO.StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + listBox2Variables.SelectedItem.ToString() + ".txt");

			for(int x = 0;x <= listBox3Options.Items.Count - 1;x++) {
				sr.WriteLine(listBox3Options.Items[x].ToString());
			}
			sr.Close();
			sr.Dispose();
			listBox3Options.Items.Clear();

			button2Cancel.Visible = true;
			button2EditFinish.Visible = true;
			button2EditVar.Visible = true;
			label7.Visible = true;
			listBox2AutoNoteVariables.Visible = true;
			listBox2Variables.Visible = true;

			listBox3Options.Visible = false;
			button3AddOption.Visible = false;
			button3EditVarDone.Visible = false;
			button3Cancel.Visible = false;
			textBox3Name.Visible = false;
			textBox3Text.Visible = false;
			textBox3AddOption.Visible = false;
			label4.Visible = false;
			label5.Visible = false;
			label6.Visible = false;
		}

		private void listBox3Options_MouseDoubleClick(object sender,MouseEventArgs e) {
			if(listBox3Options.SelectedIndex != -1) {
				listBox3Options.Items.RemoveAt(listBox3Options.SelectedIndex);
			}
		}

		private void button3Cancel_Click(object sender,EventArgs e) {
			button2Cancel.Visible = true;
			button2EditFinish.Visible = true;
			button2EditVar.Visible = true;
			label7.Visible = true;
			listBox2AutoNoteVariables.Visible = true;
			listBox2Variables.Visible = true;

			listBox3Options.Visible = false;
			button3AddOption.Visible = false;
			button3EditVarDone.Visible = false;
			button3Cancel.Visible = false;
			textBox3Name.Visible = false;
			textBox3Text.Visible = false;
			textBox3AddOption.Visible = false;
			label4.Visible = false;
			label5.Visible = false;
			label6.Visible = false;
		}

	}
}