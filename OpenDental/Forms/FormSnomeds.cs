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

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("SNOMED Code",100,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Depricated",75,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",500);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Date Of Standard",100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			if(textCode.Text.Contains(",")) {
				SnomedList=Snomeds.GetByCodes(textCode.Text);
			}
			else {
				SnomedList=Snomeds.GetByCodeOrDescription(textCode.Text);
			}
			if(SnomedList.Count>=10000) {//Max number of results returned.
				MsgBox.Show(this,"Too many results. Only the first 10,000 results will be shown.");
			}
			for(int i=0;i<SnomedList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(SnomedList[i].SnomedCode);
				row.Cells.Add("");//IsActive==NotDepricated
				row.Cells.Add(SnomedList[i].Description);
				row.Cells.Add("");
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butImport_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Snomed Codes will be cleared and and completely replaced with the codes in the file you are importing.  This will not damage patient records, but will reset any Snomed descriptions that had been changed.  Continue anyway?")) {
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
					//snomed.DateOfStandard=DateTime.MinValue();//=PIn.Date(""+fields[1].Substring(4,2)+"/"+fields[1].Substring(6,2)+"/"+fields[1].Substring(0,4));//format from yyyyMMdd to MM/dd/yyyy
					//snomed.IsActive=(fields[2]=="1");//true if column equals 1, false if column equals 0 or anything else.
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
			Snomed snomed=new Snomed();
			FormSnomedEdit FormI=new FormSnomedEdit(snomed);
			FormI.IsNew=true;
			FormI.ShowDialog();
			FillGrid();
		}

		private void butCrossMap_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This button takes a while to run. It imports two \"cross table\" files from the desktop (Ryan's computer only.) These tables are then used to create two more tables that contain a more useful form of the data. This button will probably not be part of the release version and if so it will behave much differently. \r\n\r\n Continue?\r\n (You should select CANCEL if you don't know exactly what this button does.)")) {
				return;
			}
			string snomedMap=@"C:\Users\Ryan\Downloads\SnomedCT_Release_INT_20130131\SnomedCT_Release_INT_20130131\RF1Release\CrossMaps\ICD9\der1_CrossMaps_ICD9_INT_20130131.txt";
			string icd9Targets=@"C:\Users\Ryan\Downloads\SnomedCT_Release_INT_20130131\SnomedCT_Release_INT_20130131\RF1Release\CrossMaps\ICD9\der1_CrossMapTargets_ICD9_INT_20130131.txt";
			Cursor=Cursors.WaitCursor;
			//This code is useful for debugging and should be identical to the code found in convertDB3.
			string command="DROP TABLE IF EXISTS tempcrossmap";
			DataCore.NonQ(command);
			command=@"CREATE TABLE tempcrossmap (
						TempcrossmapNum BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
						SNOMEDCode VARCHAR(255) NOT NULL,
						TargetID VARCHAR(255) NOT NULL,
						Mappable VARCHAR(255) NOT NULL
						) DEFAULT CHARSET=utf8";
			DataCore.NonQ(command); 
			command="DROP TABLE IF EXISTS tempicd9targets";
			DataCore.NonQ(command);
			command=@"CREATE TABLE tempicd9targets (
						Tempicd9targetsNum BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
						TargetID VARCHAR(255) NOT NULL,
						ICD9Codes VARCHAR(255) NOT NULL
						) DEFAULT CHARSET=utf8";
			DataCore.NonQ(command);
			//Import CrossMap Table----------------------------------------------------------------------------------------------------------
			System.IO.StreamReader sr=new System.IO.StreamReader(snomedMap);
			string[] arraySnomedMap;
			sr.ReadLine();//skip headers
			while(!sr.EndOfStream) {//each loop should read exactly one line
				arraySnomedMap=sr.ReadLine().Split('\t');
				command="INSERT INTO tempcrossmap (SNOMEDCode,TargetID,Mappable) VALUES ('"+arraySnomedMap[1]+"','"+arraySnomedMap[4]+"','"+arraySnomedMap[6]+"')";
				DataCore.NonQ(command);
			}
			//Import Target Table----------------------------------------------------------------------------------------------------------
			sr=new System.IO.StreamReader(icd9Targets);
			string[] arrayTargets;
			sr.ReadLine();//skip headers
			while(!sr.EndOfStream) {//each loop should read exactly one line
				arrayTargets=sr.ReadLine().Split('\t');
				command="INSERT INTO tempicd9targets (TargetID,ICD9Codes) VALUES ('"+arrayTargets[0]+"','"+arrayTargets[2]+"')";
				DataCore.NonQ(command);
			}
			//Import tac a code onto ICD9 codes...----------------------------------------------------------------------------------------------------------
			//command="SELECT tempcrossmap.SNOMEDCode,tempicd9targets.ICD9Codes FROM tempcrossmap,tempicd9targets WHERE tempcrossmap.TargetID=tempicd9targets.TargetID";
			//DataTable table = DataCore.GetTable(command);
			//foreach(DataRow in table.Rows){
				
			//}
			Cursor=Cursors.Default;

		}

		private void butMapToSnomed_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Will add SNOMED code to existing problems list only if the ICD9 code correlates to exactly one SNOMED code. If there is any ambiguity at all the code will not be added.")) {
				return;
			}
			int changeCount=0;
			Dictionary<string,string> dictionaryIcd9ToSnomed = Snomeds.GetICD9toSNOMEDDictionary();
			DiseaseDefs.RefreshCache();
			for(int i=0;i<DiseaseDefs.ListLong.Length;i++) {
				if(!dictionaryIcd9ToSnomed.ContainsKey(DiseaseDefs.ListLong[i].ICD9Code)) {
					continue;
				}
				DiseaseDef def=DiseaseDefs.ListLong[i];
				if(def.SnomedCode!="") {
					continue;
				}
				def.SnomedCode=dictionaryIcd9ToSnomed[def.ICD9Code];
				DiseaseDefs.Update(def);
				changeCount++;
			}
			MessageBox.Show(Lan.g(this,"SNOMED codes added: ")+changeCount);
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