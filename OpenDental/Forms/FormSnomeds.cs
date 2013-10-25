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
			ActiveControl=textCode;
			if(!FormEHR.QuarterlyKeyIsValid(DateTime.Now.ToString("yy")//year
				,Math.Ceiling(DateTime.Now.Month/3f).ToString()//quarter
				,PrefC.GetString(PrefName.PracticeTitle)//practice title
				,EhrQuarterlyKeys.GetKeyThisQuarter().KeyValue))//key
			{
				groupBox1.Visible=false;
			}
		}
		
		private void butSearch_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("SNOMED Code",100);
			gridMain.Columns.Add(col);
			//col=new ODGridColumn("Deprecated",75,HorizontalAlignment.Center);
			//gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",500);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Used By CQM's",75);
			gridMain.Columns.Add(col);
			//col=new ODGridColumn("Date Of Standard",100);
			//gridMain.Columns.Add(col);
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
			List<ODGridRow> listAll=new List<ODGridRow>();
			for(int i=0;i<SnomedList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(SnomedList[i].SnomedCode);
				//row.Cells.Add("");//IsActive==NotDeprecated
				row.Cells.Add(SnomedList[i].Description);
				row.Cells.Add(EhrCodes.GetMeasureIdsForCode(SnomedList[i].SnomedCode,"SNOMEDCT"));
				row.Tag=SnomedList[i];
				//row.Cells.Add("");
				listAll.Add(row);
			}
			listAll.Sort(SortMeasuresMet);
			for(int i=0;i<listAll.Count;i++) {
				gridMain.Rows.Add(listAll[i]);
			}
			gridMain.EndUpdate();
		}

		///<summary>Sort function to put the codes that apply to the most number of CQM's at the top so the user can see which codes they should select.</summary>
		private int SortMeasuresMet(ODGridRow row1,ODGridRow row2) {
			//First sort by the number of measures the codes apply to in a comma delimited list
			int diff=row2.Cells[2].Text.Split(new string[] { "," },StringSplitOptions.RemoveEmptyEntries).Length-row1.Cells[2].Text.Split(new string[] { "," },StringSplitOptions.RemoveEmptyEntries).Length;
			if(diff!=0) {
				return diff;
			}
			try {
				//if the codes apply to the same number of CQMs, order by the code values
				return PIn.Long(row1.Cells[0].Text).CompareTo(PIn.Long(row2.Cells[0].Text));
			}
			catch(Exception ex) {
				return 0;
			}
		}

		//private void butImport_Click(object sender,EventArgs e) {
		//	if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Snomed Codes will be cleared and and completely replaced with the codes in the file you are importing.  This will not damage patient records, but will reset any Snomed descriptions that had been changed.  Continue anyway?")) {
		//		return;
		//	}
		//	Cursor=Cursors.WaitCursor;
		//	OpenFileDialog Dlg=new OpenFileDialog();
		//	if(Directory.Exists(PrefC.GetString(PrefName.ExportPath))) {
		//		Dlg.InitialDirectory=PrefC.GetString(PrefName.ExportPath);
		//	}
		//	else if(Directory.Exists("C:\\")) {
		//		Dlg.InitialDirectory="C:\\";
		//	}
		//	if(Dlg.ShowDialog()!=DialogResult.OK) {
		//		Cursor=Cursors.Default;
		//		return;
		//	}
		//	if(!File.Exists(Dlg.FileName)) {
		//		Cursor=Cursors.Default;
		//		MsgBox.Show(this,"File not found");
		//		return;
		//	}
		//	string[] fields;
		//	Snomed snomed;
		//	using(StreamReader sr=new StreamReader(Dlg.FileName)) {
		//		//string line=sr.ReadLine();
		//		//Fields are: 0-id, 1-effectiveTime, 2-active, 3-moduleId, 4-conceptId, 5-languageCode, 6-typeId, 7-term, 8-caseSignificanceId
		//		fields=sr.ReadLine().Split(new string[] { "\t" },StringSplitOptions.None);
		//		if(fields.Length<8) {//We will attempt to access fields 4 - conceptId (SnomedCode) and 7 - term (Description). 0 indexed so field 7 is the 8th field.
		//			MsgBox.Show(this,"You have selected the wrong file. There should be 9 columns in this file.");
		//			return;
		//		}
		//		if(fields[4]!="conceptId" || fields[7]!="term") {//Headers in first line have the wrong names.
		//			MsgBox.Show(this,"You have selected the wrong file: \"conceptId\" and \"term\" are not columns 5 and 8.");
		//			return;//Headers are not right. Wrong file.
		//		}
		//		Cursor=Cursors.WaitCursor;
		//		Cursor=Cursors.WaitCursor;
		//		Snomeds.DeleteAll();//Last thing we do before looping through and adding new snomeds is to delete all the old snomeds.
		//		while(!sr.EndOfStream) {					//line=sr.ReadLine();
		//			//Fields are: 0-id, 1-effectiveTime, 2-active, 3-moduleId, 4-conceptId, 5-languageCode, 6-typeId, 7-term, 8-caseSignificanceId
		//			fields=sr.ReadLine().Split(new string[1] { "\t" },StringSplitOptions.None);
		//			if(fields.Length<8) {//We will attempt to access fieds 4 - conceptId (SnomedCode) and 7 - term (Description).
		//				sr.ReadLine();
		//				continue;
		//			}
		//			if(fields[6]!="900000000000003001") {//full qualified name(FQN), alternative is "900000000000013009", "Synonym"
		//				continue;//skip anything that is not an FQN
		//			}
		//			snomed=new Snomed();
		//			snomed.SnomedCode=fields[4];
		//			snomed.Description=fields[7];
		//			//snomed.DateOfStandard=DateTime.MinValue();//=PIn.Date(""+fields[1].Substring(4,2)+"/"+fields[1].Substring(6,2)+"/"+fields[1].Substring(0,4));//format from yyyyMMdd to MM/dd/yyyy
		//			//snomed.IsActive=(fields[2]=="1");//true if column equals 1, false if column equals 0 or anything else.
		//			Snomeds.Insert(snomed);
		//		}
		//	}
		//	Cursor=Cursors.Default;
		//	MsgBox.Show(this,"Import successful.");
		//}

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
				SelectedSnomed=(Snomed)gridMain.Rows[e.Row].Tag;
				DialogResult=DialogResult.OK;
				return;
			}
			changed=true;
			FormSnomedEdit FormSE=new FormSnomedEdit((Snomed)gridMain.Rows[e.Row].Tag);
			FormSE.ShowDialog();
			//if(FormSE.DialogResult!=DialogResult.OK) {
			//	return;
			//}
			//FillGrid();
		}

		/*private void butAdd_Click(object sender,EventArgs e) {
			//TODO: Either change to adding a snomed code instead of an ICD9 or don't allow users to add SNOMED codes other than importing.
			changed=true;
			Snomed snomed=new Snomed();
			FormSnomedEdit FormI=new FormSnomedEdit(snomed);
			FormI.IsNew=true;
			FormI.ShowDialog();
			FillGrid();
		}*/

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

