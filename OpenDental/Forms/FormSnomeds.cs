using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using System.IO;
using OpenDental.UI;

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

		private void FillGridOld() {
			Cursor=Cursors.WaitCursor;
			long resultCount=Snomeds.GetCountSearch(textCode.Text);
			if(resultCount>10000) {//List runs out of memory at 1147389 items. Tried allowing one million, took 10 minutes to load.
				MessageBox.Show(resultCount+Lan.g(this," results. Only the first 10000 results will be shown."));
			}
			Cursor=Cursors.Default;
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("SNOMED Code",100,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Depricated",75,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",270);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Date Of Standard",100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			SnomedList=Snomeds.GetByCodeOrDescription(textCode.Text);
			for(int i=0;i<SnomedList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(SnomedList[i].SnomedCode);
				row.Cells.Add((SnomedList[i].IsActive?"":"X"));//IsActive==NotDepricated
				row.Cells.Add(SnomedList[i].Description);
				row.Cells.Add(SnomedList[i].DateOfStandard.ToShortDateString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
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
				//string line=sr.ReadLine();
				//Fields are: 0-id, 1-effectiveTime, 2-active, 3-moduleId, 4-conceptId, 5-languageCode, 6-typeId, 7-term, 8-caseSignificanceId
				fields=sr.ReadLine().Split(new string[] { "\t" },StringSplitOptions.None);
				if(fields.Length<8) {//We will attempt to access fields 4 - conceptId (SnomedCode) and 7 - term (Description). 0 indexed so field 7 is the 8th field.
					MsgBox.Show(this,"You have selected the wrong file. There should be 9 columns in this file.");
					return;
				}
				if(fields[4]!="conceptId" || fields[7]!="term") {//Headers in first line have the wrong names.
					MsgBox.Show(this,"You have selected the wrong file: \"conceptId\" and \"term\" are not columns 5 and 8.");
					return;//Headers are not right. Wrong file.
				}
				Cursor=Cursors.WaitCursor;
				Cursor=Cursors.WaitCursor;
				Snomeds.DeleteAll();//Last thing we do before looping through and adding new snomeds is to delete all the old snomeds.
				while(!sr.EndOfStream) {					//line=sr.ReadLine();
					//Fields are: 0-id, 1-effectiveTime, 2-active, 3-moduleId, 4-conceptId, 5-languageCode, 6-typeId, 7-term, 8-caseSignificanceId
					fields=sr.ReadLine().Split(new string[1] { "\t" },StringSplitOptions.None);
					if(fields.Length<8) {//We will attempt to access fieds 4 - conceptId (SnomedCode) and 7 - term (Description).
						sr.ReadLine();
						continue;
					}
					if(fields[6]!="900000000000003001") {//full qualified name(FQN), alternative is "900000000000013009", "Synonym"
						continue;//skip anything that is not an FQN
					}
					snomed=new Snomed();
					snomed.SnomedCode=fields[4];
					snomed.Description=fields[7];
					snomed.DateOfStandard=PIn.Date(""+fields[1].Substring(4,2)+"/"+fields[1].Substring(6,2)+"/"+fields[1].Substring(0,4));//format from yyyyMMdd to MM/dd/yyyy
					snomed.IsActive=(fields[2]=="1");//true if column equals 1, false if column equals 0 or anything else.
					Snomeds.Insert(snomed);
				}
			}
			Cursor=Cursors.Default;
			MsgBox.Show(this,"Import successful.");
		}

		//private void listMain_DoubleClick(object sender,System.EventArgs e) {
		//  if(listMain.SelectedIndex==-1) {
		//    return;
		//  }
		//  if(IsSelectionMode) {
		//    SelectedSnomed=SnomedList[listMain.SelectedIndex];
		//    DialogResult=DialogResult.OK;
		//    return;
		//  }
		//  changed=true;
		//  FormSnomedEdit FormI=new FormSnomedEdit(SnomedList[listMain.SelectedIndex]);
		//  FormI.ShowDialog();
		//  if(FormI.DialogResult!=DialogResult.OK) {
		//    return;
		//  }
		//  FillGrid();
		//}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(IsSelectionMode) {
				SelectedSnomed=SnomedList[e.Row];
				DialogResult=DialogResult.OK;
				return;
			}
			changed=true;
			FormSnomedEdit FormSE=new FormSnomedEdit(SnomedList[e.Row]);
			FormSE.ShowDialog();
			if(FormSE.DialogResult!=DialogResult.OK) {
				return;
			}
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			//TODO: Either change to adding a snomed code instead of an ICD9 or don't allow users to add SNOMED codes other than importing.
			changed=true;
			ICD9 icd9=new ICD9();
			FormIcd9Edit FormI=new FormIcd9Edit(icd9);
			FormI.IsNew=true;
			FormI.ShowDialog();
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			//not even visible unless IsSelectionMode
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			SelectedSnomed=SnomedList[gridMain.GetSelectedIndex()];
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

	}
}