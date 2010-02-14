using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormPatientForms:Form {
		DataTable table;
		public long PatNum;

		public FormPatientForms() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormPatientForms_Load(object sender,EventArgs e) {
			Patient pat=Patients.GetLim(PatNum);
			Text=Lan.g(this,"Patient Forms for")+" "+pat.GetNameFL();
			FillGrid();
		}

		private void FillGrid(){
			//if a sheet is selected, remember it
			long selectedSheetNum=0;
			if(gridMain.GetSelectedIndex()!=-1) {
				selectedSheetNum=PIn.Long(table.Rows[gridMain.GetSelectedIndex()]["SheetNum"].ToString());
			}
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Time"),42);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Terminal"),55,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),210);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Image Category"),120);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			table=Sheets.GetPatientFormsTable(PatNum);
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["date"].ToString());
				row.Cells.Add(table.Rows[i]["time"].ToString());
				row.Cells.Add(table.Rows[i]["showInTerminal"].ToString());
				row.Cells.Add(table.Rows[i]["description"].ToString());
				row.Cells.Add(table.Rows[i]["imageCat"].ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			if(selectedSheetNum!=0) {
				for(int i=0;i<table.Rows.Count;i++) {
					if(table.Rows[i]["SheetNum"].ToString()==selectedSheetNum.ToString()) {
						gridMain.SetSelected(i,true);
						break;
					}
				}
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(table.Rows[e.Row]["DocNum"].ToString()!="0") {
				long docNum=PIn.Long(table.Rows[e.Row]["DocNum"].ToString());
				GotoModule.GotoImage(PatNum,docNum); 
				return;
			}
			//Sheets
			long sheetNum=PIn.Long(table.Rows[e.Row]["SheetNum"].ToString());
			Sheet sheet=Sheets.GetSheet(sheetNum);
			FormSheetFillEdit FormSF=new FormSheetFillEdit(sheet);
			FormSF.ShowDialog();
			if(FormSF.DialogResult==DialogResult.OK) {
				FillGrid();
			}
		}

		private void menuItemSheets_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormSheetDefs FormSD=new FormSheetDefs();
			FormSD.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Sheets");
			FillGrid();
		}

		private void menuItemImageCats_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormDefinitions formD=new FormDefinitions(DefCat.ImageCats);
			formD.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Defs");
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormSheetPicker FormS=new FormSheetPicker();
			FormS.SheetType=SheetTypeEnum.PatientForm;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			SheetDef sheetDef;
			Sheet sheet=null;//only useful if not Terminal
			for(int i=0;i<FormS.SelectedSheetDefs.Count;i++) {
				sheetDef=FormS.SelectedSheetDefs[i];
				sheet=SheetUtil.CreateSheet(sheetDef,PatNum);
				SheetParameter.SetParameter(sheet,"PatNum",PatNum);
				SheetFiller.FillFields(sheet);
				SheetUtil.CalculateHeights(sheet,this.CreateGraphics());
				if(FormS.TerminalSend) {
					sheet.InternalNote="";//because null not ok
					sheet.ShowInTerminal=Sheets.GetBiggestShowInTerminal(PatNum)+1;
					Sheets.SaveNewSheet(sheet);//save each sheet.
				}
			}
			if(FormS.TerminalSend) {
				//do not show a dialog now.
				//User will need to click the terminal button.
				FillGrid();
			}
			else{
				FormSheetFillEdit FormSF=new FormSheetFillEdit(sheet);
				FormSF.ShowDialog();
				if(FormSF.DialogResult==DialogResult.OK) {
					FillGrid();
				}
			}
		}

		private void butTerminal_Click(object sender,EventArgs e) {
			bool hasTerminal=false;
			for(int i=0;i<table.Rows.Count;i++) {
				if(table.Rows[i]["showInTerminal"].ToString()!="") {
					hasTerminal=true;
					break;
				}
			}
			if(!hasTerminal) {
				MsgBox.Show(this,"No forms for this patient are set to show in the terminal.");
				return;
			}
			FormTerminal formT=new FormTerminal();
			formT.IsSimpleMode=true;
			formT.PatNum=PatNum;
			formT.ShowDialog();
			FillGrid();
		}
		
		private void butImport_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length !=1) {
				MsgBox.Show(this,"Please select one completed form from the list above first.");
				return;
			}
			long sheetNum=PIn.Long(table.Rows[gridMain.SelectedIndices[0]]["SheetNum"].ToString());
			long docNum=PIn.Long(table.Rows[gridMain.SelectedIndices[0]]["DocNum"].ToString());
			Document doc=null;
			if(docNum!=0) {
				doc=Documents.GetByNum(docNum);
				string extens=Path.GetExtension(doc.FileName);
				if(extens.ToLower()=="pdf") {
					MsgBox.Show(this,"Images cannot be imported into the database.");
					return;
				}
			}
			Sheet sheet=null;
			if(sheetNum!=0) {
				sheet=Sheets.GetSheet(sheetNum);
				if(sheet.SheetType!=SheetTypeEnum.PatientForm) {
					MsgBox.Show(this,"For now, only sheets of type 'PatientForm' can be imported.");
					return;
				}
			}
			FormSheetImport formSI=new FormSheetImport();
			formSI.SheetCur=sheet;
			formSI.DocCur=doc;
			formSI.ShowDialog();
			//No need to refresh grid because no changes could have been made.
		}

		private void butCancel_Click(object sender,EventArgs e) {
			Close();
		}

		

		

		

		

		

		

		
	}
}