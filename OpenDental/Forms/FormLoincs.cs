using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using OpenDental;

namespace OpenDental {
	public partial class FormLoincs:Form {
		public bool IsSelectionMode;
		public Loinc SelectedLoinc;
		private List<Loinc> listLoincSearch;
		public Loinc LoincCur;

		public FormLoincs() {
			InitializeComponent();
		}

		private void FormLoincPicker_Load(object sender,EventArgs e) {
			listLoincSearch=new List<Loinc>();
		}

		private void fillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("Loinc Code",80);//,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Status",80);//,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Long Name",500);//,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("UCUM Units",100);//,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Order or Observation",100);//,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			listLoincSearch=Loincs.GetBySearchString(textCode.Text);
			for(int i=0;i<listLoincSearch.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listLoincSearch[i].LoincCode);
				row.Cells.Add(listLoincSearch[i].StatusOfCode);
				row.Cells.Add(listLoincSearch[i].NameLongCommon);
				row.Cells.Add(listLoincSearch[i].UnitsUCUM);
				row.Cells.Add(listLoincSearch[i].OrderObs);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(IsSelectionMode) {
				SelectedLoinc=listLoincSearch[e.Row];
				DialogResult=DialogResult.OK;
			}
			//Nothing to do if not selection mode
		}

		//private void butLoincFill_Click_Old(object sender,EventArgs e) {
		//  //string loincFilePath=@"C:\Users\Ryan\Desktop\LoincDB.txt";
		//  OpenFileDialog ofd=new OpenFileDialog();
		//  ofd.Title=Lan.g(this,"Please select the LoincDB.TXT file that contains the Loinc Codes.");
		//  ofd.Multiselect=false;
		//  ofd.ShowDialog();
		//  //Validate selected file---------------------------------------------------------------------------------------------------------------
		//  if(!ofd.FileName.ToLower().EndsWith(".txt")) {
		//    MsgBox.Show(this,"You must select a text file.");
		//  }
		//  try {
		//    System.IO.StreamReader sr=new System.IO.StreamReader(ofd.FileName);
		//    sr.Peek();
		//  }
		//  catch (Exception ex){
		//    MessageBox.Show(this,Lan.g(this,"Error reading file")+":\r\n"+ex.Message);
		//  }
		//  Cursor=Cursors.WaitCursor;
		//  //This code is useful for debugging and should be identical to the code found in convertDB3.
		//  //string command="DROP TABLE IF EXISTS loinc";
		//  //DataCore.NonQ(command);
		//  //command=@"CREATE TABLE loinc (
		//  //			LoincNum BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
		//  //			LoincCode VARCHAR(255) NOT NULL,
		//  //			UCUMUnits VARCHAR(255) NOT NULL,
		//  //			LongName VARCHAR(255) NOT NULL,
		//  //			ShortName VARCHAR(255) NOT NULL,
		//  //			OrderObs VARCHAR(255) NOT NULL
		//  //			) DEFAULT CHARSET=utf8";
		//  //DataCore.NonQ(command);
		//  try {
		//    System.IO.StreamReader sr=new System.IO.StreamReader(ofd.FileName);
		//    while(!sr.ReadLine().StartsWith("<----Clip Here for Data----->")) {//fast forward through the header text to the data table.
		//      if(sr.EndOfStream) {//prevent infinite loop in case we read through the whole file.
		//        throw new Exception(Lan.g(this,"Reached end of file before finding data."));
		//        //break;
		//      }
		//    }
		//    string[] headers=sr.ReadLine().Split('\t');
		//    if(headers.Length!=48) {
		//      throw new Exception(Lan.g(this,"File contains unexpected number of columns."));
		//    }
		//    if(headers[0].Trim('\"') !="Loinc_NUM"
		//      || headers[39].Trim('\"')!="EXAMPLE_UCUM_UNITS" 
		//      || headers[34].Trim('\"')!="LONG_COMMON_NAME" 
		//      || headers[28].Trim('\"')!="SHORTNAME" 
		//      || headers[29].Trim('\"')!="ORDER_OBS") 
		//    {
		//      throw new Exception(Lan.g(this,"Column names mismatch."));
		//    }
		//    //Import Loinc Codes----------------------------------------------------------------------------------------------------------
		//    string[] arrayLoinc;
		//    Loinc loincTemp=new Loinc();
		//    while(!sr.EndOfStream) {//each loop should read exactly one line of code. and each line of code should be a unique Loinc code
		//      arrayLoinc=sr.ReadLine().Split('\t');
		//      loincTemp.LoincCode		=arrayLoinc[0].Trim('\"');
		//      //loincTemp.UCUMUnits		=arrayLoinc[39].Trim('\"');
		//      //loincTemp.LongName		=arrayLoinc[34].Trim('\"');
		//      //loincTemp.ShortName		=arrayLoinc[28].Trim('\"');
		//      loincTemp.OrderObs		=arrayLoinc[29].Trim('\"');
		//      Loincs.Insert(loincTemp);
		//    }
		//  }
		//  catch(Exception ex) {
		//    MessageBox.Show(this,Lan.g(this,"Error importing Loinc codes:")+"\r\n"+ex.Message);
		//  }
		//  Cursor=Cursors.Default;
		//}

//		private void butLoincFill_Click(object sender,EventArgs e) {
//			//Maybe this should access our servers and then download the file directly from us.
//			MsgBox.Show(this,"This can take several minutes to import the Loinc table.");
//			OpenFileDialog ofd=new OpenFileDialog();
//			ofd.Title=Lan.g(this,"Please select the LoincDB.TXT file that contains the Loinc Codes.");
//			ofd.Multiselect=false;
//			if(ofd.ShowDialog()!=DialogResult.OK) {
//				return;
//			}
//			//Validate selected file---------------------------------------------------------------------------------------------------------------
//			if(!ofd.FileName.ToLower().EndsWith(".txt")) {
//				MsgBox.Show(this,"You must select a text file.");
//			}
//			try {
//				System.IO.StreamReader sr=new System.IO.StreamReader(ofd.FileName);
//				sr.Peek();
//			}
//			catch(Exception ex) {
//				MessageBox.Show(this,Lan.g(this,"Error reading file")+":\r\n"+ex.Message);
//			}
//			Cursor=Cursors.WaitCursor;
//#if DEBUG
//			string command="DROP TABLE IF EXISTS loinc";
//			DataCore.NonQ(command);
//			command=@"CREATE TABLE loinc (
//						LoincNum bigint NOT NULL auto_increment PRIMARY KEY,
//						LoincCode varchar(255) NOT NULL,
//						Component varchar(255) NOT NULL,
//						PropertyObserved varchar(255) NOT NULL,
//						TimeAspct varchar(255) NOT NULL,
//						SystemMeasured varchar(255) NOT NULL,
//						ScaleType varchar(255) NOT NULL,
//						MethodType varchar(255) NOT NULL,
//						StatusOfCode varchar(255) NOT NULL,
//						NameShort varchar(255) NOT NULL,
//						ClassType int NOT NULL,
//						UnitsRequired tinyint NOT NULL,
//						OrderObs varchar(255) NOT NULL,
//						HL7FieldSubfieldID varchar(255) NOT NULL,
//						ExternalCopyrightNotice text NOT NULL,
//						NameLongCommon varchar(255) NOT NULL,
//						UnitsUCUM varchar(255) NOT NULL,
//						RankCommonTests int NOT NULL,
//						RankCommonOrders int NOT NULL
//						) DEFAULT CHARSET=utf8";
//			DataCore.NonQ(command);
//#endif
//			try {
//				System.IO.StreamReader sr=new System.IO.StreamReader(ofd.FileName);
//				while(!sr.ReadLine().StartsWith("<----Clip Here for Data----->")) {//fast forward through the header text to the data table.
//					if(sr.EndOfStream) {//prevent infinite loop in case we read through the whole file.
//						throw new Exception(Lan.g(this,"Reached end of file before finding data."));
//						//break;
//					}
//				}
//				string[] headers=sr.ReadLine().Split('\t');
//				if(headers.Length!=48) {
//					throw new Exception(Lan.g(this,"File contains unexpected number of columns."));
//				}
//				if(headers[0].Trim('\"') !="Loinc_NUM"
//					|| headers[39].Trim('\"')!="EXAMPLE_UCUM_UNITS" 
//					|| headers[34].Trim('\"')!="LONG_COMMON_NAME" 
//					|| headers[28].Trim('\"')!="SHORTNAME" 
//					|| headers[29].Trim('\"')!="ORDER_OBS") {
//					//TODO: expand this to check all columns, though this should be sufficient.
//					throw new Exception(Lan.g(this,"Column names mismatch."));
//				}
//				//Import Loinc Codes----------------------------------------------------------------------------------------------------------
//				string[] arrayLoinc;
//				Loinc loincTemp=new Loinc();
//				while(!sr.EndOfStream) {//each loop should read exactly one line of code. and each line of code should be a unique Loinc code
//					arrayLoinc=sr.ReadLine().Split('\t');
//					loincTemp.LoincCode								=arrayLoinc[0].Trim('\"');
//					loincTemp.Component								=arrayLoinc[1].Trim('\"');
//					loincTemp.PropertyObserved				=arrayLoinc[2].Trim('\"');
//					loincTemp.TimeAspct								=arrayLoinc[3].Trim('\"');
//					loincTemp.SystemMeasured					=arrayLoinc[4].Trim('\"');
//					loincTemp.ScaleType								=arrayLoinc[5].Trim('\"');
//					loincTemp.MethodType							=arrayLoinc[6].Trim('\"');
//					loincTemp.StatusOfCode						=arrayLoinc[12].Trim('\"');
//					//loincTemp.ClassType								=PIn.Int(arrayLoinc[15].Trim('\"'));
//					loincTemp.UnitsRequired						=arrayLoinc[25].Trim('\"')=="Y";
//					loincTemp.NameShort								=arrayLoinc[28].Trim('\"');
//					loincTemp.OrderObs								=arrayLoinc[29].Trim('\"');
//					loincTemp.HL7FieldSubfieldID			=arrayLoinc[31].Trim('\"');
//					loincTemp.ExternalCopyrightNotice	=arrayLoinc[32].Trim('\"');
//					loincTemp.NameLongCommon					=arrayLoinc[34].Trim('\"');
//					loincTemp.UnitsUCUM								=arrayLoinc[39].Trim('\"');
//					loincTemp.RankCommonOrders				=PIn.Int(arrayLoinc[44].Trim('\"'));
//					loincTemp.RankCommonTests					=PIn.Int(arrayLoinc[45].Trim('\"'));
//					Loincs.Insert(loincTemp);
//				}
//			}
//			catch(Exception ex) {
//				MessageBox.Show(this,Lan.g(this,"Error importing Loinc codes:")+"\r\n"+ex.Message);
//			}
//			Cursor=Cursors.Default;
//			fillGrid();
//		}

		private void butSearch_Click(object sender,EventArgs e) {
			fillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a Loinc code from the list.");
				return;
			}
			if(IsSelectionMode) {
				SelectedLoinc=listLoincSearch[gridMain.GetSelectedIndex()];
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}
