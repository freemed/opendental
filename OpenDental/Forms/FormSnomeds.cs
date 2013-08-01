using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using System.IO;

namespace OpenDental {
	public partial class FormSnomeds:Form {
		public bool IsSelectionMode;
		public Snomed SelectedSnomed;
		private List<Snomed> SnomedList;
		private bool changed;

		public FormSnomeds() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSnomeds_Load(object sender,EventArgs e) {
			if(IsSelectionMode) {
				butClose.Text=Lan.g(this,"Cancel");
			}
			else {
				butOK.Visible=false;
			}
		}
		
		private void butSearch_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			Cursor=Cursors.WaitCursor;
			SnomedList=Snomeds.GetByCodeOrDescription(textCode.Text);
			listMain.Items.Clear();
			if(SnomedList.Count>10000) {//List runs out of memory at 1147389 items. Tried allowing one million, took 10 minutes to load.
				MessageBox.Show(SnomedList.Count+Lan.g(this," results. Only the first 10000 results will be shown."));
			}
			int itemsInList=Math.Min(SnomedList.Count,10000);
			for(int i=0;i<itemsInList;i++) {
				listMain.Items.Add(SnomedList[i].SnomedCode+" - "+SnomedList[i].Description);
			}
			Cursor=Cursors.Default;
		}

		private void butImport_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Snomed Codes will be cleared and and completely replaced with the codes in the file you are importing. (This will not affect codes currently in use.) Continue anyways?")) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			OpenFileDialog Dlg=new OpenFileDialog();
			if(Directory.Exists(PrefC.GetString(PrefName.ExportPath))) {
				Dlg.InitialDirectory=PrefC.GetString(PrefName.ExportPath);
			}
			else if(Directory.Exists("C:\\")) {
				Dlg.InitialDirectory="C:\\";
			}
			if(Dlg.ShowDialog()!=DialogResult.OK) {
				Cursor=Cursors.Default;
				return;
			}
			if(!File.Exists(Dlg.FileName)) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"File not found");
				return;
			}
			string[] fields;
			Snomed snomed;
			using(StreamReader sr=new StreamReader(Dlg.FileName)) {
				string line=sr.ReadLine();
				//Fields are: 0-id, 1-effectiveTime, 2-active, 3-moduleId, 4-conceptId, 5-languageCode, 6-typeId, 7-term, 8-caseSignificanceId
				fields=line.Split(new string[1] { "\t" },StringSplitOptions.None);
				if(fields.Length<8) {//We will attempt to access fieds 4 - conceptId (SnomedCode) and 7 - term (Description). 0 indexed so field 7 is the 8th field.
					MsgBox.Show(this,"You have selected the wrong file. There should be 9 columns in this file.");
					return;
				}
				if(fields[4]!="conceptId" || fields[7]!="term") {//Headers in first line have the wrong names.
					MsgBox.Show(this,"You have selected the wrong file: \"conceptId\" and \"term\" are not columns 5 and 8.");
					return;//Headers are not right. Wrong file.
				}
				else {//Headers in first line have the right names. Continue.
					line=sr.ReadLine();
				}
				Snomeds.DeleteAll();//Last thing we do before looping through and adding new snomeds is to delete all the old snomeds.
				while(line!=null) {
					Cursor=Cursors.WaitCursor;
					//Fields are: 0-id, 1-effectiveTime, 2-active, 3-moduleId, 4-conceptId, 5-languageCode, 6-typeId, 7-term, 8-caseSignificanceId
					fields=line.Split(new string[1] { "\t" },StringSplitOptions.None);
					if(fields.Length<8) {//We will attempt to access fieds 4 - conceptId (SnomedCode) and 7 - term (Description).
						sr.ReadLine();
						continue;
					}
					snomed=new Snomed();
					snomed.SnomedCode=fields[4];
					snomed.Description=fields[7];
					Snomeds.Insert(snomed);
					line=sr.ReadLine();
				}
			}
			Cursor=Cursors.Default;
			MsgBox.Show(this,"Import successful.");
		}

		private void listMain_DoubleClick(object sender,System.EventArgs e) {
			if(listMain.SelectedIndex==-1) {
				return;
			}
			if(IsSelectionMode) {
				SelectedSnomed=SnomedList[listMain.SelectedIndex];
				DialogResult=DialogResult.OK;
				return;
			}
			changed=true;
			FormSnomedEdit FormI=new FormSnomedEdit(SnomedList[listMain.SelectedIndex]);
			FormI.ShowDialog();
			if(FormI.DialogResult!=DialogResult.OK) {
				return;
			}
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			changed=true;
			ICD9 icd9=new ICD9();
			FormIcd9Edit FormI=new FormIcd9Edit(icd9);
			FormI.IsNew=true;
			FormI.ShowDialog();
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			//not even visible unless IsSelectionMode
			if(listMain.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			SelectedSnomed=SnomedList[listMain.SelectedIndex];
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void listView1_DoubleClick(object sender,EventArgs e) {

		}

	

	}
}