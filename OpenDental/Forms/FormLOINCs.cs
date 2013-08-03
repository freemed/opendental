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
	public partial class FormLOINCs:Form {
		private List<LOINC> listLOINCSearch;
		public LOINC LOINCCur;

		public FormLOINCs() {
			InitializeComponent();
		}

		private void FormLOINCPicker_Load(object sender,EventArgs e) {
#if !DEBUG//just in case
			butLOINCFill.Visible=false;
#endif
			listLOINCSearch=new List<LOINC>();
			fillGrid();
		}

		private void fillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("LOINC Code",80);//,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Long Name",500);//,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("UCUM Units",80);//,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Order or Observation",100);//,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			listLOINCSearch=LOINCs.GetBySearchString(textCode.Text);
			for(int i=0;i<listLOINCSearch.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listLOINCSearch[i].LOINCCode);
				row.Cells.Add(listLOINCSearch[i].LongName);
				row.Cells.Add(listLOINCSearch[i].UCUMUnits);
				row.Cells.Add(listLOINCSearch[i].OrderObs);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//MedicationCur=Medications.Listt[e.Row];
			//DialogResult=DialogResult.OK;
		}

		private void butLOINCFill_Click(object sender,EventArgs e) {
			//string loincFilePath=@"C:\Users\Ryan\Desktop\LOINCDB.txt";
			OpenFileDialog ofd=new OpenFileDialog();
			ofd.Title=Lan.g(this,"Please select the LOINCDB.TXT file that contains the LOINC Codes.");
			ofd.Multiselect=false;
			ofd.ShowDialog();
			//Validate selected file---------------------------------------------------------------------------------------------------------------
			if(!ofd.FileName.ToLower().EndsWith(".txt")) {
				MsgBox.Show(this,"You must select a text file.");
			}
			try {
				System.IO.StreamReader sr=new System.IO.StreamReader(ofd.FileName);
				sr.Peek();
			}
			catch (Exception ex){
				MessageBox.Show(this,Lan.g(this,"Error reading file")+":\r\n"+ex.Message);
			}
			Cursor=Cursors.WaitCursor;
			//This code is useful for debugging and should be identical to the code found in convertDB3.
			//string command="DROP TABLE IF EXISTS loinc";
			//DataCore.NonQ(command);
			//command=@"CREATE TABLE loinc (
			//			LOINCNum BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
			//			LOINCCode VARCHAR(255) NOT NULL,
			//			UCUMUnits VARCHAR(255) NOT NULL,
			//			LongName VARCHAR(255) NOT NULL,
			//			ShortName VARCHAR(255) NOT NULL,
			//			OrderObs VARCHAR(255) NOT NULL
			//			) DEFAULT CHARSET=utf8";
			//DataCore.NonQ(command);
			try {
				System.IO.StreamReader sr=new System.IO.StreamReader(ofd.FileName);
				while(!sr.ReadLine().StartsWith("<----Clip Here for Data----->")) {//fast forward through the header text to the data table.
					if(sr.EndOfStream) {//prevent infinite loop in case we read through the whole file.
						throw new Exception(Lan.g(this,"Reached end of file before finding data."));
						//break;
					}
				}
				string[] headers=sr.ReadLine().Split('\t');
				if(headers.Length!=46) {
					throw new Exception(Lan.g(this,"File contains unexpected number of columns."));
				}
				if(headers[0].Trim('\"') !="LOINC_NUM"
					|| headers[39].Trim('\"')!="EXAMPLE_UCUM_UNITS" 
					|| headers[34].Trim('\"')!="LONG_COMMON_NAME" 
					|| headers[28].Trim('\"')!="SHORTNAME" 
					|| headers[29].Trim('\"')!="ORDER_OBS") 
				{
					throw new Exception(Lan.g(this,"Column names mismatch."));
				}
				//Import LOINC Codes----------------------------------------------------------------------------------------------------------
				string[] arrayLOINC;
				LOINC loincTemp=new LOINC();
				while(!sr.EndOfStream) {//each loop should read exactly one line of code. and each line of code should be a unique LOINC code
					arrayLOINC=sr.ReadLine().Split('\t');
					loincTemp.LOINCCode		=arrayLOINC[0].Trim('\"');
					loincTemp.UCUMUnits		=arrayLOINC[39].Trim('\"');
					loincTemp.LongName		=arrayLOINC[34].Trim('\"');
					loincTemp.ShortName		=arrayLOINC[28].Trim('\"');
					loincTemp.OrderObs		=arrayLOINC[29].Trim('\"');
					LOINCs.Insert(loincTemp);
				}
			}
			catch(Exception ex) {
				MessageBox.Show(this,Lan.g(this,"Error importing LOINC codes:")+"\r\n"+ex.Message);
			}
			Cursor=Cursors.Default;
		}

		private void butSearch_Click(object sender,EventArgs e) {
			fillGrid();
		}

		private void FillSNOMEDTemp() {

		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}