//		private void butRSIT_Click(object sender,EventArgs e) {
//			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This tool is being used by Ryan to process the raw SNOMED data. Push Cancel if you do not know what this tool is doing.")) {
//				return;
//			}
//			Cursor=Cursors.WaitCursor;
//						string command="DROP TABLE IF EXISTS snomedonly.snomedusraw";
//						DataCore.NonQ(command);	
//						command=@"CREATE TABLE snomedonly.snomedusraw ("
//									//snomedrawNum bigint NOT NULL auto_increment PRIMARY KEY,
//								+@"id VarChar(20) NOT NULL,
//									effectiveTime VarChar(8) NOT NULL,
//									active VarChar(1) NOT NULL,
//									moduleId VarChar(20) NOT NULL,
//									conceptId VarChar(20) NOT NULL,
//									languageCode VarChar(2) NOT NULL,
//									typeId VarChar(20) NOT NULL,
//									term text NOT NULL,
//									caseSignificanceId VarChar(20) NOT NULL,"
//								//INDEX(snomedrawNum),
//								+@"INDEX(id),
//									INDEX(active),
//									INDEX(moduleId),
//									INDEX(conceptId),
//									INDEX(languageCode),
//									INDEX(typeId),
//									INDEX(caseSignificanceId)
//									) DEFAULT CHARSET=utf8";
//						DataCore.NonQ(command);
//						//Load raw data into DB
//						string[] lines=File.ReadAllLines(@"C:\Docs\SNOMEDUS.TXT");
//						for(int i=1;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
//						//foreach(string line in lines){
//							string[] arraysnomed=lines[i].Split(new string[] { "\t" },StringSplitOptions.None);
//							command=@"INSERT INTO snomedonly.snomedusraw VALUES (";
//								for(int j=0;j<arraysnomed.Length;j++){
//									command+="'"+POut.String(arraysnomed[j])+"'"+",";
//								}
//							command=command.Trim(',')+")";
//							DataCore.NonQ(command);
//						}
//			//Manipulate here.
//			//900000000000013009 is synonym
//			//900000000000003001 is Fully specified name
//			command="DROP TABLE IF EXISTS snomedonly.snomed";
//			DataCore.NonQ(command);
//			command=@"CREATE TABLE snomedonly.snomed (
//						SnomedNum bigint NOT NULL auto_increment PRIMARY KEY,
//						SnomedCode VarChar(255) NOT NULL,
//						Description VarChar(255) NOT NULL,
//						INDEX(SnomedCode)
//						) DEFAULT CHARSET=utf8";
//			DataCore.NonQ(command);
//			//Load raw data into DB
//			//command="INSERT INTO snomedonly.snomed (SnomedCode,Description) SELECT t.* FROM (SELECT conceptID, term FROM snomedonly.snomedusraw WHERE GROUP BY conceptid) t";
//			command="SELECT t.* FROM (SELECT conceptID, term FROM snomedonly.snomedusraw WHERE typeid='900000000000003001' ORDER BY Cast(conceptid as unsigned) asc) t";
//			//DataCore.NonQ(command);
//			DataTable Table=DataCore.GetTable(command);
//			HashSet<string> hss=new HashSet<string>();
//			//string[] 
//			lines=File.ReadAllLines(@"C:\Docs\SNOMEDUS.TXT");
//			for(int i=1;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
//				//foreach(string line in lines){
//				string[] arraysnomed=lines[i].Split(new string[] { "\t" },StringSplitOptions.None);
//				//if(arraysnomed[6]!="900000000000003001") {
//				//	continue;//not equal to a fully specified name.
//				//}
//				if(hss.Contains(arraysnomed[4])) {
//					continue;//snomedcode already added.
//				}
//				hss.Add(arraysnomed[4]);
//				command=@"INSERT INTO snomedonly.snomed VALUES ("+i+",'"+POut.String(arraysnomed[4])+"','"+POut.String(arraysnomed[7])+"')";
//				DataCore.NonQ(command);
//			}
//			Cursor=Cursors.Default;
//			MsgBox.Show(this,"Done.");
//		}

		private void butOK_Click(object sender,EventArgs e) {
			//not even visible unless IsSelectionMode
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			SelectedSnomed=(Snomed)gridMain.Rows[gridMain.GetSelectedIndex()].Tag;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

	}
}