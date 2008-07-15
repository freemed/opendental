using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class LabelSingle{
		//private PrintDocument pd;
		//private Patient Pat;
		//private Carrier CarrierCur;
		//private Referral ReferralCur;

		///<summary></summary>
		public LabelSingle(){
			
		}

		///<summary></summary>
		public void PrintPat(int patNum){
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelPatientMail);
			sheetDef.SetParameter("PatNum",patNum);
			SheetFiller.FillFields(sheetDef);
			try{
				SheetPrinting.Print(sheetDef);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatientLFAddress(int patNum) {
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelPatientLFAddress);
			sheetDef.SetParameter("PatNum",patNum);
			SheetFiller.FillFields(sheetDef);
			try {
				SheetPrinting.Print(sheetDef);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatientLFChartNumber(int patNum) {
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelPatientLFChartNumber);
			sheetDef.SetParameter("PatNum",patNum);
			SheetFiller.FillFields(sheetDef);
			try {
				SheetPrinting.Print(sheetDef);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatientLFPatNum(int patNum) {
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelPatientLFPatNum);
			sheetDef.SetParameter("PatNum",patNum);
			SheetFiller.FillFields(sheetDef);
			try {
				SheetPrinting.Print(sheetDef);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatRadiograph(int patNum) {
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelPatientRadiograph);
			sheetDef.SetParameter("PatNum",patNum);
			SheetFiller.FillFields(sheetDef);
			try {
				SheetPrinting.Print(sheetDef);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		///<summary></summary>
		public void PrintCarriers(List<int> carrierNums){
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelCarrier);
			List<SheetDef> sheetDefBatch=SheetUtil.CreateBatch(sheetDef,carrierNums);
			try{
				SheetPrinting.PrintBatch(sheetDefBatch);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		///<summary>Have to supply printer name because this can be called multiple times in a loop. Returns false if fails.</summary>
		public void PrintCarrier(int carrierNum){//Carrier carrierCur,string printerName){
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelCarrier);
			sheetDef.SetParameter("CarrierNum",carrierNum);
			SheetFiller.FillFields(sheetDef);
			try {
				SheetPrinting.Print(sheetDef);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		///<summary></summary>
		public void PrintReferral(int referralNum) {
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelReferral);
			sheetDef.SetParameter("ReferralNum",referralNum);
			SheetFiller.FillFields(sheetDef);
			try {
				SheetPrinting.Print(sheetDef);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		

	}

}
